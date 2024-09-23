using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoProject.Akash
{
    [TestFixture]
    public class OrangeHRMTests
    {
        private IBrowser browser;
        private IPage page;
        [OneTimeSetUp]
        public async Task StartSuite()
        {
            Console.WriteLine("Starting Suite");
        }


        [SetUp]
        public async Task Setup()
        {
            // Initialize Playwright
            var playwright = await Playwright.CreateAsync();

            // Launch browser (choose either Chromium, Firefox, or WebKit)
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // Set this to true if you want headless mode

            });

            // Create a new browser page
            var context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
        }

        [Test]
        public async Task OpenOrangeHRMDemoSite()
        {
            // Navigate to the OrangeHRM demo site
            await page.GotoAsync("https://opensource-demo.orangehrmlive.com/");


            // Verify that the login page is displayed
            var title = await page.TitleAsync();

            //Assert.AreEqual("OrangeHRM", title, "Page title is not as expected.");

            // Optionally, you can check if the login form is present
            //var loginButton = await page.QuerySelectorAsync("input#btnLogin");
            //Assert.IsNotNull(loginButton, "Login button is not present.");
        }

        [TearDown]
        public async Task TearDown()
        {
            // Close the browser after the test
            await browser.CloseAsync();
        }
    }
}
