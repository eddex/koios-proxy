#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["KoiosProxy/KoiosProxy.csproj", "KoiosProxy/"]
RUN dotnet restore "KoiosProxy/KoiosProxy.csproj"
COPY . .
WORKDIR "/src/KoiosProxy"
RUN dotnet build "KoiosProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KoiosProxy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ALLOWED_ORIGINS=*
ENTRYPOINT ["dotnet", "KoiosProxy.dll"]