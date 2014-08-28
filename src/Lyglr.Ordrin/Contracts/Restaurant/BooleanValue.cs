// <copyright file="BooleanValue.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>
namespace Lyglr.Ordrin.Contracts.Restaurant
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a boolean value as an enum.
    /// </summary>
    public enum BooleanValue
    {
        /// <summary>
        /// A boolean false value.
        /// </summary>
        [EnumMember(Value = "no")]
        False = 0,

        /// <summary>
        /// A boolean true value.
        /// </summary>
        [EnumMember(Value = "yes")]
        True = 1,
    }
}