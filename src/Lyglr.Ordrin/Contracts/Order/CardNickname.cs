// <copyright file="CardNickname.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a credit card indentified by a nickname.
    /// </summary>
    public class CardNickname
    {
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        [JsonProperty("card_nick", Required = Required.Always)]
        public string Nickname { get; set; }
    }
}