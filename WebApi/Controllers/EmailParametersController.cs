using Business.Repositories.EmailParameterRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {
        private readonly IEmailParameterService _emailParameterService;

        public EmailParametersController(IEmailParameterService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }

        [HttpPost("add")]
        public IActionResult Add(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Add(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Update(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Delete(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _emailParameterService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _emailParameterService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
