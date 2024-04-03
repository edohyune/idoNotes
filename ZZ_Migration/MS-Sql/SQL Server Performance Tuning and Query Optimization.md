  

> [!info] SQL Server Performance Tuning and Query Optimization  
> In this SQL Server course we will cover everything you need to identify bottlenecks and improve the performance of SQL queries.  
> [https://www.youtube.com/playlist?list=PL6n9fhu94yhXg5A0Fl3CQAo1PbOcRPjd0](https://www.youtube.com/playlist?list=PL6n9fhu94yhXg5A0Fl3CQAo1PbOcRPjd0)  

### INDEX

```SQL
SELECT DatabaseName=DB_NAME(ius.database_id),
TableName=OBJECT_NAME(ius.object_id),
ius.object_id,
i.name AS IndexName,
ius.user_seeks,
ius.user_scans,
ius.user_lookups,
ius.user_updates
FROM sys.dm_db_index_usage_stats AS ius
JOIN sys.indexes AS i ON i.object_id = ius.object_id AND i.index_id = ius.index_id
WHERE ius.database_id = DB_ID()  -- 현재 데이터베이스
and OBJECT_NAME(ius.object_id)='FBA300'
ORDER BY DatabaseName, TableName, IndexName;
```

—user_seeks: 인덱스를 사용하여 데이터를 검색하는 쿼리의 수  
—user_scans: 인덱스 전체를 스캔하는 쿼리의 수  
—user_lookups: 힙 또는 클러스터링된 인덱스를 사용하여 데이터를 검색하는 쿼리의 수  
—user_updates: 인덱스를 수정하는 쿼리의 수 (예: INSERT, UPDATE, DELETE)  

```SQL
SELECT IndexName = i.name,
IndexSizeKB = SUM(ps.in_row_data_page_count) * 8
FROM sys.indexes AS i
JOIN sys.dm_db_partition_stats AS ps ON i.object_id = ps.object_id AND i.index_id = ps.index_id
WHERE i.object_id = OBJECT_ID('FBA300') -- YourTableName에는 테이블 이름을 입력하세요.
GROUP BY i.name
ORDER BY IndexSizeKB DESC;
```