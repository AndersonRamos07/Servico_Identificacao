#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Identificacao.Api/Identificacao.Api.csproj", "Identificacao.Api/"]
COPY ["Identificacao.Dominio/Identificacao.Dominio.csproj", "Identificacao.Dominio/"]
COPY ["Identificacao.Infra/Identificacao.Infra.csproj", "Identificacao.Infra/"]
RUN dotnet restore "Identificacao.Api/Identificacao.Api.csproj"
COPY . .
WORKDIR "/src/Identificacao.Api"
RUN dotnet build "Identificacao.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identificacao.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identificacao.Api.dll"]