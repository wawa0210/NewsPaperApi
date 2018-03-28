FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY EmergencyMapCoreApi.sln ./
COPY WebApi/WebApi.csproj WebApi/
COPY EmergencyAccount/EmergencyAccount.csproj EmergencyAccount/
COPY CommonLib/CommonLib.csproj CommonLib/
COPY EmergencyBaseService/EmergencyBaseService.csproj EmergencyBaseService/
COPY EmergencyData/EmergencyData.csproj EmergencyData/
COPY PaperNewsService/PaperNewsService.csproj PaperNewsService/
COPY MagickNetService/MagickNetService.csproj MagickNetService/
COPY EmergencyEntity/EmergencyEntity.csproj EmergencyEntity/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]
