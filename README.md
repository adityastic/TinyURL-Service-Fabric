# TinyURL

![.NET CI](https://github.com/adityastic/TinyURL-Service-Fabric/workflows/.NET%20CI/badge.svg) [![Codacy Badge](https://app.codacy.com/project/badge/Grade/ed8da28cfb53439e928c4405b37e5a7f)](https://www.codacy.com/manual/adityastic/TinyURL-Service-Fabric?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=adityastic/TinyURL-Service-Fabric&amp;utm_campaign=Badge_Grade)

This is an implimentation of Tiny URL using [Azure Service Fabric](https://azure.microsoft.com/en-us/services/service-fabric/)

## Deploy it on your Azure
[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

## Pre-Requisites
1. [Visual Studio](https://visualstudio.microsoft.com/vs/)
2. [Service Fabric Development Environment](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started)

## Setup Environment
1. ```git clone https://github.com/adityastic/TinyURL-Service-Fabric``` to your desired location. 
The Directory should look something like this:
![image](https://user-images.githubusercontent.com/11988517/87421735-56008c00-c5cf-11ea-849a-79d0eaa1b91f.png)
2. Double-CLick on TinyURL.sln or Open this folder using [Visual Studio](https://visualstudio.microsoft.com/vs/).

## Running Tiny URL on local dev cluster
> Note: Make sure you have a minimum 5 Node Service Fabric Cluster on your machine. Checkout [Setting up a Development Environment](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started).
1. `Right-Click` on the TinyURL Project.
2. Select `Debug`.
3. Select `Start new instance`.

Image for reference:
![image](https://user-images.githubusercontent.com/11988517/87422175-171f0600-c5d0-11ea-8904-66b236992baf.png)

## License
This project is Free and Open Source software. The project is licensed under the [MIT](LICENSE).
