FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SwProjAdmin/SwProjAdmin.csproj", "SwProjAdmin/"]
RUN dotnet restore "SwProjAdmin/SwProjAdmin.csproj"
COPY . .
WORKDIR "/src/SwProjAdmin"
RUN dotnet build "SwProjAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwProjAdmin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwProjAdmin.dll"]
