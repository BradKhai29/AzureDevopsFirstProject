using OpenQA.Selenium;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    /// <summary>
    ///     Base interface for Page Object Models pattern class.
    /// </summary>
    public interface IObjectModel : IDisposable, ISearchContext, IActionsCreator, IWaiting
    {
        /// <summary>
        ///     Gets the WebDriver of this <see cref="IPageObjectModel"/> object.
        /// </summary>
        IWebDriver WebDriver { get; }

        /// <summary>
        ///     Gets the JavaScriptExecutor of this <see cref="IPageObjectModel"/> object.
        /// </summary>
        IJavaScriptExecutor JavaScriptExecutor { get; }

        /// <summary>
        ///     Find all elements by input <paramref name="locator"/>. 
        /// </summary>
        /// <remarks>
        ///     This method implement with yield return for better foreach loop experience.
        /// </remarks>
        /// <param name="locator"></param>
        /// <exception cref="NotFoundException"></exception>
        IEnumerable<IWebElement> FindAllElementsOnDemand(By locator);
    }
}
