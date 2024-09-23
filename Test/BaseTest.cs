using PlaywrightDemoProject.Utilitis.InitPage;
using NUnit.Framework;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace PlaywrightDemoProject.Test.BaseTest
{
    public class BaseTest : Browser
    {
        String ExtendRepoertPath;
        ExtentReports ExtentReports;
        ExtentTest Test;

        [OneTimeSetUp]
        public async Task SuiteSetUp()
        {
            System.Console.WriteLine("------------Suite SetUp-------------");

            String CurrentPath = Directory.GetCurrentDirectory();

            // Move back three levels
            string thirdLevelUp = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentPath).FullName).FullName).FullName;

            ExtendRepoertPath = Path.Combine(thirdLevelUp, "Report", "CRMReport.html");

            System.Console.WriteLine(ExtendRepoertPath);

            var htmlReporter = new ExtentSparkReporter(ExtendRepoertPath);
            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(htmlReporter);
        }

        [SetUp]
        public async Task TestSetUp()
        {
            System.Console.WriteLine("------------Test SetUp-------------");
            page = await initPage();
        }

        [Test]
        public async Task TestFirst()
        {
            

            await page.GotoAsync("https://app.hubspot.com/login/");

            Test = ExtentReports.CreateTest("My test");
            Test.Log(Status.Info, "Navigating to Login Page");
            Test.Log(Status.Pass, "Passed -------");
            await Task.Delay(10000); // Simulate a delay for demo purposes
        }

        [TearDown]
        public async Task TestTearDown()
        {
            System.Console.WriteLine("------------Test TearDown-------------");

            if (page != null)
            {
                await page.CloseAsync(); // Close the page after each test
            }

            if (browserContext != null)
            {
                await browserContext.CloseAsync(); // Ensure browser context is closed after the test
            }
        }

        [OneTimeTearDown]
        public async Task SuiteTearDown()
        {
            ExtentReports.Flush();

            System.Console.WriteLine("------------Suite TearDown-------------");

            if (browser != null)
            {
                await browser.CloseAsync(); // Close the browser after the suite ends
            }
        }


        // Extend Report 
        public async Task ExtendSuiteSteup()
        {

        }
    }
}
