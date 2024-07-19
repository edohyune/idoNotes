---
Start Date: 2024-06-10
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-10
tags: 
CDT: 2024-06-10 15:26
MDT: 2024-06-10 15:34
---
---
#### Prologue / Concept

#### Manifestation

#### Integration

###### REFERENCE




![[Syntax Match]]



### 1. 컬럼 정보 추출 ([[Extract - Column]])

Select 쿼리에서 사용된 컬럼 정보를 추출하는 것은 쿼리의 실행 계획을 수립하고, 필요한 데이터를 정확하게 가져오기 위해 중요합니다. 이를 통해 쿼리의 성능을 최적화할 수 있습니다.

### 2. 변수 정보 추출 ([[Extract - Variable (Class Syntax)]])

쿼리에 사용된 변수 정보를 추출하는 것은 쿼리의 실행에 필요한 파라미터를 설정하는 데 필수적입니다. 이 변수들은 쿼리 실행 전에 적절한 값으로 설정되어야 합니다.

### 3. GPatternMatch 및 OPatternMatch

- **GPatternMatch (for <$변수>)**:
    - <$변수> 형태의 변수는 `Lib.Common`의 개체에서 값을 가져옵니다.
    - 예: `<$userId>`는 `Lib.Common`의 `userId` 필드에서 값을 가져옵니다.
- **OPatternMatch (for @변수)**:
    - @변수 형태의 변수는 다른 워크셋의 컬럼이나 컨트롤러에서 값을 가져옵니다.
    - 예: `@orderId`는 다른 워크셋의 `orderId` 컬럼이나 특정 컨트롤러에서 값을 가져옵니다.

### 4. 파라미터의 역할

쿼리에 사용되는 파라미터는 각기 다른 역할을 수행합니다.

- **Get Parameters** (Select):
    - Select 쿼리에서 데이터를 가져오기 위한 파라미터입니다.
    - 예: `SELECT * FROM Orders WHERE orderId = @orderId`
- **Set Parameters** (Controller에 Select 값을 전달):
    - Select 쿼리 결과를 컨트롤러에 전달하는 파라미터입니다.
    - 예: `controller.SetParameter("orderDate", selectResult.orderDate)`
- **Reference Parameters** (다른 워크셋의 컬럼, 또는 Controller에서 값을 가져옴):
    - Insert, Update, Delete 쿼리에서 다른 워크셋의 컬럼이나 컨트롤러에서 값을 가져오는 파라미터입니다.
    - 예: `INSERT INTO Orders (userId, productId) VALUES (<$userId>, @productId)`

### 예제

#### 컬럼 및 변수 정보 추출

```SQL
Select 쿼리 예제 
-- Select 쿼리 예제 
SELECT orderId, orderDate, userId FROM Orders WHERE status = @status AND region = <$region>`
```

#### GPatternMatch 및 OPatternMatch 사용

```C#
GPatternMatch: <$region> 변수의 값을 Lib.Common에서 가져오기 
string region = Lib.Common.GetValue("region");

OPatternMatch: @status 변수의 값을 다른 워크셋이나 컨트롤러에서 가져오기 
string status = otherWorkSet.GetColumnValue("status");
```


#### 파라미터 역할 구분
```C#
Get Parameters: Select 쿼리 실행을 위한 파라미터 설정 
string selectQuery = "SELECT orderId, orderDate, userId FROM Orders WHERE status = @status AND region = <$region>";  

Set Parameters: Select 쿼리 결과를 컨트롤러에 전달 
var result = ExecuteSelectQuery(selectQuery); 
controller.SetParameter("orderId", result["orderId"]); 
controller.SetParameter("orderDate", result["orderDate"]); 
controller.SetParameter("userId", result["userId"]);  
Reference Parameters: Insert 쿼리에서 다른 워크셋의 값을 사용 

string insertQuery = "INSERT INTO Orders (userId, productId) VALUES (<$userId>, @productId)"; ExecuteInsertQuery(insertQuery);
```




SQL 문장은 구조적 문장과 동적 문장으로 구분한다. 
구조적문장(Structural Components)에는 `SELECT`, `FROM`, `WHERE`, `DECLARE @_변수` 등이 있다. 
동적문장(Dynamic Components)을 구성하는 것은 값을 참조 해오거나 상황에 따라 구조적 문장을 동적으로 만든다. 
1. 값을 참조해오는 경우 
	1. @변수  
	2. <$변수> 
2. 동적문장
	1. andif~ ~endif

위 세가지를 다음과 같이 용어를 규정하고 사용한다.
	1. OPattern : @변수 ORM에서 사용하고 Get, Set, Reference해오는 값
	   다른 워크셋의 컬럼이나 컨트롤러에서 값을 가져온다.
	3. GPattern : <$공용전역변수> Lib.Common에 정의되어 GetValue, SetValue로 활용된다. 
	4. CPattern : Conditional Clauses andif~ ~endif


>[!quote]
return Value를 Dictionary<string, string>로 처리한 이유는 Key로 찾고 Value에 값을 찾아 추가하여 파라미터로 던지기 위해서 임


ExtractPattern()
`Lib.Syntax.SQLVariableExtractor.ExtractPattern(value, match => match.OPatternMatch)`
특정 패턴만 추출해서 보고 싶을때 사용
```C#
var items = Lib.Syntax.SQLVariableExtractor.ExtractPattern(value, match => match.OPatternMatch);
foreach (var item in items)
{
	Common.gMsg = item.Key + " : " + item.Value;
}
```


RemoveCPattern()
임시로 변경한다. 


GenFunc.ReplaceGPatternQuery 문장에 포함된것을 변경 '' 안에 값을 넣는다.
GenFunc.ReplaceGPatternWord Default Text 값을 변경





### 3. GPatternMatch 및 OPatternMatch

- **GPatternMatch (for <$변수>)**:
    - <$변수> 형태의 변수는 `Lib.Common`의 개체에서 값을 가져옵니다.
    - 예: `<$userId>`는 `Lib.Common`의 `userId` 필드에서 값을 가져옵니다.
- **OPatternMatch (for @변수)**:
    - @변수 형태의 변수는 다른 워크셋의 컬럼이나 컨트롤러에서 값을 가져옵니다.
    - 예: `@orderId`는 다른 워크셋의 `orderId` 컬럼이나 특정 컨트롤러에서 값을 가져옵니다.