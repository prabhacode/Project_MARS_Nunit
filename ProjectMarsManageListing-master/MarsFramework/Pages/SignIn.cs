using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace MarsFramework.Pages
{
   public class SignIn
    {
       
        public SignIn()
        {

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignIn");
            username = GlobalDefinitions.ExcelLib.ReadData(2, "Username");
            password = GlobalDefinitions.ExcelLib.ReadData(2, "Password");

            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        private string username;
        private string password;

        #region  Initialize Web Elements 
        //Finding the Sign Link
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
        private IWebElement SignIntab { get; set; }

        // Finding the Email Field
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email { get; set; }

        //Finding the Password Field
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password { get; set; }

        //Finding the Login Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        private IWebElement LoginButton { get; set; }

        //Validating Login 
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

        #endregion

        internal void LoginSteps()
        {
         
            //Click signin
            SignIntab.Click();
            //enter email
            Email.SendKeys(username);
            //enter password
            Password.SendKeys(password);
            //click login button
            LoginButton.Click();
            GlobalDefinitions.wait(30);
            Assert.That(ShareSkillButton.Displayed);
        }
    }
}