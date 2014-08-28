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
        /// <param name="text">User visible text.</param>
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
        /// Status code returned by the service during the call.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// User visible text returned by the service.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="statusCode">The status code of the http response.</param>
        /// <param name="message">The exception message.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(HttpStatusCode statusCode, string message)
        {
            OrdrInClientException exception = new OrdrInClientException(message);
            exception.StatusCode = statusCode;
            return exception;
        }

        /// <summary>
        /// Creates a new <see cref="OrdrInClientException"/>.
        /// </summary>
        /// <param name="statusCode">The status code of the http response.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="text">User visible text.</param>
        /// <returns>A new <see cref="OrdrInClientException"/>.</returns>
        public static OrdrInClientException CreateException(HttpStatusCode statusCode, string message, string text)
        {
            OrdrInClientException exception = new OrdrInClientException(message);
            exception.StatusCode = statusCode;
            exception.Text = text;
            return exception;
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
            OrdrInClientException exception = new OrdrInClientException(requestException.Message);
            exception.StatusCode = statusCode;
            return exception;
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