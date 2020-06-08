using CMS.EventLog;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace CitroDigital.InvisibleRecaptcha.Infrastructure
{
    public class InvisibleRecaptchaValidator
    {
        private const string VERIFYURL = "https://www.google.com/recaptcha/api/siteverify";
        private string mRemoteIp;

        /// <summary>The shared key between the site and reCAPTCHA.</summary>
        public string PrivateKey { get; set; }

        /// <summary>The user's IP address.</summary>
        public string RemoteIP
        {
            get
            {
                return mRemoteIp;
            }
            set
            {
                var ipAddress = IPAddress.Parse(value);
                if (ipAddress == null || ipAddress.AddressFamily != AddressFamily.InterNetwork && ipAddress.AddressFamily != AddressFamily.InterNetworkV6)
                    throw new ArgumentException("Expecting an IP address, got " + (object)ipAddress);
                mRemoteIp = ipAddress.ToString();
            }
        }

        /// <summary>
        /// The user response token provided by reCAPTCHA, verifying the user on your site.
        /// </summary>
        public string Response { get; set; }

        /// <summary>Validate reCAPTCHA response</summary>
        public InvisibleRecaptchaResponse Validate()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(VERIFYURL);
            httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.Timeout = 30000;
            httpWebRequest.Method = "POST";
            httpWebRequest.UserAgent = "reCAPTCHA/ASP.NET";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            var bytes = Encoding.ASCII.GetBytes("secret=" + HttpUtility.UrlEncode(PrivateKey) + "&remoteip=" + HttpUtility.UrlEncode(RemoteIP) + "&response=" + HttpUtility.UrlEncode(Response));
            using (var requestStream = httpWebRequest.GetRequestStream())
                requestStream.Write(bytes, 0, bytes.Length);
            try
            {
                using (var response = httpWebRequest.GetResponse())
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        return JsonConvert.DeserializeObject<InvisibleRecaptchaResponse>(streamReader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                EventLogProvider.LogException("ReCAPTCHA", "VALIDATE", (Exception)ex);
                return null;
            }
        }
    }
}
