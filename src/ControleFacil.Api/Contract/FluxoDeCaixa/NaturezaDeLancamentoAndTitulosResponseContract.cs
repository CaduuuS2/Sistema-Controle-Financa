using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Contract.NaturezaDeLancamento;

namespace ControleFacil.Api.Contract.FluxoDeCaixa
{
    public class NaturezaDeLancamentoAndTitulosResponseContract
    {
       public NaturezaDeLancamentoResponseContract NaturezaDeLancamento {get; set;}

       public IEnumerable<ApagarResponseContract> TitulosApagar {get; set;}

       public IEnumerable<AreceberResponseContract> TitulosAreceber {get; set;}
    }
}