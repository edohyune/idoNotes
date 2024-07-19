---
Start Date: 2024-06-10
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-10
tags:
  - DataSet
CDT: 2024-06-10 15:07
MDT: 2024-06-10 15:10
---
---
#### Prologue / Concept

#### Manifestation

#### Integration

###### REFERENCE


```C#
using (var db = new GaiaHelper())
{
	DataSet dSet = db.GetGridColumns(new { FrwId = selectedDoc.FrwId, FrmId = selectedDoc.FrmId, WrkId = selectedDoc.WrkId, CRUDM = "R" });
	if (dSet != null)
	{
		foreach (DataColumn cols in dSet.Tables[0].Columns)
		{
            주석 : Table(WRKFLD)에 저장
		}
		t10.DataSource = wrkFldbs;
	}
}
```