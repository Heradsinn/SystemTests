using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.Accounting
{
    using _ = ViewJournalPage;

    [Url("Journals/Entries")]
    public class ViewJournalPage : BasePage<_>, IViewJournalPage
    {
        [Term("Journals")]
        public H2<_> JournalTitle { get; private set; }

        public Clickable<_> AddNewEntry { get; private set; }
        public KendoGrid<JournalEntryRow, _> Entries { get; private set; }

        //TODO: Add [Format] attribute
        [FindByName("FileUpload")]
        public FileInput<_> UploadFile { get; private set; }

        [Term("Import")]
        [WaitForDocumentReadyState(TriggerEvents.BeforeClick)]
        public Button<_> SubmitImport { get; private set; }
        public Clickable<_> SubmitForApproval { get; private set; }

        [FindByTitle("Approve this Journal")]
        public Clickable<_> Approve { get; private set; }

        public ViewJournalPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IViewJournalPage AddEntry(string debitAccount, string creditAccount, decimal amount, string entryType, string paymentRef, string merchantRef, string narrative)
        {
            _logger.LogInformation($"Adding an entry to the journal...");
            return AddNewEntry.Click()
                .Entries.Rows[Entries.Rows.Count - 1].DebitAccount.Set(debitAccount)
                .Entries.Rows[Entries.Rows.Count - 1].CreditAccount.Set(creditAccount)
                .Entries.Rows[Entries.Rows.Count - 1].Amount.Set(amount.ToString())
                .Entries.Rows[Entries.Rows.Count - 1].EntryType.Set(entryType)
                .Entries.Rows[Entries.Rows.Count - 1].PaymentRef.Set(paymentRef)
                .Entries.Rows[Entries.Rows.Count - 1].MerchantRef.Set(merchantRef)
                .Entries.Rows[Entries.Rows.Count - 1].Narrative.Set(narrative)
                .Entries.Rows[Entries.Rows.Count - 1].Update.Click();
        }

        public IViewJournalPage ImportJournal(string path)
        {
            _logger.LogInformation($"Importing a journal with the file {path}...");
            UploadFile.Set(path);
            SubmitImport.Focus();
            _logger.LogInformation($"SubmitButton state - IsEnabled: {Submit.IsEnabled.Value}, IsPresent: {Submit.IsPresent.Value}, IsVisible: {Submit.IsVisible.Value}, IsVisisibleInViewPort: {Submit.IsVisibleInViewPort.Value}");
            return SubmitImport.Click();
        }

        public IViewJournalPage SubmitJournal()
        {
            _logger.LogInformation($"Submitting the journal for approval...");
            return SubmitForApproval.Click();
        }

        public IViewJournalPage ApproveJournal()
        {
            _logger.LogInformation($"Approving the journal...");
            return Approve.Click();
        }

        public IViewJournalPage Authenticate(string username, string password)
        {
            _logger.LogInformation($"Navigating to and authenticating ViewJournalPage...");
            return base.Authenticate<IViewJournalPage>(username, password);
        }

        public int NumberOfEntries()
        {
            var entriesCount = Entries.Rows.Count.Value;
            return entriesCount;
        }

        public class JournalEntryRow : KendoGridRow<_>
        {
            public TextInput<_> DebitAccount { get; private set; }
            public TextInput<_> CreditAccount { get; private set; }
            public TextInput<_> Amount { get; private set; }
            public TextInput<_> EntryType { get; private set; }
            public TextInput<_> PaymentRef { get; private set; }
            public TextInput<_> MerchantRef { get; private set; }
            public TextInput<_> Narrative { get; private set; }

            [FindByCss("k-grid-update")]
            public Clickable<_> Update { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
