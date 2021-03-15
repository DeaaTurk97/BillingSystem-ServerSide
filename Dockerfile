FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . ./
# Execute entry script which runs migrations and 'dotnet watch'
COPY docker-entrypoint.sh /usr/local/bin
RUN ls -la /usr/local/bin

RUN ln -s /usr/local/bin/docker-entrypoint.sh /
ENTRYPOINT ["docker-entrypoint.sh"]