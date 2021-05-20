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
  serverUrl: string = "https://localhost:44360/"
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  login(userLogin: LogUser)
  {
    let UrlServer = this.serverUrl + "UserAuth/Login"  
    let heleper = this.http.post<boolean>(UrlServer, JSON.stringify(userLogin), this.httpOptions)
    return heleper;
  }

  getAllUsers()
  {
    let UrlServer = this.serverUrl + "UserAuth/GetAllUsers" 
    let helper = this.http.get<User[]>(UrlServer)
    return helper;
  }
}
