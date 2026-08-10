---
name: engin3d-maui-tests
description: Test Engin3D MAUI presentation, State, graphics abstractions, and model-driven editor behavior.
---

# Engin3D MAUI Tests Skill

Use this skill for client-side tests, including MAUI presentation tests and state/graphics behavior.

## Test boundaries

Prefer unit tests for Domain, Application, and State. Use bUnit for Blazor-based UI only when the feature under test is actually hosted by a Blazor component; do not force bUnit onto native MAUI XAML views.

Graphics tests should exercise engine-neutral abstractions with deterministic doubles. GPU tests are integration tests and must be explicitly requested by the issue.

## Models and resources

When a test needs a 3D model, use repository test resources under:

```text
Resources/Models/<ModelName>/
```

Model variations and future supported formats live inside the model directory. Textures live under:

```text
Resources/Models/<ModelName>/Textures/
```

Keep large binary resources under Git LFS when repository policy requires it.

## Editor behavior

Cover authoritative state rather than screenshot-only behavior whenever possible. For transform/rotation tools test both directions: viewport operation updates the selected object's state/Inspector, and Inspector edits update the rendered representation through the same state path.

Verify that selecting a second object never inherits transform/rotation values from the first. Verify cancellation and stale callback protection.

## Validation honesty

Never claim a test ran unless it was actually executed. Tests that require local assets, GPU drivers, or external services must declare those prerequisites rather than silently skipping behavior.
