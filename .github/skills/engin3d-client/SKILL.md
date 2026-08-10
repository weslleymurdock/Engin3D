---
name: engin3d-client
description: Work on the Engin3D .NET MAUI client architecture, presentation MVVM, State integration, backend client integration, and editor infrastructure.
---

# Engin3D Client Skill

Use this skill for cross-cutting work in the MAUI editor client that spans presentation, State, Application, Infrastructure, or backend integration.

## Architecture

The client uses .NET 10 and .NET MAUI 10.x with UraniumUI and CommunityToolkit.Mvvm. The application follows DDD using ApplicationLayer, DomainLayer, and InfrastructureLayer. State libraries live under `src/client` and are consumed through abstractions and DI.

Presentation uses MVVM and feature aggregates. Keep each View, ViewModel, and feature-specific control together under the same feature aggregate. Do not create global presentation folders that mix unrelated features.

## State ownership

Project/scene state is the authoritative editable representation. Viewport/camera state and transient interaction state are separate. Renderer state is derived and must not overwrite authoritative project state.

Transform/rotation edits from Inspector and viewport tools must converge on the same state update path and preserve per-object state across selection changes.

## Graphics

Graphics APIs are selected through an `IGraphicsBackend` strategy. The first backend may use Silk.NET, but Silk.NET types must remain behind the graphics infrastructure abstraction. Do not couple Domain, Application, State, or Presentation to Silk.NET.

Separate renderer, camera, viewport, interaction, and project state. Pointer operations require explicit begin/update/end/cancel lifecycle and must tolerate pointer capture loss and cancellation.

## Asset import

Use `IAssetImporter` strategies for external 3D object/assembly formats. Importers produce engine-neutral scene/asset models and do not allocate GPU resources.

Preserve hierarchy, transforms, coordinate systems, units, materials, textures, and metadata whenever the source format provides them.

## Backend

The client reaches backend APIs through the Gateway. It must not access SQL Server or MongoDB directly. Git is durable project/generated-source state. Mosquitto is notification-only for active Project operations and is never a source of truth.

## UI and CSS

UraniumUI conventions must be respected. Preserve existing page/component CSS and media rules. Canvas-specific styling must remain inside the canvas scope and must never resize or replace the editor layout accidentally.

## Validation

Prefer deterministic tests around State, Application, importers, and graphics abstractions. GPU integration tests are opt-in. Never claim a build or test was executed unless it actually ran.
