using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Contract.Areceber;

namespace ControleFacil.Api.Contract.FluxoDeCaixa
{
    public class SaldoPorPeriodoResponseContract : TotalTitulosResponseContract
    {
        public decimal SaldoDoPeriodo {get; set;}
        public IEnumerable<ApagarResponseContract> TitulosApagar {get; set;}
        public IEnumerable<AreceberResponseContract> TitulosAreceber {get; set;}
    }
}