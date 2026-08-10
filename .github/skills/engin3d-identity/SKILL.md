---
name: engin3d-identity
description: Work on the Engin3D Identity microservice for user profiles, account data, and current-user identity data.
---

# Engin3D Identity Microservice Skill

Use this skill for `src/server/identity`.

## Projects

```text
Engin3D.Identity
Engin3D.Identity.Application
Engin3D.Identity.Composition
Engin3D.Identity.Domain
Engin3D.Identity.Infrastructure
```

Follow Presentation -> Application/Composition; Application -> Domain; Composition -> Application/Infrastructure; Infrastructure -> Application.

## Responsibility

Identity owns user profile and account data. Authentication and token issuance remain exclusively in Auth. Identity consumes the authenticated user identity from JWT claims and does not issue the system's authentication tokens.

## Infrastructure

Primary stack dependency: SQL Server. The Gateway is the public ingress. Identity does not directly own MongoDB/GridFS, Git, or Mosquitto responsibilities.

Do not access Metadata, Storage, or another service's database directly. References to other domain identifiers must remain service boundaries unless an explicit feature defines an API/event integration.

## Composition and localization

All DI belongs in Composition. Keep Program.cs declarative and use the repository WebApplicationBuilder/WebApplication extension pattern. Use `ILocalizer` rather than leaking `IStringLocalizerFactory` into Application logic.

## Testing

Test profile/account behavior independently of Auth and Gateway where possible. Verify authorization boundaries and current-user isolation. Never claim tests were executed unless they were actually run.
