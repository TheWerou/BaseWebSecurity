import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResponseDTO, User } from '../Interfaces/interfaces';

@Component({
  selector: 'app-site',
  templateUrl: './site.component.html',
  styleUrls: ['./site.component.scss']
})
export class SiteComponent implements OnInit {

  constructor(private http: HttpClient,
    public router: Router) { }
  public message: User[] = []

  ngOnInit(): void {
    this.getData();
  }

  getData()
  {
    let serverUrl: string = "https://localhost:44360/UserAuth/"
    let UrlServer = serverUrl + "SecuredService" 
    this.http.get<User[]>(UrlServer).subscribe(
      event => {this.message = event}
    )
  }

  logout()
  {
    localStorage.removeItem("token");
    this.router.navigateByUrl('/')
  }
}
