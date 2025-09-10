# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Render expects app to run on port 10000
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Copy published output
COPY --from=build /app/publish .

# Run your app
ENTRYPOINT ["dotnet", "web-apis.dll"]
