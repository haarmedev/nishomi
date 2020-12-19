#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Analystor.Nishomi.Admin/Analystor.Nishomi.Admin.csproj", "Services/Analystor.Nishomi.Admin/"]
COPY ["Analystor.Nishomi.Common/Analystor.Nishomi.Common.csproj", "Analystor.Nishomi.Common/"]
COPY ["Core/Analystor.Nishomi.Core/Analystor.Nishomi.Core.csproj", "Core/Analystor.Nishomi.Core/"]
COPY ["Core/Analystor.Nishomi.Persistence/Analystor.Nishomi.Persistence.csproj", "Core/Analystor.Nishomi.Persistence/"]
COPY ["Core/Analystor.Nishomi.Domain/Analystor.Nishomi.Domain.csproj", "Core/Analystor.Nishomi.Domain/"]
RUN dotnet restore "Services/Analystor.Nishomi.Admin/Analystor.Nishomi.Admin.csproj"
COPY . .
WORKDIR "/src/Services/Analystor.Nishomi.Admin"
RUN dotnet build "Analystor.Nishomi.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Analystor.Nishomi.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Analystor.Nishomi.Admin.dll"]