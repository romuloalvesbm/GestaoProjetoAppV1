using Asp.Versioning;
using GestaoProjeto.Application.Interfaces.Services;
using GestaoProjeto.Presentation.Contracts;
using GestaoProjeto.Presentation.Contracts.Dtos;
using GestaoProjeto.Presentation.Contracts.Extensions;
using GestaoProjeto.Presentation.Contracts.v1.Common;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProjeto.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioApplicationService _funcionarioApplicationService;

        public FuncionarioController(IFuncionarioApplicationService funcionarioApplicationService)
        {
            _funcionarioApplicationService = funcionarioApplicationService;
        }

        /// <summary>
        /// Serviço para cadastrar Funcionario.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(FuncionarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] FuncionarioCadastroRequest request)
        {
            try
            {
                var result = await _funcionarioApplicationService.Create(request);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para atualizar Funcionario.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(Results), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] FuncionarioEdicaoRequest request)
        {
            try
            {
                var result = await _funcionarioApplicationService.Update(request);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para deletar Funcionario.
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(Results), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] FuncionarioExclusaoRequest request)
        {
            try
            {
                var result = await _funcionarioApplicationService.Delete(request);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para listar Funcionario.
        /// </summary>
        [HttpGet("GetAllFuncionario")]
        [ProducesResponseType(typeof(FuncionarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<FuncionarioDto>>> GetAllFuncionario([FromQuery] FuncionarioGridRequest request)
        {

            try
            {
                var result = await _funcionarioApplicationService.GetAll(request);
                return Ok(result.Value.ToPagedList(request.CurrentPage, request.PageSize));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para localizar Funcionario.
        /// </summary>
        [HttpGet("GetFuncionario")]
        [ProducesResponseType(typeof(Results), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<FuncionarioDto>>> GetFuncionario([FromQuery] Guid id)
        {
            try
            {
                var result = await _funcionarioApplicationService.GetById(id);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }
    }
}
