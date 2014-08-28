// <copyright file="AddressNickname.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an address identified by a nickname.
    /// </summary>
    public class AddressNickname
    {
        /// <summary>
        /// Gets or sets the address nickname.
        /// </summary>
        [JsonProperty("nick", Required = Required.Always)]
        public string Nickname { get; set; }
    }
}