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


ZTA100	Form		Id, Sys_cd, Frm_id, Frm_nm, Frm_ty, Hide_chk, Use_chk, Memo
ZTA200	WorkSet		Id, Sys_cd, Frm_id, Wkset_id, Wkset_ty, Wkset_nm, New_chk, Delete_chk, Update_chk, Use_chk, Edit_chk, ShowFooter_chk, ShowGroupPanel_chk, OptionsFind_chk, ColumnAutoWidth_chk, EvenRow_chk, Memo
ZTA300	Control		Id, Sys_cd, Frm_id, Ctrl_id, Ctrl_ty, CtrlW, CtrlH, Title, TitleAlign, Show_chk, Edit_chk, Wkset_id, Wkset_ty
ZTA310	Control Properties		Id, Sys_cd, Wkset_id, Wkset_ty, Frm_id, Ctrl_id, Ctrl_ty, Field_ty, Title, TitleAlign, TitleW, Popup, Txt, TxtAlign, Fix_chk, Group_chk, Show_chk, Need_chk, Edit_chk, Band1, Band2, Sum_ty, Format_ty, Color_bg, Color_fore, Seq
ZTA31Pull	Pull Parameters		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid
ZTA31Push	Push Parameters		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid
ZTA31Ref	Save See		Id, Sys_cd, Frm_id, Wkset_id, Ctrl_id, Param_wkset, Param_name, Param_value, Pid
ZTA400	CRUD Query		Sys_cd, Frm_id, Wkset_id, CRUD, Sql_txt, Memo
ZTA500			
ZTA50Pull			
ZTAUSR		