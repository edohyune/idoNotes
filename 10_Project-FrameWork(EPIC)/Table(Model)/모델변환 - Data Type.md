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

##### Collection Select Type
`public List<FrmWrk> GetByFrwFrm(string frwId, string frmId)
```C#
string sql = @"
";
using (var db = new Lib.GaiaHelper())
{
	var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

	if (result == null)
	{
		throw new KeyNotFoundException($"A record with the code {frwId},{frmId} was not found.");
	}
	else
	{
		foreach (var item in result)
		{
			item.ChangedFlag = MdlState.None;
		}
		return result;
	}
}
```

##### Select Type
`public FrmWrk GetByFrwFrm(string frwId, string frmId)
```C#
string sql = @"
";
using (var db = new Lib.GaiaHelper())
{
	var result = db.Query<WrkSql>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId, CRUDM = crudm }).SingleOrDefault();

	if (result == null)
	{
		throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId},{crudm} was not found.");
	}
	else
	{
		result.ChangedFlag = MdlState.None;
		return result;
	}
}
```

##### Execute Query : Object
`public void Add(WrkSql wrkSql)
```C#
string sql = @"
@CId, getdate(), @MId, getdate()
";
using (var db = new Lib.GaiaHelper())
{
	db.OpenExecute(sql, frmCtrl);
}
```

##### Execute Query : Element
`public void Delete(string abc, string def, string ghi)
```C#
string sql = @"
";
using (var db = new Lib.GaiaHelper())
{
	db.OpenExecute(sql, new { FrwId = abc, FrmId = def, CtrlNm = ghi });
}
```




#### Manifestation


bit → `bool`

time** → `TimeSpan`

datetime2, datetime, date** → `DateTime`
datetimeoffset** → `DateTimeOffset`

int, tinyint, smallint → `int`
bigint → `long`

float, money, decimal, numeric** → `decimal`

nchar, nvarchar, ntext, char, varchar, text** → `string`

uniqueidentifier** → `Guid`

varbinary, image** → `byte[]`

#### Integration



---
## REFERENCE
---
### MSSQL 데이터 타입과 C# 데이터 타입 매핑

1. **bit** → `bool`
   - `bit`은 0 또는 1의 값을 가지며, C#에서는 `bool`로 표현

2. **varbinary** → `byte[]`
   - 바이너리 데이터를 처리할 때 사용되며, C#에서는 `byte[]` 배열로 매핑

3. **time** → `TimeSpan`
   - 시간만을 저장하며, C#에서는 `TimeSpan`으로 매핑됩니다.

4. **datetime2, datetime, date** → `DateTime`
   - 날짜와 시간을 저장하는 타입으로, C#에서는 모두 `DateTime`으로 처리함

5. **datetimeoffset** → `DateTimeOffset`
   - 시간대 정보를 포함한 날짜와 시간을 저장하며, C#에서는 `DateTimeOffset`으로 매핑

6. **smallint** → `short`
   - 작은 범위의 정수를 저장하며, C#에서는 `short`로 표현

7. **tinyint** → `byte`
   - 아주 작은 범위의 정수를 저장하며, C#에서는 `byte`로 표현

8. **bigint** → `long`
   - 매우 큰 범위의 정수를 저장하며, C#에서는 `long`으로 표현

9. **int** → `int`
   - 일반적인 범위의 정수를 저장하며, C#에서는 `int`로 표현

10. **decimal, numeric** → `decimal`
    - 정밀한 소수점 연산이 필요할 때 사용되며, C#에서는 `decimal`로 매핑

11. **money** → `decimal`
    - 금액과 관련된 데이터 타입으로, C#에서도 `decimal`로 처리하는 것이 일반

12. **float** → `double`
    - 부동 소수점 수를 저장하며, C#에서는 `double`로 매핑

13. **char, varchar, text** → `string`
    - 문자 데이터를 저장하며, C#에서는 `string`으로 처리

14. **nchar, nvarchar, ntext** → `string`
    - 유니코드 문자 데이터를 저장하며, C#에서는 `string`으로 처리

15. **uniqueidentifier** → `Guid`
    - 글로벌 고유 식별자를 저장하며, C#에서는 `Guid`로 표현

16. **image** → `byte[]`
    - 이미지 데이터를 저장하는 데 사용되며, C#에서는 이를 `byte[]` 배열로 매핑하여 처리



이 매핑은 .NET의 `System.Data` 네임스페이스 아래에 있는 `SqlDbType` 및 관련 API를 사용하여 데이터베이스와의 상호 작용 중에 적절히 처리할 수 있습니다. 데이터 타입을 이해하고 올바르게 매핑하는 것은 데이터 무결성과 애플리케이션의 성능에 매우 중요합니다.


---
```mssql 
select COLUMN_NAME, 
	   DATATY = (case when DATA_TYPE in ('bit') then 'bool'
					  when DATA_TYPE in ('time') then 'TimeSpan'
					  when DATA_TYPE in ('datetimeoffset') then 'DateTimeOffset'
					  when DATA_TYPE in ('bigint') then 'long'
					  when DATA_TYPE in ('uniqueidentifier') then 'Guid'
					  when DATA_TYPE in ('varbinary', 'image') then 'byte[]'
					  when DATA_TYPE in ('datetime2', 'datetime', 'date') then 'DateTime'
					  when DATA_TYPE in ('int', 'tinyint', 'smallint') then 'int'
					  when DATA_TYPE in ('int', 'tinyint', 'smallint') then 'int'
					  when DATA_TYPE in ('float', 'money', 'decimal', 'numeric') then 'decimal'
					  when DATA_TYPE in ('nchar', 'nvarchar', 'ntext', 'char', 'varchar', 'text') then 'string'
					  else 'DATATYPE_ERR' end)
  from INFORMATION_SCHEMA.COLUMNS
 where TABLE_NAME = @tbl
 order by ORDINAL_POSITION;
```

---


C#에서는 `System.Data.SqlClient` 네임스페이스를 통해 SQL Server 데이터베이스와의 상호 작용을 관리할 수 있습니다. 여기에는 `SqlCommand`, `SqlConnection`, 그리고 `SqlParameter` 클래스가 포함되어 있습니다. 이러한 클래스들은 데이터베이스 명령을 실행하고, 연결을 관리하며, 쿼리에 파라미터를 추가하는 데 사용됩니다.

### 기본적인 사용 예시
다음은 `SqlDbType` 및 관련 API를 사용하여 SQL Server 데이터베이스에 쿼리를 실행하는 간단한 예시입니다.

1. **SqlConnection을 설정하고 엽니다**:
```csharp
using System.Data.SqlClient;

string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    // 이후 작업 수행
}
```

2. **SqlCommand를 사용하여 SQL 쿼리를 실행합니다**:
```csharp
string query = "INSERT INTO Employees (Name, JoinDate) VALUES (@Name, @JoinDate)";
using (SqlCommand command = new SqlCommand(query, connection))
{
    // SqlParameter를 추가하고 SqlDbType를 설정
    command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "John Doe";
    command.Parameters.Add("@JoinDate", SqlDbType.DateTime).Value = DateTime.Now;

    int result = command.ExecuteNonQuery();
    // result 변수는 영향 받은 행의 수를 반환
    Console.WriteLine("Rows affected: " + result);
}
```

### SqlParameter와 SqlDbType
`SqlParameter` 객체를 사용하여 쿼리의 매개변수를 정의하고, 이 매개변수의 `SqlDbType` 속성을 사용하여 데이터베이스의 열 데이터 타입과 일치시킵니다. 이는 SQL 인젝션 공격을 방지하고, 데이터 형식의 명확성을 보장하는 데 도움이 됩니다.

### SqlDataReader를 사용하여 데이터 읽기
```csharp
string selectQuery = "SELECT Name, JoinDate FROM Employees";
using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
{
    using (SqlDataReader reader = selectCommand.ExecuteReader())
    {
        while (reader.Read())
        {
            string name = reader["Name"].ToString();
            DateTime joinDate = (DateTime)reader["JoinDate"];
            Console.WriteLine($"Name: {name}, Joined: {joinDate}");
        }
    }
}
```

### 사용시 주의사항
- **예외 처리**: 데이터베이스 작업은 다양한 종류의 예외를 발생시킬 수 있습니다. 따라서 `try-catch` 블록을 사용하여 예외를 적절히 처리해야 합니다.
- **리소스 관리**: `SqlConnection`, `SqlCommand` 등은 `IDisposable` 인터페이스를 구현하므로, `using` 문을 사용하여 이들의 리소스를 자동으로 정리해야 합니다.

이러한 기본적인 예시를 통해 데이터베이스 작업에 대한 기본적인 이해를 할 수 있으며, 더 복잡한 작업을 수행하기 위한 기반을 마련할 수 있습니다.




