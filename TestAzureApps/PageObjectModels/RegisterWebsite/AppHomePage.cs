using DemoSeleniumAutomationTesting.PageObjectModels.Commons;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TestAzureApps.PageObjectModels.RegisterWebsite
{
    public class AppHomePage : BasePageObjectModel
    {
        #region Properties
        public override string PageUrl { get; set; } = "http://www.azurefirstproject.com/";
        #endregion

        #region WebElements
        [FindsBy(How = How.CssSelector, Using = "body > footer")]
        private IWebElement Footer { get; set; }
        #endregion

        public AppHomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void VerifyHomePageShown()
        {
            Assert.True(Footer.Displayed && Footer.Enabled);
        }
    }
}
