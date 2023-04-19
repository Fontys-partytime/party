#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Partytime.Party.Service/Partytime.Party.Service.csproj", "src/Partytime.Party.Service/"]
COPY ["src/Partytime.Party.Contracts/Partytime.Party.Contracts.csproj", "src/Partytime.Party.Contracts/"]
RUN dotnet restore "src/Partytime.Party.Service/Partytime.Party.Service.csproj"
COPY . .
WORKDIR "/src/src/Partytime.Party.Service"
RUN dotnet build "Partytime.Party.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Partytime.Party.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Partytime.Party.Service.dll"]