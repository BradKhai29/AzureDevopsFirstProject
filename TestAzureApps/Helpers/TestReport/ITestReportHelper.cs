using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using NUnit.Framework.Interfaces;

namespace DemoSeleniumAutomationTesting.Helpers.TestReport
{
    public interface ITestReportHelper : IDisposable
    {
        public ExtentTest CreateTest(string testname);

        public void LogResult(TestStatus testStatus, string testcaseName, Media screenshot);

        public void GenerateReport();
    }
}
