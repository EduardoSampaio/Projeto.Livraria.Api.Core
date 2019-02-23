FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 9090

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Projeto.Livaria.Api/Projeto.Livaria.Api.csproj", "Projeto.Livaria.Api/"]
COPY ["Projeto.Livraria.Dados/Projeto.Livraria.Dados.csproj", "Projeto.Livraria.Dados/"]
COPY ["Projeto.Livraria.Entidades/Projeto.Livraria.Entidades.csproj", "Projeto.Livraria.Entidades/"]
RUN dotnet restore "Projeto.Livaria.Api/Projeto.Livaria.Api.csproj"
COPY . .
WORKDIR "/src/Projeto.Livaria.Api"
RUN dotnet build "Projeto.Livaria.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Projeto.Livaria.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Projeto.Livaria.Api.dll"]