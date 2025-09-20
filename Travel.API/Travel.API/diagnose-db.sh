#!/bin/bash

echo "=== 数据库诊断 ==="

echo "1. 检查容器状态："
docker ps | grep postgres

echo -e "\n2. 检查数据库列表："
docker exec -it my-postgres psql -U postgres -c '\l'

echo -e "\n3. 检查 traveldb 中的表："
docker exec -it my-postgres psql -U postgres -d traveldb -c '\dt'

echo -e "\n4. 检查迁移历史："
docker exec -it my-postgres psql -U postgres -d traveldb -c 'SELECT * FROM "__EFMigrationsHistory";' 2>/dev/null || echo "迁移历史表不存在"

echo -e "\n5. 当前迁移列表："
dotnet ef migrations list

echo -e "\n6. 数据库信息："
dotnet ef dbcontext info
