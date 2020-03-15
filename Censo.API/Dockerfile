#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Censo.API/Censo.API.csproj", "Censo.API/"]
COPY ["Censo.Domain/Censo.Domain.csproj", "Censo.API/"]
COPY ["Censo.Infra/Censo.Infra.csproj", "Censo.API/"]
RUN dotnet restore "Censo.API/Censo.API.csproj"
COPY . .
WORKDIR "/src/Censo.API"
RUN dotnet build "Censo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Censo.API.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 4321
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Censo.API.dll"]