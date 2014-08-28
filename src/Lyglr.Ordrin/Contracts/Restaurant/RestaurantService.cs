// <copyright file="RestaurantService.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a service from the restaurant.
    /// </summary>
    public class RestaurantService
    {
        /// <summary>
        /// Gets or sets the delivery time.
        /// </summary>
        [JsonProperty("time")]
        public int DeliveryTime { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount.
        /// </summary>
        [JsonProperty("mino")]
        public double MinimumAmount { get; set; }

        /// <summary>
        /// Gets or sets the can.
        /// </summary>
        [JsonProperty("can")]
        public BooleanValue Can { get; set; }
    }
}