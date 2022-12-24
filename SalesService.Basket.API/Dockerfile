#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalesService.Basket.API/SalesService.Basket.API.csproj", "SalesService.Basket.API/"]
RUN dotnet restore "SalesService.Basket.API/SalesService.Basket.API.csproj"
COPY . .
WORKDIR "/src/SalesService.Basket.API"
RUN dotnet build "SalesService.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesService.Basket.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesService.Basket.API.dll"]