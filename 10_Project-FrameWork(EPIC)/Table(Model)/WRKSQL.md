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

```SQL
WRKSQL {
FrwId varchar(20)
FrmId varchar(20)
WrkId varchar(50)
CRUDM char(1)
Query varchar(-1)
Memo varchar(-1)
Id bigint
PId bigint
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

private string _CRUDM;
public string CRUDM
{
    get => _CRUDM;
    set => Set(ref _CRUDM, value);
}

private string _Query;
public string Query
{
    get => _Query;
    set => Set(ref _Query, value);
}

private string _Memo;
public string Memo
{
    get => _Memo;
    set => Set(ref _Memo, value);
}

private long _Id;
public long Id
{
    get => _Id;
    set => Set(ref _Id, value);
}

private long _PId;
public long PId
{
    get => _PId;
    set => Set(ref _PId, value);
}

```

```SQL
select a.FrwId, a.FrmId, a.WrkId, a.CRUDM, a.Query,
       a.Memo, a.Id, a.PId, a.CId, a.CDt,
       a.MId, a.MDt
  from WRKSQL a
 where 1=1
   and a.CRUDM = @CRUDM
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
   
insert into WRKSQL
      (FrwId, FrmId, WrkId, CRUDM, Query,
       Memo, Id, PId, CId, CDt,
       MId, MDt)
select @FrwId, @FrmId, @WrkId, @CRUDM, @Query,
       @Memo, @Id, @PId, @CId, @CDt,
       @MId, @MDt
       
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       WrkId= @WrkId,
       CRUDM= @CRUDM,
       Query= @Query,
       Memo= @Memo,
       Id= @Id,
       PId= @PId,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from WRKSQL a
 where 1=1
   and CRUDM = @CRUDM_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and WrkId = @WrkId_old
delete
  from WRKSQL
 where 1=1
   and CRUDM = @CRUDM_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and WrkId = @WrkId_old
```


#### Integration
```SQL
if exists(select 1 from WRKSQL where FrwId=@FrwId and FrmId=@FrmId and WrkId=@WrkId and CRUDM=@CRUDM)
begin 
    update a
       set Query= @Query,
           Memo= @Memo,
           PId= @PId,
           MId= @MId,
           MDt= getdate()
      from WRKSQL a
     where 1=1
       and Id = @Id
end else begin 
    insert into WRKSQL
          (FrwId, FrmId, WrkId, CRUDM, Query,
           Memo, PId, CId, CDt,
           MId, MDt)
    select @FrwId, @FrmId, @WrkId, @CRUDM, @Query,
           @Memo, @PId, 
           @CId, getdate(), @MId, getdate()
end 
```



###### REFERENCE
