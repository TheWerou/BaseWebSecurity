import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public formBuilder: FormBuilder) { 
    this.formGrup = this.formBuilder.group({
      login: new FormControl("",[Validators.required]),
      pass: new FormControl("",[Validators.required]),
    });
  }
  formGrup: FormGroup;

  ngOnInit(): void {
    
  }

  login()
  {
    let login = this.formGrup.getRawValue()["login"];
    let pass = this.formGrup.getRawValue()["pass"];
    console.log(login);
    console.log(pass);
  }
}
