using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemoProject.Utilitis.InitPage
{
    public class Browser
    {
        public static IBrowser browser;
        public static IBrowserContext browserContext;
        public static IPage page;


        public static async Task<IPage> initPage(String Browser = "chrome")
        {
            var playwright =  await Playwright.CreateAsync();
            String b = Browser.ToLower();

            if (b == "chrome")
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

            }
            else if (b =="firefox")
            {
                browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            }

            browserContext = await browser.NewContextAsync();
            page = await browserContext.NewPageAsync();

            return page;

        }

    }
}
