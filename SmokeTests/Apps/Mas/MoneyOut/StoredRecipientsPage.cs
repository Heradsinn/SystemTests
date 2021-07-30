using Atata;
using Atata.Bootstrap;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    using _ = StoredRecipientsPage;

    [Url("StoredRecipient")]
    public class StoredRecipientsPage : BasePage<_>, IStoredRecipientsPage
    {
        [FindById("create")]
        public Button<_> CreateNewStoredRecipient { get; private set; }

        public KendoGrid<StoredRecipientRow, _> Entries { get; private set; }

        public StoredRecipientsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IStoredRecipientsPage Authenticate(string username, string password)
        {
            _logger.LogInformation($"Navigating to and authenticating StoredRecipientsPage");
            return base.Authenticate<IStoredRecipientsPage>(username, password);
        }

        public IStoredRecipientCreatePage NewStoredRecipient()
        {
            _logger.LogInformation($"Creating a new stored recipient...");
            return base.ClickAndGetPage<IStoredRecipientCreatePage>(CreateNewStoredRecipient);
        }

        public int NumberOfEntries()
        {
            var entriesCount = Entries.Rows.Count.Value;
            return entriesCount;
        }

        public class StoredRecipientRow : KendoGridRow<_>
        {
            public StoredRecipientActionDropdownToggle Action { get; private set; }
            public Text<_> RecipientName { get; private set; }
            public Text<_> Country { get; private set; }
            public Text<_> Currency { get; private set; }
            public Text<_> Account { get; private set; }
            public DateTime<_> UpdatedOn { get; private set; }

            public class StoredRecipientActionDropdownToggle : BSDropdownToggle<_>
            {
                [Term("View...")]
                public Link<_> View { get; private set; }

                [Term("Edit...")]
                public Link<_> Edit { get; private set; }

                [Term("Delete...")]
                public Link<_> Delete { get; private set; }

                [Term("History...")]
                public Link<_> History { get; private set; }
            }
        }

        private readonly ILogger _logger;
    }
}
