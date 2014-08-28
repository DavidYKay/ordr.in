// <copyright file="CreditCardCreation.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the information required for a user's credit card
    /// creation.
    /// </summary>
    public class CreditCardCreation
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the nickname.
        /// </summary>
        /// <remarks>The nickname of this credit card.</remarks>
        [JsonProperty("nick")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or Sets the number.
        /// </summary>
        /// <remarks>The 15 or 16 digit credit card number.</remarks>
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or Sets the cvc.
        /// </summary>
        /// <remarks>The 3 or 4 digit security code.</remarks>
        [JsonProperty("card_cvc")]
        public string CardCvc { get; set; }

        /// <summary>
        /// Gets or Sets the expiryMonth.
        /// </summary>
        /// <remarks>Two digits/Four digits expiration.</remarks>
        [JsonProperty("card_expiry")]
        public string CardExpiry { get; set; }

        /// <summary>
        /// Gets or Sets the billingAddress.
        /// </summary>
        /// <remarks>The street address of this card's billing address.</remarks>
        [JsonProperty("bill_addr")]
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or Sets the billingAddress2.
        /// </summary>
        /// <remarks>(optional) The second part of the street address.</remarks>
        [JsonProperty("bill_addr2")]
        public string BillingAddress2 { get; set; }

        /// <summary>
        /// Gets or Sets the billingCity.
        /// </summary>
        /// <remarks>The city of this card's billing address.</remarks>
        [JsonProperty("bill_city")]
        public string BillingCity { get; set; }

        /// <summary>
        /// Gets or Sets the billingState.
        /// </summary>
        /// <remarks>The state of this card's billing address.</remarks>
        [JsonProperty("bill_state")]
        public string BillingState { get; set; }

        /// <summary>
        /// Gets or Sets the billingZip.
        /// </summary>
        /// <remarks>The zip of this card's billing address.</remarks>
        [JsonProperty("bill_zip")]
        public string BillingZip { get; set; }

        /// <summary>
        /// Gets or Sets the billingPhone.
        /// </summary>
        /// <remarks>The phone number on this credit card.</remarks>
        [JsonProperty("bill_phone")]
        public string BillingPhone { get; set; }
    }
}