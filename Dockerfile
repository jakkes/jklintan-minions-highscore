FROM mcr.microsoft.com/dotnet/core/sdk:3.0

WORKDIR /usr/src/app
COPY . .

RUN dotnet build -c Release -o build

CMD [ "dotnet" "./build/jklintan-minions.dll"]