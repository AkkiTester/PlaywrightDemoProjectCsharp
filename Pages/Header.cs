using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlaywrightDemoProject.Test.Base;
using AventStack.ExtentReports;

namespace PlaywrightDemoProject.Pages.HeaderAction
{
    public class HeaderPage : BaseTest
    {
        IPage page;

        string userDropDown = "//div[@class='panel header']//button[@type='button']";
        string signOut = "//div[@aria-hidden='false']//a[normalize-space()='Sign Out']";

        public HeaderPage(IPage page) { 
            this.page = page;
        }

        public async Task clickSignOut()
        {
            await page.Locator(userDropDown).ClickAsync();
            await page.Locator(signOut).ClickAsync();
            await LogTestInfo(Status.Info, "Sign Out");
        }

    }
}
