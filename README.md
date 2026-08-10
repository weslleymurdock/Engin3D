# Engin3D

## ❓ What is Engin3D?

Engin3D is a .NET 10 / .NET MAUI 10.x application and backend platform for creating, editing, organizing, and eventually generating executable 3D projects from scenes, assemblies, objects, assets, metadata, and associated behavior.

The editor is intended to provide a simpler workflow inspired by tools such as Stride, Evergine, Unity, and Godot while remaining focused on opening assets produced by external 3D modeling, CAD, and assembly applications. The long-term goal is to allow imported assemblies and objects to be animated, customized with materials, textures and shaders, composed into scenes and prefabs, and associated with generated application code.

The repository is a monorepo containing the MAUI editor/client and its REST backend services.

## 🏗️ Architecture

### Client

The `src/client` side uses DDD with:

- `ApplicationLayer`
- `DomainLayer`
- `InfrastructureLayer`
- dedicated State libraries
- MVVM Presentation feature aggregates

Each presentation feature groups its View, ViewModel, and feature-specific controls. State is the authoritative source for editable project/scene data; renderer, camera, viewport, and pointer interaction state remain separate concerns.

Graphics are implemented behind a strategy abstraction so different GPU/video APIs can be introduced independently. The first implementation may use Silk.NET, but the graphics API remains an infrastructure detail and does not leak into the engine-neutral project/state model.

External 3D object and assembly formats are also handled through importer strategies. Importers convert source data into engine-neutral assets/scenes while preserving hierarchy, transforms, units, coordinate systems, materials, textures, and metadata whenever supported.

### Backend

The backend uses independently deployable REST microservices. Except for the Gateway, every microservice follows:

```text
<service>                 Presentation
<service>.Application     Application
<service>.Composition     Composition / all DI
<service>.Domain          Domain
<service>.Infrastructure  Infrastructure
```

The dependency direction is:

```text
Presentation -> Application, Composition
Application  -> Domain
Composition  -> Application, Infrastructure
Infrastructure -> Application
Domain       -> none
```

Each service owns its own domain and persistence boundary. Shared infrastructure does not mean shared ownership of another service's tables or database collections.

### Gateway

`Engin3DGateway` is a single-project API Gateway using YARP Reverse Proxy. It is the public ingress for the MAUI client and routes requests to Auth, Identity, Metadata, Project, Storage, and future services.

The Gateway is a technical mediator for routing and gateway concerns, not a business-domain mediator. Business rules remain in their respective microservices.

### Backend services

- **Auth** — authentication, JWT issuance, signing, and JWKS publication.
- **Identity** — user and profile data.
- **Metadata** — relational metadata associated with assets used during the current authenticated user's Engin3D session.
- **Project** — project specification processing, source generation, MAUI build/test/debug/publish operations, and generated-source synchronization.
- **Storage** — binary asset persistence through MongoDB/GridFS.
- **Gateway** — public routing and gateway-level concerns.

## 🧱 Infrastructure

The initial container stack contains:

```text
SQL Server       relational service data
MongoDB/GridFS   binary assets
Mosquitto        asynchronous Project operation notifications
Git server       project specification and generated-source repositories
```

The initial design intentionally does not require a vector database. A vector store such as Qdrant may be introduced later as a derived semantic index for code, metadata, or assets; it will not be the source of truth for project source or assets.

### Project synchronization

A Project operation uses durable state and Git for source synchronization:

```text
Engin3D MAUI
    │
    │ start operation
    ▼
Engin3DProject
    │
    ├── source generation
    ├── build
    ├── test (optional)
    └── publish (optional)
            │
            ├── success ──► MQTT notification ──► Git pull
            │
            └── failure ──► MQTT notification + operation output
```

A project can use two Git repositories: one for the project specification and one for generated MAUI source. MQTT only signals operation state; it never carries source code or large assets and is never the source of truth.

## 🌎 Development direction

The project is being organized around releases, milestones, Features, and Tasks. The first milestone targets a complete backend/container foundation and the initial client/backend architecture required for a usable development flow.

The documentation site project is reserved for the release documentation pipeline and is not part of active editor implementation until the first beta release is ready.

Future development is expected to include:

1. complete the MAUI editor and persistent project/scene state;
2. introduce graphics backends through the graphics Strategy abstraction;
3. introduce external object/assembly import strategies;
4. persist assets and relational session metadata through the backend;
5. synchronize project specifications and generated source through Git;
6. execute source generation/build/test/publish through `Engin3DProject`;
7. provide generated MAUI projects back to the editor;
8. evolve scene components, animation, materials, shaders, prefabs, and associated code authoring;
9. add semantic/vector indexing only when an implemented feature requires it.

## ⚡ Getting Started

The repository is currently in architectural preparation for the first beta-oriented development cycle. Follow the repository agent instructions and GitHub Issues/Milestones before implementing features.

## 🔧 Building and Running

The exact build, container, and deployment workflow will be documented as the corresponding Features are implemented and released.

## 🤝 Collaborate with Engin3D

Use the repository issue templates for all implementation work. Features group Tasks and belong to a Milestone. Pull Requests follow the repository PR template and workflow rules.
