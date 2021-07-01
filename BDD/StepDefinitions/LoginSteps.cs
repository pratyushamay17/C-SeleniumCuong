using TechTalk.SpecFlow;
using OpenQA.Selenium;
using JupiterToys.Model.Data;
using JupiterToys.Model.Pages;
using JupiterToys.Model.Components.Dialogs;
using NUnit.Framework;
using BoDi;

namespace JupiterToys.BDD.StepDefinitions
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        [Given(@"A valid user")]
        public void GivenAValidUser()
        {
            //IConfiguration config = objectContainer.Resolve<IConfiguration>();
            string username = "anything";//config.GetSection("USERNAME").Value;
            string password = "letmein"; //config.GetSection("PASSWORD").Value;

            objectContainer.RegisterInstanceAs(new LoginData(username, password), "login");
        }

        [When(@"User logs in to the application")]
        public void WhenUserLogsInToTheApplication()
        {
            LoginData loginData = objectContainer.Resolve<LoginData>("login");
            HomePage homePage = new HomePage(driver);
            var loginDialog = homePage.ClickLoginMenu();
            loginDialog.Login(loginData.Username, loginData.Password);
        }

        [Then(@"User should see the welcome message")]
        public void ThenUserShouldSeeTheWelcomeMessage()
        {
            LoginData loginData = objectContainer.Resolve<LoginData>("login");
            HomePage homePage = new HomePage(driver);
            AccountPage accountPage = homePage.ClickAccountMenu();
            Assert.AreEqual("Welcome "+ loginData.Username, accountPage.WelcomeMessage);
        }

        [Given(@"An invalid user")]
        public void GivenAnInvalidUser()
        {
            //IConfiguration config = objectContainer.Resolve<IConfiguration>();
            string username = "anything";//config.GetSection("USERNAME").Value;
            string password = "dontletmein"; //config.GetSection("PASSWORD").Value;

            objectContainer.RegisterInstanceAs(new LoginData(username, password), "login");
        }

        [Then(@"User should see the incorrect login error message")]
        public void ThenUserShouldSeeTheIncorrectLoginErrorMessage()
        {
            var loginDialog = new LoginDialog<HomePage>(driver.FindElement(By.ClassName("popup")), new HomePage(driver));
            Assert.AreEqual("Your login details are incorrect", loginDialog.ErrorMessage);
        }
    }
}
