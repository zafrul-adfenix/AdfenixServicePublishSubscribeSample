using System.Collections.Generic;

namespace WebAppConsumer
{
    public interface IDataEventRepository
    {
        void AddDataEventRecord(DataEventRecord dataEventRecord);
        void UpdateDataEventRecord(long dataEventRecordId, DataEventRecord dataEventRecord);
        void DeleteDataEventRecord(long dataEventRecordId);
        DataEventRecord GetDataEventRecord(long dataEventRecordId);
        IList<DataEventRecord> GetDataEventRecords();
    }
}
