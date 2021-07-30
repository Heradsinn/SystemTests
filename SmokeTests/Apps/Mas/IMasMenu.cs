using SmokeTests.Apps.Mas.Administration;
using SmokeTests.Apps.Mas.MoneyOut;

namespace SmokeTests.Apps.Mas
{
    public interface IMasMenu
    {
        IMasLoginPage Logoff();
        IStoredRecipientsPage MoneyOutStoredRecipients();
        IApiPage AdministrationApi();
    }
}
