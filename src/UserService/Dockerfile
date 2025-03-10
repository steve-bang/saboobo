# Use the official .NET 9.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy project files to take advantage of Docker layer caching
COPY ["src/UserService","UserService/"]
COPY ["src/Domain.Shared/", "Domain.Shared/"]
COPY ["src/MigrationService/", "MigrationService/"]


# Restore dependencies
RUN dotnet restore UserService/UserService.csproj

# Build the project in release mode
RUN dotnet publish UserService/UserService.csproj -c Release -o /app/out

# Use the official .NET 9.0 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port your application is running on
EXPOSE 8080

# Set the entry point to the built application
ENTRYPOINT ["dotnet", "UserService.dll"]

# Exec: 
#   Cd root project
#   docker buildx build --platform linux/amd64 -t mrstevebang/saboobo-user-api:1.3.3 -f src/UserService/Dockerfile .
#   docker push mrstevebang/saboobo-user-api:1.3.3