using System.Web.Mvc;

namespace CitroDigital.GoogleReCaptchaV3
{
    public static class RecaptchaExtensionPointExtensions
    {
        public static RecaptchaExtensionPoint<HtmlHelper> ReCaptcha(this HtmlHelper helper) =>
            new RecaptchaExtensionPoint<HtmlHelper>(helper);

    }
}
