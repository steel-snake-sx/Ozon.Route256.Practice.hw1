FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ozon.Route256.Practice.CustomerService1.csproj", "src/Ozon.Route256.Practice.CustomerService1/"]
RUN dotnet restore "src/Ozon.Route256.Practice.CustomerService1/Ozon.Route256.Practice.CustomerService1.csproj"
WORKDIR "/src/Ozon.Route256.Practice.CustomerService1"
COPY . .
RUN dotnet build "Ozon.Route256.Practice.CustomerService1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ozon.Route256.Practice.CustomerService1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ozon.Route256.Practice.CustomerService1.dll"]
