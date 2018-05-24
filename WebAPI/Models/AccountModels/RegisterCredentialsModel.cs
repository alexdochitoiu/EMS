using System;
using Data.Core.Domain;
using Data.Core.Domain.Entities;

namespace WebAPI.Models.AccountModels
{
    public class RegisterCredentialsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        
        // Address
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
    }
}
