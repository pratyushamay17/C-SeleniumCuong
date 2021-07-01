using System;

using JupiterToys.Model.Pages;
using JupiterToys.Model.Components.Dialogs;
using NUnit.Framework;

namespace JupiterToys.Test
{

    public class LoginTestSuite : BaseTestSuite
    {


        [Test]
        public void TestIncorrectCredetialsMessage()
        {

            var loginDialog = open<HomePage>().ClickLoginMenu();
            loginDialog.Login("Juan", "Juan");
            Assert.AreEqual("Your login details are incorrect", loginDialog.ErrorMessage);

        }

        [Test]
        public void LoginUser()
        {

            var message = open<HomePage>()
                    .ClickLoginMenu()
                    .Login("Juan", "letmein")
                    .ClickAccountMenu()
                    .WelcomeMessage;
            Assert.AreEqual("Welcome Juan", message);

        }
    }
}
