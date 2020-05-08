using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.GoogleReCaptchaV3.Models
{
    public class RecaptchaV3Properties : FormComponentProperties<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecaptchaV3Component" /> class.
        /// </summary>
        /// <remarks>
        /// The constructor initializes the base class to data type <see cref="FieldDataType.Text" /> and size 1.
        /// </remarks>
        public RecaptchaV3Properties()
            : base(FieldDataType.Text, 1, -1)
        {
        }

        /// <summary>
        /// Gets or sets the default value of the form component and underlying field.
        /// </summary>
        public override string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether the underlying field is required. False by default.
        /// If false, the form component's implementation must accept nullable input.
        /// </summary>
        public override bool Required { get; set; }
    }
}
