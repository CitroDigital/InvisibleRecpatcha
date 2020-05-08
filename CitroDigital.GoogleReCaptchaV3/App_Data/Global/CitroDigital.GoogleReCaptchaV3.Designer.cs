﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CitroDigital.GoogleReCaptchaV3.App_Data.Global {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CitroDigital_GoogleReCaptchaV3 {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CitroDigital_GoogleReCaptchaV3() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CitroDigital.GoogleReCaptchaV3.App_Data.Global.CitroDigital.GoogleReCaptchaV3", typeof(CitroDigital_GoogleReCaptchaV3).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to reCAPTCHA
        ///Provides a robust CAPTCHA service based on reCAPTCHA, which requires the user to click a checkbox indicating the user is not a robot. This will either pass the user immediately (with No CAPTCHA) or challenge them to validate whether or not they are human..
        /// </summary>
        internal static string Google_Recaptcha_V3_Description {
            get {
                return ResourceManager.GetString("Google.Recaptcha.V3.Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to reCAPTCHA V3.
        /// </summary>
        internal static string Google_Recaptcha_V3_Name {
            get {
                return ResourceManager.GetString("Google.Recaptcha.V3.Name", resourceCulture);
            }
        }
    }
}
