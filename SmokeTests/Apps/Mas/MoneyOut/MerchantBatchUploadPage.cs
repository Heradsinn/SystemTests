using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    using _ = MerchantBatchUploadPage;

    [Url("MerchantBatchUpload")]
    public class MerchantBatchUploadPage : BasePage<_>, IMerchantBatchUploadPage
    {
        [FindByName("FileUpload")]
        public FileInput<_> UploadFile { get; private set; }

        public Button<_> Upload { get; private set; }

        public MerchantBatchUploadPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMerchantBatchesPage ImportBatch(string path)
        {
            _logger.LogInformation($"Importing batch with the file '{path}'...");
            UploadFile.Set(path);
            return base.ClickAndGetPage<IMerchantBatchesPage>(Upload);
        }

        private readonly ILogger _logger;
    }
}
