namespace DemoSeleniumAutomationTesting.Helpers.Settings
{
    public interface IJsonSettingsReader
    {
        string SettingsFilePath { get; }

        /// <summary>
        ///     Extracts the value with the specified key and converts it to type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The target type to convert.
        /// </typeparam>
        /// <param name="key">
        ///     The key of the configuration section's value to convert.
        /// </param>
        /// <returns>
        ///     The converted value.
        /// </returns>
        TValue GetValue<TValue>(string key) where TValue : new();

        /// <summary>
        ///     Extracts the value with the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key of the configuration section's value to extract.
        /// </param>
        /// <returns>
        ///     The section value.
        /// </returns>
        string GetStringValue(string key);
    }
}
