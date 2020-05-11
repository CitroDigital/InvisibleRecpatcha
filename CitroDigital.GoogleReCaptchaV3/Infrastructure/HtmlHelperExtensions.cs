using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an invisible reCAPTCHA
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IHtmlString InvisibleRecaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            string action = null)
        {
            var id = helper.IdFor(expression).ToString();
            var scriptBuilder = new RecaptchaScriptElement(options => options.Action = action);
            var component = $@"
                 {scriptBuilder.Write(id)}
                 {helper.HiddenFor(expression)}
            ";

            return MvcHtmlString.Create(component);
        }
    }
}
