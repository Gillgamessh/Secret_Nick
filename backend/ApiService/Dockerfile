# Docker image ARGS
ARG BASE_IMAGE_REPO=mcr.microsoft.com
ARG BASE_IMAGE_BUILD=dotnet/sdk
ARG BASE_IMAGE_BUILD_TAG=9.0
ARG BASE_IMAGE_RUNTIME=dotnet/aspnet
ARG BASE_IMAGE_RUNTIME_TAG=9.0

# Setup Build Image
FROM ${BASE_IMAGE_REPO}/${BASE_IMAGE_BUILD}:${BASE_IMAGE_BUILD_TAG} AS build

# Build, Test and Publish ARGS
ARG VERSION_PREFIX=0.1.3
ARG VERSION_SUFFIX
ARG ENVIRONMENT=docker

# Build, Test and Publish ENVS
ENV DOTNET_ENVIRONMENT=${ENVIRONMENT}

WORKDIR /sln

# Dotnet Restore
COPY ./*.sln ./
COPY Source/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p Source/${file%.*}/ && mv $file Source/${file%.*}/; done
COPY Tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p Tests/${file%.*}/ && mv $file Tests/${file%.*}/; done

RUN dotnet restore -v minimal

# Bust Cache
ARG CACHE_BUST 1

# Copy Remaining Files
COPY . .

# Dotnet Build
RUN dotnet build --no-restore -c Release -v minimal -p:VersionPrefix=${VERSION_PREFIX} -p:VersionSuffix=${VERSION_SUFFIX}

FROM scratch AS coverage
COPY --from=test /sln/Tests/results/*.xml .

# Dotnet Publish
FROM build AS publish
ARG VERSION_PREFIX=0.1.3
ARG VERSION_SUFFIX
RUN dotnet publish ./Source/**/Api.csproj --no-restore -c Release -v quiet -o app -p:VersionPrefix=${VERSION_PREFIX} -p:VersionSuffix=${VERSION_SUFFIX}

# Runtime Image
FROM ${BASE_IMAGE_REPO}/${BASE_IMAGE_RUNTIME}:${BASE_IMAGE_RUNTIME_TAG} AS run
WORKDIR /
EXPOSE 8080
COPY --from=publish /sln/app .
ENTRYPOINT ["dotnet", "Epam.ItMarathon.ApiService.Api.dll"]
