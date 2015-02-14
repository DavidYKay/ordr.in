Lyglr ordr.in
=======
This is a portable Win8.1/WP8.1 library for the ordr.in service.
See full API documentation at http://hackfood.ordr.in

## Installation

### Source
Download the zip from the project home page.

###Binaries
Download the package from [NuGet](https://www.nuget.org/packages/Lyglr.Ordrin) or run the following command in your project package manager:
```
PM> Install-Package Lyglr.Ordrin 
```

## Usage
While the tests available in the source code provide a good idea on how to use the library, here is a comprehensive guide.

### Global to all clients
All clients require at least two values: The service address and the developer key.
The service address should be one of the constants available in `Lyglr.Ordrin.Client.OrdrInServiceAddresses`.
The developer key can be found in your [ordr.in account](https://hackfood.ordr.in/account).

### User Client ([API Reference](https://hackfood.ordr.in/docs/user))
#### Initialization
--------------------------------------------------
```CSharp
OrdrInUserClient client = new OrdrInUserClient(serviceAddress, developerKey, email, password);
```
* serviceAddress: See [Global to all clients](#Global to all clients)
* developerKey: See [Global to all clients](#Global to all clients)
* email: The ordr.in client email.
* password: SHA256 hashed password.

#### Usage
--------------------------------------------------
##### Gets information about a given authenticated user.
```CSharp
AccountInformation result = await client.GetAccountInformationAsync(cancellationToken);
```
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Creates a new account.
```CSharp
string result = await client.CreateAccountAsync(accountCreation, cancellationToken);
```
* accountCreation <AccountCreation>: Information used to create the account.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets all addresses saved to a user's account.
```CSharp
Dictionary<string, UserAddress> result = await client.GetSavedAddressesAsync(cancellationToken);
```
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets details about a single user address.
```CSharp
UserAddress result = await client.GetSavedAddressAsync(nick, cancellationToken);
```
* nick <String>: Nickname of the address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Creates a new address. If used with the nickname of an existing address it will edit it.
```CSharp
await client.CreateOrReplaceAddressAsync(userAddress, cancellationToken);
```
* userAddress <UserAddressCreation>: User address to create or replace.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Deletes a user's address.
```CSharp
await client.RemoveAddressAsync(nick, cancellationToken);
```
* nick <String>: Nickname of the address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets all of a user's saved credit cards.
```CSharp
Dictionary<string, CreditCardInformation> result = await client.GetSavedCreditCardsAsync(cancellationToken);
```
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets a single saved credit card by its nickname.
```CSharp
CreditCardInformation result = await client.GetSavedCreditCardAsync(nick, cancellationToken);
```
* nick <String>: Nickname of the credit card.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Adds a new credit card. If a card already exists with the given nickname, the call replaces it.
```CSharp
await client.CreateOrReplaceCreditCardAsync(creditCardCreation, cancellationToken);
```
* creditCardCreation <CreditCardCreation>: The credit card creation information.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Removes a user's saved credit card.
```CSharp
await client.RemoveCreditCardAsync(nick, cancellationToken);
```
* nick <String>: Nickname of the credit card.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets a summary of previous orders.
```CSharp
List<Order> result = await client.GetOrderHistoryAsync(cancellationToken);
```
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Get's details about a specific order.
```CSharp
Order result = await client.GetOrderAsync(orderId, cancellationToken);
```
* orderId <String>: Id of the order to look up.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Changes a user's password.
```CSharp
await client.ChangePasswordAsync(newCredentials, cancellationToken);
```
* newCredentials <UserCredentials>: New password.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.

### Order Client ([API Reference](http://hackfood.ordr.in/docs/order))
#### Initialization
--------------------------------------------------
```CSharp
OrdrInOrderClient client = new OrdrInOrderClient(serviceBaseUrl, developerKey, email, hashedPassword);
```
* serviceBaseUrl <String>: See [Global to all clients](#Global to all clients).
* developerKey <String>: See [Global to all clients](#Global to all clients).
* email <String>: Email of the user account.
* hashedPassword <String>: Hash password of the user account.

#### Usage
--------------------------------------------------
##### Places an order. The order is of types based on UserBaseOrder: {GuestOrder, UserOrder, UserOrderWithSavedAddress, UserOrderWithSavedCreditCard, UserOrderWithSavedInfo}.
```CSharp
OrderInformation result = await client.CreateOrderAsync(order, cancellationToken);
```
* order <UserBaseOrder>: The order to placed.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.

### Restaurant Client ([API Reference](https://hackfood.ordr.in/docs/restaurant))
#### Initialization
--------------------------------------------------
```CSharp
OrdrInRestaurantClient client = new OrdrInRestaurantClient(serviceBaseUrl, developerKey);
```
* serviceBaseUrl <String>: See [Global to all clients](#Global to all clients).
* developerKey <String>: See [Global to all clients](#Global to all clients).

#### Usage
--------------------------------------------------
##### Gets a list of all restaurants that deliver to a given address.
```CSharp
List<Restaurant> result = await client.GetDeliveryListAsync(zipCode, city, address, cancellationToken);
```
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets if a particular restaurant will deliver to a given address at a given time.
```CSharp
List<Restaurant> result = await client.GetDeliveryListAsync(deliveryTime, zipCode, city, address, cancellationToken);
```
* deliveryTime <DateTime>: Delivery date and time.
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Checks if the given restaurant can deliver ASAP and for the given address.
```CSharp
RestaurantDeliveryCheck result = await client.GetDeliveryCheckAsync(restaurantId, zipCode, city, address, cancellationToken);
```
* restaurantId <String>: Ordr.in's unique restaurant identifier for the restaurant.
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Checks if the given restaurant can deliver for the given time and address.
```CSharp
RestaurantDeliveryCheck result = await client.GetDeliveryCheckAsync(restaurantId, deliveryTime, zipCode, city, address, cancellationToken);
```
* restaurantId <String>: Ordr.in's unique restaurant identifier for the restaurant.
* deliveryTime <DateTime>: Delivery date and time.
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Calculates all fees for a given subtotal and delivery address.
```CSharp
RestaurantFee result = await client.GetRestaurantFeeAsync(restaurantId, deliveryTime, subtotal, tip, zipCode, city, address, cancellationToken);
```
* restaurantId <String>: Ordr.in's unique restaurant identifier for the restaurant.
* deliveryTime <DateTime>: Delivery date and time.
* subtotal <String>: The cost of all items in the tray in dollars and cents.
* tip <String>: The tip in dollars and cents.
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Calculates all fees for a given subtotal and delivery address.
```CSharp
RestaurantFee result = await client.GetRestaurantFeeAsync(restaurantId, subtotal, tip, zipCode, city, address, cancellationToken);
```
* restaurantId <String>: Ordr.in's unique restaurant identifier for the restaurant.
* subtotal <String>: The cost of all items in the tray in dollars and cents.
* tip <String>: The tip in dollars and cents.
* zipCode <String>: The zip code part of the address.
* city <String>: Delivery location city.
* address <String>: Delivery location street address.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.


##### Gets restaurant details such as the restaurant's menu.
```CSharp
RestaurantDetails result = await client.GetRestaurantDetailsAsync(restaurantId, cancellationToken);
```
* restaurantId <String>: Ordr.in's unique restaurant identifier for the restaurant.
* cancellationToken <CancellationToken>: Token used to cancel the asynchronous call.
