import { Injectable } from '@angular/core';
import { OneAppAuthenticationService, OneAppConfigurationService, OneAppHttpService, OneAppUIService }
    from '../../../common/oneAppProxy/services';

import { SendEmailVerificationCodeModel } from '../models/sendEmailVerificationCode.model';
import { UserModel } from '../models/user.model';
import { LoginInfoModel } from '../models/loginInfo.model';

import { VerifyForgotPasswordEmailModel } from '../models/verifyForgotPasswordEmail.model';
import { ResetPasswordModel } from '../models/resetPassword.model';
import { ChangePasswordModel } from '../models/changePassword.model';
 import {RuleEntityScope} from '../../styles/models'
@Injectable()
export class AccountsService {
    constructor(private oneAppHttpService: OneAppHttpService,
        private oneAppAuthenticationService: OneAppAuthenticationService, 
        private oneAppConfigurationService: OneAppConfigurationService,
        private oneAppUIService: OneAppUIService) {
    }

    public sendEmailVerificationCode(sendEmailVerificationCodeModel: SendEmailVerificationCodeModel): Promise<any> {
        return this.oneAppHttpService.post('api/accounts/sendEmailVerificationCode', sendEmailVerificationCodeModel);
    };

    public register(userModel: UserModel): Promise<any> {

        this.oneAppAuthenticationService.setAuthentication(null);
        var accountService = this;
        return this.oneAppHttpService.post('api/accounts/register', userModel).then(function () {
            accountService.login({ username: userModel.username, password: userModel.password });
        });
    };

    public login(loginInfo: LoginInfoModel): Promise<any> {

        var data = "grant_type=password&username=" + loginInfo.username + "&password=" + loginInfo.password;
        this.oneAppAuthenticationService.setAuthentication(null);
        var accountService = this;
        return this.oneAppHttpService.post('token', data,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).
            then(function (response) {
                accountService.oneAppAuthenticationService.setAuthentication(
                    {
                        isAuthenticated: true,
                        username: loginInfo.username,
                        access_token: response.access_token,
                        token_type: response.token_type,
                        expires_in: response.expires_in
                    })
                accountService.oneAppUIService.loadStyle(RuleEntityScope.User,null,accountService.oneAppHttpService, accountService.oneAppConfigurationService);
                accountService.oneAppUIService.NavigateToHome();
                Promise.resolve();
            });
    };

    public verifyForgotPasswordEmail(verifyForgotPasswordEmailModel: VerifyForgotPasswordEmailModel): Promise<any> {
        return this.oneAppHttpService.post('api/accounts/verifyForgotPasswordEmail', verifyForgotPasswordEmailModel);
    };

    public resetPassword(resetPasswordModel: ResetPasswordModel): Promise<any> {
        var service = this;
        return this.oneAppHttpService.post('api/accounts/resetPassword', resetPasswordModel).then(function () {
            service.oneAppAuthenticationService.setAuthentication(null);

        });
    };
    public changePassword(changePasswordModel: ChangePasswordModel): Promise<any> {
        var service = this;
        return this.oneAppHttpService.post('api/accounts/changePassword', changePasswordModel).then(
            function () {
                service.oneAppAuthenticationService.setAuthentication(null);
                service.oneAppUIService.NavigateToLogin();
            }

        );
    };


}