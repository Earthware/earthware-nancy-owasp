namespace earthware_nancy_owasp
{
    using earthware_nancy_owasp.Configurations;

    using global::Nancy;
    using global::Nancy.Bootstrapper;

    public class PipelinesConfiguration : IApplicationStartup
    {
        private static readonly StripServerHeaders StripServerHeaders = new StripServerHeaders();

        public void Initialize(IPipelines pipelines)
        {
            pipelines.BeforeRequest += SecurityConfiguration.CheckForMaliciousRequest;

            pipelines.AfterRequest += CompressionConfiguration.CheckForCompression;

            pipelines.AfterRequest += SecurityConfiguration.AddResponseHeaders;

        }
    }
}
