```VB.Net
If TabControl.SelectedTabPageIndex = 0 Then
    t10.Open()
ElseIf TabControl.SelectedTabPageIndex = 1 Then
    t10.Open()
ElseIf TabControl.SelectedTabPageIndex = 2 Then
    t10.Open()
End If
```

OpenParameters 이용

```VB.Net
Dim P As New OpenParameters
P.Add("@reqDt", f_reqDt.Text)
Me.Open("org100_doGetNumber", P)
```

  

[[OpenDataSet]]