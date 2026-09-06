# build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY *.slnx .
COPY Core/*.csproj ./Core/
COPY Application/*.csproj ./Application/
COPY Data/*.csproj ./Data/
COPY WebAPI/*.csproj ./WebAPI/
COPY Test/*.csproj ./Test/

RUN dotnet restore

COPY . .

WORKDIR WebAPI
RUN dotnet publish -c Release -o /app/publish

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]
