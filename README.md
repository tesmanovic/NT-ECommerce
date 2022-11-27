# Ecommerce NulTien

## Requirements
Design and implement back-end solution for a fictive e-commerce application. The system 
should support following operations:
- add product to shopping cart
- list shopping cart content
- create order from shopping cart

All operations should be exposed through a HTTP API.

## Database Schema
![UML-Diagram](https://i.imgur.com/Gg6qaJv.png)

## How to run the project in local enviroment
- Clone solution repository using url: https://github.com/tesmanovic/NT-ECommerce.git
- Run command for creating and inserting data in database:
	- Package Manager Console tools: **Update-Database InitialCreate** 
	- .NET Core CLI: **dotnet ef database update InitialCreate**

## How to use API
- For an api overview and testing **Swagger** is included for development environment
- For using Postman, Postman colleciton is provided inside the root folder of soultion > ***NulTien Ecommerce Api.postman_collection.*** Inside of collection, default port value is set to **7244**. You can change it if you need to use other port.

## How to run tests
- Tests can be run by running the project ECommerceNulTienTests
