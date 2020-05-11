using CitroDigital.InvisibleRecaptcha.Models;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    public static class InvisibleRecaptchaMarkupRegistration
    {
        public static void HideInvisibleRecaptchaLabel(this GetFormFieldRenderingConfigurationEventArgs e)
        {
            if (e.FormComponent.Definition.Identifier == InvisibleRecaptchaComponent.IDENTIFIER)
            {
                if (e.Configuration.LabelHtmlAttributes.ContainsKey("style"))
                {
                    e.Configuration.LabelHtmlAttributes["style"] += " display:none;";
                }
                else
                {
                    e.Configuration.LabelHtmlAttributes["style"] = "display:none;";
                }
            }
        }
    }
}
