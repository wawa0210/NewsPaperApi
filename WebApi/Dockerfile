FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./

RUN dotnet publish -c release -r ubuntu.16.04-x64 -o out

# build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 2300

ENTRYPOINT ["dotnet", "WabApi.dll"]