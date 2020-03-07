using System;
using System.Net;
using System.Net.Http;
namespace Migraine_v2.Discord_Spammer_Lib {
    public static class Client {
        public static HttpClient Create(bool usingProxy, string Proxy, string Token) {
            HttpClient result;
            if (usingProxy) {
                bool flag = Proxy == null || Proxy == "";
                if (flag)
                    throw new Exception("Proxy was null");
                HttpClientHandler handler = new HttpClientHandler {
                    Proxy = new WebProxy(Proxy),
                    UseProxy = true
                };
                result = new HttpClient(handler, true) {
                    DefaultRequestHeaders =  {
                        {
                            "Authorization",
                            Token
                        }
                    }
                };
            }
            else {
                result = new HttpClient {
                    DefaultRequestHeaders = {
                        {
                            "Authorization",
                            Token
                        }
                    }
                };
            } return result;
        }
    }
}
