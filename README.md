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

### User Client ([API Reference](http://hackfood.ordr.in/docs/order#order_user))
#### Initialization
```CSharp
OrdrInUserClient client = new OrdrInUserClient(serviceAddress, developerKey, email, password);
```
* serviceAddress: See [Global to all clients](#Global to all clients)
* developerKey: See [Global to all clients](#Global to all clients)

In progress..

