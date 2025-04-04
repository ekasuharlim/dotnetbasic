FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./publish /app
WORKDIR /app
ENTRYPOINT [ "dotnet", "FruitAPI.dll" ]