FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BakeryShop.Api/BakeryShop.Api.csproj", "src/BakeryShop.Api/"]
COPY ["src/BakeryShop.Infrastructure/BakeryShop.Infrastructure.csproj", "src/BakeryShop.Infrastructure/"]
COPY ["src/BakeryShop.Application/BakeryShop.Application.csproj", "src/BakeryShop.Application/"]
COPY ["src/BakeryShop.Domain/BakeryShop.Domain.csproj", "src/BakeryShop.Domain/"]
RUN dotnet restore "src/BakeryShop.Api/BakeryShop.Api.csproj"
COPY . .
WORKDIR "/src/src/BakeryShop.Api"
RUN dotnet build "BakeryShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BakeryShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BakeryShop.Api.dll"]
