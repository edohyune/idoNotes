
UCFieldSet >> BindControl
```C#
if (toolName == "UCRichText" && ctrl is UCRichText ucRichText)
{
	ucRichText.DataBindings.Add("BindText", bindingSource, propertyName);
}
if (toolName == "UCCheckBox" && ctrl is UCCheckBox ucCheckBox)
{
	ucCheckBox.DataBindings.Add("BindBool", bindingSource, propertyName);
}
```

UCLookUp >> InitBinding
```C#
case "uctextbox":
case "uctext":
	UCTextBox uctxt = ctrl as UCTextBox;
	if (uctxt != null)
	{
		uctxt.BindText = value.ToString();
	}
	break;
```

DesignerV004 >> Get Control Properties



Table >> CTRLMST
```SQL

select a.FrwId, a.FrmId, a.FrmNm, a.UsrRegId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt,
       FullPath='',
       WorkSpace='',
       FileName=a.FileNm,
       FilePath=a.FilePath,
       FrameWork=a.FrwId,
       NameSpace=a.NmSpace,
       FormId=a.FrmId,
       FormName=a.FrmNm,
       Directory='',
       fromFullPath='',
       fromFormId=a.FrmNm,
       fromNameSpace=a.NmSpace
  from FRWFRM a

```



