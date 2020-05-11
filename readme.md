# Invisible Recaptcha Form Component
Adds the [Google Invisible Recaptcha](https://developers.google.com/recaptcha/docs/v3) form component to your MVC Site.

# Installation
1. Install the `CitroDigital.InvisibleRecaptcha` [Nuget Package](https://www.nuget.org/packages/CitroDigital.InvisibleRecaptcha/) to your MVC Site


# Configuration
1. Configure reCAPTCHA
  1. Go to https://www.google.com/recaptcha/admin and sign in with your Google account.
  2. Select the reCAPTCHA v2 type (other reCAPTCHA types are not supported by default).
  3. Fill in all required details, including the domain where your site is running (the presentation domain of your MVC live site).
  4. Copy your Site key and Secret key.
2. In the Kentico Admin interface
  1. Open the Settings application in the administration interface.
  2. Navigate to the Security & Membership -> Protection settings category.
  3. Under CAPTCHA settings, paste the API keys into the reCAPTCHA site key and reCAPTCHA secret key settings respectively.
  4. Save the settings.
3. To remove the label from the form widget see [Adding contextual markup to forms](https://docs.kentico.com/k12sp/developing-websites/form-builder-development/customizing-the-form-widget#CustomizingtheFormwidget-Addingcontextualmarkuptoforms) of the form widget.
```csharp
public class FormFieldMarkupInjection
{
        public static void RegisterEventHandlers()
        {
            FormFieldRenderingConfiguration.GetConfiguration.Execute += InjectMarkupIntoKenticoComponents;
        }

        private static void InjectMarkupIntoKenticoComponents(object sender, GetFormFieldRenderingConfigurationEventArgs e)
        {
            //Hides the recaptcha form label on the field using display:none;
            e.HideInvisibleRecaptchaLabel();

            //To do it yourself using a class
            if (e.FormComponent.Definition.Identifier == InvisibleRecaptchaComponent.IDENTIFIER)
            {
                if (e.Configuration.LabelHtmlAttributes.ContainsKey("class"))
                {
                    e.Configuration.LabelHtmlAttributes["class"] += " hide";
                }
                else
                {
                    e.Configuration.LabelHtmlAttributes["class"] = "hide";
                }
            }
        }
}
```
4. Drag the Invisible reCAPTCHA form component in the Form Builder
5. Configure the Action and Score Properties
  1. Default Score if left blank is 0.5
  2. Default Action if left blank is the FormName


# Utilization outside of Kentico Forms
There is an HtmlHelper extensions `@Html.InvisibleRecaptchaFor(Expression<Func<TModel, TProperty>> expression, string action = null)` that you can utilize to render the invisible recaptcha inside of a razor form.

There is also a ValidationAttribute called InvisibleRecaptcha that will validate recaptcha property value, with the specified action

InvisibleRecaptcha Attribute has two properties Action and Score.

1. Action
  1. reCAPTCHA v3 introduces a new concept: actions. When you specify an action name in each place you execute reCAPTCHA, you enable the following new features: A detailed break-down of data for your top ten actions in the admin console Adaptive risk analysis based on the context of the action, because abusive behavior can vary. Importantly, when you verify the reCAPTCHA response, you should verify that the action name is the name you expect.
2. Score
  1. reCAPTCHA v3 returns a score (1.0 is very likely a good interaction, 0.0 is very likely a bot). Based on the score, you can take variable action in the context of your site. Every site is different, but below are some examples of how sites use the score. As in the examples below, take action behind the scenes instead of blocking traffic to better protect your site.
## Newsletter Subscription

### NewsletterSubscribeViewModel.cs
```csharp
    public class NewsletterSubscribeViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The Email address cannot be empty.")]
        [DisplayName("Email address")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(254, ErrorMessage = "The Email address cannot be longer than 254 characters.")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether the newsletter requires double-opt in for subscription.
        /// Allows the view to display appropriate information to newly subscribed users.
        /// </summary>
        [Bindable(false)]
        public bool RequireDoubleOptIn
        {
            get;
            set;
        }

        [InvisibleRecaptcha(Action = "NewsletterSubscription")]
        public string Recaptcha { get; set; }
    }
```


### NewsletterForm.cshtml
```csharp
@model NewsletterSubscribeViewModel

@Html.Kentico().AntiForgeryToken()
<div class="input-group margin-top-1">
    @Html.TextBoxFor(_ => _.Email, new { @class = "input-group-field", placeholder = "Email Address" })
    <div class="input-group-button">
        <input type="submit" class="button secondary" value="Submit" />
    </div>
</div>
<div class="input-group margin-top-1">
    @Html.ValidationMessageFor(_ => _.Email, "Please fill out your Email Address", htmlAttributes: new Dictionary<string, object>() { { "class", "form-error" } })
</div>

@Html.InvisibleRecaptchaFor(_ => _.Recaptcha, "NewsletterSubscription")
@Html.ValidationMessageFor(_ => _.Recaptcha)
```

