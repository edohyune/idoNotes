---
CDT: 2024-03-28 06:25
---
---
``` SQL

USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[BSP_SP_HELP]    Script Date: 2024-03-28 11:37:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[BSP_SP_HELP](@objNm nvarchar(50))
as
begin
    if exists(select 1 from SCT150 where tbl_nm=@objNm)
    begin 
        -- 테이블의 기본 정보 조회
        --EXEC dbo.BSP_SP_HELP 'ha100'
        select	TABLE_NAME, 
               ORDINAL_POSITION, COLUMN_NAME, 
               [declare]=concat(COLUMN_NAME, ' ', DATA_TYPE, (case when DATA_TYPE in ('varchar','nvarchar','char', 'nchar') then concat('(', CHARACTER_MAXIMUM_LENGTH, ')')
                                                                   when DATA_TYPE in ('decimal') then concat('(', NUMERIC_PRECISION, ',', NUMERIC_SCALE, ')')
                                                                   else '' end), ','),
               DATA_TYPE, COLUMN_DEFAULT, IS_NULLABLE, COLLATION_NAME
               --TABLE_CATALOG, TABLE_SCHEMA, CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION_RADIX, CHARACTER_MAXIMUM_LENGTH, DATETIME_PRECISION, CHARACTER_SET_CATALOG, CHARACTER_SET_SCHEMA, CHARACTER_SET_NAME, COLLATION_CATALOG, COLLATION_SCHEMA, COLLATION_NAME, DOMAIN_CATALOG, DOMAIN_SCHEMA, DOMAIN_NAME
          from INFORMATION_SCHEMA.COLUMNS a 
         where TABLE_NAME=@objNm

        select	a.col_nm, a.title, a.dsc, a.col_no, a.dic_cd, 
               [declare] = 
               concat(col_nm, ' ', data_ty, (case when data_ty in ('varchar','nvarchar','char', 'nchar') then concat('(', db_len, ')')
                                                  when data_ty in ('decimal') then concat('(', db_len, ',', dec_prec, ')')
                                                  else '' end), ','),
               data_ty = x.name,
               a.data_ty, a.db_len, a.dec_prec, a.dec_scale, 
               case when z.prec = -1 then 'MAX' else cast(z.prec as varchar(4)) end as db_len2,
               cast(z.prec as int) as db_len,
               cast(z.scale as int) as dec_prec,
               a.popup_id, popup_nm = (select z.title from scl100 z where z.popup_id = a.popup_id),
               a.pk_yn, a.need_yn, a.def_text, a.mask,
               a.def_cell_width, a.def_fld_width, a.ctr_ty, a.ttl_align_ty, a.txt_align_ty, a.fmt_ty, 
               a.join_ty, a.chg_yn, a.use_yn
          from SCT150 a
          left join sysobjects y on	y.name = a.tbl_nm 
          left join syscolumns z on	z.id = y.id and		z.name = a.col_nm
          left join systypes x on	x.xusertype = z.xtype 
         where a.tbl_nm = @objNm
           and a.use_yn = N'1' 
         order by a.pk_yn desc, a.need_yn desc

        select IndexName = i.name, 
               ColumnName = COL_NAME(ic.object_id, ic.column_id),
               IndexType = i.type_desc,
               IsUnique = i.is_unique,
               IsPrimaryKey = i.is_primary_key,
               IsUniqueConstraint = i.is_unique_constraint
          from sys.indexes i 
          join sys.index_columns ic ON i.object_id = ic.object_id and i.index_id = ic.index_id
          join sys.objects o ON i.object_id = o.object_id
         where o.type = 'U' 
           and o.name = @objNm
         order by i.name, ic.key_ordinal;

    end else begin
        select	TABLE_NAME, 
               ORDINAL_POSITION, COLUMN_NAME, 
               [declare]=concat(COLUMN_NAME, ' ', DATA_TYPE, (case when DATA_TYPE in ('varchar','nvarchar','char', 'nchar') then concat('(', CHARACTER_MAXIMUM_LENGTH, ')')
                                                                   when DATA_TYPE in ('decimal') then concat('(', NUMERIC_PRECISION, ',', NUMERIC_SCALE, ')')
                                                                   else '' end), ','),
               DATA_TYPE, COLUMN_DEFAULT, IS_NULLABLE, COLLATION_NAME
               --TABLE_CATALOG, TABLE_SCHEMA, CHARACTER_OCTET_LENGTH, NUMERIC_PRECISION_RADIX, CHARACTER_MAXIMUM_LENGTH, DATETIME_PRECISION, CHARACTER_SET_CATALOG, CHARACTER_SET_SCHEMA, CHARACTER_SET_NAME, COLLATION_CATALOG, COLLATION_SCHEMA, COLLATION_NAME, DOMAIN_CATALOG, DOMAIN_SCHEMA, DOMAIN_NAME
          from INFORMATION_SCHEMA.COLUMNS a 
         where TABLE_NAME=@objNm

        exec sp_help @objNm
    end

end

GO



```