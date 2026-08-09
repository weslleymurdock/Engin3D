# Engin3D Agent Rules

## Scope

These rules apply to the entire Engin3D repository. More specific instruction files under `.github/instructions/` refine them for client or server code. Do not copy rules from another repository into Engin3D unless they are explicitly adapted to this architecture.

## GitHub workflow

- Treat GitHub Issues, Milestones, Pull Requests, tags, and Releases as the project's implementation history.
- Every implementation starts from an Issue using one of the repository's existing issue templates. Do not create ad-hoc implementation issues outside those templates.
- Features aggregate Tasks; Tasks belong to a Feature; Features belong to the appropriate Milestone.
- A Milestone represents one releasable functional group. When its scope is complete, create the corresponding version tag and GitHub Release according to repository conventions.
- Document implementation scope in the issue using the template fields. Do not create parallel tracking documents unless explicitly requested.
- Pull Requests must use the repository PR template exactly. Do not replace it with a custom structure.
- Implementation PRs start as draft unless explicitly requested otherwise. Do not merge PRs unless explicitly requested.
- Keep a PR focused on one issue/task or one coherent task group.

## Commits

- Use conventional prefixes already established by the repository: `feat:`, `fix:`, `docs:`, and `tests:`.
- Keep commit messages concise and descriptive.
- Prefer intentional, reviewable commits. Split commits when separation materially improves reviewability or the task explicitly requires separate commits.
- Never amend, rewrite, force-push, or delete history unless explicitly requested.

## .NET 10 / C#

- Target .NET 10 and use current C# features supported by the solution.
- Prefer nullable reference types, implicit usings, file-scoped namespaces, pattern matching, collection expressions, records/value objects where appropriate, and async APIs.
- Do not introduce APIs deprecated in .NET 10 when a supported alternative exists.
- Respect nullable analysis; do not silence warnings with `!` unless the invariant is demonstrably guaranteed.
- Use `CancellationToken` for asynchronous operations crossing service, persistence, import, or long-running application boundaries.
- Do not add packages when framework functionality is sufficient.
- Document public APIs when XML documentation is enabled or required by the project.

## Architecture and feature aggregates

Engin3D is a monorepo containing a .NET MAUI editor client and REST backend services. Organize DDD artifacts by **feature aggregate within each architectural layer**, rather than creating broad cross-feature folders such as one global `Services`, `Commands`, or `Repositories` directory.

For every DDD project, use feature aggregation consistently:

```text
Application/
  FeatureName/
    Commands/
    Queries/
    DTOs/
    Validators/

Domain/
  FeatureName/
    Entities/
    ValueObjects/
    Events/

Infrastructure/
  FeatureName/
    Persistence/
    Providers/
    Services/
```

Names and subfolders should follow the actual feature and existing project conventions. Do not create empty layers or folders merely to satisfy the diagram.

### MAUI client

The MAUI application uses DDD with `ApplicationLayer`, `DomainLayer`, and `InfrastructureLayer` to avoid namespace collisions with server `Application` projects. State is separated into dedicated MAUI class libraries under `src/client`.

The MAUI Presentation layer additionally uses **MVVM**. Each presentation feature is a self-contained feature aggregate containing its View, ViewModel, and feature-specific Controls/components:

```text
Presentation/
  FeatureName/
    Views/
    ViewModels/
    Controls/
```

A View and its ViewModel must belong to the same feature aggregate. Do not create global `Views`, `ViewModels`, or `Controls` collections spanning unrelated features. MVVM is a MAUI Presentation rule only; it must not be imposed on server projects.

### Server DDD

Except for `Engin3DGateway`, REST services use self-contained DDD: `Application`, `Domain`, and `Infrastructure` live inside each service project, while the Web API project root is the Presentation layer. Keep feature aggregation inside each layer.

Use abstractions in Application and implementations in Infrastructure unless an implementation is intentionally application- or presentation-specific.

Each Web API project has a root DI composition file exposing `WebApplicationBuilder` extensions for service registration and `WebApplication` extensions for HTTP pipeline configuration/execution. Keep `Program.cs` declarative and minimal.

## Graphics architecture

Use two independent Strategy families:

1. `IGraphicsBackend` for GPU/API backend selection.
2. `IAssetImporter` and external asset-provider strategies for object/assembly ingestion.

The first graphics implementation may use Silk.NET, but Silk.NET is an infrastructure implementation detail. Domain, Application, State, Presentation, and engine-neutral scene models must not depend directly on Silk.NET types.

The renderer consumes an engine-neutral scene model. Importers produce engine-neutral asset/assembly models and never create GPU resources directly.

Keep responsibilities separate:

- Project/scene state owns authoritative editable data.
- Viewport state owns camera and viewport state.
- Interaction state owns transient pointer/operation state.
- Renderer consumes state and renders it; it is not the authoritative owner of transforms.
- Importers convert external assets into the Engin3D asset/scene model.
- Graphics backends translate rendering abstractions into API-specific resources.

Never make a concrete importer depend on a concrete graphics backend.

## MAUI

- Keep `MauiProgram.cs` declarative. Prefer `MauiAppBuilderExtensions` methods that compose client setup and return the builder/app as appropriate.
- Register State, Application, Infrastructure, HTTP clients, graphics backend, asset importers, and UI services through DI.
- Do not instantiate services manually in Views/ViewModels when DI is appropriate.
- Keep Presentation responsible for presentation and user interaction, not persistence or concrete renderer implementation details.
- Prefer lifecycle-aware disposal for GPU resources, streams, subscriptions, timers, and other disposable resources.
- Do not block the UI thread with synchronous I/O, asset loading, compilation, or GPU initialization.
- Use async/background work for expensive imports and project operations and marshal only UI updates to the UI thread.
- Keep platform-specific code behind abstractions, handlers, or platform services.
- Respect UraniumUI conventions already present in the solution.

## Authentication and backend

`Engin3DAuth` is the authentication/JWKS authority. `Engin3DIdentity` owns user/profile management but must implement the registration, confirmation, authentication, and related flow manually rather than relying on ASP.NET Identity API endpoint opaque tokens. Microservices validate access tokens using JWKS.

`EngineStorage` owns asset persistence in MongoDB GridFS. `Engin3DMetadata` owns SQL Server project/asset metadata. RabbitMQ is for asynchronous integration events where eventual consistency is justified; do not introduce messaging merely to replace a simple synchronous request.

## Project generation

The future `Engin3DProject` service consumes persisted project/scene/assets and generates a MAUI project. Generated source code, assets, metadata, and scene definitions remain separate concerns. Do not make the editor depend on generated source code to render a scene.

## Testing

- Add tests with the implementation issue in the appropriate project/layer.
- Test Domain/Application/state transitions independently from graphics.
- Graphics integration tests should use the graphics abstraction and deterministic test doubles where possible.
- Asset importer tests should use representative repository resources and verify hierarchy, transforms, units, materials, textures, and metadata when applicable.
- Do not require a physical GPU unless an issue explicitly defines a GPU integration test.
- Never claim tests passed unless they were actually executed.

## Validation honesty

- Never claim a build, test, benchmark, container run, migration, or deployment succeeded unless it was actually performed and observed.
- If the task says not to run tests, do not run them.
- Prefer the narrowest validation required by the issue before broader validation.

## Security

- Never commit credentials, JWT signing keys, connection-string secrets, certificates, tokens, or personal data.
- Use configuration, environment variables, development secrets, or secret stores.
- Validate external input at service boundaries.
- Never log passwords, access/refresh tokens, signing keys, or authentication secrets.

## Documentation and language

- Source comments, XML documentation, logs, and developer-facing technical documentation should use en-US unless the existing file clearly establishes another convention.
- Keep documentation aligned with implemented behavior.
- Do not document planned behavior as implemented behavior.
