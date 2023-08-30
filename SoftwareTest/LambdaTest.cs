using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace SoftwareTestDemo
{
    class SoftwareTestDemo
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            string chromedriverPath = Environment.GetEnvironmentVariable("ENV_CHROMEDRIVER_PATH");
            
            if (chromedriverPath == null)
            {
                Console.WriteLine("No ENV variables found.");
            }
            driver = new ChromeDriver(chromedriverPath);
        }

        [Test]
        public async Task homepageButtonTest()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");
            driver.Url = testSiteURL;

            IWebElement logoClick = driver.FindElement(By.XPath("//*[@id=\"logo\"]/a/img"));

            await Task.Delay(1000);
            logoClick.Click();
            await Task.Delay(1000);

            IWebElement homeButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]"));
            Boolean logoVisible = homeButton.Displayed;

            Assert.IsTrue(logoVisible);

        }

        [Test]
        public async Task featuredProductsVisibility()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");
            driver.Url = testSiteURL;
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]"));
            Boolean status = element.Displayed;

            Console.WriteLine(status);
            
            await Task.Delay(1000);

            Assert.IsTrue(status);
        }

        [Test]
        public async Task registerAccountFunctionality()
        {
            string testSiteURL = Environment.GetEnvironmentVariable("ENV_TEST_SITE_URL");
            driver.Url = testSiteURL;

            IWebElement openDropdownMenu = driver.FindElement(By.XPath("//*[@id=\"top\"]/div[2]/div[2]/ul/li[2]/div/a/i[2]"));
            openDropdownMenu.Click();

            IWebElement registerButton = driver.FindElement(By.XPath("//*[@id=\"top\"]/div[2]/div[2]/ul/li[2]/div/ul/li[1]/a"));
            registerButton.Click();

            RegisterAccount("RandomName", "RandomLastName", "randomemail@gmail.com", "RandomPassword");

            Console.WriteLine("Registered successfully.");

            await Task.Delay(2000);


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