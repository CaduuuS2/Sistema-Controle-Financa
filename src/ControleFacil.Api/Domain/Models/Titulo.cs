using System.ComponentModel.DataAnnotations;

namespace ControleFacil.Api.Domain.Models
{
    public abstract class Titulo
    {
        [Key]
        public long Id {get; set;}

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao {get; set;} = string.Empty;

        public string Observacao {get; set;} = string.Empty;

        [Required]
        public DateTime DataCadastro {get; set;}

        public DateTime? DataInativacao {get; set;}
        
        [Required(ErrorMessage = "O campo DataVencimento é obrigatório.")]
        public DateTime DataVencimento {get; set;}

        public DateTime? DataReferencia {get; set;}

        [Required]
        public long IdNaturezaDeLancamento {get; set;}

        public NaturezaDeLancamento NaturezaDeLancamento {get; set;}

        [Required(ErrorMessage = "O campo ValorOriginal é obrigatório.")]
        public double ValorOriginal {get; set;}
    }
}