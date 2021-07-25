using System.Collections.Specialized;
using System.Net;

namespace Evolve.Discord
{
    internal class Http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.UploadValues(uri, pairs);
            }
        }
    }
}