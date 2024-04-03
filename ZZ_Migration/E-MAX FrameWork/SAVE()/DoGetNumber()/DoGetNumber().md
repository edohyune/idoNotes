DoGetNumber 고유번호 생성

```VB.Net
                If TabControl.SelectedTabPageIndex = 0 Then
                    If f_rno.Text = "" Then
                        Dim p As New OpenParameters
                        p.Add("@reqDt", f_reqDt.Text)
                        Me.Open("org100_doGetNumber", p)
                    End If
                End If
```

Stored Procedure

```SQL
USE [FNT]
GO
/****** Object:  StoredProcedure [dbo].[spGetNumber]    Script Date: 1/27/2023 8:17:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spGetNumber] (@company varchar(20), @sys varchar(20), @dt date)
As 
Begin
  declare @maxNo int

  select @maxNo=max(pno)+1
    from cd_numbering
   where company=@company
     and sys=@sys
     and year(pdate)= year(@dt) and  month(pdate)=  month(@dt)
  ----------------------------------------
  --Editing Number
  ----------------------------------------
	if @maxNo is null
    begin
      insert into cd_numbering
      values (@company, @sys, @dt, 1)
      
  	  select @maxNo = 1
    end
  else
    begin
      update cd_numbering
         set pno=@maxNo
       where company=@company
         and sys=@sys
         and year(pdate)= year(@dt) and  month(pdate)=  month(@dt)  
    end;

  if @sys='CUST'
    select codeNo = 'C'+convert(varchar(6),@dt,112) + right('0000000' + convert(nvarchar(30), convert(int,isnull(@maxNo,0)) ) ,3)
  else if @sys='PREORDER'
    select codeNo = right(replace(convert(varchar(7),@dt,112),'-',''),4) + 'PR' + 
    right('0000' + convert(nvarchar(30), convert(int,isnull(@maxNo,0)) ) ,4)
  else 
    select codeNo = convert(varchar(8),@dt,112) + @sys + right('0000000' + convert(nvarchar(30), convert(int,isnull(@maxNo,0)) ) ,4)
End
```

![[ZZ_Files/Untitled 26.png|Untitled 26.png]]