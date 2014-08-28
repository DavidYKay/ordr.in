// <copyright file="RestaurantAddress.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the restaurant's address.
    /// </summary>
    public class RestaurantAddress
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("addr")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        [JsonProperty("addr2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        [JsonProperty("postal_code")]
        public string ZipCode { get; set; }
    }
}