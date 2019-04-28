FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY InterfaceService/InterfaceService.csproj InterfaceService/
RUN dotnet restore InterfaceService/InterfaceService.csproj
COPY . .
WORKDIR /src/InterfaceService
RUN dotnet build InterfaceService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish InterfaceService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "InterfaceService.dll"]
