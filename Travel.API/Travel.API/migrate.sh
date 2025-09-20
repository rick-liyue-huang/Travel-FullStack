# 1. 删除所有迁移
rm -rf Migrations/

# 2. 确保 PostgreSQL 正在运行
docker ps | grep postgres

# 3. 创建数据库（如果还没有）
docker exec -it my-postgres psql -U postgres -c "DROP DATABASE IF EXISTS traveldb;"
docker exec -it my-postgres psql -U postgres -c "CREATE DATABASE traveldb;"

# 4. 创建初始迁移
dotnet ef migrations add InitialCreate

# 5. 应用迁移
dotnet ef database update
