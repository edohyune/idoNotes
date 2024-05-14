---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: 2024-05-03 12:22
MDT: 2024-05-03 15:59
---
---

```
FRWMST {
FrwId varchar(20)
FrwNm nvarchar(30)
Memo nvarchar(500)
Ver varchar(10)
PId varchar(20)
}
```

```C#
private string _FrwId;
public string FrwId
{
    get => _FrwId;
    set => Set(ref _FrwId, value);
}

private string _FrwNm;
public string FrwNm
{
    get => _FrwNm;
    set => Set(ref _FrwNm, value);
}

private string _Memo;
public string Memo
{
    get => _Memo;
    set => Set(ref _Memo, value);
}

private string _Ver;
public string Ver
{
    get => _Ver;
    set => Set(ref _Ver, value);
}

private string _PId;
public string PId
{
    get => _PId;
    set => Set(ref _PId, value);
}

```

```SQL
select a.FrwId, a.FrwNm, a.Memo, a.Ver, a.PId,
       a.CId, a.CDt, a.MId, a.Mdt
  from FRWMST a
 where 1=1
   and a.FrwId = @FrwId

insert into FRWMST
      (FrwId, FrwNm, Memo, Ver, PId,
       CId, CDt, MId, Mdt)
select @FrwId, @FrwNm, @Memo, @Ver, @PId,
       @CId, @CDt, @MId, @Mdt
update a
   set FrwId= @FrwId,
       FrwNm= @FrwNm,
       Memo= @Memo,
       Ver= @Ver,
       PId= @PId,
       MId= @MId,
       Mdt= @Mdt
  from FRWMST a
 where 1=1
   and FrwId = @FrwId_old

delete
  from FRWMST
 where 1=1
   and FrwId = @FrwId_old
```