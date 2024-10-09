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



        public static IBrowser customBrowser;
        public static IBrowserContext customBrowserContext;
        public static IPage customPage;



        public static async Task<IPage> initPage(String Browser = "chrome")
        {
            var playwright = await Playwright.CreateAsync();
            String b = Browser.ToLower();

            // 1. Get the dynamic Chrome path
            string chromePath = GetChromePath();

            if (b == "chromium")
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

            }
            else if (b == "chrome")
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    ExecutablePath = chromePath,// Use dynamic Chrome path
                    Headless = false, // You can set this to true if you want the browser to run headles
                    SlowMo =250

                });
                }
            else if (b == "firefox")
            {
                browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            }

            browserContext = await browser.NewContextAsync();
            page = await browserContext.NewPageAsync();

            return page;
        }

        public static async Task<IPage> initCustomPage()
        {

            // 1. Get the dynamic Chrome path
            string chromePath = GetChromePath();


            string CurrentPath = Directory.GetCurrentDirectory();

            // Move back three levels
            string thirdLevelUp = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentPath).FullName).FullName).FullName;


            // 1. Define the path to the user data directory (this is where the profile will be stored)
            string customChromeProfilePath = Path.Combine(thirdLevelUp, "CustomProfileChrome","OrgCHR");

            string mp="C:\\Users\\ADMIN\\AppData\\Local\\Google\\Chrome\\User Data\\Profile 1";

            //string userDataDir = @"C:\playwright\userDataProfile";

            // 2. Create an instance of Playwright
            var playwright = await Playwright.CreateAsync();

            // 3. Launch the browser in persistent context mode
            var browserContext = await playwright.Chromium.LaunchPersistentContextAsync(customChromeProfilePath, new BrowserTypeLaunchPersistentContextOptions
            {
                ExecutablePath = chromePath ,// Use dynamic Chrome path
                Headless = false, // You can set this to true if you want the browser to run headles

                Args = new[] {
                "--disable-blink-features=AutomationControlled",  // Disable automation detection
                "--disable-infobars",  // Disable Chrome is controlled info bar
                "--enable-javascript", // Ensure JavaScript is enabled
                "--start-maximized"    // Maximize the window
                }

            
            });

            // 4. Get the first page in the persistent context
            var page = browserContext.Pages[0];

            // 5. Navigate to a website
            await page.GotoAsync("https://example.com");

            return page;

           
            // 6. Close the context and browser
            //await browserContext.CloseAsync();


           

        }





        private static string GetChromePath()
        {
            if (OperatingSystem.IsWindows())
            {
                // Common paths for Chrome on Windows
                string[] windowsPaths = {
                @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            };

                foreach (var path in windowsPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
            }
            else if (OperatingSystem.IsMacOS())
            {
                // Common path for Chrome on macOS
                string macPath = @"/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
                if (File.Exists(macPath))
                {
                    return macPath;
                }
            }
            else if (OperatingSystem.IsLinux())
            {
                // Common path for Chrome on Linux
                string linuxPath = @"/usr/bin/google-chrome";
                if (File.Exists(linuxPath))
                {
                    return linuxPath;
                }
            }

            // If Chrome is not found
            return null;
        }



    }
}
