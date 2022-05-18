using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CelebContracts;
using InfraContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelebApi.Controllers
{
    [ApiController]
    public class CelebController : ControllerBase
    {
        private readonly ICelebService _service;
        public CelebController(ICelebService service)
        {
            _service = service;
            if (!_service.DbExists())
            {
                _service.ResetDb();
            }
        }

        [Route("[controller]")]
        [HttpGet]
        public Response Show()
        {
            var ret = _service.GetAllCelebs();
            return ret;
        }

        [Route("[controller]/Get")]
        [HttpPost]
        public Response Get()
        {
            var ret = _service.GetAllCelebs();
            return ret;
        }

        [Route("[controller]/Reset")]
        [HttpPost]
        public Response Reset()
        {
            return _service.ResetDb();
        }

        [Route("[controller]/Remove")]
        [HttpPost]
        public Response Remove([FromBody] RemoveRuleRequest request)
        {
            return _service.Remove(request.Key);
        }

        [Route("[controller]/RemoveAll")]
        [HttpPost]
        public Response RemoveAll()
        {
            return _service.RemoveAll();
        }
    }
}
