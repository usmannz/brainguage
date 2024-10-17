using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common
{
    public static class AppSettings
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IHostEnvironment Environment { get; set; }

        public static string CacheConnection => Configuration["AppSettings:CacheConnectionString"];

        public static BuildMode BuildMode
        {
            get
            {
                return (BuildMode)Convert.ToInt32(Configuration["AppSettings:build-mode"]);
            }
        }

        public static bool UseMinify => Configuration["AppSettings:use-minify"].Equals("1");

        public static string EnvironmentName
        {
            get
            {
                return Configuration["AppSettings:environment-name"];
            }
        }

        public static string FromEmail
        {
            get
            {
                return Configuration["AppSettings:from-email"];
            }
        }

        public static string GoogleReCaptchaKey
        {
            get
            {
                return Configuration["AppSettings:GoogleReCaptchaKey"];
            }
        }

        public static string ToEmail
        {
            get
            {
                return Configuration["AppSettings:to-email"];
            }
        }

        public static string ToEmailCC
        {
            get
            {
                return Configuration["AppSettings:to-email-cc"];
            }
        }

        public static string ApiUrl
        {
            get
            {
                return Configuration["AppSettings:ApiUrl"];
            }
        }

        public static string AppVersion
        {
            get
            {
                return Configuration["AppSettings:app-version"];
            }
        }

        public static string SmtpUser
        {
            get
            {
                return Configuration["AppSettings:smtp-user"];
            }
        }

        public static string SmtpPwd
        {
            get
            {
                return Configuration["AppSettings:smtp-pwd"];
            }
        }

        public static string ExceptionEmail
        {
            get
            {
                return Configuration["AppSettings:exception-email"];
            }
        }

        public static string JwtSigningKey
        {
            get
            {
                return Configuration["AppSettings:JwtSigningKey"];
            }
        }

        public static int JwtLifeMins
        {
            get
            {
                return Convert.ToInt32(Configuration["AppSettings:JwtLifeMins"]);
            }
        }

        public static string InstanceName
        {
            get
            {
                return Configuration["AppSettings:instance-name"];
            }
        }

        public static string PathAssetDir => Configuration["AppSettings:pathAssetDir"];
        public static string PathAppData => Configuration["AppSettings:path-app-data"];
        public static string PathSeo => Path.Combine(Configuration["AppSettings:path-app-data"], "seo");
        public static string PathTemplatesDir => Path.Combine(AppSettings.PathAssetDir, $"templates");
        public static int UserImageWidth => Convert.ToInt32(Configuration["AppSettings:user-img-width"]);
        public static int UserImageHeight => Convert.ToInt32(Configuration["AppSettings:user-img-height"]);

        public static string GetUserFolderPath(string guid) => System.IO.Path.Combine($"questions", $"{guid}");
        public static string WebPathData => Configuration["AppSettings:web-path-data"];

        public static string SiteUrl => Configuration["AppSettings:siteUrl"];

        public static int LastMonthCountForSitemap => Convert.ToInt32(Configuration["AppSettings:lastMonthCountForSitemap"]);

        public static int MaxSitemapEntries => Convert.ToInt32(Configuration["AppSettings:maxSitemapEntries"]);

        public static string ConnectionStringDefault => Configuration["ConnectionStrings:default"];
        //public static string ConnectionStringHangfire => Configuration["ConnectionStrings:hangfire"];

        public static string ResetUrl => Configuration["AppSettings:reset-url"];
        public static string EmailVerify => Configuration["AppSettings:email-verify"];

        public static string PasswordResetTokenExpiry => Configuration["AppSettings:pwd-reset-token-expiry"];
        public static string LicenseTypeTrial => Configuration["AppSettings:license-type-trial"];

        public static string StripePublicKey => Configuration["AppSettings:StripeSettings:PublicKey"];
        public static string StripeWHKey => Configuration["AppSettings:StripeSettings:WHSecret"];
 //public static MailSettings MailSettings
 //   {
 //       get
 //       {
 //           var mailSettingsSection = Configuration.GetSection("AppSettings:MailSettings");
 //           return new MailSettings
 //           {
 //               Host = mailSettingsSection["Host"],
 //               Port = int.Parse(mailSettingsSection["Port"]),
 //               DisplayName = mailSettingsSection["DisplayName"],
 //               Mail = mailSettingsSection["Mail"],
 //               Username = mailSettingsSection["Username"],
 //               Password = mailSettingsSection["Password"]
 //           };
 //       }
 //   }
    }
}
