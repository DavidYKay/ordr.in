// <copyright file="UserAddress.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Contracts.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents one of the user's addresses.
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Gets or Sets the nickname.
        /// </summary>
        [JsonProperty("nick", Required = Required.Always)]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or Sets the address.
        /// </summary>
        [JsonProperty("addr", Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the address2.
        /// </summary>
        [JsonProperty("addr2", Required = Required.AllowNull)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or Sets the city.
        /// </summary>
        [JsonProperty("city", Required = Required.Always)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets the state.
        /// </summary>
        [JsonProperty("state", Required = Required.Always)]
        public string State { get; set; }

        /// <summary>
        /// Gets or Sets the zip.
        /// </summary>
        [JsonProperty("zip", Required = Required.Always)]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or Sets the phone.
        /// </summary>
        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }
    }
}