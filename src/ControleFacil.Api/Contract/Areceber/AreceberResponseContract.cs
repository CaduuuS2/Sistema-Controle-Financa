
namespace ControleFacil.Api.Contract.Areceber
{
    public class AreceberResponseContract : AreceberRequestContract
    {
        public long Id {get; set;}
        public DateTime DataCadastro {get; set;}
        public DateTime? DataInativacao {get; set;}
    }
}