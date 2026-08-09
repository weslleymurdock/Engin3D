---
name: Server .NET MAUI Projects
description: Instructions for .NET Web api server projects.
applyTo: "src/server/**/*;"
---

# Server Projects

The server projects uses .NET 10 with web-api templates.

## Description

Each project in [server](./../../src/server/) directory is a microservice, except for [Engin3DGateway](./../../src/server/Engin3DGateway/Engin3DGateway.csproj) which is a api-gateway orchestrator built with Yarp nuget package (2.3.0). 

All web api projects are present in `docker-compose.yml` and `docker-compose.override.yml` , that also contains a shared database service (latest sqlserver database shared between microservices), mongodb for gridfs usage, and rabbitmq for microservice communication through production-consumption of messages between microservices.
