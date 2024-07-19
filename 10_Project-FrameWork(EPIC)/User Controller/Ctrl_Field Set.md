---
Start Date:
Status:
Concept: false
Manifestation: false
Integration: false
Done:
tags:
CDT: 2024-05-14 11:36
---
---
#### Prologue / Concept


**1. FieldSet의 Open() 메소드의 목적과 주요 기능은 무엇인가요?**
데이터베이스에서 데이터를 가져와서 UI 컨트롤과 바인딩하는 기능
- 데이터베이스에서 `Select` 쿼리를 실행하여 데이터를 가져옵니다.
- 데이터를 모델로 변환합니다.
- 모델의 데이터를 각 UI 컨트롤과 바인딩합니다.

**2. FieldSet의 Open() 메소드에서 어떤 데이터 소스를 사용하나요?**
var sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = "R" });으로 데이터소스를 가져올 SQL을 동적으로 불러오고 SQL 실행결과를 바인딩한다. 
**3. FieldSet의 Open() 메소드에서 UI 컨트롤을 어떻게 초기화하나요?**
Select쿼리의 컬럼 정보를 추출(다른 프로그램)하여 WRKFLD 개체를 생성한다. 
바인딩 될 UI 컨트롤의 목록은 WRKFLD Table에 있고 WRKFLD 모델을 사용하면 된다. 

**4. FieldSet의 Open() 메소드에서 데이터를 UI 컨트롤과 어떻게 바인딩하나요?**
바인딩 정보는 WRKSET모델에 정의되어있다.
**5. FieldSet의 Open() 메소드에서 예외 처리는 어떻게 할 건가요?**




첨부된 파일에서 UCFieldSet을 완성할꺼야.
먼저 Open()메소드의 구성을 어떻게 할지 UCGridNav(GridSet)를 참고해서 제안해줘
그리고 주요하게 고려해야할 사항은 FrmBase에서 Open()을 통해 제어되어야 한다는 것이다. 


데이터 바인딩을 위한 두가지 이슈

컬럼 및 컬럼에 맵핑되는 데이터를 쉽게 연결하기 위해 ORM을 사용  
ORM을 사용하면 강력한 모델을 사용해야 한다.  

강력한 모델을 사용하려면 Select되는 컬럼의 변화에도 모델을 수정해야하는 번거로움이 발생한다.

UCField의 바인딩소스에는 CRUD 쿼리가 할당대고 특정 상황에 실행된다. 
하나의 UCField의 R(Select)된 데이터는 모델을 생성하여 CUD에 활용된다. 
R(Select)된 데이터 가령 예를 들어 
```
select CustId, CustNm, CustAge
  from custTbl
```
컬럼 CustId, CustNm, CustAge는 각각 txtCustId, txtCustNm, txtCustAge 택스트 박스와 바인딩 되어 사용된다. 
  
  
이부분이 나의 개념과 약간 다르다. 
UCField 컨트롤러의 역할은 Form에 존재하는 Controller와 모델과 Table을 연결하는 역할을 한다. 

WinForm CustInfo
TextBox txtcustId = new TextBox();
TextBox txtcustNm = new TextBox();
TextBox txtcustAge = new TextBox();
UCField custFieldSet = new UCField());


custFieldSet.DataSource = custFieldSet.Open<Cust>(); // read
저장된  select CustId, CustNm, CustAge  from custTbl 를 동적으로 불러와 
FrmMapping 테이블에 저장된 바인딩 연결 정보를 이용해 
txtcustId-CustId
txtcustNm-CustNm
txtcustAge-CustAge
  
custFieldSet.Save<Cust>(); // insert , update
custFieldSet.Delete<Cust>(); // delete


Database에 저장되 R Query

R Query 를 저장할때 모델을 생성해서  빌드한다. 
public class Cust { public int CustId { get; set; } public string CustNm { get; set; } public int CustAge { get; set; } }

Form에 존재하는 필드를 읽어 

FrmMapping 테이블에 
txtcustId-CustId
txtcustNm-CustNm
txtcustAge-CustAge

정보를 이용해 동적으로 아래의 바인딩을 수행하다. 

txtCustId.DataBindings.Add("Text", custIdField, "Value");
txtCustNm.DataBindings.Add("Text", custNmField, "Value");
txtCustAge.DataBindings.Add("Text", custAgeField, "Value");









#### Manifestation

#### Integration

###### REFERENCE

