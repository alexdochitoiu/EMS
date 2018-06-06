export class UserRegisterModel {
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
  Gender: any;
  DateOfBirth: string;
  Phone: string;
  Country: string;
  City: string;
  Street: string;
  Number: string;
  ZipCode: string;
}

export class UserLoginModel {
  EmailOrUsername: string;
  Password: string;
}

export class UserModel {
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Gender: string;
  DateOfBirth: string;
  Phone: string;
  Address: any;

  constructor(firstName, lastName, username, email, gender, birthDate, phone, address) {
    this.FirstName = firstName;
    this.LastName = lastName;
    this.Username = username;
    this.Email = email;
    this.Gender = gender;
    this.DateOfBirth = birthDate;
    this.Phone = phone;
    this.Address = address;
  }
}
