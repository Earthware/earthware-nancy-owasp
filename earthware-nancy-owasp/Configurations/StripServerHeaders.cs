namespace earthware_nancy_owasp.Configurations
{
    using System.Web;

    public class StripServerHeaders : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestContent += (sender, args) =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-AspNet-Version");
                context.Response.Headers.Remove("X-Powered-By");
            };
        }

        public void Dispose()
        {
        }
    }
}