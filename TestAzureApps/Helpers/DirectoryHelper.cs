using System.Reflection;

namespace DemoSeleniumAutomationTesting.Helpers
{
    public class DirectoryHelper
    {
        private static string _currentProjectPath = null;
        private static readonly object _lockObject = new object();

        /// <summary>
        ///     Get the current project path of this assembly.
        /// </summary>
        /// <returns>
        ///     The project path which contains the [project-name].csproj file.
        /// </returns>
        public static string GetCurrentProjectPath()
        {
            if (_currentProjectPath == null)
            {
                lock (_lockObject)
                {
                    var currentExecutionDirectoryPath = Directory.GetCurrentDirectory();

                    // The currentAssembly is represented for current project.
                    var currentProjectAssembly = Assembly.GetExecutingAssembly();
                    var projectName = currentProjectAssembly.GetName().Name;

                    int projectNamePosition = currentExecutionDirectoryPath.IndexOf(value: projectName);
                    int projectPathLength = projectNamePosition + projectName.Length;

                    _currentProjectPath = currentExecutionDirectoryPath
                        .Substring(startIndex: 0, length: projectPathLength);
                }
            }

            return _currentProjectPath;
        }
    }
}
