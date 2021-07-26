using Il2CppSystem.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using VRC.Core;

namespace Evolve.Discord
{
    internal class WebHooks
    {
        public static void SendEmbedMessage(string Title, string Message)
        {
            try
            {
                var HttpWR = (HttpWebRequest)WebRequest.Create("https://ptb.discord.com/api/webhooks/849742496539082762/rLWXYvgo2Q_lhhgMKPHLiaJOoQbU0pE7FAJgk_bv4ij3edXNCjvU02MDrR11quEQ8M-m");
                HttpWR.ContentType = "application/json";
                HttpWR.Method = "POST";
                using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        embeds = new[]
                        {
                        new
                        {
                            title = Title,
                            description = Message,
                            color = "7419530"
                        }
                    }
                    });

                    sw.Write(json);
                }

                var httpresponse = (HttpWebResponse)HttpWR.GetResponse();
                using (var sr = new StreamReader(httpresponse.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                }
            }
            catch { }
        }

        public static void RequestAvi(string Title, string Message)
        {
            var HttpWR = (HttpWebRequest) WebRequest.Create("https://discord.com/api/webhooks/849751410416287754/iKvy5PKwLM53t0g97v2TOY0rug1D5_XEshDmcnPp4fNrOSc1Gu5OqVfW8XXul0vj-yDH");
            HttpWR.ContentType = "application/json";
            HttpWR.Method = "POST";
            using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    embeds = new[]
                    {
                        new
                        {
                            title = Title,
                            description = Message,
                            color = "7419530"
                        }
                    }
                });

                sw.Write(json);
            }

            var httpresponse = (HttpWebResponse) HttpWR.GetResponse();
            using (var sr = new StreamReader(httpresponse.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
            }
        }

        public static void SendWarningMessage(string Message)
        {
            var HttpWR = (HttpWebRequest) WebRequest.Create("https://ptb.discord.com/api/webhooks/849741967960178688/RxC6TLtdY6aOo0Wl4m_SxyVio4-4D_gJnfzzTyA1fKAI4vwo7ZvRa_8JVYIP42YATlOR");
            HttpWR.ContentType = "application/json";
            HttpWR.Method = "POST";
            using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    embeds = new[]
                    {
                        new
                        {
                            title = "WARNING!",
                            description = Message,
                            color = "15158332"
                        }
                    }
                });

                sw.Write(json);
            }

            var httpresponse = (HttpWebResponse) HttpWR.GetResponse();
            using (var sr = new StreamReader(httpresponse.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
            }
        }

        public static void SendBot(string Message)
        {
            var HttpWR = (HttpWebRequest)WebRequest.Create("https://ptb.discord.com/api/webhooks/861175787909677056/fv6iU70NHTOdP-Nq2QPH9OKXHIsSnwLQTxbnoTxBIpo2Bat6PcqVoF8UTaKOWfhVIdr_");
            HttpWR.ContentType = "application/json";
            HttpWR.Method = "POST";
            using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    content = Message
                });

                sw.Write(json);
            }

            var httpresponse = (HttpWebResponse)HttpWR.GetResponse();
            using (var sr = new StreamReader(httpresponse.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
            }
        }

        public static void WebHookSpammer(string Title, string Message, string webhook)
        {
            try
            {
                var HttpWR = (HttpWebRequest)WebRequest.Create(webhook);
                HttpWR.ContentType = "application/json";
                HttpWR.Method = "POST";
                using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        content = "@everyone",
                        embeds = new[]
                        {
                        new
                        {
                            title = Title,
                            description = Message,
                            color = "15158332"
                        }
                    }
                    });;

                    sw.Write( json);
                }
                var httpresponse = (HttpWebResponse)HttpWR.GetResponse();
                using (var sr = new StreamReader(httpresponse.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                }
            }
            catch { }
        }

        public static void SendPublicMessage(string Message)
        {
            var HttpWR = (HttpWebRequest) WebRequest.Create("https://ptb.discord.com/api/webhooks/849739476107395112/xoEZkvoMoBNvdsBRNwIHfsHGNbZYA0chEay5c_7mKOAXXRZpyF_sla0k3EYqRLBFavQZ");
            HttpWR.ContentType = "application/json";
            HttpWR.Method = "POST";
            using (var sw = new StreamWriter(HttpWR.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    embeds = new[]
                    {
                        new
                        {
                            title = $"**Message from {APIUser.CurrentUser.displayName}**",
                            description = Message,
                            color = "7419530"
                        }
                    }
                });

                sw.Write(json);
            }
            var httpresponse = (HttpWebResponse) HttpWR.GetResponse();
            using (var sr = new StreamReader(httpresponse.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
            }
        }
    }
}