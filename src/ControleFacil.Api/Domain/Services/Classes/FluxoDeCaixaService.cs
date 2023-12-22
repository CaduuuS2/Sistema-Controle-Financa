using ControleFacil.Api.Contract.FluxoDeCaixa;
using ControleFacil.Api.Domain.Services.Interfaces;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class FluxoDeCaixaService : IFluxoDeCaixaService
    {
        private readonly IApagarService _apagarService;
        private readonly IAreceberService _areceberService;
        private readonly INaturezaDeLancamentoService _naturezaDeLancamentoService;
    
        public FluxoDeCaixaService(IApagarService apagarService, IAreceberService areceberService, INaturezaDeLancamentoService naturezaDeLancamentoService)
        {
            _apagarService = apagarService;
            _areceberService = areceberService;
            _naturezaDeLancamentoService = naturezaDeLancamentoService;
        }

        public async Task<IEnumerable<NaturezaDeLancamentoAndTitulosResponseContract>> ObterNaturezaAndTitulosVinculados(long idUsuario)
        {
            var naturezas = await _naturezaDeLancamentoService.Obter(idUsuario);
            var apagares = await _apagarService.Obter(idUsuario);
            var areceberes = await _areceberService.Obter(idUsuario);
            
            List<NaturezaDeLancamentoAndTitulosResponseContract> listaRetorno = new List<NaturezaDeLancamentoAndTitulosResponseContract>();
            try
            {
                foreach(var n in naturezas)
                {
                    var titulosApagar = apagares.Where(a => a.IdNaturezaDeLancamento == n.Id);
                    var titulosAreceber = areceberes.Where(a => a.IdNaturezaDeLancamento == n.Id);

                    NaturezaDeLancamentoAndTitulosResponseContract objeto = 
                    new NaturezaDeLancamentoAndTitulosResponseContract
                    {
                        NaturezaDeLancamento = n,
                        TitulosApagar = titulosApagar,
                        TitulosAreceber = titulosAreceber
                    };
                    listaRetorno.Add(objeto);
                }
                return listaRetorno;
            }
            catch(Exception e)
            {
                throw new Exception("Erro no processamento interno");
            }
        }

        public async Task<SaldoPorPeriodoResponseContract> ObterSaldoPeriodo(long idUsuario, DateTime dataInicial, DateTime dataFinal)
        {
            var apagares = await _apagarService.ObterPorVencimento(idUsuario, dataInicial, dataFinal);
            var areceberes = await _areceberService.ObterPorVencimento(idUsuario, dataInicial, dataFinal);
            decimal totalApagar = decimal.Parse(apagares.Sum(a => a.ValorOriginal).ToString());
            decimal totalAreceber = decimal.Parse(areceberes.Sum(a => a.ValorOriginal).ToString());
            decimal saldo = totalAreceber - totalApagar;
            try
            {
                return new SaldoPorPeriodoResponseContract
                {
                    TotalApagar = totalApagar,
                    TotalAreceber = totalAreceber,
                    SaldoDoPeriodo = saldo,
                    TitulosApagar = apagares,
                    TitulosAreceber = areceberes
                };
            }
            catch(Exception e)
            {
                throw new Exception("Erro no processamento interno");
            }        
        }

        public async Task<TitulosPorPeriodoResponseContract> ObterTitulosPorPeriodo(long idUsuario, DateTime dataInicial, DateTime dataFinal)
        {
            var apagares = await _apagarService.ObterPorVencimento(idUsuario, dataInicial, dataFinal);
            var areceberes = await _areceberService.ObterPorVencimento(idUsuario, dataInicial, dataFinal);
            decimal totalApagar = decimal.Parse(apagares.Sum(a => a.ValorOriginal).ToString());
            decimal totalPago = decimal.Parse(apagares.Sum(a => a.ValorPago).ToString());
            decimal totalApagarAberto = totalApagar - totalPago;

            decimal totalAreceber = decimal.Parse(areceberes.Sum(a => a.ValorOriginal).ToString());
            decimal totalRecebido = decimal.Parse(areceberes.Sum(a => a.ValorRecebido).ToString());
            decimal totalAreceberAberto = totalAreceber - totalRecebido;
            try
            {
                return new TitulosPorPeriodoResponseContract
                {
                    TotalApagar = totalApagar,
                    TotalAreceber = totalAreceber,
                    TotalPago = totalPago,
                    TotalApagarAberto = totalApagarAberto,
                    TotalRecebido = totalRecebido,
                    TotalAreceberAberto = totalAreceberAberto
                };
            }
            catch(Exception e)
            {
                throw new Exception("Erro no processamento interno");
            } 
        }
    }
}