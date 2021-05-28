using System.ComponentModel.DataAnnotations;
using System;

namespace API.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Range(1, 150)]
        public byte Age { get; set; }
    }
}
