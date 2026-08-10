---
name: engin3d-graphics
description: Work on the engine-neutral graphics layer, GPU backend strategies, renderer, camera, viewport, gizmos, and MAUI canvas integration.
---

# Engin3D Graphics Skill

Use this skill for rendering and viewport work in the Engin3D client.

## Strategy boundary

Use an `IGraphicsBackend` strategy so video/GPU APIs can be implemented one at a time and replaced without changing Domain, Application, State, or Presentation.

Silk.NET may be the first implementation. Treat Silk.NET as Infrastructure only. Do not expose Silk.NET types through engine-neutral interfaces, state models, ViewModels, or domain entities.

## Responsibilities

- Renderer: consumes engine-neutral scene state and produces frames.
- Camera: owns camera/view configuration and coordinate conversion.
- Viewport: owns canvas/viewport dimensions and presentation-specific rendering surface concerns.
- Interaction: translates pointer input into explicit tool operations.
- Project State: remains the authoritative owner of transforms and scene data.

The renderer must never reset project transforms when a frame completes or an interaction ends.

## Interaction safety

Tool operations have explicit begin/update/end/cancel phases. Handle pointer capture loss, pointercancel, pointerleave, component disposal, selection changes, tool changes, and renderer recreation. Ignore stale callbacks from previous operations.

Axis selection must be explicit. Hover and active-axis visual state must not be inferred from the last rendered axis. Camera orientation must be considered when projecting pointer movement onto a world-space axis.

## Gizmos

Gizmo visuals and hit-testing must be generated from the same axis definitions so the visible line/handle and clickable area cannot drift apart. Active-axis highlighting should isolate the selected axis. Labels must remain readable and camera-facing where the feature requires it.

## Canvas

Canvas CSS must be scoped to the canvas only. Do not place grids or overlays in the outer editor container if that changes the editor layout. Preserve existing editor styles and media queries.

## Resource lifecycle

Dispose GPU buffers, textures, pipelines, command resources, event subscriptions, timers, and unmanaged resources deterministically. Recreating a renderer must not leak previous resources or callbacks.

## Testing

Prefer renderer tests against abstractions and deterministic test doubles. Use real GPU integration only when explicitly required. Verify state-to-render and interaction-to-state paths independently.
