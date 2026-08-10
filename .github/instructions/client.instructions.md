---
name: Client .NET MAUI Projects
description: Instructions for .NET MAUI client projects, presentation MVVM, State libraries, graphics abstraction, asset import, and backend integration.
applyTo: "src/client/**/*"
---

# Client Projects

Engin3D client projects target .NET 10 and .NET MAUI 10.x. UraniumUI is the UI component library and CommunityToolkit.Mvvm is used for MVVM.

## Architecture

The MAUI application follows DDD with `ApplicationLayer`, `DomainLayer`, and `InfrastructureLayer`. State is kept in dedicated MAUI class libraries under `src/client`.

Presentation uses MVVM and feature aggregates:

```text
Presentation/
  FeatureName/
    Views/
    ViewModels/
    Controls/
```

A View, ViewModel, and feature-specific control belong to the same feature aggregate. Do not create global presentation folders that mix unrelated features.

Infrastructure and Application also organize code by feature aggregate. Do not introduce broad global `Services`, `Repositories`, or `ViewModels` folders when a feature-specific aggregate is appropriate.

## Dependency direction

Presentation depends on Application contracts and State. Application depends on Domain. Infrastructure implements Application abstractions. Graphics and asset-import implementations remain infrastructure concerns behind interfaces.

Views and ViewModels must not instantiate persistence, renderer, importer, or HTTP implementations directly. Register implementations through DI.

## MauiProgram

Keep `MauiProgram.cs` declarative. Prefer `MauiAppBuilderExtensions` methods that compose State, Application, Infrastructure, HTTP clients, authentication, localization, graphics, importers, and UI setup and return the builder/app for fluent composition.

## State

Project/scene state is authoritative for editable project data. Keep persistent transform, hierarchy, asset metadata, and other project data separate from transient pointer/interaction state and viewport/camera state.

A renderer must consume state; it must never become the authoritative owner of object transforms. Updating a transform from the Inspector and updating it through a tool must converge on the same state path.

Avoid shared mutable state between selected objects. Selection changes must restore the selected object's own state without leaking values from the previously selected object.

## Graphics

Use strategy-based abstractions so graphics backends can be implemented independently. The renderer must not expose Silk.NET types outside the graphics infrastructure boundary.

The first backend may use Silk.NET, but it is replaceable. Do not let a graphics API dictate Domain, Application, State, or Presentation models.

Separate camera, viewport, interaction, renderer, and project state. Pointer operations must have explicit begin/update/end lifecycle and must be cancelled safely when selection, tool, camera, or rendering context changes.

Do not use render callbacks as the source of truth for transforms. Avoid asynchronous callbacks that can apply stale state after a newer interaction has started.

## Asset import

Use strategy-based importers. Each importer converts an external model/assembly format into engine-neutral asset and scene models. Importers must not create GPU resources directly.

Preserve hierarchy, transforms, units, materials, textures, and metadata whenever supported by the source format. Do not assume all external formats use the same coordinate system or unit scale.

## Canvas and input

Canvas CSS/layout must not change the editor's surrounding layout or canvas dimensions. Keep canvas-only styles scoped to the canvas. Do not remove existing editor CSS or media rules while changing canvas presentation.

Pointer interactions must tolerate pointerup/pointercancel/pointerleave and lost capture. Never assume pointerup is guaranteed to arrive. Release interaction state on cancellation and component disposal.

Camera orientation must be accounted for when converting pointer movement into world-space axis movement. Do not hard-code screen-space movement as world-space transform.

## Backend integration

The client accesses backend functionality through typed client abstractions and the Gateway. It must not connect directly to SQL Server or MongoDB.

MQTT is an asynchronous notification channel for Project operations. Git is the source of truth for project specification and generated source repositories. MQTT must never be treated as the source of truth or as a transport for source/assets.

Subscribe to operation topics only for active operations and use an operation identifier. On success, use the Git repository/commit advertised by the operation result. On failure, display the operation output and allow recovery through the Project API.

## Validation

Keep Domain, Application, State, importer, and graphics tests independent where possible. Do not require a physical GPU unless the issue explicitly requires GPU integration testing. Never claim validation that was not actually executed.
