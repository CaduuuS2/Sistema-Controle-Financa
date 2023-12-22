using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controller
{
    [ApiController]
    [Route("titulos-Areceber")]
    public class AreceberController : BaseController
    {
        
        private readonly IAreceberService _areceberService;
        private long _idUsuario;
        public AreceberController(IAreceberService AreceberService)
        {
            _areceberService = AreceberService;
        } 

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(AreceberRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _areceberService.Adicionar(contrato, _idUsuario));
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.Obter(_idUsuario));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long idAreceber)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.Obter(idAreceber, _idUsuario));
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
                return Ok(await _areceberService.ObterPorNatureza(idNatureza, _idUsuario));
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
        public async Task<IActionResult> Atualizar(long id, AreceberRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.Atualizar(id, contrato, _idUsuario));
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
        public async Task<IActionResult> Inativar(long id, AreceberRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _areceberService.Inativar(id, _idUsuario);
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
                return Ok(await _areceberService.ObterPorVencimento(_idUsuario, vencimentoInicial, vencimentoFinal));
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
                return Ok(await _areceberService.ObterTituloBaixado(_idUsuario));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}