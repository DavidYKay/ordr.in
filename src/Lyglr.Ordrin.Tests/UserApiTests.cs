// <copyright file="UserApiTests.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Lyglr.Ordrin.Client;
    using Lyglr.Ordrin.Contracts.User;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    /// <summary>
    /// Set of tests for the User API.
    /// </summary>
    [TestClass]
    public class UserApiTests : TestBase
    {
        /// <summary>
        /// The user api tests can change the password.
        /// </summary>
        /// <returns>A task to track the asynchronous progress.</returns>
        [TestMethod]
        public async Task UserApiTests_Success_ChangePassword()
        {
            await Run(async (client, accountInformation) =>
                {
                    UserCredentials newCredentials = new UserCredentials
                    {
                        Email = accountInformation.Email,
                        Password = Utilities.CalculateSHA256(Guid.NewGuid().ToString("N"))
                    };

                    await client.ChangePasswordAsync(newCredentials, CancellationToken.None);

                    AccountInformation accountInfo = await client.GetAccountInformationAsync(CancellationToken.None);

                    Assert.AreEqual(accountInformation.Email, accountInfo.Email);
                    Assert.AreEqual(accountInformation.FirstName, accountInfo.FirstName);
                    Assert.AreEqual(accountInformation.LastName, accountInfo.LastName);
                    Assert.AreEqual(newCredentials.Password, accountInfo.Password);
                });
        }

        /// <summary>
        /// The user api tests can save an address.
        /// </summary>
        /// <returns>A task to track the asynchronous progress.</returns>
        [TestMethod]
        public async Task UserApiTests_Success_SaveAddress()
        {
            await Run(async (client, accountInformation) =>
                {
                    UserAddressCreation newAddress = new UserAddressCreation
                    {
                        Email = accountInformation.Email,
                        Address = "1007 Mountain Drive",
                        City = "Gotham City",
                        Zip = "08224",
                        State = "NJ",
                        Nickname = "Wayne Manor",
                        Phone = "6095455617"
                    };

                    await client.CreateOrReplaceAddressAsync(newAddress, CancellationToken.None);

                    UserAddress savedAddress = await client.GetSavedAddressAsync(newAddress.Nickname, CancellationToken.None);

                    Assert.AreEqual(newAddress.Nickname, savedAddress.Nickname);
                    Assert.AreEqual(newAddress.Address, savedAddress.Address);
                    Assert.AreEqual(newAddress.City, savedAddress.City);
                    Assert.AreEqual(newAddress.Phone, savedAddress.Phone);
                    Assert.AreEqual(newAddress.State, savedAddress.State);
                    Assert.AreEqual(newAddress.Zip, savedAddress.Zip);
                });
        }

        /// <summary>
        /// The user api tests can save addresses and update an address.
        /// </summary>
        /// <returns>A task to track the asynchronous progress.</returns>
        [TestMethod]
        public async Task UserApiTests_Success_UpdateAddressSaveAddresses()
        {
            await Run(async (client, accountInformation) =>
                {
                    UserAddressCreation newAddress = new UserAddressCreation
                    {
                        Email = accountInformation.Email,
                        Address = "1007 Mountain Drive",
                        City = "Gotham City",
                        Zip = "08224",
                        State = "NJ",
                        Nickname = "Wayne Manor",
                        Phone = "6095455617"
                    };

                    UserAddressCreation newAddress2 = new UserAddressCreation
                    {
                        Email = accountInformation.Email,
                        Address = "141 W Jackson Blvd",
                        City = "Gotham City",
                        Zip = "08224",
                        State = "NJ",
                        Nickname = "Wayne Tower",
                        Phone = "6095457423"
                    };

                    await client.CreateOrReplaceAddressAsync(newAddress, CancellationToken.None);
                    await client.CreateOrReplaceAddressAsync(newAddress2, CancellationToken.None);

                    Dictionary<string, UserAddress> addresses = await client.GetSavedAddressesAsync(CancellationToken.None);

                    Assert.IsTrue(addresses.Any(address =>
                        string.Equals(newAddress.Address, address.Value.Address)
                        && string.Equals(newAddress.City, address.Value.City)
                        && string.Equals(newAddress.Phone, address.Value.Phone)
                        && string.Equals(newAddress.State, address.Value.State)
                        && string.Equals(newAddress.Zip, address.Value.Zip)));

                    Assert.IsTrue(addresses.Any(address =>
                        string.Equals(newAddress2.Address, address.Value.Address)
                        && string.Equals(newAddress2.City, address.Value.City)
                        && string.Equals(newAddress2.Phone, address.Value.Phone)
                        && string.Equals(newAddress2.State, address.Value.State)
                        && string.Equals(newAddress2.Zip, address.Value.Zip)));

                    newAddress2.Address = "142 W Jackson Blvd";

                    await client.CreateOrReplaceAddressAsync(newAddress2, CancellationToken.None);

                    Dictionary<string, UserAddress> addresses2 = await client.GetSavedAddressesAsync(CancellationToken.None);

                    Assert.IsTrue(addresses2.Any(address =>
                        string.Equals(newAddress2.Address, address.Value.Address)
                        && string.Equals(newAddress2.City, address.Value.City)
                        && string.Equals(newAddress2.Phone, address.Value.Phone)
                        && string.Equals(newAddress2.State, address.Value.State)
                        && string.Equals(newAddress2.Zip, address.Value.Zip)));
                });
        }

        /// <summary>
        /// The user api tests can save credit card.
        /// </summary>
        /// <returns>A task to track the asynchronous progress.</returns>
        [TestMethod]
        public async Task UserApiTests_Success_SaveCreditCard()
        {
            await Run(async (client, accountInformation) =>
                {
                    CreditCardCreation newCreditCard = new CreditCardCreation
                    {
                        BillingAddress = "1007 Mountain Drive",
                        BillingCity = "Gotham City",
                        BillingZip = "08224",
                        BillingState = "NJ",
                        Nickname = "Credit Card 1",
                        BillingPhone = "(609) 545-5617",
                        CardCvc = "000",
                        CardExpiry = "12/2121",
                        CardNumber = "4111111111111111"
                    };

                    await client.CreateOrReplaceCreditCardAsync(newCreditCard, CancellationToken.None);

                    CreditCardInformation creditCard = await client.GetSavedCreditCardAsync(newCreditCard.Nickname, CancellationToken.None);
                    Assert.AreEqual(newCreditCard.CardNumber.Substring(11), creditCard.CardLast5Numbers);
                    Assert.AreEqual(newCreditCard.BillingAddress, creditCard.BillingAddress);
                    Assert.AreEqual(newCreditCard.BillingZip, creditCard.BillingZip);
                });
        }

        /// <summary>
        /// The user api tests can save credit cards and update a credit card.
        /// </summary>
        /// <returns>A task to track the asynchronous progress.</returns>
        [TestMethod]
        public async Task UserApiTests_Success_UpdateCardSaveCreditCards()
        {
            await Run(async (client, accountInformation) =>
                {
                    CreditCardCreation newCreditCard = new CreditCardCreation
                    {
                        BillingAddress = "1007 Mountain Drive",
                        BillingCity = "Gotham City",
                        BillingZip = "08224",
                        BillingState = "NJ",
                        Nickname = "Credit Card 1",
                        BillingPhone = "(609) 545-5617",
                        CardCvc = "000",
                        CardExpiry = "12/2121",
                        CardNumber = "4111111111111111",
                    };

                    CreditCardCreation newCreditCard2 = new CreditCardCreation
                    {
                        BillingAddress = "1007 Mountain Drive",
                        BillingCity = "Gotham City",
                        BillingZip = "08224",
                        BillingState = "NJ",
                        Nickname = "Credit Card 2",
                        BillingPhone = "(609) 545-6617",
                        CardCvc = "000",
                        CardExpiry = "12/2121",
                        CardNumber = "4111111111111111",
                    };

                    await client.CreateOrReplaceCreditCardAsync(newCreditCard, CancellationToken.None);
                    await client.CreateOrReplaceCreditCardAsync(newCreditCard2, CancellationToken.None);

                    Dictionary<string, CreditCardInformation> creditCards = await client.GetSavedCreditCardsAsync(CancellationToken.None);

                    Assert.IsTrue(creditCards.Any(cc =>
                        string.Equals(cc.Value.BillingAddress, newCreditCard.BillingAddress)
                        && string.Equals(cc.Value.BillingZip, newCreditCard.BillingZip)
                        && string.Equals(cc.Value.CardLast5Numbers, newCreditCard.CardNumber.Substring(11))));

                    Assert.IsTrue(creditCards.Any(cc =>
                        string.Equals(cc.Value.BillingAddress, newCreditCard2.BillingAddress)
                        && string.Equals(cc.Value.BillingZip, newCreditCard2.BillingZip)
                        && string.Equals(cc.Value.CardLast5Numbers, newCreditCard2.CardNumber.Substring(11))));

                    newCreditCard2.BillingAddress = "142 W Jackson Blvd";

                    await client.CreateOrReplaceCreditCardAsync(newCreditCard2, CancellationToken.None);

                    Dictionary<string, CreditCardInformation> creditCards2 = await client.GetSavedCreditCardsAsync(CancellationToken.None);

                    Assert.IsTrue(creditCards2.Any(cc =>
                        string.Equals(cc.Value.BillingAddress, newCreditCard2.BillingAddress)
                        && string.Equals(cc.Value.BillingZip, newCreditCard2.BillingZip)
                        && string.Equals(cc.Value.CardLast5Numbers, newCreditCard2.CardNumber.Substring(11))));
                });
        }
        
        private static async Task Run(Func<OrdrInUserClient, AccountCreation, Task> testToRun)
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
                    FirstName = "Bruce",
                    LastName = "Wayne"
                };

            await client.CreateAccountAsync(userAccountInformation, CancellationToken.None);

            // Run the test
            await testToRun(client, userAccountInformation);
        }
    }
}