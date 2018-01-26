using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAppConsumer
{
    // >dotnet ef migration add testMigration in AspNet5MultipleProject
    public class DomainModelPostgreSqlContext : DbContext
    {
        public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) :base(options)
        {
            this.Database.EnsureCreated();
        }
        
        public DbSet<DataEventRecord> DataEventRecords { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);           
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");            
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();            
            updateUpdatedProperty<DataEventRecord>();
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}