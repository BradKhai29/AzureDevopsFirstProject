using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using DemoSeleniumAutomationTesting.Configurations;
using DemoSeleniumAutomationTesting.Helpers.Settings;
using DemoSeleniumAutomationTesting.Helpers.TestReport;
using DemoSeleniumAutomationTesting.Helpers.WebDriver;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SampleUnitTestProject_NetCore.Helpers.Screenshot;

namespace DemoSeleniumAutomationTesting.TestCases.Commons
{
    /// <summary>
    ///     The base class to create any TestCase class. 
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public abstract class BaseTestCase
    {
        #region Fields
        protected readonly EnvironmentSettings _environmentSettings;
        protected readonly TestSettings _testSettings;
        #endregion

        #region Services & Helpers
        // Thread local service section.
        private readonly ThreadLocal<IWebDriver> _webDriverPool = new ThreadLocal<IWebDriver>();
        protected IWebDriver _webDriver => _webDriverPool.Value;

        // Json settings service section.
        protected readonly IJsonSettingsReader _jsonConfigurationReader;
        // Screenshot service section.
        private readonly IScreenshotHelper _screenshotHelper = ScreenshotHelper.Instance;

        // Test report services section.
        protected readonly ITestReportHelper _reportHelper;
        #endregion

        public BaseTestCase()
        {
            if (EnvironmentSettings.IsAvailable())
            {
                _environmentSettings = EnvironmentSettings.Get();
            }
            else
            {
                _jsonConfigurationReader = new JsonSettingsReader(CommonConstants.JSON_SETTINGS_FILENAME);
                _testSettings = _jsonConfigurationReader.GetValue<TestSettings>(nameof(TestSettings));
            }
            _reportHelper = TestReportHelper.Create(testcaseName: GetTestcaseName());
        }

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
        }

        /// <summary>
        ///     <b>Default implementation</b>: Set up the default configuration 
        ///     for WebDriverPool and all WebDriver instances of this testcase.
        /// </summary>
        /// <remarks>
        ///     <b>Note</b>: Override this method for custom setup; 
        ///     otherwise, the base implementation will take place.
        /// </remarks>
        [SetUp]
        public virtual void SetUp()
        {
            if (_environmentSettings != null)
            {
                _webDriverPool.Value = WebDriverFactory.Create(
                    webDriverType: _environmentSettings.WebDriverType,
                    browserExecutionMode: _environmentSettings.BrowserExecutionMode);
            }
            else
            {
                _webDriverPool.Value = WebDriverFactory.Create(
                    webDriverType: _testSettings.WebDriverType,
                    browserExecutionMode: _testSettings.BrowserExecutionMode.Convert());
            }

            _reportHelper.CreateTest(testname: TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        ///     <b>Default implementation</b>: Tear down the current test and 
        ///     dispose all <see cref="_webDriver"/> that used for testing.
        /// </summary>
        /// <remarks>
        ///     <b>Note</b>: Override this method for custom tear down; 
        ///     otherwise, the base implementation will take place.
        /// </remarks>
        [TearDown]
        public virtual void TearDown()
        {
            TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            string testcaseName = TestContext.CurrentContext.Test.Name;
            Media screenshot = GetScreenshotMedia((WebDriver) _webDriver);

            //_reportHelper.LogResult(testStatus, testcaseName, screenshot);
            var webDriver = _webDriver;
            if(webDriver != null) 
            {
                webDriver.Quit();
            }
            Thread.Sleep(500);
        }

        /// <summary>
        ///     Clean all related components of the testcase.
        /// </summary>
        [OneTimeTearDown]
        protected void CleanUp()
        {
            //_reportHelper.GenerateReport();
            _webDriverPool.Dispose();
            _reportHelper.Dispose();
        }

        #region Private Methods
        private Media GetScreenshotMedia(WebDriver webDriver, string screenshotName = "screenshot")
        {
            screenshotName = _screenshotHelper.FormatScreenshotFileName(screenshotName);

            string screenShotAsBase64 = webDriver.TakeScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder
                .CreateScreenCaptureFromBase64String(
                    base64: screenShotAsBase64,
                    title: screenshotName)
                .Build();
        }

        private string GetTestcaseName()
        {
            var testcaseName = TestContext.CurrentContext.Test.ClassName.Split('.').Last();
            return testcaseName;
        }
        #endregion
    }
}