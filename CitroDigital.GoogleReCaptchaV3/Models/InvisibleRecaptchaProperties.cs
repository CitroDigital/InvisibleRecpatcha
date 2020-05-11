using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.InvisibleRecaptcha.Models
{
    public class InvisibleRecaptchaProperties : FormComponentProperties<string>
    {
        private const string ACTION_DESCRIPTION = "When you specify an action name in each place you execute reCAPTCHA, you gain access to A detailed break-down of data for your top ten actions and Adaptive risk analysis based on the context of the action, because abusive behavior can vary.";

        private const string SCORE_DESCRIPTION =
            "reCAPTCHA v3 returns a score (1.0 is very likely a good interaction, 0.0 is very likely a bot). Based on the score, you can take variable action in the context of your site. Every site is different, but below are some examples of how sites use the score. As in the examples below, take action behind the scenes instead of blocking traffic to better protect your site. Default value is 0.5";

        public InvisibleRecaptchaProperties()
            : base(FieldDataType.Text, 1, -1)
        {
        }

        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Action", ExplanationText = ACTION_DESCRIPTION, Tooltip = ACTION_DESCRIPTION)]
        public string Action { get; set; } = string.Empty;

        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Score", ExplanationText = SCORE_DESCRIPTION,
            Tooltip = SCORE_DESCRIPTION)]
        [EditingComponentProperty(nameof(IntInputProperties.DefaultValue), "0.5")]
        public string Score { get; set; } = "0.5";


        public override string DefaultValue { get; set; }

        public override bool Required { get; set; }
    }
}
