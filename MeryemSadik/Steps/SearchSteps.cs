using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using MeryemSadik.Pages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;
using MeryemSadik;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(6)]
namespace MeryemSadik.Features 
{

    [Binding]
    [Parallelizable(ParallelScope.Self)]
    public class SearchSteps : SearchPage
    {
        [Before]
        public void startchrome()
        {
            Setup();
            ExtentTestManager.CreateTest();
        }




        [After]
        public void CloseChrome()
        {
            OneTimeTearDown();
        }


    
        [Given(@"Given The user opens \[https://demoqa\.com/text-box] in the web browser")]
        public void GivenGivenTheUserOpensHttpsDemoqa_ComText_BoxInTheWebBrowser()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/text-box");
            System.Threading.Thread.Sleep(5000);
        }

        [When(@"the user fill in the Full Name with \[Errortesttname\$&]")]
        public void WhenTheUserFillInTheFullNameWithErrortesttname()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).SendKeys("Errortesttname$&");
        }

        [When(@"the User press the \[Submit] button")]
        public void WhenTheUserPressTheSubmitButton()
        {
            FindElementAndWaitClickable(By.XPath("//button[@id=\"submit\"]")).Click();
        }

        [Then(@"an alert error will appear")]
        public void ThenAnAlertErrorWillAppear()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = driver.FindElement(By.XPath($"//*[@id=\"output\"]//p[@id=\"name\"][contains(text(),'Errortesttname$&')]"));
                Assert.IsNotNull(result);
            });
        }

        [When(@"the user fill in the Email with \[Thisisnotanemail]")]
        public void WhenTheUserFillInTheEmailWithThisisnotanemail()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).SendKeys("Thisisnotanemail");
        }

        [Then(@"an error red indicator appears in the Email texbox")]
        public void ThenAnErrorRedIndicatorAppearsInTheEmailTexbox()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = driver.FindElement(By.XPath($"//*[@class=\"mr-sm-2 field-error form-control\"]"));
                Assert.IsNotNull(result);
            });
        }

    


        [When(@"the user fill in the Full Name with \[John Smith]")]
        public void WhenTheUserFillInTheFullNameWithJohnSmith()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).SendKeys("John Smith");
        }

        [When(@"fill in the Email with \[john\.smith@mailinator\.com]")]
        public void WhenFillInTheEmailWithJohn_SmithMailinator_Com()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).SendKeys("john.smith@mailinator.com");
        }

        [When(@"fill in the Current Address with \[Street Smith (.*), London, UK]")]
        public void WhenFillInTheCurrentAddressWithStreetSmithLondonUK(int p0)
        {
            FindElementAndWaitClickable(By.XPath("//*[@id=\"currentAddress\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//*[@id=\"currentAddress\"]")).SendKeys("Street Smith 3, London, UK");

        }

        [When(@"fill in the Permanent Address with \[Street Smith (.*), London,UK]")]
        public void WhenFillInThePermanentAddressWithStreetSmithLondonUK(int p0)
        {
            FindElementAndWaitClickable(By.XPath("//*[@id=\"permanentAddress\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//*[@id=\"permanentAddress\"]")).SendKeys("Street Smith 6, London,UK");

        }

        [Then(@"the user can see the output results with the correct data")]
        public void ThenTheUserCanSeeTheOutputResultsWithTheCorrectData()
        {
            Assert.DoesNotThrow(() =>
            {
                var selectName = driver.FindElement(By.XPath("//*[@id=\"name\"]"));
                Assert.IsTrue(selectName.Text.Trim() == "Name:John Smith", "Check enteries!!");
                var selectEmail = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
                Assert.IsTrue(selectEmail.Text.Trim() == "Email:john.smith@mailinator.com", "Check enteries!!");
                var selectCurrentAddress = driver.FindElement(By.XPath("//*[@id=\"output\"]//p[@id=\"currentAddress\"]"));
                Assert.IsTrue(selectCurrentAddress.Text.Trim() == "Current Address :Street Smith 3, London, UK", "Check enteries!!");
                var selectPermanentAddress = driver.FindElement(By.XPath("//*[@id=\"output\"]//p[@id=\"permanentAddress\"]"));
                Assert.IsTrue(selectPermanentAddress.Text.Trim() == "Permananet Address :Street Smith 6, London,UK", "Check enteries!!");

            });

        }

        [When(@"the user fill in the Full Name with \[test user]")]
        public void WhenTheUserFillInTheFullNameWithTestUser()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userName\"]")).SendKeys("test user");
        }

        [When(@"fill in the Email with \[test@blabla\.com]")]
        public void WhenFillInTheEmailWithTestBlabla_Com()
        {
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//input[@id=\"userEmail\"]")).SendKeys("test@blabla.com");
        }

        [When(@"fill in the Current Address with \[C\. Dobla, (.*), (.*) Madrid, Spain]")]
        public void WhenFillInTheCurrentAddressWithC_DoblaMadridSpain(int p0, int p1)
        {
            FindElementAndWaitClickable(By.XPath("//*[@id=\"currentAddress\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//*[@id=\"currentAddress\"]")).SendKeys("C. Dobla, 5, 28054 Madrid, Spain");
        }
        [When(@"fill in the Permanent Address with \[Street X, (.*) Madrid, Spain]")]
        public void WhenFillInThePermanentAddressWithStreetXMadridSpain(int p0)
        {
            FindElementAndWaitClickable(By.XPath("//*[@id=\"permanentAddress\"]")).Click();
            FindElementAndWaitClickable(By.XPath("//*[@id=\"permanentAddress\"]")).SendKeys("Street X, 28013 Madrid, Spain");
        }
        [Then(@"the user can see the output results with the correct data sent")]
        public void ThenTheUserCanSeeTheOutputResultsWithTheCorrectDataSent()
        {
            var selectName = driver.FindElement(By.XPath("//*[@id=\"name\"]"));
            Assert.IsTrue(selectName.Text.Trim() == "Name:test user", "Check enteries!!");
            var selectEmail = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
            Assert.IsTrue(selectEmail.Text.Trim() == "Email:test@blabla.com", "Check enteries!!");
            var selectCurrentAddress = driver.FindElement(By.XPath("//*[@id=\"output\"]//p[@id=\"currentAddress\"]"));
            Assert.IsTrue(selectCurrentAddress.Text.Trim() == "Current Address :C. Dobla, 5, 28054 Madrid, Spain", "Check enteries!!");
            var selectPermanentAddress = driver.FindElement(By.XPath("//*[@id=\"output\"]//p[@id=\"permanentAddress\"]"));
            Assert.IsTrue(selectPermanentAddress.Text.Trim() == "Permananet Address :Street X, 28013 Madrid, Spain", "Check enteries!!");
        }
    }

}
