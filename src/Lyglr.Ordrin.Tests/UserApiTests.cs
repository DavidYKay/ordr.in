// <copyright file="UserApiTests.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Tests
{
    using System;
    using System.Threading;
    using Lyglr.Ordrin.Client;
    using Lyglr.Ordrin.Contracts;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    /// <summary>
    /// Set of tests for the User API.
    /// </summary>
    [TestClass]
    public class UserApiTests : TestBase
    {
        /// <summary>
        /// The user api tests can get user info.
        /// </summary>
        [TestMethod]
        public void UserApiTests_CanGetUserInfo()
        {
            Run(UserApiTests_CanGetUserInfo);
        }

        private static void UserApiTests_CanGetUserInfo(OrdrInUserClient client, AccountCreation accountInformation)
        {
            AccountInformation accountInfo = client.GetAccountInformationAsync(CancellationToken.None).Result;
            Assert.AreEqual(accountInformation.Email, accountInfo.Email);
            Assert.AreEqual(accountInformation.FirstName, accountInfo.FirstName);
            Assert.AreEqual(accountInformation.LastName, accountInfo.LastName);
            Assert.AreEqual(accountInformation.Password, accountInfo.Password);
        }

        private static void Run(Action<OrdrInUserClient, AccountCreation> toRun)
        {
            string email = Utilities.InvariantFormat("{0}@test.com", Guid.NewGuid().ToString("N"));
            string password = Utilities.CalculateSHA256(Guid.NewGuid().ToString("N"));

            // Create the client
            OrdrInUserClient client = new OrdrInUserClient(OrdrInServiceAddresses.UserTestEndpoint, DeveloperKey, email, password);

            // Create an account
            AccountCreation userAccountInformation = new AccountCreation
                {
                    Email = email,
                    Password = password,
                    FirstName = "Test",
                    LastName = "Test"
                };

            client.CreateAccountAsync(userAccountInformation, CancellationToken.None).Wait();

            // Run the test
            toRun(client, userAccountInformation);
        }
    }
}