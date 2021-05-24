import { Injectable, Output } from '@angular/core';
import { LogUser, ReturnJWT, User } from '../Interfaces/interfaces';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  constructor(private http: HttpClient, public jwtHelper: JwtHelperService) { }
  serverUrl: string = "https://localhost:44360/UserAuth/"
  
  login(userLogin: LogUser): Observable<ReturnJWT>
  {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(userLogin);
    const UrlServer = this.serverUrl + "LoginJWT"
    return this.http.post<ReturnJWT>(UrlServer, body, {'headers': headers})
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
}
