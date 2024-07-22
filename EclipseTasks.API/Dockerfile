#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EclipseTasks.Api/EclipseTasks.Api.csproj", "EclipseTasks.Api/"]
COPY ["EclipseTasks.Application/EclipseTasks.Application.csproj", "EclipseTasks.Application/"]
COPY ["EclipseTasks.Core/EclipseTasks.Core.csproj", "EclipseTasks.Core/"]
COPY ["EclipseTasks.Infrastructure/EclipseTasks.Infrastructure.csproj", "EclipseTasks.Infrastructure/"]
RUN dotnet restore "./EclipseTasks.Api/EclipseTasks.Api.csproj"
COPY . .
WORKDIR "/src/EclipseTasks.Api"
RUN dotnet build "./EclipseTasks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EclipseTasks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EclipseTasks.Api.dll"]