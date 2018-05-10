using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace FlowerShopAppTests.Mocks
{
    public class SeleniumTest
    {
        public IWebDriver Driver { get; set; }

        [Fact]
        public void Check_HomeViewPartial_ShouldContainProperInformationWhenUserIsNotLogged()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            var welcomeMessage = Driver.FindElement(By.Id("WelcomeMessage")).Text;
            Assert.Equal("Welcome stranger!", welcomeMessage);

            Driver.Close();
        }

        [Fact]
        public void Check_LoginWithoutEntryAnyData_ShouldReturnWarning()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Login")).Click();
            var emailMessage = Driver.FindElement(By.Id("Email-error")).Text;
            var passwordMessage = Driver.FindElement(By.Id("Password-error")).Text;
            Assert.Contains("field is required", emailMessage);
            Assert.Contains("field is required", passwordMessage);

            Driver.Close();
        }

        [Fact]
        public void Check_LoginWithProperValues_ShouldLogin()
        {
            var email = "testtest@wp.pl";
            var password = "Test123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();

            var message = Driver.FindElement(By.Id("WelcomeMessage")).Text;
            Assert.Contains("Hurra!", message);

            Driver.Close();
        }

        [Fact]
        public void Check_RegisterWithWrongPassowrd_ShouldReturnWarning()
        {
            string email = "OstatniUser@wp.pl";
            string pass = "zlehaslo";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("RegisterButton")).Click();
            Driver.FindElement(By.Id("EmailInput")).SendKeys(email);
            Driver.FindElement(By.Id("PasswordInput")).SendKeys(pass);
            Driver.FindElement(By.Id("ConfirmPasswordInput")).SendKeys(pass);
            Driver.FindElement(By.Id("Register")).Click();
            int size = Driver.FindElements(By.XPath("//ul")).Count;
            Assert.Equal(3, size);

            Driver.Close();
        }

        [Fact]
        public void Check_Register_ShouldRegisterAndLog()
        {
            string email = "newacc@wp.pl";
            string pass = "Pass123!";
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("RegisterButton")).Click();
            Driver.FindElement(By.Id("EmailInput")).SendKeys(email);
            Driver.FindElement(By.Id("PasswordInput")).SendKeys(pass);
            Driver.FindElement(By.Id("ConfirmPasswordInput")).SendKeys(pass);
            Driver.FindElement(By.Id("Register")).Click();
            var message = Driver.FindElement(By.Id("UserMessage")).Text;

            Assert.Contains(email, message);

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedUserClickedOnSellers_ShouldReturnAccessDeniedView()
        {
            var email = "useruser@wp.pl";
            var password = "User123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Sellers")).Click();

            var title = Driver.Title;
            Assert.Equal("Access denied - Flower", title);

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedUserSearchCustomersWithSearchString_ShouldReturnProperTable()
        {
            var email = "useruser@wp.pl";
            var password = "User123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Customers")).Click();
            Driver.FindElement(By.Name("SearchString")).SendKeys("Gol");
            Driver.FindElement(By.Id("SearchButton")).Click();

            int size = Driver.FindElements(By.Id("CustomersTable")).Count;

            Assert.Equal(2, size);

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedUserSearchCustomersWithWrongSearchString_ShouldReturnProperTable()
        {
            var email = "useruser@wp.pl";
            var password = "User123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Customers")).Click();
            Driver.FindElement(By.Name("SearchString")).SendKeys("bak");
            Driver.FindElement(By.Id("SearchButton")).Click();

            int size = Driver.FindElements(By.Id("CustomersTable")).Count;

            Assert.Equal(0, size);

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedUser_ShouldNotSeeEditDeleteAndDetailsButtons()
        {
            var email = "useruser@wp.pl";
            var password = "User123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Customers")).Click();

            Assert.Throws<NoSuchElementException>(() => Driver.FindElement(By.Id("EditButton")));
            Assert.Throws<NoSuchElementException>(() => Driver.FindElement(By.Id("DeleteButton")));
            Assert.Throws<NoSuchElementException>(() => Driver.FindElement(By.Id("DetailsButton")));

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedAdmin_ShouldSeeEditDeleteAndDetailsButtons()
        {
            var email = "testtest@wp.pl";
            var password = "Test123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Customers")).Click();

            Assert.NotNull(Driver.FindElement(By.Id("EditButton")));
            Assert.NotNull(Driver.FindElement(By.Id("DeleteButton")));
            Assert.NotNull(Driver.FindElement(By.Id("DetailsButton")));

            Driver.Close();
        }

        [Fact]
        public void Check_LoggedAdminClickOnDetails_ShouldSeeDetailView()
        {
            var email = "testtest@wp.pl";
            var password = "Test123!";

            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("LoginButton")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Password")).SendKeys(password);
            Driver.FindElement(By.Id("Login")).Click();
            Driver.FindElement(By.Id("Customers")).Click();

            Driver.FindElement(By.Id("DetailsButton")).Click();
            var name = Driver.FindElement(By.Id("ModelName")).Text;
            var surname = Driver.FindElement(By.Id("ModelSurname")).Text;

            Assert.Equal("Maria", name);
            Assert.Equal("Klimek", surname);

            Driver.Close();
        }

        [Fact]
        public void Check_ClickOnProductsWhenUserIsNotLogged_ShouldReturnViewWithProductsTable()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("Products")).Click();
            var productsTableName = Driver.FindElement(By.Id("ProductsTableName")).Text;

            Assert.Equal("Products", productsTableName);

            Driver.Close();
        }

        [Fact]
        public void Check_ClickOnCustomersWhenUserIsNotLogged_ShouldReturnViewLoginView()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory());
            Driver.Navigate().GoToUrl("https://localhost:44336/");

            Driver.FindElement(By.Id("Customers")).Click();
            var viewName = Driver.FindElement(By.Id("Title")).Text;

            Assert.Equal("Log in", viewName);

            Driver.Close();
        }
    }
}
