[![Build Status Travis](https://travis-ci.com/brunovitorprado/LoanManager.svg?branch=master)](https://travis-ci.org/brunovitorprado/LoanManager)
[![Build Status](https://hortohome.visualstudio.com/LoanManager/_apis/build/status/brunovitorprado.LoanManager?branchName=master)](https://hortohome.visualstudio.com/LoanManager/_build/latest?definitionId=2&branchName=master)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=bugs)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=code_smells)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=coverage)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=ncloc)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=security_rating)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=alert_status)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=brunovitorprado_LoanManager&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=brunovitorprado_LoanManager)

# :video_game: :minidisc: LoanManager
A back-end solution for manage your game midia loans

## :dart: Objectives
Loan Manager is a recreative project to practice acquired knowledgements and learn with it in real life

## :memo: Features

- __Game registration__. You can registrate your game midias one by one individually, persisting information like Title, Description, Genre and Platform (Xbox, Playstation, PC, etc.).
- __Friend registration__. You can registrate your friends, persisting Name and Phone number. You will use that data to register your media game loans.
- __Loan registration__. With this feature, you are able to registrate your game loans, so you can loan a game midia to a friend and manage that loan with this api. You can also close a loan, search loans by friend name or game id.

## :policewoman: Safety

All endpoints on Loan Manager are protected by authentication, so all requisitions need to have a Bearer token in the header.   
To make the authentication process more easy, Loan Manager have an integrated authentication service that uses JWT and provides endpoints to signup and signin.

## :running_woman: How to run this project

Loan manager is a Restfull API that uses a docker based environment with a docker-compose file, so you just need docker running on your computer.   
So assuming you have docker running, you can push the commands below into your prompt terminal.

```
$ docker-compose build
```
```
$ docker-compose up
```
So voilà! docker will up the API and the PostgreSql server, create the database, create and populate the tables and make the Loan manager API accessible at localhost:8000.  
You can look at http://localhost:8000/docs for endpoints description and do tests with swagger.  
You can check application health at http://localhost:8000/health.

## 🧰: Project dependencies
- .Net 6;
- Dapper;
- Swashbuckle (Swagger);
- Npgsql;
- ApplicationInsights.

## :construction_worker: Work todo
You can contribute if you want :grin:

- Features must be segregated in use cases;
- keys mus be placed on secrets;
- Coupling must be reduced to minimum;
- Some project structural issues must be fixed;
- Unit tests coverage must be increased;
- Code smells identified by sonar cloud must be fixed;
- Load test coverage must be introduced.

## :rocket: Demo   

[DEMO](https://loanmanager-api-sandbox.azurewebsites.net/docs) 
