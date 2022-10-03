using System.Runtime.Serialization;

namespace MyMicroservice.Services
{
    [Serializable]
    internal class exeption : Exception
    {
        public exeption()
        {
        }

        public exeption(string? message) : base(message)
        {
        }

        public exeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected exeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}