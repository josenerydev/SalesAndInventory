FROM mcr.microsoft.com/mssql/server:2019-latest

# Switch to root user
USER root

# Install .NET Core SDK
RUN apt-get update \
    && apt-get install -y wget apt-transport-https software-properties-common \
    && wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && add-apt-repository universe \
    && apt-get update \
    && apt-get install -y dotnet-sdk-3.1

# Install Evolve CLI
RUN dotnet tool install --global Evolve.Tool --version 2.2.0

# Add dotnet tools to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

ARG PROJECT_DIR=/tmp/devdatabase
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY mssql/wait-for-it.sh ./
COPY mssql/entrypoint.sh ./
COPY mssql/setup.sh ./
COPY mssql/create_database.sql ./
COPY db/migrations ./db/migrations
CMD ["/bin/bash", "entrypoint.sh"]