// <copyright file="OrdrInBaseClient.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Client base class for the ordr.in REST apis
    /// </summary>
    public abstract class OrdrInBaseClient : HttpClient
    {
        private const string XNaamaClientAuthenticationKey = "X-NAAMA-CLIENT-AUTHENTICATION";
        private const string XNaamaClientAuthenticationValueFormat = "id=\"{0}\", version=\"1\"";

        private readonly string serviceBaseUrl;
        private readonly string developerKey;

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="serviceBaseUrl">Base url of the ordr.in service.</param>
        /// <param name="developerKey">Developer Key from ordr.in.</param>
        protected OrdrInBaseClient(string serviceBaseUrl, string developerKey)
        {
            this.serviceBaseUrl = serviceBaseUrl;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// Builds a request <see cref="Uri"/>.
        /// </summary>
        /// <param name="path">Path to append onto the service url.</param>
        /// <returns>An absolute <see cref="Uri"/>.</returns>
        protected Uri BuildRequestUri(string path)
        {
            UriBuilder builder = new UriBuilder(new Uri(this.serviceBaseUrl, UriKind.Absolute));
            builder.Path = path;
            return builder.Uri;
        }

        /// <summary>
        /// Performs an ordr.in authenticated http request.
        /// </summary>
        /// <param name="requestMessage">Request message to be sent to the service.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        protected Task SendRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<object>(requestMessage, cancellationToken);
        }

        /// <summary>
        /// Performs an ordr.in authenticated http request.
        /// </summary>
        /// <param name="requestMessage">Request message to be sent to the service.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <typeparam name="T">Type to transform the result to.</typeparam>
        /// <returns>Returns the response from the service.</returns>
        protected async Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage");
            }

            // Required for any call to the ordr.in service.
            requestMessage.Headers.Add(XNaamaClientAuthenticationKey, Utilities.InvariantFormat(XNaamaClientAuthenticationValueFormat, this.developerKey));

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            try
            {
                using (HttpResponseMessage response = await this.SendAsync(requestMessage, cancellationToken))
                {
                    statusCode = response.StatusCode;

                    // No need to go through the whole content processing
                    // if there is nothing to process.
                    if (response.Content.Headers.ContentLength == 0)
                    {
                        return default(T);
                    }

                    JObject content;
                    using (JsonTextReader contentReader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync())))
                    {
                        content = JObject.Load(contentReader);
                    }

                    T result = ParseContentResult<T>(content);
                    response.EnsureSuccessStatusCode();
                    return result;
                }
            }
            catch (TaskCanceledException ex)
            {
                throw OrdrInClientException.CreateException(this.Timeout, ex);
            }
            catch (HttpRequestException ex)
            {
                throw OrdrInClientException.CreateException(statusCode, ex);
            }
            catch (Exception ex)
            {
                throw OrdrInClientException.CreateException(ex);
            }
        }

        /// <summary>
        /// Sets the json content of a message.
        /// </summary>
        /// <param name="request">Request to set the content on.</param>
        /// <param name="content">Content to set.</param>
        protected void SetMessageContent(HttpRequestMessage request, object content)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(content));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        /// <summary>
        /// Parses the content.
        /// </summary>
        /// <param name="content">Content return by the service.</param>
        /// <typeparam name="T">Type to transform the result to.</typeparam>
        /// <returns>Returns the parsed response from the service.</returns>
        private static T ParseContentResult<T>(JObject content)
        {
            JToken error;
            if (content.TryGetValue("_err", out error) && error.Value<int>() == 1)
            {
                JToken message;
                if (content.TryGetValue("msg", out message))
                {
                    throw OrdrInClientException.CreateException(message.Value<string>());
                }

                return default(T);
            }

            return content.ToObject<T>();
        }
    }
}