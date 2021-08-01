namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class HttpClient : System.Net.Http.HttpClient
    {

        public HttpClient()
#if NETSTANDARD
            : base(new HttpClientHandler(){ SslProtocols = SslProtocols.Tls12 } )
#endif
        {
        }
    }
}
