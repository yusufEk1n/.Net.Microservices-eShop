FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Web/ClientApp/ClientApp.csproj", "Web/ClientApp/"]
RUN dotnet restore "Web/ClientApp/ClientApp.csproj"
COPY . .
WORKDIR "/src/Web/ClientApp"
RUN dotnet build "ClientApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientApp.dll"]