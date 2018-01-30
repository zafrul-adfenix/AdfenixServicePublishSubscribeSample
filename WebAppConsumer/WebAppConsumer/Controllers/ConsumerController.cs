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
        private readonly IDataEventRepository _dataAccessProvider;

        public ConsumerController(IDataEventRepository dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        // GET api/values
        [HttpGet("{id}")]
        public DataEventRecord Get(long id)
        {
            return _dataAccessProvider.GetDataEventRecord(id);
        }

        // GET api/values
        [HttpGet]
        public IList<DataEventRecord> Get()
        {
            return _dataAccessProvider.GetDataEventRecords();
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
