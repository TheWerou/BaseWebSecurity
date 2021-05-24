export interface LogUser {
    Login: string;
    Password: string;

  }
export interface User {
    login: string;
    password: string;
    email: string
}
export interface ReturnJWT {
  jwtToken: string;
}
export interface ResponseDTO {
  massage: string;
}
