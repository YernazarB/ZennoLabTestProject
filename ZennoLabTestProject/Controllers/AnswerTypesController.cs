using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ZennoLabTestProject.Domain;
using ZennoLabTestProject.Helpers;

namespace ZennoLabTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerTypesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var answerTypes = (AnswerType[])Enum.GetValues(typeof(AnswerType));
            return Ok(answerTypes.Select(x => new {Id = x, Name = x.AnswerTypeString()}));
        }
    }
}
