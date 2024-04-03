```VB.Net
    Private Function getIdFromFTPTable() As String

        Dim id As String = Guid.NewGuid().ToString("N")
        Dim p As New OpenParameters

        p.Add("@id", id + yymm)
        Dim dset As DataSet = OpenDataSet("wrs110_ftpID", p)

        If Not IsEmpty(dset) Then
            getIdFromFTPTable()
        End If

        Return id
    End Function
```

WorkSet - wrs110_ftpID

```SQL
select 1
  from wrs102
 where fid=@id
```