import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LogUser } from '../../Interfaces/interfaces';
import { LoginServiceService } from '../../Services/login-service.service';


@Component({
  selector: 'app-two-falogin',
  templateUrl: './two-falogin.component.html',
  styleUrls: ['./two-falogin.component.scss']
})
export class TwoFALoginComponent implements OnInit {

  constructor(
    public formBuilder: FormBuilder, 
    public loginService: LoginServiceService,
    public router: Router) {

    this.formGrup = this.formBuilder.group({
      code: new FormControl("", [Validators.required]),

    });
  }
  formGrup: FormGroup;
  @Input() UserData: LogUser = {
    Login: "",
    Password: "",
  };
  ngOnInit(): void {

  }

  twoAutorize() {
    let objectToSend = {
      Login: this.UserData.Login,
      Password: this.UserData.Password,
      Token: this.formGrup.getRawValue()["code"],
    } 
    console.log(objectToSend);
    this.loginService.login(objectToSend).subscribe(
      event => {
        console.log(event);
        localStorage.setItem('token', event.jwtToken);
        this.router.navigateByUrl('/Site');
      }
    )
  }

}
