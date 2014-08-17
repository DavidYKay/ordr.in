// <copyright file="OrdrInUserClient.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin;
    using Lyglr.Ordrin.Contracts;
    using Newtonsoft.Json;

    /// <summary>
    /// Client for the ordr.in user api.
    /// </summary>
    /// <remarks>See https://hackfood.ordr.in/docs/user</remarks>
    public class OrdrInUserClient : OrdrInBaseClient
    {
        private const string XNaamaAuthenticationKey = "X-NAAMA-AUTHENTICATION";
        private const string XNaamaAuthenticationValueFormat = "username=\"{0}\", response=\"{1}\", version=\"1\"";

        private readonly string email;
        private readonly string hashedPassword;

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="serviceBaseUrl">Base url of the ordr.in service.</param>
        /// <param name="developerKey">Developer Key from ordr.in.</param>
        /// <param name="email">Email of the user account.</param>
        /// <param name="hashedPassword">Hash password of the user account.</param>
        public OrdrInUserClient(string serviceBaseUrl, string developerKey, string email, string hashedPassword)
            : base(serviceBaseUrl, developerKey)
        {
            this.email = email;
            this.hashedPassword = hashedPassword;
        }
        
        /// <summary>
        /// Gets information about a given authenticated user.
        /// </summary>
        /// <remarks>GET /u/[email]</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The user account information.</returns>
        public Task<AccountInformation> GetAccountInformationAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<AccountInformation>(this.BuildRequest(HttpMethod.Get, string.Empty), cancellationToken);
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <remarks>POST /u/[email]</remarks>
        /// <param name="accountCreation">Information used to create the account.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The account Id.</returns>
        public async Task<string> CreateAccountAsync(AccountCreation accountCreation, CancellationToken cancellationToken)
        {
            if (accountCreation == null)
            {
                throw new ArgumentNullException("accountCreation");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Post, string.Empty, authRequired: false);
            this.SetMessageContent(request, accountCreation);
            AccountUserId accountInformation = await this.SendRequestAsync<AccountUserId>(request, cancellationToken);
            return accountInformation.UserId;
        }

        /// <summary>
        /// Gets all addresses saved to a user's account.
        /// </summary>
        /// <remarks>GET /u/[email]/addrs</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A list of the saved <see cref="UserAddress"/>.</returns>
        public Task<List<UserAddress>> GetSavedAddressesAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<List<UserAddress>>(this.BuildRequest(HttpMethod.Get, "/addrs"), cancellationToken);
        }

        /// <summary>
        /// Gets details about a single user address.
        /// </summary>
        /// <remarks>GET /u/[email]/addrs/[nick]</remarks>
        /// <param name="nick">Nickname of the address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The looked up <see cref="UserAddress"/>.</returns>
        public Task<UserAddress> GetSavedAddressAsync(string nick, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentNullException("nick");
            }

            return this.SendRequestAsync<UserAddress>(this.BuildRequest(HttpMethod.Get, Utilities.InvariantFormat("/addrs/{0}", nick)), cancellationToken);
        }

        /// <summary>
        /// Creates a new address.
        /// If used with the nickname of an existing address it will edit it.
        /// </summary>
        /// <remarks>PUT /u/[email]/addrs/[nick]</remarks>
        /// <param name="userAddress">User address to create or replace.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task CreateOrReplaceAddressAsync(UserAddress userAddress, CancellationToken cancellationToken)
        {
            if (userAddress == null)
            {
                throw new ArgumentNullException("userAddress");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Post, Utilities.InvariantFormat("/addrs/{0}", userAddress.Nickname));
            this.SetMessageContent(request, userAddress);
            return this.SendRequestAsync<string>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes a user's address.
        /// </summary>
        /// <remarks>DELETE /u/[email]/addrs/[nick]</remarks>
        /// <param name="nick">Nickname of the address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task RemoveAddressAsync(string nick, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentNullException("nick");
            }

            return this.SendRequestAsync(this.BuildRequest(HttpMethod.Delete, Utilities.InvariantFormat("/addrs/{0}", nick)), cancellationToken);
        }

        /// <summary>
        /// Gets all of a user's saved credit cards.
        /// </summary>
        /// <remarks>GET /u/[email]/ccs</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A list of all the saved <see cref="CreditCardInformation"/>.</returns>
        public Task<List<CreditCardInformation>> GetSavedCreditCardsAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<List<CreditCardInformation>>(this.BuildRequest(HttpMethod.Get, "/ccs"), cancellationToken);
        }

        /// <summary>
        /// Gets a single saved credit card by its nickname.
        /// </summary>
        /// <remarks>GET /u/[email]/ccs/[nick]</remarks>
        /// <param name="nick">Nickname of the credit card.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The looked up <see cref="CreditCardInformation"/>.</returns>
        public Task<CreditCardInformation> GetCreditCardAsync(string nick, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentNullException("nick");
            }

            return this.SendRequestAsync<CreditCardInformation>(this.BuildRequest(HttpMethod.Get, Utilities.InvariantFormat("/ccs/{0}", nick)), cancellationToken);
        }

        /// <summary>
        /// Adds a new credit card.
        /// If a card already exists with the given nickname it edits.
        /// </summary>
        /// <remarks>PUT /u/[email]/ccs/[nick]</remarks>
        /// <param name="creditCardCreation">The credit card creation information.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task CreateCreditCardAsync(CreditCardCreation creditCardCreation, CancellationToken cancellationToken)
        {
            if (creditCardCreation == null)
            {
                throw new ArgumentNullException("creditCardCreation");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Post, Utilities.InvariantFormat("/ccs/{0}", creditCardCreation.Nickname));
            this.SetMessageContent(request, creditCardCreation);
            return this.SendRequestAsync(request, cancellationToken);
        }

        /// <summary>
        /// Removes a user's saved credit card.
        /// </summary>
        /// <remarks>DELETE /u/[email]/ccs/[nick]</remarks>
        /// <param name="nick">Nickname of the credit card.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task RemoveCreditCardAsync(string nick, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentNullException("nick");
            }

            return this.SendRequestAsync(this.BuildRequest(HttpMethod.Delete, Utilities.InvariantFormat("/ccs/{0}", nick)), cancellationToken);
        }

        /// <summary>
        /// Gets a summary of previous orders.
        /// </summary>
        /// <remarks>GET /u/[email]/orders</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A list of all of the previous <see cref="Order"/>.</returns>
        public Task<List<Order>> GetOrderHistoryAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<List<Order>>(this.BuildRequest(HttpMethod.Get, "/orders"), cancellationToken);
        }

        /// <summary>
        /// Get's details about a specific order.
        /// </summary>
        /// <remarks>GET /u/[email/order/[oid]</remarks>
        /// <param name="orderId">Id of the order to look up.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The looked up <see cref="Order"/>.</returns>
        public Task<Order> GetOrderAsync(string orderId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentNullException("orderId");
            }

            return this.SendRequestAsync<Order>(this.BuildRequest(HttpMethod.Get, Utilities.InvariantFormat("/order/{0}", orderId)), cancellationToken);
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <remarks>PUT /u/[email]/password</remarks>
        /// <param name="newHashedPassword">New password.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task ChangePasswordAsync(string newHashedPassword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(newHashedPassword))
            {
                throw new ArgumentNullException("newHashedPassword");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Put, "/password");
            this.SetMessageContent(request, newHashedPassword);
            return this.SendRequestAsync(request, cancellationToken);
        }

        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="requestUri">Request uri to call.</param>
        /// <returns>The http request.</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string requestUri)
        {
            return this.BuildRequest(method, requestUri, true);
        }

        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="requestUri">Request uri to call.</param>
        /// <param name="authRequired">Indicates if the auth is required or not.</param>
        /// <returns>The http request.</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string requestUri, bool authRequired)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }

            string path = Utilities.InvariantFormat("/u/{0}{1}", WebUtility.UrlEncode(this.email), requestUri);
            Uri fullRequestUri = this.BuildRequestUri(path);

            HttpRequestMessage requestMessage = new HttpRequestMessage(method, fullRequestUri);

            if (authRequired)
            {
                string authHash = Utilities.CalculateSHA256(this.hashedPassword + this.email + path);
                requestMessage.Headers.Add(XNaamaAuthenticationKey, Utilities.InvariantFormat(XNaamaAuthenticationValueFormat, this.email, authHash));    
            }

            return requestMessage;
        }
    }
}