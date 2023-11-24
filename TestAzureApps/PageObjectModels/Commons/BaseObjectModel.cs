using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SampleUnitTestProject_NetCore.Helpers.Screenshot;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    public abstract class BaseObjectModel : BaseWaiting, IObjectModel
    {
        #region Attributes
        protected readonly IScreenshotHelper _screenshotHelper = ScreenshotHelper.Instance;
        protected readonly object _lockObject = new object();
        #endregion

        #region Properties
        public IWebDriver WebDriver => _webDriver;

        public IJavaScriptExecutor JavaScriptExecutor => (IJavaScriptExecutor) _webDriver;
        #endregion

        #region Constructors
        /// <summary>
        ///     Create new ObjectModel with input <paramref name="webDriver"/>
        ///     and default Implicit Wait duration <see cref="DEFAULT_IMPLICIT_WAIT_TIMEOUTS"/>.
        /// </summary>
        /// <param name="webDriver">
        ///     The WebDriver of this <see cref="IObjectModel"/>.
        /// </param>
        protected BaseObjectModel(IWebDriver webDriver) : base(webDriver)
        {
            PageFactory.InitElements(driver: webDriver, page: this);
        }
        #endregion

        #region Public methods
        public void Quit()
        {
            Dispose();
        }

        public virtual IWebElement FindElement(By by)
        {
            return _webDriver.FindElement(by);
        }

        public virtual ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _webDriver.FindElements(by);
        }

        public void Dispose()
        {
            _webDriver.Quit();
        }

        public Actions CreateActions() => new Actions(driver: _webDriver);

        public IEnumerable<IWebElement> FindAllElementsOnDemand(By locator)
        {
            int index = 1;
            while (true)
            {
                string criteria = $"{locator.Criteria}:nth-child({index})";
                index++;
                yield return _webDriver.FindElement(By.CssSelector(criteria));
            }
        }
        #endregion

        #region Deconstructor
        ~BaseObjectModel()
        {
            Dispose();
        }
        #endregion
    }
}
