using System;
using System.Web.Mvc;

namespace CitroDigital.GoogleReCaptchaV3
{
    public static class ReCaptchaHtmlHelperExtensions
    {
        public static void Render(this RecaptchaExtensionPoint<HtmlHelper> helper, Action<RecaptchaOptions> options)
        {
            var script = new RecaptchaScriptElement(options);
            script.Write(helper.Instance.ViewContext.Writer);
        } 
    }
}
