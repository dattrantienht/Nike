#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#ENV ASPNETCORE_URLS=https://+:5006;http://+:5005
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Apps/Nike.Api/Nike.Api.csproj", "src/Apps/Nike.Api/"]
COPY ["src/Common/Nike.Infrastructure/Nike.Infrastructure.csproj", "src/Common/Nike.Api.Infrastructure/"]
COPY ["src/Common/Nike.Application/Nike.Application.csproj", "src/Common/Nike.Api.Application/"]
COPY ["src/Common/Nike.Domain/Nike.Domain.csproj", "src/Common/Nike.Api.Domain/"]
RUN dotnet restore "src/Apps/Nike.Api/Nike.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/Nike.Api"
RUN dotnet build "Nike.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nike.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nike.Api.dll"]