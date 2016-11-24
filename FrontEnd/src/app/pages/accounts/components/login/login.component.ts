import {Component, ViewEncapsulation} from '@angular/core';
import {FormGroup, AbstractControl, FormBuilder, Validators} from '@angular/forms';
import { AccountsService } from '../../services/accounts.service';
import { LoginInfoModel } from '../../models/loginInfo.model';

@Component({
  selector: 'login',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./login.scss')],
  template: require('./login.html'),
})
export class LoginComponent {

  public form:FormGroup;
  public email:AbstractControl;
  public password:AbstractControl;
  public submitted:boolean = false;

  constructor(fb:FormBuilder, private accountsService: AccountsService) {
    this.form = fb.group({
      'email': ['', Validators.compose([Validators.required])],
      'password': ['', Validators.compose([Validators.required])]
    });

    this.email = this.form.controls['email'];
    this.password = this.form.controls['password'];
  }

  public onSubmit(values:Object):void {
    this.submitted = true;
    if (this.form.valid) {
     var loginInfo=new LoginInfoModel();
     loginInfo.username=this.email.value;
    loginInfo.password=this.password.value;
      this.accountsService.login(loginInfo);
    }
  }
}
