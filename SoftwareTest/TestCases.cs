using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;




namespace TestCases
{
    class TestCases
    {
  

        RemoteWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            string usernameENV = Environment.GetEnvironmentVariable("ENV_LT_USERNAME");
            string accessKeyENV = Environment.GetEnvironmentVariable("ENV_LT_ACCESS_KEY");

            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "114.0";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", usernameENV);
            ltOptions.Add("accessKey", accessKeyENV);
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("build", "ST");
            ltOptions.Add("project", "STDemo");
            ltOptions.Add("name", "Demo1");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-nunit");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);

            driver = new RemoteWebDriver(new Uri($"https://{usernameENV}:{accessKeyENV}@hub.lambdatest.com/wd/hub/"), capabilities);

        }

        [Test]
        public void homepageButtonTest()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");

            driver.Navigate().GoToUrl(testSiteURL);
            IWebElement logoClick = driver.FindElement(By.XPath("//*[@id=\"logo\"]/a/img"));

            logoClick.Click();

            IWebElement homeButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]"));
            Boolean logoVisible = homeButton.Displayed;

            Assert.IsTrue(logoVisible);

        }

        [Test]
        public void featuredProductsVisibility()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");

            driver.Navigate().GoToUrl(testSiteURL);
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]"));
            Boolean status = element.Displayed;

            Console.WriteLine(status);

            Assert.IsTrue(status);
        }

        [Test]
        public void registerAccountFunctionality()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");

            driver.Navigate().GoToUrl(testSiteURL);

            IWebElement openDropdownMenu = driver.FindElement(By.XPath("//*[@id=\"top\"]/div[2]/div[2]/ul/li[2]/div/a/i[2]"));
            openDropdownMenu.Click();

            IWebElement registerButton = driver.FindElement(By.XPath("//*[@id=\"top\"]/div[2]/div[2]/ul/li[2]/div/ul/li[1]/a"));
            registerButton.Click();

            RegisterAccount("RandomName", "RandomLastName", "randomemail@gmail.com", "RandomPassword");

            Console.WriteLine("Registered successfully.");



        }
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

        private void RegisterAccount(string firstName, string lastName, string email, string password)
        {
            var firstNameInputField = driver.FindElement(By.XPath("//*[@id=\"input-firstname\"]"));
            var lastNameInputField = driver.FindElement(By.XPath("//*[@id=\"input-lastname\"]"));
            var emailInputField = driver.FindElement(By.XPath("//*[@id=\"input-email\"]"));
            var passwordInputField = driver.FindElement(By.XPath("//*[@id=\"input-password\"]"));
            var newsletterClick = driver.FindElement(By.XPath("//*[@id=\"input-newsletter-yes\"]"));
            var agreeClick = driver.FindElement(By.XPath("//*[@id=\"form-register\"]/div/div/div/input"));
            var continueClick = driver.FindElement(By.XPath("//*[@id=\"form-register\"]/div/div/button"));

            firstNameInputField.SendKeys(firstName);
            lastNameInputField.SendKeys(lastName);
            emailInputField.SendKeys(email);
            passwordInputField.SendKeys(password);
            newsletterClick.Click();
            agreeClick.Click();
            continueClick.Click();

        }

    }
}