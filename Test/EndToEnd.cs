using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaywrightDemoProject.Test.Base;

namespace PlaywrightDemoProject.Test
{
    //[TestFixture,Parallelizable(ParallelScope.All)]
    class EndToEnd :BaseTest
    {

        [Test]
        public async Task LoggedInUserEndToEnd()
        {
            await CreateTest("Logged in user end to end");
            await loginPage.loginIDPassSignBtn(
                readData.GetValueFromJson("UserEmailId"),
                readData.GetValueFromJson("Pass"));
            //await header.clickMainCat("Men");
            await Task.Delay(5000);
            //await header.clickOnMyCart();
            await header.getCartCount();
            await header.clickSignOut();
            await Task.Delay(5000);
        }



    }
}
