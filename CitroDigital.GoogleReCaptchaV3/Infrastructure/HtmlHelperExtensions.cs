using System;
using System.Linq.Expressions;
using System.Text;
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
            var sb = new StringBuilder();
            var apiKey = RecaptchaSettings.TryGetKey();
            sb.Append($@"<script src=""~/Scripts/recaptcha/loader.js""></script>");
            sb.Append($@"<script type=""text/javascript"">               
                    grecaptcha.ready(function() {{onRecaptchaLoad('{apiKey}', '{helper.IdFor(expression)}'); }});
            </script>");
            sb.Append($@"
                {helper.HiddenFor(expression, new
            {
                data_action = action,
                data_recaptcha = "",
            })}
             ");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Injects the script tag to render reCAPTCHA on the site
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IHtmlString RenderRecaptchaLib(this HtmlHelper helper, string key = null)
        {
            var apiKey = key ?? RecaptchaSettings.TryGetKey();
            return MvcHtmlString.Create($@"<script src=""https://www.google.com/recaptcha/api.js?render={apiKey}""></script>");
        }
    }
}
