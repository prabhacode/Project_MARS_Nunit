using OpenQA.Selenium;
using AutoItX3Lib;
using MarsFramework.Global;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.IO;
using SeleniumExtras.PageObjects;

namespace MarsFramework.Pages
{
  
    internal class ShareSkill
    {
      
        public ShareSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
        }

        #region Initialize WebElement
        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Enter the Description in textbox
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        private IWebElement Tags { get; set; }

        //one-off service type
        [FindsBy(How = How.CssSelector, Using = "input[name='serviceType'][value='1']")]
        private IWebElement OneOff { get; set; }

        //hourly basis service type
        [FindsBy(How = How.CssSelector, Using = "input[name='serviceType'][value='0']")]
        private IWebElement hourBasis { get; set; }

        //select on-site location type
        [FindsBy(How = How.CssSelector, Using = "input[name='locationType'][value='0']")]
        private IWebElement Onsite { get; set; }

        //select online location type
        [FindsBy(How = How.CssSelector, Using = "input[name='locationType'][value='1']")]
        private IWebElement Online { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //click option for skill exchange
        [FindsBy(How = How.CssSelector, Using = "input[name='skillTrades'][value='true']")]
        private IWebElement optSkillExchange { get; set; }

        //click option for credit
        [FindsBy(How = How.CssSelector, Using = "input[name='skillTrades'][value='false']")]
        private IWebElement optCredit { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        private IWebElement SkillExchange { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //select Active
        [FindsBy(How = How.CssSelector, Using = "input[name='isActive'][value='true']")]
        private IWebElement Active { get; set; }

        //select Hidden
        [FindsBy(How = How.CssSelector, Using = "input[name='isActive'][value='false']")]
        private IWebElement Hidden { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }

        //message popup
        [FindsBy(How = How.XPath, Using = "//div[@class='ns-box-inner']")]
        private IWebElement popup {get;set;}

        //work sample
        [FindsBy(How =How.CssSelector,Using = "i[class='huge plus circle icon padding-25']")]
        private IWebElement WorkSample { get; set; }
        #endregion

        //enter share skill form
        internal void EnterShareSkill()
        {
            //enter Title
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //enter Description
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //enter Category
            new SelectElement(CategoryDropDown).SelectByText(GlobalDefinitions.ExcelLib.ReadData(2, "Category"));

            //enter SubCategory
            new SelectElement(SubCategoryDropDown).SelectByText(GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));

            //enter Tags
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tags"));
            Tags.SendKeys(Keys.Enter);

            //enter service type
            SelectServiceType();

            //select location type
            SelectLocationType();

            //select start date
            StartDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"));

            //select End date
            EndDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate"));

            //select day and time
            SelectDayNTime();

            //select skill trade
            SelectSkillTrade();

            //upload file using autoit
            fileupload();

            //select active/hidden
            SelectActive();

            //click save
            Save.Click();

           
        }

        internal void fileupload()
        {
            WorkSample.Click();
            AutoItX3 autoIt = new AutoItX3();

            autoIt.WinWait("Open", "File Upload", 1);
            autoIt.WinActivate("Open", "File Upload");
            autoIt.ControlFocus("Open", "File Upload", "[CLASS:Edit; INSTANCE:1]");

            autoIt.Send(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")) + "\\Text.txt");
            //Workaround: Copy WorkSample.txt file from root folder to Desktop
            //autoIt.Send("C:\\Users\\OEM\\Desktop\\WorkSample.txt");

            autoIt.Sleep(1000);
            autoIt.Send("{ENTER}");
        }
        internal void SelectServiceType()
        {
            //enter service type
            if (GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType") == "One-off service")
            {
                OneOff.Click();
            }
            else
            {
                hourBasis.Click();
            }
        }

        internal void SelectLocationType()
        {
            //enter service type
            if (GlobalDefinitions.ExcelLib.ReadData(2, "LocationType") == "On-site")
            {
                Onsite.Click();
            }
            else
            {
                Online.Click();
            }
        }

        internal void SelectDayNTime()
        {
            //iterate through day's rows and once found the right day, click it and enter the start and end time
            for (int i = 0; i <= 6; i++)
            {

                var day = GlobalDefinitions.driver.FindElement(By.CssSelector("input[name='Available'][index='"+i+"']"));
                var starttime = GlobalDefinitions.driver.FindElement(By.CssSelector("input[name='StartTime'][index='"+i+"']"));
                var endtime = GlobalDefinitions.driver.FindElement(By.CssSelector("input[name='EndTime'][index='"+i+"']"));
                var Label = GlobalDefinitions.driver.FindElement(By.XPath("//div[@class='fields']["+(2+i)+"]/div/div//label"));
                
                if (GlobalDefinitions.ExcelLib.ReadData(2, "Selectday") == Label.Text)
                {
                    day.Click();
                    starttime.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"));
                    endtime.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"));
                    break;
                }
                
            }
        }

        internal void SelectSkillTrade()
        {
            if(GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade")== "Skill-Exchange")
            {
                optSkillExchange.Click();
                SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
            }
            else
            {
                optCredit.Click();
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
            }
        }

        internal void SelectActive()
        {
            if((GlobalDefinitions.ExcelLib.ReadData(2, "Active") == "Hidden"))
            {
                Hidden.Click();
            }
            else
            {
                Active.Click();
            }
        }

        //edit share skill form
        internal void EditShareSkill()
        {
            EnterShareSkill();
        }
    }
}
