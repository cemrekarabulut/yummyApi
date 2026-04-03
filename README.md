# Yummy API

Modernized and production-ready REST API for restaurant operations, built with ASP.NET Core 8 and Entity Framework Core.

## Highlights

- Clean architecture direction with `Controller -> Service -> DbContext` separation.
- Async-first data access with `AsNoTracking` on read paths.
- Centralized exception handling middleware.
- FluentValidation-based request validation.
- Configurable CORS and fixed-window rate limiting.
- Swagger/OpenAPI ready API surface.

## Tech Stack

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core (SQL Server)
- AutoMapper
- FluentValidation
- Swashbuckle (Swagger UI)

## Project Structure

```text
YummyApi/
  Controller/           # HTTP endpoints
  Services/             # Business logic layer
  Context/              # EF Core DbContext
  entities/             # Domain entities
  Dtos/                 # Request/response models
  ValidationRules/      # FluentValidation rules
  Middlewares/          # Cross-cutting concerns
```

## Getting Started

1. Update database connection in `YummyApi/appsettings.json`:
   - `ConnectionStrings:DefaultConnection`
2. Configure allowed front-end origins in:
   - `Cors:AllowedOrigins`
3. Run:

```bash
dotnet restore
dotnet build
dotnet run --project YummyApi
```

Swagger UI:

- `https://localhost:<port>/swagger`

## Security Defaults

- Global exception middleware prevents stack trace leakage.
- Rate limiting enabled (`100 req / minute` per client policy).
- CORS policy restricted to configured origins.
- FluentValidation guards request payload quality.
- JWT auth/authorization is enabled for API controllers.

## JWT Authentication

Login endpoint:

- `POST /api/auth/login`
- `POST /api/auth/refresh`

Sample request:

```json
{
  "username": "admin",
  "password": "ChangeThisStrongPassword!"
}
```

Use the returned token as:

- `Authorization: Bearer <accessToken>`

Refresh request sample:

```json
{
  "refreshToken": "your_refresh_token_here"
}
```

JWT/Auth settings are configurable in:

- `YummyApi/appsettings.json`

For first-time setup (new auth tables), run EF migration commands:

```bash
dotnet ef migrations add AddAuthEntities --project YummyApi
dotnet ef database update --project YummyApi
```

## Performance Notes

- Read queries use `AsNoTracking()` for lower change-tracker overhead.
- Controllers and services are fully async for better thread utilization.
- Removed unnecessary `SaveChanges()` calls from read-only endpoints.

## Scale Recommendations

- Add Redis caching for frequently requested read endpoints.
- Introduce pagination and filtering for large lists.
- Move to repository + unit-of-work pattern for larger team/codebase growth.
- Add message queue (RabbitMQ/Azure Service Bus) for long-running workflows.
- Add observability (OpenTelemetry + centralized logs + metrics + tracing).
