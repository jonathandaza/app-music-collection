using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserUpdateDto
    {
        [Required]
        //[MaxLength(50)]
        [StringLength(50, ErrorMessage = "Name too long (50 character limit).")]
        public string Name { get; set; }

        [Required]
        //[MaxLength(50)]
        [StringLength(50, ErrorMessage = "Name too long (50 character limit).")]
        public string LastName { get; set; }

        [Required]
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public byte Age { get; set; }

        [Required]
        public byte GenreId { get; set; }
    }
}
