﻿using System.ComponentModel.DataAnnotations;

namespace BlazorJwtAuth.Models
{
    public class LoginDTO
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
