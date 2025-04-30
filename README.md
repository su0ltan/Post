# Post

**Post** is an ASP.NET MVC application that enables users to create and share posts, and allows others to reply to those posts. The project uses Clean Architecture principles for maximum readability, maintainability, and testability. Built-in Authentication and Authorization ensure secure access and user management.

---

## Table of Contents

- [Features](#features)  
- [Architecture](#architecture)  
- [Prerequisites](#prerequisites)  
- [Getting Started](#getting-started)  
  - [Clone the Repo](#clone-the-repo)  
  - [Configure the Database](#configure-the-database)  
  - [Run Migrations](#run-migrations)  
  - [Launch the App](#launch-the-app)  
- [Authentication & Authorization](#authentication--authorization)  
- [Clean Architecture Layers](#clean-architecture-layers)  
- [Testing](#testing)  
- [Contributing](#contributing)  
- [License](#license)  

---

## Features

- **User Posts**  
  - Create, edit, and delete your own posts.  
  - View a feed of all users’ posts.
- **Replies/Comments**  
  - Reply to any post.  
  - View threaded conversations.
- **Authentication**  
  - Register and log in with email/password.  
  - Secure password hashing and validation.
- **Authorization**  
  - Role-based access control (e.g. Admin, User).  
  - Only owners can edit/delete their posts and replies.
- **Clean Architecture**  
  - Separation of concerns  
  - Dependency injection  
  - Layered project structure  
  - Unit– and integration-friendly  

---

## Architecture

This solution follows the **Clean Architecture** pattern, split into four main layers:

1. **Domain**  
   - Entities, value objects, domain exceptions, interfaces (e.g. `IPostRepository`)
2. **Application**  
   - Business logic, commands/queries (CQRS), DTOs, validators
3. **Infrastructure**  
   - EF Core DbContext, repository implementations, external services
4. **Presentation (MVC)**  
   - ASP.NET Core MVC controllers, views, view-models

---

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download)  
- [SQL Server](https://www.microsoft.com/sql-server) (or any provider configured in `appsettings.json`)  
- Optional: [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## Getting Started

### Clone the Repo

```bash
git clone https://github.com/your-username/Post.git
cd Post
