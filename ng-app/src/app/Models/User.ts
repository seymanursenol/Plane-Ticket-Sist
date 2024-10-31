
export class User {
  id?: number;
  UserName: string;
  Email: string;
  Password: string;

}


export interface IUser{
  id?: number;
  UserName: string;
  Email: string;
  Password: string;

}
export interface IUserLogin{
  id?: number;
  Email: string;
  Password: string;

}


export interface   ILoginResponse
{
  token: string;
  username: string;
}
