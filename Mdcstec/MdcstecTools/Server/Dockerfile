#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mdcstec/MdcstecTools/Server/MdcstecTools.Server.csproj", "Mdcstec/MdcstecTools/Server/"]
COPY ["Mdcstec/MdcstecTools/Client/MdcstecTools.Client.csproj", "Mdcstec/MdcstecTools/Client/"]
COPY ["Mdcstec/MdcstecTools/Shared/MdcstecTools.Shared.csproj", "Mdcstec/MdcstecTools/Shared/"]
RUN dotnet restore "Mdcstec/MdcstecTools/Server/MdcstecTools.Server.csproj"
COPY . .
WORKDIR "/src/Mdcstec/MdcstecTools/Server"
RUN dotnet build "MdcstecTools.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MdcstecTools.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MdcstecTools.Server.dll"]