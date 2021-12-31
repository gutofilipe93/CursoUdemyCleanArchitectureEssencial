using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required!")]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
