using System;
using System.IO;
using CMS.Core;
using CMS.DataEngine;
using CMS.SiteProvider;

namespace CitroDigital.GoogleReCaptchaV3.Infrastructure
{
    public class RecaptchaScriptElement
    {
        #region Private Variables

        private readonly string mApiKey;
        private readonly string mAction;

        #endregion

        #region Constructor

        public RecaptchaScriptElement(Action<RecaptchaOptions> options) : this(Build(options)) { }

        private RecaptchaScriptElement(RecaptchaOptions options)
        {
            mApiKey = TryGetKey() ?? throw new ArgumentNullException(nameof(options), @"Please set API key in Kentico Admin Settings via CMSRecaptchaPublicKey or in web.config <add key=""RecaptchaV3Key"" value=""<key>"" />");
            mAction = options.Action;
        }

        #endregion

        public void Write(TextWriter viewContextWriter, string controlId)
        {
            viewContextWriter.Write($@"<script src=""https://www.google.com/recaptcha/api.js?&render={mApiKey}""></script>");
            viewContextWriter.Write(@"<script type=""text/javascript"">");
            viewContextWriter.Write(@"grecaptcha.ready(function() {");
            viewContextWriter.Write($@"grecaptcha.execute('{mApiKey}'");
            if (!string.IsNullOrWhiteSpace(mAction))
            {
                viewContextWriter.Write($@", {{action: '{mAction}'}}");
            }
            viewContextWriter.Write(@").then(function(token) {");
            viewContextWriter.Write($@"document.getElementById(""{controlId}"").value = token;");
            viewContextWriter.Write(@"}); });");
            viewContextWriter.Write(@"</script>");
        }

        #region Private Methods

        private static RecaptchaOptions Build(Action<RecaptchaOptions> configuration)
        {
            var options = new RecaptchaOptions();
            configuration(options);
            return options;
        }

        private static string TryGetKey()
        {
            var settingsKey = 
                SettingsKeyInfoProvider.GetValue("CMSRecaptchaPublicKey", SiteContext.CurrentSiteID);

            if (string.IsNullOrWhiteSpace(settingsKey))
                settingsKey = CoreServices.AppSettings["RecaptchaV3Key"];

            return string.IsNullOrWhiteSpace(settingsKey) ? null : settingsKey;
        }

        #endregion
    }
}
