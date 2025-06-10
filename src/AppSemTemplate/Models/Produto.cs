using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSemTemplate.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nome { get; set; }

        [MaxLength (100)]
        public string? Imagem { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        public decimal Valor { get; set; }
    }
}
