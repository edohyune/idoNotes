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
    CUSTOMER {
            string name
            string custNumber
            string sector
        }
---
#### Prologue / Concept
폼을 로드 하게되면 우선 폼에 등록된 정보를 가지고 온다. 
컨트롤러 목록과 각각의 속성
#### Manifestation

Form을 Load하여 Form에 사용된 컨트롤러 목록을 가져와서 FRMCTRL에 저장
UCGrid가 있는지
UCField가 있는지 

WorkSet 
	1. GridSet
		1. UCGrid가 있고 쿼리를 이용하여 컬럼을 정의하고 맵핑한다.
		2. 컨트롤러가 없으면 정의할 수 없다.
	2. FieldSet
		1. UCField가 있고 쿼리의 컬럼과 필드를 맵핑한다. 
		2. 컨트롤러가 없으면 정의할 수 없다.
	3. DataSet
		1. DataSet WorkSet을 신규로 생성하고 쿼리를 정의한다.
Not WorkSet
	WorkSet(특히 FieldSet)에 포함되지 않은 input Controller
	Custom된 Container(UCPanel)

컨트롤러를 셋팅한다.

##### CTRLCLS
CTRLMST - 사용중인 전체 컨트롤러 목록이다. 
프로그램에서 컨트롤러를 사용하기 위해서는 여기에 우선 등록한다. 
등록의 목적은 컨트롤러 별로 관리해야할 이슈가 있기 때문이다. 

CtrlId - 컨트롤러 고유번호 
CtrlNm - 컨트롤러 이름
CtrlGrpCd - 컨트롤러의 종류 (button, input, workset, container) 
CtrlRegNm - 컨트롤러 소속 NameSpace
ContainYn 컨테이너인지 아닌지
CustomYn 우리 컨트롤러 인지 아닌지 
Rnd - 버젼별 정리
Memo - 
PId - 연관된(선행버전) 컨트롤러 ID

```
CtrlCls {
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

##### FRMCTRL
FrmCtrl - 폼별 사용중인 컨트롤러 컨트롤러 맵핑의 기초 자료로 사용된다. 
실제 사용중인 폼의 컨트롤러

1안) 삭제 후 재등록을 기본으로 한다. 
-- 2안) 비교하여 신규추가 및 기존 수정.

```
FRMCTRL {
FrmId varchar(20)
CtrlNm nvarchar(50)
ToolNm varchar(20)

}


```

```C#
private string _FrmId;
public string FrmId
{
    get => _FrmId;
    set => Set(ref _FrmId, value);
}

private string _CtrlNm;
public string CtrlNm
{
    get => _CtrlNm;
    set => Set(ref _CtrlNm, value);
}

private string _ToolNm;
public string ToolNm
{
    get => _ToolNm;
    set => Set(ref _ToolNm, value);
}

private int _CtrlW;
public int CtrlW
{
    get => _CtrlW;
    set => Set(ref _CtrlW, value);
}

private int _CtrlH;
public int CtrlH
{
    get => _CtrlH;
    set => Set(ref _CtrlH, value);
}

private string _TitleText;
public string TitleText
{
    get => _TitleText;
    set => Set(ref _TitleText, value);
}

private string _TitleAlign;
public string TitleAlign
{
    get => _TitleAlign;
    set => Set(ref _TitleAlign, value);
}

private bool _VisibleYn;
public bool VisibleYn
{
    get => _VisibleYn;
    set => Set(ref _VisibleYn, value);
}

private bool _ReadonlyYn;
public bool ReadonlyYn
{
    get => _ReadonlyYn;
    set => Set(ref _ReadonlyYn, value);
}
```
```SQL
select a.FrmId, a.CtrlNm, a.ToolNm, a.CtrlW, a.CtrlH,
       a.TitleText, a.TitleAlign, a.VisibleYn, a.ReadonlyYn, a.CId,
       a.CDt, a.MId, a.MDt
  from FRMCTRL a
 where 1=1
   and a.FrmId = @FrmId 
   and a.CtrlNm = @CtrlNm
 
insert into FRMCTRL
      (FrmId, CtrlNm, ToolNm, CtrlW, CtrlH,
       TitleText, TitleAlign, VisibleYn, ReadonlyYn, CId,
       CDt, MId, MDt)
select @FrmId, @CtrlNm, @ToolNm, @CtrlW, @CtrlH,
       @TitleText, @TitleAlign, @VisibleYn, @ReadonlyYn, @CId,
       @CDt, @MId, @MDt
       
update a
   set FrmId= @FrmId,
       CtrlNm= @CtrlNm,
       ToolNm= @ToolNm,
       CtrlW= @CtrlW,
       CtrlH= @CtrlH,
       TitleText= @TitleText,
       TitleAlign= @TitleAlign,
       VisibleYn= @VisibleYn,
       ReadonlyYn= @ReadonlyYn,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from FRMCTRL a
 where 1=1
 
delete
  from FRMCTRL
 where 1=1
```
FrmId - FRMLOD
ToolNm - UCTextBox
CtrlNm - uctextbox1

PropWidth
Prop



FRMWRK
```
FRMWRK {
FrmId varchar(20)
CtrlNm nvarchar(50)
ToolNm varchar(20)


}
```
##### CTRLMAP - WorkSet 구성요소
#### Integration

###### REFERENCE



