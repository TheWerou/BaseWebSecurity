import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginServiceService } from '../Services/login-service.service';

@Component({
  selector: 'app-acceptaccount',
  templateUrl: './acceptaccount.component.html',
  styleUrls: ['./acceptaccount.component.scss']
})
export class AcceptaccountComponent implements OnInit {

  tokenID: string = "";
  constructor(
    public loginService: LoginServiceService,
    private activatedRoute: ActivatedRoute,
    public router: Router) {
    this.activatedRoute.queryParams.subscribe(params => {
      this.tokenID = params['tokenID'];
      console.log(this.tokenID);
    });
  }

  ngOnInit(): void {
    let newObject = {
      jwtToken: this.tokenID,
    }
    this.loginService.conformAccount(newObject);
    this.router.navigateByUrl('/');
  }

}
