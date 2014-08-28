// <copyright file="OrderAddress.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represent an order address.
    /// </summary>
    public class OrderAddress
    {
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <remarks>The customer's phone number.</remarks>
        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets the zip.
        /// </summary>
        [JsonProperty("zip", Required = Required.Always)]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or Sets the address.
        /// </summary>
        [JsonProperty("addr", Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the address2.
        /// </summary>
        [JsonProperty("addr2", Required = Required.AllowNull)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or Sets the city.
        /// </summary>
        [JsonProperty("city", Required = Required.Always)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets the state.
        /// </summary>
        [JsonProperty("state", Required = Required.Always)]
        public string State { get; set; } 
    }
}