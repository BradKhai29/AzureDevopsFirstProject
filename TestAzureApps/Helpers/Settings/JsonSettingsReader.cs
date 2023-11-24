using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace DemoSeleniumAutomationTesting.Helpers.Settings
{
    /// <summary>
    ///     This class support in fetching and reading data
    ///     from the json configuration file.
    /// </summary>
    public class JsonSettingsReader : IJsonSettingsReader
    {
        #region Fields
        /// <summary>
        ///    The root of json-settings file.
        /// </summary>
        private IConfigurationRoot configurationRoot;
        #endregion

        public string SettingsFilePath { get; private set; }

        public JsonSettingsReader(string jsonFileName)
        {
            var settingsFilePath = Path.Combine(DirectoryHelper.GetCurrentProjectPath(), jsonFileName);
            SettingsFilePath = settingsFilePath;

            var configurationBuilder = new ConfigurationBuilder().AddJsonFile(path: settingsFilePath);
            configurationRoot = configurationBuilder.Build();
        }

        #region Public Methods
        public TValue GetValue<TValue>(string key) where TValue : new()
        {
            IConfigurationSection section = configurationRoot.GetRequiredSection(key: key);
            var childSections = section.GetChildren();

            TValue configurationObject = new TValue();
            Type targetType = typeof(TValue);

            LoadConfiguration(childSections, configurationObject, targetType);
            return configurationObject;
        }

        public string GetStringValue(string key)
        {
            IConfigurationSection section = configurationRoot.GetRequiredSection(key: key);

            return section.Value;
        }
        #endregion

        #region Private Methods
        private void LoadConfiguration<TValue>(
            IEnumerable<IConfigurationSection> childSections,
            TValue configurationObject,
            Type targetType) where TValue : new()
        {
            var typeProperties = targetType.GetProperties();

            foreach (var property in typeProperties)
            {
                var targetSection = childSections
                    .FirstOrDefault(predicate: section => section.Key == property.Name);

                if (targetSection != null)
                {
                    property.SetValue(configurationObject, targetSection.Value);
                }
            }
        }
        #endregion
    }
}
