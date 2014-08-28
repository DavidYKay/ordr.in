// <copyright file="RestaurantFee.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a restaurant fee and tax.
    /// </summary>
    public class RestaurantFee : RestaurantDeliveryCheck
    {
        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        [JsonProperty("tax")]
        public double Tax { get; set; }

        /// <summary>
        /// Gets or sets the fee.
        /// </summary>
        [JsonProperty("fee")]
        public double Fee { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        [JsonProperty("discount")]
        public double Discount { get; set; }

        /// <summary>
        /// Gets or sets the meals.
        /// </summary>
        [JsonProperty("meals")]
        public List<int> Meals { get; set; }

        /// <summary>
        /// Gets or sets the provided id.
        /// </summary>
        [JsonProperty("provided_id")]
        public string ProvidedId { get; set; }
    }
}