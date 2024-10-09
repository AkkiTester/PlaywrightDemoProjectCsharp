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

        string defaultUsername = "//div[@class='panel header']//span[contains(normalize-space(),'Default welcome msg!')]";
        string userDropDown = "//div[@class='panel header']//button[@type='button']";
        string signOut = "//div[@aria-hidden='false']//a[normalize-space()='Sign Out']";
        string mainCategoryList = "//nav[@class='navigation']/ul/li/a/span[not(@class)]";
        string myCart = "//a[@class='action showcart']";
        string cardCount = "//span[@class='counter-number']";
        public HeaderPage(IPage page) { 
            this.page = page;
        }

     

        public async Task clickSignOut()
        {
            try
            {
                
             await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);  // Wait for HTML to be loaded
             await page.WaitForLoadStateAsync(LoadState.Load);  // Wait for additional resources like images to load
             await page.WaitForLoadStateAsync(LoadState.NetworkIdle);  // Ensure all network requests have completed

             await page.Locator(userDropDown).ClickAsync();
             await page.Locator(signOut).ClickAsync();
             await LogTestInfo(Status.Info, "Sign Out");

            }
            catch (Exception ex)
            {
                await LogTestInfo(Status.Fail, "Faild to Sign-Out");
            }
        }

        public async Task clickMainCat(string catName)
        {
            try
            {
                page.Locator($"//span[normalize-space()='{catName}']").ClickAsync();
                await LogTestInfo(Status.Info, $"Click on {catName} cat");
                //page.Locator("").ClickAsync();
            }
            catch (Exception ex)
            {
                await LogTestInfo(Status.Fail, "Faild to click on Man cat");
            }
        }

        public async Task clickOnMyCart()

        {
            try
            {
                page.Locator(".minicart-wrapper").ClickAsync();
                await LogTestInfo(Status.Info, "Click on mycart");
            }
            catch (Exception ex)
            {
                await LogTestInfo(Status.Fail, "Faild to click on my cart");
            }

        }

        public async Task<string> getCartCount ()
        {
            try
            {
                await page.Locator(cardCount).WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                });
                var count = await page.Locator(cardCount).InnerTextAsync();
                System.Console.WriteLine("count" + count);
                await LogTestInfo(Status.Info, $"Cart count is {count} ");
                return count;
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                await LogTestInfo(Status.Fail, "Faild to get cart count");
                return "0";
            }
        }
    }
}
