﻿using MwangiFinancial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MwangiFinancial.ExtensionMethods
{
    public static class InvitationExtensions
    {
        public static ApplicationDbContext db = new ApplicationDbContext();

        public static async Task SendAsync(this Invitation invitation)
        {
            try
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var callbackUrl = urlHelper.Action("InviteRegister", "Account", new { email = invitation.ReciepientEmail, code = invitation.Code, householdId = invitation.HouseholdId }, protocol: HttpContext.Current.Request.Url.Scheme);
                var house = db.Households.Find(invitation.HouseholdId);
                var from = "MwangiFinancial<smtp.mosesmwangi1979@gmail.com>";
                var email = new MailMessage(from, invitation.ReciepientEmail)
                {
                    Subject = $"You have been invited to join {house.Name}.",
                    Body = $"<p>You've been invited by {invitation.ReciepientEmail} to plan your finances together on DT Financial Portal!<p><br><br><p>Click <a href={callbackUrl}>here</a> to register as a new user and accept the invite!<p><br> Or, if you're already a registered user, use this code in the Join Household panel of the lobby:{invitation.Code}",
                    IsBodyHtml = true
                };
                var svc = new PersonalEmail();
                await svc.SendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
        }
    }
}