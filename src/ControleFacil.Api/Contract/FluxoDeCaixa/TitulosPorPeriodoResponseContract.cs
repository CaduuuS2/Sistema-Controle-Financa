namespace ControleFacil.Api.Contract.FluxoDeCaixa
{
    public class TitulosPorPeriodoResponseContract : TotalTitulosResponseContract
    {
        public decimal TotalPago {get; set;}
        public decimal TotalApagarAberto {get; set;}
        public decimal TotalRecebido {get; set;}    
        public decimal TotalAreceberAberto {get; set;}
    }
}