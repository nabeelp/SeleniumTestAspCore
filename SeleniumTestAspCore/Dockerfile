FROM selenium/standalone-chrome AS base
USER root

RUN apt-get update \
  && apt-get install -y unzip --no-install-recommends \
  && mkdir -p /defaulthome/hostingstart \ 
  && echo "root:Docker!" | chpasswd \
  && echo "cd /home" >> /etc/bash.bashrc \
  && apt update \
  && apt install -y --no-install-recommends openssh-server curl \
  && mkdir -p /home/LogFiles/ \
  && rm -rf /var/lib/apt/lists/*

# Install ASP.NET Core
ENV ASPNETCORE_VERSION 2.1.10

RUN curl -SL --output aspnetcore.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/aspnetcore/Runtime/$ASPNETCORE_VERSION/aspnetcore-runtime-$ASPNETCORE_VERSION-linux-x64.tar.gz \
    && aspnetcore_sha512='f812ac2992f1f16f44df719c434080c8d7920ce4881282d5a678218606b039e0eeac43574034c050e11ca5a1fe3172c2d5b43c2f3d08c3595a20164d8e979fa2' \
    && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -zxf aspnetcore.tar.gz -C /usr/share/dotnet \
    && rm aspnetcore.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SeleniumTestAspCore/SeleniumTestAspCore.csproj", "SeleniumTestAspCore/"]
RUN dotnet restore "SeleniumTestAspCore/SeleniumTestAspCore.csproj"
COPY . .
WORKDIR "/src/SeleniumTestAspCore"
RUN dotnet build "SeleniumTestAspCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SeleniumTestAspCore.csproj" -c Release -o /app

FROM base AS final
USER root
COPY SeleniumTestAspCore/init_container.sh /bin/
COPY SeleniumTestAspCore/hostingstart.html /home/site/wwwroot/
COPY SeleniumTestAspCore/sshd_config /etc/ssh/

USER seluser

WORKDIR /home/site/wwwroot/
EXPOSE 2222 8080
ENV PORT 8080
ENV WEBSITE_ROLE_INSTANCE_ID localRoleInstance
ENV WEBSITE_INSTANCE_ID localInstance
ENV PATH ${PATH}:/home/site/wwwroot
ENV ASPNETCORE_URLS=
COPY --from=publish /app .
ENTRYPOINT ["/bin/init_container.sh"]