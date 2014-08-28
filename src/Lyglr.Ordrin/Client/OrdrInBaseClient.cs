// <copyright file="OrdrInBaseClient.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
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
        private const string XNaamaAuthenticationKey = "X-NAAMA-AUTHENTICATION";
        private const string XNaamaAuthenticationValueFormat = "username=\"{0}\", response=\"{1}\", version=\"1\"";

        private readonly string serviceBaseUrl;
        private readonly string developerKey;

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="serviceBaseUrl">Base url of the ordr.in service.</param>
        /// <param name="developerKey">Developer Key from ordr.in.</param>
        protected OrdrInBaseClient(string serviceBaseUrl, string developerKey)
        {
            if (string.IsNullOrWhiteSpace(serviceBaseUrl))
            {
                throw new ArgumentNullException("serviceBaseUrl");
            }

            if (string.IsNullOrWhiteSpace(developerKey))
            {
                throw new ArgumentNullException("developerKey");
            }

            this.serviceBaseUrl = serviceBaseUrl;
            this.developerKey = developerKey;
        }

        /// <summary>
        /// Builds a request <see cref="Uri"/>.
        /// </summary>
        /// <param name="path">Path to append onto the service url.</param>
        /// <returns>An absolute <see cref="Uri"/>.</returns>
        private Uri BuildRequestUri(string path)
        {
            UriBuilder builder = new UriBuilder(new Uri(this.serviceBaseUrl, UriKind.Absolute));
            builder.Path = path;
            return builder.Uri;
        }

        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="requestFormat">Request uri format.</param>
        /// <param name="parameters">Parameters of the request uri.</param>
        /// <returns>The http request.</returns>
        protected HttpRequestMessage BuildRequest(HttpMethod method, string requestFormat, params object[] parameters)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (requestFormat == null)
            {
                throw new ArgumentNullException("requestFormat");
            }

            string uriPath = Utilities.InvariantFormat(requestFormat, parameters);
            Uri fullRequestUri = this.BuildRequestUri(uriPath);

            HttpRequestMessage requestMessage = new HttpRequestMessage(method, fullRequestUri);

            return requestMessage;
        }

        /// <summary>
        /// Sets authentication on a <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="email">The email required to compute the authentication hash.</param>
        /// <param name="hashedPassword">The hashed password required to compute the authentication hash.</param>
        protected void SetAuthentication(HttpRequestMessage requestMessage, string email, string hashedPassword)
        {
            // This is a valid scenario for a guest order.
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return;
            }

            // Compute the auth hash. The hash required by the ordr.in API is: "hashcode = SHA256( SHA256([password]) + [email] + [uri] )".
            // The password is already kept as a hash in this object.
            string authHash = Utilities.CalculateSHA256(hashedPassword + email + requestMessage.RequestUri.AbsolutePath);

            requestMessage.Headers.Add(XNaamaAuthenticationKey, Utilities.InvariantFormat(XNaamaAuthenticationValueFormat, email, authHash));
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

                    string stringContent = await response.Content.ReadAsStringAsync();

                    T result;
                    if (!TryParseContentResult(stringContent, statusCode, out result))
                    {
                        response.EnsureSuccessStatusCode();    
                    }

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
        /// <param name="statusCode">Status code from the service.</param>
        /// <param name="value">Returns the parsed response from the service.</param>
        /// <typeparam name="T">Type to transform the result to.</typeparam>
        /// <returns>True if the parsing succeeded.</returns>
        private static bool TryParseContentResult<T>(string content, HttpStatusCode statusCode, out T value)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                value = JsonConvert.DeserializeObject<T>(content);
                return true;
            }

            JObject jsonContent = JObject.Parse(content);

            JToken error;
            if ((jsonContent.TryGetValue("_err", out error) || jsonContent.TryGetValue("_error", out error)) && error.Value<int>() == 1)
            {
                JToken message;
                if (jsonContent.TryGetValue("msg", out message) || jsonContent.TryGetValue("_msg", out message))
                {
                    JToken text;
                    if (jsonContent.TryGetValue("text", out text))
                    {
                        throw OrdrInClientException.CreateException(statusCode, message.Value<string>(), text.Value<string>());
                    }

                    throw OrdrInClientException.CreateException(statusCode, message.Value<string>());
                }
            }

            value = default(T);
            return false;
        }
    }
}