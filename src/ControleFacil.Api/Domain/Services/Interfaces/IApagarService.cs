using ControleFacil.Api.Contract.Apagar;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IApagarService : IService<ApagarRequestContract, ApagarResponseContract, long>
    {
        Task<IEnumerable<ApagarResponseContract>> ObterPorNatureza(long idNatureza, long idUsuario);
        Task<IEnumerable<ApagarResponseContract>> ObterPorVencimento(long idUsuario, DateTime vencimentoInicial, DateTime vencimentoFinal);
        Task<IEnumerable<ApagarResponseContract>> ObterTituloBaixado(long idUsuario);
    }
}