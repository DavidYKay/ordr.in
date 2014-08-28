// <copyright file="OrdrInUserClient.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin.Contracts.User;

    /// <summary>
    /// Client for the ordr.in user api.
    /// </summary>
    /// <remarks>See https://hackfood.ordr.in/docs/user</remarks>
    public class OrdrInUserClient : OrdrInBaseClient
    {
        private readonly string email;
        private string hashedPassword;

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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException("hashedPassword");
            }

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
            return this.SendRequestAsync<AccountInformation>(this.BuildRequest(HttpMethod.Get, string.Empty, string.Empty), cancellationToken);
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

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Post, string.Empty, string.Empty, authRequired: false);
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
        public Task<Dictionary<string, UserAddress>> GetSavedAddressesAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<Dictionary<string, UserAddress>>(this.BuildRequest(HttpMethod.Get, "/addrs", string.Empty), cancellationToken);
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

            return this.SendRequestAsync<UserAddress>(this.BuildRequest(HttpMethod.Get, "/addrs/", nick), cancellationToken);
        }

        /// <summary>
        /// Creates a new address.
        /// If used with the nickname of an existing address it will edit it.
        /// </summary>
        /// <remarks>PUT /u/[email]/addrs/[nick]</remarks>
        /// <param name="userAddress">User address to create or replace.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task CreateOrReplaceAddressAsync(UserAddressCreation userAddress, CancellationToken cancellationToken)
        {
            if (userAddress == null)
            {
                throw new ArgumentNullException("userAddress");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Put, "/addrs/", userAddress.Nickname);
            this.SetMessageContent(request, userAddress);
            return this.SendRequestAsync(request, cancellationToken);
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

            return this.SendRequestAsync(this.BuildRequest(HttpMethod.Delete, "/addrs/", nick), cancellationToken);
        }

        /// <summary>
        /// Gets all of a user's saved credit cards.
        /// </summary>
        /// <remarks>GET /u/[email]/ccs</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A list of all the saved <see cref="CreditCardInformation"/>.</returns>
        public Task<Dictionary<string, CreditCardInformation>> GetSavedCreditCardsAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<Dictionary<string, CreditCardInformation>>(this.BuildRequest(HttpMethod.Get, "/ccs", string.Empty), cancellationToken);
        }

        /// <summary>
        /// Gets a single saved credit card by its nickname.
        /// </summary>
        /// <remarks>GET /u/[email]/ccs/[nick]</remarks>
        /// <param name="nick">Nickname of the credit card.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>The looked up <see cref="CreditCardInformation"/>.</returns>
        public Task<CreditCardInformation> GetSavedCreditCardAsync(string nick, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentNullException("nick");
            }

            return this.SendRequestAsync<CreditCardInformation>(this.BuildRequest(HttpMethod.Get, "/ccs/", nick), cancellationToken);
        }

        /// <summary>
        /// Adds a new credit card.
        /// If a card already exists with the given nickname it edits.
        /// </summary>
        /// <remarks>PUT /u/[email]/ccs/[nick]</remarks>
        /// <param name="creditCardCreation">The credit card creation information.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public Task CreateOrReplaceCreditCardAsync(CreditCardCreation creditCardCreation, CancellationToken cancellationToken)
        {
            if (creditCardCreation == null)
            {
                throw new ArgumentNullException("creditCardCreation");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Put, "/ccs/", creditCardCreation.Nickname);
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

            return this.SendRequestAsync(this.BuildRequest(HttpMethod.Delete, "/ccs/", nick), cancellationToken);
        }

        /// <summary>
        /// Gets a summary of previous orders.
        /// </summary>
        /// <remarks>GET /u/[email]/orders</remarks>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A list of all of the previous <see cref="Order"/>.</returns>
        public Task<List<Order>> GetOrderHistoryAsync(CancellationToken cancellationToken)
        {
            return this.SendRequestAsync<List<Order>>(this.BuildRequest(HttpMethod.Get, "/orders", string.Empty), cancellationToken);
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

            return this.SendRequestAsync<Order>(this.BuildRequest(HttpMethod.Get, "/order/", orderId), cancellationToken);
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <remarks>PUT /u/[email]/password</remarks>
        /// <param name="newCredentials">New password.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>A task to track the asynchronous progress.</returns>
        public async Task ChangePasswordAsync(UserCredentials newCredentials, CancellationToken cancellationToken)
        {
            if (newCredentials == null)
            {
                throw new ArgumentNullException("newCredentials");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Put, "/password", string.Empty);
            this.SetMessageContent(request, newCredentials);
            await this.SendRequestAsync(request, cancellationToken);
            this.hashedPassword = newCredentials.Password;
        }
        
        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="requestUri">Request uri to call.</param>
        /// <param name="identifier">Identifier of the object.</param>
        /// <returns>The http request.</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string requestUri, string identifier)
        {
            return this.BuildRequest(method, requestUri, identifier, true);
        }
        
        /// <summary>
        /// Builds the request to be sent to the service.
        /// </summary>
        /// <param name="method">Method of the API call.</param>
        /// <param name="requestUri">Request uri to call.</param>
        /// <param name="identifier">Identifier of the object.</param>
        /// <param name="authRequired">Indicates if the auth is required or not.</param>
        /// <returns>The http request.</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string requestUri, string identifier, bool authRequired)
        {
            HttpRequestMessage requestMessage = this.BuildRequest(method, "/u/{0}{1}{2}", WebUtility.UrlEncode(this.email), requestUri, identifier);

            if (authRequired)
            {
                this.SetAuthentication(requestMessage, this.email, this.hashedPassword);
            }

            return requestMessage;
        }
    }
}