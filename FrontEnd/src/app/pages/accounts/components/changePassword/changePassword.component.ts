import { Component, ViewEncapsulation } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { EmailValidator, EqualPasswordsValidator } from '../../../../theme/validators';
import { AccountsService } from '../../services/accounts.service';
import { ChangePasswordModel } from '../../models/changePassword.model';

@Component({
  selector: 'change-password',
  encapsulation: ViewEncapsulation.None,
  styles: [require('./changePassword.scss')],
  template: require('./changePassword.html'),
})
export class ChangePasswordComponent {

  public form: FormGroup;
  public currentPassword: AbstractControl;

  public newPassword: AbstractControl;
  public confirmNewPassword: AbstractControl;
  public passwords: FormGroup;



  constructor(fb: FormBuilder, private accountsService: AccountsService) {

    this.form = fb.group({
      'currentPassword': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
      'passwords': fb.group({
        'newPassword': ['', Validators.compose([Validators.required, Validators.minLength(6)])],
        'confirmNewPassword': ['', Validators.compose([Validators.required, Validators.minLength(6)])]
      }, { validator: EqualPasswordsValidator.validate('newPassword', 'confirmNewPassword') })
    });

    this.currentPassword = this.form.controls['currentPassword'];
    this.passwords = <FormGroup>this.form.controls['passwords'];
    this.newPassword = this.passwords.controls['newPassword'];
    this.confirmNewPassword = this.passwords.controls['confirmNewPassword'];
  }

  public onSubmit(values: Object): void {
    if (this.form.valid) {
      let changePasswordModel = new ChangePasswordModel();
      changePasswordModel.current_password = this.currentPassword.value;
      changePasswordModel.new_password = this.newPassword.value;
      changePasswordModel.confirm_new_password = this.confirmNewPassword.value;
      this.accountsService.changePassword(changePasswordModel).then(function (result) {

      });

    }
  }

}
