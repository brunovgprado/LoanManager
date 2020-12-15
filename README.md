[![Build Status Travis](https://travis-ci.com/brunovitorprado/LoanManager.svg?branch=release/1.0.0)](https://travis-ci.org/brunovitorprado/LoanManager)
[![Build Status](https://hortohome.visualstudio.com/LoanManager/_apis/build/status/brunovitorprado.LoanManager?branchName=release/1.0.0)](https://hortohome.visualstudio.com/LoanManager/_build/latest?definitionId=2&branchName=release/1.0.0)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=bugs)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=code_smells)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=coverage)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=ncloc)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=security_rating)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=alert_status)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=hortohome_LoanManager&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=hortohome_LoanManager)

# LoanManager
Solution for manage your game midia loans  

## Features

- __Game registration__. You can registrate your game midias one by one individually, recording information like Title, Description, Genre and Platform.
- __Friend registration__. You can registrate your friends, recording Name and Phone number. You will use that data to register your media game loans.
- __Loan registration__. With this feature, you are able to registrate your game loans, so you can loan a game midia to a friend and manage that loan. You can also end a loan, search loans by friend name and search loans by game identification id.

## Safety

All endpoints on Loan Manager are protected by authentication, so all requisitions need to have a Bearer token in the header.   
To make the authentication process more easy, Loan Manager have an integrated authentication service that uses JWT and provides endpoints to signup and signin.

## How to run the project

Loan manager is a Restfull API project that uses a docker based environment with a docker-compose file, so you just need docker running on your computer.   
So assuming you have docker running, you can push the commands below into your prompt terminal.

```
$ docker-compose build
```
```
$ docker-compose up
```
So voilà! docker will up the API and the PostgreSql server, create the database, create and populate the tables and make the Loan manager API accessible at localhost:8000.  
You can look at http://localhost:8000/docs for endpoints description and make tests with swagger.
