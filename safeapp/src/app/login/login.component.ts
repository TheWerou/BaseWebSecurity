import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { LogUser, ReturnJWT, User } from '../Interfaces/interfaces';
import { LoginServiceService } from '../Services/login-service.service';
import { TwoFALoginComponent } from '../login/two-falogin/two-falogin.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public formBuilder: FormBuilder,
    public loginService: LoginServiceService,
    public router: Router) {

    this.formGrup = this.formBuilder.group({
      login: new FormControl("", [Validators.required]),
      pass: new FormControl("", [Validators.required]),
    });
  }

  private destroyed$ = new Subject<any>();
  public twoFA = false;
  formGrup: FormGroup;
  public UserObject: any;

  ngOnInit(): void {

  }

  login() {
    let login = this.formGrup.getRawValue()["login"];
    let pass = this.formGrup.getRawValue()["pass"];
    let object = {
      Login: login,
      Password: pass,
    }

    // this.loginService.login(object).subscribe(
    //   event => {
    //     console.log(event);
    //     localStorage.setItem('token', event.jwtToken);  
    //     this.router.navigateByUrl('/Site');
    //   }
    // )
    console.log(login);
    console.log(pass);
    console.log(object);

  }

  newLoginRequest() {
    let login = this.formGrup.getRawValue()["login"];
    let pass = this.formGrup.getRawValue()["pass"];
    this.UserObject = {
      Login: login,
      Password: pass,
    }

    this.loginService.requestLogin(this.UserObject).subscribe(event => {
      if(event == true)
      {
        this.twoFA = true;
      }
      
    });

  }
}
