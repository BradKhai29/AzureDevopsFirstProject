using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DemoSeleniumAutomationTesting.Configurations
{
    public static class WebDriverTypeConverter
    {
        #region Constants
        public const string Chrome = "chromedriver";
        public const string Firefox = "firefoxdriver";
        public const string Edge = "edgedriver";
        #endregion

        public static Type Convert(string webDriverType)
        {
            if (webDriverType != null)
            {
                webDriverType = webDriverType.Trim().ToLower();
            }

            switch (webDriverType)
            {
                case Chrome:
                    return typeof(ChromeDriver);

                case Firefox:
                    return typeof(FirefoxDriver);

                case Edge:
                    return typeof(EdgeDriver);

                default:
                    return typeof(ChromeDriver);
            }
        }
    }
}
