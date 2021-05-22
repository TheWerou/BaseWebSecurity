import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { LogUser, User } from '../Interfaces/interfaces';
import { LoginServiceService } from '../Services/login-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public formBuilder: FormBuilder, public loginService: LoginServiceService) { 
    this.formGrup = this.formBuilder.group({
      login: new FormControl("",[Validators.required, Validators.maxLength(30)]),
      pass: new FormControl("",[Validators.required, Validators.minLength(10), Validators.maxLength(30)]),
    });
  }
  private destroyed$ = new Subject<any>();
  formGrup: FormGroup;
  public output2: any = false;
  ngOnInit(): void {
    
  }

  login()
  {
    let login = this.formGrup.getRawValue()["login"];
    let pass = this.formGrup.getRawValue()["pass"];
    let object = {
      Login: login,
      Password: pass,
    }
    
    this.loginService.login(object).subscribe(
      event => {this.output2 = event}
    )
    console.log(login);
    console.log(pass);
    console.log(object);
    console.log(this.output2);
  }
}
