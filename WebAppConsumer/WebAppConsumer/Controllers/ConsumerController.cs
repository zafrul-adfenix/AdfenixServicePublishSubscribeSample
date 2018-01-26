using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppConsumer.Controllers
{
    [Route("api/[controller]")]
    public class ConsumerController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public ConsumerController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        // GET api/values
        [HttpGet("{id}")]
        public DataEventRecord Get(long id)
        {
            return _dataAccessProvider.GetDataEventRecord(id);
        }

        [HttpPost]
        public void Post([FromBody]DataEventRecord value)
        {
            _dataAccessProvider.AddDataEventRecord(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]DataEventRecord value)
        {
            _dataAccessProvider.UpdateDataEventRecord(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _dataAccessProvider.DeleteDataEventRecord(id);
        }
    }
}
