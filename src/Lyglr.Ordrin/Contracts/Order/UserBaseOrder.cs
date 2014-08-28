// <copyright file="UserBaseOrder.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a user base order.
    /// </summary>
    public abstract class UserBaseOrder
    {
        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        /// <remarks>Ordr.in's unique restaurant identifier for the restaurant.</remarks>
        [JsonProperty("email", Required = Required.Always)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        /// <remarks>Ordr.in's unique restaurant identifier for the restaurant.</remarks>
        [JsonProperty("rid", Required = Required.Always)]
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets the tray.
        /// </summary>
        /// <remarks>Represents a tray of menu items in the format 
        /// '[menu item id]/[qty],[option id],...,[option id]'</remarks>
        [JsonProperty("tray", Required = Required.Always)]
        public string Tray { get; set; }

        /// <summary>
        /// Gets or sets the tip.
        /// </summary>
        /// <remarks>Tip amount in dollars and cents.</remarks>
        [JsonProperty("tip", Required = Required.Always)]
        public string Tip { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <remarks>The customer's first name.</remarks>
        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <remarks>The customer's last name.</remarks>
        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <remarks>Delivery date.</remarks>
        [JsonProperty("delivery_date", Required = Required.Always)]
        public string DeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the delivery time.
        /// </summary>
        /// <remarks>Delivery time.</remarks>
        [JsonProperty("delivery_time", Required = Required.AllowNull)]
        public string DeliveryTime { get; set; }
    }
}