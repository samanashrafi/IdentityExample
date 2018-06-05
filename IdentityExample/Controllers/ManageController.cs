﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityExample.DomainClasses;
using IdentityExample.Helpers;
using IdentityExample.Models;
using IdentityExample.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IdentityExample.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IApplicationUserManager _userManager;
        public ManageController(
            IApplicationUserManager userManager,
            IAuthenticationManager authenticationManager,
            IApplicationSignInManager applicationSignInManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _applicationSignInManager = applicationSignInManager;
        }

        //
        // GET: /Account/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Account/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(_userManager.GetCurrentUserId(), model.Number).ConfigureAwait(false);
            if (_userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await _userManager.SmsService.SendAsync(message).ConfigureAwait(false);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(_userManager.GetCurrentUserId(), model.OldPassword, model.NewPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
                if (user != null)
                {
                    await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            addErrors(result);
            return View(model);
        }

        //
        // POST: /Manage/DisableTFA
        [HttpPost]
        public async Task<ActionResult> DisableTFA()
        {
            await _userManager.SetTwoFactorEnabledAsync(_userManager.GetCurrentUserId(), false).ConfigureAwait(false);
            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            if (user != null)
            {
                await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/EnableTFA
        [HttpPost]
        public async Task<ActionResult> EnableTFA()
        {
            await _userManager.SetTwoFactorEnabledAsync(_userManager.GetCurrentUserId(), true).ConfigureAwait(false);
            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            if (user != null)
            {
                await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/ForgetBrowser
        [HttpPost]
        public ActionResult ForgetBrowser()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Account/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two factor provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "The phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = _userManager.GetCurrentUserId();
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(userId).ConfigureAwait(false),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(userId).ConfigureAwait(false),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(userId).ConfigureAwait(false),
                Logins = await _userManager.GetLoginsAsync(userId).ConfigureAwait(false),
                BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString()).ConfigureAwait(false)
            };
            return View(model);
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId()).ConfigureAwait(false);
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(_userManager.GetCurrentUserId(), loginInfo.Login).ConfigureAwait(false);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Account/Manage
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(_userManager.GetCurrentUserId()).ConfigureAwait(false);
            var otherLogins = _authenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/RememberBrowser
        [HttpPost]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = _authenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, rememberBrowserIdentity);
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Account/RemoveLogin
        public async Task<ActionResult> RemoveLogin()
        {
            var userId = _userManager.GetCurrentUserId();
            var linkedAccounts = await _userManager.GetLoginsAsync(userId).ConfigureAwait(false);
            ViewBag.ShowRemoveButton = await _userManager.HasPasswordAsync(userId).ConfigureAwait(false) || linkedAccounts.Count > 1;
            return View(linkedAccounts);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await _userManager.RemoveLoginAsync(_userManager.GetCurrentUserId(), new UserLoginInfo(loginProvider, providerKey)).ConfigureAwait(false);
            if (result.Succeeded)
            {
                var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
                if (user != null)
                {
                    await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }
        //
        // GET: /Account/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await _userManager.SetPhoneNumberAsync(_userManager.GetCurrentUserId(), null).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
            if (user != null)
            {
                await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(_userManager.GetCurrentUserId(), model.NewPassword).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
                    if (user != null)
                    {
                        await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                addErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            // This code allows you exercise the flow without actually sending codes
            // For production use please register a SMS provider in IdentityConfig and generate a code here.
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(_userManager.GetCurrentUserId(), phoneNumber).ConfigureAwait(false);
            ViewBag.Status = "For DEMO purposes only, the current code is " + code;
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Account/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePhoneNumberAsync(_userManager.GetCurrentUserId(), model.PhoneNumber, model.Code).ConfigureAwait(false);
            if (result.Succeeded)
            {
                var user = await _userManager.GetCurrentUserAsync().ConfigureAwait(false);
                if (user != null)
                {
                    await _applicationSignInManager.RefreshSignInAsync(user, isPersistent: false).ConfigureAwait(false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }
        private void addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}