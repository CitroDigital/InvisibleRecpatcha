using CitroDigital.GoogleReCaptchaV3.Models;
using CMS.Base;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using Kentico.Forms.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[assembly: RegisterFormComponent(
    RecaptchaV3Component.IDENTIFIER,
    typeof(RecaptchaV3Component),
    "{$Google.Recaptcha.V3.Name$}",
    Description = "{$Google.Recaptcha.V3.Name$}",
    IconClass = "icon-recaptcha",
    ViewName = "~/Views/Shared/Kentico/Selectors/FormComponents/_RecaptchaV3.cshtml"
)]

namespace CitroDigital.GoogleReCaptchaV3.Models
{
    public class RecaptchaV3Component : FormComponent<RecaptchaV3Properties, string>
    {
        public const string IDENTIFIER = "Google.Recaptcha.V3";
        private string mPublicKey;
        private string mPrivateKey;
        private bool? mSkipRecaptcha;

        
        [BindableProperty]
        public string RecaptchaResponse { get; set; }

        public string Action
        {
            get { return Properties.Action; }
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
            var recaptchaValidator = new RecaptchaValidator
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
            }
            else
                validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.serverunavailable", (string)null, true)));
            return (IEnumerable<ValidationResult>)validationResultList;
        }
    }
}
