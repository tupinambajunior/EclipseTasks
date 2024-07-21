FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EclipseTasks.Api/EclipseTasks.Api.csproj", "EclipseTasks.Api/"]
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