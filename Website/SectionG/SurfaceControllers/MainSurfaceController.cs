﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using SectionG.Code;
using SectionG.Code.Pocos;
using System.Net.Mail;
using Umbraco.Core;
using Umbraco.Web;
using Newtonsoft.Json.Linq;
namespace SectionG.SurfaceControllers
{
    public class MainController : Umbraco.Web.Mvc.SurfaceController
    {

        [System.Web.Mvc.HttpPost]
        public ActionResult SubmitLease()
        {
            
            // Pick up parameters
            var price = -1;
            int.TryParse(Request.Params["price"], out price);

            // Validation
            var hasError = false;
            var errorMessage = "";

            // Validate the email with MailMessage
            var courriel = "seb@ldo.ldo";
            try
            {
                var message = new MailMessage { From = new MailAddress(courriel) };
            }
            catch
            {
                hasError = true;
                errorMessage += "Invalid email format" + "<br/>";
            }

            // Required fields
            if (string.IsNullOrWhiteSpace(courriel) || price == -1)
            {
                hasError = true;
                errorMessage += "All fields required" + "<br/>";
            }

            if (hasError)
            {
                return Content("no - validation failed - " + errorMessage);
            }

            // Database entry
            var db = new PetaPoco.Database("umbracoDbDSN");
            var lease = new Lease();
            // lease.Id = "";
            lease.Price = price;
            lease.DateCreated = DateTime.Now;
            lease.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            db.Insert(lease);

            return Content("ok");
            
        }

    }
}