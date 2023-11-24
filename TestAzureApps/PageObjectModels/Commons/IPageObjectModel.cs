namespace DemoSeleniumAutomationTesting.PageObjectModels.Commons
{
    /// <summary>
    ///     Base interface to create PageObject following the Page Object Models pattern.
    /// </summary>
    /// <remarks>
    ///     Implement this interface to create PageObject class.
    /// </remarks>
    public interface IPageObjectModel : IObjectModel
    {
        /// <summary>
        ///     Gets or sets the BaseURL of this page.
        /// </summary>
        string PageUrl { get; set; }

        /// <summary>
        ///     Gets the title of this page.
        /// </summary>
        string Title { get; }

        /// <summary>
        ///     Visit the page by current <see cref="PageUrl"/>.
        /// </summary>
        void GoTo();

        /// <summary>
        ///     Close the current page and also quit the WebDriver
        ///     of this <see cref="IPageObjectModel"/> instance.
        /// </summary>
        void Quit();

        /// <summary>
        ///     Take the screenshot for the current page and save 
        ///     the screenshot image to the default folder (Screenshots).<br/>
        ///     Default folder (Screenshots) locates at the directory 
        ///     that contains [project_name.SLN] file of current project.
        /// </summary>
        /// <remarks>
        ///     This method will automatically generate the filename for the screenshot
        ///     with the following format.<br/>
        ///     Format : yyyyMMdd_HHmmss_(auto_generat_image_name).png
        /// </remarks>
        void TakeScreenShot();

        /// <summary>
        ///     Take the screenshot for the current page and save 
        ///     the screenshot image to the specified <paramref name="savingPath"/>.
        /// </summary>
        /// <remarks>
        ///     This method will automatically generate the filename for the screenshot
        ///     with the following format.<br/>
        ///     Format : ddMMyy_hh:mm:ss_(auto_generat_image_name).png
        /// </remarks>
        /// <param name="savingPath">
        ///     The path where the screenshot image will be stored.<br/>
        ///     Note: The <paramref name="savingPath"/> must not include file name,
        ///     just path for saving screenshot only.
        /// </param>
        void TakeScreenShot(string savingPath);

        /// <summary>
        ///     Take the screenshot for the current page and save 
        ///     the screenshot image to the specified <paramref name="savingPath"/>.
        /// </summary>
        /// <remarks>
        ///     This method will automatically generate the filename for the screenshot
        ///     with the following format.<br/>
        ///     Format : ddMMyy_hh:mm:ss_(<paramref name="screenshotName"/>).png
        /// </remarks>
        /// <param name="savingPath">
        ///     The path where the screenshot image will be stored.<br/>
        ///     Note: The <paramref name="savingPath"/> must not include file name,
        ///     just path for saving screenshot only.
        /// </param>
        /// <param name="screenshotName">
        ///     The specified name for the screenshot.
        /// </param>
        void TakeScreenShot(string savingPath, string screenshotName);
    }
}