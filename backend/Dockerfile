FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY backend/backend.sln . 

COPY backend/ .

COPY .env .

RUN dotnet restore backend.sln

RUN dotnet build backend.sln -c Release

RUN dotnet publish backend.API/backend.API.csproj -c Release -o /out

# Финальный этап
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копируем опубликованные артефакты
COPY --from=build /out .

# Явно копируем файл .env из этапа сборки
COPY --from=build /app/.env .

ENTRYPOINT ["dotnet", "backend.API.dll"]
