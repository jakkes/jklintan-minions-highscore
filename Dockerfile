FROM mcr.microsoft.com/dotnet/core/sdk:3.0

WORKDIR /usr/src/app
COPY . .

dotnet build 