# ICDify

**ICDify** is an enterprise-grade microservice built with **.NET 9**, designed to extract drug indications from FDA-approved DailyMed labels and map them to **ICD-10 clinical terminology** using AI-driven NLP and semantic matching.

This service exposes a fully queryable RESTful API, complete with CRUD operations, role-based authentication, and OpenAPI documentation. Architected using the **Clean Architecture pattern**, it ensures modularity, scalability, and long-term maintainability across layers.

> _ICDify your data. Clarify your care._

## 🔍 Key Features

- ✅ DailyMed drug label ingestion and indication extraction
- 🧠 AI-driven semantic mapping to ICD-10 codes
- 💡 Clean, layered architecture (Domain → Application → Infrastructure → API)
- 🧪 Full test coverage (TDD-first, xUnit)
- 🔐 JWT-based authentication with RBAC
- 🌍 OpenAPI/Swagger integration
- 🐳 Fully containerized with Docker + docker-compose

## Projects

- **ICDify.API**: Main entry point, ASP.NET Core RESTful API.
- **ICDify.Domain**: Core business logic and domain models.
- **ICDify.Infrastructure**: Data access, integrations, and infrastructure.
- **ICDify.Tests**: Unit and integration tests (xUnit).

## Prerequisites

- .NET 9 SDK
- Docker (optional, for containerized deployment)
- Visual Studio 2022 or later (recommended)

## Getting Started

1. **Clone the Repository**:git clone <repository-url>
cd ICDify2. **Build the Solution**:dotnet build3. **Run the API**:cd ICDify.API
   dotnet run4. **Run Tests**:cd ICDify.Tests
   dotnet test
## Docker Support

The solution includes a `Dockerfile` in the `ICDify.API` project for containerized deployment.

1. **Build the Docker Image**:docker build -t icdify-api ./ICDify.API2. **Run the Docker Container**:docker run -p 8080:80 icdify-api
## License

This project is licensed under the MIT License.