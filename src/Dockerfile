FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG BUILD_CONFIGURATION=Release
ARG Nuget_CustomFeedPassword

EXPOSE 8080
EXPOSE 8081

COPY ./zScalerRootCertificates/GWLE*.crt /usr/local/share/ca-certificates/

RUN update-ca-certificates

WORKDIR /src

COPY ./CLUK.Skeleton.API ./CLUK.Skeleton.API

WORKDIR "/src/CLUK.Skeleton.API"

RUN dotnet restore "./CLUK.Skeleton.API.csproj"
RUN dotnet build "./CLUK.Skeleton.API.csproj" --no-restore  -c %BUILD_CONFIGURATION% -o /app/build
RUN dotnet publish "./CLUK.Skeleton.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

WORKDIR /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CLUK.Skeleton.API.dll"]