// <copyright file="UserCredentials.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// base credentials for a user.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}