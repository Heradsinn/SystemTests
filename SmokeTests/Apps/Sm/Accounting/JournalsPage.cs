using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.Accounting
{
    using _ = JournalsPage;

    [Url("Journals")]
    public class JournalsPage : BasePage<_>, IJournalsPage
    {
        public KendoGrid<JournalsRow, _> Journals { get; private set; }

        [FindById("Reference")]
        public TextInput<_> NewJournalReference { get; private set; }

        [FindByContent("Create Journal")]
        public Button<_> SubmitNewJournal { get; private set; }

        public JournalsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IJournalsPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating JournalsPage...");
            return base.Authenticate<IJournalsPage>(username, password);
        }

        public IViewJournalPage CreateJournal(string reference)
        {
            _logger.LogInformation($"Creating a new journal with the reference {reference}...");
            NewJournalReference.Set(reference);
            return base.ClickAndGetPage<IViewJournalPage>(SubmitNewJournal);
        }

        public IViewJournalPage ViewJournal(string reference)
        {
            _logger.LogInformation($"Viewing the journal with the reference {reference}...");
            var journalReferenceToClick = Journals.Rows[x => x.Reference.Content.Value == reference];
            return base.ClickAndGetPage<IViewJournalPage>(journalReferenceToClick.Reference);
        }

        public IViewJournalPage ViewJournal(int journalId)
        {
            _logger.LogInformation($"Viewing the journal with the ID {journalId}...");
            var journalIdToClick = Journals.Rows[x => x.JournalId.Content.Value == journalId.ToString()];
            return base.ClickAndGetPage<IViewJournalPage>(journalIdToClick.JournalId);
        }

        public class JournalsRow : KendoGridRow<_>
        {
            [FindByColumnHeader("Journal Id")]
            public Link<_> JournalId { get; private set; }

            [FindByColumnHeader("Reference")]
            public Link<_> Reference { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
