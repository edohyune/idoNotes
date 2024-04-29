- Form Copy with Workset
    
      
    
- Automatic document number numbering (how to define in VB)
    
    - Using to VB define From VB
        
        Numbering Policy Form(BB500)
        
        ![[Untitled 17.png|Untitled 17.png]]
        
        ```VB.Net
        Private Sub MM120_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
                pi_no.CodeNo = "PI"
                pi_no.CodeDateField = pi_date
        End Sub
        ```
        
    
    - Using to Numbering Table and StoredProcedure

[[EMAX - Custom FTP settings and application|EMAX - Custom FTP settings and application]]

- How to send data to Groupware
    - WorkSet Copy
        
        - WorkSet Copy 할때 EMPLOYEE 때문에 오류나는 부분 수정하고 아래 부분 수정할 것
        
        ```VB.Net
        --The sample form is based on HR370 or HR390.
        declare @cpyfrm    varchar(20) = 'HR370'
        declare @cpyinit   varchar(20) = 'hr370_approvalInit'
        declare @cpyhtml   varchar(20) = 'hr370_approvalHtml'
        declare @cpysubmit varchar(20) = 'hr370_approvalSubmit'
        
        declare @frm       varchar(20) = ''------------------------------------Edit
        declare @frminit   varchar(20) = concat(lower(@frm),'_approvalInit')
        declare @frmhtml   varchar(20) = concat(lower(@frm),'_approvalHtml')
        declare @frmsubmit varchar(20) = concat(lower(@frm),'_approvalSubmit')
        
        exec	System_Copy_WorkSet_In_DB	@cpyfrm,	@cpyinit,	@frm,	@frminit,
        		                             'Approval GW Inital', 'SQL',''
        exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpyhtml, @frm, @frmhtml,
        		                             'Approval GW MAKE HTML', 'SQL', ''
        exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpysubmit, @frm, @frmsubmit,
        		                             'Approval GW Submit', 'SQL', ''
        ```
        
    - WorkSet Edit
        
        - EA002 코드에서 doc_code확인하는 방법
        
        ```VB.Net
        [FromName]_approvalInit
        select 
        doc_code  ='EA002008',  ---------------------Edit
        from_name ='HR390',     ---------------------Edit
        table_name='HA500',     ---------------------Edit
        doc_status='FI020200',  ---------------------Edit
        emp_no=<$emp_no>,       --If it is not in the document, it should be the author.
        dept_cd=<$dept_cd>,     --If it is not in the document, it should be the author.
        reg_id= <$reg_id>
        
        [FromName]_approvalHtml
        -- 1.0 Insert ea_erp     
        insert into ea_erp
               (form_name, table_name, doc_code, doc_no, sub_doc_no, doc_date, dept, staff, cid, doc_status)
        values (@form_name, @table_name, @doc_code, @doc_no, @doc_no, @doc_date,  @dept, @emp_no, <$reg_id>, 'FI020200')  
        
        -- 1.1 Make Groupware 
        exec ea_work @doc_code,@doc_no
        
        -- 1.2 HTML              ---------->>EDIT
        select a.doc_no, a.apply_date, a.emp_no, emp_name=b.other_lang_name, b.dept, 
               a.area, b.position, 
               frto_date = concat(a.from_date,' ~ ', a.to_date,'(',DATEDIFF(DAY,a.from_date,a.to_date)+1,')'), days, 
               a.place, a.subject_trip, a.memo
          from HA560 a
          left join HA100 b on a.emp_no =b.emp_no
         where a.doc_no=@doc_no
        
        select a.seq, 
               allowance_type=[dbo].[fnCodeNm](a.allowance_type), 
               a.allowance_amt, a.memo
          from HA570 a 
         where a.doc_no=@doc_no
        
        select total=sum(a.allowance_amt)
          from HA570 a 
         where a.doc_no=@doc_no
        
        [FromName]_approvalSubmit
        -- 1.0 Update ea_erp     
        update a
           set doc_html= @htm
          from EA_ERP a
         where form_name= @form_name
           and table_name= @table_name
           and doc_no= @doc_no
        
        -- 2.0 Update document status
        update a
           set a.status = @doc_stat
          from HA560 a           --------->>EDIT
         where a.doc_no = @doc_no
        
        select rtn = 'OK'
        ```
        
    - VB Edit
        
        - 참조할때 사용하는 DLL 목록 확인
            
            ![[Untitled 1 9.png|Untitled 1 9.png]]
            
        
        ![[Untitled 13.png|Untitled 13.png]]
        
        > Always Print Form Name is ApprovalDoc.vb
        
        - Single approval document
            
            ![[Untitled 2 6.png|Untitled 2 6.png]]
            
            ```VB.Net
            \#Region "Approval Button =========================================  Edit"
                Public m_stop_event As Boolean
                'Connect to Handles
                Private Sub btn_groupware_Click(sender As Object, e As EventArgs) Handles btn_groupware.Click
                    If m_stop_event Then Exit Sub
                    m_stop_event = True
            
                    'Condtion Check
                    If g20.RowCount < 1 Then      '''''''''''''''''''''''''''''''''Edit Grid 
                        Exit Sub
                    End If
            
                    'Current Document Parameters Value Setting
                    Dim docNo As String = doc_no.Text        ''''''''''''''''''''''Edit
                    Dim docDt As String = apply_date.Text    ''''''''''''''''''''''Edit
                    Dim empNo As String = "" 'emp_no.Text    ''''''''''''''''''''''Edit
                    Dim deptCd As String = "" 'dept.Text     ''''''''''''''''''''''Edit
                    Dim testYn As Boolean = False            ''''''''''''''''''''''Edit (True : Can See Print)
            
            
                    Dim rtnGroupWareProcess As String = groupWareProcess(docNo, docDt, empNo, deptCd, testYn)
                    If rtnGroupWareProcess = "OK" Then
                        m_stop_event = False
                        PutMessage("BJC100_24", "It has been processed succeed.")
                        Dim find As String = docNo
                        Me.Open()
                        g10.Find("doc_no=" + find)
                        Exit Sub
                    End If
                End Sub
            \#End Region
            \#Region "Approval Can See Print ======================= Do not need Edit"
                'Handles EButton1.Click
                Private Sub CanSeePrint_Click(sender As Object, e As EventArgs) 'Handles EButton1.Click
                    'Condtion Check
                    If m_stop_event Then Exit Sub
                    m_stop_event = True
            
                    If g20.RowCount < 1 Then      '''''''''''''''''''''''''''''''''Edit Grid 
                        Exit Sub
                    End If
            
                    'Current Document Parameters Value Setting
                    Dim docNo As String = doc_no.Text        ''''''''''''''''''''''Edit
                    Dim docDt As String = apply_date.Text    ''''''''''''''''''''''Edit
                    Dim empNo As String = emp_no.Text        ''''''''''''''''''''''Edit
                    Dim deptCd As String = dept.Text         ''''''''''''''''''''''Edit
                    Dim testYn As Boolean = True             ''''''''''''''''''''''Edit
            
                    Dim rtnGroupWareProcess As String = groupWareProcess(docNo, docDt, empNo, deptCd, testYn)
                    If rtnGroupWareProcess = "OK" Then
                        MsgBox("")
                    ElseIf rtnGroupWareProcess = "TEST" Then
                        MsgBox("testYn값을 False로 변경하면 그룹웨어로 데이터가 전송됩니다.")
                    Else
                        MsgBox(rtnGroupWareProcess)
                    End If
                End Sub
            \#End Region
            \#Region "Approval ===================================== Do not need Edit"
                Private Function groupWareProcess(docNo As String, docDt As String, empNo As String, deptCd As String, testYn As Boolean) As String
                    Dim frmCd As String = Me.Name
            
                    Dim dinitSet As System.Data.DataSet = MyBase.OpenDataSet(frmCd + "_approvalInit")
                    Dim gwNo As String = DataValue(dinitSet, "doc_code")
                    Dim docStat As String = DataValue(dinitSet, "doc_status")
                    Dim tblNm As String = DataValue(dinitSet, "table_name")
                    If empNo = "" Then
                        empNo = DataValue(dinitSet, "emp_no")
                    End If
                    If deptCd = "" Then
                        deptCd = DataValue(dinitSet, "dept_cd")
                    End If
            
                    Dim p As OpenParameters = New OpenParameters
                    p.Add("@form_name", frmCd)
                    p.Add("@table_name", tblNm)
                    p.Add("@doc_code", gwNo)
                    p.Add("@doc_no", docNo)
                    p.Add("@doc_date", docDt)
                    p.Add("@dept", deptCd)
                    p.Add("@emp_no", empNo)
                    p.Add("@doc_stat", docStat)
            
                    Dim dSet1 As DataSet = OpenDataSet(frmCd + "_approvalHtml", p)
            
                    If IsEmpty(dSet1) Then
                        'Exit Function
                        Return "Error in " + frmCd + "_approvalHtml"
                    End If
            
                    Dim Rpt1 As New ApprovalDoc(dSet1)
            
                    If testYn Then
                        Dim ReportPrintTool As New DevExpress.XtraReports.UI.ReportPrintTool(Rpt1)
                        ReportPrintTool.ShowPreviewDialog()
                        Return "TEST"
                    Else
                        If Directory.Exists("c:\temp\") = False Then
                            Directory.CreateDirectory("c:\temp\")
                        End If
            
                        Rpt1.CreateDocument()
            
                        Dim htmlId As String = Guid.NewGuid().ToString("N")
                        Dim fileNm As String = "c:\temp\" + htmlId + ".html"
            
                        Rpt1.ExportToHtml(fileNm)
            
                        Dim client As New WebClient
                        client.Encoding = System.Text.Encoding.UTF8
                        Dim html As String = client.DownloadString(New Uri(fileNm))
            
                        html = Replace(html, "'", "''")
            
                        p.Add("@htm", html)
            
                        Dim dset2 As DataSet = OpenDataSet(frmCd + "_approvalSubmit", p)
            
                        If IsEmpty(dset2) Then
                            Return frmCd + "_approvalSubmit ERROR"
                        Else
                            Return DataValue(dset2, "rtn")
                        End If
                    End If
                End Function
            \#End Region
            ```
            
        - Multi-approval document
            
            ![[ZZ_Files/SA100 2.zip|SA100 2.zip]]
            
            ```VB.Net
            \#Region "Approval Button =========================================  Edit"
                'Connect to Handles
                Private Sub btn_groupware_Click(sender As Object, e As EventArgs) Handles btn_groupware.Click
                    If Not CheckRequired(sop_type) Then
                        Exit Sub
                    End If
                    '
                    If sop_type.Text = "BJ006100" Then
                        If Not CheckRequired(bid_no) Then
                            Exit Sub
                        End If
                    ElseIf sop_type.Text = "BJ006200" Then
                        If Not CheckRequired(rfq_no) Then
                            Exit Sub
                        End If
                    ElseIf sop_type.Text = "BJ006300" Then
                        If Not CheckRequired(project_no) Then
                            Exit Sub
                        End If
                    End If
            
                    If Me.Save() Then
                        If m_stop_event Then Exit Sub
            
                        'Current Document Parameters Value Setting
                        Dim docNo As String = Nothing
                        Dim docDt As String = Nothing
                        Dim empNo As String = Nothing
                        Dim deptCd As String = Nothing
                        Dim fileYn As String = "0"
            
                        If cuFtp.RowCount > 0 Then
                            fileYn = "1"
                        End If
            
                        Select Case sop_type.Text
                            Case "BJ006100" 'Budgetary      BJ006100
                                docNo = bid_no.Text
                                docDt = bid_date.Text
                                empNo = ""
                                deptCd = ""
                            Case "BJ006200" 'Official RFQ	BJ006200
                                docNo = rfq_no.Text
                                docDt = Today.ToString("yyyy-MM-dd")
                                empNo = ""
                                deptCd = ""
                            Case "BJ006300" 'Final Proposal	BJ006300
                                docNo = project_no.Text
                                docDt = Today.ToString("yyyy-MM-dd")
                                empNo = ""
                                deptCd = ""
                        End Select
                        Dim rtnGroupWareProcess As String = groupWareProcess(sop_type.Text, docNo, docDt, empNo, deptCd, fileYn)
                        If rtnGroupWareProcess = "OK" Then
                            m_stop_event = False
                            PutMessage("BJC100_24", "It has been processed succeed.")
                            Dim find As String = bid_no.Text
                            Me.Open()
                            g10.Find("bid_no=" + find)
                            Exit Sub
                        End If
                    End If
                End Sub
            
                Private Function groupWareProcess(sopTy As String, docNo As String, docDt As String, empNo As String, deptCd As String, fileYn As String) As String
            
                    Dim frmCd As String = Me.Name
                    Dim dinitSet As System.Data.DataSet = Nothing
            
                    Select Case sopTy
                        Case "BJ006100" 'Budgetary      BJ006100
                            dinitSet = MyBase.OpenDataSet("sa100_appBudInit")
                            If IsEmpty(dinitSet) Then Return "disiSet을 구성하는데 에러가 있습니다."
                        Case "BJ006200" 'Official RFQ	BJ006200
                            dinitSet = MyBase.OpenDataSet("sa100_appRFQInit")
                            If IsEmpty(dinitSet) Then Return "disiSet을 구성하는데 에러가 있습니다."
                        Case "BJ006300" 'Final Proposal	BJ006300
                            dinitSet = MyBase.OpenDataSet("sa100_appFNLInit")
                            If IsEmpty(dinitSet) Then Return "disiSet을 구성하는데 에러가 있습니다."
                    End Select
            
                    Dim gwNo As String = DataValue(dinitSet, "doc_code")
                    Dim docStat As String = DataValue(dinitSet, "doc_status")
                    Dim tblNm As String = DataValue(dinitSet, "table_name")
            
                    If empNo = "" Then
                        empNo = DataValue(dinitSet, "emp_no")
                    End If
                    If deptCd = "" Then
                        deptCd = DataValue(dinitSet, "dept_cd")
                    End If
            
                    Dim p As OpenParameters = New OpenParameters
                    p.Add("@form_name", frmCd)
                    p.Add("@table_name", tblNm)
                    p.Add("@doc_code", gwNo)
                    p.Add("@doc_no", docNo)
                    p.Add("@doc_date", docDt)
                    p.Add("@dept", deptCd)
                    p.Add("@emp_no", empNo)
                    p.Add("@doc_stat", docStat)
                    p.Add("@file_yn", fileYn)
            
                    Dim dSet1 As DataSet = Nothing
                    Select Case sopTy
                        Case "BJ006100" 'Budgetary      BJ006100
                            dSet1 = OpenDataSet("sa100_appBudHtml", p)
                        Case "BJ006200" 'Official RFQ	BJ006200
                            dSet1 = OpenDataSet("sa100_appRFQHtml", p)
                        Case "BJ006300" 'Final Proposal	BJ006300
                            dSet1 = OpenDataSet("sa100_appFNLHtml", p)
                    End Select
            
                    If IsEmpty(dSet1) Then
                        Return "Error in " + frmCd + "_approvalHtml"
                    End If
            
                    If Directory.Exists("c:\temp\") = False Then
                        Directory.CreateDirectory("c:\temp\")
                    End If
            
                    Dim htmlId As String = Guid.NewGuid().ToString("N")
                    Dim fileNm As String = "c:\temp\" + htmlId + ".html"
            
            
                    Select Case sopTy
                        Case "BJ006100" 'Budgetary      BJ006100
                            Dim RptBud As New BudgetaryDoc(dSet1)
                            RptBud.CreateDocument()
                            RptBud.ExportToHtml(fileNm)
                        Case "BJ006200" 'Official RFQ	BJ006200
                            Dim RptRFQ As New OfficialRFQ(dSet1)
                            RptRFQ.CreateDocument()
                            RptRFQ.ExportToHtml(fileNm)
                        Case "BJ006300" 'Final Proposal	BJ006300
                            Dim RptFnl As New FinalProposal(dSet1)
                            RptFnl.CreateDocument()
                            RptFnl.ExportToHtml(fileNm)
                    End Select
            
                    Dim client As New WebClient
                            client.Encoding = System.Text.Encoding.UTF8
                    Dim html As String = client.DownloadString(New Uri(fileNm))
            
                    html = Replace(html, "'", "''")
            
                    p.Add("@htm", html)
            
                    Dim dset2 As DataSet = Nothing
                    Select Case sopTy
                        Case "BJ006100" 'Budgetary      BJ006100
                            dset2 = OpenDataSet("sa100_appBudSubmit", p)
                        Case "BJ006200" 'Official RFQ	BJ006200
                            dset2 = OpenDataSet("sa100_appRFQSubmit", p)
                        Case "BJ006300" 'Final Proposal	BJ006300
                            dset2 = OpenDataSet("sa100_appFNLSubmit", p)
                    End Select
            
                    If IsEmpty(dset2) Then
                        Return frmCd + "_approvalSubmit ERROR"
                    Else
                        Return DataValue(dset2, "rtn")
                    End If
            
                End Function
            ```
            
    - [Add]GroupWare Document Init
        
        - 그룹웨어 데이터 초기화에 대한 설명이 필요함
        
        ```SQL
        -- 1.1 Duplicate check
        declare @doc_no varchar(30) = ''
        select * from ea_doc where doc_no = @doc_no
        select * from ea_erp where doc_no = @doc_no
        --delete from ea_doc where doc_no = @doc_no
        --delete from ea_erp where doc_no = @doc_no
        
        update a
           set a.status = 'FI020100'
          from GA300 a -------------------->>EDIT
         where a.contract_no = @doc_no
        ```
        
    - [Add] devExpress Basic Print Form
        
        - 프린트 폼과 쿼리데이터 멥핑하는 방법에 대한 설명
        
        ```VB.Net
            Public Sub New()
                InitializeComponent()
            End Sub
        
            Public Sub New(ByVal dSet As DataSet)
                InitializeComponent()
                Me.SetDateSet(dSet)
            End Sub
        
            Public Sub SetDateSet(ByVal dSet As DataSet)
                'Select contract_no, plate_no, contract_date, brand, rental_ty
                '       model, vehicle_ty,
                '       frto_date = concat(from_date, '~', to_date),
                '       dept_name = dbo.fnDeptNm_BJC(dept),
                '       project_name = dbo.fnProjectNm_BJC(project),
                '       month_cnt = DateDiff(Month, from_date, to_date) + 1,
                '       contract_amount, emp_no, emp_name = dbo.fnEmpNm_BJC(emp_no),
                '       down_payment, cust_name = dbo.fnCustNm_BJC(cust),
                '       monthly_fee, usage
                '  From ga300
                ' Where contract_no = @doc_no
        
                'Select seq, pay_month, rental_fee, status, memoDtl=memo
                '  From ga310
                ' Where contract_no = @doc_no
        
                'master1
                DetailReport1.DataSource = dSet.Tables(1)
        
                bid_no.DataBindings.Add("Text", dSet.Tables(0), "bid_no")
                bid_date.DataBindings.Add("Text", dSet.Tables(0), "bid_date")
                sop_type.DataBindings.Add("Text", dSet.Tables(0), "sop_type")
                validity.DataBindings.Add("Text", dSet.Tables(0), "validity")
                rev.DataBindings.Add("Text", dSet.Tables(0), "rev")
                scope.DataBindings.Add("Text", dSet.Tables(0), "scope")
                memo.DataBindings.Add("Text", dSet.Tables(0), "memo")
                client.DataBindings.Add("Text", dSet.Tables(0), "client")
                client_name.DataBindings.Add("Text", dSet.Tables(0), "client_name")
                project_owner.DataBindings.Add("Text", dSet.Tables(0), "project_owner")
                project_name.DataBindings.Add("Text", dSet.Tables(0), "project_name")
                project_location.DataBindings.Add("Text", dSet.Tables(0), "project_location")
                invoice_to.DataBindings.Add("Text", dSet.Tables(0), "invoice_to")
                invoice_client.DataBindings.Add("Text", dSet.Tables(0), "invoice_client")
                invoice_addr.DataBindings.Add("Text", dSet.Tables(0), "invoice_addr")
                delivery_company.DataBindings.Add("Text", dSet.Tables(0), "delivery_company")
                company_name.DataBindings.Add("Text", dSet.Tables(0), "company_name")
                company_addr.DataBindings.Add("Text", dSet.Tables(0), "company_addr")
                delivery_term.DataBindings.Add("Text", dSet.Tables(0), "delivery_term")
        
        
                'detail1
                sq.DataBindings.Add("Text", dSet.Tables(1), "sq")
                job_desc.DataBindings.Add("Text", dSet.Tables(1), "job_desc")
                job_ref.DataBindings.Add("Text", dSet.Tables(1), "job_ref")
                qty.DataBindings.Add("Text", dSet.Tables(1), "qty", "{0:#,0}")
                memoDtl.DataBindings.Add("Text", dSet.Tables(1), "memo")
        
        
                'ImageBinding(dSet.Tables(0), img, "photo")
            End Sub
        
            Private Sub ImageBinding(ByVal dTable As DataTable, ByVal PictureBoxControl As DevExpress.XtraReports.UI.XRPictureBox, ByVal ImageField As String)
                If IsDBNull(dTable.Rows(0)(ImageField)) Then
                    PictureBoxControl.Image = Nothing
                Else
                    Dim photo() As Byte = dTable.Rows(0)(ImageField)
                    Dim ms As New System.IO.MemoryStream(photo)
                    PictureBoxControl.Image = New Bitmap(ms)
                    PictureBoxControl.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze
                End If
            End Sub
        ```
        
    - Modify BJC Groupware Process
        - Check Value
            
            xxxxxx_approvalInit
            
            ```SQL
            select 
            doc_code  ='EA002009',      ---------------------Edit
            from_name ='HR370',      ---------------------Edit 
            table_name='HA560',      ---------------------Edit
            doc_status='FI020100',      ---------------------Edit
            emp_no=<$emp_no>,
            dept_cd=<$dept_cd>,
            reg_id= <$reg_id>
            ```
            
            xxxxxx_approvalHtml
            
            ```SQL
            -- 1.0 Insert ea_erp
            if exists (select 1 from EA_ERP where doc_code = @doc_code and doc_no = @doc_no)
                delete from EA_ERP where doc_code = @doc_code and doc_no = @doc_no
            
            insert into ea_erp
                   (form_name, table_name, doc_code, doc_no, sub_doc_no, doc_date, dept, staff, cid, doc_status)
            values (@form_name, @table_name, @doc_code, @doc_no, @doc_no, @doc_date,  @dept, @emp_no, <$reg_id>, @doc_stat)  
            
            -- 1.1 Make Groupware 
            exec ea_work @doc_code,@doc_no
            ```
            
            xxxxxx_approvalSubmit
            
            ```SQL
            update a
               set doc_html= @htm
              from EA_ERP a
             where form_name= @form_name
               and table_name= @table_name
               and doc_no= @doc_no
            
            --
            update a
               set a.status = @doc_stat
              from HA560 a                ------------------------------------------------------------->>Edit Table
             where a.doc_no = @doc_no     ------------------------------------------------------------->>Edit PK Column
            ```
            
              
            
        - Add WorkSet
            
            ```SQL
            declare @frm varchar(20) = ''  -------------EDIT
            exec System_Copy_WorkSet_In_DB	'HR370',	'hr370_approvalOpen',	@frm,	concat(@frm,'_approvalOpen'),
            		                            'Open GW Document', 'SQL',''
            ```
            
        - Add Button[Viewer]
            
            When testing groupware, you must log in with ID:kh Password:1.
            
            ![[Untitled 3 6.png|Untitled 3 6.png]]
            
        - Add VisualBasic Code
            
            ```VB.Net
            \#Region "Move To Groupware"
                Private Sub openGroupWare(docNo As String, empNo As String)
            
                    Dim p As OpenParameters = New OpenParameters
                    p.Add("@docNo", docNo)
                    p.Add("@empNo", empNo)
            
                    Dim dOpen As DataSet = OpenDataSet(Me.Name + "_approvalOpen", p)
                    If IsEmpty(dOpen) Then
                        MsgBox("Error in " + Me.Name + "_approvalOpen")
                        Exit Sub
                    End If
            
                    System.Diagnostics.Process.Start("http://gw.bjc1994.com:88/demo/login/sso_login?" _
                    + "loginCorpCODE=" + DataValue(dOpen, "loginCorpCODE").ToString() _
                    + "&loginLang=" + DataValue(dOpen, "loginLang").ToString() _
                    + "&loginId=" + DataValue(dOpen, "loginId").ToString() _
                    + "&loginPw=" + DataValue(dOpen, "loginPw").ToString() _
                    + "&doc_no=" + DataValue(dOpen, "doc_no").ToString())
                End Sub
            \#End Region
            
            \#Region "Viewer"
                Private Sub btn_viewer_Click(sender As Object, e As EventArgs) Handles btn_viewer.Click
                    Dim docNo As String = pr_no.Text + "_" + rev_no.Text  ''''''''''''''''''''''Edit
                    Dim empNo As String = ""                              ''''''''''''''''''''''Edit
                    openGroupWare(docNo, empNo)
                End Sub
            \#End Region
            ```
            
        - Modify VisualBasic Code
            
            ```VB.Net
            \#Region "Approval Button =========================================  Edit"
                Public m_stop_event As Boolean
                Private Sub btn_groupware_Click(sender As Object, e As EventArgs) Handles btn_groupware.Click
                   '''
                   '''Skip code
                   '''
                    If Me.Save() Then
                        '''
                        '''Skip code
                        '''
                        If rtnGroupWareProcess = "OK" Then
                            'PutMessage("BJC100_24", "It has been processed succeed.")''''''''''''''''Comment processing
                            Dim find As String = doc_no.Text
                            openGroupWare(docNo, empNo)   '''''''''''''''''''''''''''''''''''''''''''Add this line
                            Me.Open()
                            g10.Find("doc_no=" + find)
                            Exit Sub
                        End If
                    End If
                    '
                    m_stop_event = False
                End Sub
            \#End Region
            ```
            
              
            
- Sample
    
    ![[ZZ_Files/Print_GroupWare 2.zip|Print_GroupWare 2.zip]]
    
- PRINT
    - New Print Form
        - Form Copy
            
            System > Copy
            
            ![[Untitled 4 4.png|Untitled 4 4.png]]
            
        - Workset
            
            ```SQL
            --The sample form is based on HR370 or HR390.
            declare @cpyfrm    varchar(20) = 'HR370'
            declare @cpywkset  varchar(20) = 'hr370_printForm'
            
            declare @frm       varchar(20) = ''------------------------------------Edit
            declare @frmwkset  varchar(20) = concat(lower(@frm),'_printForm')
            
            exec	System_Copy_WorkSet_In_DB	@cpyfrm,	@cpyinit,	@frm,	@frminit,
            		                             'Approval GW Inital', 'SQL',''
            exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpyhtml, @frm, @frmhtml,
            		                             'Approval GW MAKE HTML', 'SQL', ''
            exec	System_Copy_WorkSet_In_DB	@cpyfrm, @cpysubmit, @frm, @frmsubmit,
            		                             'Approval GW Submit', 'SQL', ''
            ```
            
        - VB
            
            ```VB.Net
            Dim p As OpenParameters = New OpenParameters
            p.Add("@vc_no", vc_no.Text)       ''''''''''''''''''''''''''''''''''''Edit Document No
            Dim dSet As DataSet = MyBase.OpenDataSet(Me.Name+"_print", p)
            Dim Rpt1 As New FB100R(dSet)      ''''''''''''''''''''''''''''''''''''Edit Form
            Dim ReportPrintTool As New DevExpress.XtraReports.UI.ReportPrintTool(Rpt1)
            ReportPrintTool.ShowPreviewDialog()
            ```
            
    - Include Print in a Form
        
        - Workset
        
          
        
        - VB

  

  

  

POPUP Control

팝업의 내용 중 PK_DATA의 중복 선택을 허용하지 않는 경우

팝업의 내용을 부모의 문서에 넣는 방법 2가지

파라미터로 필요한 데이터 모두를 전달하여 넣는 경우

파라미터로 PK만 전송하여 WorkSet(SQL)을 이용하여 넣는 경우

주의 ) 아직 저장되지 않은 데이터의 RowDelete는 먹지 않는다. 이 경우 CancelRowChange()를 이용하여 등록을 취소하는 방법을 사용한다.