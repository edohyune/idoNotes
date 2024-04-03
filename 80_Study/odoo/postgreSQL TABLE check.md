  

postgreSql 원격접속

  

- 비교 TABLE 생성
    
    ```SQL
    -- Create
    call update_table_statistics('Remarks 입력합니다.')
    
    -- Read
    select * from xtbl_status_mst;
    select * from xtbl_status_dtl;
    ```
    
    ```SQL
    -- Table: public.xtbl_status_mst
    -- DROP TABLE IF EXISTS public.xtbl_status_mst;
    
    CREATE TABLE IF NOT EXISTS public.xtbl_status_mst
    (
        statid bigint NOT NULL,
        tblcnt integer,
        rmks character varying(2000) COLLATE pg_catalog."default",
        CONSTRAINT xtbl_status_mst_pkey PRIMARY KEY (statid)
    )
    TABLESPACE pg_default;
    ALTER TABLE IF EXISTS public.xtbl_status_mst
        OWNER to postgres;
    
    
    -- Table: public.xtbl_status_dtl
    -- DROP TABLE IF EXISTS public.xtbl_status_dtl;
    
    CREATE TABLE IF NOT EXISTS public.xtbl_status_dtl
    (
        statid bigint NOT NULL,
        tblnm character varying(255) COLLATE pg_catalog."default" NOT NULL,
        rowcnt bigint,
        CONSTRAINT xtbl_status_dtl_pkey PRIMARY KEY (statid, tblnm)
    )
    TABLESPACE pg_default;
    ALTER TABLE IF EXISTS public.xtbl_status_dtl
        OWNER to postgres;
    ```
    
      
    
- 데이터 수집
    
    ```Plain
    -- EXEC
    call update_table_statistics('Remarks 입력합니다.')
    ```
    
    ```SQL
    -- PROCEDURE: public.update_table_statistics(text)
    -- DROP PROCEDURE IF EXISTS public.update_table_statistics(text);
    
    CREATE OR REPLACE PROCEDURE public.update_table_statistics(
    	IN rmks_text text)
    LANGUAGE 'plpgsql'
    AS $BODY$
    DECLARE 
        tbl_name VARCHAR(255);
        cnt BIGINT;
        total_tables INT := 0;
        total_rows BIGINT := 0;
        current_statId BIGINT := EXTRACT(EPOCH FROM NOW())::BIGINT; -- 시간 기반 ID
    BEGIN 
        -- 각 테이블의 행 수를 저장
        FOR tbl_name IN (SELECT tablename FROM pg_tables WHERE schemaname = 'public')
        LOOP
            EXECUTE 'SELECT count(*) FROM ' || tbl_name INTO cnt;
            INSERT INTO xtbl_status_dtl(statId, tblNm, rowCnt) VALUES (current_statId, tbl_name, cnt);
            
            total_tables := total_tables + 1;
            total_rows := total_rows + cnt;
        END LOOP;
        
        -- 전체 통계를 저장
        INSERT INTO xtbl_status_mst(statId, tblCnt, rmks) VALUES (current_statId, total_tables, rmks_text);
    END;
    $BODY$;
    ALTER PROCEDURE public.update_table_statistics(text)
        OWNER TO postgres;
    ```
    
- 데이터 조회
    
    ```SQL
    select * 
      from get_table_difference()
     where status<>'Unchanged';
    ```
    
    ```SQL
    --DROP FUNCTION get_table_difference()
    CREATE OR REPLACE FUNCTION get_table_difference()
    RETURNS TABLE (
        result_tblNm VARCHAR(255),
        status VARCHAR(50),
        difference BIGINT
    ) AS $$
    DECLARE
        last_statId BIGINT;
        previous_statId BIGINT;
        current_table_name VARCHAR(255);
        current_diff BIGINT;
    BEGIN
        -- 가장 최근의 두 statId 값을 가져옴
        SELECT statId INTO last_statId FROM xtbl_status_mst ORDER BY statId DESC LIMIT 1 OFFSET 0;
        SELECT statId INTO previous_statId FROM xtbl_status_mst ORDER BY statId DESC LIMIT 1 OFFSET 1;
        
        FOR current_table_name, current_diff IN 
            (SELECT 
                current_status.tblNm AS current_table, 
                COALESCE(current_status.rowCnt, 0) - COALESCE(previous_status.rowCnt, 0) AS diff
             FROM 
                xtbl_status_dtl AS current_status
                FULL JOIN xtbl_status_dtl AS previous_status ON current_status.tblNm = previous_status.tblNm
             WHERE 
                (current_status.statId = last_statId OR current_status.statId IS NULL)
                AND (previous_status.statId = previous_statId OR previous_status.statId IS NULL))
        LOOP
            IF current_diff IS NULL THEN
                CONTINUE;
            ELSIF current_diff = 0 THEN
                status := 'Unchanged';
            ELSIF current_diff > 0 THEN
                status := 'Increased';
            ELSE
                status := 'Decreased';
                current_diff := -current_diff; -- make the number positive for clarity
            END IF;
            
            result_tblNm := current_table_name;
            difference := current_diff;
            
            RETURN NEXT;
        END LOOP;
        
        FOR current_table_name IN (SELECT tblNm FROM xtbl_status_dtl WHERE statId = last_statId
                      EXCEPT
                      SELECT tblNm FROM xtbl_status_dtl WHERE statId = previous_statId)
        LOOP
            status := 'Newly Added';
            difference := NULL;
            
            result_tblNm := current_table_name;
            
            RETURN NEXT;
        END LOOP;
    
        FOR current_table_name IN (SELECT tblNm FROM xtbl_status_dtl WHERE statId = previous_statId
                      EXCEPT
                      SELECT tblNm FROM xtbl_status_dtl WHERE statId = last_statId)
        LOOP
            status := 'Removed';
            difference := NULL;
            
            result_tblNm := current_table_name;
            
            RETURN NEXT;
        END LOOP;
    
    END;
    $$ LANGUAGE plpgsql;
    ```