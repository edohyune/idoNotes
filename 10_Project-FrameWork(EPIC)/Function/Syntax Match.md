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



##### 사용법
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


###### REFERENCE
