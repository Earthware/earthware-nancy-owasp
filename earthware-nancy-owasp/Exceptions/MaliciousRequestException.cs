namespace earthware_nancy_owasp.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class MaliciousRequestException : Exception
    {
        public MaliciousRequestException()
        {
        }
        public MaliciousRequestException(string message)
            : base(message)
        {
        }

        public MaliciousRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MaliciousRequestException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
