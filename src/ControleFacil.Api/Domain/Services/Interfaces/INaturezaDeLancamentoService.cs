using ControleFacil.Api.Contract.NaturezaDeLancamento;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface INaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long>
    {
        Task<IEnumerable<NaturezaDeLancamentoResponseContract>> ObterNaturezasAtivas(long idUsuario);
        }
}