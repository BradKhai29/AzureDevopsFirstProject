namespace SampleUnitTestProject_NetCore.Helpers.Screenshot
{
    public interface IScreenshotHelper
    {
        string FormatScreenshotFileName(string rawScreenshotName);

        /// <summary>
        ///     Save the screenShot file at the default folder.
        /// </summary>
        /// <remarks>
        ///     Default folder (Screenshots) locates at the directory 
        ///     that contains [project_name.SLN] file of current project.
        /// </remarks>
        /// <param name="screenshotFileName">
        ///     The filename of the screenShot.
        /// </param>
        /// <param name="imageBytes">
        ///     The screenshot file bytes.
        /// </param>
        /// <returns>
        ///     Boolean: The file saving operation result.
        /// </returns>
        bool SaveScreenshotFileDefault(
            string screenshotFileName,
            byte[] imageBytes);

        /// <summary>
        ///     Save the screenShot file at the specifed <paramref name="savingPath"/>.
        /// </summary>
        /// <param name="screenshotFileName">
        ///     The filename of the screenShot.
        /// </param>
        /// <param name="imageBytes">
        ///     The screenshot file bytes.
        /// </param>
        /// <param name="savingPath"></param>
        /// <returns>
        ///     Boolean: The file saving operation result.
        /// </returns>
        bool SaveScreenshotFile(
            string screenshotFileName,
            byte[] imageBytes,
            string savingPath);
    }
}
