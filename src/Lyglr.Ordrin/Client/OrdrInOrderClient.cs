// <copyright file="OrdrInOrderClient.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Client
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin.Contracts.Order;

    /// <summary>
    /// Client for the ordr.in order api.
    /// </summary>
    /// <remarks>See https://hackfood.ordr.in/docs/order</remarks>
    public class OrdrInOrderClient : OrdrInBaseClient
    {
        private readonly string email;
        private readonly string hashedPassword;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdrInOrderClient"/> class.
        /// </summary>
        /// <remarks>Used for guest orders.</remarks>
        /// <param name="serviceBaseUrl">The service base url.</param>
        /// <param name="developerKey">The developer key.</param>
        public OrdrInOrderClient(string serviceBaseUrl, string developerKey)
            : this(serviceBaseUrl, developerKey, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdrInOrderClient"/> class.
        /// </summary>
        /// <param name="serviceBaseUrl">The service base url.</param>
        /// <param name="developerKey">The developer key.</param>
        /// <param name="email">Email of the user account.</param>
        /// <param name="hashedPassword">Hash password of the user account.</param>
        public OrdrInOrderClient(string serviceBaseUrl, string developerKey, string email, string hashedPassword)
            : base(serviceBaseUrl, developerKey)
        {
            this.email = email;
            this.hashedPassword = hashedPassword;
        }

        /// <summary>
        /// Places an order.
        /// </summary>
        /// <remarks>/o/[rid]</remarks>
        /// <param name="order">The order to placed.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The order information.</returns>
        public Task<OrderInformation> CreateOrderAsync(UserBaseOrder order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Post, order.RestaurantId);
            this.SetMessageContent(request, order);
            return this.SendRequestAsync<OrderInformation>(request, cancellationToken);
        }

        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="restaurantId">Id of the restaurant to order at.</param>
        /// <returns>The http request.</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string restaurantId)
        {
            HttpRequestMessage requestMessage = this.BuildRequest(method, "/o/{0}", restaurantId);
            this.SetAuthentication(requestMessage, this.email, this.hashedPassword);
            return requestMessage;
        }
    }
}