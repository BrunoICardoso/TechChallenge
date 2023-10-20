#!/bin/sh
set -e

# Aguarde até que o SQL Server esteja pronto
until dotnet ef database update --project BurgerRoyale.Infrastructure/BurgerRoyale.Infrastructure.csproj; do
>&2 echo "SQL Server is starting up"
sleep 1
done

# Inicie o aplicativo
exec dotnet BurgerRoyale.API.dll