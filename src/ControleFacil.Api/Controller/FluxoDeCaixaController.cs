using ControleFacil.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controller
{
    [ApiController]
    [Route("fluxos-de-caixa")]
    public class FluxoDeCaixaController : BaseController
    {
        private readonly IFluxoDeCaixaService _fluxoDeCaixaService;
        private long _idUsuario;

        public FluxoDeCaixaController(IFluxoDeCaixaService fluxoDeCaixaService)
        {
            _fluxoDeCaixaService = fluxoDeCaixaService;
        } 

        [HttpGet]
        [Route("saldos-por-periodo")]
        [Authorize]
        public async Task<IActionResult> ObterSaldoPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _fluxoDeCaixaService.ObterSaldoPeriodo(_idUsuario, dataInicial, dataFinal));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("titulos-por-periodo")]
        [Authorize]
        public async Task<IActionResult> ObterTitulosPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _fluxoDeCaixaService.ObterTitulosPorPeriodo(_idUsuario, dataInicial, dataFinal));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("naturezas-e-titulos")]
        [Authorize]
        public async Task<IActionResult> ObterNaturezaComTitulos()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _fluxoDeCaixaService.ObterNaturezaAndTitulosVinculados(_idUsuario));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}