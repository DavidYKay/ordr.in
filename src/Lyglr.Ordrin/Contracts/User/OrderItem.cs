// <copyright file="OrderItem.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents of the item in the <see cref="Order"/> tray.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the restaurant partner id.
        /// </summary>
        [JsonProperty("rest_partner_id")]
        public string RestaurantPartnerId { get; set; }

        /// <summary>
        /// Gets or sets the partner args.
        /// </summary>
        [JsonProperty("partner_args")]
        public string PartnerArgs { get; set; }

        /// <summary>
        /// Gets or sets the provided id.
        /// </summary>
        [JsonProperty("provided_id")]
        public string ProvidedId { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the menu item id.
        /// </summary>
        [JsonProperty("menu_item_id")]
        public string MenuItemId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [JsonProperty("qty")]
        public string Quantity { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}