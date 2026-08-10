---
name: engin3d-state
description: Work on Engin3D client State libraries and authoritative project, scene, selection, viewport, camera, and interaction state.
---

# Engin3D State Skill

Use this skill when implementing or changing State projects under `src/client`.

## Ownership

State is the authoritative source for editable project data. Keep persistent project/scene state separate from transient UI interaction and viewport state.

At minimum distinguish:

- Project/Scene state: hierarchy, assets, transforms, rotations, materials, metadata, and other persisted editing data.
- Selection state: selected entity identifiers and selection-specific data.
- Viewport/Camera state: camera position, orientation, zoom, and viewport settings.
- Interaction state: active tool, active axis, pointer capture, drag start values, and cancellation state.

## Transform consistency

Every object owns its own transform and rotation state. Changing an axis must preserve the other axes. Switching selection must never reuse mutable transform values from the previous object.

Inspector and viewport tools must update the same authoritative state. Rendering must read that state; it must not reset it on mouseup, rerender, selection changes, or frame completion.

## Concurrency

Interaction updates must reject stale operations. Use operation/session identifiers or equivalent state ownership where asynchronous rendering or callbacks can outlive the interaction that created them. Cancel active interaction when the selected object, tool, camera, or rendering context changes.

Do not use delayed callbacks as an implicit synchronization mechanism.

## Design

Prefer immutable snapshots/value objects for state crossing rendering or asynchronous boundaries. Keep mutable state transitions explicit and testable. Do not make State depend on Silk.NET, concrete asset importers, HTTP clients, or UI controls.

## Testing

Test state transitions independently of rendering. Cover per-object persistence, selection switching, multi-axis edits, Inspector-to-canvas updates, canvas-to-Inspector updates, cancellation, and stale callback protection.
