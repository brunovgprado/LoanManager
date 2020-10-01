FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY LoanManager/*.sln ./
COPY LoanManager/LoanManager.Api/*.csproj ./LoanManager/LoanManager.Api/
COPY LoanManager/LoanManager.Domain/*.csproj ./LoanManager/LoanManager.Domain/
COPY LoanManager/LoanManager.Application/*.csproj ./LoanManager/LoanManager.Application/
COPY LoanManager/LoanManager.Infrastructure/*.csproj ./LoanManager/LoanManager.Infrastructure/
COPY LoanManager/LoanManager.IoC/*.csproj ./LoanManager/LoanManager.IoC/
COPY LoanManager/LoanManager.Tests/*.csproj ./LoanManager/LoanManager.Tests/

RUN dotnet restore LoanManager/LoanManager.Api/LoanManager.Api.csproj

COPY LoanManager/LoanManager.Api/. ./LoanManager/LoanManager.Api/
COPY LoanManager/LoanManager.Domain/. ./LoanManager/LoanManager.Domain/
COPY LoanManager/LoanManager.Application/. ./LoanManager/LoanManager.Application/
COPY LoanManager/LoanManager.Infrastructure/. ./LoanManager/LoanManager.Infrastructure/
COPY LoanManager/LoanManager.IoC/. ./LoanManager/LoanManager.IoC/

WORKDIR /app/LoanManager
RUN dotnet publish LoanManager.Api/LoanManager.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/LoanManager/out ./
ENTRYPOINT ["dotnet", "LoanManager.Api.dll"]