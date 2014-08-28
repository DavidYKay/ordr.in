// <copyright file="RestaurantDeliveryCheck.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Respresents a restaurant delivery check.
    /// </summary>
    public class RestaurantDeliveryCheck
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("rid")]
        public string Id { get; set; }

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
        /// Gets or sets the message.
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the can service.
        /// </summary>
        [JsonProperty("can_service")]
        public BooleanValue CanService { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        [JsonProperty("service")]
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the standard delivery time.
        /// </summary>
        [JsonProperty("delivery")]
        public int StandardDeliveryTime { get; set; }

        /// <summary>
        /// Gets or sets the minimum order.
        /// </summary>
        [JsonProperty("mino")]
        public double MinimumOrder { get; set; }

        /// <summary>
        /// Gets or sets the estimated ready.
        /// </summary>
        [JsonProperty("ready")]
        public string EstimatedReady { get; set; }

        /// <summary>
        /// Gets or sets the estimated delivery.
        /// </summary>
        [JsonProperty("del")]
        public string EstimatedDelivery { get; set; }
    }
}