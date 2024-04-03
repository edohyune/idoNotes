```SQL
WITH CTE AS(
   SELECT PROGRAM, co_cd, inout, cashbank, accCode, pcCode, suCode,
          RN = ROW_NUMBER()OVER(PARTITION BY PROGRAM, co_cd, inout, cashbank, accCode, pcCode, suCode ORDER BY id DESC)
     FROM IPO_MAPPING_CB
)
delete FROM CTE WHERE RN > 1
```