using VitesseSDK;
using Environment = VitesseSDK.Environment;

namespace SmokeTests.Apps.TransactionApi
{
    public class SdkConfigFactory : ISdkConfigFactory
    {
        public Configuration Create(string token, string uri)
        {
            return new Configuration(token, new Environment(uri));
        }
    }
}
