FROM mcr.microsoft.com/dotnet/core/sdk:3.0

WORKDIR /usr/src/app
COPY . .

RUN dotnet build -c Release -o build

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

CMD ["dotnet", "./build/jklintan-minions.dll"]