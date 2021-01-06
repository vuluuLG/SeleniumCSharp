using Framework.Test.Common.DriverWrapper;
using Framework.Test.Common.DriverWrapper.Browser;
using Framework.Test.Common.Helper;
using Framework.Test.UI.DataObject;
using Framework.Test.UI.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.UI.Helper
{
    public sealed class AccessTokenHelper
    {
        private static readonly object Instancelock = new object();
        private static string token = null;
        private static DateTime start_time = DateTime.MinValue;
        private const int EXPRIRES_AFTER_MINUTES = 55;
        private const string JWT_TOKEN_FILE = "c:\\temp\\jwt.txt";
        private const string STARTTIME_FILE = "c:\\temp\\starttime.txt";
        private const string TIME_FORMAT = "M/d/yyyy H:mm:ss";

        public static string GetAccessToken(string browserName, string url, string email, string password)
        {
            if (File.Exists(JWT_TOKEN_FILE)&& File.Exists(STARTTIME_FILE))
            {
                token = FileHelper.ReadAllTextFromFile(JWT_TOKEN_FILE);
                var dateIn = FileHelper.ReadAllTextFromFile(STARTTIME_FILE);
                try
                {
                    start_time = DateTime.ParseExact(dateIn, TIME_FORMAT, null);
                }
                catch { }
            }

            if (token == null || DateTime.Now.AddMinutes(-EXPRIRES_AFTER_MINUTES) >= start_time)
            {
                lock (Instancelock)
                {
                    if (token == null || DateTime.Now.AddMinutes(-EXPRIRES_AFTER_MINUTES) >= start_time)
                    {
                        GetAccessTokenFromBrowserStorage(browserName, url, email, password);
                        FileHelper.WriteAllTextToFile(JWT_TOKEN_FILE, token);
                        FileHelper.WriteAllTextToFile(STARTTIME_FILE, start_time.ToString(TIME_FORMAT));
                    }
                }
            }
            return token;
        }

        private static void GetAccessTokenFromBrowserStorage(string browserName, string baseUrl, string email, string password)
        {
            try
            {
                //0. Prepare web driver
                DriverProperty driverProperty = new DriverProperty();
                if (WebDriver.Driver == null)
                {
                    WebDriver.InitDriverManager(driverProperty);
                }

                //1. Open web app
                Browser.Open(browserName, baseUrl);
                LoginPage loginPage = new LoginPage();
                loginPage.WaitForPageLoad();

                Account account = new Account
                {
                    Email = email,
                    Password = password,
                    Success = true
                };

                //2. Login to Welcome Page
                WelcomePage welcomePage = loginPage.Login(account);

                //3. Get access token from browser local stroge
                token = WebDriver.ExecuteScript("return window.localStorage.access_token")?.ToString();
                if (string.IsNullOrWhiteSpace(token))
                {
                    token = WebDriver.ExecuteScript("return window.sessionStorage.access_token")?.ToString();
                }
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception("Can not extract access token from browser strorage.");
                }
                start_time = DateTime.Now;
                Console.WriteLine("Bearer Token: " + token);
            }
            finally
            {
                try
                {
                    WebDriver.Quit();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
