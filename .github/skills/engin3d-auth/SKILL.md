---
name: engin3d-auth
description: Work on the Engin3D Auth microservice, including JWT authentication, token issuance, JWKS, registration, confirmation, and authentication flows.
---

# Engin3D Auth Microservice Skill

Use this skill for `src/server/auth`.

## Projects

```text
src/server/auth/Engin3D.Auth
src/server/auth/Engin3D.Auth.Application
src/server/auth/Engin3D.Auth.Composition
src/server/auth/Engin3D.Auth.Domain
src/server/auth/Engin3D.Auth.Infrastructure
```

Follow Presentation -> Application/Composition; Application -> Domain; Composition -> Application/Infrastructure; Infrastructure -> Application.

## Responsibility

Auth is the authentication authority. It owns registration, confirmation, authentication, JWT issuance, signing-key management, and JWKS publication. Do not move user profile ownership into Auth; that belongs to Identity.

Use JWT/JWKS rather than opaque Identity API tokens. Other services validate access tokens using the JWKS endpoint.

## Infrastructure

Primary stack dependency: SQL Server for Auth persistence. The Gateway is the public ingress. JWKS URL configuration must support gateway-mediated access and direct internal Auth access without changing application behavior.

Auth must not depend on MongoDB, Mosquitto, or Git for core authentication.

## Security

Never log passwords, tokens, signing keys, or recovery secrets. Keep signing material outside source control and use environment/secret configuration.

## Composition

All DI belongs in Composition. Keep `Program.cs` declarative and use the repository's WebApplicationBuilder/WebApplication extension pattern.

## Testing

Test authentication and token validation without requiring the Gateway where possible. Include registration/confirmation/authentication/JWKS behavior and failure paths. Never claim tests were executed unless they were actually run.
