import { Injectable, Output } from '@angular/core';
import { LogUser, User } from '../Interfaces/interfaces';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  constructor(private http: HttpClient) { }
  serverUrl: string = "https://localhost:44360/UserAuth/"
  
  login(userLogin: LogUser): Observable<Object>
  {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(userLogin);
    const UrlServer = this.serverUrl + "Login"
    console.log(body);
    return this.http.post(UrlServer, body, {'headers': headers});
  }

  getAllUsers()
  {
    let UrlServer = this.serverUrl + "GetAllUsers" 
    let helper = this.http.get<User[]>(UrlServer)
    return helper;
  }
}
