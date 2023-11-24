using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    public interface IWaiting
    {
        /// <summary>
        ///     Explicitly wait for a specified <paramref name="timeouts"/> by the given <paramref name="waitingCondition"/>.
        /// </summary>
        /// <param name="timeouts">
        ///     The specified duration for the driver to explicitly wait.
        /// </param>
        /// <param name="waitingCondition">
        ///     The specified condition for the driver to explicitly wait.
        /// </param>
        bool ExplicitWait(TimeSpan timeouts, Func<IWebDriver, bool> waitingCondition);

        /// <summary>
        ///     Explicitly wait with <paramref name="webDriverWait"/> configuration object 
        ///     by the given <paramref name="waitingCondition"/>.
        /// </summary>
        /// <param name="webDriverWait">
        ///     The instance of <see cref="WebDriverWait"/> to configure for explicit wait.
        /// </param>
        /// <param name="waitingCondition">
        ///     The specified condition to explicitly wait for.
        /// </param>
        bool ExplicitWait(WebDriverWait webDriverWait, Func<IWebDriver, bool> waitingCondition);

        /// <summary>
        ///     Waiting for all elements that located by input <paramref name="locators"/> to be visible.
        /// </summary>
        /// <param name="locators">
        ///     The locators of element that need to wait.
        /// </param>
        IWaiting WaitForElementsBeVisible(params By[] locators);

        /// <summary>
        ///     Waiting for all elements that located by input <paramref name="locators"/> to be clickable.
        /// </summary>
        /// <param name="locators">
        ///     The locators of element that need to wait.
        /// </param>
        IWaiting WaitForElementsBeClickable(params By[] locators);
    }
}
