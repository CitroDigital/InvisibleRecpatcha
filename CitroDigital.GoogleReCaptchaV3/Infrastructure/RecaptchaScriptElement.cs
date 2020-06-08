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

        public RecaptchaScriptElement(Action<InvisibleRecaptchaOptions> options) : this(Build(options)) { }

        private RecaptchaScriptElement(InvisibleRecaptchaOptions options)
        {
            mApiKey = RecaptchaSettings.TryGetKey();
            mAction = options.Action;
        }


        #region Private Methods

        private static InvisibleRecaptchaOptions Build(Action<InvisibleRecaptchaOptions> configuration)
        {
            var options = new InvisibleRecaptchaOptions();
            configuration(options);
            return options;
        }

        

        #endregion
    }
}
