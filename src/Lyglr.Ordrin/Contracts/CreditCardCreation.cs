// <copyright file="CreditCardCreation.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the information required for a user's credit card
    /// creation.
    /// </summary>
    public class CreditCardCreation
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
        /// Gets or Sets the number.
        /// </summary>
        /// <remarks>The 15 or 16 digit credit card number.</remarks>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or Sets the cvc.
        /// </summary>
        /// <remarks>The 3 or 4 digit security code.</remarks>
        [JsonProperty("cvc")]
        public string Cvc { get; set; }

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
        /// Gets or Sets the billingAddress2.
        /// </summary>
        /// <remarks>(optional) The second part of the street address.</remarks>
        [JsonProperty("bill_addr2")]
        public int BillingAddress2 { get; set; }

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

        /// <summary>
        /// Gets or Sets the billingPhone.
        /// </summary>
        /// <remarks>The phone number on this credit card.</remarks>
        [JsonProperty("bill_phone")]
        public int BillingPhone { get; set; }
    }
}