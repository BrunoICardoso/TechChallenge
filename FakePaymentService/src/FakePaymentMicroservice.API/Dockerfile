#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FakePaymentMicroservice.API/FakePaymentMicroservice.API.csproj", "src/FakePaymentMicroservice.API/"]
COPY ["src/FakePaymentMicroservice.Application/FakePaymentMicroservice.Application.csproj", "src/FakePaymentMicroservice.Application/"]
COPY ["src/FakePaymentMicroservice.Domain/FakePaymentMicroservice.Domain.csproj", "src/FakePaymentMicroservice.Domain/"]
COPY ["src/FakePaymentMicroservice.Infrastructure/FakePaymentMicroservice.Infrastructure.csproj", "src/FakePaymentMicroservice.Infrastructure/"]
COPY ["src/FakePaymentMicroservice.IOC/FakePaymentMicroservice.IOC.csproj", "src/FakePaymentMicroservice.IOC/"]
RUN dotnet restore "src/FakePaymentMicroservice.API/FakePaymentMicroservice.API.csproj"
COPY . .
WORKDIR "/src/src/FakePaymentMicroservice.API"
RUN dotnet build "FakePaymentMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakePaymentMicroservice.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakePaymentMicroservice.API.dll"]