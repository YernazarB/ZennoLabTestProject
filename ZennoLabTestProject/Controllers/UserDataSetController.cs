using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using ZennoLabTestProject.Dtos;
using ZennoLabTestProject.Queries;

namespace ZennoLabTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataSetController : ControllerBase
    {
        private readonly ISender _mediator;
        public UserDataSetController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserDataSetDto dto)
        {
            var message = await _mediator.Send(new CreateUserDataSetQuery {UserDataSet = dto });
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetUserDataSetsQuery()));
        }
    }
}
