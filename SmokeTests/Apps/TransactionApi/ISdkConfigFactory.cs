using VitesseSDK;

namespace SmokeTests.Apps.TransactionApi
{
    /// <summary>
    /// Factory to handle creation of the Vitesse SDK Configuration object.
    /// </summary>
    public interface ISdkConfigFactory
    {
        Configuration Create(string token, string uri);
    }
}
