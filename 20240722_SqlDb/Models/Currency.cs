using System.ComponentModel.DataAnnotations;

namespace _20240722_SqlDb.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public float Rate { get; set; }
    }
}
