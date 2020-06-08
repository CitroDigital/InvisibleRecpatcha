using CitroDigital.InvisibleRecaptcha.Infrastructure;
using CitroDigital.InvisibleRecaptcha.Models;
using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using Kentico.Forms.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CMS.EventLog;
using Lucene.Net.Support;
using Newtonsoft.Json;

[assembly: RegisterFormComponent(
    InvisibleRecaptchaComponent.IDENTIFIER,
    typeof(InvisibleRecaptchaComponent),
    "{$Google.InvisibleRecaptcha.Name$}",
    Description = "{$Google.InvisibleRecaptcha.Name$}",
    IconClass = "icon-recaptcha",
    ViewName = "~/Views/Shared/Kentico/Selectors/FormComponents/_InvisibleRecaptcha.cshtml"
)]

namespace CitroDigital.InvisibleRecaptcha.Models
{
    public class InvisibleRecaptchaComponent : FormComponent<InvisibleRecaptchaProperties, string>
    {
        private static readonly Regex mRegex = new Regex("[^a-zA-Z_]");
        public const string IDENTIFIER = "Google.Recaptcha.Invisible";
        private string mPublicKey;
        private string mPrivateKey;
        private bool? mSkipRecaptcha;

        [BindableProperty]
        public string RecaptchaResponse { get; set; }

        public string Action
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Properties.Action))
                {
                    return this.GetBizFormComponentContext()?.FormInfo.FormName;
                }

                return mRegex.Replace(Properties.Action, string.Empty);
            }
        }

        public double Score
        {
            get
            {
                if (double.TryParse(Properties.Score, out var score))
                {
                    return score;
                }
                //Default score
                return 0.5;
            }
        }

        public string Value { get; set; }

        public string PublicKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.mPublicKey))
                    this.mPublicKey = SettingsKeyInfoProvider.GetValue("CMSRecaptchaPublicKey", SiteContext.CurrentSiteName);
                return this.mPublicKey;
            }
            set
            {
                this.mPublicKey = value;
            }
        }

        public string PrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.mPrivateKey))
                    this.mPrivateKey = SettingsKeyInfoProvider.GetValue("CMSRecaptchaPrivateKey", SiteContext.CurrentSiteName);
                return this.mPrivateKey;
            }
            set
            {
                this.mPrivateKey = value;
            }
        }

        public bool IsConfigured
        {
            get
            {
                return this.AreKeysConfigured && !this.SkipRecaptcha;
            }
        }

        private bool SkipRecaptcha
        {
            get
            {
                if (!this.mSkipRecaptcha.HasValue)
                    this.mSkipRecaptcha = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["RecaptchaSkipValidation"], false);
                return this.mSkipRecaptcha.Value;
            }
        }

        private bool AreKeysConfigured
        {
            get
            {
                return !string.IsNullOrEmpty(this.PublicKey) && !string.IsNullOrEmpty(this.PrivateKey);
            }
        }

        public override string LabelForPropertyName
        {
            get
            {
                return string.Empty;
            }
        }

        public override string GetValue()
        {
            return string.Empty;
        }

        public override void SetValue(string value)
        {
        }

        /// <summary>Performs validation of the reCAPTCHA component.</summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A collection that holds failed-validation information.</returns>
        public override IEnumerable<ValidationResult> Validate(
          ValidationContext validationContext)
        {
            var validationResultList = new List<ValidationResult>();
            validationResultList.AddRange(base.Validate(validationContext));
            if (!this.IsConfigured | VirtualContext.IsInitialized)
                return (IEnumerable<ValidationResult>)validationResultList;
            var recaptchaValidator = new InvisibleRecaptchaValidator
            {
                PrivateKey = PrivateKey,
                RemoteIP = RequestContext.UserHostAddress,
                Response = RecaptchaResponse
            };

            var recaptchaResponse = recaptchaValidator.Validate();
            if (recaptchaResponse != null)
            {
                if (!string.IsNullOrEmpty(recaptchaResponse.ErrorMessage))
                    validationResultList.Add(new ValidationResult(recaptchaResponse.ErrorMessage));
                if (Action != null && !CMSString.Equals(Action, recaptchaResponse.Action))
                    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.actioninvalid")));
                if (recaptchaResponse.Score < Score)
                    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.scoreinvalid")));
            }
            else
            {
                validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.serverunavailable",
                    (string)null, true)));
            }

            return (IEnumerable<ValidationResult>)validationResultList;
        }
    }
}
