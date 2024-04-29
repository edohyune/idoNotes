개요

새로운 솔루션을 구축하기 위한,  
두 가지 주요 관리 부분은 메타데이터 관리와 운영 데이터 관리이다.  

GAIA - 메타데이터 관리 시스템의 버전 및 기술 개발
SELENE - 메타데이터 관리 시스템  
Mars - 운영데이터 관리 시스템  
  
#### DATABASE 생성
- Image 분리형 Database 생성 [[Create Epic DataBase]]
#### Back Up
- MaintenancePlan에 DB 추가
	- [ ] MaintenancePlan추가 쿼리 만들기
- 스케쥴 등록 [[Database Backup Schedule]]
- Stored Procedure 백업 쿼리 추가 [[AP_PROC_SCRIPT_EPIC]]
- [ ] Table 백업 쿼리 추가
- [ ] Talbe Bulk Insert 준비
#### 기타 셋팅
- Linked Server 연결 [[Linked Server 연결]]
- 기본테이블 생성
	- [ ] Form Meta Tables
	- [ ] BASIC CODE
	- [ ] FTP


##### 아래를 참고해서 만들것
            ```SQL
            /*Basic Code*/
            select * from BCDMST
            --BCA100 
            --Master
            --main_cd varchar(5),
            --title nvarchar(50),
            --mdl_cd varchar(10),
            --grp_nm nvarchar(50),
            --sort_bc varchar(10),
            --user_yn char(1),
            --sys_yn char(1),
            --use_yn char(1),
            --popup_id int,
            --rmks nvarchar(50),
            --cid int,
            --cdt datetime,
            --mid int,
            --mdt datetime,
            select * from BCDDTL--BCA200 --Detail
            --base_cd varchar(10),
            --main_cd varchar(5),
            --sub_cd varchar(5),
            --title nvarchar(254),
            --ord_sq int,
            --user_yn char(1),
            --sys_yn char(1),
            --use_yn char(1),
            --old_cd varchar(20),
            --rmks nvarchar(1000),
            --cid int,
            --cdt datetime,
            --mid int,
            --mdt datetime,
            select * from BMNMST--BCA150 --Management Master - Type
            --main_cd varchar(5),
            --mng_sq int,
            --title nvarchar(50),
            --cid int,
            --cdt datetime,
            --mid int,
            --mdt datetime,
            select * from BMNDTL--BCA250 --Management Code
            --base_cd varchar(10),
            --mng_sq int,
            --mng_val nvarchar(500),
            /*Language Pack*/
            select * from BCDLMST--BCA100L --Master
            --main_cd varchar(5),
            --lan_id int,
            --nm nvarchar(100),
            select * from BCDLDTL--BCA200L --Detail
            --base_cd varchar(10),
            --lan_id int,
            --nm nvarchar(254),
            select * from BMNLMST--BCA150L --Management Master - Type
            --main_cd varchar(5),
            --mng_sq int,
            --lan_id int,
            --title nvarchar(100),
            select * from BMNLDTL--Management Code
            ```
            
        - TABLE Function
            
            - fnBCA100
                
                ```SQL
                USE [BJC_DEV]
                GO
                /****** Object:  UserDefinedFunction [dbo].[fnBCA100]    Script Date: 2023-10-17 오후 5:26:32 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                
                ALTER  FUNCTION [dbo].[fnBCA100]  (@lan_id		int, 
                									@reg_id		int)
                
                	returns @BCA100	table(
                			[main_cd] [varchar](5),
                			[title] [nvarchar](100),
                			[mdl_cd] [varchar](10),
                			[grp_nm] [nvarchar](100),
                			[sort_bc] [varchar](10),
                			[user_yn] [char](1),
                			[sys_yn] [char](1),
                			[use_yn] [char](1),
                			[popup_id] [int],
                			[rmks] [nvarchar](100),
                			[cid] [int],
                			[cdt] [datetime],
                			[mid] [int],
                			[mdt] [datetime],
                			[m1] [nvarchar](100),
                			[m2] [nvarchar](100),
                			[m3] [nvarchar](100),
                			[m4] [nvarchar](100),
                			[m5] [nvarchar](100),
                			[m6] [nvarchar](100),
                			[m7] [nvarchar](100),
                			[m8] [nvarchar](100),
                			[m9] [nvarchar](100),
                			[m10] [nvarchar](100),
                			[m11] [nvarchar](100),
                			[m12] [nvarchar](100),
                			[m13] [nvarchar](100),
                			[m14] [nvarchar](100),
                			[m15] [nvarchar](100),
                			[m16] [nvarchar](100),
                			[m17] [nvarchar](100),
                			[m18] [nvarchar](100),
                			[m19] [nvarchar](100),
                			[m20] [nvarchar](100)
                		)
                as
                
                begin
                
                	set @lan_id = dbo.fnLanNo(@lan_id);
                
                	declare		@sys_yn		char(1) = 0
                
                
                	if exists(select 0 from SCU100
                				where	reg_id = @reg_id
                				and 	usr_ty >= 'SC700800') --컨설턴트 이상, 개발자, 시스템관리자
                		set	@sys_yn = '1'
                
                	insert	into @BCA100
                			(main_cd, 
                			title, 
                			mdl_cd, grp_nm, sort_bc, user_yn, sys_yn, use_yn, popup_id, 
                			rmks, cid, cdt, mid, mdt, 
                			m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19, m20
                			)
                	select	a.main_cd, isnull(max(b.nm),a.title) as title, 
                			 a.mdl_cd, a.grp_nm, a.sort_bc, a.user_yn, a.sys_yn, a.use_yn, a.popup_id, 
                			a.rmks, a.cid,a.cdt, a.mid, a.mdt,
                			m1  = max(case when c.mng_sq = 1 then isnull(d.title,c.title) end),
                			m2  = max(case when c.mng_sq = 2 then isnull(d.title,c.title) end),
                			m3  = max(case when c.mng_sq = 3 then isnull(d.title,c.title) end),
                			m4  = max(case when c.mng_sq = 4 then isnull(d.title,c.title) end),
                			m5  = max(case when c.mng_sq = 5 then isnull(d.title,c.title) end),
                			m6  = max(case when c.mng_sq = 6 then isnull(d.title,c.title) end),
                			m7  = max(case when c.mng_sq = 7 then isnull(d.title,c.title) end),
                			m8  = max(case when c.mng_sq = 8 then isnull(d.title,c.title) end),
                			m9  = max(case when c.mng_sq = 9 then isnull(d.title,c.title) end),
                			m10 = max(case when c.mng_sq =10 then isnull(d.title,c.title) end),
                			m11 = max(case when c.mng_sq =11 then isnull(d.title,c.title) end),
                			m12 = max(case when c.mng_sq =12 then isnull(d.title,c.title) end),
                			m13 = max(case when c.mng_sq =13 then isnull(d.title,c.title) end),
                			m14 = max(case when c.mng_sq =14 then isnull(d.title,c.title) end),
                			m15 = max(case when c.mng_sq =15 then isnull(d.title,c.title) end),
                			m16 = max(case when c.mng_sq =16 then isnull(d.title,c.title) end),
                			m17 = max(case when c.mng_sq =17 then isnull(d.title,c.title) end),
                			m18 = max(case when c.mng_sq =18 then isnull(d.title,c.title) end),
                			m19 = max(case when c.mng_sq =19 then isnull(d.title,c.title) end),
                			m20 = max(case when c.mng_sq =20 then isnull(d.title,c.title) end)
                	from	dbo.BCA100 a
                			left join BCA100L b on a.main_cd = b.main_cd and b.lan_id = @lan_id
                			left join dbo.BCA150 c on a.main_cd = c.main_cd 
                			left join dbo.BCA150L d on a.main_cd = d.main_cd and c.mng_sq = d.mng_sq and d.lan_id = @lan_id
                	where 1 = case when @sys_yn = '1' then 1 else case when a.sys_yn <> '1' then 1 else 0 end end
                	group by a.main_cd, a.title, 
                			a.mdl_cd, a.grp_nm, a.sort_bc, a.user_yn, a.sys_yn, a.use_yn, a.rmks, a.cid, 
                			a.cdt, a.mid, a.mdt, a.popup_id
                	
                	return 
                
                end
                ```
                
            - fnBCA200
                
                ```SQL
                USE [BJC_DEV]
                GO
                /****** Object:  UserDefinedFunction [dbo].[fnBCA200]    Script Date: 2023-10-17 오후 5:28:03 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                
                ALTER  FUNCTION [dbo].[fnBCA200]  (@lan_id		int, 
                									@reg_id		int)
                	returns @BCA200	table(
                			[base_cd] [varchar](10),
                			[main_cd] [varchar](5),
                			[sub_cd] [varchar](5),
                			[title] [nvarchar](250),
                			[ord_sq] [int],
                			[user_yn] [char](1),
                			[sys_yn] [char](1),
                			[use_yn] [char](1),
                			[old_cd] [varchar](20),
                			[rmks] [nvarchar](1000),
                			[cid] [int],
                			[cdt] [datetime],
                			[mid] [int],
                			[mdt] [datetime],
                			[m1] [nvarchar](500),
                			[m2] [nvarchar](500),
                			[m3] [nvarchar](500),
                			[m4] [nvarchar](500),
                			[m5] [nvarchar](500),
                			[m6] [nvarchar](500),
                			[m7] [nvarchar](500),
                			[m8] [nvarchar](500),
                			[m9] [nvarchar](500),
                			[m10] [nvarchar](500),
                			[m11] [nvarchar](500),
                			[m12] [nvarchar](500),
                			[m13] [nvarchar](500),
                			[m14] [nvarchar](500),
                			[m15] [nvarchar](500),
                			[m16] [nvarchar](500),
                			[m17] [nvarchar](500),
                			[m18] [nvarchar](500),
                			[m19] [nvarchar](500),
                			[m20] [nvarchar](500)
                		)
                as
                
                begin
                
                	set @lan_id = dbo.fnLanNo(@lan_id);
                
                	declare		@sys_yn		char(1) = 0
                
                
                	if exists(select 0 from SCU100
                				where	reg_id = @reg_id
                				and 	usr_ty >= 'SC700800') --컨설턴트 이상, 개발자, 시스템관리자
                		set	@sys_yn = '1'
                
                	insert	into @BCA200
                			(base_cd, 
                			main_cd, sub_cd, 
                			title, 
                			ord_sq, user_yn, sys_yn, use_yn, old_cd, 
                			rmks, cid, cdt, mid, mdt, 
                			m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19, m20
                			)
                	select	a.base_cd, 
                			a.main_cd, a.sub_cd, 
                			isnull(max(b.nm),a.title) as title, 
                			a.ord_sq, a.user_yn, a.sys_yn, a.use_yn, a.old_cd, 
                			a.rmks, a.cid,a.cdt, a.mid, a.mdt,
                			m1  = max(case when c.mng_sq = 1 then c.mng_val end),
                			m2  = max(case when c.mng_sq = 2 then c.mng_val end),
                			m3  = max(case when c.mng_sq = 3 then c.mng_val end),
                			m4  = max(case when c.mng_sq = 4 then c.mng_val end),
                			m5  = max(case when c.mng_sq = 5 then c.mng_val end),
                			m6  = max(case when c.mng_sq = 6 then c.mng_val end),
                			m7  = max(case when c.mng_sq = 7 then c.mng_val end),
                			m8  = max(case when c.mng_sq = 8 then c.mng_val end),
                			m9  = max(case when c.mng_sq = 9 then c.mng_val end),
                			m10 = max(case when c.mng_sq =10 then c.mng_val end),
                			m11 = max(case when c.mng_sq =11 then c.mng_val end),
                			m12 = max(case when c.mng_sq =12 then c.mng_val end),
                			m13 = max(case when c.mng_sq =13 then c.mng_val end),
                			m14 = max(case when c.mng_sq =14 then c.mng_val end),
                			m15 = max(case when c.mng_sq =15 then c.mng_val end),
                			m16 = max(case when c.mng_sq =16 then c.mng_val end),
                			m17 = max(case when c.mng_sq =17 then c.mng_val end),
                			m18 = max(case when c.mng_sq =18 then c.mng_val end),
                			m19 = max(case when c.mng_sq =19 then c.mng_val end),
                			m20 = max(case when c.mng_sq =20 then c.mng_val end)
                	from	dbo.BCA200 a
                			left join BCA200L b on a.base_cd = b.base_cd and b.lan_id = @lan_id
                			left join dbo.BCA250 c on a.base_cd = c.base_cd 
                	where 1 = case when @sys_yn = '1' then 1 else case when a.sys_yn = '0' then 1 else 0 end end
                	group by a.base_cd, a.main_cd, a.sub_cd, a.title, 
                			a.ord_sq, a.user_yn, a.sys_yn, a.use_yn, a.old_cd, 
                			a.rmks, a.cid,a.cdt, a.mid, a.mdt
                	
                	return 
                
                end
                ```
                
            
              
            
        - Stored Procedures
            - BCA200_SAVE
                
                ```SQL
                USE [BJC_DEV]
                GO
                /****** Object:  StoredProcedure [dbo].[BCA200_Save]    Script Date: 2023-10-17 오후 5:29:57 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                
                /* *************************************************************** 
                   ID           : BCA200_Save
                   Description	: 기초코드 등록
                   Author		: b.h.song 
                   Date         : 2017.12.22       
                   Remak        : 소분류코드(sub_cd)가 자동부여일 경우에만 사용.
                   *************************************************************** */
                -- EXEC BCA200_Save '01','XXX',NULL,'소분류코드',1,'test'
                ALTER PROCEDURE [dbo].[BCA200_Save]
                                @co_cd      varchar(10),
                				@main_cd    varchar(5),
                			    @sub_cd     varchar(5),
                				@title      nvarchar(254),
                				@ord_sq     int,
                				@rmks       nvarchar(100),
                				@use_yn		char(01) = '1',
                				@reg_id		int      = null
                
                AS
                BEGIN
                
                	declare @_mng_title nvarchar(50) = '법인[AA100S]',
                	        @_sys_dt    datetime     = getdate()
                
                	declare @_base_cd    varchar(10),
                	        @_mng_sq     int  =  1
                
                	-------------------------------------------------
                	set nocount on
                	set transaction isolation level read uncommitted
                	-------------------------------------------------
                
                	-- 법인관리항목 검색 
                	select @_mng_sq = z1.mng_sq
                	  from BCA150 z1
                	 where z1.main_cd = @main_cd
                	   and z1.title   = @_mng_title
                	-----------------
                	if @@ROWCOUNT = 0
                	begin
                	    set @_mng_sq = 1
                
                		insert into BCA150
                			  (main_cd,  mng_sq,   title,    
                			   cid,      cdt, 	   mid,     mdt      )
                		values(@main_cd, @_mng_sq, @_mng_title, 
                		       @reg_id,  @_sys_dt, @reg_id, @_sys_dt )
                	end
                		
                 
                	-- 동일법인내 동일명칭 사용불가 --
                	if exists (select 0
                	             from BCA200 z2
                				inner join BCA250 z3 on z3.base_cd = z2.base_cd
                				                    and z3.mng_sq  = @_mng_sq
                									and z3.mng_val = @co_cd
                				where z2.main_cd = @main_cd
                				  and z2.title   = @title
                				  and z2.sub_cd <> isnull(@sub_cd,'')
                				)
                	begin
                		exec PutMessage 'MSG_FK_ERR', '이미 다른 곳에서 사용 중 입니다: @1', @title
                		return
                	end
                
                	set @_base_cd = @main_cd + @sub_cd
                
                	-- 수정 --
                	if @sub_cd > ''
                	begin
                		update BCA200
                		   set title  = @title,
                		       ord_sq = @ord_sq,
                			   rmks   = @rmks,
                			   use_yn = @use_yn,
                			   mid    = @reg_id,
                			   mdt    = @_sys_dt
                		 where base_cd = @_base_cd
                	end
                	-- 신규입력
                	else
                	begin
                
                		select @sub_cd = cast(cast(isnull(max(sub_cd),'10000') as int) + 1 as char(5)) -- 2016.10.06:b.h.song
                		  from BCA200 z2 
                		 where z2.main_cd = @main_cd
                		   and z2.sub_cd between '10001' and '99999'
                		   and len(sub_cd) = 5		
                
                		set @_base_cd = @main_cd + @sub_cd
                
                		insert into BCA200 
                			  (base_cd,   main_cd,  sub_cd,   title,    ord_sq,  rmks,  
                			   use_yn,    cid,      cdt,      mid,      mdt          )
                		values(@_base_cd, @main_cd, @sub_cd,  @title,   @ord_sq, @rmks,
                		       @use_yn,   @reg_id,  @_sys_dt, @reg_id,  @_sys_dt     )
                
                		insert into BCA250 
                			  (base_cd,   mng_sq,   mng_val)
                		values(@_base_cd, @_mng_sq, @co_cd )
                
                	end
                
                
                END
                ----------------------- ( end of procedure ) -----------------------------------
                ```
                
            - BCA200_Deleted
                
                  
                
                ```SQL
                USE [BJC_DEV]
                GO
                /****** Object:  StoredProcedure [dbo].[BCA200_Delete]    Script Date: 2023-10-17 오후 5:29:14 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                
                /*###################################################################################################
                 제         목  :  기초코드 삭제
                 사  용    D B  :  
                 VB PROGRAM     :  
                 작   성   자   :  JINSEE
                 작   성   일   :  2016.09.30
                 개	     요     :  
                 ###################################################################################################*/
                
                ALTER Procedure [dbo].[BCA200_Delete]  
                		@base_cd		varchar(20),  
                		@reg_id			int  
                 
                AS  
                Begin  
                
                	------------------------------------------------------ 필수  
                	Set Nocount On  
                	Set Transaction Isolation Level Read Uncommitted  
                	------------------------------------------------------  
                
                	if exists (select 0 from BCA200 
                				where base_cd = @base_cd and sys_yn = '1')
                	begin
                			exec PutMessage 'MSG_CHECK_SYSTEM', 'SYSTEM 자료는 수정/삭제할 수 없습니다', @base_cd
                			return
                	end
                
                	delete
                	from	BCA200L
                	where	base_cd = @base_cd
                
                	delete
                	from	BCA201
                	where	base_cd = @base_cd
                
                	delete
                	from	BCA250
                	where	base_cd = @base_cd
                
                	delete
                	from	BCA200
                	where	base_cd = @base_cd
                end
                ```
                
                  
                
            - BCA150_SAVE
                
                ```SQL
                USE BJC_DEV
                GO
                SET ANSI_NULLS, QUOTED_IDENTIFIER ON
                GO
                
                create	Procedure [dbo].[BCA150_Save]
                			    @main_cd		varchar(5),
                				@mng_sq			int,
                				@title			nvarchar(50),
                				@reg_id			int
                
                 WiTh
                
                  	  EnCrYpTion
                As
                begin
                
                	------------------------------------------------- 필수
                	set nocount on
                	set transaction isolation level read uncommitted
                	-------------------------------------------------
                
                	if (isnull(@main_cd,'') = '' or isnumeric(@mng_sq) = 0)
                		return
                
                	if exists(select 0
                				from BCA150
                				where	main_cd = @main_cd
                				and		mng_sq = @mng_sq)
                		begin
                			if isnull(@title,'') = ''
                				begin
                					if exists(select 0 from BCA150L
                							where main_cd = @main_cd
                							and		mng_sq = @mng_sq)
                						delete from BCA150L
                						where main_cd = @main_cd
                						and		mng_sq = @mng_sq
                
                						delete from BCA150
                						where main_cd = @main_cd
                						and		mng_sq = @mng_sq
                				end
                			else
                				if exists(select 0 from BCA150
                						where main_cd = @main_cd
                						and		mng_sq = @mng_sq
                						and		isnull(title,'') <> @title)
                					update BCA150
                					set title = @title,
                						mid = @reg_id, mdt = getdate()
                					where main_cd = @main_cd
                					and		mng_sq = @mng_sq
                		end
                	else
                		begin
                			if isnull(@title,'') = ''
                				return
                
                			insert into BCA150
                				(main_cd, mng_sq, 
                				 title, 
                				 cid, cdt, mid, mdt)
                			values(@main_cd, @mng_sq, 
                				   @title, 
                				   @reg_id, getdate(), @reg_id, getdate())
                		end
                end
                GO
                ```