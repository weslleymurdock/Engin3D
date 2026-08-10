# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/server/metadata/Engin3D.Metadata/Engin3D.Metadata.csproj", "src/server/metadata/Engin3D.Metadata/"]
COPY ["src/server/metadata/Engin3D.Metadata.Composition/Engin3D.Metadata.Composition.csproj", "src/server/metadata/Engin3D.Metadata.Composition/"]
COPY ["src/server/metadata/Engin3D.Metadata.Application/Engin3D.Metadata.Application.csproj", "src/server/metadata/Engin3D.Metadata.Application/"]
COPY ["src/server/metadata/Engin3D.Metadata.Domain/Engin3D.Metadata.Domain.csproj", "src/server/metadata/Engin3D.Metadata.Domain/"]
COPY ["src/server/metadata/Engin3D.Metadata.Infrastructure/Engin3D.Metadata.Infrastructure.csproj", "src/server/metadata/Engin3D.Metadata.Infrastructure/"]
RUN dotnet restore "./src/server/metadata/Engin3D.Metadata/Engin3D.Metadata.csproj"
COPY . .
WORKDIR "/src/src/server/metadata/Engin3D.Metadata"
RUN dotnet build "./Engin3D.Metadata.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Engin3D.Metadata.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Engin3D.Metadata.dll"]