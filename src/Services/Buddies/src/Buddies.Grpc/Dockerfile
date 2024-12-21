FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Buddies/src/Buddies.Grpc/Buddies.Grpc.csproj", "Services/Buddies/src/Buddies.Grpc/"]
RUN dotnet restore "Services/Buddies/src/Buddies.Grpc/Buddies.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Buddies/src/Buddies.Grpc"
RUN dotnet build "Buddies.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Buddies.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Buddies.Grpc.dll"]
