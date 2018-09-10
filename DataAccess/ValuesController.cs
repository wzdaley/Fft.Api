using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataAccess
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet("api/value")]
        public ActionResult Get()
        {
            return Ok(":goodgood:");
        }

        [HttpGet("api/value2/{requiredParameter}")]
        public ActionResult Get2(int requiredParameter)
        {
            return Ok(":good int test of requiredValue good:");
        }

        [MapToApiVersion("2")]
        [HttpPost("api/value3")]
        public ActionResult Post([FromBody]BodyParameters bodyParameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState?.Values.FirstOrDefault().Errors?.Select(error => error?.Exception?.Message) ?? new List<string> { { "An error occured in the request." } });
            return Ok(":good int test of bodyParameters good:");
        }

        [HttpGet("api/value4")]
        [MapToApiVersion("3")] //as there is no version three declared in startup for swagger, this will not show up in swagger
        public IEnumerable<string> Get3()
        {
            return new string[] { "value1", "value2" };
        }
    }

    public class BodyParameters
    {
        [Required]
        public int MyProperty;
    }
}