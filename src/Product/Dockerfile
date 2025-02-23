# Use the official .NET 9.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy project files to take advantage of Docker layer caching
COPY ["src/Product/Api/","Product/Api/"]
COPY ["src/Product/Application/","Product/Application/"]
COPY ["src/Product/Domain/","Product/Domain/"]
COPY ["src/Product/Infrastructure/","Product/Infrastructure/"]
COPY ["src/Product/Constract/","Product/Constract/"]
COPY ["src/Domain.Shared/", "Domain.Shared/"]
COPY ["src/MigrationService/", "MigrationService/"]
COPY ["src/AppHost.Shared/", "AppHost.Shared/"]


# Restore dependencies
RUN dotnet restore Product/Api/Product.Api.csproj

# Build the project in release mode
RUN dotnet publish Product/Api/Product.Api.csproj -c Release -o /app/out

# Use the official .NET 9.0 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port your application is running on
EXPOSE 5154

# Set the entry point to the built application
ENTRYPOINT ["dotnet", "Product.Api.dll"]

# Exec: 
#   Cd root project
#   docker buildx build --platform linux/amd64 -t mrstevebang/saboobo-product-api:1.0.6 -f src/Product/Dockerfile .
#   docker push mrstevebang/saboobo-product-api:1.0.6