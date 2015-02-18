// <copyright file="Utilities.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin
{
		using System;
    	using System.Globalization;
		using PCLCrypto;
		// using System.Security.Cryptography;
		using System.Text;
//    using Windows.Security.Cryptography;
//    using Windows.Security.Cryptography.Core;
//    using Windows.Storage.Streams;

    /// <summary>
    /// Utilities set.
    /// </summary>
    public class Utilities
    {
        /// Calculates the SHA256 result from a string 
        public static string CalculateSHA256(string text)
        {
		  byte[] data = Encoding.UTF8.GetBytes(text);
          var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
          byte[] hash = hasher.HashData(data);
          string hashBase64 = Convert.ToBase64String(hash);
          return hashBase64;
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
