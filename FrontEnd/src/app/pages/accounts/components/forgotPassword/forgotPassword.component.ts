import { Component, ViewEncapsulation } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { EmailValidator, EqualPasswordsValidator } from '../../../../theme/validators';
import { AccountsService } from '../../services/accounts.service';
import { VerifyForgotPasswordEmailModel } from '../../models/verifyForgotPasswordEmail.model';
import { ResetPasswordModel } from '../../models/resetPassword.model';

@Component({
  selector: 'forgot-password',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./forgotPassword.scss')],
  template: require('./forgotPassword.html'),
})
export class ForgotPasswordComponent {

  public form: FormGroup;
  public email: AbstractControl;
  
  public password: AbstractControl;
  public confirmPassword: AbstractControl;
  public passwords: FormGroup;
  
  public verificationCode: AbstractControl;

  private hashKey = null;
  public emailVerified: boolean = false;


  constructor(fb: FormBuilder, private accountsService: AccountsService) {

    this.form = fb.group({
      'email': ['', Validators.compose([Validators.required, EmailValidator.validate])],
      'passwords': fb.group({
        'password': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
        'confirmPassword': ['', Validators.compose([Validators.required, Validators.minLength(6)])]
      }, { validator: EqualPasswordsValidator.validate('password', 'confirmPassword') }),
      'verificationCode': ['', Validators.compose([Validators.required, Validators.minLength(3)])]
    });

     this.email = this.form.controls['email'];
     this.passwords = <FormGroup>this.form.controls['passwords'];
    this.password = this.passwords.controls['password'];
    this.confirmPassword = this.passwords.controls['confirmPassword'];
    this.verificationCode = this.form.controls['verificationCode'];
  }

  public onSubmit(values: Object): void {
    if (this.form.valid) {
      let resetPasswordModel = new ResetPasswordModel();
      resetPasswordModel.email = this.email.value;
      resetPasswordModel.password = this.password.value;
      resetPasswordModel.confirm_password = this.confirmPassword.value;
      resetPasswordModel.hash_key = this.hashKey;
      resetPasswordModel.verification_code = this.verificationCode.value;
      this.accountsService.resetPassword(resetPasswordModel).then(function (result) {

      });
    }
  }
  public VerifyEmail(): void {
    if (this.email.valid) {
      var verifyForgotPasswordEmailModel = new VerifyForgotPasswordEmailModel();
      verifyForgotPasswordEmailModel.email = this.email.value;
      var currentComponent = this;
      this.accountsService.verifyForgotPasswordEmail(verifyForgotPasswordEmailModel).then(function (result) {
        currentComponent.hashKey = result;
        currentComponent.emailVerified = true;
      });
    }
  }

}
