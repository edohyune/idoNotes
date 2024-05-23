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

WRKFLD
```SQL
WRKSET {
FrwId varchar(20)
FrmId varchar(20)
WrkId varchar(50)
CtrlNm varchar(50)
ParamWrk int
ParamName int
ParamValue int
Id bigint
Pid bigint
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

private int _ParamWrk;
public int ParamWrk
{
    get => _ParamWrk;
    set => Set(ref _ParamWrk, value);
}

private int _ParamName;
public int ParamName
{
    get => _ParamName;
    set => Set(ref _ParamName, value);
}

private int _ParamValue;
public int ParamValue
{
    get => _ParamValue;
    set => Set(ref _ParamValue, value);
}

private long _Id;
public long Id
{
    get => _Id;
    set => Set(ref _Id, value);
}

private long _Pid;
public long Pid
{
    get => _Pid;
    set => Set(ref _Pid, value);
}

```

```SQL
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.ParamWrk,
       a.ParamName, a.ParamValue, a.Id, a.Pid, a.CId,
       a.CDt, a.MId, a.MDt
  from WRKSET a
 where 1=1
   and a.CtrlNm = @CtrlNm
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
   
insert into WRKSET
      (FrwId, FrmId, WrkId, CtrlNm, ParamWrk,
       ParamName, ParamValue, Id, Pid, CId,
       CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @CtrlNm, @ParamWrk,
       @ParamName, @ParamValue, @Id, @Pid, 
       @CId, @CDt, @MId, @MDt
       
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       WrkId= @WrkId,
       CtrlNm= @CtrlNm,
       ParamWrk= @ParamWrk,
       ParamName= @ParamName,
       ParamValue= @ParamValue,
       Id= @Id,
       Pid= @Pid,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from WRKSET a
 where 1=1
   and CtrlNm = @CtrlNm_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and WrkId = @WrkId_old
delete
  from WRKSET
 where 1=1
   and CtrlNm = @CtrlNm_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and WrkId = @WrkId_old
```


#### Integration

###### REFERENCE
