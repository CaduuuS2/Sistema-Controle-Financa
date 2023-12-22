using System.ComponentModel.DataAnnotations;

namespace ControleFacil.Api.Domain.Models
{
    public class Areceber : Titulo
    {
        [Required(ErrorMessage = "O campo ValorRecebido é obrigatório.")]
        public double ValorRecebido {get; set;}
        public DateTime? DataRecebimento {get; set;}
    }
}