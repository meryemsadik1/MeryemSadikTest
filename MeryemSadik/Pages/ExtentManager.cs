using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeryemSadik.Pages
{
    class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            var htmlReporter = new ExtentHtmlReporter(TestContext.CurrentContext.TestDirectory + "\\Extent.trx");
            Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager()
        {
        }
    }
}
