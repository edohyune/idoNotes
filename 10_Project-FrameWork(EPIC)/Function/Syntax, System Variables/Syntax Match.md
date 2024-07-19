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


Syntax Variables 종류

GPattern - Global - 시스템 또는 Variables 등록폼에 쿼리 또는 String으로 입력 
`Regex gPattern = new Regex(@"<\$\w+>");`

OPattern - ORM - ORM Mapping에 사용되는 패턴(dPattern제외)
`Regex oPattern = new Regex(@"@\w+");`

DPattern - 쿼리내부에서 사용하는 Declare 변수
`Regex dPattern = new Regex(@"@_\w+");`

Conditional Clauses - 
`var conditionalPattern = new Regex(@"andif\s+(.+?)\s+endif");`


Syntax, Pattern 사용처

WorkSet Get, Set, Ref 설정에 Default Text 값으로 설정 되었을때.
	- String 자체가 패턴임
`public string ReplaceDefaultText(string defaultText)`

#### WorkSet CRUD Query에 포함이 되어 있을때.

##### Query를 분석

###### Make GetParameters Data
```C#
SQLVariableExtractor extractor = new SQLVariableExtractor();
SQLSyntaxMatch cvariables = extractor.ExtractVariables(rtSelect.Text);

foreach (var kvp in cvariables.OPatternMatch)
{
	//wrkGets에 있으면 update 없으면 insert
	var wrkGet = wrkGetbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
	if (wrkGet == null)
	{
		wrkGet = wrkGetbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
		if (wrkGet != null)
		{
			wrkGet.FldNm = kvp.Key;
			wrkGet.ChangedFlag = MdlState.Updated;
		}
		else
		{
			wrkGetbs.Add(new WrkGet
			{
				FrwId = selectedDoc.FrwId,
				FrmId = selectedDoc.FrmId,
				WrkId = selectedDoc.WrkId,
				FldNm = kvp.Key,
				ChangedFlag = MdlState.Inserted
			});
		}
	}
}
```
###### Make Reference Data
```C#
SQLVariableExtractor extractor = new SQLVariableExtractor();
SQLSyntaxMatch cvariables = extractor.ExtractVariables(rtUpdate.Text);

foreach (var kvp in cvariables.OPatternMatch)
{
	//wrkRefs에 있으면 update 없으면 insert
	var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
	if (wrkRef == null)
	{
		wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
		if (wrkRef != null)
		{
			wrkRef.FldNm = kvp.Key;
			wrkRef.ChangedFlag = MdlState.Updated;
		}
		else
		{
			wrkRefbs.Add(new WrkRef
			{
				FrwId = selectedDoc.FrwId,
				FrmId = selectedDoc.FrmId,
				WrkId = selectedDoc.WrkId,
				FldNm = kvp.Key,
				ChangedFlag = MdlState.Inserted
			});
		}
	}
}

cvariables = extractor.ExtractVariables(rtInsert.Text);

foreach (var kvp in cvariables.OPatternMatch)
{
	var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
	if (wrkRef == null)
	{
		wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
		if (wrkRef != null)
		{
			wrkRef.FldNm = kvp.Key;
			wrkRef.ChangedFlag = MdlState.Updated;
		}
		else
		{
			wrkRefbs.Add(new WrkRef
			{
				FrwId = selectedDoc.FrwId,
				FrmId = selectedDoc.FrmId,
				WrkId = selectedDoc.WrkId,
				FldNm = kvp.Key,
				ChangedFlag = MdlState.Inserted
			});
		}
	}
}
```


WorkSet R Query에 포함되어 Controller를 Load할때.
`private string RemoveConditionalClauses(string sql)`

InitNewRow / InitNewControl
`public string ReplaceDefaultText(string defaultText)`


`private string ReplaceOPatternMatch(string sql, string[] param, string[] value)`

목록을 구성하고 찾아서 바꾼다. 

Extract - PatternMatch를 목록화 한다. 

PatternMatch - 문장에서 정규식에 부합하는 패턴을 찾아낸다. 

Find - `!variables.DPatternMatch.ContainsKey(variableName)`

Replace - 패턴을 찾아 String을 바꾼다.
	- 문장속에서 변경될때
	- 변경되어 그리드나 개체로 들어갈때








##### 입력폼에서 사용법
```C#
private void btnTEST03_Click(object sender, EventArgs e)
{
	SQLVariableExtractor extractor = new SQLVariableExtractor();
	SQLSyntaxMatch variables = extractor.ExtractVariables(txtMemo.Text);

	foreach (var kvp in variables.GPatternMatch)
	{
		Lib.Common.gMsg = ($"Key: {kvp.Key}, Value: {kvp.Value}");
	}
}
```

##### Key값에 Value 업데이트
```C#
using System;
using System.Collections.Generic;

public class PatternMatcher
{
    public Dictionary<string, string> OPatternMatch { get; set; } = new Dictionary<string, string>();

    public void UpdateValue(string key, string newValue)
    {
        // 1. 딕셔너리에 키가 존재하는지 확인
        if (OPatternMatch.ContainsKey(key))
        {
            // 2. 키가 존재하면 값 업데이트
            OPatternMatch[key] = newValue;
            Console.WriteLine($"키 '{key}'에 대한 값이 '{newValue}'로 업데이트되었습니다.");
        }
        else
        {
            // 3. 키가 존재하지 않으면 새로운 키-값 쌍 추가 (선택 사항)
            OPatternMatch.Add(key, newValue);
            Console.WriteLine($"키 '{key}'와 값 '{newValue}'가 추가되었습니다.");
        }
    }
}

// 사용 예시
public class Program
{
    public static void Main()
    {
        PatternMatcher matcher = new PatternMatcher();
        // 딕셔너리에 몇 가지 키-값 쌍 추가 (예시)
        matcher.OPatternMatch.Add("key1", "value1");
        matcher.OPatternMatch.Add("key2", "value2");
        // 키 값으로 값 찾고 업데이트
        matcher.UpdateValue("key1", "updatedValue1");

        // 존재하지 않는 키에 대한 값 업데이트 시도 (새로운 키-값 쌍 추가)
        matcher.UpdateValue("key3", "value3");
    }
}

```

RefParamValue
GetParamValue
SetControlValue

쿼리의 등록 및 참고하기 
	컬럼추출
		OPatternMatch(DPatternMatch), GPatternMatch
		Conditional Clauses
	Make GetParameters Data
		Default Text를 
	Make Reference Data
		SQLVariableExtractor를 통해서 
		Dictionary<string, string> 타입 OPatternMatch(DPatternMatch), GPatternMatch 구성 

쿼리의 실행
	Read - Select
		쿼리에 있는 Pattern
			GPattern, 
			OPattern, 
			Conditional Clauses

Grid
	InitNewRow
		ReplaceGPatternVariable return 'value', ReplaceGVariables Global Variable목록을 기준으로 값을 교체
		










