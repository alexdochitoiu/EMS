using System;
using Data.Core.Domain;
using WebAPI.Models.AddressModels;

namespace WebAPI.Models.UserModels
{
    public class DisplayUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DisplayAddressModel Address { get; set; }
    }
}
