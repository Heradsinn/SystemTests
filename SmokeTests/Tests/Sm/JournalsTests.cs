using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeTests.Apps.Sm.Accounting;
using SmokeTests.Utils;

namespace SmokeTests.Tests.Sm
{
    [TestClass]
    [TestCategoryApp(App.Sm)]
    public class JournalsTests : WebTestFixture
    {
        [TestMethod]
        public void Journals_CreateNewJournal_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;
            const string reference = "New Journal";

            // Act
            var newJournalResult = GetPage<IJournalsPage>()
                .Authenticate(username, password)
                .CreateJournal(reference)
                .GetPageContent();

            // Assert
            newJournalResult
                .Should()
                .NotContain(ErrorCode, "because creating a new journal should display that journal's page")
                .And.Contain(reference, $"because a journal created with the reference {reference} should have and display that reference");
        }

        [TestMethod]
        public void Journals_ApproveJournal_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.SmApprover;
            var password = TestRun.SmokeTestData.UserPassword;

            //TODO: explore options to setup correct data repeatedly i.e. call the db
            //int journalId = 3418; // Use a custom test value when testing the tests, choose the ID of a submitted journal. Comment this out when deploying
            var journalId = TestRun.SmokeTestData.JournalId; // This will likely throw an error as it is our already used journal ID. Comment this out and use the line above when testing the tests

            // Act
            var approvingJournalResult = GetPage<IJournalsPage>()
                .Authenticate(username, password)
                .ViewJournal(journalId)
                .ApproveJournal()
                .GetPageContent();

            // Assert
            approvingJournalResult
                .Should()
                .NotContain(ErrorCode)
                .And.Contain(journalId.ToString(), $"because viewing a journal with the id {journalId} should have that id")
                .And.Contain("Approved", "because approving a journal should put it in the 'Approved' status");
        }

        [TestMethod]
        public void Journals_ImportJournal_IsSuccessful()
        {
            // Arrange
            var username = TestRun.SmokeTestData.SmCreator;
            var password = TestRun.SmokeTestData.UserPassword;

            const string fileName = "JournalImport.csv";
            var path = TestRun.GetProjectFilePath(fileName);
            const string reference = "Import Journal";

            // Act
            var importJournalResult = GetPage<IJournalsPage>()
                .Authenticate(username, password)
                .CreateJournal(reference) //TODO: replace with existing under construction journal
                .ImportJournal(path);

            // Assert
            importJournalResult.GetPageContent()
                .Should()
                .NotContain(ErrorCode)
                .And.Contain("Under Construction", "because importing a journal should put it in the 'Under Construction' status");

            importJournalResult.NumberOfEntries()
                .Should()
                .BePositive("because importing a journal should add entries to it");
        }

        //TODO: Implement submit for approval test

    }
}
