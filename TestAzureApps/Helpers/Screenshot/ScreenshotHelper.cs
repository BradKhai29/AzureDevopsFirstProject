using DemoSeleniumAutomationTesting.Helpers.Screenshot;

namespace SampleUnitTestProject_NetCore.Helpers.Screenshot
{
    public class ScreenshotHelper : BaseScreenshotHelper
    {
        #region Properties
        /// <summary>
        ///     Create new <see cref="IScreenshotHelper"/> instance.
        /// </summary>
        public static ScreenshotHelper Instance => new ScreenshotHelper();
        #endregion

        private ScreenshotHelper() : base()
        {
        }
    }
}
