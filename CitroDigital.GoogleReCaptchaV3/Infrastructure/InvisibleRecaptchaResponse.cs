using CMS.FormEngine;
using Newtonsoft.Json;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    public class InvisibleRecaptchaResponse : RecaptchaResponse
    {
        [JsonProperty("action")]
        public string Action { get;set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}
