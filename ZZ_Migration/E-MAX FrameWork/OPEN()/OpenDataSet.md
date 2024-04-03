- Style 1.
    
    ```VB.Net
    Dim p As New OpenParameters
    p.Add("@ivc_no", invoice_no.Text)
    
    Dim dset As DataSet = OpenDataSet("sg120_f1Sub", p)
    
    If IsEmpty(dset) Then
       Exit Sub
    End If
    
    If DataValue(dset, "codeNo") <> "" Then
        invoice_no.Text = DataValue(dset1, "codeNo")
    End If
    ```
    
- Style 2.
    
    ```VB.Net
    Dim p As New OpenParameters
    p.Add("@ivc_no", invoice_no.Text)
    
    Dim dset As DataSet = OpenDataSet("sg120_f1Sub", p)
    
    Dim dtbl As DataTable = dset.Tables(0)
    
    dSet.Talbes(0).Rows(0)("currency_rate")
    ```