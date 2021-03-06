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
using PetaPoco;

namespace SectionG.SurfaceControllers
{
    public class MainController : Umbraco.Web.Mvc.SurfaceController
    {

        [System.Web.Mvc.HttpPost]
        public ActionResult SubmitLease(Lease lease)
        {
            
            // Pick up parameters
            var price = -1;
            int.TryParse(Request.Params["Prix"], out price);

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
 
            // Champs calculés
            lease.DateCreated = DateTime.Now;
            lease.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            // TODO: Mettre les valeurs du formulaire           
            lease.Details = "";
            lease.Inclusions = "";

            try
            {
                db.Insert(lease);
            }
            catch (Exception ex)
            {
                return Content("Insert error: " + ex.Message);
            }

            var listOfLeases = db.Fetch<Lease>(new Sql().Select("*").From("_sg_Lease").Where("Street = @0", lease.Street));

            return Json(listOfLeases);            
            //return Content("ok");            
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetLeases(Lease lease)
        {            
            var db = new PetaPoco.Database("umbracoDbDSN");            
           
            Sql sql = new Sql().Select("*").From("_sg_Lease");
            if (!string.IsNullOrEmpty(lease.Street)) sql = sql.Where("Street = @0", lease.Street);
            if (!string.IsNullOrEmpty(lease.AddressNumber)) sql = sql.Where("AddressNumber = @0", lease.AddressNumber);
            if (!string.IsNullOrEmpty(lease.AppartmentNumber)) sql = sql.Where("AppartmentNumber = @0", lease.AppartmentNumber);
            if (!string.IsNullOrEmpty(lease.PostalCode)) sql = sql.Where("PostalCode = @0", lease.PostalCode);

            var listOfLeases = db.Fetch<Lease>(sql);

            return Json(listOfLeases);
        }
    }
}