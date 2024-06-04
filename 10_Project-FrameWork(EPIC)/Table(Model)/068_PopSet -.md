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

```SQL
select a.FrwId, a.FrmId, a.PopId, a.FldNm, a.SetPopId,
       a.SetFldNm, a.SetDefaultValue, a.SqlId, a.Id, a.Pid,
       a.CId, a.CDt, a.MId, a.MDt
  from POPSET a
 where 1=1
   and a.FldNm = @FldNm
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.PopId = @PopId
insert into POPSET
      (FrwId, FrmId, PopId, FldNm, SetPopId,
       SetFldNm, SetDefaultValue, SqlId, Id, Pid,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @PopId, @FldNm, @SetPopId,
       @SetFldNm, @SetDefaultValue, @SqlId, @Id, @Pid,
       @CId, @CDt, @MId, @MDt
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       PopId= @PopId,
       FldNm= @FldNm,
       SetPopId= @SetPopId,
       SetFldNm= @SetFldNm,
       SetDefaultValue= @SetDefaultValue,
       SqlId= @SqlId,
       Id= @Id,
       Pid= @Pid,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from POPSET a
 where 1=1
   and FldNm = @FldNm_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and PopId = @PopId_old
delete
  from POPSET
 where 1=1
   and FldNm = @FldNm_old
   and FrmId = @FrmId_old
   and FrwId = @FrwId_old
   and PopId = @PopId_old
```