#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BurgerRoyale.API/BurgerRoyale.API.csproj", "src/BurgerRoyale.API/"]
COPY ["src/BurgerRoyale.Application/BurgerRoyale.Application.csproj", "src/BurgerRoyale.Application/"]
COPY ["src/BurgerRoyale.Domain/BurgerRoyale.Domain.csproj", "src/BurgerRoyale.Domain/"]
COPY ["src/BurgerRoyale.Infrastructure/BurgerRoyale.Infrastructure.csproj", "src/BurgerRoyale.Infrastructure/"]
COPY ["src/BurgerRoyale.IOC/BurgerRoyale.IOC.csproj", "src/BurgerRoyale.IOC/"]
RUN dotnet restore "src/BurgerRoyale.API/BurgerRoyale.API.csproj"
COPY . .
WORKDIR "/src/src/BurgerRoyale.API"
RUN dotnet build "BurgerRoyale.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BurgerRoyale.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BurgerRoyale.API.dll"]