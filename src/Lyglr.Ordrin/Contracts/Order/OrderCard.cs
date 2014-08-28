// <copyright file="OrderCard.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.Order
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represent an order card.
    /// </summary>
    public class OrderCard
    {
        /// <summary>
        /// Gets or sets the card name.
        /// </summary>
        /// <remarks>Full name as it appears on the credit card.</remarks>
        [JsonProperty("card_name", Required = Required.AllowNull)]
        public string CardName { get; set; }

        /// <summary>
        /// Gets or Sets the number.
        /// </summary>
        /// <remarks>The 15 or 16 digit credit card number.</remarks>
        [JsonProperty("card_number", Required = Required.Always)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or Sets the cvc.
        /// </summary>
        /// <remarks>The 3 or 4 digit security code.</remarks>
        [JsonProperty("card_cvc", Required = Required.Always)]
        public string CardCvc { get; set; }

        /// <summary>
        /// Gets or Sets the expiryMonth.
        /// </summary>
        /// <remarks>Two digits/Four digits expiration.</remarks>
        [JsonProperty("card_expiry", Required = Required.Always)]
        public string CardExpiry { get; set; }

        /// <summary>
        /// Gets or Sets the billingAddress.
        /// </summary>
        /// <remarks>The street address of this card's billing address.</remarks>
        [JsonProperty("card_bill_addr", Required = Required.Always)]
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or Sets the billingAddress2.
        /// </summary>
        /// <remarks>(optional) The second part of the street address.</remarks>
        [JsonProperty("card_bill_addr2")]
        public string BillingAddress2 { get; set; }

        /// <summary>
        /// Gets or Sets the billingCity.
        /// </summary>
        /// <remarks>The city of this card's billing address.</remarks>
        [JsonProperty("card_bill_city", Required = Required.Always)]
        public string BillingCity { get; set; }

        /// <summary>
        /// Gets or Sets the billingState.
        /// </summary>
        /// <remarks>The state of this card's billing address.</remarks>
        [JsonProperty("card_bill_state", Required = Required.Always)]
        public string BillingState { get; set; }

        /// <summary>
        /// Gets or Sets the billingZip.
        /// </summary>
        /// <remarks>The zip of this card's billing address.</remarks>
        [JsonProperty("card_bill_zip", Required = Required.Always)]
        public string BillingZip { get; set; }

        /// <summary>
        /// Gets or sets the billing phone.
        /// </summary>
        /// <remarks>The credit card's billing phone number.</remarks>
        [JsonProperty("card_bill_phone", Required = Required.Always)]
        public string BillingPhone { get; set; } 
    }
}