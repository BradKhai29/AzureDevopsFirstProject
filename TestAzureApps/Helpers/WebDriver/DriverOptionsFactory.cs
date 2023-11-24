using DemoSeleniumAutomationTesting.Helpers.Settings;
using OpenQA.Selenium.Chrome;

namespace DemoSeleniumAutomationTesting.Helpers.WebDriver
{
    public class DriverOptionsFactory
    {
        #region Static fields
        private static DriverOptionsFactory _instance;
        private static readonly object LockObject = new object();
        public static DriverOptionsFactory Instance => GetInstance();
        #endregion

        #region Private attributes
        private IJsonSettingsReader _jsonSettingsReader;
        private ChromeOptions _headlessChromeOptions;
        #endregion

        #region Constructors
        private DriverOptionsFactory(IJsonSettingsReader jsonSettingsReader) { }
        #endregion

        public static DriverOptionsFactory GetInstance()
        {
            if (_instance == null)
            {
                lock (LockObject)
                {
                    var appSettingsPath = Path.Combine(DirectoryHelper.GetCurrentProjectPath(), "appsettings.json");
                    var jsonSettingReader = new JsonSettingsReader(jsonFileName: appSettingsPath);

                    _instance = new DriverOptionsFactory(jsonSettingsReader: jsonSettingReader);
                }
            }

            return _instance;
        }

        public ChromeOptions HeadlessChromeOptions()
        {
            if (_headlessChromeOptions == null)
            {
                lock (LockObject)
                {
                    _headlessChromeOptions = new ChromeOptions();
                    _headlessChromeOptions.AddArguments(new string[]
                    {
                        ChromeOptionsArguments.Headless,
                        ChromeOptionsArguments.DisableGpu
                    });
                }
            }

            return _headlessChromeOptions;
        }

        public ChromeOptions NoSandBoxOptions()
        {
            var sandBoxOptions = new ChromeOptions();
            sandBoxOptions.AddArguments(new string[]
            {
                ChromeOptionsArguments.NoSandBox
            });

            return sandBoxOptions;
        }

        #region Inner Classes
        private static class ChromeOptionsArguments
        {
            public const string Headless = "--headless=new";
            public const string DisableGpu = "--disable-gpu";
            public const string NoSandBox = "--no-sandbox";
        }
        #endregion
    }
}
