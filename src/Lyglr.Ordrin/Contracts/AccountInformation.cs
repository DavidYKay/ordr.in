// <copyright file="AccountInformation.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represent the user's information.
    /// </summary>
    public class AccountInformation
    {
        /// <summary>
        /// Gets or Sets the userId.
        /// </summary>
        [JsonProperty("_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        [JsonProperty("em")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the firstName.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets the lastName.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets the password.
        /// </summary>
        [JsonProperty("pw")]
        public string Password { get; set; }
    }
}