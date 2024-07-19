---
Start Date: 2024-06-10
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-10
tags:
  - 변수추출
  - Extract
  - Variable
  - Class_Syntax
CDT: 2024-06-10 14:54
MDT: 2024-06-10 15:03
---
---
#### Prologue / Concept

#### Manifestation

#### Integration

###### REFERENCE

###### Syntax Class
```C#
using System.Text.RegularExpressions;
using Match = System.Text.RegularExpressions.Match;

namespace Lib.Syntax
{
    public class SQLSyntaxMatch
    {
        public Dictionary<string, string> OPatternMatch { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DPatternMatch { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> GPatternMatch { get; set; } = new Dictionary<string, string>();
    }

    public class SQLVariableExtractor
    {
        public SQLSyntaxMatch ExtractVariables(string query)
        {
            SQLSyntaxMatch variables = new SQLSyntaxMatch();

            //Regex oPattern = new Regex(@"@\w+", RegexOptions.IgnoreCase);
            //Regex dPattern = new Regex(@"@_\w+", RegexOptions.IgnoreCase);
            //Regex gPattern = new Regex(@"<\$\w+>", RegexOptions.IgnoreCase);

            Regex oPattern = new Regex(@"@\w+");
            Regex dPattern = new Regex(@"@_\w+");
            Regex gPattern = new Regex(@"<\$\w+>");

            foreach (Match match in dPattern.Matches(query))
            {
                string variableName = match.Value;
                //string variableName = match.Value.ToLower();
                if (!variables.DPatternMatch.ContainsKey(variableName))
                {
                    variables.DPatternMatch[variableName] = null;
                }
            }

            foreach (Match match in oPattern.Matches(query))
            {
                string variableName = match.Value;
                //string variableName = match.Value.ToLower();
                if (!variables.OPatternMatch.ContainsKey(variableName) && !variables.DPatternMatch.ContainsKey(variableName))
                {
                    variables.OPatternMatch[variableName] = null;
                }
            }

            foreach (Match match in gPattern.Matches(query))
            {
                string variableName = match.Value;
                //string variableName = match.Value.ToLower();
                if (!variables.GPatternMatch.ContainsKey(variableName))
                {
                    variables.GPatternMatch[variableName] = null;
                }
            }

            return variables;
        }
    }
}
```

###### Class 활용
 - Extract variables from Syntax - Target rtSelect.Text
 - OPatternMatch(`@variable`), 
   DPatternMatch(`@_variable`), 
   GPatternMatch(`<$variable>`)를 활용한다. 
```C#
public static string ReplaceGPatternVariable(string input)
{
	SQLVariableExtractor extractor = new SQLVariableExtractor();
	SQLSyntaxMatch cvariables = extractor.ExtractVariables(input);
	
	foreach (var variable in cvariables.GPatternMatch)
	{
		string variableName = variable.Key;
		string variableValue = Common.GetValue(variableName); // Lib.Common에서 값 가져오기
		input = input.Replace(variable.ToString(), variableValue);
	}
	return input;
}
```


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

