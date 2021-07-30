using Atata;
using Atata.Bootstrap;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = ApiPage;

    [Url("MerchantApiToken")]
    public class ApiPage : BasePage<_>, IApiPage
    {
        [FindById("create")]
        public Button<_> CreateTokenButton { get; private set; }

        public KendoGrid<TokenRow, _> Entries { get; private set; }

        public ApiPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IApiPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Logging into the MAS API page...");
            return base.Authenticate<IApiPage>(username, password);
        }

        public IApiCreateTokenPage Create()
        {
            return base.ClickAndGetPage<IApiCreateTokenPage>(CreateTokenButton);
        }

        public int NumberOfEntries()
        {
            var rowCount = Entries.Rows.Count.Value;
            _logger.LogInformation($"Found {rowCount} api tokens in the grid.");
            return rowCount;
        }

        public class TokenRow : KendoGridRow<_>
        {
            public ApiActionDropdownToggle Action { get; private set; }
            public Link<_> TokenName { get; private set; }
            public DateTime<_> CreatedOn { get; private set; }
            public Text<_> CreatedBy { get; private set; }
            public DateTime<_> ValidFrom { get; private set; }
            public DateTime<_> ValidTo { get; private set; }
            public Text<_> IsValid { get; private set; }
            public DateTime<_> RevokedOn { get; private set; }
            public Text<_> RevokedBy { get; private set; }

            public class ApiActionDropdownToggle : BSDropdownToggle<_>
            {
                [Term("View...")]
                public Link<_> View { get; private set; }

                [Term("Revoke")]
                public Link<_> Revoke { get; private set; }

                [Term("History...")]
                public Link<_> History { get; private set; }
            }
        }

        private readonly ILogger _logger;
    }
}
