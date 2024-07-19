---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: 2024-05-19 22:58
MDT: 2024-05-28 13:04
---
---
#### Prologue / Concept
```C#
using (var db = new GaiaHelper())
{
	MdlUc ucInfo = db.GetUc(new { sys = SysCode, frm = FrmID, field = FldID }).SingleOrDefault();
	if (ucInfo != null)
	{
		this.Visible = (ucInfo.Show_chk == "0" ? false : true);
	}
}
```

```C#
MdlUc ucInfo = new MdlUcRepo().GetUc(SysCode, FrmID, FldID);
if (ucInfo != null)
{
	this.Visible = (ucInfo.Show_chk == "0" ? false : true);
}
```

#### Manifestation

#### Integration

###### REFERENCE


```C#
frmCtrl = new FrmCtrl();
frmCtrlRepo = new FrmCtrlRepo();
frmCtrlbs = new BindingList<FrmCtrl>(frmCtrlRepo.GetByFrwFrm(selectedFrwFrm.FrwId, selectedFrwFrm.FrmId));
gridControls.DataSource = frmCtrlbs;

frmWrk = new FrmWrk();
frmWrkRepo = new FrmWrkRepo();
frmWrkbs = new BindingList<FrmWrk>(frmWrkRepo.GetByFrwFrm(selectedFrwFrm.FrwId, selectedFrwFrm.FrmId));
gridWorkset.DataSource = frmWrkbs;
```


데이터 삭제
```C#
var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
if (view == null) return;
selectedDoc = view.GetFocusedRow() as FrmWrk;
```

GridControl 선택데이터
```C#
var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
if (view == null) return;
selectedDoc = view.GetFocusedRow() as FrmWrk;
```