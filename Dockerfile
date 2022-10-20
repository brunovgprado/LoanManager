FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV ASPNETCORE_ENVIRONMENT="Development"

WORKDIR /app
COPY src ./

RUN dotnet restore LoanManager.Api/LoanManager.Api.csproj
RUN dotnet publish LoanManager.Api/LoanManager.Api.csproj -c Release -o out

FROM pull mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "LoanManager.Api.dll"]