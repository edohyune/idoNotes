---
CDT: 2024-03-28 06:25
---
---
Form					 	ZTA100
	Controls					
		Grid				
			TreeGrid			
			PivotGrid			
			Grid			
		Text				
		Date				
		Combo		LookUpEdit		
		CheckCombo				
		Check				
		Radio				
		Button				
		Tree				
		Split				
		Panel				
	WorkSet					ZTA200
		GridSet				
			TreeGrid			
			PivotGrid			
			Grid			ZTA300
				Head		
				Column		ZTA310
					Text	
					Date	
					Combo(PopUp)	
					Date	
					Check	
					Button	
						
				Foot		
		FieldSet				ZTA300
			All Controls			ZTA310
		SQLSet				

#### Form
##### FRMMST
[[FRM -  Form]]

ZTA100	Form		Id, Sys_cd, Frm_id, Frm_nm, Frm_ty, Hide_chk, Use_chk, Memo
#### Controller
##### CTRLMST
- [ ] CTRLMST 나중에 관리할 것
사용중인 컨트롤러가 어떤 것이 있는지 관리
##### CTRLINC
FRMMST이 가지고 있는 모든 컨트롤러 목록
컨트롤이 가능한 것  
컨트롤이 불가능한 것

```
ZTA300	Control		Id, Sys_cd, Frm_id, Ctrl_id, Ctrl_ty, CtrlW, CtrlH, Title, TitleAlign, Show_chk, Edit_chk, Wkset_id, Wkset_ty
```
Controller Master 에서 워크셋이면 WorkSet에 저장

#### WorkSet
ZTA200	WorkSet		Id, Sys_cd, Frm_id, Wkset_id, Wkset_ty, Wkset_nm, New_chk, Delete_chk, Update_chk, Use_chk, Edit_chk, ShowFooter_chk, ShowGroupPanel_chk, OptionsFind_chk, ColumnAutoWidth_chk, EvenRow_chk, Memo
##### WRKMST
GridSet
	Form의 Controller가 등록될때 Custom이고 UCGrid이면 등록
FieldSet
	Form의 Controller가 등록될때 Custom이고 UCField이면 등록
DataSet
	필요에 의해 등록한다. 
	Pull, Push가 있으면 FreeForm의 역할을 수행하고 없으면 DataSet을 반환한다.
#### Controller Properties
##### CTRLPRT
ZTA310	Control Properties		Id, Sys_cd, Wkset_id, Wkset_ty, Frm_id, Ctrl_id, Ctrl_ty, Field_ty, Title, TitleAlign, TitleW, Popup, Txt, TxtAlign, Fix_chk, Group_chk, Show_chk, Need_chk, Edit_chk, Band1, Band2, Sum_ty, Format_ty, Color_bg, Color_fore, Seq

#### Query 
ZTA400	CRUD Query		Sys_cd, Frm_id, Wkset_id, CRUD, Sql_txt, Memo
ZTA500			
##### SQLMST


#### Parameters
ZTA31Pull	Pull Parameters		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid
ZTA31Push	Push Parameters		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid
ZTA31Ref	Save See		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid

ZTA50Pull			
ZTAUSR		