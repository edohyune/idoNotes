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

