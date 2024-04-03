USE [EPIC]
GO

/****** Object:  Table [dbo].[TBL100]    Script Date: 2023-10-07 ¿ÀÈÄ 1:25:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL100](
	[tbl_nm] [nvarchar](50) NOT NULL,
	[dic_cd] [varchar](50) NULL,
	[title] [nvarchar](50) NULL,
	[mdl_cd] [varchar](10) NULL,
	[sys_yn] [char](1) NOT NULL,
	[chg_yn] [char](1) NOT NULL,
	[use_yn] [char](1) NOT NULL,
	[dsc] [nvarchar](50) NULL,
	[cid] [int] NULL,
	[cdt] [datetime] NULL,
	[mid] [int] NULL,
	[mdt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[tbl_nm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TBL100] ADD  DEFAULT ('0') FOR [sys_yn]
GO

ALTER TABLE [dbo].[TBL100] ADD  DEFAULT ('0') FOR [chg_yn]
GO

ALTER TABLE [dbo].[TBL100] ADD  DEFAULT ('1') FOR [use_yn]
GO

ALTER TABLE [dbo].[TBL100]  WITH CHECK ADD CHECK  (([chg_yn]='0' OR [chg_yn]='1'))
GO

ALTER TABLE [dbo].[TBL100]  WITH CHECK ADD CHECK  (([sys_yn]='0' OR [sys_yn]='1'))
GO

ALTER TABLE [dbo].[TBL100]  WITH CHECK ADD CHECK  (([use_yn]='0' OR [use_yn]='1'))
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table Master' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBL100'
GO


