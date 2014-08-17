// <copyright file="Utilities.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin
{
    using System.Globalization;
    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    /// <summary>
    /// Utilities set.
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Calculates the SHA256 result from a string <see cref="value"/>.
        /// </summary>
        /// <remarks>Following: http://stackoverflow.com/questions/12503032/how-to-create-sha-256-hashes-in-winrt</remarks>
        /// <param name="value">Value to hash.</param>
        /// <returns>The hashed value.</returns>
        public static string CalculateSHA256(string value)
        {
            // put the string in a buffer, UTF-8 encoded...
            IBuffer input = CryptographicBuffer.ConvertStringToBinary(value, BinaryStringEncoding.Utf8);

            // hash it...
            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm("SHA256");
            IBuffer hashed = hasher.HashData(input);

            // format it...
            return CryptographicBuffer.EncodeToHexString(hashed);
        }

        /// <summary>
        /// Invariantly formats the <paramref name="parameters"/>.
        /// </summary>
        /// <param name="format">Format to use for formatting.</param>
        /// <param name="parameters">Parameters to format.</param>
        /// <returns>Invariantly formatted string.</returns>
        public static string InvariantFormat(string format, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture, format, parameters);
        }
    }
}