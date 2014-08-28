// <copyright file="UserOrderWithSavedInfo.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a user order using saved card and saved address.
    /// </summary>
    public class UserOrderWithSavedInfo : UserBaseOrder
    {
        /// <summary>
        /// Gets or sets the address nickname.
        /// </summary>
        [JsonProperty("nick", Required = Required.Always)]
        public string AddressNickname { get; set; }

        /// <summary>
        /// Gets or sets the card nickname.
        /// </summary>
        [JsonProperty("card_nick", Required = Required.Always)]
        public string CardNickname { get; set; }
    }
}