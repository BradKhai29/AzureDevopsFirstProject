using DemoSeleniumAutomationTesting.Helpers.DateTimeFormat;
using SampleUnitTestProject_NetCore.Helpers.DateTimeFormat;
using SampleUnitTestProject_NetCore.Helpers.Screenshot;
using System.Text;

namespace DemoSeleniumAutomationTesting.Helpers.Screenshot
{
    public abstract class BaseScreenshotHelper : IScreenshotHelper
    {
        #region Fields
        public const string SCREENSHOT_FOLDER_NAME = "Screenshots";
        private static bool isScreenshotFolderExisted = false;
        private readonly DateTimeHelper dateTimeHelper = DateTimeHelper.Instance;
        #endregion

        protected BaseScreenshotHelper()
        {
        }

        /// <summary>
        ///     Create a default (Screenshots) folder at
        ///     the current project path to store the screenshot files.
        /// </summary>
        /// <remarks>
        ///     This method will also call InternalInitiallizeCurrentProjectPath() method
        ///     to init the current project path.
        /// </remarks>
        /// <returns>
        ///     Boolean: The create operation result.
        /// </returns>
        private bool CreateScreenshotFolder()
        {
            var currentProjectPath = DirectoryHelper.GetCurrentProjectPath();

            var screenshotDirectoryPath = Path.Combine(currentProjectPath, SCREENSHOT_FOLDER_NAME);

            isScreenshotFolderExisted = Directory.Exists(screenshotDirectoryPath);

            if (isScreenshotFolderExisted)
            {
                return isScreenshotFolderExisted;
            }

            try
            {
                Directory.CreateDirectory(path: screenshotDirectoryPath);
                isScreenshotFolderExisted = true;
            }
            catch
            {
                isScreenshotFolderExisted = false;
            }

            return isScreenshotFolderExisted;
        }

        public string FormatScreenshotFileName(string rawScreenshotName)
        {
            var dateTimePrefix = dateTimeHelper.ToStringWithFormat(
                inputDateTime: DateTime.Now,
                format: BuiltInFormat.ScreenshotFormat);

            StringBuilder screenShotFileNameBuilder = new StringBuilder(dateTimePrefix + "_");
            screenShotFileNameBuilder.Append($"{rawScreenshotName}.png");

            return screenShotFileNameBuilder.ToString();
        }

        public bool SaveScreenshotFile(
            string screenshotFileName,
            byte[] imageBytes,
            string savingPath)
        {
            var filePath = Path.Combine(
                path1: savingPath,
                path2: screenshotFileName);

            try
            {
                // Create new fileSteam for writing imageBytes.
                var fileStream = new FileStream(path: filePath, mode: FileMode.Create);

                fileStream.Write(buffer: imageBytes, offset: 0, count: imageBytes.Length);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveScreenshotFileDefault(string screenshotFileName, byte[] imageBytes)
        {
            // Create default screenshot folder to store screenshot file.
            var createScreenshotFolderSuccess = CreateScreenshotFolder();

            // If create screenshot folder failed.
            if (!createScreenshotFolderSuccess)
            {
                return false;
            }

            var screenshotDirectoryPath = Path.Combine(
                path1: DirectoryHelper.GetCurrentProjectPath(),
                path2: SCREENSHOT_FOLDER_NAME);

            return SaveScreenshotFile(
                screenshotFileName: screenshotFileName,
                imageBytes: imageBytes,
                savingPath: screenshotDirectoryPath);
        }
    }
}
