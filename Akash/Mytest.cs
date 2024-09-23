using Microsoft.Playwright;
using System;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoProject.Akash
{
    public class Mytest
    {
        IPlaywright playwright;
        IPage page;
        IBrowserContext browserContext;
        IBrowser browser;


        [OneTimeSetUp]
        public async Task SuiteSetUP()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            browserContext = await browser.NewContextAsync();


            page = await browserContext.NewPageAsync();

        }

        [Test]
        public async Task MytestCase()
        {
            //playwright = await Playwright.CreateAsync();
            //browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            // {
            //     Headless = false,
            // });
            // browserContext = await browser.NewContextAsync();


            // page = await browserContext.NewPageAsync();

            Console.WriteLine("--------------Akash---------------");
            Console.WriteLine("--------------Demo----------------");
            Console.WriteLine("-------------Project--------------");
            Console.WriteLine(Directory.GetCurrentDirectory());

            await page.GotoAsync("https://www.saucedemo.com/");
            await page.Locator("//input[@id='user-name']").FillAsync("standard_user");

            await page.Locator("//input[@id='password']").FillAsync("secret_sauce");
            await page.Locator("//input[@id='login-button']").ClickAsync();

            Console.WriteLine("----------- Login Done ------------");


            await Task.Delay(10000);


            await page.CloseAsync();
            await browserContext.CloseAsync();
            await browser.CloseAsync();
        }

    }
}
