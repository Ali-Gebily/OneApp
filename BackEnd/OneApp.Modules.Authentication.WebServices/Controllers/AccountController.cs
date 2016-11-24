using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Managers.Emails;
using OneApp.Common.WebServices.Controllers;
using OneApp.Common.WebServices.Exceptions;
using OneApp.Common.WebServices.Models;
using OneApp.Modules.Authentication.Data.Repositories;
using OneApp.Modules.Authentication.WebServices.Models;

namespace OneApp.Modules.Authentication.WebServices.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private IAuthenticationRepository _repo = null;
        private IEmailSender _emailSender = null;
        public AccountController(IAuthenticationRepository repository, IEmailSender emailSender)
        {
            _repo = repository;
            _emailSender = emailSender;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<BaseHttpActionResult> SendEmailVerificationCode([Required]SendEmailVerificationCodeViewModel verifyEmailViewModel)
        {
            var generatedCode = Common.Core.Utilities.OneAppUtility.GetCodeForValue(verifyEmailViewModel.Email);
            StringBuilder body = new StringBuilder("Hello " + verifyEmailViewModel.Email + ",");
            body.Append("<br /><br />Please copy the following code and paste it in verification screen to complete your account registeration or password resetting.");
            body.Append("<br />Your verification code is <b>" + generatedCode.Code + "</b>");
            body.Append("<br />Note: This code is valid only for current screen");
            body.Append("<br /><br />Thanks");
            _emailSender.Send(verifyEmailViewModel.Email, "OneApp Account Email Verification", body.ToString(), true);

            return new SuccessHttpActionResult(generatedCode.KeyInitialValue);

        }
        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<BaseHttpActionResult> Register([Required]UserViewModel userModel)
        {
            var generatedCode = Common.Core.Utilities.OneAppUtility.GetCodeForValue(userModel.HashKey, userModel.Email);

            if (generatedCode.Code != userModel.VerificationCode)
            {
                return ErrorHttpActionResult.GenerateBadRequest("Invalid or expired code");
            }
            IdentityResult result = await _repo.RegisterUser(userModel.Username, userModel.Password,
                userModel.Email, userModel.PhoneNumber, true);

            return GetResult(result);

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<BaseHttpActionResult> VerifyForgotPasswordEmail([Required]VerifyForgotPasswordEmailViewModel forgotPasswordViewModel)
        {
            var user = await _repo.FindUserWithEmail(forgotPasswordViewModel.Email);
            if (user == null)
            {
                return ErrorHttpActionResult.GenerateBadRequest("no user registered with this email");

            }
            return await SendEmailVerificationCode(forgotPasswordViewModel);

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<BaseHttpActionResult> ResetPassword([Required]ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await _repo.FindUserWithEmail(resetPasswordViewModel.Email);
            if (user == null)
            {
                return ErrorHttpActionResult.GenerateBadRequest("no user registered with this email");
            }
            var generatedCode = Common.Core.Utilities.OneAppUtility.GetCodeForValue(resetPasswordViewModel.HashKey, resetPasswordViewModel.Email);

            if (generatedCode.Code != resetPasswordViewModel.VerificationCode)
            {
                return ErrorHttpActionResult.GenerateBadRequest("Invalid or expired code");
            }

            string token = await _repo.GeneratePasswordResetToken(user.Id.ToString());

            IdentityResult result = await _repo.ResetPassword(user.Id, token, resetPasswordViewModel.Password);

            return GetResult(result);

        }
        [Authorize]
        [HttpPost]
        public async Task<BaseHttpActionResult> ChangePassword([Required]ChangePasswordViewModel changePasswordViewModel)
        {
            //load user using username and provided password
            var user = await _repo.FindUser(this.User.Identity.GetUserName(), changePasswordViewModel.CurrentPassword);
            if (user == null)
            {
                return ErrorHttpActionResult.GenerateBadRequest("The current password is invalid");
            }

            IdentityResult result = await _repo.ChangePassword(user.Id, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
            return GetResult(result);
        }
        [HttpPost]
        public async Task<BaseHttpActionResult> Logout()
        {
            //invalidate the token, so that it's never be used
            return new SuccessHttpActionResult();
        }
        [HttpPost]
        public async Task<BaseHttpActionResult> GetRoles()
        {
            var roles = await _repo.GetRoles();
            return new SuccessHttpActionResult(roles);
        }

        [HttpPost]
        public async Task<BaseHttpActionResult> FindUsers([Required]FindUsersFilterModel findUserFilterModel)
        {
            var users = await _repo.FindUsers(findUserFilterModel.UsernamePart);
            return new SuccessHttpActionResult(users);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private BaseHttpActionResult GetResult(IdentityResult result)
        {
            if (result == null)
            {
                return new InternalServerErrorHttpActionResult();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                return new ErrorHttpActionResult(HttpStatusCode.BadRequest, new ErrorResponse(ModelState));
            }

            return new SuccessHttpActionResult(new SuccessResponse());

        }
    }
}