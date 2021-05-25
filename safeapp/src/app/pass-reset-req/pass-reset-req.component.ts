import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { LoginServiceService } from '../Services/login-service.service';

@Component({
  selector: 'app-pass-reset-req',
  templateUrl: './pass-reset-req.component.html',
  styleUrls: ['./pass-reset-req.component.scss']
})
export class PassResetReqComponent implements OnInit {
  formGrup: any;

  constructor(public formBuilder: FormBuilder, public loginService: LoginServiceService) { 
    this.formGrup = this.formBuilder.group({
      email: new FormControl("",[Validators.required]),
    });
  }

  ngOnInit(): void {

  }

  passwordReset()
  {
    let email = this.formGrup.getRawValue()["email"];
    console.log(email);
    this.loginService.requestPasswordReset(email);
  }
}
