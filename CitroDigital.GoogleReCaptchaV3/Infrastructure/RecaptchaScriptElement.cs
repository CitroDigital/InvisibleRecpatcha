using CMS.Core;
using CMS.DataEngine;
using CMS.SiteProvider;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    internal class RecaptchaScriptElement
    {
        #region Private Variables

        private readonly string mApiKey;
        private readonly string mAction;

        #endregion

        #region Constructor

        public RecaptchaScriptElement(Action<InvisibleRecaptchaOptions> options) : this(Build(options)) { }

        private RecaptchaScriptElement(InvisibleRecaptchaOptions options)
        {
            mApiKey = TryGetKey();
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

        public IHtmlString Write(string id)
        {
            var sb = new StringBuilder();
            using (var writter = new StringWriter(sb))
            {
                Write(writter, id);
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        #region Private Methods

        private static InvisibleRecaptchaOptions Build(Action<InvisibleRecaptchaOptions> configuration)
        {
            var options = new InvisibleRecaptchaOptions();
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
