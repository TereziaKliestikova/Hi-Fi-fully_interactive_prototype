# Use the .NET SDK image as build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Expose the necessary ports
EXPOSE 7179
EXPOSE 443

COPY ./*csproj .

RUN dotnet restore --verbosity detailed

COPY . ./

# Build and publish
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL org.opencontainers.image.source https://github.com/fiit-hipa/hipa-be

# Set the working directory
WORKDIR /app

# Copy the published output from the build environment
COPY --from=build-env /app/out .

# Copy Assets and Email directories
COPY --from=build-env /app/assets /app/assets
COPY --from=build-env /app/Email/Templates /app/Email/Templates

ENV ASPNETCORE_HTTP_HOST="http://localhost"
ENV ASPNETCORE_HTTP_PORT=81
# Signals if connection to BE is https for the purpose of email confirmations
# (BE can run using HTTP but reverse proxy is going through HTTPS)
ENV INGRESS_IS_HTTPS=false

ENV DB_HOST=hipa_db
ENV DB_PORT=5432
ENV DB=postgres
ENV DB_USERNAME=postgres
ENV DB_PASSWORD=postgres

ENV FRONTEND_HOST=http://localhost
ENV FRONTEND_PORT=80
ENV ICONS_PATH="/app/assets/icons/"
ENV BODY_SYSTEM_ICONS_PATH="/app/assets/bodySys/"

ENV EMAIL_HOST=smtp.gmail.com
ENV EMAIL_PORT=587
ENV EMAIL_USERNAME=hipa.buddy@gmail.com
ENV EMAIL_PASSWORD="hoot fvmw tgte debc"

# Set the entry point for the container
ENTRYPOINT ["dotnet", "HIPA-BE.dll"]
