namespace CitroDigital.GoogleReCaptchaV3
{
    public class RecaptchaExtensionPoint<TModel> where TModel : class
    {
        public TModel Instance { get; set; }

        public RecaptchaExtensionPoint(TModel instance)
        {
            Instance = instance;
        }
    }
}
