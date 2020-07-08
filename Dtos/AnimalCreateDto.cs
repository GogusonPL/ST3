using System.ComponentModel.DataAnnotations;

namespace ST3.Dtos
{
    public class AnimalCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int ShelterId { get; set; }
    }
}
