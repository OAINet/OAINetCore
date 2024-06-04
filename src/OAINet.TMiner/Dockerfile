FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OAINet.TMiner/OAINet.TMiner.csproj", "src/OAINet.TMiner/"]
RUN dotnet restore "src/OAINet.TMiner/OAINet.TMiner.csproj"
COPY . .
WORKDIR "/src/src/OAINet.TMiner"
RUN dotnet build "OAINet.TMiner.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OAINet.TMiner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAINet.TMiner.dll"]
