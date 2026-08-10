---
name: Engin3D Backend Services
description: Instructions for self-contained DDD REST microservices and the YARP gateway.
applyTo: "src/server/**/*"
---

# Backend Projects

Backend services target .NET 10. Each microservice is independently responsible for its domain and persistence boundary.

## Microservice architecture

Except for the Gateway, each service follows:

```text
<service> (Presentation)
<service>.Application
<service>.Composition
<service>.Domain
<service>.Infrastructure
```

References are Presentation -> Application, Composition; Application -> Domain; Composition -> Application, Infrastructure; Infrastructure -> Application; Domain -> none.

Application contains abstractions and use cases. Infrastructure contains implementations unless intentionally application- or presentation-specific. Composition is responsible for assembling dependency injection. Organize each layer by feature aggregate rather than global cross-feature folders.

## Web API composition

Each microservice has a root DI composition implementation. Keep `Program.cs` declarative by composing service registration through `WebApplicationBuilder` extensions, configuring the HTTP pipeline through `WebApplication` extensions, and finishing with `RunAsync()`.

## Gateway

`Engin3DGateway` is a single-project gateway and does not use the microservice DDD split. It uses YARP reverse proxy as the public ingress and routes requests to Auth, Identity, Metadata, Project, Storage, and future services.

The Gateway is a technical boundary, not a business mediator. Business rules remain in the target microservice. Do not add cross-service domain orchestration to YARP configuration merely to compose business behavior.

## Authentication

`Engin3DAuth` is the authentication authority and JWT/JWKS issuer. Other services validate bearer tokens using published JWKS. The Gateway is the external ingress for Auth, while JWKS retrieval remains configurable so an environment can use the Gateway or direct internal service addressing.

Do not use opaque Identity API tokens. Registration, confirmation, authentication, token issuance, and related Auth behavior must remain compatible with the JWT/JWKS architecture.

## Service boundaries

- Auth owns authentication and signing/JWKS.
- Identity owns user/profile data.
- Metadata owns relational metadata for assets used by the current authenticated user's Engin3D session.
- Storage owns binary asset persistence through MongoDB/GridFS.
- Project owns project generation, source generation, build, test, debug, publish, and Git synchronization.
- Gateway owns public routing and gateway-level technical concerns.

Do not access another service's database tables directly. Shared SQL Server is an infrastructure choice; ownership of schemas and tables remains service-specific.

## Infrastructure stack

The initial backend stack is SQL Server, MongoDB/GridFS, Mosquitto, and a Git server. Qdrant is not a source-of-truth database and must not be introduced until a feature requires semantic/vector indexing.

Mosquitto is an asynchronous notification channel for Project operations. It must not carry source code or assets and must not be treated as durable state.

Git is the source of truth for the Project specification repository and generated-source repository. REST starts operations; MQTT signals completion/failure; Git provides durable source state.

## API and resilience

Validate external input at presentation boundaries. Use cancellation tokens across long-running operations and external calls. Avoid synchronous blocking I/O. Keep service-to-service calls explicit and resilient without hiding business orchestration in shared infrastructure.

## Localization

Depend on the repository `ILocalizer` abstraction in Application/Presentation code. Concrete `IStringLocalizerFactory` integration belongs in Infrastructure/Composition. Each Web API owns its localization resource scope.

## Security

Never commit credentials, signing keys, connection secrets, tokens, certificates, or personal data. Use configuration/environment/secret stores. Never log authentication secrets or sensitive request payloads.

## Testing

Test Domain and Application independently. Infrastructure integration tests should use controlled service dependencies. API tests should verify authentication, authorization, validation, and service boundaries. Never claim tests were executed unless they actually were.
