---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: <% tp.file.creation_date() %>
MDT: <% tp.file.last_modified_date() %>
---
---
#### Prologue / Concept

#### Manifestation

#### Integration

###### REFERENCE

```mssql
USE [GAIA]
GO
/****** Object:  StoredProcedure [dbo].[BSP_MAKE_Model]    Script Date: 2024-05-06 오후 3:49:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[BSP_MAKE_Model](@tbl nvarchar(50))
as
begin
    --exec BSP_MAKE_Model 'FRWMST'
    set @tbl = rtrim(ltrim(replace(replace(@tbl, ']', ''), '[', '')))
    declare @rtntbl table(cols varchar(max))

    declare @cnttot int
    select @cnttot = count(*)
      from INFORMATION_SCHEMA.COLUMNS
     where TABLE_NAME = @tbl

    declare @cols varchar(200)
    declare @data varchar(200)

    declare currselect cursor LOCAL for
        select COLUMN_NAME, 
               DATATY = (case when DATA_TYPE in ('bit') then 'bool'
                              when DATA_TYPE in ('time') then 'TimeSpan'
                              when DATA_TYPE in ('datetimeoffset') then 'DateTimeOffset'
                              when DATA_TYPE in ('bigint') then 'long'
                              when DATA_TYPE in ('uniqueidentifier') then 'Guid'
                              when DATA_TYPE in ('varbinary', 'image') then 'byte[]'
                              when DATA_TYPE in ('datetime2', 'datetime', 'date') then 'DateTime'
                              when DATA_TYPE in ('int', 'tinyint', 'smallint') then 'int'
                              when DATA_TYPE in ('int', 'tinyint', 'smallint') then 'int'
                              when DATA_TYPE in ('float', 'money', 'decimal', 'numeric') then 'decimal'
                              when DATA_TYPE in ('nchar', 'nvarchar', 'ntext', 'char', 'varchar', 'text') then 'string'
                              else 'DATATYPE_ERR' end)
          from INFORMATION_SCHEMA.COLUMNS
         where TABLE_NAME = @tbl
           and COLUMN_NAME not in ('CId','CDt','MId','MDt')
         order by ORDINAL_POSITION;
    open currselect
    while (1=1)
    begin
        fetch next from currselect into @cols, @data
        IF (@@FETCH_STATUS <> 0) BREAK;

            insert into @rtntbl
            select concat('private ', @data, ' _', @cols, ';')

            insert into @rtntbl
            select concat('public ', @data, ' ', @cols)

            insert into @rtntbl
            select '{' 

            insert into @rtntbl
            select concat('    get => _', @cols, ';')

            insert into @rtntbl
            select concat('    set => Set(ref _', @cols,', value);')

            insert into @rtntbl
            select '}'

            insert into @rtntbl
            select ''

    end
    close currselect
    deallocate currselect
--------------------------------------------------------
    select * from @rtntbl
end

```