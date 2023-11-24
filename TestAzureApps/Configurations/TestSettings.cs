namespace DemoSeleniumAutomationTesting.Configurations
{
    public class TestSettings
    {
        /// <summary>
        ///     Gets and sets the WebDriver type that used for this test program.
        /// </summary>
        public string WebDriverType { get; set; }

        /// <summary>
        ///     Gets and sets the Browser execution mode that used for this test program.
        /// </summary>
        public string BrowserExecutionMode { get; set; }

        /// <summary>
        ///     Gets and sets the folder name that used to store screenshot for this test program.
        /// </summary>
        public string ScreenshotFolder { get; set; }

        /// <summary>
        ///     Gets and sets the folder name that used to store html-report for this test program.
        /// </summary>>
        public string ReportFolder { get; set; }
    }
}
