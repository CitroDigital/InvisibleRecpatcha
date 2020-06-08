using CMS.Core;
using CMS.DataEngine;
using CMS.SiteProvider;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    internal static class RecaptchaSettings
    {
        public static string TryGetKey()
        {
            var settingsKey =
                SettingsKeyInfoProvider.GetValue("CMSRecaptchaPublicKey", SiteContext.CurrentSiteID);

            if (string.IsNullOrWhiteSpace(settingsKey))
                settingsKey = CoreServices.AppSettings["RecaptchaV3Key"];

            return string.IsNullOrWhiteSpace(settingsKey) ? null : settingsKey;
        }
    }
}
