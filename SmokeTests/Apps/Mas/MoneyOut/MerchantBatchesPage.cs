using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    using _ = MerchantBatchesPage;

    [Url("MerchantBatches/All")]
    public class MerchantBatchesPage : BasePage<_>, IMerchantBatchesPage
    {
        [FindById("create")]
        public Button<_> UploadBatchButton { get; private set; }

        public KendoGrid<MerchantBatchesRow, _> Entries { get; private set; }

        public MerchantBatchesPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMerchantBatchesPage Authenticate(string username, string password)
        {
            return base.Authenticate<IMerchantBatchesPage>(username, password);
        }

        public int NumberOfEntries()
        {
            return Entries.Rows.Count;
        }

        public IMerchantBatchUploadPage UploadBatch()
        {
            _logger.LogInformation("Navigating to batch upload page...");
            return base.ClickAndGetPage<IMerchantBatchUploadPage>(UploadBatchButton);
        }

        public class MerchantBatchesRow : KendoGridRow<_>
        {
            public Text<_> BatchId { get; private set; }
            public Text<_> Filename { get; private set; }
            public Text<_> Status { get; private set; }
            public Text<_> HasDeferred { get; private set; }
            public DateTime<_> CreatedOn { get; private set; }
            public DateTime<_> UpdatedOn { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
