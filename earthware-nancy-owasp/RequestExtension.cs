namespace earthware_nancy_owasp
{
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using global::Nancy;
    using global::Nancy.Extensions;

    public static class RequestExtension
    {
        public const string Pattern = @"((\%3C)|<)[^\n]+((\%3E)|>)";

        public static bool IsRequestMalicious(this Request request)
        {
            var regEx = new Regex(Pattern, RegexOptions.IgnoreCase);

            var slugs = request.Path.Split('/');

            // protect against injected commands within each url segment.
            if (slugs.Any(slug => regEx.Matches(slug).Count > 0))
            {
                return true;
            }

            // protect against injected commands within the url query string.
            foreach (var name in request.Query)
            {
                if (regEx.Matches(request.Query[name].Value).Count > 0)
                {
                    return true;
                }
            }

            // Protect against injected commands within the form data.
            foreach (var name in request.Form)
            {
                if (request.Form[name] != null
                    && regEx.Matches(request.Form[name].Value).Count > 0)
                {
                    return true;
                }
            }

            // Protect against injected commands in the request pay load
            if (request.Body.Length > 0)
            {
                var payLoad = request.Body.AsString();
                request.Body.Seek(0, SeekOrigin.Begin);

                return regEx.Matches(payLoad).Count > 0;
            }

            return false;
        }
    }
}