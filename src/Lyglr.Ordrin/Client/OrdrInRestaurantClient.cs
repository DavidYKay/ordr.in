// <copyright file="OrdrInRestaurantClient.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin.Contracts.Restaurant;
    using Newtonsoft.Json;

    /// <summary>
    /// Client for the ordr.in restaurant api.
    /// </summary>
    /// <remarks>See https://hackfood.ordr.in/docs/restaurant</remarks>
    public class OrdrInRestaurantClient : OrdrInBaseClient
    {
        private const string DateFormat = "MM-dd+HH:mm";

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdrInRestaurantClient"/> class.
        /// </summary>
        /// <param name="serviceBaseUrl">The service base url.</param>
        /// <param name="developerKey">The developer key.</param>
        public OrdrInRestaurantClient(string serviceBaseUrl, string developerKey)
            : base(serviceBaseUrl, developerKey)
        {
        }

        /// <summary>
        /// Gets a list of all restaurants that deliver to a given address.
        /// </summary>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<List<Restaurant>> GetDeliveryListAsync(string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Get, "/dl/{0}/{1}/{2}/{3}", "ASAP", zipCode, city, address);
            return this.SendRequestAsync<List<Restaurant>>(request, cancellationToken);
        }

        /// <summary>
        /// Gets if a particular restaurant will deliver to a given address at a given time.
        /// </summary>
        /// <param name="deliveryTime">Delivery date and time.</param>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<List<Restaurant>> GetDeliveryListAsync(DateTime deliveryTime, string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Get, "/dl/{0}/{1}/{2}/{3}", deliveryTime.ToString(DateFormat), zipCode, city, address);
            return this.SendRequestAsync<List<Restaurant>>(request, cancellationToken);
        }

        /// <summary>
        /// Checks if the given restaurant can deliver ASAP and for the given address.
        /// </summary>
        /// <param name="restaurantId">Ordr.in's unique restaurant identifier for the restaurant.</param>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<RestaurantDeliveryCheck> GetDeliveryCheckAsync(string restaurantId, string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(restaurantId))
            {
                throw new ArgumentNullException("restaurantId");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Get, "/dc/{0}/{1}/{2}/{3}/{4}", restaurantId, "ASAP", zipCode, city, address);
            return this.SendRequestAsync<RestaurantDeliveryCheck>(request, cancellationToken);
        }

        /// <summary>
        /// Checks if the given restaurant can deliver for the given time and address.
        /// </summary>
        /// <param name="restaurantId">Ordr.in's unique restaurant identifier for the restaurant.</param>
        /// <param name="deliveryTime">Delivery date and time.</param>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<RestaurantDeliveryCheck> GetDeliveryCheckAsync(string restaurantId, DateTime deliveryTime, string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(restaurantId))
            {
                throw new ArgumentNullException("restaurantId");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(HttpMethod.Get, "/dc/{0}/{1}/{2}/{3}/{4}", restaurantId, deliveryTime.ToString(DateFormat), zipCode, city, address);
            return this.SendRequestAsync<RestaurantDeliveryCheck>(request, cancellationToken);
        }

        /// <summary>
        /// Calculates all fees for a given subtotal and delivery address.
        /// </summary>
        /// <param name="restaurantId">Ordr.in's unique restaurant identifier for the restaurant.</param>
        /// <param name="deliveryTime">Delivery date and time.</param>
        /// <param name="subtotal">The cost of all items in the tray in dollars and cents.</param>
        /// <param name="tip">The tip in dollars and cents.</param>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<RestaurantFee> GetRestaurantFeeAsync(string restaurantId, DateTime deliveryTime, string subtotal, string tip, string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(restaurantId))
            {
                throw new ArgumentNullException("restaurantId");
            }

            if (string.IsNullOrWhiteSpace(subtotal))
            {
                throw new ArgumentNullException("subtotal");
            }

            if (string.IsNullOrWhiteSpace(tip))
            {
                throw new ArgumentNullException("tip");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(
                HttpMethod.Get, 
                "/fee/{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                restaurantId, 
                subtotal, 
                tip,
                deliveryTime.ToString(DateFormat),
                zipCode,
                city,
                address);

            return this.SendRequestAsync<RestaurantFee>(request, cancellationToken);
        }

        /// <summary>
        /// Calculates all fees for a given subtotal and delivery address.
        /// </summary>
        /// <param name="restaurantId">Ordr.in's unique restaurant identifier for the restaurant.</param>
        /// <param name="subtotal">The cost of all items in the tray in dollars and cents.</param>
        /// <param name="tip">The tip in dollars and cents.</param>
        /// <param name="zipCode">The zip code part of the address.</param>
        /// <param name="city">Delivery location city.</param>
        /// <param name="address">Delivery location street address.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<RestaurantFee> GetRestaurantFeeAsync(string restaurantId, string subtotal, string tip, string zipCode, string city, string address, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(restaurantId))
            {
                throw new ArgumentNullException("restaurantId");
            }

            if (string.IsNullOrWhiteSpace(subtotal))
            {
                throw new ArgumentNullException("subtotal");
            }

            if (string.IsNullOrWhiteSpace(tip))
            {
                throw new ArgumentNullException("tip");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentNullException("zipCode");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException("city");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }

            HttpRequestMessage request = this.BuildRequest(
                HttpMethod.Get,
                "/fee/{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                restaurantId,
                subtotal,
                tip,
                "ASAP",
                zipCode,
                city,
                address);

            return this.SendRequestAsync<RestaurantFee>(request, cancellationToken);
        }

        /// <summary>
        /// Gets restaurant details such as the restaurant's menu.
        /// </summary>
        /// <param name="restaurantId">Ordr.in's unique restaurant identifier for the restaurant.</param>
        /// <param name="cancellationToken">Token used to cancel the asynchronous call.</param>
        /// <returns>Nothing for now.</returns>
        public Task<RestaurantDetails> GetRestaurantDetailsAsync(string restaurantId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(restaurantId))
            {
                throw new ArgumentNullException("restaurantId");
            }

            return this.SendRequestAsync<RestaurantDetails>(this.BuildRequest(HttpMethod.Get, "/rd/{0}", restaurantId), cancellationToken);
        }
    }
}