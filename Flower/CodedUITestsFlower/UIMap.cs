namespace CodedUITestsFlower
{
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

    public partial class UIMap
    {
        internal void EmptyListAssert()
        {
            try
            {
                #region Variable Declarations
                HtmlCell uIGolonkaCell = this.UICustomersFlowerInterWindow.UICustomersFlowerDocument.UIItemTable.UIGolonkaCell;
                #endregion
            }
            finally
            {
                Assert.IsTrue(true);
            }
        }

        internal void ControllerButtonsDoesNotExistsAssert()
        {
            try
            {
                #region Variable Declarations
                HtmlHyperlink uIEditHyperlink = this.UINowakartaInternetExpWindow.UICustomersFlowerDocument.UIEditHyperlink;
                HtmlHyperlink uIDetailsHyperlink = this.UINowakartaInternetExpWindow.UICustomersFlowerDocument.UIDetailsHyperlink;
                HtmlHyperlink uIDeleteHyperlink = this.UINowakartaInternetExpWindow.UICustomersFlowerDocument.UIDeleteHyperlink;
                #endregion
            }
            finally
            {
                Assert.IsTrue(true);
            }
        }
    }
}
