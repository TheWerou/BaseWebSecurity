import { Injectable, Output } from '@angular/core';
import { LogUser, LogUserWithToken, ResponseDTO, ReturnJWT, User, UserPassRestart } from '../Interfaces/interfaces';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  constructor(private http: HttpClient, public jwtHelper: JwtHelperService, public router: Router) { }
  serverUrl: string = "https://localhost:44360/UserAuth/"
  headers: any = { 'content-type': 'application/json' }

  login(userLogin: LogUserWithToken): Observable<ReturnJWT>
  {
    const body = JSON.stringify(userLogin);
    const UrlServer = this.serverUrl + "LoginJWT"
    return this.http.post<ReturnJWT>(UrlServer, body, {'headers': this.headers})
  }

  public isAuthenticated(): boolean {
    let token = localStorage.getItem('token' || '');
    let stringValue
    if(token == null)
    {
      stringValue = String(token);
    }
    let check = !this.jwtHelper.isTokenExpired(stringValue);
    return check;
  }

  getAllUsers()
  {
    let UrlServer = this.serverUrl + "GetAllUsers" 
    let helper = this.http.get<User[]>(UrlServer)
    return helper;
  }

  registerNewUser(user: User)
  {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(user);
    const UrlServer = this.serverUrl + "AddUser"
    this.http.post(UrlServer, body, {'headers': this.headers}).subscribe();
  }

  conformAccount(retunJwt: ReturnJWT)
  {
    const body = JSON.stringify(retunJwt);
    const UrlServer = this.serverUrl + "AutorizeAccount"
    this.http.post(UrlServer, body, {'headers': this.headers}).subscribe();
  }

  resetPassword(userNewPass: UserPassRestart)
  {
    console.log(userNewPass);
    const body = JSON.stringify(userNewPass);
    const UrlServer = this.serverUrl + "RestartPassword"
    this.http.post(UrlServer, body, {'headers': this.headers}).subscribe();
  }

  requestPasswordReset(userEmail: string)
  {
    let requestObject: ResponseDTO = {
      massage: userEmail
    }
    console.log(requestObject);
    const body = JSON.stringify(requestObject);
    const UrlServer = this.serverUrl + "RequestPasswordChange"
    this.http.post(UrlServer, body, {'headers': this.headers}).subscribe();
  }

  requestLogin(userLogin: LogUser)
  {
    const body = JSON.stringify(userLogin);
    const UrlServer = this.serverUrl + "RequestLogin"
    return this.http.post(UrlServer, body, {'headers': this.headers})
  }
}
