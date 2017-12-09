using System;
using Data.Core.Domain;

namespace WebAPI.Models.UserModels
{
    public class CreatingUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public Address Address { get; set; }
    }
}
