// <copyright file="AccountUserId.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the response received from an account creation.
    /// </summary>
    public class AccountUserId
    {
        /// <summary>
        /// Gets or Sets the userId.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}