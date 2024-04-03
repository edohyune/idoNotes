- MSSQL을 이용한 방법
    
    ```SQL
    Create table xSTK(
    itm varchar(10),
    stock int
    )
    Create table xBOM(
    bom varchar(10),
    itm varchar(10),
    qty int
    )
    insert into xSTK(itm, stock)
    values ('itm001', 10),
           ('itm002', 5),
           ('itm003', 7)
    
    insert into xBOM(bom, itm, qty)
    values ('B612', 'itm001', 2),
           ('B612', 'itm002', 2),
           ('B612', 'itm003', 3)
    
    --xSTK, xBOM의 정보를 이용해서 'B612'를 몇개 만들수 있는지 알아보는 MSSQL 쿼리를 만들어줘
    
    ;WITH CTE AS (
        SELECT b.bom, b.itm, required_qty=b.qty, s.stock, max_qty_per_item=s.stock / b.qty
          FROM xBOM b
          JOIN xSTK s ON b.itm = s.itm
         WHERE b.bom = 'B612'
    ) SELECT bom, max_product_qty=MIN(max_qty_per_item)
        FROM CTE
       GROUP BY bom;
    ```
    
- Visual Basic Linq