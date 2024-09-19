FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY . .
CMD ["sh", "-c", "cd Auth.API && dotnet run"]
