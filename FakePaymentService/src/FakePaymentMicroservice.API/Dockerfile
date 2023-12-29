#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FakePaymentService.API/FakePaymentService.API.csproj", "src/FakePaymentService.API/"]
COPY ["src/FakePaymentService.Application/FakePaymentService.Application.csproj", "src/FakePaymentService.Application/"]
COPY ["src/FakePaymentService.Domain/FakePaymentService.Domain.csproj", "src/FakePaymentService.Domain/"]
COPY ["src/FakePaymentService.Infrastructure/FakePaymentService.Infrastructure.csproj", "src/FakePaymentService.Infrastructure/"]
COPY ["src/FakePaymentService.IOC/FakePaymentService.IOC.csproj", "src/FakePaymentService.IOC/"]
RUN dotnet restore "src/FakePaymentService.API/FakePaymentService.API.csproj"
COPY . .
WORKDIR "/src/src/FakePaymentService.API"
RUN dotnet build "FakePaymentService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakePaymentService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakePaymentService.API.dll"]