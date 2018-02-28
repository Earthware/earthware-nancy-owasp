namespace earthware_nancy_owasp.Configurations
{
    using System;

    using global::Nancy;

    using earthware_nancy_owasp.Exceptions;

    public static class SecurityConfiguration
    {
        public static Response CheckForMaliciousRequest(NancyContext context)
        {
            if (context.Request.IsRequestMalicious())
            {
                throw new MaliciousRequestException();
            }

            return context.Response;
        }

        public static void AddResponseHeaders(NancyContext context)
        {
            context.Response.Headers.Remove("Cache-Control");

#if !DEBUG
            context.Response.Headers.Add("Cache-Control", "private");
#endif

            context.Response.Headers.Add("Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            context.Response.Headers.Add("Frame-Options", "DENY");
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            context.Response.Headers.Add("XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

#if !DEBUG
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            context.Response.Headers.Add("X-Strict-Transport-Security", "max-age=31536000; includeSubDomains");
#endif
        }
    }
}