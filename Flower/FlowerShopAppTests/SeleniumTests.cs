using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace FlowerShopAppTests.Mocks
{
    public class SeleniumTest : TestsBase
    {

        [Fact]
        public void Check_CanLoginWithoutEntryAnyData()
        {
            Driver.FindElement(By.XPath("//a[contains(.,'Log')]")).Click();
            Driver.FindElement(By.XPath("//button[contains(.,'Log')]")).Click();
            var message = Driver.FindElement(By.XPath("//li[contains(.,'required')]")).Text;
            Assert.Contains("field is required", message);
        }

        [Fact]
        public void Check_LoginWithProperValues_ShouldLogin()
        {
            var email = "xvoxin@ok.pl";
            var password = "niepodamcipluszaczku:3";

            Driver.FindElement(By.XPath("//a[contains(.,'Log')]")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.XPath("//button[contains(.,'Log')]")).Click();

            var message = Driver.FindElement(By.XPath("//h2[contains(.,'Hello')]")).Text;
            Assert.Contains("Hello " + email, message);
        }
    }

    public abstract class TestsBase : IDisposable
    {
        public IWebDriver Driver { get; set; }
        protected TestsBase()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://flowershopmfi.azurewebsites.net/");
        }

        public void Dispose()
        {
            Driver.Close();
        }
    }
}
