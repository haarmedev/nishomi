#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Analystor.Nishomi.Api/Analystor.Nishomi.Api.csproj", "Services/Analystor.Nishomi.Api/"]
COPY ["Analystor.Nishomi.Common/Analystor.Nishomi.Common.csproj", "Analystor.Nishomi.Common/"]
COPY ["Core/Analystor.Nishomi.Core/Analystor.Nishomi.Core.csproj", "Core/Analystor.Nishomi.Core/"]
COPY ["Core/Analystor.Nishomi.Persistence/Analystor.Nishomi.Persistence.csproj", "Core/Analystor.Nishomi.Persistence/"]
COPY ["Core/Analystor.Nishomi.Domain/Analystor.Nishomi.Domain.csproj", "Core/Analystor.Nishomi.Domain/"]
RUN dotnet restore "Services/Analystor.Nishomi.Api/Analystor.Nishomi.Api.csproj"
COPY . .
WORKDIR "/src/Services/Analystor.Nishomi.Api"
RUN dotnet build "Analystor.Nishomi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Analystor.Nishomi.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Analystor.Nishomi.Api.dll"]