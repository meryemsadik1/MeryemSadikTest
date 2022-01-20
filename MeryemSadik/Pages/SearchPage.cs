using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace MeryemSadik.Pages
{
    public class SearchPage
    {
        public static ExtentTest test;
        public static ExtentReports extent;
        public const int TimeOutMin = 1;
        protected IServiceProvider serviceProvider;

        public IWebDriver Browser { get; }
        public ILogs Logs { get; }

        protected IWebDriver driver;
        public HttpClient Client { get; }

        private const string HTTPEXCEPTION = "[HttpException]";
        protected IWebElement MoveToElement(By by, int seconds = 10)
        {
            Actions builder = new Actions(driver);
            var element = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementExists(by));
            builder.MoveToElement(element).Perform();
            return element;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            ExtentTestManager.CreateParentTest();
            CultureInfo cultureInfo = new CultureInfo("es-ES");

            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);

#if DEBUG
            options.AddArgument("--start-maximized");
#else
            var releaseDownloadPath = "C:\\Temp\\Selenium";
            if (!Directory.Exists(releaseDownloadPath))
            {
                Directory.CreateDirectory(releaseDownloadPath);
            }
            var files = Directory.EnumerateFiles(releaseDownloadPath);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
            options.AddArgument("headless");
            options.AddArgument("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", releaseDownloadPath);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            options.AddArguments("test-type");
            options.AddArgument("--disable-gpu");
            options.AddArgument("no-sandbox");
#endif
            options.AddArgument("disable-extensions");
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            options.AddArgument("--lang=es");
            options.AddArgument("--intl.accept_languages=es-ES");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(assemblyFolder, options, TimeSpan.FromMinutes(3));

#if !DEBUG
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["behavior"] = "allow";
            parameters["downloadPath"] = releaseDownloadPath;
            ((ChromeDriver)driver).ExecuteChromeCommand("Page.setDownloadBehavior", parameters);
#endif
            
        }

        [TearDown]
        public void TearDown()
        {

            if (driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
#if !DEBUG
                    try
                    {
                        var filename = Directory.GetCurrentDirectory() + TestContext.CurrentContext.Test.ID + ".png";
                        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(filename);
                        TestContext.AddTestAttachment(filename);
                    }
                    catch
                    {
                        
                    }                    
#endif
                }
                var handles = driver.WindowHandles;
                IAlert alert;
                foreach (var tab in handles.Skip(1))
                {
                    driver.SwitchTo().Window(tab);
                    alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                    while (alert != null)
                    {
                        driver.SwitchTo().Alert().Accept();
                        alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                    }
                    driver.Close();
                }
                driver.SwitchTo().Window(handles[0]);
                alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                while (alert != null)
                {
                    driver.SwitchTo().Alert().Accept();
                    alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            var errormsg = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message) ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            if (driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var filename = Directory.GetCurrentDirectory() + TestContext.CurrentContext.Test.ID + ".png";
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(filename);
                    TestContext.AddTestAttachment(filename);
                    switch (status)
                    {
                        case TestStatus.Failed:
                            logstatus = Status.Fail;
                            break;
                        case TestStatus.Inconclusive:
                            logstatus = Status.Warning;
                            break;
                        case TestStatus.Skipped:
                            logstatus = Status.Skip;
                            break;
                        default:
                            logstatus = Status.Pass;
                            break;
                    }

                    ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + errormsg + stacktrace, MediaEntityBuilder.CreateScreenCaptureFromPath(filename).Build());

                }


                var handles = driver.WindowHandles;
                IAlert alert;
                foreach (var tab in handles.Skip(1))
                {
                    driver.SwitchTo().Window(tab);
                    alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                    while (alert != null)
                    {
                        driver.SwitchTo().Alert().Accept();
                        alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                    }
                    driver.Close();
                }
                driver.SwitchTo().Window(handles[0]);
                alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                while (alert != null)
                {
                    driver.SwitchTo().Alert().Accept();
                    alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
                }
                driver.Close();
                driver.Quit();
                ExtentManager.Instance.Flush();
            }
        }
        protected IWebElement FindElementAndWait(By by, int seconds = 10)
        {
            return new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(by));
        }

        protected IWebElement FindElementAndWaitClickable(By by, int seconds = 10)
        {
            MoveToElement(by);
            return new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(by));
        }
        protected void Wait(int seconds)
        {
            System.Threading.Thread.CurrentThread.Join(seconds * 1000);
        }
        protected void WaitForLoadToFinish()
        {
            Wait(2);
            new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromMinutes(TimeOutMin)).Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[@aria-label=\"Loading content\"]")));
        }


  

      
    }
}
