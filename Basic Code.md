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
![[Pasted image 20240601183006.png]]

BCA200
SCL100
SCL150
SCU100
BCA100L
BCA150
BCA150L
BCA200L
BCA250

#### Prologue / Concept
![[Pasted image 20240601150433.png]]
###### system_codes	기초코드 Combo
```SQL
if exists(select top 1 0 from BCA200 where main_cd = @main_cd)
			select	a.base_cd, dbo.fnBaseNm(a.base_cd,<$lan_no>) as nm
			from	BCA200 a
			where	a.main_cd = @main_cd
			order by isnull(a.use_yn,9999), a.main_cd, a.ord_sq, a.sub_cd
else
		begin
				declare	@qry	nvarchar(500)
				select @qry	= b.combo_sql
				from	SCL100 a
						join SCL150 b on a.popup_id = b.popup_id
				where a.sub_cd = @main_cd

				if isnull(@qry,'') <> ''
					begin
						set @qry = dbo.fnQueryMacroReplace(@qry,<$co_cd>,<$bs_cd>,<$sys_cd>,<$reg_id>,<$lan_no>)
						exec(@qry)
					end
				else
						select '', ''
		end
```

###### system_codes_g1	기초코드대분류
```C#
declare	@_lan_id	int = case when isnumeric(<$lan_no>) = 1 then <$lan_no> end

select a.main_cd, a.title, 
		--a.main_nm1, a.main_nm2, a.main_nm3, a.main_nm4, a.main_nm5, 
		a.mdl_cd, a.grp_nm, a.sort_bc, a.user_yn, a.sys_yn, a.use_yn, 
		a.rmks, a.cid, a.cdt, a.mid, a.mdt, a.popup_id,
		a.m1, a.m2, a.m3, a.m4, a.m5, a.m6, a.m7, a.m8, a.m9, a.m10, a.m11, a.m12, a.m13, a.m14, a.m15, a.m16, a.m17, a.m18, a.m19, a.m20
from dbo.fnBCA100(@_lan_id, <$reg_id>) a
		left join SCL100 z on z.sub_cd = a.main_cd
where	1=1
andif	(a.main_cd like '%' + @main_cd + '%' or a.title like '%' + @main_cd + '%')	endif
andif	a.title like '%' + @main_nm + '%'	endif
andif	a.mdl_cd = @mdl_cd 			endif
andif	z.popup_id = @popup_id		endif
order by (case when a.use_yn is null or a.use_yn = 0 then 9999 end), a.main_cd

```

###### system_codes_g2	기초코드
```C#
select	a.base_cd, a.main_cd, a.sub_cd, a.title,
				a.ord_sq, a.user_yn, a.sys_yn, a.use_yn, a.old_cd, a.rmks, a.cid, a.cdt, a.mid, a.mdt,
				a.m1, a.m2, a.m3, a.m4, m5, a.m6, a.m7, a.m8, a.m9, a.m10,	
				a.m11, a.m12, a.m13, a.m14, a.m15, a.m16, a.m17, a.m18, a.m19, a.m20,

				cbo1  = a.m1,  cbo2  = a.m2,  cbo3  = a.m3,  cbo4  = a.m4,  cbo5  = a.m5,
				cbo6  = a.m6,  cbo7  = a.m7,  cbo8  = a.m8,  cbo9  = a.m9,  cbo10 = a.m10,
				cbo11 = a.m11, cbo12 = a.m12, cbo13 = a.m13, cbo14 = a.m14, cbo15 = a.m15,
				cbo16 = a.m16, cbo17 = a.m17, cbo18 = a.m18, cbo19 = a.m19, cbo20 = a.m20,

				chk1  = a.m1,  chk2  = a.m2,  chk3  = a.m3,  chk4  = a.m4,  chk5  = a.m5,
				chk6  = a.m6,  chk7  = a.m7,  chk8  = a.m8,  chk9  = a.m9,  chk10 = a.m10,
				chk11 = a.m11, chk12 = a.m12, chk13 = a.m13, chk14 = a.m14, chk15 = a.m15,
				chk16 = a.m16, chk17 = a.m17, chk18 = a.m18, chk19 = a.m19, chk20 = a.m20

from	dbo.fnBCA200(<$lan_no>,<$reg_id>) a
where	a.main_cd = @main_cd
order by isnull(a.ord_sq,99999), a.sub_cd

```

###### system_codes_g3	Detail List
```C#
declare	@_lan_id	int = case when isnumeric(<$lan_no>) = 1 then <$lan_no> end

select *
from (
select mdl_cd = case when isnull(a.mdl_cd,'') = '' then
							'SC100' + left(a.main_cd,2)
						else
							a.mdl_cd 
						end,
		a.grp_nm, 
		a.main_cd, a.title as main_nm, --a.sort_bc, a.user_yn, a.sys_yn,
		b.sub_cd,  b.title as sub_nm, b.ord_sq
from 	dbo.fnBCA100(@_lan_id, <$reg_id>) a
		join dbo.fnBCA200(<$lan_no>,<$reg_id>) b on b.main_cd = a.main_cd and b.use_yn = '1'
where	a.use_yn = '1'
andif	a.main_cd like @main_cd + '%'	endif
andif	a.mdl_cd = @mdl_cd 			endif
)  a
order by a.mdl_cd, a.main_cd, isnull(a.ord_sq,99999), a.sub_cd

```

![[Pasted image 20240601164350.png]]
#### Manifestation

#### Integration

###### REFERENCE
