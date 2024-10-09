using PlaywrightDemoProject.Utilitis.InitPage;
using PlaywrightDemoProject.Utilitis;
using NUnit.Framework;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System.IO;
using System.Threading;
using PlaywrightDemoProject.Pages.LoginPageNS;
using PlaywrightDemoProject.Pages.HeaderAction;
using PlaywrightDemoProject.Utilitis.ReadJson;
using Microsoft.Playwright;
using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;


namespace PlaywrightDemoProject.Test.Base
{
    public class BaseTest : Browser
    {
        // Constants for key press and key release events
        const int KEY_DOWN = 0x0000; // Press key
        const int KEY_UP = 0x0002;   // Release key

        // Key codes for Windows and Up Arrow keys
        const int WIN_KEY = 0x5B;    // Left Windows key
        const int UP_ARROW = 0x26;   // Up Arrow key


        string screenshotFileName;
        string screenshotPath;
        string ExtendReportPath;
        static ExtentReports ExtentReports;
        static ExtentTest testLog;
        protected LoginPage loginPage;
        protected HeaderPage header;
        string thirdLevelUp;

        public ReadJson readData = new ReadJson();




        [DllImport("user32.dll")]
        static extern void keybd_event(byte key, byte scanCode, uint flags, UIntPtr extraInfo);


        [OneTimeSetUp]
        public async Task SuiteSetUp()
        {
            System.Console.WriteLine("------------Suite SetUp-------------");

            string CurrentPath = Directory.GetCurrentDirectory();

            // Move back three levels
            thirdLevelUp = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentPath).FullName).FullName).FullName;

            ExtendReportPath = Path.Combine(thirdLevelUp, "Report", $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

            System.Console.WriteLine(ExtendReportPath);
            ExtentSparkReporter htmlReporter;
            if (ExtentReports == null)
            {
                htmlReporter = new ExtentSparkReporter(ExtendReportPath);
                ExtentReports = new ExtentReports();
                ExtentReports.AttachReporter(htmlReporter);
            }


        }

        [SetUp]
        public async Task TestSetUp()
        {
            System.Console.WriteLine("------------Test SetUp-------------");
            page = await initPage();
            //page = await initCustomPage();


            loginPage = new LoginPage(page);
            header = new HeaderPage(page);
            await page.GotoAsync(readData.GetValueFromJson("LoginURL"));


            // Simulate pressing Windows key and Up Arrow key
            keybd_event((byte)WIN_KEY, 0, KEY_DOWN, UIntPtr.Zero); // Press Windows key
            keybd_event((byte)UP_ARROW, 0, KEY_DOWN, UIntPtr.Zero); // Press Up Arrow

            // Simulate releasing Windows key and Up Arrow key
            keybd_event((byte)UP_ARROW, 0, KEY_UP, UIntPtr.Zero);   // Release Up Arrow
            keybd_event((byte)WIN_KEY, 0, KEY_UP, UIntPtr.Zero);    // Release Windows key


        }
        [TearDown]
        public async Task TestTearDown()
        {

            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;

            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Capture screenshot on failure
                var screenshotPath = await CaptureScreenshot(testName);
                testLog.AddScreenCaptureFromPath(screenshotPath);
                //testLog.AddScreenCaptureFromBase64String(screenshotPath);
                //testLog.Fail("Test Failed").AddScreenCaptureFromPath(screenshotPath);
                //testLog.Fail("Test Failed").AddScreenCaptureFromBase64String(screenshotPath);
            }
            else
            {
                //testLog.Pass("Test Passed");
            }




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
            if (ExtentReports != null)
            {
                ExtentReports.Flush();
            }


            System.Console.WriteLine("------------Suite TearDown-------------");

            if (browser != null)
            {
                await browser.CloseAsync(); // Close the browser after the suite ends
            }
        }



        public static async Task CreateTest(string testName)
        {
            testLog = ExtentReports.CreateTest(testName);
        }

        public static async Task LogTestInfo(Status status, string message)
        {
            //testLog.Log(status, message);

            lock (testLog)
            {
                testLog.Log(status, message);
            }
        }


        // Method to capture screenshot

        // Screenshot directory


        //private async Task<string> CaptureScreenshot(string testName)
        //{
        //    string screenshotDir = Path.Combine(thirdLevelUp, "Screenshots");
        //    // Ensure the Screenshots folder exists
        //    if (!Directory.Exists(screenshotDir))
        //    {
        //        Directory.CreateDirectory(screenshotDir);
        //    }

        //    // Define screenshot path with test case name
        //    var screenshotPath = Path.Combine(screenshotDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg");

        //    // Capture and save screenshot
        //    var screenshotStream = await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });

        //    return screenshotPath;
        //}


        public async Task<string> CaptureScreenshot(string testName)
        {
            string screenshotDir = Path.Combine(thirdLevelUp, "Screenshots");
            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }

            // Define screenshot path with test case name
            screenshotFileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
            screenshotPath = Path.Combine(screenshotDir, screenshotFileName);

            // Capture and save screenshot
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true
            });
            //await page.ScreenshotAsync(new PageScreenshotOptions 
            //{ Path = screenshotPath 
            //});

            // Return relative path for the report
            return Path.Combine("..", "Screenshots", screenshotFileName);
        }

    }
}