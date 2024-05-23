FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OAINet.Node/OAINet.Node.csproj", "src/OAINet.Node/"]
RUN dotnet restore "src/OAINet.Node/OAINet.Node.csproj"
COPY . .
WORKDIR "/src/src/OAINet.Node"
RUN dotnet build "OAINet.Node.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OAINet.Node.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAINet.Node.dll"]
