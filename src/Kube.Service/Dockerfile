# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=http://+:5000;https://+:5050
# ASP.NET Core'un sertifikayı tanıması için ortam değişkenlerini ayarla
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetcore.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123456
ENV TZ=Europe/Istanbul

# Sertifika için dizin oluştur
RUN mkdir /https
# Sertifika oluşturmak için OpenSSL'i kur
RUN apt-get update && apt-get install -y openssl && apt-get clean
# HTTPS için bir self-signed sertifika oluştur
RUN openssl req -x509 -nodes -days 365 \
    -subj "/CN=localhost" \
    -newkey rsa:2048 \
    -keyout /https/aspnetcore.key \
    -out /https/aspnetcore.crt && \
    openssl pkcs12 -export -out /https/aspnetcore.pfx \
    -inkey /https/aspnetcore.key \
    -in /https/aspnetcore.crt \
    -passout pass:123456

# Sertifikayı güvenilir hale getir
RUN cp /https/aspnetcore.crt /usr/local/share/ca-certificates/aspnetcore.crt && \
    update-ca-certificates

WORKDIR /app

EXPOSE 5000
EXPOSE 5050

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release

RUN echo "WORKDIR /app" \
    && TARGET_DIR=$(pwd) \
    && PARENT_DIR=$(dirname $TARGET_DIR) \
    && echo "Target folder is: $TARGET_DIR" \
    && echo "Full path to target folder: $(readlink -f $TARGET_DIR)" \
    && ls -la $TARGET_DIR \
    && echo "Parent folder is: $PARENT_DIR" \
    && echo "Full path to target folder: $(readlink -f $PARENT_DIR)" \
    && ls -la $PARENT_DIR


WORKDIR /src
RUN echo "WORKDIR /src" \
    && TARGET_DIR=$(pwd) \
    && PARENT_DIR=$(dirname $TARGET_DIR) \
    && echo "Target folder is: $TARGET_DIR" \
    && echo "Full path to target folder: $(readlink -f $TARGET_DIR)" \
    && ls -la $TARGET_DIR \
    && echo "Parent folder is: $PARENT_DIR" \
    && echo "Full path to target folder: $(readlink -f $PARENT_DIR)" \
    && ls -la $PARENT_DIR
COPY . .
RUN echo "WORKDIR /src" \
    && TARGET_DIR=$(pwd) \
    && PARENT_DIR=$(dirname $TARGET_DIR) \
    && echo "Target folder is: $TARGET_DIR" \
    && echo "Full path to target folder: $(readlink -f $TARGET_DIR)" \
    && ls -la $TARGET_DIR \
    && echo "Parent folder is: $PARENT_DIR" \
    && echo "Full path to target folder: $(readlink -f $PARENT_DIR)" \
    && ls -la $PARENT_DIR

WORKDIR /src/src
RUN echo "WORKDIR /src/src" \
    && TARGET_DIR=$(pwd) \
    && PARENT_DIR=$(dirname $TARGET_DIR) \
    && echo "Target folder is: $TARGET_DIR" \
    && echo "Full path to target folder: $(readlink -f $TARGET_DIR)" \
    && ls -la $TARGET_DIR \
    && echo "Parent folder is: $PARENT_DIR" \
    && echo "Full path to target folder: $(readlink -f $PARENT_DIR)" \
    && ls -la $PARENT_DIR

WORKDIR /src
# install nuget pakcages
RUN dotnet restore "src/Kube.Service/Kube.Service.csproj"
# build project
RUN dotnet build "src/Kube.Service/Kube.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Kube.Service/Kube.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kube.Service.dll"]