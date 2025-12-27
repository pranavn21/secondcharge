# SecondCharge

## Created by Abby Arce and Pranav Nair

SecondCharge is a full-stack EV marketplace project. The goal is to make it easy to browse EV models and build out a real listing workflow (create, search, and manage listings) backed by a database.

This repository contains both the Angular frontend and the .NET backend.

## Repo layout

- `frontend/` — Angular application (UI)
- `backend/` — ASP.NET Core API + SQL Server (services + data)

## Tech stack

- Frontend: Angular (generated with Angular CLI)
- Backend: ASP.NET Core Web API (C#)
- Database: Microsoft SQL Server

## Local setup

### Prerequisites

- Node.js (LTS) + npm
- Angular CLI (match the version used by the frontend project)
- .NET SDK (recent version)
- SQL Server (LocalDB, Docker, or a full SQL Server instance) + SSMS optional

### 1) Database

1. Create a SQL Server database (local is fine).
2. Update the backend connection string (see configuration section below).
3. Create tables / run migrations (depending on how your backend is currently set up).
4. Optional: seed data (for example, `dbo.Cars`) so the UI has something to show.

### 2) Backend (API)

From the `backend/` directory:

```bash
dotnet restore
dotnet run
