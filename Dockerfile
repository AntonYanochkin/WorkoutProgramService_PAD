##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8082
#EXPOSE 8083
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["WorkoutProgramService.csproj", "."]
#RUN dotnet restore "./WorkoutProgramService.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./WorkoutProgramService.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./WorkoutProgramService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WorkoutProgramService.dll"]
# Use the official .NET image as the base image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the project file and restore any dependencies
COPY ["WorkoutProgramService.csproj", "."]
RUN dotnet restore "WorkoutProgramService.csproj"
# Copy the rest of the source code
COPY . . 
WORKDIR "/src"
# Build the project
RUN dotnet build "WorkoutProgramService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application for release
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkoutProgramService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the base image to create the final image with the application
FROM base AS final
WORKDIR /app
# Copy the published output to the final image
COPY --from=publish /app/publish .
# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "WorkoutProgramService.dll"]
