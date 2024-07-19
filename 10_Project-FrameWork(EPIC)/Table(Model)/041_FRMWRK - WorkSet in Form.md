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
![[Pasted image 20240604224430.png]]
gvCtrl.OptionsFind.AlwaysVisible
gvCtrl.OptionsView.ShowGroupPanel
gvCtrl.OptionsView.ColumnAutoWidth
gvCtrl.OptionsView.EnableAppearanceEvenRow
gvCtrl.OptionsBehavior.Editable
gvCtrl.OptionsSelection.MultiSelect = true;
gvCtrl.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;




SelectMode - Multy, Single
MultySelect - RowSelect, CellSelect, CheckBoxRowSelect

```C#
FRMWRK {
FrwId varchar(20)
FrmId varchar(20)
WrkId varchar(50)
CtrlNm varchar(50)
WrkNm varchar(200)
WrkCd varchar(10)
SelectMode varchar(10)
MultiSelect varchar(10)
UseYn bit
NavAdd bit
NavDelete bit
NavSave bit
NavCancel bit
SaveSq int
OpenSq int
OpenTrg varchar(50)
Memo varchar(200)
}
```

```C#
private string _FrwId;
public string FrwId
{
    get => _FrwId;
    set => Set(ref _FrwId, value);
}

private string _FrmId;
public string FrmId
{
    get => _FrmId;
    set => Set(ref _FrmId, value);
}

private string _WrkId;
public string WrkId
{
    get => _WrkId;
    set => Set(ref _WrkId, value);
}

private string _CtrlNm;
public string CtrlNm
{
    get => _CtrlNm;
    set => Set(ref _CtrlNm, value);
}

private string _WrkNm;
public string WrkNm
{
    get => _WrkNm;
    set => Set(ref _WrkNm, value);
}

private string _WrkCd;
public string WrkCd
{
    get => _WrkCd;
    set => Set(ref _WrkCd, value);
}

private string _SelectMode;
public string SelectMode
{
    get => _SelectMode;
    set => Set(ref _SelectMode, value);
}

private string _MultiSelect;
public string MultiSelect
{
    get => _MultiSelect;
    set => Set(ref _MultiSelect, value);
}

private bool _UseYn;
public bool UseYn
{
    get => _UseYn;
    set => Set(ref _UseYn, value);
}

private bool _NavAdd;
public bool NavAdd
{
    get => _NavAdd;
    set => Set(ref _NavAdd, value);
}

private bool _NavDelete;
public bool NavDelete
{
    get => _NavDelete;
    set => Set(ref _NavDelete, value);
}

private bool _NavSave;
public bool NavSave
{
    get => _NavSave;
    set => Set(ref _NavSave, value);
}

private bool _NavCancel;
public bool NavCancel
{
    get => _NavCancel;
    set => Set(ref _NavCancel, value);
}

private int _SaveSq;
public int SaveSq
{
    get => _SaveSq;
    set => Set(ref _SaveSq, value);
}

private int _OpenSq;
public int OpenSq
{
    get => _OpenSq;
    set => Set(ref _OpenSq, value);
}

private string _OpenTrg;
public string OpenTrg
{
    get => _OpenTrg;
    set => Set(ref _OpenTrg, value);
}

private string _Memo;
public string Memo
{
    get => _Memo;
    set => Set(ref _Memo, value);
}

```

```SQL
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkId = @WrkId

insert into FRMWRK
      (FrwId, FrmId, WrkId, CtrlNm, WrkNm,
       WrkCd, SelectMode, MultiSelect, UseYn, NavAdd,
       NavDelete, NavSave, NavCancel, SaveSq, OpenSq,
       OpenTrg, Memo, 
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @CtrlNm, @WrkNm,
       @WrkCd, @SelectMode, @MultiSelect, @UseYn, @NavAdd,
       @NavDelete, @NavSave, @NavCancel, @SaveSq, @OpenSq,
       @OpenTrg, @Memo, 
       @CId, @CDt, @MId, @MDt

update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       WrkId= @WrkId,
       CtrlNm= @CtrlNm,
       WrkNm= @WrkNm,
       WrkCd= @WrkCd,
       SelectMode= @SelectMode,
       MultiSelect= @MultiSelect,
       UseYn= @UseYn,
       NavAdd= @NavAdd,
       NavDelete= @NavDelete,
       NavSave= @NavSave,
       NavCancel= @NavCancel,
       SaveSq= @SaveSq,
       OpenSq= @OpenSq,
       OpenTrg= @OpenTrg,
       Memo= @Memo,
       MId= @MId,
       MDt= @MDt
  from FRMWRK a
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId
   and WrkId = @WrkId

delete
  from FRMWRK
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId
   and WrkId = @WrkId
```