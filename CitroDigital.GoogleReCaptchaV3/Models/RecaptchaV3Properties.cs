using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.GoogleReCaptchaV3.Models
{
    public class RecaptchaV3Properties : FormComponentProperties<string>
    {
        public RecaptchaV3Properties()
            : base(FieldDataType.Text, 1, -1)
        {
        }

        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Action")]
        public string Action { get; set; } = string.Empty;

        public override string DefaultValue { get; set; }

        public override bool Required { get; set; }
    }
}
