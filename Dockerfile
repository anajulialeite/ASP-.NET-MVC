FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
COPY .env /app/.env
COPY scripts/ /app/scripts/

# Instalar SQL Server CLI tools para scripts de inicialização
RUN apt-get update && apt-get install -y gnupg curl && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Set environment variables
ENV ASPNETCORE_URLS=http://+:7064
ENV PATH="${PATH}:/opt/mssql-tools/bin"
ENV INIT_DATABASE=true
 
# Make the scripts executable
RUN chmod +x /app/scripts/*.sh

# Create directory for DataProtection keys
RUN mkdir -p /root/.aspnet/DataProtection-Keys

# Expose port 7064
EXPOSE 7064

ENTRYPOINT ["/app/scripts/entrypoint.sh"]