FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserService.Migrator/UserService.Migrator.csproj", "UserService.Migrator/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
RUN dotnet restore "UserService.Migrator/UserService.Migrator.csproj"
COPY . .
WORKDIR "/src/UserService.Migrator"
RUN dotnet build "UserService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Migrator.dll"]
