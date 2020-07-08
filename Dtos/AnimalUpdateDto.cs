using System.ComponentModel.DataAnnotations;

namespace ST3.Dtos
{
    public class AnimalUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int ShelterId { get; set; }
    }
}
