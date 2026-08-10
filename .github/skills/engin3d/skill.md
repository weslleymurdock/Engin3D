---
name: engin3d
description: Work on the Engin3D .NET MAUI editor, including MVVM presentation, State integration, viewport/editor interaction, project state, and backend client integration.
---

# Engin3D Client Skill

Use this skill for work in `src/client/Engin3D` and cross-cutting editor behavior.

## Stack

The editor targets .NET 10 / .NET MAUI 10.x, uses UraniumUI for UI components and CommunityToolkit.Mvvm for MVVM.

## Architecture

The client follows DDD using `ApplicationLayer`, `DomainLayer`, and `InfrastructureLayer`. Dedicated State libraries under `src/client` own reusable state abstractions.

Presentation uses feature aggregates:

```text
Presentation/
  FeatureName/
    Views/
    ViewModels/
    Controls/
```

Keep each View, ViewModel, and feature-specific control within its feature aggregate. Do not introduce global folders mixing unrelated presentation concerns.

## State ownership

Project/scene state is authoritative for editable data. Selection, viewport/camera, and transient pointer/tool state are separate. Renderer state is derived and must never become the authoritative transform owner.

Inspector edits and viewport-tool edits must use the same authoritative state path. Selection changes must restore the selected object's own transform/rotation/metadata without leaking values from another object.

Avoid stale callbacks. Pointer operations require explicit begin/update/end/cancel lifecycle and must cancel when selection, tool, camera, renderer, or component lifecycle changes.

## Graphics

Graphics are strategy-based. `IGraphicsBackend` isolates the rendering API. Silk.NET may implement the first backend but must remain an Infrastructure implementation detail. No Silk.NET types may leak into Domain, Application, State, or Presentation.

Separate renderer, camera, viewport, interaction, and project state. Camera orientation must be considered when mapping pointer movement to world-space axes.

Do not fix visual transform bugs by resetting authoritative state during render. Render from state and update state explicitly from interaction/Inspector operations.

## Asset import

Use `IAssetImporter` strategies for external 3D object and assembly formats. Importers produce engine-neutral scene/asset models and do not create GPU resources. Preserve hierarchy, transforms, coordinate system, units, materials, textures, and metadata where supported.

## UI/CSS

Preserve existing editor styles and `@media` rules. Canvas-only styling must remain scoped to the canvas and must not alter the surrounding editor layout or canvas size. Do not delete component/page styles while changing canvas styling.

## Backend integration

Use the Gateway as the public backend boundary. The MAUI client must not connect directly to SQL Server or MongoDB. Git is durable project/generated-source state. Mosquitto is notification-only for active Project operations and is not a source of truth.

## Validation

Prefer State/Application/importer/graphics-abstraction tests that do not require a physical GPU. GPU integration tests are opt-in. Never claim tests or builds ran unless they were actually executed.
