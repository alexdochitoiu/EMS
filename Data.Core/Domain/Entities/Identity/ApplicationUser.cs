using System;
using EnsureThat;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public GenderEnum Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; } 
        public Address Address { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age = age - 1;

                return age;
            } 
        }


        public static ApplicationUser Create(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth,
            string email, string username, string phone, Address address)
        {
            var user = new ApplicationUser
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            user.Update(firstName, lastName, gender, dateOfBirth, email, username, phone, address);
            return user;
        }

        public void Update(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth,
            string email, string username, string phone, Address address)
        {
            Validate(email, username, phone);

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            UserName = username;
            PhoneNumber = phone;
            Address = address;
            Modified = DateTime.Now;
        }

        private static void Validate(string email, string username, string phone)
        {
            Ensure.That(email).IsNotNullOrEmpty();
            Ensure.That(username).IsNotNullOrEmpty();
            Ensure.That(phone).IsNotNullOrEmpty();
        }
    }
}
