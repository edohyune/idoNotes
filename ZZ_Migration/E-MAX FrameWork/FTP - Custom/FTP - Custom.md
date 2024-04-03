- TABLE
    
    ```SQL
    USE [BJC_DEV]
    GO
    
    /****** Object:  Table [dbo].[GLBFTP]    Script Date: 2023-10-26 오후 1:49:08 ******/
    SET ANSI_NULLS ON
    GO
    
    SET QUOTED_IDENTIFIER ON
    GO
    
    CREATE TABLE [dbo].[GLBFTP](
    	[fId] [varchar](50) NOT NULL,
    	[docId] [varchar](20) NULL,
    	[docSq] [int] NULL,
    	[refId] [varchar](20) NULL,
    	[refSq] [int] NULL,
    	[fPath] [varchar](100) NOT NULL,
    	[fNm] [nvarchar](200) NOT NULL,
    	[fSize] [int] NOT NULL,
    	[fExt] [varchar](10) NOT NULL,
    	[frmNm] [varchar](20) NULL,
    	[rmks] [nvarchar](200) NULL,
    	[cid] [int] NULL,
    	[cdt] [datetime] NOT NULL,
    	[mid] [int] NULL,
    	[mdt] [datetime] NULL,
    PRIMARY KEY CLUSTERED 
    (
    	[fId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Identity For FTP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'fId'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Related Identity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'docId'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Related Sequency' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'docSq'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Other Related Identity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'refId'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Other Related Sequency' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'refSq'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Path' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'fPath'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'fNm'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'fSize'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Extensions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'fExt'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Form Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'frmNm'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Remarks' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'rmks'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'생성자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'cid'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'생성일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'cdt'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'mid'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'수정일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP', @level2type=N'COLUMN',@level2name=N'mdt'
    GO
    
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FTP Templet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GLBFTP'
    GO
    ```
    
- Server Setting
    
    ![[ZZ_Files/Untitled 22.png|Untitled 22.png]]
    
    ![[ZZ_Files/Untitled 1 11.png|Untitled 1 11.png]]
    
    ![[ZZ_Files/Untitled 2 7.png|Untitled 2 7.png]]
    
    ![[ZZ_Files/Untitled 3 7.png|Untitled 3 7.png]]
    
- WorkSet
    
    glbftp_cuFtp
    
    ```SQL
    select a.docId, a.docSq, a.refId, a.refSq,
           a.fId, a.fPath, a.fNm, a.fSize, a.fExt,
           a.frmNm, a.rmks, 
           a.cid, a.cdt, a.mid, a.mdt,
           show = '', del = '', down = '', upload=''
      from GLBFTP a
     where a.docId=@docId
       and a.docSq=@docSq
       and a.frmNm=<$form_cd>
    ```
    
    glbftp_ftpInfo
    
    ```SQL
    declare @_extUser char(1) = '0'
    set @_extUser = (case when <$user_id> like 'kolao%' then '1' else '0' end)
    
    select cuFrmcd=a.frm_cd, 
           cuFtpid=b.ftp_id, 
           cuFtppwd=b.ftp_pwd, 
           cuFtproot=replace(concat('/',replace(a.ftp_path,'\','/'),'/'),'//','/'),
           cuLocalpath=a.tmp_path, 
           cuFtpip=(case when b.ftp_port='21' then concat('ftp://', (case when @_extUser='1' then b.dsc else b.ftp_ip end))
                        else concat('ftp://', (case when @_extUser='1' then b.dsc else b.ftp_ip end), ':', b.ftp_port)
                   end)
      from DCF100 a 
      join DCF150 b on a.ftp_cd=b.ftp_cd
     where a.frm_cd=<$form_cd>
    ```
    
    glbftp_g10
    
      
    

작업하는 방법

- WorkSet Copy
    
    ```SQL
    declare @cpyfrm    varchar(20) = 'FB100'
    declare @cpyinit   varchar(20) = 'fb100_ftpInfo'
    declare @cpygrid   varchar(20) = 'fb100_cuFtp'
    
    declare @frm       varchar(20) = ''------------------------------------Edit
    declare @frminit   varchar(20) = concat(lower(@frm),'_ftpInfo')
    declare @frmgrid   varchar(20) = concat(lower(@frm),'_cuFtp')
    
    exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpygrid, @frm, @frmgrid,
    		                             'FTP GRID', 'GRID', 'cuFtp'
    
    exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpyinit, @frm, @frminit,
    		                             'FTP Initialize', 'SQL', ''
    ```