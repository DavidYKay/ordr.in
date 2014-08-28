// <copyright file="RestaurantDeliveryPartner.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the restaurant delivery partner.
    /// </summary>
    public class RestaurantDeliveryPartner
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}