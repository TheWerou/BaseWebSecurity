import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../Interfaces/interfaces';
import { LoginServiceService } from '../Services/login-service.service';

@Component({
  selector: 'app-registry',
  templateUrl: './registry.component.html',
  styleUrls: ['./registry.component.scss']
})
export class RegistryComponent implements OnInit {

  constructor(public formBuilder: FormBuilder, public loginService: LoginServiceService) {
    this.formGrup = this.formBuilder.group({
      login: new FormControl("",[Validators.required]),
      pass: new FormControl("",[Validators.required, Validators.pattern("^(?=.[0-9])(?=.[a-z])(?=.[A-Z])(?=.[!@#$%^&*():;\"'|,.<>~]).{12,}$") ]),
      pass2: new FormControl("",[Validators.required, Validators.pattern("^(?=.[0-9])(?=.[a-z])(?=.[A-Z])(?=.[!@#$%^&*():;\"'|,.<>~]).{12,}$")]),
      email: new FormControl("",[Validators.required]),
    });
   }
  formGrup: FormGroup;

  ngOnInit(): void {
  }

  registry()
  {
    let login = this.formGrup.getRawValue()["login"];
    let pass = this.formGrup.getRawValue()["pass"];
    let pass2 = this.formGrup.getRawValue()["pass2"];
    let email = this.formGrup.getRawValue()["email"];
    let newUser: User = {
      login: login,
      password: pass,
      email: email,
    } 

    console.log(login);
    console.log(pass);
    console.log(pass2);
    console.log(email);
    
    this.loginService.registerNewUser(newUser)
  }

}
