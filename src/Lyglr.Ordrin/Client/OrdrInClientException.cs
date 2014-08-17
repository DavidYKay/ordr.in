// <copyright file="OrdrInClientException.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Lyglr.Ordrin;

    /// <summary>
    /// The ordr.in client exception.
    /// </summary>
    public class OrdrInClientException : Exception
    {
        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        private OrdrInClientException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        private OrdrInClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="statusCode">Status code returned by the service during the call.</param>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        private OrdrInClientException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Status code returned by the service during the call.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(string message)
        {
            return new OrdrInClientException(message);
        }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="requestException">The <see cref="HttpRequestException"/> exception.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(Exception requestException)
        {
            return new OrdrInClientException(requestException.Message, requestException);
        }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="statusCode">Status code returned by the service during the call.</param>
        /// <param name="requestException">The <see cref="HttpRequestException"/> exception.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(HttpStatusCode statusCode, Exception requestException)
        {
            return new OrdrInClientException(statusCode, requestException.Message, requestException);
        }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="timeout">The timeout value.</param>
        /// <param name="taskCanceledException">The timeout exception.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(TimeSpan timeout, TaskCanceledException taskCanceledException)
        {
            return new OrdrInClientException(Utilities.InvariantFormat("The request timed-out after {0}", timeout), taskCanceledException);
        }
    }
}