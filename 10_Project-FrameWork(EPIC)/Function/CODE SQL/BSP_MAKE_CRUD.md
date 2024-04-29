---
CDT: 2024-03-28 06:25
---
---
``` SQL

USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[BSP_MAKE_CRUD]    Script Date: 2024-03-28 11:35:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[BSP_MAKE_CRUD](@tbl nvarchar(50))
as
begin
    set @tbl = replace(replace(@tbl, ']', ''), '[', '')
    --exec BSP_MAKE_CRUD 'HFI10A'
    declare @rtntbl table(cols varchar(max))
    declare @cntint int = 1
    declare @cnttot int
    declare @insertQ varchar(max)
    declare @selectQ varchar(max)
    declare @select varchar(max)

    select @cnttot = count(*)
      from INFORMATION_SCHEMA.COLUMNS
     where TABLE_NAME = @tbl

    declare @cols varchar(200)

    declare currselect cursor LOCAL for
        select COLUMN_NAME
          from INFORMATION_SCHEMA.COLUMNS
         where TABLE_NAME = @tbl
         order by ORDINAL_POSITION;
    open currselect
    while (1=1)
    begin
        fetch next from currselect into @cols
        IF (@@FETCH_STATUS <> 0) BREAK;

            if @cntint = @cnttot 
            begin 
                --마지막 컬럼 처리
                if @cntint % 5 = 1
                begin 
                    set @select = concat(@select, '       a.', @cols) 
                end else begin
                    set @select = concat(@select, 'a.',@cols)
                end
                
                insert into @rtntbl
                select @select
                insert into @rtntbl
                select concat('  from ', @tbl, ' a') union all
                select ' where 1=1'
                declare @wcols varchar(200)
                declare curSelectW cursor LOCAL for
                    SELECT kcu.COLUMN_NAME
                      FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                      JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                     WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
                       AND tc.TABLE_NAME = @tbl;
                open curSelectW
                while (1=1)
                begin
                    fetch next from curSelectW into @wcols
                    IF (@@FETCH_STATUS <> 0) BREAK;
                    insert into @rtntbl
                    select concat('   and a.', @wcols, ' = @', @wcols)
                end
                close curSelectW
                deallocate curSelectW
            end else begin 
                if @cntint % 5 = 0
                begin 
                    set @select = concat(@select, 'a.', @cols, ',')
                    insert into @rtntbl
                    select @select
                    set @select = null
                end else begin 
                    if @cntint % 5 = 1
                    begin 
                        if @cntint = 1        
                        begin 
                            set @select = concat('select a.', @cols, ', ')
                        end else begin 
                            set @select = concat('       a.', @cols, ', ')
                        end
                    end else begin
                        set @select = concat(@select, 'a.', @cols, ', ')
                    end
                end
            end
        set @cntint = @cntint + 1
    end
    close currselect
    deallocate currselect

    set @cntint = 1

    insert into @rtntbl
    select concat('insert into ', @tbl)


    declare curInsert cursor LOCAL for
        select COLUMN_NAME
          from INFORMATION_SCHEMA.COLUMNS
         where TABLE_NAME = @tbl
         order by ORDINAL_POSITION;
    open curInsert
    while (1=1)
    begin
        fetch next from curInsert into @cols
        IF (@@FETCH_STATUS <> 0) BREAK;

            if @cntint = @cnttot 
            begin 
                if @cntint % 5 = 1
                begin 
                    set @insertQ = concat(@insertQ, '       ', @cols, ')')
                end else begin
                    set @insertQ = concat(@insertQ, @cols, ')')
                end

                insert into @rtntbl
                select @insertQ
                set @insertQ = null
                --set @selectQ = concat(@selectQ, @cols, char(10), char(13))
            end else begin 
                if @cntint % 5 = 0
                begin 
                    set @insertQ = concat(@insertQ, @cols, ',')
                    insert into @rtntbl
                    select @insertQ
                    set @insertQ = null
                    --set @selectQ = concat(@selectQ, @cols, ',       ')
                end else begin 
                    if @cntint % 5 = 1
                    begin 
                        if @cntint = 1        
                        begin 
                            set @insertQ = concat('      (', @cols, ', ')
                        end else begin 
                            set @insertQ = concat('       ', @cols, ', ')
                        end
                    end else begin
                        set @insertQ = concat(@insertQ, @cols, ', ')
                    end
                    --set @selectQ = concat(@selectQ, '@', @cols, ', ')
                end
            end
        set @cntint = @cntint + 1
    end
    close curInsert
    deallocate curInsert

    set @cntint = 1

    declare curselect cursor LOCAL for
        select COLUMN_NAME
          from INFORMATION_SCHEMA.COLUMNS
         where TABLE_NAME = @tbl
         order by ORDINAL_POSITION;
    open curselect
    while (1=1)
    begin
        fetch next from curselect into @cols
        IF (@@FETCH_STATUS <> 0) BREAK;

            if @cntint = @cnttot 
            begin 
                if @cntint % 5 = 1
                begin 
                    set @selectQ = concat(@selectQ, '       @', @cols) 
                end else begin
                    set @selectQ = concat(@selectQ, '@', @cols)
                end

                insert into @rtntbl
                select @selectQ
                set @selectQ = null
                --set @selectQ = concat(@selectQ, @cols, char(10), char(13))
            end else begin 
                if @cntint % 5 = 0
                begin 
                    set @selectQ = concat(@selectQ, '@', @cols, ',')
                    insert into @rtntbl
                    select @selectQ
                    set @selectQ = null
                end else begin 
                    if @cntint % 5 = 1
                    begin 
                        if @cntint = 1        
                        begin 
                            set @selectQ = concat('select ', '@', @cols, ', ')
                        end else begin 
                            set @selectQ = concat('       ', '@', @cols, ', ')
                        end
                    end else begin
                        set @selectQ = concat(@selectQ, '@', @cols, ', ')
                    end
                end
            end
        set @cntint = @cntint + 1
    end
    close curselect
    deallocate curselect

    set @cntint = 1

    declare curupdate cursor LOCAL for
        select COLUMN_NAME
          from INFORMATION_SCHEMA.COLUMNS
         where TABLE_NAME = @tbl
         order by ORDINAL_POSITION;
    open curupdate
    while (1=1)
    begin
        fetch next from curupdate into @cols
        IF (@@FETCH_STATUS <> 0) BREAK;

            if @cntint = @cnttot 
            begin 
                insert into @rtntbl
                select concat('       ', @cols, '= @', @cols) union all
                select concat('  from ', @tbl, ' a') union all
                select ' where 1=1'

                declare @ucols varchar(200)
                declare curUpdateW cursor LOCAL for
                    SELECT kcu.COLUMN_NAME
                      FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                      JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                     WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
                       AND tc.TABLE_NAME = @tbl;
                open curUpdateW
                while (1=1)
                begin
                    fetch next from curUpdateW into @ucols
                    IF (@@FETCH_STATUS <> 0) BREAK;
                    insert into @rtntbl
                    select concat('   and ', @ucols, ' = @', @ucols, '_old')
                end

                close curUpdateW
                deallocate curUpdateW
            end else begin 
                if @cntint = 1        
                begin 
                    insert into @rtntbl
                    select 'update a' union all
                    select concat('   set ', @cols, '= @', @cols, ',')
                end else begin 
                    insert into @rtntbl
                    select concat('       ', @cols, '= @', @cols, ',')
                end
            end
        set @cntint = @cntint + 1
    end
    close curupdate
    deallocate curupdate

    insert into @rtntbl
    select 'delete' union all
    select concat('  from ', @tbl) union all
    select ' where 1=1'

    declare @dcols varchar(200)
    declare curDeleteW cursor LOCAL for
        SELECT kcu.COLUMN_NAME
          FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
          JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
         WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
           AND tc.TABLE_NAME = @tbl;
    open curDeleteW
    while (1=1)
    begin
        fetch next from curDeleteW into @dcols
        IF (@@FETCH_STATUS <> 0) BREAK;
        insert into @rtntbl
        select concat('   and ', @dcols, ' = @', @dcols, '_old')
    end

    close curDeleteW
    deallocate curDeleteW
    --declare @rtnInsert varchar(max)
    ----set @rtnInsert = concat(SUBSTRING(@insertQ, 0, len(@insertQ)-1), ')', SUBSTRING(@selectQ, 0, len(@selectQ)-2))
    --set @rtnInsert = concat(@insertQ, @selectQ)
    --SELECT QueryInsert = @rtnInsert
    select * from @rtntbl
 end
GO



```