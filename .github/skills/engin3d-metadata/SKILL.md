---
name: engin3d-metadata
description: Work on the Engin3D Metadata microservice for relational metadata associated with assets used by the current authenticated user's Engin3D session.
---

# Engin3D Metadata Microservice Skill

Use this skill for `src/server/metadata`.

## Projects

```text
Engin3D.Metadata
Engin3D.Metadata.Application
Engin3D.Metadata.Composition
Engin3D.Metadata.Domain
Engin3D.Metadata.Infrastructure
```

Follow Presentation -> Application/Composition; Application -> Domain; Composition -> Application/Infrastructure; Infrastructure -> Application.

## Responsibility

Metadata owns relational metadata for assets used during the current authenticated user's Engin3D session. It is not the owner of binary asset content, project source generation, or generated MAUI source.

The service must remain independently responsible for its data and must not read Storage, Project, or future service databases directly.

## Infrastructure

Primary stack dependency: shared SQL Server, with service-owned schema/tables. The Gateway is the public ingress. Do not introduce MongoDB, Git, or Mosquitto unless a future feature explicitly requires them.

## Current-user isolation

Every operation that is user-scoped must derive the current user from authenticated context and enforce ownership at the application boundary. Never trust a client-supplied user identifier when the authenticated identity is available.

## Composition and localization

All DI belongs in Composition. Keep Program.cs declarative and use `ILocalizer` in application-facing code.

## Testing

Cover CRUD/query behavior, current-user isolation, validation, and persistence mapping independently from Gateway and other services.
