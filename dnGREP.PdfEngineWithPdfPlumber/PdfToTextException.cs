using System;
using System.Runtime.Serialization;

namespace dnGREP.Engines.PdfPlumber
{
    [Serializable]
    public class PdfPlumberException : Exception
    {
        /// <summary>
        /// Creates a new PdfToTextException.
        /// </summary>
        public PdfPlumberException() : base()
        {
        }

        /// <summary>
        /// Creates a new PdfToTextException.
        /// </summary>
        public PdfPlumberException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new PdfToTextException.
        /// </summary>
        public PdfPlumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Deserializes a PdfToTextException.
        /// </summary>
        protected PdfPlumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
