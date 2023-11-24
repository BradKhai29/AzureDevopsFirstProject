using DemoSeleniumAutomationTesting.Helpers;
using System.Collections;

namespace DemoSeleniumAutomationTesting.Configurations
{
    public class EnvironmentSettings
    {
        #region Fields
        public const string BROWSER_MODE = "browserMode";
        public const string EXECUTION_MODE = "executionMode";
        public const string WEBDRIVER_TYPE = "webDriverType";
        public const string WEBDRIVER_TYPE2 = "webDriver";
        public const string REPORT_FOLDER = "reportFolder";
        public const string SCREENSHOT_FOLDER = "screenshotFolder";
        #endregion

        #region Attributes
        private static readonly object _lockObject = new object();
        private static EnvironmentSettings _environmentSettings;
        #endregion

        #region Properties
        public BrowserExecutionMode BrowserExecutionMode { get; set; }

        public Type WebDriverType { get; set; }
        #endregion

        #region Constructors
        private EnvironmentSettings() { }
        #endregion

        #region Public Methods
        public static EnvironmentSettings Get()
        {
            lock (_lockObject)
            {
                if (_environmentSettings == null)
                {
                    _environmentSettings = new EnvironmentSettings();
                    _environmentSettings.LoadSettingsFromEnvironment();
                }
            }

            return _environmentSettings;
        }

        public static bool IsAvailable()
        {
            var isAvailable = Environment.GetEnvironmentVariable(BROWSER_MODE) != null
                            || Environment.GetEnvironmentVariable(WEBDRIVER_TYPE) != null
                            || Environment.GetEnvironmentVariable(WEBDRIVER_TYPE2) != null;

            return isAvailable;
        }
        #endregion

        #region Private Methods
        private void LoadSettingsFromEnvironment()
        {
            LoadBrowserExecutionModeSetting();
            LoadWebDriverTypeSetting();
        }

        private void LoadBrowserExecutionModeSetting()
        {
            string executionMode = Environment.GetEnvironmentVariable(BROWSER_MODE);
            if (executionMode == null)
            {
                executionMode = Environment.GetEnvironmentVariable(EXECUTION_MODE);
            }

            BrowserExecutionMode = BrowserExecutionModeConverter.ConvertAs(executionMode);
        }

        private void LoadWebDriverTypeSetting()
        {
            string webDriverType = Environment.GetEnvironmentVariable(WEBDRIVER_TYPE);
            if (webDriverType == null)
            {
                webDriverType = Environment.GetEnvironmentVariable(WEBDRIVER_TYPE2);
            }

            WebDriverType = WebDriverTypeConverter.Convert(webDriverType);
        }

        private void LoadTest()
        {
            var path = Path.Combine(DirectoryHelper.GetCurrentProjectPath(), "text.txt");
            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process))
            {
                if (item.Key.Equals(BROWSER_MODE) || item.Key.Equals(WEBDRIVER_TYPE2))
                {
                    File.AppendAllText(path, item.Value.ToString() + "\n");
                }
            }
        }
        #endregion
    }
}
