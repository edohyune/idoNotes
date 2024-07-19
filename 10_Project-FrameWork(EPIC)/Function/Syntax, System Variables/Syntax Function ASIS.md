

### Lib.GenFunc
#### Call : GridSet Open, Save, InitNewRow
```C#
public static string ReplaceGPatternVariable(string input)
{
	SQLVariableExtractor extractor = new SQLVariableExtractor();
	SQLSyntaxMatch cvariables = extractor.ExtractVariables(input);

	foreach (var variable in cvariables.GPatternMatch)
	{
		string variableName = variable.Key;

		string pattern = @"<\$(.*?)>";

		Match match = Regex.Match(variableName, pattern);

		if (match.Success)
		{
			string variableValue = Common.GetValue(match.Groups[1].Value); // Lib.Common에서 값 가져오기
			//input = input.Replace(variable.ToString(), variableValue);
			input = input.Replace(variableName, $"'{variableValue}'");
		}
		else
		{
			input = input.Replace(variableName, @"''");
		}
	}
	return input;
}
```

### Lib.GAIAHelper
실행전에 다음과 같이 수행한다. 
```C#
public List<T> Query<T>(string sql)
{
	try
	{
		sql = ProcessQuery(sql, null);
		sql = GenFunc.ReplaceGPatternVariable(sql);
		return SqlMapper.Query<T>(_conn, sql, null, _tran).ToList();
	}
	catch (Exception ex)
	{
		LogException(ex, sql);
		throw;
	}
}
```

`ExecuteScalar, Execute, Query<T>` 전에 쿼리안의 System Variables를 정리 한다.
GetGVariables() - 설정된 Global 변수 목록을 구성한다. 

```C#
private string ProcessQuery(string sql, object param)
{
	if (param != null)
	{
		sql = ReplaceConditionalClauses(sql, param);
	}
	sql = ReplaceGVariables(sql);
	return sql;
}

public string ReplaceGVariables(string sql)
{
	var gVariables = GetGVariables();
	foreach (var variable in gVariables)
	{
		sql = sql.Replace(variable.Key, variable.Value);
		//sql = Regex.Replace(sql, Regex.Escape(variable.Key), variable.Value, RegexOptions.IgnoreCase);
	}
	return sql;
}

private Dictionary<string, string> GetGVariables()
{
	var globalVariables = new Dictionary<string, string>();
	var fields = typeof(Common).GetFields(BindingFlags.Public | BindingFlags.Static);

	foreach (var field in fields)
	{
		var value = field.GetValue(null)?.ToString();
		if (value != null)
		{
			globalVariables.Add($"<${field.Name}>", value);
		}
	}
	return globalVariables;
}
```
