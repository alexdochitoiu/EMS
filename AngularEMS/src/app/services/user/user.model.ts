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
