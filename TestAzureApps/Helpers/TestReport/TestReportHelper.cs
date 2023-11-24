using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using DemoSeleniumAutomationTesting.Configurations;
using DemoSeleniumAutomationTesting.Helpers.DateTimeFormat;
using NUnit.Framework.Interfaces;
using SampleUnitTestProject_NetCore.Helpers.DateTimeFormat;

namespace DemoSeleniumAutomationTesting.Helpers.TestReport
{
    public sealed class TestReportHelper : ITestReportHelper
    {
        #region Fields
        public readonly string REPORT_FOLDER_NAME;
        private const string HostName = "localhost";
        private const string TestEnvironment = "Development";
        private const string Tester = "Nguyen Duong Khai";
        private const string ExecutionDate = "Execution Date";

        private readonly ExtentSparkReporter _reporter;
        private readonly ExtentReports _testReports;
        private readonly ThreadLocal<ExtentTest> _extentTestPool = new ThreadLocal<ExtentTest>();
        private ExtentTest ExtentTest
        {
            get => _extentTestPool.Value;
            set => _extentTestPool.Value = value;
        }
        #endregion

        #region Constructors
        private TestReportHelper(string testcaseName)
        {
            REPORT_FOLDER_NAME = SettingsManager.GetSettings<TestSettings>().ReportFolder;
            string reportFilePath = GetReportFilePath(testcaseName);
            // Initialize the report objects.
            _reporter = new ExtentSparkReporter(reportFilePath);
            _testReports = new ExtentReports();
            _testReports.AttachReporter(_reporter);

            // Add system information for current test report.
            _testReports.AddSystemInfo(name: nameof(HostName), value: HostName);
            _testReports.AddSystemInfo(name: nameof(TestEnvironment), value: TestEnvironment);
            _testReports.AddSystemInfo(name: nameof(Tester), value: Tester);
            _testReports.AddSystemInfo(name: ExecutionDate, value: DateTimeHelper.Instance.ToStringDefault(DateTime.Now));
        }
        #endregion

        #region Public Methods
        public static TestReportHelper Create(string testcaseName) => new TestReportHelper(testcaseName);

        public ExtentTest CreateTest(string testname)
        {
            ExtentTest = _testReports.CreateTest(testname);
            return ExtentTest;
        }

        public void LogResult(TestStatus testStatus, string testcaseName, Media screenshot)
        {
            switch (testStatus)
            {
                case TestStatus.Passed:
                    ExtentTest.Pass(screenshot);
                    ExtentTest.Log(Status.Pass, details: $"{testcaseName} : Passed.");
                    break;

                case TestStatus.Failed:
                    var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                    ExtentTest.Fail(screenshot);
                    ExtentTest.Log(Status.Fail, details: $"{testcaseName} : Failed.")
                              .Log(Status.Fail, stackTrace);
                    break;
            }
        }

        public void GenerateReport()
        {
            _testReports.Flush();
        }

        public void Dispose()
        {
            _extentTestPool.Dispose();
        }
        #endregion

        #region Private Methods
        private string GetReportFilePath(string testcaseName)
        {
            string dateTimePrefix = DateTimeHelper.Instance
                                    .ToStringWithFormat(inputDateTime: DateTime.Now, format: BuiltInFormat.ReportFormat);

            string reportFilePath = Path.Combine(
                path1: DirectoryHelper.GetCurrentProjectPath(),
                path2: REPORT_FOLDER_NAME,
                path3: $"{dateTimePrefix}_{testcaseName}_report.html");

            return reportFilePath;
        }
        #endregion
    }
}
