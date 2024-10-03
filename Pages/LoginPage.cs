using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightDemoProject.Pages.HeaderAction;
using PlaywrightDemoProject.Test.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlaywrightDemoProject.Pages.LoginPageNS
{
    public class LoginPage : BaseTest
    {
        IPage page;

        string userName = "//input[@id='email']";
        string pass = "//fieldset[@class='fieldset login']//input[@id='pass']";
        string signInBtn =
            "//fieldset[@class='fieldset login']//span[contains(text(),'Sign In')]";
        string loginUserName = "div[class='panel header'] span[class='logged-in']";
        string contactInfo = "//div[@class='box box-information']//div[@class='box-content']/p";

        public LoginPage(IPage page)
        {
            this.page = page;
        }

        public async Task EnterUserName(string emailID)
        {
            await LogTestInfo(Status.Info, "Enter Email ID");
            await page.FillAsync(this.userName, readData.GetValueFromJson("UserEmailId"));
        }

        public async Task EnterPass(string password)
        {
            await LogTestInfo(Status.Info, "Enter Password");
            await page.FillAsync(pass, readData.GetValueFromJson("Pass"));
        }

        public async Task clickSignBtn()
        {
            await LogTestInfo(Status.Info, "Click on Sign in Button");
            await page.ClickAsync(signInBtn);
        }


        public async Task loginIDPassSignBtn(string emailID, string password)
        {
            await EnterUserName(emailID);
            await EnterPass(password);
            await clickSignBtn();
        }

        public async Task<bool> verifyLogin(string LoggedUserName)
        {
            await CreateTest("Verify Login Test");
            await LogTestInfo(Status.Info, "Navigating to Login Page");
            await loginIDPassSignBtn(readData.GetValueFromJson("UserEmailId"), readData.GetValueFromJson("Pass"));

            //await page.Locator(loginUserName).WaitForAsync(new LocatorWaitForOptions
            //{
            //    State = WaitForSelectorState.Visible
            //});


            //var userName = await page.Locator(loginUserName).InnerTextAsync();

            var contactInfotext = await page.Locator(contactInfo).InnerTextAsync();

            //System.Console.WriteLine(userName);
            System.Console.WriteLine(contactInfotext);
            await Task.Delay(5000);
            //System.Console.WriteLine(userName);
            System.Console.WriteLine(contactInfotext);

            //await LogTestInfo(Status.Info, $" contact Info {contactInfotext}");

            if (contactInfotext.Contains(LoggedUserName))
            {
                await LogTestInfo(Status.Pass, "Test login Pass");
                return true;
            }
            else
            {
                await LogTestInfo(Status.Fail, "Test login Fail");
                return false;
            }
            
            
            //Assert.That(userName, Does.Contain(LoggedUserName));
            //Assert.That(contactInfotext, Does.Contain(LoggedUserName));


        }
    }
}
