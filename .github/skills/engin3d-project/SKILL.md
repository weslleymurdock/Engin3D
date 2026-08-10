---
name: engin3d-project
description: Work on the Engin3D Project microservice for project specification synchronization, source generation, MAUI build/test/debug/publish, Git repositories, and operation notifications.
---

# Engin3D Project Microservice Skill

Use this skill for `src/server/project`.

## Projects

```text
Engin3D.Project
Engin3D.Project.Application
Engin3D.Project.Composition
Engin3D.Project.Domain
Engin3D.Project.Infrastructure
```

Follow Presentation -> Application/Composition; Application -> Domain; Composition -> Application/Infrastructure; Infrastructure -> Application.

## Responsibility

Project consumes the project specification produced by the Engin3D client and generates a MAUI project. It owns source generation, build, optional tests, debug, publish, operation status, and synchronization of generated source.

## Git model

Maintain two durable repositories per project/session as required by the feature: one for the project specification and one for generated MAUI source. Git is the durable source of truth. REST starts operations and exposes status; MQTT only signals completion/failure and applicable operation output.

Do not transport source code or assets through MQTT. A successful notification identifies the generated repository/commit that the client can fetch.

## Infrastructure

- SQL Server: relational operation/project state when required.
- Git server: specification and generated-source repositories.
- Mosquitto: asynchronous operation notifications.
- Gateway: public API ingress.

MongoDB/GridFS is not a core Project dependency. Do not directly access Storage or Metadata databases.

## Long-running operations

Generation/build/test/publish must be cancellation-aware and must not block request threads. Operations require stable identifiers and deterministic status transitions. Persist enough operation state to recover from process restarts and missed MQTT messages.

## Composition and localization

All DI belongs in Composition. Keep Program.cs declarative. Use `ILocalizer` for user-facing operation/error messages.

## Testing

Test source-generation and operation state independently. Integration tests may use controlled Git/Mosquitto/build dependencies. Never claim generation/build/test/publish succeeded unless it was actually executed.
