import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserPassRestart } from '../Interfaces/interfaces';
import { LoginServiceService } from '../Services/login-service.service';

@Component({
  selector: 'app-pass-reset',
  templateUrl: './pass-reset.component.html',
  styleUrls: ['./pass-reset.component.scss']
})
export class PassResetComponent implements OnInit {
  formGrup: any;
  token: string =""

  constructor(private activatedRoute: ActivatedRoute,
     public formBuilder: FormBuilder,
     public loginService: LoginServiceService) { 
    this.activatedRoute.queryParams.subscribe(params => {
      this.token = params['tokenID'];
      console.log(this.token);
  });
  this.formGrup = this.formBuilder.group({
    pass1: new FormControl("",[Validators.required]),
    pass2: new FormControl("",[Validators.required]),
  });
  }

  ngOnInit(): void {
  }

  resetpass()
  {
    let pass1 = this.formGrup.getRawValue()["pass1"];
    let pass2 = this.formGrup.getRawValue()["pass2"];

    let requestObject: UserPassRestart = {
      password: pass1,
      token: this.token,
    }
    this.loginService.resetPassword(requestObject);

  }

}
