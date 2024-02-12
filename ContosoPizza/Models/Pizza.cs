using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{

    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }
    }
}