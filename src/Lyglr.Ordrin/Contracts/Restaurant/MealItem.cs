// <copyright file="MealItem.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a meal item that can contain other meal items.
    /// </summary>
    public class MealItem
    {
        /// <summary>
        /// Gets or sets the additional fee.
        /// </summary>
        [JsonProperty("additional_fee", Required = Required.Default)]
        public double AdditionalFee { get; set; }

        /// <summary>
        /// Gets or sets the availability.
        /// </summary>
        [JsonProperty("availability", Required = Required.Default)]
        public List<int> Availability { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("descrip", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the is orderable.
        /// </summary>
        [JsonProperty("is_orderable", Required = Required.Default)]
        public BooleanValue IsOrderable { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the item select weight.
        /// </summary>
        [JsonProperty("item_select_weight", Required = Required.Default)]
        public BooleanValue ItemSelectWeight { get; set; }

        /// <summary>
        /// Gets or sets the max child select.
        /// </summary>
        [JsonProperty("max_child_select", Required = Required.Default)]
        public int MaxChildSelect { get; set; }

        /// <summary>
        /// Gets or sets the max free child select.
        /// </summary>
        [JsonProperty("max_free_child_select", Required = Required.Default)]
        public int MaxFreeChildSelect { get; set; }

        /// <summary>
        /// Gets or sets the min child select.
        /// </summary>
        [JsonProperty("min_child_select", Required = Required.Default)]
        public int MinChildSelect { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [JsonProperty("price", Required = Required.Default)]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        [JsonProperty("children", Required = Required.Default)]
        public List<MealItem> Children { get; set; }
    }
}