---
name: engin3d-mqtt
description: Work on Engin3D MQTT notification integration between the MAUI editor and Project service.
---

# Engin3D MQTT Skill

Use this skill for client-side MQTT operation notifications.

## Purpose

Mosquitto is used as an application-controlled notification channel for long-running Project operations. It replaces dependence on external push-notification providers but is not a persistence mechanism.

## Protocol responsibility

The client starts a Project operation through the Gateway/Project API, subscribes to the operation-specific MQTT topic, and waits for completion or failure notifications. Notifications identify the operation and, on success, the durable Git repository/commit that can be fetched.

MQTT must never carry source code or large assets. Git/API remain the source of truth.

## Reliability

Treat MQTT messages as hints about durable state. Persist operation status in Project and allow the client to recover through the Project API if a notification is missed. Use operation identifiers to prevent a late notification from affecting a newer operation.

Unsubscribe when the active operation completes, fails, is cancelled, or the ViewModel is disposed. Handle reconnects and duplicate notifications idempotently.

## Security

Use authenticated broker connections and TLS when required by the environment. Never place access tokens or sensitive project data in topics or payloads.

## Testing

Test subscription lifecycle, success/failure handling, duplicate messages, missed notifications/recovery, cancellation, and disposal without requiring an external broker where possible.
