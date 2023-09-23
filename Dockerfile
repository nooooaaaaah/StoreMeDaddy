# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy everything and build the application
COPY StoreMeDaddy ./
RUN dotnet publish -c Debug -o out

# Use the .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copy the build app from the build-env container
COPY --from=build-env /app/out .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "StoreMeDaddy.dll"]