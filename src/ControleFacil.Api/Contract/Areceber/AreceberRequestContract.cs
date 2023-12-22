namespace ControleFacil.Api.Contract.Areceber
{
    public class AreceberRequestContract : TituloRequestContract
    {
        public DateTime? DataRecebimento {get; set;}        
        public double ValorRecebido {get; set;}
    }
}