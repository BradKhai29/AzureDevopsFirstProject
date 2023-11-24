using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    public class BaseWaiting : IWaiting
    {
        #region Attributes
        protected readonly IWebDriver _webDriver;

        public BaseWaiting(IWebDriver webDriver)
        {
            _webDriver = webDriver;

            // Set default implicit wait duration for current WebDriver.
            _webDriver.Manage().Timeouts().ImplicitWait = DEFAULT_IMPLICIT_WAIT_TIMEOUTS;
        }

        /// <summary>
        ///     Total timeouts to find an element with explicit wait.
        /// </summary>
        protected readonly TimeSpan DEFAULT_EXPLICIT_WAIT_TIMEOUTS = TimeSpan.FromSeconds(10);

        /// <summary>
        ///     Total timeouts to find an element with implicit wait.
        /// </summary>
        protected readonly TimeSpan DEFAULT_IMPLICIT_WAIT_TIMEOUTS = TimeSpan.FromSeconds(10);
        #endregion

        #region Public Methods
        public bool ExplicitWait(TimeSpan timeouts, Func<IWebDriver, bool> waitingCondition)
        {
            SetImplicitWait(TimeSpan.FromSeconds(0));
            WebDriverWait wait = new WebDriverWait(driver: _webDriver, timeout: timeouts);

            // Add exception-types needed to be ignored when using explicit wait.
            wait.IgnoreExceptionTypes(exceptionTypes: typeof(StaleElementReferenceException));

            var waitResult = wait.Until(condition: waitingCondition);

            SetImplicitWait(timeouts: DEFAULT_IMPLICIT_WAIT_TIMEOUTS);

            return waitResult;
        }

        public bool ExplicitWait(WebDriverWait webDriverWait, Func<IWebDriver, bool> waitingCondition)
        {
            SetImplicitWait(TimeSpan.FromSeconds(0));
            var waitResult = webDriverWait.Until(condition: waitingCondition);

            SetImplicitWait(timeouts: DEFAULT_IMPLICIT_WAIT_TIMEOUTS);

            return waitResult;
        }

        public IWaiting WaitForElementsBeVisible(params By[] locators)
        {
            foreach (var locator in locators)
            {
                if (locator != null)
                {
                    var webDriverWait = new WebDriverWait(
                         driver: _webDriver,
                         timeout: DEFAULT_EXPLICIT_WAIT_TIMEOUTS);

                    webDriverWait.Until(condition: ExpectedConditions.ElementIsVisible(locator));
                }
            }

            return this;
        }

        public IWaiting WaitForElementsBeClickable(params By[] locators)
        {
            foreach (var locator in locators)
            {
                if (locator != null)
                {
                var webDriverWait = new WebDriverWait(
                     driver: _webDriver,
                     timeout: DEFAULT_EXPLICIT_WAIT_TIMEOUTS);

                webDriverWait.Until(condition: ExpectedConditions.ElementToBeClickable(locator));
                }
            }

            return this;
        }
        #endregion

        #region Private Methods
        protected void SetImplicitWait(TimeSpan timeouts)
        {
            _webDriver.Manage().Timeouts().ImplicitWait = timeouts;
        }
        #endregion
    }
}
