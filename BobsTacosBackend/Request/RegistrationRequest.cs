﻿using System.ComponentModel.DataAnnotations;
using BobsTacosBackend.Enums;
namespace BobsTacosBackend.Request
{

    public class RegistrationRequest
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get { return this.Email; } set { } }

        [Required]
        public string? Password { get; set; }

        public Role Role { get; set; } = Role.User;
    }
}
