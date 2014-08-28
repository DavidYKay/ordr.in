// <copyright file="UserOrderWithSavedCreditCard.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an order using a saved credit card.
    /// </summary>
    public class UserOrderWithSavedCreditCard : UserBaseOrder
    {
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <remarks>The customer's phone number.</remarks>
        [JsonProperty("phone", Required = Required.Always, Order = 5)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets the zip.
        /// </summary>
        [JsonProperty("zip", Required = Required.Always, Order = 6)]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or Sets the address.
        /// </summary>
        [JsonProperty("addr", Required = Required.Always, Order = 7)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the address2.
        /// </summary>
        [JsonProperty("addr2", Required = Required.AllowNull, Order = 8)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or Sets the city.
        /// </summary>
        [JsonProperty("city", Required = Required.Always, Order = 9)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets the state.
        /// </summary>
        [JsonProperty("state", Required = Required.Always, Order = 10)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the card nickname.
        /// </summary>
        [JsonProperty("card_nick", Required = Required.Always, Order = 11)]
        public string CardNickname { get; set; }
    }
}