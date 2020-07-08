using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int ShelterId { get; set; }
        [ForeignKey("ShelterId")]
        public Shelter Shelter { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
