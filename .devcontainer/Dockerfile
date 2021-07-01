FROM ubuntu:20.10

ENV DEBIAN_FRONTEND=noninteractive
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

RUN apt-get update && \
    apt-get install -y wget software-properties-common sudo git

RUN wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    add-apt-repository universe && \
    apt-get update && \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y dotnet-sdk-5.0 && \
    mkdir -m 777 -p /usr/local/share/dotnet/sdk/NuGetFallbackFolder

RUN useradd -ms /bin/bash dev && \
    sudo adduser dev sudo && \
    echo "dev ALL=(ALL) NOPASSWD:ALL" | tee /etc/sudoers

ENV MSBUILDSINGLELOADCONTEXT=1

USER dev