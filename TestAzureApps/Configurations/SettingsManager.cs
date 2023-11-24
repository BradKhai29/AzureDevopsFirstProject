using DemoSeleniumAutomationTesting.Helpers.Settings;
using DemoSeleniumAutomationTesting.TestCases.Commons;

namespace DemoSeleniumAutomationTesting.Configurations
{
    public class SettingsManager
    {
        private static readonly IDictionary<Type, object> _settings = new Dictionary<Type, object>();
        private static readonly IJsonSettingsReader _jsonSettingsReader = new JsonSettingsReader(CommonConstants.JSON_SETTINGS_FILENAME);
        private static readonly object _lockObject = new object();

        public static TSettings GetSettings<TSettings>()
        {
            var settingsType = typeof(TSettings);
            lock (_lockObject)
            {
                bool settingsFound = _settings.ContainsKey(settingsType);
                if (!settingsFound)
                {
                    InitSettings(settingsType);
                }
            }

            object settings = _settings[settingsType];
            return (TSettings) settings;
        }

        private static void InitSettings(Type settingsType)
        {
            if (settingsType == typeof(EnvironmentSettings))
            {
                var environmentSettings = EnvironmentSettings.Get();
                _settings.Add(settingsType, environmentSettings);
                return;
            }

            if (settingsType == typeof(TestSettings))
            {
                var testSettings = _jsonSettingsReader.GetValue<TestSettings>(nameof(TestSettings));
                _settings.Add(settingsType, testSettings);
                return;
            }

            var settingsValue = Activator.CreateInstance(settingsType);
            _settings.Add(settingsType, settingsValue);
        }
    }
}
