using System;

namespace SmokeTests.TestData
{
    public class SmokeTestData
    {
        public string UserPassword { get; set; }
        public string MasCreator { get; set; }
        public string MasApprover { get; set; }
        public string MasUserToSetPasswordId { get; set; }
        public string MasUserToEditId { get; set; }
        public int SingleAccountPaymentToApprove { get; set; }
        public int SingleFundedPaymentToApprove { get; set; }
        public int MerchantBatchToApprove { get; set; }
        public int MerchantBatchToRequestApproval { get; set; }
        public string StoredRecipientId { get; set; }
        public string SmCreator { get; set; }
        public string SmApprover { get; set; }
        public int JournalId { get; set; }
        public int BankBatchToLockId { get; set; }
        public int BankBatchToExportId { get; set; }
        public int BankBatchToCompleteId { get; set; }
        public Guid TxToRemediateId { get; set; }
        public int DeferredGroupId { get; set; }
        public int ForexRateGroupId { get; set; }
        public string ApiToken { get; set; }
        public Guid TxToRetreive { get; set; }
        public Guid DeferredTxToRetreive { get; set; }
        public int DeferredTxToConfirmPaidId { get; set; }
        public int DeferredTxToAddLiquidityId { get; set; }
        public int GbpAccountId { get; set; }
        public int EurAccountId { get; set; }
    }
}
