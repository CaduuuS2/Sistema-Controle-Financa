
namespace ControleFacil.Api.Contract.Apagar
{
    public class ApagarResponseContract : ApagarRequestContract
    {
        public long Id {get; set;}
        public DateTime DataCadastro {get; set;}
        public DateTime? DataInativacao {get; set;}
    }
}