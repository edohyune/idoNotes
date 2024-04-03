USE [EPIC]
GO
/****** Object:  Table [dbo].[TBL150]    Script Date: 2023-10-07 ¿ÀÈÄ 1:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL150](
	[tbl_nm] [nvarchar](50) NOT NULL,
	[col_nm] [nvarchar](50) NOT NULL,
	[col_no] [int] NULL,
	[dic_cd] [varchar](50) NULL,
	[title] [nvarchar](50) NULL,
	[data_ty] [varchar](10) NULL,
	[db_len] [int] NULL,
	[dec_prec] [int] NULL,
	[dec_scale] [int] NULL,
	[need_yn] [char](1) NOT NULL,
	[pk_yn] [char](1) NOT NULL,
	[def_text] [nvarchar](50) NULL,
	[def_cell_width] [int] NULL,
	[def_fld_width] [int] NULL,
	[ctr_ty] [varchar](10) NULL,
	[ttl_align_ty] [varchar](10) NULL,
	[txt_align_ty] [varchar](10) NULL,
	[fmt_ty] [varchar](10) NULL,
	[join_ty] [varchar](10) NULL,
	[mask] [nvarchar](50) NULL,
	[lookup_no] [varchar](50) NULL,
	[popup_id] [int] NULL,
	[chg_yn] [char](1) NOT NULL,
	[dsc] [nvarchar](500) NULL,
	[rmks] [nvarchar](500) NULL,
	[cid] [int] NULL,
	[cdt] [datetime] NULL,
	[mid] [int] NULL,
	[mdt] [datetime] NULL,
 CONSTRAINT [TBL150_PK] PRIMARY KEY CLUSTERED 
(
	[tbl_nm] ASC,
	[col_nm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TBL150] ADD  CONSTRAINT [ZERO_845636267]  DEFAULT ('0') FOR [need_yn]
GO

ALTER TABLE [dbo].[TBL150] ADD  CONSTRAINT [ZERO_148029567]  DEFAULT ('0') FOR [pk_yn]
GO

ALTER TABLE [dbo].[TBL150] ADD  CONSTRAINT [ZERO_1243120471]  DEFAULT ('0') FOR [chg_yn]
GO

ALTER TABLE [dbo].[TBL150]  WITH CHECK ADD  CONSTRAINT [TBL100_FK_TBL150] FOREIGN KEY([tbl_nm])
REFERENCES [dbo].[TBL100] ([tbl_nm])
GO

ALTER TABLE [dbo].[TBL150] CHECK CONSTRAINT [TBL100_FK_TBL150]
GO

ALTER TABLE [dbo].[TBL150]  WITH CHECK ADD  CONSTRAINT [ZERO_OR_ONE_1225214438] CHECK  (([chg_yn]='0' OR [chg_yn]='1'))
GO

ALTER TABLE [dbo].[TBL150] CHECK CONSTRAINT [ZERO_OR_ONE_1225214438]
GO

ALTER TABLE [dbo].[TBL150]  WITH CHECK ADD  CONSTRAINT [ZERO_OR_ONE_182519034] CHECK  (([need_yn]='0' OR [need_yn]='1'))
GO

ALTER TABLE [dbo].[TBL150] CHECK CONSTRAINT [ZERO_OR_ONE_182519034]
GO

ALTER TABLE [dbo].[TBL150]  WITH CHECK ADD  CONSTRAINT [ZERO_OR_ONE_509228330] CHECK  (([pk_yn]='0' OR [pk_yn]='1'))
GO

ALTER TABLE [dbo].[TBL150] CHECK CONSTRAINT [ZERO_OR_ONE_509228330]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table Columns ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL150'
GO


