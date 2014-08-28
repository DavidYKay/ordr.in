// <copyright file="RestaurantDetails.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents the restaurant details.
    /// </summary>
    public class RestaurantDetails
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("addr")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the allow asap.
        /// </summary>
        [JsonProperty("allow_asap")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BooleanValue AllowAsap { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [JsonProperty("cs_contact_phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the cuisine.
        /// </summary>
        [JsonProperty("cuisine")]
        public List<string> Cuisine { get; set; }

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
        /// Gets or sets the meal name.
        /// </summary>
        [JsonProperty("meal_name")]
        public Dictionary<int, string> MealName { get; set; }

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        [JsonProperty("menu")]
        public List<MealItem> Menu { get; set; }

        /// <summary>
        /// Gets or sets the restaurant name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        [JsonProperty("postal_code")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the delivery partner information.
        /// </summary>
        [JsonProperty("rds_info")]
        public RestaurantDeliveryPartner DeliveryPartnerInformation { get; set; }

        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        [JsonProperty("restaurant_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        [JsonProperty("services")]
        public Dictionary<string, BooleanValue> Services { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}