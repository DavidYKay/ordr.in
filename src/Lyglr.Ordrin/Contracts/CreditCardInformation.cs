// <copyright file="CreditCardInformation.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents one of the user's credit cards.
    /// </summary>
    public class CreditCardInformation
    {
        /// <summary>
        /// Gets or Sets the nickname.
        /// </summary>
        /// <remarks>The nickname of this credit card.</remarks>
        [JsonProperty("nick")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        /// <remarks>The name on the card.</remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the last5Numbers.
        /// </summary>
        /// <remarks>The last 5 digits of the card.</remarks>
        [JsonProperty("cc_last5")]
        public string Last5Numbers { get; set; }

        /// <summary>
        /// Gets or Sets the expiryMonth.
        /// </summary>
        /// <remarks>The 2 digit expiration month.</remarks>
        [JsonProperty("expiry_month")]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or Sets the expiryYear.
        /// </summary>
        /// <remarks>The 4 digit expiration year.</remarks>
        [JsonProperty("expiry_year")]
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Gets or Sets the type.
        /// </summary>
        /// <remarks>The type of card (i.e. Amex, Visa).</remarks>
        [JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or Sets the billingAddress.
        /// </summary>
        /// <remarks>The street address of this card's billing address.</remarks>
        [JsonProperty("bill_addr")]
        public int BillingAddress { get; set; }

        /// <summary>
        /// Gets or Sets the billingCity.
        /// </summary>
        /// <remarks>The city of this card's billing address.</remarks>
        [JsonProperty("bill_city")]
        public int BillingCity { get; set; }

        /// <summary>
        /// Gets or Sets the billingState.
        /// </summary>
        /// <remarks>The state of this card's billing address.</remarks>
        [JsonProperty("bill_state")]
        public int BillingState { get; set; }

        /// <summary>
        /// Gets or Sets the billingZip.
        /// </summary>
        /// <remarks>The zip of this card's billing address.</remarks>
        [JsonProperty("bill_zip")]
        public int BillingZip { get; set; }
    }
}