FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . .

RUN dotnet publish src/UniqueArticles/ -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./

ENV ASPNETCORE_URLS http://0.0.0.0:5000/
EXPOSE 5000

ENTRYPOINT ["dotnet", "UniqueArticles.dll"]