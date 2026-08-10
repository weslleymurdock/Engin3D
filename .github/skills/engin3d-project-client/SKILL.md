---
name: engin3d-project-client
description: Work on the MAUI client integration with Engin3DProject, Git project repositories, generated source synchronization, and operation lifecycle.
---

# Engin3D Project Client Skill

Use this skill when the MAUI editor starts or consumes Project generation, build, test, debug, or publish operations.

## Responsibilities

The client owns the editable project specification and scene state. Project service owns generation/build/test/publish execution and generated source.

Use the Gateway for REST operations. Do not execute source generation or builds inside the MAUI editor unless an explicit feature requires local execution.

## Git

A project may have two repositories: the project specification repository and the generated MAUI source repository. Git is the durable source of truth. The client pulls the generated repository only after a successful operation notification or explicit status recovery.

Do not treat MQTT as a repository or asset transport.

## Operation lifecycle

Use a stable operation identifier from start through completion. Subscribe to the operation notification before or atomically with starting the operation so fast completion cannot be missed. Handle success, failure, cancellation, timeout, reconnect, duplicate notification, and ViewModel disposal.

On success, reconcile the advertised commit with the Git repository before presenting generated source as current. On failure, surface diagnostics without corrupting the current project state.

## MVVM

Keep operation state in the feature ViewModel/Application contract. MQTT and Git implementations belong behind Infrastructure abstractions and are registered through DI. Views must not contain repository or broker logic.
