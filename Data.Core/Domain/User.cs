using System;
using EnsureThat;

namespace Data.Core.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public GenderEnum Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; } 
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Phone { get; private set; }
        public Address Address { get; private set; }
        public int Age
        {
            get
            {
                var age = 0;
                age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age = age - 1;

                return age;
            } 
        }

        public static User Create(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth,
            string email, string password, string phone, Address address)
        {
            Validate(firstName, lastName, gender, dateOfBirth, email, password, phone, address);

            var user = new User
            {
                Id = new Guid()
            };
            user.Update(firstName, lastName, gender, dateOfBirth, email, password, phone, address);
            return user;
        }

        public void Update(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth,
            string email, string password, string phone, Address address)
        {
            Validate(firstName, lastName, gender, dateOfBirth, email, password, phone, address);

            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            Password = password;
            Phone = phone;
            Address = address;
        }

        private static void Validate(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth,
            string email, string password, string phone, Address address)
        {
            Ensure.That(firstName).IsNotNullOrEmpty();
            Ensure.That(lastName).IsNotNullOrEmpty();
            Ensure.That(Enum.IsDefined(typeof(GenderEnum), gender)).IsTrue();
            Ensure.That(dateOfBirth).IsLt(DateTime.Now);
            Ensure.That(email).IsNotNullOrEmpty();
            Ensure.That(password).IsNotNullOrEmpty();
            Ensure.That(phone).IsNotNullOrEmpty();
            Ensure.That(address).IsNotNull();
        }
    }
}
