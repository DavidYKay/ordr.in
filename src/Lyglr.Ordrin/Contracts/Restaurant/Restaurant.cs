// <copyright file="Restaurant.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents a restaurant.
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the restaurant name.
        /// </summary>
        [JsonProperty("na")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [JsonProperty("cs_phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the delivery partner information.
        /// </summary>
        [JsonProperty("rds_info")]
        public RestaurantDeliveryPartner DeliveryPartnerInformation { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        [JsonProperty("services")]
        public Dictionary<string, RestaurantService> Service { get; set; }

        /// <summary>
        /// Gets or sets the allow tip.
        /// </summary>
        [JsonProperty("allow_tip")]
        public BooleanValue AllowTip { get; set; }

        /// <summary>
        /// Gets or sets the allow asap.
        /// </summary>
        [JsonProperty("allow_asap")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BooleanValue AllowAsap { get; set; }

        /// <summary>
        /// Gets or sets the cuisine.
        /// </summary>
        [JsonProperty("cu")]
        public List<string> Cuisine { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("addr")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the full address.
        /// </summary>
        [JsonProperty("full_addr")]
        public RestaurantAddress FullAddress { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the delivery time.
        /// </summary>
        [JsonProperty("del")]
        public int DeliveryTime { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount.
        /// </summary>
        [JsonProperty("mino")]
        public double MinimumAmount { get; set; }

        /// <summary>
        /// Gets or sets the is delivering.
        /// </summary>
        [JsonProperty("is_delivering")]
        public BooleanValue IsDelivering { get; set; }
    }
}