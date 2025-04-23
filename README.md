🚀 Prerequisites

Docker Desktop ✅ 

.NET 6 SDK

⚙️ 1. Clone and Enter Project Directory

git clone https://github.com/berkayetis/Bookstore_01.git

cd bsStoreApp

🐳 2.  Apply EF Core Migrations & Start Services with Docker Compose

dotnet ef database update -s WebApi/WebApi.csproj -p Repositories/Repositories.csproj

docker-compose up --build

API will be available at:
http://localhost:5000/swagger
