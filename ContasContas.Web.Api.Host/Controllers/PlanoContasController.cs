using Contas.Web.Api.Data.Dto;
using Contas.Web.Api.Service.PlanoContasService.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contas.Web.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoContasController : ControllerBase
    {
        private readonly IPlanoContasService planoContasService;

        public PlanoContasController(IPlanoContasService planoContasService)
        {
            this.planoContasService = planoContasService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<PlanoContasDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.planoContasService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpGet("GetByName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<PlanoContasDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByName(string Name)
        {
            try
            {
                return Ok(this.planoContasService.GetByName(Name));
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpGet("GetById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlanoContasDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
                return Ok(await this.planoContasService.GetById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpGet("GetParents")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<PlanoContasDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetParents(string Type)
        {
            try
            {
                return Ok(this.planoContasService.GetParents(Type));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpGet("GetNextId")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNextId(string ParentId)
        {
            try
            {
                return Ok(await this.planoContasService.GetNextId(ParentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PlanoContasDto value)
        {
            try
            {
                return Ok(await this.planoContasService.AddAsync(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] PlanoContasDto value)
        {
            try
            {
                return Ok(await this.planoContasService.UpdateAsync(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok(await this.planoContasService.DeleteAsync(new PlanoContasDto { Codigo = id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message ?? ex?.Message);
            }
        }
    }
}
