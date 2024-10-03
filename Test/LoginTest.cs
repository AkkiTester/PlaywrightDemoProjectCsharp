using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaywrightDemoProject.Test.Base;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using PlaywrightDemoProject.Pages.LoginPageNS;

namespace PlaywrightDemoProject.Test
{
    class LoginTest : BaseTest
    {
        [Test] 
        public async Task loginTestCase()
        {
            bool result = await loginPage.verifyLogin(
                readData.GetValueFromJson("UserName"));
            
            await header.clickSignOut();
            Assert.That(result, Is.True);
            //Assert.That(result, Is.False);
        }

    }
}
