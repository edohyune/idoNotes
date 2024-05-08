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
폼을 로드 하게되면 우선 폼에 등록된 정보를 가지고 온다. 
컨트롤러 목록과 각각의 속성
#### Manifestation
테이블
CTRLMST - 사용중인 전체 컨트롤러 목록, 속성 변경이 가능한 것들...
컨트롤러 소속, 컨트롤러 이름, 컨트롤러의 종류, 컨테이너인가?, 맵핑지원할것인가?, 
```
CTRLMST {
CtrlId int
CtrlNm nvarchar(50)
CtrlGrpCd varchar(10)
CtrlRegNm varchar(200)
ContainYn bit
CustomYn bit
Rnd varchar(200)
Memo nvarchar(1000)
PId int
}
```
CtrlNm 
CtrlGrpCd [button, input, workset, container]
CtrlRegNm ["UCTextBox (Ctrls)"] - 시스템(도구상자 또는 )에서 인식하는 이름
ContainYn 컨테이너인지 아닌지
CustomYn 우리 컨트롤러 인지 아닌지
Rnd 같은 이름의 다른 버젼
Memo
PId 연관된 컨트롤러 ID
```SQL
select a.CtrlId, a.CtrlNm, a.CtrlGrpCd, a.CtrlRegNm, a.ContainYn,
       a.CustomYn, a.Rnd, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from CTRLMST a
 where 1=1
   and a.CtrlId = @CtrlId
   
insert into CTRLMST
      (CtrlId, CtrlNm, CtrlGrpCd, CtrlRegNm, ContainYn,
       CustomYn, Rnd, Memo, PId, CId,
       CDt, MId, MDt)
select @CtrlId, @CtrlNm, @CtrlGrpCd, @CtrlRegNm, @ContainYn,
       @CustomYn, @Rnd, @Memo, @PId, @CId,
       getdate(), @MId, getdate()
       
update a
   set CtrlId= @CtrlId,
       CtrlNm= @CtrlNm,
       CtrlGrpCd= @CtrlGrpCd,
       CtrlRegNm= @CtrlRegNm,
       ContainYn= @ContainYn,
       CustomYn= @CustomYn,
       Rnd= @Rnd,
       Memo= @Memo,
       PId= @PId,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from CTRLMST a
 where 1=1
   and CtrlId = @CtrlId_old
   
delete
  from CTRLMST
 where 1=1
   and CtrlId = @CtrlId_old
```

CTRLINC - 폼별 사용중인 컨트롤러 컨트롤러 맵핑의 기초 자료로 사용된다. 
실제 사용중인 폼의 컨트롤러

1안) 삭제 후 재등록을 기본으로 한다. 
2안) 비교하여 신규추가 및 기존 수정.


CTRLMAP - WorkSet 구성요소
#### Integration

###### REFERENCE

