using ControleFacil.Api.Contract.FluxoDeCaixa;
namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IFluxoDeCaixaService
    {
        Task<SaldoPorPeriodoResponseContract> ObterSaldoPeriodo(long idUsuario, DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<NaturezaDeLancamentoAndTitulosResponseContract>> ObterNaturezaAndTitulosVinculados(long idUsuario);
        Task<TitulosPorPeriodoResponseContract> ObterTitulosPorPeriodo(long idUsuario, DateTime dataInicial, DateTime dataFinal);
    }
}