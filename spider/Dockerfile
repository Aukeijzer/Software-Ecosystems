# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /spider
COPY spider/*.csproj .
RUN dotnet restore
COPY spider .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 7167 5205
ENTRYPOINT ["dotnet", "spider.dll"]