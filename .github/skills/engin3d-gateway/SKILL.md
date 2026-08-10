---
name: engin3d-gateway
description: Work on the Engin3D API Gateway using YARP reverse proxy as the single public ingress.
---

# Engin3D Gateway Skill

Use this skill for `src/server/gateway`.

## Responsibility

The Gateway is a single-project technical ingress. It uses YARP ReverseProxy to route public client requests to Auth, Identity, Metadata, Project, Storage, and future services.

It is not a DDD microservice and must not be split into Application/Domain/Infrastructure projects merely to match the other services.

## Mediator boundary

The Gateway may mediate technical concerns such as routing, authentication boundary, headers, rate limiting, observability, and aggregated API documentation. It must not own business rules or orchestrate domain workflows that belong to a microservice.

If a workflow requires business decisions across services, implement the appropriate application contract/service rather than embedding the workflow in YARP routing.

## Authentication

Auth is exposed through the Gateway as the public authentication boundary. Downstream services validate JWTs using Auth's JWKS. Do not make the Gateway responsible for issuing tokens or owning authentication state.

## Swagger

Keep the Gateway's Swagger experience aligned with the routed microservice APIs without duplicating their business contracts in Gateway code.

## Resilience

Do not hide downstream failures. Preserve meaningful status codes and diagnostics while avoiding leakage of secrets or internal credentials. Use cancellation and request propagation correctly.

## Testing

Test route mapping, gateway authentication boundary, forwarded headers, failure handling, and Swagger aggregation independently from business logic. Never claim tests ran unless they were actually executed.
