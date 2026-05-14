# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore ./FabricaDeSorrisos.Web/FabricaDeSorrisos.Web.csproj
RUN dotnet publish ./FabricaDeSorrisos.Web/FabricaDeSorrisos.Web.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:3000

EXPOSE 3000

ENTRYPOINT ["dotnet", "FabricaDeSorrisos.Web.dll"]
