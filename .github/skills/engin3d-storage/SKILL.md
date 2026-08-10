---
name: engin3d-storage
description: Work on the Engin3D Storage microservice for type-specific binary asset persistence using MongoDB GridFS.
---

# Engin3D Storage Microservice Skill

Use this skill for `src/server/storage`.

## Projects

```text
Engin3D.Storage
Engin3D.Storage.Application
Engin3D.Storage.Composition
Engin3D.Storage.Domain
Engin3D.Storage.Infrastructure
```

Follow Presentation -> Application/Composition; Application -> Domain; Composition -> Application/Infrastructure; Infrastructure -> Application.

## Responsibility

Storage owns binary asset persistence. Each supported asset type is represented by its own collection/resource abstraction and stored in MongoDB GridFS. The service must expose a consistent API contract across asset types.

Do not put relational project metadata, user profiles, or generated source in Storage. Those concerns belong to Metadata, Identity, and Project respectively.

## Generic asset operations

Prefer reusable generic application/domain abstractions for common asset operations. Route/resource naming may be derived from the asset type where safe, but HTTP routing remains a presentation concern and must not leak into Domain.

## Infrastructure

Primary stack dependency: MongoDB/GridFS. The Gateway is the public ingress. Storage does not require SQL Server, Mosquitto, or Git for core asset persistence.

Do not access another service's database directly. User/project ownership identifiers may be stored as metadata needed for authorization, but authoritative profile/metadata data remains outside Storage.

## Composition

All DI belongs in Composition. Keep Program.cs declarative and use the repository's builder/application pipeline extensions.

## Validation and lifecycle

Validate asset type, identifiers, metadata, stream size, and content before persistence. Stream large assets asynchronously and cancellation-aware. Dispose streams deterministically. Do not load large assets fully into memory when GridFS streaming is sufficient.

## Testing

Test storage abstractions independently from the Gateway. Integration tests should verify GridFS upload/download/delete and metadata behavior with controlled MongoDB dependencies.
