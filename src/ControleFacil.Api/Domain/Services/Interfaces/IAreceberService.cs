using ControleFacil.Api.Contract.Areceber;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IAreceberService : IService<AreceberRequestContract, AreceberResponseContract, long>
    {
        Task<IEnumerable<AreceberResponseContract>> ObterPorNatureza(long idNatureza, long idUsuario);
        Task<IEnumerable<AreceberResponseContract>> ObterPorVencimento(long idUsuario, DateTime vencimentoInicial, DateTime vencimentoFinal);
        Task<IEnumerable<AreceberResponseContract>> ObterTituloBaixado(long idUsuario);
    }
}