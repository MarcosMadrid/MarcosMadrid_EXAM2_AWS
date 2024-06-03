using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWSServerlessApiEventos.Models
{
    [Table("categoriaevento")]
    public class Categoria
    {
        [Key]
        [Column("idcategoria")]
        public int Id { get; set; }
        [Column("nombre")]
        public string? Name { get; set; }
    }
}
