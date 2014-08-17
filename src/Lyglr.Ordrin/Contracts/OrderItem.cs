// <copyright file="OrderItem.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents of the item in the <see cref="Order"/> tray.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or Sets the itemId.
        /// </summary>
        [JsonProperty("iid")]
        public string ItemId { get; set; }
    }
}