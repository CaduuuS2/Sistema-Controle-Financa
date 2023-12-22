using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controller
{
    [ApiController]
    [Route("titulos-apagar")]
    public class ApagarController : BaseController
    {
        
        private readonly IApagarService _apagarService;
        private long _idUsuario;
        public ApagarController(IApagarService apagarService)
        {
            _apagarService = apagarService;
        } 

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ApagarRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _apagarService.Adicionar(contrato, _idUsuario));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(_idUsuario));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long idApagar)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(idApagar, _idUsuario));
            }
            catch(NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("natureza")]
        [Authorize]
        public async Task<IActionResult> ObterPorNatureza(long idNatureza)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.ObterPorNatureza(idNatureza, _idUsuario));
            }
            catch(NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, ApagarRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Atualizar(id, contrato, _idUsuario));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch(NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Inativar(long id, ApagarRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _apagarService.Inativar(id, _idUsuario);
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("vencimento")]
        [Authorize]
        public async Task<IActionResult> ObterPorVencimento(DateTime vencimentoInicial, DateTime vencimentoFinal)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();            
                return Ok(await _apagarService.ObterPorVencimento(_idUsuario, vencimentoInicial, vencimentoFinal));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("baixa")]
        [Authorize]
        public async Task<IActionResult> ObterTituloBaixado()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();            
                return Ok(await _apagarService.ObterTituloBaixado(_idUsuario));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}