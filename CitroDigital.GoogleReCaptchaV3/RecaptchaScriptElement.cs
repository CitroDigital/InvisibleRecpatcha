using System;
using System.IO;
using System.Web;
using CMS.IO;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.GoogleReCaptchaV3
{
    internal class RecaptchaScriptElement
    {
        #region Private Variables

        private string mApiKey;
        private string mAction;
        private static string SCRIPT_TAG_FORMAT = @"<script src=""https://www.google.com/recaptcha/api.js?render={0}""></script>";

        #endregion

        #region Constructor

        public RecaptchaScriptElement(Action<RecaptchaOptions> options) : this(Build(options)) { }

        private RecaptchaScriptElement(RecaptchaOptions options)
        {
            mApiKey = options.Key ?? throw new ArgumentNullException(nameof(options.Key));
            mAction = options.Action;
        }

        #endregion

        public void Write(TextWriter viewContextWriter) =>
            viewContextWriter.Write($@"
                {string.Format(SCRIPT_TAG_FORMAT, mApiKey)}
                <script type=""text/javascript"">
                    grecaptcha.ready(function() {{
				        document.getElementById('g-recaptcha-response').value = token;
                    }});
                </script>
            ");

        #region Private Methods

        private static RecaptchaOptions Build(Action<RecaptchaOptions> configuration)
        {
            var options = new RecaptchaOptions();
            configuration(options);
            return options;
        }

        #endregion
    }
}
