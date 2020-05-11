using CitroDigital.InvisibleRecaptcha.Infrastructure;
using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CitroDigital.InvisibleRecaptcha.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class InvisibleRecaptchaAttribute : ValidationAttribute
    {
        private static readonly Regex mRegex = new Regex("[^a-zA-Z_]");

        private string mPublicKey;
        private string mPrivateKey;
        private bool? mSkipRecaptcha;
        private string mAction;
        /// <summary>
        /// Sets the action paramater to validate
        /// </summary>
        public string Action
        {
            get { return mAction; }
            set { mAction = mRegex.Replace(value, string.Empty); }
        }

        /// <summary>
        /// Sets the score value to validate default is 0.5
        /// </summary>
        public double Score { get; set; } = 0.5;


        private string PublicKey
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

        private string PrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(mPrivateKey))
                    mPrivateKey = SettingsKeyInfoProvider.GetValue("CMSRecaptchaPrivateKey", SiteContext.CurrentSiteName);
                return mPrivateKey;
            }
            set
            {
                mPrivateKey = value;
            }
        }

        private bool IsConfigured => this.AreKeysConfigured && !this.SkipRecaptcha;

        private bool SkipRecaptcha
        {
            get
            {
                if (!this.mSkipRecaptcha.HasValue)
                    this.mSkipRecaptcha =
                        ValidationHelper.GetBoolean(SettingsHelper.AppSettings["RecaptchaSkipValidation"], false);
                return this.mSkipRecaptcha.Value;

            }
        }

        private bool AreKeysConfigured => !string.IsNullOrEmpty(this.PublicKey) && !string.IsNullOrEmpty(this.PrivateKey);

        public InvisibleRecaptchaAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            var responseValue = ValidationHelper.GetString(value, string.Empty);
            if (!IsConfigured | VirtualContext.IsInitialized)
                return false;

            var recaptchaValidator = new InvisibleRecaptchaValidator
            {
                PrivateKey = PrivateKey,
                RemoteIP = RequestContext.UserHostAddress,
                Response = responseValue
            };

            var recaptchaResponse = recaptchaValidator.Validate();
            if (recaptchaResponse != null)
            {
                if (!string.IsNullOrEmpty(recaptchaResponse.ErrorMessage))
                    return false;
                if (Action != null && !CMSString.Equals(Action, recaptchaResponse.Action))
                    return false;
                if (recaptchaResponse.Score < Score)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
