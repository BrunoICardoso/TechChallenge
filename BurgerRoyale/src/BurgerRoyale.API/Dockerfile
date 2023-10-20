#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BurgerRoyale.API/BurgerRoyale.API.csproj", "BurgerRoyale.API/"]
COPY ["BurgerRoyale.Application/BurgerRoyale.Application.csproj", "BurgerRoyale.Application/"]
COPY ["BurgerRoyale.Domain/BurgerRoyale.Domain.csproj", "BurgerRoyale.Domain/"]
COPY ["BurgerRoyale.Infrastructure/BurgerRoyale.Infrastructure.csproj", "BurgerRoyale.Infrastructure/"]
COPY ["BurgerRoyale.IOC/BurgerRoyale.IOC.csproj", "BurgerRoyale.IOC/"]
RUN dotnet restore "BurgerRoyale.API/BurgerRoyale.API.csproj"
COPY . .
WORKDIR "/src/BurgerRoyale.API"
RUN dotnet build "BurgerRoyale.API.csproj" -c Release -o /app/build
RUN dotnet tool restore

FROM build AS publish
RUN dotnet publish "BurgerRoyale.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BurgerRoyale.API.dll"]

