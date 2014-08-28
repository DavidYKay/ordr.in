// <copyright file="UserApiTests.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
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
        [TestMethod]
        public void UserApiTests_CanChangePassword()
        {
            Run(this.UserApiTests_Success_ChangePassword);
        }

        /// <summary>
        /// The user api tests can save an address.
        /// </summary>
        [TestMethod]
        public void UserApiTests_Success_SaveAddress()
        {
            Run(this.UserApiTests_Success_SaveAddress);
        }

        /// <summary>
        /// The user api tests can save addresses and update an address.
        /// </summary>
        [TestMethod]
        public void UserApiTests_Success_UpdateAddressSaveAddresses()
        {
            Run(this.UserApiTests_Success_UpdateAddressSaveAddresses);
        }

        /// <summary>
        /// The user api tests can save credit card.
        /// </summary>
        [TestMethod]
        public void UserApiTests_Success_SaveCreditCard()
        {
            Run(this.UserApiTests_Success_SaveCreditCard);
        }

        /// <summary>
        /// The user api tests can save credit cards and update a credit card.
        /// </summary>
        [TestMethod]
        public void UserApiTests_Success_UpdateCardSaveCreditCards()
        {
            Run(this.UserApiTests_Success_UpdateCardSaveCreditCards);
        }

        private void UserApiTests_Success_ChangePassword(OrdrInUserClient client, AccountCreation accountInformation)
        {
            UserCredentials newCredentials = new UserCredentials
                {
                    Email = accountInformation.Email,
                    Password = Utilities.CalculateSHA256(Guid.NewGuid().ToString("N"))
                };

            client.ChangePasswordAsync(newCredentials, CancellationToken.None).Wait();

            AccountInformation accountInfo = client.GetAccountInformationAsync(CancellationToken.None).Result;
            
            Assert.AreEqual(accountInformation.Email, accountInfo.Email);
            Assert.AreEqual(accountInformation.FirstName, accountInfo.FirstName);
            Assert.AreEqual(accountInformation.LastName, accountInfo.LastName);
            Assert.AreEqual(newCredentials.Password, accountInfo.Password);
        }

        private void UserApiTests_Success_SaveAddress(OrdrInUserClient client, AccountCreation accountInformation)
        {
            UserAddressCreation newAddress = new UserAddressCreation
                {
                    Email = accountInformation.Email,
                    Address = "1007 Mountain Drive",
                    City = "Gotham City",
                    Zip = "08224",
                    State = "NJ",
                    Nickname = "Wayne Manor",
                    Phone = "(609) 545-5617"
                };

            client.CreateOrReplaceAddressAsync(newAddress, CancellationToken.None).Wait();

            UserAddress savedAddress = client.GetSavedAddressAsync(newAddress.Nickname, CancellationToken.None).Result;

            Assert.AreEqual(newAddress.Nickname, savedAddress.Nickname);
            Assert.AreEqual(newAddress.Address, savedAddress.Address);
            Assert.AreEqual(newAddress.City, savedAddress.City);
            Assert.AreEqual(newAddress.Phone, savedAddress.Phone);
            Assert.AreEqual(newAddress.State, savedAddress.State);
            Assert.AreEqual(newAddress.Zip, savedAddress.Zip);
        }

        private void UserApiTests_Success_UpdateAddressSaveAddresses(OrdrInUserClient client, AccountCreation accountInformation)
        {
            UserAddressCreation newAddress = new UserAddressCreation
            {
                Email = accountInformation.Email,
                Address = "1007 Mountain Drive",
                City = "Gotham City",
                Zip = "08224",
                State = "NJ",
                Nickname = "Wayne Manor",
                Phone = "(609) 545-5617"
            };

            UserAddressCreation newAddress2 = new UserAddressCreation
            {
                Email = accountInformation.Email,
                Address = "141 W Jackson Blvd",
                City = "Gotham City",
                Zip = "08224",
                State = "NJ",
                Nickname = "Wayne Tower",
                Phone = "(609) 545-7423"
            };

            client.CreateOrReplaceAddressAsync(newAddress, CancellationToken.None).Wait();
            client.CreateOrReplaceAddressAsync(newAddress2, CancellationToken.None).Wait();

            Dictionary<string, UserAddress> addresses = client.GetSavedAddressesAsync(CancellationToken.None).Result;

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

            client.CreateOrReplaceAddressAsync(newAddress2, CancellationToken.None).Wait();

            Dictionary<string, UserAddress> addresses2 = client.GetSavedAddressesAsync(CancellationToken.None).Result;

            Assert.IsTrue(addresses2.Any(address =>
                string.Equals(newAddress2.Address, address.Value.Address)
                && string.Equals(newAddress2.City, address.Value.City)
                && string.Equals(newAddress2.Phone, address.Value.Phone)
                && string.Equals(newAddress2.State, address.Value.State)
                && string.Equals(newAddress2.Zip, address.Value.Zip)));
        }

        private void UserApiTests_Success_SaveCreditCard(OrdrInUserClient client, AccountCreation accountInformation)
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

            client.CreateOrReplaceCreditCardAsync(newCreditCard, CancellationToken.None).Wait();

            CreditCardInformation creditCard = client.GetSavedCreditCardAsync(newCreditCard.Nickname, CancellationToken.None).Result;
            Assert.AreEqual(newCreditCard.CardNumber.Substring(11), creditCard.CardLast5Numbers);
            Assert.AreEqual(newCreditCard.BillingAddress, creditCard.BillingAddress);
            Assert.AreEqual(newCreditCard.BillingZip, creditCard.BillingZip);
        }

        private void UserApiTests_Success_UpdateCardSaveCreditCards(OrdrInUserClient client, AccountCreation accountInformation)
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

            client.CreateOrReplaceCreditCardAsync(newCreditCard, CancellationToken.None).Wait();
            client.CreateOrReplaceCreditCardAsync(newCreditCard2, CancellationToken.None).Wait();

            Dictionary<string, CreditCardInformation> creditCards = client.GetSavedCreditCardsAsync(CancellationToken.None).Result;

            Assert.IsTrue(creditCards.Any(cc => 
                string.Equals(cc.Value.BillingAddress, newCreditCard.BillingAddress)
                && string.Equals(cc.Value.BillingZip, newCreditCard.BillingZip)
                && string.Equals(cc.Value.CardLast5Numbers, newCreditCard.CardNumber.Substring(11))));

            Assert.IsTrue(creditCards.Any(cc =>
                string.Equals(cc.Value.BillingAddress, newCreditCard2.BillingAddress)
                && string.Equals(cc.Value.BillingZip, newCreditCard2.BillingZip)
                && string.Equals(cc.Value.CardLast5Numbers, newCreditCard2.CardNumber.Substring(11))));

            newCreditCard2.BillingAddress = "142 W Jackson Blvd";

            client.CreateOrReplaceCreditCardAsync(newCreditCard2, CancellationToken.None).Wait();

            Dictionary<string, CreditCardInformation> creditCards2 = client.GetSavedCreditCardsAsync(CancellationToken.None).Result;

            Assert.IsTrue(creditCards2.Any(cc =>
                string.Equals(cc.Value.BillingAddress, newCreditCard2.BillingAddress)
                && string.Equals(cc.Value.BillingZip, newCreditCard2.BillingZip)
                && string.Equals(cc.Value.CardLast5Numbers, newCreditCard2.CardNumber.Substring(11))));
        }

        private static void Run(Action<OrdrInUserClient, AccountCreation> testToRun)
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

            client.CreateAccountAsync(userAccountInformation, CancellationToken.None).Wait();

            // Run the test
            testToRun(client, userAccountInformation);
        }
    }
}