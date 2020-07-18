using MarsFramework.Global;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace MarsFramework.Pages
{
    class SignUp
    {
        public SignUp()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignUp");
            firstname = GlobalDefinitions.ExcelLib.ReadData(2, "FirstName");
            lastname = GlobalDefinitions.ExcelLib.ReadData(2, "LastName");
            email = GlobalDefinitions.ExcelLib.ReadData(2, "Email");
            password = GlobalDefinitions.ExcelLib.ReadData(2, "Password");
            confirmpassword = GlobalDefinitions.ExcelLib.ReadData(2, "ConfirmPassword");

            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region  Initialize Web Elements
        private string firstname;
        private string lastname;
        private string email;
        private string password;
        private string confirmpassword;
        //Finding the Join 
        [FindsBy(How = How.XPath, Using = "//*[@id='home']/div/div/div[1]/div/button")]
        private IWebElement Join { get; set; }

        //Identify FirstName Textbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[1]/input")]
        private IWebElement FirstName { get; set; }

        //Identify LastName Textbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[2]/input")]
        private IWebElement LastName { get; set; }

        //Identify Email Textbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[3]/input")]
        private IWebElement Email { get; set; }

        //Identify Password Textbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[4]/input")]
        private IWebElement Password { get; set; }

        //Identify Confirm Password Textbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[5]/input")]
        private IWebElement ConfirmPassword { get; set; }

        //Identify Term and Conditions Checkbox
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/form/div[6]/div/div/input")]
        private IWebElement Checkbox { get; set; }

        //Identify join button
        [FindsBy(How = How.XPath, Using = "//*[@id='submit-btn']")]
        private IWebElement JoinBtn { get; set; }
        #endregion

        internal void register()
        {
            
            //Click on Join button
            Join.Click();

            //Enter FirstName
            FirstName.SendKeys(firstname);

            //Enter LastName
            LastName.SendKeys(lastname);

            //Enter Email
            Email.SendKeys(email);

            //Enter Password
            Password.SendKeys(password);

            //Enter Password again to confirm
            ConfirmPassword.SendKeys(confirmpassword);

            //Click on Checkbox
            Checkbox.Click();

            //Click on join button to Sign Up
            JoinBtn.Click();

            
        }
    }
}
