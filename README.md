🚀 Prerequisites

Docker Desktop ✅ 
.NET 6 SDK

⚙️ 1. Clone and Enter Project Directory
git clone https://github.com/berkayetis/Bookstore_01.git
cd Bookstore_01

🐳 2. Start Services with Docker Compose
docker-compose up --build

🗄️ 3. Apply EF Core Migrations
Run this command to create your database schema inside the running SQL Server container:
dotnet ef database update -s WebApi/WebApi.csproj -p Repositories/Repositories.csproj

API will be available at:
http://localhost:5000/swagger
