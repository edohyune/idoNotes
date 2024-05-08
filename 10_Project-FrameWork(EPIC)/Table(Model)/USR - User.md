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
USRMST
```
USRMST {
Id int
UsrId varchar(50)
UsrNm nvarchar(50)
Pwd nvarchar(1024)
Cls varchar(20)
}
```

```C#
private int _Id;
public int Id
{
    get => _Id;
    set => Set(ref _Id, value);
}

private string _UsrId;
public string UsrId
{
    get => _UsrId;
    set => Set(ref _UsrId, value);
}

private string _UsrNm;
public string UsrNm
{
    get => _UsrNm;
    set => Set(ref _UsrNm, value);
}

private string _Pwd;
public string Pwd
{
    get => _Pwd;
    set => Set(ref _Pwd, value);
}

private string _Cls;
public string Cls
{
    get => _Cls;
    set => Set(ref _Cls, value);
}

```

```SQL
select a.Id, a.UsrId, a.UsrNm, a.Pwd, a.Cls,
       a.CId, a.CDt, a.MId, a.MDt
  from USRMST a
 where 1=1
   and a.Id = @Id

insert into USRMST
      (UsrId, UsrNm, Pwd, Cls,
       CId, CDt, MId, MDt)
select @UsrId, @UsrNm, @Pwd, @Cls,
       @CId, getdate(), @MId, getdate()

update a
   set UsrId= @UsrId,
       UsrNm= @UsrNm,
       Pwd= @Pwd,
       Cls= @Cls,
       MId= @MId,
       MDt= getdate()
  from USRMST a
 where 1=1
   and Id = @Id

delete
  from USRMST
 where 1=1
   and Id = @Id
```