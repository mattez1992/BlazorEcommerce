using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared.Dtos
{
    public class UserRegristrationDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(50, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Required,Compare(nameof(Password), ErrorMessage = "The passwords must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
