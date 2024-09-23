using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightDemoProject.Akash
{
    public class GoogleSearchTests
    {
        IBrowser browser;
        IPage page;

        [SetUp]
        public async Task Setup()
        {
            // Initialize Playwright
            var playwright = await Playwright.CreateAsync();

            // Launch the browser (Chrome) with headless = false
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // Set headless to false to see the browser window
            });

            // Open a new page
            page = await browser.NewPageAsync();
        }

        [Test]
        public async Task NavigateToGoogle()
        {
            // Navigate to Google
            await page.GotoAsync("https://www.google.com");

            // Verify the title contains "Google"
            Assert.That(await page.TitleAsync(), Does.Contain("Google"));

            // Additional test steps can go here
        }

        [TearDown]
        public async Task Teardown()
        {
            // Close the browser after the test
            await browser.CloseAsync();
        }
    }
}
