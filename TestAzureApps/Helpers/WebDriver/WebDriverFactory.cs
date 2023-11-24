using DemoSeleniumAutomationTesting.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoSeleniumAutomationTesting.Helpers.WebDriver
{
    public static class WebDriverFactory
    {
        /// <summary>
        ///     Create new WebDriver instance by input <typeparamref name="TDriver"/> type.
        /// </summary>
        /// <typeparam name="TDriver"></typeparam>
        /// <returns></returns>
        public static TDriver Create<TDriver>() where TDriver : IWebDriver, new()
        {
            return new TDriver();
        }

        /// <summary>
        ///     Create new <see cref="WebDriver"/> instance by input <typeparamref name="TDriver"/> type
        ///     with DriverOptions.
        /// </summary>
        /// <typeparam name="TDriver"></typeparam>
        /// <param name="driverOptions">
        ///     The <see cref="DriverOptions"/> object that used to instanitiate this
        ///     <see cref="WebDriver"/> instance.
        /// </param>
        /// <returns></returns>
        public static TDriver Create<TDriver>(DriverOptions driverOptions)
            where TDriver : IWebDriver, new()
        {
            return (TDriver) Activator.CreateInstance(type: typeof(TDriver), args: driverOptions)!;
        }

        public static IWebDriver Create(Type webDriverType)
        {
            if (webDriverType == null)
            {
                throw new ArgumentNullException();
            }

            if (webDriverType == typeof(IWebDriver))
            {
                throw new ArgumentException("The input webDriver type must be non-abstract type.");
            }

            return (IWebDriver) Activator.CreateInstance(webDriverType)!;
        }

        public static IWebDriver Create(Type webDriverType, DriverOptions driverOptions)
        {
            if (webDriverType == null)
            {
                throw new ArgumentNullException();
            }

            if (webDriverType == typeof(IWebDriver))
            {
                throw new ArgumentException("The input webDriver type must be non-abstract type.");
            }

            return (IWebDriver) Activator.CreateInstance(webDriverType, driverOptions)!;
        }

        public static IWebDriver Create(Type webDriverType, BrowserExecutionMode browserExecutionMode)
        {
            return Create(webDriverType.Name, browserExecutionMode);
        }

        public static IWebDriver Create(
            string webDriverType,
            BrowserExecutionMode browserExecutionMode = BrowserExecutionMode.Headful)
        {
            if (webDriverType == null || webDriverType == nameof(IWebDriver))
            {
                throw new ArgumentException("The input webDriver type must be non-abstract type.");
            }

            Type driverType = null;
            DriverOptions driverOptions = null;
            switch (webDriverType)
            {
                case nameof(ChromeDriver):
                    driverType = typeof(ChromeDriver);
                    driverOptions = DriverOptionsFactory.Instance.HeadlessChromeOptions();
                    break;

                default:
                    driverType = typeof(ChromeDriver);
                    break;
            }

            switch (browserExecutionMode)
            {
                case BrowserExecutionMode.Headful:
                    return Create(driverType);

                case BrowserExecutionMode.Headless:
                    return Create(driverType, driverOptions);

                default:
                    return Create(driverType);
            }
        }
    }
}
