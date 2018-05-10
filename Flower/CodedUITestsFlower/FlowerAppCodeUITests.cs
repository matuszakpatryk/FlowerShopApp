using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITestsFlower
{
    /// <summary>
    /// Summary description for FlowerAppCodeUITests
    /// </summary>
    [CodedUITest]
    public class FlowerAppCodeUITests
    {
        public FlowerAppCodeUITests()
        {
        }

        [TestMethod]
        public void Check_HomeViewPartial_ShouldContainProperInformationWhenUserIsNotLogged()
        {
            this.UIMap.Check_HomeViewPartial_ShouldContainProperInformationWhenUserIsNotLogged();
            this.UIMap.Check_HomeViewPartial_ShouldContainProperInformationWhenUserIsNotLoggedAssert();
        }

        [TestMethod]
        public void Check_LoginWithoutEntryAnyData_ShouldReturnWarning()
        {

            this.UIMap.GoToLoginPage();
            this.UIMap.ClickLoginButton();
            this.UIMap.EmailErrorAssert();
        }

        [TestMethod]
        public void Check_LoginWithProperValues_ShouldLogin()
        {
            this.UIMap.GoToLoginPage();
            this.UIMap.InsertAdminCredentials();
            this.UIMap.ClickLoginButton();
            this.UIMap.WelcomeMessageContainsAfterLoginAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_RegisterWithWrongPassowrd_ShouldReturnWarning()
        {
            this.UIMap.GoToRegisterPageAndInsertInvalidPassword();
            this.UIMap.InvalidPasswordErrorsCountAssert();
        }

        [TestMethod]
        public void Check_LoggedUserClickedOnSellers_ShouldReturnAccessDeniedView()
        {
            this.UIMap.LoginAsNormalUser();
            this.UIMap.ClickOnSellersTab();
            this.UIMap.AccessDeniedAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_LoggedUserSearchCustomersWithSearchString_ShouldReturnProperTable()
        {
            this.UIMap.LoginAsNormalUser();
            this.UIMap.ClickCustomerAndInputStringIntoSearchString();
            this.UIMap.FirstTableItemContainsProperString();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_LoggedUserSearchCustomersWithWrongSearchString_ShouldReturnProperTable()
        {
            this.UIMap.LoginAsNormalUser();
            this.UIMap.GoToCustomerPageAndInsertWrongString();
            this.UIMap.EmptyListAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_LoggedUser_ShouldNotSeeEditDeleteAndDetailsButtons()
        {
            this.UIMap.LoginAsNormalUser();
            this.UIMap.GoToCustomerPage();
            this.UIMap.ControllerButtonsDoesNotExistsAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_LoggedAdmin_ShouldSeeEditDeleteAndDetailsButtons()
        {
            this.UIMap.DoEverything();
            this.UIMap.CustomersTableContainsProperButtons();
            this.UIMap.ProperButtonsAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_LoggedAdminClickOnDetails_ShouldSeeDetailView()
        {
            this.UIMap.LoginAsAdmin();
            this.UIMap.GoToCustomerPage();
            this.UIMap.GoToDetailsPage();
            this.UIMap.DetailsViewHasProperValuesAssert();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void Check_ClickOnProductsWhenUserIsNotLogged_ShouldReturnViewWithProductsTable()
        {
            this.UIMap.GoToProductsPageWhenNotLogged();
            this.UIMap.ProductsViewHasProperTitle();
        }


        [TestMethod]
        public void Check_ClickOnCustomersWhenUserIsNotLogged_ShouldReturnViewLoginView()
        {
            this.UIMap.GoToCustomerPageWhenNotLogged();
            this.UIMap.LoginTitleAssert();
        }







        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
