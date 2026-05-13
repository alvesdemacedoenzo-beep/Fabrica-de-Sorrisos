FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy all project files for restore
COPY FabricaDeSorrisos.Domain/FabricaDeSorrisos.Domain.csproj FabricaDeSorrisos.Domain/
COPY FabricaDeSorrisos.Application/FabricaDeSorrisos.Application.csproj FabricaDeSorrisos.Application/
COPY FabricaDeSorrisos.Infrastructure/FabricaDeSorrisos.Infrastructure.csproj FabricaDeSorrisos.Infrastructure/
COPY FabricaDeSorrisos.Api/FabricaDeSorrisos.Api.csproj FabricaDeSorrisos.Api/

RUN dotnet restore FabricaDeSorrisos.Api/FabricaDeSorrisos.Api.csproj

# Copy everything and publish
COPY . .
RUN dotnet publish FabricaDeSorrisos.Api/FabricaDeSorrisos.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-3000}
ENTRYPOINT ["dotnet", "FabricaDeSorrisos.Api.dll"]
