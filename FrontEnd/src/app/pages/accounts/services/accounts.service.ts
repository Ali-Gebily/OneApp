import { Injectable } from '@angular/core';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppNavigationService,OneAppUIService }  
from '../../../common/oneAppProxy/services';

 import { SendEmailVerificationCodeModel } from '../models/sendEmailVerificationCode.model';
import { UserModel } from '../models/user.model';
import { LoginInfoModel } from '../models/loginInfo.model';

import { VerifyForgotPasswordEmailModel } from '../models/verifyForgotPasswordEmail.model';
import { ResetPasswordModel } from '../models/resetPassword.model';
import { ChangePasswordModel } from '../models/changePassword.model';

@Injectable()
export class AccountsService {
    constructor(private oneAppHttpService: OneAppHttpService,
        private oneAppAuthenticationService: OneAppAuthenticationService,
        private oneAppNavigationService: OneAppNavigationService) {
    }

    public sendEmailVerificationCode(sendEmailVerificationCodeModel: SendEmailVerificationCodeModel): Promise<any> {
        return this.oneAppHttpService.post('api/account/sendEmailVerificationCode', sendEmailVerificationCodeModel);
    };

    public register(userModel: UserModel): Promise<any> {

        this.oneAppAuthenticationService.clearAuthenticationData();
        var accountService = this;
        return this.oneAppHttpService.post('api/account/register', userModel).then(function () {
            accountService.login({ username: userModel.username, password: userModel.password });
        });
    };

    public login(loginInfo: LoginInfoModel): Promise<any> {

        var data = "grant_type=password&username=" + loginInfo.username + "&password=" + loginInfo.password;
        this.oneAppAuthenticationService.clearAuthenticationData();
        var accountService = this;
        return this.oneAppHttpService.post('token', data,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).
            then(function (response) {
                accountService.oneAppAuthenticationService.setAuthentication(
                    {
                        isAuth: true,
                        username: loginInfo.username,
                        access_token: response.access_token,
                        token_type: response.token_type,
                        expires_in: response.expires_in
                    })

                accountService.oneAppNavigationService.NavigateToHome();
                Promise.resolve();
            });
    };

    public verifyForgotPasswordEmail(verifyForgotPasswordEmailModel: VerifyForgotPasswordEmailModel): Promise<any> {
        return this.oneAppHttpService.post('api/account/verifyForgotPasswordEmail', verifyForgotPasswordEmailModel);
    };

    public resetPassword(resetPasswordModel: ResetPasswordModel): Promise<any> {
        var service = this;
        return this.oneAppHttpService.post('api/account/resetPassword', resetPasswordModel).then(function () {
            service.oneAppAuthenticationService.clearAuthenticationData();

        });
    };
    public changePassword(changePasswordModel: ChangePasswordModel): Promise<any> {
        var service = this;
        return this.oneAppHttpService.post('api/account/changePassword', changePasswordModel).then(
            function () {
                service.oneAppAuthenticationService.clearAuthenticationData();
                service.oneAppNavigationService.NavigateToLogin();
            }

        );
    };


}