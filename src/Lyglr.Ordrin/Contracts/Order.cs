// <copyright file="Order.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents on of the user's orders.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or Sets the orderId.
        /// </summary>
        /// <remarks>Ordr.in's unique order id.</remarks>
        [JsonProperty("oid")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or Sets the restaurantId.
        /// </summary>
        /// <remarks>Ordr.in's unique restaurant id for the restaurant that delivered this order.</remarks>
        [JsonProperty("rid")]
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or Sets the restaurantName.
        /// </summary>
        /// <remarks>The name of the restaurant that delivered this order.</remarks>
        [JsonProperty("rname")]
        public string RestaurantName { get; set; }

        /// <summary>
        /// Gets or Sets the subTotal.
        /// </summary>
        /// <remarks>The formatted subtotal for this order.</remarks>
        [JsonProperty("subtotal")]
        public string SubTotal { get; set; }

        /// <summary>
        /// Gets or Sets the tip.
        /// </summary>
        /// <remarks>The formatted tip amount for this order.</remarks>
        [JsonProperty("tip")]
        public string Tip { get; set; }

        /// <summary>
        /// Gets or Sets the tax.
        /// </summary>
        /// <remarks>The formatted tax amount of this order.</remarks>
        [JsonProperty("tax")]
        public string Tax { get; set; }

        /// <summary>
        /// Gets or Sets the fee.
        /// </summary>
        /// <remarks>The formatted fee of this order.</remarks>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Gets or Sets the totalPrice.
        /// </summary>
        /// <remarks>The formatted total price of this order.</remarks>
        [JsonProperty("total")]
        public string TotalPrice { get; set; }

        /// <summary>
        /// Gets or Sets the orderTimestamp.
        /// </summary>
        /// <remarks>The time of this order. In seconds since the Unix Epoch.</remarks>
        [JsonProperty("ctime")]
        public long OrderTimestamp { get; set; }

        /// <summary>
        /// Gets or Sets the itemTray.
        /// </summary>
        /// <remarks>An array of items in this order.</remarks>
        [JsonProperty("tray")]
        public List<OrderItem> ItemTray { get; set; }
    }
}