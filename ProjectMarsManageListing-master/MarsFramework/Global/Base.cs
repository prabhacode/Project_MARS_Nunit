using MarsFramework.Config;
using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using System.IO;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    
   public class Base
   {
        public static int Browser = Int32.Parse(MarsResource.Browser);
        public static string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        public static string ScreenshotPath = path + "\\" + MarsResource.ScreenShotPath;
        public static string ExcelPath = path + "\\" + MarsResource.ExcelPath;
        public string BaseUrl = "http://192.168.99.100:5000/";



        #region setup and tear down
        [SetUp]
        public void Inititalize()
        {
            //initialize browser
            InitializeBrowser(Browser);
            driver.Navigate().GoToUrl(BaseUrl);
            
            //go to sign in or signup
            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                
                loginobj.LoginSteps();
            }
            else
            {
                SignUp signUpobj = new SignUp();
                signUpobj.register();
            }

        }

        [TearDown]
        public void TearDown()
        { // End Test Report and Close the driver
            ExtentReport.AfterTest();          
        }
        #endregion

        [OneTimeSetUp]
        public void BeforeTestFixture()
        {
            ExtentReport.InitializeReport();
        }

        [OneTimeTearDown]
        public void AfterTestFixture()
        {
            //end report
            ExtentReport.EndReport();
        }


      
    
   }
}