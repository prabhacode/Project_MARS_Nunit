using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
using System;

namespace MarsFramework
{
    public class NUnitTest
    {
        [TestFixture]
        [Category("Listing")]
        class User : Global.Base
        {

            [Test, Order(1)]
            public void AddShareSkill()
            {
                //test report
                ExtentReport.test = ExtentReport.extent.StartTest("Add Listing");
                Profile profile = new Profile();
                ShareSkill shareSkill = new ShareSkill();
                ManageListings manageListings = new ManageListings();

                //go to shareskill page            
                profile.GoToShareSkill();

                //Adding details in share skill form
                shareSkill.EnterShareSkill();

                //validate the listing on the manage listing page
                manageListings.FindListing();

            }


            [Test, Order(2)]
            public void EditShareSkill()
            {
                //test report
                ExtentReport.test = ExtentReport.extent.StartTest("Edit Listing");
                Profile profile = new Profile();
                //go to manage listing              
                profile.GoToManageListing();

                ManageListings manageListings = new ManageListings();
                //click edit listing
                manageListings.EditListings();

                ShareSkill shareSkill = new ShareSkill();
                //Edit the Listing
                shareSkill.EditShareSkill();

                //validate the listing on the manage listing page 
                manageListings.FindListing();
            }


            [Test, Order(3)]
            public void DeleteListings()
            {
                //test report
                ExtentReport.test = ExtentReport.extent.StartTest("Delete a Listing");

                Profile profile = new Profile();
                //go to manage listing              
                profile.GoToManageListing();

                //manage listing page object
                ManageListings manageListings = new ManageListings();
                //delete listing
                manageListings.DeleteListing();
            }
        }
    }
}