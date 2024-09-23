using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace PlaywrightDemoProject.Akash
{
    [TestFixture]
    public class SimpleTests
    {
        private ExtentReports _extent;
        private ExtentTest _test;
        private string reportPath;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            try
            {
                // Define the path for the report
                reportPath = Path.Combine(Directory.GetCurrentDirectory(), "ExtentReport.html");

                // Initialize Extent Reports and attach the HTML reporter
                var htmlReporter = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);

                Console.WriteLine("Extent Reports initialized and attached. Report path: " + reportPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during Extent Reports setup: " + e.Message);
            }
        }

        [SetUp]
        public void StartTest()
        {
            try
            {
                // Create a test instance for each test method
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
                Console.WriteLine("Test started: " + TestContext.CurrentContext.Test.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during test setup: " + e.Message);
            }
        }

        [Test]
        public void TestEquality()
        {
            try
            {
                // Perform a simple test where 2 == 2
                _test.Log(Status.Info, "Starting the test for 2 == 2");
                Assert.Equals(2, 2);
                _test.Log(Status.Pass, "Test passed: 2 is equal to 2");
            }
            catch (Exception e)
            {
                _test.Log(Status.Fail, "Test failed: " + e.Message);
            }
        }

        [TearDown]
        public void LogResult()
        {
            try
            {
                // Log the result in Extent Report
                var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
                if (testStatus == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    _test.Log(Status.Pass, "Test passed");
                }
                else if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    _test.Log(Status.Fail, "Test failed: " + TestContext.CurrentContext.Result.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during result logging: " + e.Message);
            }
        }

        [OneTimeTearDown]
        public void TearDownReporting()
        {
            try
            {
                // Generate the report when all tests are done
                _extent.Flush();
                Console.WriteLine("Report generated successfully at: " + reportPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during report generation: " + e.Message);
            }
        }
    }
}
