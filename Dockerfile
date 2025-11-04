# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "Portfolio.dll"]


# ========== Build Stage ==========
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src

# # Copy csproj and restore first (leverages Docker cache)
# COPY *.csproj ./
# RUN dotnet restore

# # Copy everything else & publish
# COPY . .
# RUN dotnet publish -c Release -o /app/out

# # ========== Runtime Stage ==========
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app

# COPY --from=build /app/out .

# # Expose port (Render listens on $PORT, defaults to 8080)
# EXPOSE 8080
# ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

# ENTRYPOINT ["dotnet", "Portfolio.dll"]
