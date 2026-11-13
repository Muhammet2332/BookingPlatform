# BookingPlatform

A modern, scalable booking platform built with **.NET 10** and **Clean Architecture** principles. Designed as a portfolio project to demonstrate modular backend development, separation of concerns, and production-ready project structuring.

## Tech Stack

* **Framework:** .NET 10 / ASP.NET Core Web API
* **Architecture:** Clean Architecture / Modular Monolith (Domain, Application, Infrastructure, API)
* **IDE & Tooling:** Visual Studio 2022/2026 (`.slnx` solution format)

## Project Structure

The solution is organized into distinct layers to ensure maintainability, testability, and loose coupling:

```text
BookingPlatform/
├── src/
│   └── Catalog/
│       ├── Catalog.Api/            # Entry point, controllers/endpoints, middleware, DI setup
│       ├── Catalog.Application/    # Business use cases, DTOs, interfaces, CQRS handlers
│       ├── Catalog.Domain/         # Enterprise entities, value objects, domain logic
│       └── Catalog.Infrastructure/ # Database context, repositories, external integrations
└── BookingPlatform.slnx