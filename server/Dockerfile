#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app/server
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["server/server.csproj", "server/"]
COPY ["logic/logic.csproj", "logic/"]
RUN dotnet restore "server/server.csproj"
COPY . .
WORKDIR "/src/server"
RUN dotnet build "server.csproj" -c Release -o /app/server/build

FROM build AS publish
RUN dotnet publish "server.csproj" -c Release -o /app/server/publish

FROM base AS final
WORKDIR /app/server
COPY --from=publish /app/server/publish .
ENTRYPOINT ["dotnet", "server.dll"]