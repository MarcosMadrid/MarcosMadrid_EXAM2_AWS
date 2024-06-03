using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWSServerlessApiEventos.Models
{
    [Table("eventos")]
    public class Evento
    {
        [Key]
        [Column("idevento")]
        public int Id { get; set; }
        [Column("nombre")]
        public string? Name { get; set; }
        [Column("artista")]
        public string? Artist { get; set; }
        [Column("idcategoria")]
        public int? IdCategory { get; set; }
        [Column("imagen")]
        public string? Image { get; set; }
    }
}
