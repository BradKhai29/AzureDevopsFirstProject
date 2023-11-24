namespace DemoSeleniumAutomationTesting.Configurations
{
    public enum BrowserExecutionMode
    {
        Headful = 1,
        headful = 2,
        Headless = 3,
        headless = 4
    }

    public static class BrowserExecutionModeConverter
    {
        public static BrowserExecutionMode ConvertAs(string browserMode)
        {
            switch (browserMode)
            {
                case nameof(BrowserExecutionMode.Headful):
                case nameof(BrowserExecutionMode.headful):
                    return BrowserExecutionMode.Headful;

                case nameof(BrowserExecutionMode.Headless):
                case nameof(BrowserExecutionMode.headless):
                    return BrowserExecutionMode.Headless;

                default: return BrowserExecutionMode.Headful;
            }
        }

        public static BrowserExecutionMode Convert(this string browserMode)
        {
            return ConvertAs(browserMode);
        }
    }
}
