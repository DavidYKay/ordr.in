// <copyright file="OrderInformation.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the order information.
    /// </summary>
    public class OrderInformation
    {
        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        /// <remarks>Ordr.in's unique reference string for this order. 
        /// Only exists if the order was successful.</remarks>
        [JsonProperty("refnum")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the restaurant order id.
        /// </summary>
        /// <remarks>(Optional) Ordr.in delivery partner's reference number for this order.</remarks>
        [JsonProperty("cs_order_id")]
        public string RestaurantOrderId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <remarks>This is an end-user visible success message.</remarks>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the customer service.
        /// </summary>
        [JsonProperty("custserv")]
        public OrderCustomerService CustomerService { get; set; }
    }
}