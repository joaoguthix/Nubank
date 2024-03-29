#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:8000;http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAPIs/WebAPIs.csproj", "WebAPIs/"]
COPY ["Infrastucture/Infrastucture.csproj", "Infrastucture/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "WebAPIs/WebAPIs.csproj"
COPY . .
WORKDIR "/src/WebAPIs"
RUN dotnet build "WebAPIs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPIs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPIs.dll"]