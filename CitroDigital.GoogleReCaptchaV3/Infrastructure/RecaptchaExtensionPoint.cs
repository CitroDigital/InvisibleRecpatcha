using System;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CitroDigital.GoogleReCaptchaV3.Infrastructure
{
    public static class RecaptchaExtensionPoint
    {

        public static IHtmlString RecaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> instance,
            Expression<Func<TModel, TProperty>> expression, string action = null)
        {
            var id = instance.IdFor(expression).ToString();
            RenderRecaptchaScript(instance.ViewContext.Writer, id, action);
            return instance.HiddenFor(expression);
        }

        private static void RenderRecaptchaScript(TextWriter writer, string contolId, string action)
        {
            var scriptRender = new RecaptchaScriptElement(options => options.Action = action);
            scriptRender.Write(writer, contolId);
        }
    }
}
