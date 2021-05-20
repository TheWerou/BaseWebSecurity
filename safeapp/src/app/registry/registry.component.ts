import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-registry',
  templateUrl: './registry.component.html',
  styleUrls: ['./registry.component.scss']
})
export class RegistryComponent implements OnInit {

  constructor(public formBuilder: FormBuilder) {
    this.formGrup = this.formBuilder.group({
      login: new FormControl("",[Validators.required]),
      pass: new FormControl("",[Validators.required]),
      pass2: new FormControl("",[Validators.required]),
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
    console.log(login);
    console.log(pass);
    console.log(pass2);
    console.log(email);
  }

}
