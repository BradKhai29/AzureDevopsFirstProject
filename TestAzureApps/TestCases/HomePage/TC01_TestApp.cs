using DemoSeleniumAutomationTesting.TestCases.Commons;
using TestAzureApps.PageObjectModels.RegisterWebsite;

namespace TestAzureApps.TestCases.HomePage
{
    public class TC01_TestApp : BaseTestCase
    {
        [Test]
        public void TestApp()
        {
            var homePage = new AppHomePage(_webDriver);
            homePage.GoTo();

            homePage.VerifyHomePageShown();
        }
    }
}
