using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SampleUnitTestProject_NetCore.Helpers.Screenshot;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;

namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    /// <summary>
    ///     The base implementation of <see cref="IPageObjectModel"/> interface.
    /// </summary>
    /// <remarks>
    ///     The default implicit wait duration of the WebDriver of
    ///     PageModel will be set at 1 second.
    /// </remarks>
    public abstract class BasePageObjectModel : BaseObjectModel, IPageObjectModel
    {
        #region Attributes
        private string _pageTitle;
        #endregion

        #region Properties
        public abstract string PageUrl { get; set; }

        public string Title => InternalGetTitle();

        /// <summary>
        ///     The name for screenshot image that 
        ///     used with TakeScreenshot method to auto-generate the screenshot file.
        /// </summary>
        /// <remarks>
        ///     Override this property to change the name of the screenshot file.
        /// </remarks>
        public virtual string ScreenshotName { get; } = "screenshot";
        #endregion

        #region Constructors
        /// <summary>
        ///     Create new PageModel with input <paramref name="webDriver"/>
        ///     and default Implicit Wait duration <see cref="BaseObjectModel.DEFAULT_IMPLICIT_WAIT_TIMEOUTS"/>.
        /// </summary>
        /// <param name="webDriver">
        ///     The WebDriver of this <see cref="IPageObjectModel"/>.
        /// </param>
        protected BasePageObjectModel(IWebDriver webDriver) : base(webDriver: webDriver)
        {
            _webDriver.Manage().Window.Maximize();
        }
        #endregion

        #region Public Methods
        public void GoTo()
        {
            _webDriver.Navigate().GoToUrl(url: PageUrl);
        }

        public void TakeScreenShot()
        {
            var screenshotFileName = _screenshotHelper.FormatScreenshotFileName(ScreenshotName);
            var imageBytes = InternalGetScreenshotDataBytes();

            var saveSuccess = _screenshotHelper.SaveScreenshotFileDefault(
                screenshotFileName: screenshotFileName,
                imageBytes: imageBytes);
        }

        public void TakeScreenShot(string savingPath)
        {
            TakeScreenShot(screenshotName: ScreenshotName, savingPath: savingPath);
        }

        public void TakeScreenShot(string screenshotName, string savingPath)
        {
            var screenshotFileName = _screenshotHelper.FormatScreenshotFileName(screenshotName);
            var imageBytes = InternalGetScreenshotDataBytes();

            var saveSuccess = _screenshotHelper.SaveScreenshotFile(
                screenshotFileName: screenshotFileName,
                imageBytes: imageBytes,
                savingPath: savingPath);
        }
        #endregion

        #region Private Methods
        /// <summary>
        ///     Get the title of the current Page.
        /// </summary>
        /// <returns>
        ///     The title of the page.
        /// </returns>
        private string InternalGetTitle()
        {
            if (_pageTitle == null)
            {
                _pageTitle = _webDriver.Title;
            }

            return _pageTitle;
        }

        private byte[] InternalGetScreenshotDataBytes()
        {
            Screenshot screenShot = ((ITakesScreenshot) _webDriver).GetScreenshot();
            return screenShot.AsByteArray;
        }
        #endregion

        #region Deconstructor
        ~BasePageObjectModel()
        {
            Dispose();
        }
        #endregion
    }
}