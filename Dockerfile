FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS build

ENV ASPNETCORE_ENVIRONMENT="Development"

WORKDIR /app
COPY src ./

RUN dotnet restore LoanManager.Api/LoanManager.Api.csproj
RUN dotnet publish LoanManager.Api/LoanManager.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:6.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "LoanManager.Api.dll"]