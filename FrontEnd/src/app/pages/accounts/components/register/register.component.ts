import { Component, ViewEncapsulation } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { EmailValidator, EqualPasswordsValidator } from '../../../../theme/validators';
import { AccountsService } from '../../services/accounts.service';
import { UserModel } from '../../models/user.model';
import { SendEmailVerificationCodeModel } from '../../models/sendEmailVerificationCode.model';

@Component({
  selector: 'register',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./register.scss')],
  template: require('./register.html'),
})
export class RegisterComponent {

  public form: FormGroup;
  public name: AbstractControl;
  public email: AbstractControl;
  public phoneNumber: AbstractControl;
  public password: AbstractControl;
  public confirmPassword: AbstractControl;
  public passwords: FormGroup;
  public verificationCode: AbstractControl;

  private hashKey = null;
  public emailVerified: boolean = false;


  constructor(fb: FormBuilder, private accountsService: AccountsService) {

    this.form = fb.group({
      'name': ['', Validators.compose([Validators.required, Validators.minLength(4)])],
      'email': ['', Validators.compose([Validators.required, EmailValidator.validate])],
      'phoneNumber': ['', Validators.compose([Validators.required])],
      'passwords': fb.group({
        'password': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
        'confirmPassword': ['', Validators.compose([Validators.required, Validators.minLength(6)])]
      }, { validator: EqualPasswordsValidator.validate('password', 'confirmPassword') }),
      'verificationCode': ['', Validators.compose([Validators.required, Validators.minLength(3)])]
    });

    this.name = this.form.controls['name'];
    this.email = this.form.controls['email'];
    this.phoneNumber = this.form.controls['phoneNumber'];
    this.passwords = <FormGroup>this.form.controls['passwords'];
    this.password = this.passwords.controls['password'];
    this.confirmPassword = this.passwords.controls['confirmPassword'];
    this.verificationCode = this.form.controls['verificationCode'];
  }

  public onSubmit(values: Object): void {
    if (this.form.valid) {
      let user = new UserModel();
      user.name = this.name.value;
      user.email = this.email.value;
      user.username = this.email.value;
      user.phone_number = this.phoneNumber.value;
      user.password = this.password.value;
      user.confirm_password = this.confirmPassword.value;
      user.hash_key = this.hashKey;
      user.verification_code = this.verificationCode.value;
      this.accountsService.register(user).then(function (result) {

      });
    }
  }
  public VerifyEmail(): void {
    if (this.email.valid) {
      var model = new SendEmailVerificationCodeModel();
      model.email = this.email.value;
      var currentComponent = this;
      this.accountsService.sendEmailVerificationCode(model).then(function (result) {
        currentComponent.hashKey = result;
        currentComponent.emailVerified = true;
      });
    }
  }

}
