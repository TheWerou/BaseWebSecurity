export interface LogUser {
    Login: string;
    Password: string;
  }
  export interface LogUserWithToken {
    Login: string;
    Password: string;
    Token: string;
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
export interface UserPassRestart {
  password: string;
  token: string;
}
