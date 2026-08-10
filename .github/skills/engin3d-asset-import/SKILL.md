---
name: engin3d-asset-import
description: Work on external 3D object and assembly import strategies and engine-neutral asset conversion.
---

# Engin3D Asset Import Skill

Use this skill for opening/importing external 3D objects and assemblies.

## Strategy

Implement format-specific importers behind `IAssetImporter` or the repository's equivalent abstraction. The importer is responsible for reading the source format and producing engine-neutral asset/scene data.

Do not couple an importer to Silk.NET, a renderer, a GPU device, or a MAUI View.

## Imported data

Preserve, when supported by the source format:

- assembly/object hierarchy;
- local and world transforms;
- coordinate-system orientation;
- measurement units and scale;
- materials;
- textures and texture references;
- object/part identifiers;
- metadata;
- animation information;
- external references.

Do not silently discard unsupported information. Report unsupported or lossy conversions through the importer result/diagnostics model.

## Coordinate systems

Every importer must explicitly describe the source coordinate system and unit scale. Conversion into the Engin3D coordinate system happens at the engine-neutral boundary, not implicitly inside rendering code.

## Lifecycle

Asset loading must be asynchronous and cancellation-aware. Large models must not block the MAUI UI thread. Dispose source streams and parser resources deterministically.

## Testing

Use representative models under the repository test resources when available. Tests should verify hierarchy, transforms, units, materials, textures, metadata, and diagnostics. Do not require a GPU to validate import correctness.
