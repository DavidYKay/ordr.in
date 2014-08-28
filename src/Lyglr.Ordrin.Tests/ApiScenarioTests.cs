// <copyright file="ApiScenarioTests.cs" company="Lexsoft.fr">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Lyglr.Ordrin.Client;
    using Lyglr.Ordrin.Contracts.Order;
    using Lyglr.Ordrin.Contracts.Restaurant;
    using Lyglr.Ordrin.Contracts.User;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class ApiScenarioTests : TestBase
    {
        [TestMethod]
        public void ApiScenario_Success_OrderWithSavedInfo()
        {
            this.Run(this.ApiScenario_Success_OrderWithSavedInfo);
        }

        [TestMethod]
        public void ApiScenario_Success_OrderWithSavedAddress()
        {
            this.Run(this.ApiScenario_Success_OrderWithSavedAddress);
        }

        [TestMethod]
        public void ApiScenario_Success_OrderWithSavedCC()
        {
            this.Run(this.ApiScenario_Success_OrderWithSavedCC);
        }

        [TestMethod]
        public void ApiScenario_Success_GuestOrder()
        {
            const string zipCode = "10003";
            const string city = "New York";
            const string address = "410 Lafayette St";

            OrdrInRestaurantClient restaurantClient = new OrdrInRestaurantClient(OrdrInServiceAddresses.RestaurantTestEndpoint, DeveloperKey);
            OrdrInOrderClient orderClient = new OrdrInOrderClient(OrdrInServiceAddresses.OrderTestEndpoint, DeveloperKey);

            DateTime orderDate = DateTime.UtcNow.Date.AddDays(1).AddHours(19);

            List<Restaurant> deliveryList = restaurantClient.GetDeliveryListAsync(orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryList);
            Restaurant firstRestaurant = deliveryList.FirstOrDefault(restaurant => restaurant.IsDelivering == BooleanValue.True && restaurant.DeliveryTime > 0);
            Assert.IsNotNull(firstRestaurant);

            RestaurantDetails restaurantDetails = restaurantClient.GetRestaurantDetailsAsync(firstRestaurant.Id, CancellationToken.None).Result;
            Assert.IsNotNull(restaurantDetails);

            RestaurantDeliveryCheck deliveryCheck = restaurantClient.GetDeliveryCheckAsync(firstRestaurant.Id, orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryCheck);

            MealItem itemToOrder = FindItemToOrder(restaurantDetails.Menu);
            Assert.IsNotNull(itemToOrder);
            List<MealItem> options = new List<MealItem>();
            FindOptionsToOrder(itemToOrder, 0, ref options);

            double subtotal = Math.Round((itemToOrder.Price + options.Sum(item => item.Price)) * 10, 2);
            double tip = Math.Round(subtotal * 0.15, 2);

            RestaurantFee fee = restaurantClient.GetRestaurantFeeAsync(firstRestaurant.Id, orderDate, subtotal.ToString(CultureInfo.InvariantCulture), tip.ToString(CultureInfo.InvariantCulture), zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(fee);

            string tray = Utilities.InvariantFormat("{0}/10{1}", itemToOrder.Id, string.Join(string.Empty, options.Select(item => "," + item.Id)));

            GuestOrder userOrderWithSavedCC = new GuestOrder
            {
                Email = Utilities.InvariantFormat("{0}@test.com", Guid.NewGuid().ToString("N")),
                DeliveryDate = orderDate.ToString("MM-dd"),
                DeliveryTime = orderDate.ToString("HH:mm"),
                FirstName = "Bruce",
                LastName = "Wayne",
                RestaurantId = firstRestaurant.Id,
                Tray = tray,
                Tip = tip.ToString(CultureInfo.InvariantCulture),
                Address = "410 Lafayette St",
                City = "New York",
                Zip = "10003",
                State = "NY",
                Phone = "(609) 545-5617",
                BillingAddress = "1007 Mountain Drive",
                BillingCity = "Gotham City",
                BillingZip = "08224",
                BillingState = "NJ",
                BillingPhone = "(609) 545-5617",
                CardCvc = "000",
                CardExpiry = "12/2121",
                CardNumber = "4111111111111111",
                CardName = "Bruce Wayne"
            };

            OrderInformation orderWithSavedInfo = orderClient.CreateOrderAsync(userOrderWithSavedCC, CancellationToken.None).Result;
            Assert.IsNotNull(orderWithSavedInfo);
            Assert.IsNotNull(orderWithSavedInfo.OrderId);
        }

        public void ApiScenario_Success_OrderWithSavedInfo(OrdrInUserClient userClient, OrdrInOrderClient orderClient, OrdrInRestaurantClient restaurantClient, AccountCreation account)
        {
            const string zipCode = "10003";
            const string city = "New York";
            const string address = "410 Lafayette St";

            DateTime orderDate = DateTime.UtcNow.Date.AddDays(1).AddHours(19);

            List<Restaurant> deliveryList = restaurantClient.GetDeliveryListAsync(orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryList);
            Restaurant firstRestaurant = deliveryList.FirstOrDefault(restaurant => restaurant.IsDelivering == BooleanValue.True && restaurant.DeliveryTime > 0);
            Assert.IsNotNull(firstRestaurant);

            RestaurantDetails restaurantDetails = restaurantClient.GetRestaurantDetailsAsync(firstRestaurant.Id, CancellationToken.None).Result;
            Assert.IsNotNull(restaurantDetails);

            RestaurantDeliveryCheck deliveryCheck = restaurantClient.GetDeliveryCheckAsync(firstRestaurant.Id, orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryCheck);

            MealItem itemToOrder = FindItemToOrder(restaurantDetails.Menu);
            Assert.IsNotNull(itemToOrder);
            List<MealItem> options = new List<MealItem>();
            FindOptionsToOrder(itemToOrder, 0, ref options);

            double subtotal = Math.Round((itemToOrder.Price + options.Sum(item => item.Price)) * 10, 2);
            double tip = Math.Round(subtotal * 0.15, 2);

            RestaurantFee fee = restaurantClient.GetRestaurantFeeAsync(firstRestaurant.Id, orderDate, subtotal.ToString(CultureInfo.InvariantCulture), tip.ToString(CultureInfo.InvariantCulture), zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(fee);

            KeyValuePair<string, UserAddress> savedAddress = userClient.GetSavedAddressesAsync(CancellationToken.None).Result.FirstOrDefault();
            Assert.IsNotNull(savedAddress);
            Assert.IsNotNull(savedAddress.Value);

            KeyValuePair<string, CreditCardInformation> savedCC = userClient.GetSavedCreditCardsAsync(CancellationToken.None).Result.FirstOrDefault();
            Assert.IsNotNull(savedCC);
            Assert.IsNotNull(savedCC.Value);

            string tray = Utilities.InvariantFormat("{0}/10{1}", itemToOrder.Id, string.Join(string.Empty, options.Select(item => "," + item.Id)));

            UserOrderWithSavedInfo userOrderWithSavedInfo = new UserOrderWithSavedInfo
                {
                    Email = account.Email,
                    DeliveryDate = orderDate.ToString("MM-dd"),
                    DeliveryTime = orderDate.ToString("HH:mm"),
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    RestaurantId = firstRestaurant.Id,
                    Tray = tray,
                    Tip = tip.ToString(CultureInfo.InvariantCulture),
                    AddressNickname = savedAddress.Value.Nickname,
                    CardNickname = savedCC.Value.Nickname
                };

            OrderInformation orderWithSavedInfo = orderClient.CreateOrderAsync(userOrderWithSavedInfo, CancellationToken.None).Result;
            Assert.IsNotNull(orderWithSavedInfo);
            Assert.IsNotNull(orderWithSavedInfo.OrderId);

            Order historyOneOrder = userClient.GetOrderAsync(orderWithSavedInfo.OrderId, CancellationToken.None).Result;
            Assert.IsNotNull(historyOneOrder);
            Assert.AreEqual(orderWithSavedInfo.OrderId, historyOneOrder.OrderId);
            Assert.AreEqual(fee.Tax, historyOneOrder.Tax);

            List<Order> historyOrders = userClient.GetOrderHistoryAsync(CancellationToken.None).Result;
            Assert.IsTrue(historyOrders.Any(order => string.Equals(orderWithSavedInfo.OrderId, order.OrderId)));
        }

        public void ApiScenario_Success_OrderWithSavedAddress(OrdrInUserClient userClient, OrdrInOrderClient orderClient, OrdrInRestaurantClient restaurantClient, AccountCreation account)
        {
            const string zipCode = "10003";
            const string city = "New York";
            const string address = "410 Lafayette St";

            DateTime orderDate = DateTime.UtcNow.Date.AddDays(1).AddHours(19);

            List<Restaurant> deliveryList = restaurantClient.GetDeliveryListAsync(orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryList);
            Restaurant firstRestaurant = deliveryList.FirstOrDefault(restaurant => restaurant.IsDelivering == BooleanValue.True && restaurant.DeliveryTime > 0);
            Assert.IsNotNull(firstRestaurant);

            RestaurantDetails restaurantDetails = restaurantClient.GetRestaurantDetailsAsync(firstRestaurant.Id, CancellationToken.None).Result;
            Assert.IsNotNull(restaurantDetails);

            RestaurantDeliveryCheck deliveryCheck = restaurantClient.GetDeliveryCheckAsync(firstRestaurant.Id, orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryCheck);

            MealItem itemToOrder = FindItemToOrder(restaurantDetails.Menu);
            Assert.IsNotNull(itemToOrder);
            List<MealItem> options = new List<MealItem>();
            FindOptionsToOrder(itemToOrder, 0, ref options);

            double subtotal = Math.Round((itemToOrder.Price + options.Sum(item => item.Price)) * 10, 2);
            double tip = Math.Round(subtotal * 0.15, 2);

            RestaurantFee fee = restaurantClient.GetRestaurantFeeAsync(firstRestaurant.Id, orderDate, subtotal.ToString(CultureInfo.InvariantCulture), tip.ToString(CultureInfo.InvariantCulture), zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(fee);

            KeyValuePair<string, UserAddress> savedAddress = userClient.GetSavedAddressesAsync(CancellationToken.None).Result.FirstOrDefault();
            Assert.IsNotNull(savedAddress);
            Assert.IsNotNull(savedAddress.Value);
            
            string tray = Utilities.InvariantFormat("{0}/10{1}", itemToOrder.Id, string.Join(string.Empty, options.Select(item => "," + item.Id)));

            UserOrderWithSavedAddress userOrderWithSavedAddress = new UserOrderWithSavedAddress()
            {
                Email = account.Email,
                DeliveryDate = orderDate.ToString("MM-dd"),
                DeliveryTime = orderDate.ToString("HH:mm"),
                FirstName = "Bruce",
                LastName = "Wayne",
                RestaurantId = firstRestaurant.Id,
                Tray = tray,
                Tip = tip.ToString(CultureInfo.InvariantCulture),
                AddressNickname = savedAddress.Value.Nickname,
                BillingAddress = "1007 Mountain Drive",
                BillingCity = "Gotham City",
                BillingZip = "08224",
                BillingState = "NJ",
                BillingPhone = "(609) 545-5617",
                CardCvc = "000",
                CardExpiry = "12/2121",
                CardNumber = "4111111111111111",
                CardName = "Bruce Wayne"
            };

            OrderInformation orderWithSavedInfo = orderClient.CreateOrderAsync(userOrderWithSavedAddress, CancellationToken.None).Result;
            Assert.IsNotNull(orderWithSavedInfo);
            Assert.IsNotNull(orderWithSavedInfo.OrderId);

            Order historyOneOrder = userClient.GetOrderAsync(orderWithSavedInfo.OrderId, CancellationToken.None).Result;
            Assert.IsNotNull(historyOneOrder);
            Assert.AreEqual(orderWithSavedInfo.OrderId, historyOneOrder.OrderId);
            Assert.AreEqual(fee.Tax, historyOneOrder.Tax);

            List<Order> historyOrders = userClient.GetOrderHistoryAsync(CancellationToken.None).Result;
            Assert.IsTrue(historyOrders.Any(order => string.Equals(orderWithSavedInfo.OrderId, order.OrderId)));
        }

        public void ApiScenario_Success_OrderWithSavedCC(OrdrInUserClient userClient, OrdrInOrderClient orderClient, OrdrInRestaurantClient restaurantClient, AccountCreation account)
        {
            const string zipCode = "10003";
            const string city = "New York";
            const string address = "410 Lafayette St";

            DateTime orderDate = DateTime.UtcNow.Date.AddDays(1).AddHours(19);

            List<Restaurant> deliveryList = restaurantClient.GetDeliveryListAsync(orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryList);
            Restaurant firstRestaurant = deliveryList.FirstOrDefault(restaurant => restaurant.IsDelivering == BooleanValue.True && restaurant.DeliveryTime > 0);
            Assert.IsNotNull(firstRestaurant);

            RestaurantDetails restaurantDetails = restaurantClient.GetRestaurantDetailsAsync(firstRestaurant.Id, CancellationToken.None).Result;
            Assert.IsNotNull(restaurantDetails);

            RestaurantDeliveryCheck deliveryCheck = restaurantClient.GetDeliveryCheckAsync(firstRestaurant.Id, orderDate, zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(deliveryCheck);

            MealItem itemToOrder = FindItemToOrder(restaurantDetails.Menu);
            Assert.IsNotNull(itemToOrder);
            List<MealItem> options = new List<MealItem>();
            FindOptionsToOrder(itemToOrder, 0, ref options);

            double subtotal = Math.Round((itemToOrder.Price + options.Sum(item => item.Price)) * 10, 2);
            double tip = Math.Round(subtotal * 0.15, 2);

            RestaurantFee fee = restaurantClient.GetRestaurantFeeAsync(firstRestaurant.Id, orderDate, subtotal.ToString(CultureInfo.InvariantCulture), tip.ToString(CultureInfo.InvariantCulture), zipCode, city, address, CancellationToken.None).Result;
            Assert.IsNotNull(fee);

            KeyValuePair<string, CreditCardInformation> savedCC = userClient.GetSavedCreditCardsAsync(CancellationToken.None).Result.FirstOrDefault();
            Assert.IsNotNull(savedCC);
            Assert.IsNotNull(savedCC.Value);

            string tray = Utilities.InvariantFormat("{0}/10{1}", itemToOrder.Id, string.Join(string.Empty, options.Select(item => "," + item.Id)));

            UserOrderWithSavedCreditCard userOrderWithSavedCC = new UserOrderWithSavedCreditCard
            {
                Email = account.Email,
                DeliveryDate = orderDate.ToString("MM-dd"),
                DeliveryTime = orderDate.ToString("HH:mm"),
                FirstName = "Bruce",
                LastName = "Wayne",
                RestaurantId = firstRestaurant.Id,
                Tray = tray,
                Tip = tip.ToString(CultureInfo.InvariantCulture),
                CardNickname = savedCC.Value.Nickname,
                Address = "410 Lafayette St",
                City = "New York",
                Zip = "10003",
                State = "NY",
                Phone = "(609) 545-5617"
            };

            OrderInformation orderWithSavedInfo = orderClient.CreateOrderAsync(userOrderWithSavedCC, CancellationToken.None).Result;
            Assert.IsNotNull(orderWithSavedInfo);
            Assert.IsNotNull(orderWithSavedInfo.OrderId);

            Order historyOneOrder = userClient.GetOrderAsync(orderWithSavedInfo.OrderId, CancellationToken.None).Result;
            Assert.IsNotNull(historyOneOrder);
            Assert.AreEqual(orderWithSavedInfo.OrderId, historyOneOrder.OrderId);
            Assert.AreEqual(fee.Tax, historyOneOrder.Tax);

            List<Order> historyOrders = userClient.GetOrderHistoryAsync(CancellationToken.None).Result;
            Assert.IsTrue(historyOrders.Any(order => string.Equals(orderWithSavedInfo.OrderId, order.OrderId)));
        }

        private static MealItem FindItemToOrder(List<MealItem> items)
        {
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.Availability != null && item.IsOrderable == BooleanValue.True)
                    {
                        return item;
                    }

                    return FindItemToOrder(item.Children);
                }
            }

            return null;
        }

        private static void FindOptionsToOrder(MealItem rootItem, int optionCount, ref List<MealItem> options)
        {
            if (rootItem.Children == null)
            {
                return;
            }

            foreach (MealItem item in rootItem.Children)
            {
                if (item.IsOrderable == BooleanValue.True && options.Count < optionCount)
                {
                    options.Add(item);
                }
                else if (item.IsOrderable == BooleanValue.False)
                {
                    List<MealItem> newList = new List<MealItem>();
                    FindOptionsToOrder(item, item.MinChildSelect, ref newList);
                    options.AddRange(newList);
                }
            }
        }

        private void Run(Action<OrdrInUserClient, OrdrInOrderClient, OrdrInRestaurantClient, AccountCreation> testToRun)
        {
            string email = Utilities.InvariantFormat("{0}@test.com", Guid.NewGuid().ToString("N"));
            string password = Utilities.CalculateSHA256(Guid.NewGuid().ToString("N"));

            // Create the clients
            OrdrInUserClient client = new OrdrInUserClient(OrdrInServiceAddresses.UserTestEndpoint, DeveloperKey, email, password);
            OrdrInRestaurantClient restaurantClient = new OrdrInRestaurantClient(OrdrInServiceAddresses.RestaurantTestEndpoint, DeveloperKey);
            OrdrInOrderClient orderClient = new OrdrInOrderClient(OrdrInServiceAddresses.OrderTestEndpoint, DeveloperKey, email, password);

            // Create an account
            AccountCreation userAccountInformation = new AccountCreation
            {
                Email = email,
                Password = password,
                FirstName = "Bruce",
                LastName = "Wayne"
            };

            client.CreateAccountAsync(userAccountInformation, CancellationToken.None).Wait();

            // Create a default address
            UserAddressCreation newAddress = new UserAddressCreation
            {
                Email = email,
                Address = "410 Lafayette St",
                City = "New York",
                Zip = "10003",
                State = "NY",
                Nickname = "Wayne Manor",
                Phone = "(609) 545-5617"
            };

            client.CreateOrReplaceAddressAsync(newAddress, CancellationToken.None).Wait();

            // Create a default card
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

            // Run the test
            testToRun(client, orderClient, restaurantClient, userAccountInformation);
        }
    }
}