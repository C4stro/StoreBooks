# Store Books
## AWS + Github Action + .NET (In Memory + EF Core + DDD + Swagger)

###### Current project link: http://storebooks-env.eba-nrkgk92y.us-east-2.elasticbeanstalk.com/ ######

[![Deploy AWS.](https://github.com/C4stro/StoreBooks/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/C4stro/StoreBooks/actions/workflows/dotnet.yml)


Project for the purchase and registration of books with the possibility of converting the value into EUR, BRL, GBP and USD.
###### Using the API of https://exchangeratesapi.io/ ######
##
# Features

- Book Registration
- Book Listing
- Book registration update
- Book Purchase Checkout
- List of purchases made

## Tech
It was used in this project

- [NET] - WebAPI and backend made in DDD modeling!
- [EF Core + In Memory] - For database use/abstraction
- [Swagger] - For easy use of API endpoints
- [DDD] - For better application structuring/modeling
- [AWS + Github Action] - To deploy I continue from the main branch

## EndPoints Available
The endpoints below are released for use.

```sh
[HTTP Return: 200 Ok, 201 Created, 204 NoContent and 400 BadRequest]

##Books
GET  ​/api​/v1​/Books​/All​/{currency} (List of books valued in the chosen currency)
POST ​/api​/v1​/Books​/Create (Create a new book)
GET  ​/api​/v1​/Books​/ID (View a book from its ID)
GET  ​/api​/v1​/Books​/Author​/{author} (View a book from its ID author)
GET  ​/api​/v1​/Books​/Title​/{title} (View a book from its title)
PUT  ​/api​/v1​/Books​/Update (update a book)

##Shopping
GET  ​/api​/v1​/Shopping​/All (View all purchases made)
POST ​/api​/v1​/Shopping​/Buy​/{currency} (Make a new purchase)
```
Permitted values ​​for currency are:
"EUR" = EURO, 
"GBP" = Pounds Sterling, 
"USD" = American dollar
"BRL" = Brazilian real

## Installation

Store Books requires [.NET](https://nodejs.org/) and [exchangeRates](https://exchangeratesapi.io/) to run.


```sh
dotnet restore
dotnet build
dotnet publish
```


## API Extern

Using exchangerates in the free version for converting values.

| API Extern | 
| ------ |
| exchange.io |


## Development

```sh
http://localhost:32762/
```

## License

MIT

**Free Software, Hell Yeah!**
