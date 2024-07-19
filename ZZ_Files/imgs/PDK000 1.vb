Imports Frame8
Imports Base8
Imports Base8.Shared
Imports System.Data

Public Class PDK000

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        XtraTabControl1.SelectedTabPageIndex = 0
        init_kiosk("z")
        Open("pdk000_z02")

        If IsManager Then
            dt.ReadOnly = False
        Else
            dt.ReadOnly = True
        End If

    End Sub


#Region "a  PD350380 Assy F2A Body Start"
    Private Sub a_btnSearch_Click(sender As Object, e As EventArgs) Handles a_btnSearch.Click
        Open("pdk000_a10")
    End Sub

    Private Sub a_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles a_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_a10")
        End If
    End Sub

    Private Sub a_btnExec_Click(sender As Object, e As EventArgs) Handles a_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", a_kiosk.Text)

        p.Add("@job_no", a10.Text("job_no"))
        p.Add("@set_line", a10.Text("set_line"))
        p.Add("@line_cd", a10.Text("line_cd"))
        p.Add("@line_sq", a10.Text("line_sq"))

        p.Add("@mission_no", a_missionNo.Text)
        p.Add("@key_no", a_keyNo.Text)
        p.Add("@cabin_no", a_cabinNo.Text)
        p.Add("@cargo_no", a_cargoNo.Text)
        p.Add("@airbag_no", a_airbagNo.Text)
        p.Add("@rearaxle_no", a_rearaxleNo.Text)

        p.Add("@ref1", "")
        p.Add("@ref2", "")
        p.Add("@ref3", "")
        p.Add("@ref4", "")
        p.Add("@ref5", "")
        p.Add("@ref6", "")
        p.Add("@ref7", "")
        p.Add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", a10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(a_Kiosk.Text, a10.Text("line_cd"), a10.Text("vin_no"), a10.Text("job_no"), 1, a10.Text("line_sq"))

        init_kiosk("a")



    End Sub

    Private Sub a_btnTakeout_Click(sender As Object, e As EventArgs) Handles a_btnTakeout.Click
        Takeout("a")
    End Sub
#End Region
#Region "b  PD350390 Assy F2A Engine Ship"
    Private Sub b_btnSearch_Click(sender As Object, e As EventArgs) Handles b_btnSearch.Click
        Open("pdk000_b10")
    End Sub

    Private Sub b_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles b_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_b10")
        End If
    End Sub

    Private Sub b_btnExec_Click(sender As Object, e As EventArgs) Handles b_btnExec.Click

        Dim p As OpenParameters = New OpenParameters


        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", b_kiosk.Text)

        p.Add("@job_no", b10.Text("job_no"))
        p.Add("@set_line", b10.Text("set_line"))
        p.Add("@line_cd", b10.Text("line_cd"))
        p.Add("@line_sq", b10.Text("line_sq"))

        p.Add("@mission_no", b_missionNo.Text)
        p.Add("@key_no", b_keyNo.Text)
        p.Add("@cabin_no", b_cabinNo.Text)
        p.Add("@cargo_no", b_cargoNo.Text)
        p.Add("@airbag_no", b_airbagNo.Text)
        p.Add("@rearaxle_no", b_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", b10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(b_Kiosk.Text, b10.Text("line_cd"), b10.Text("vin_no"), b10.Text("job_no"), 1, b10.Text("line_sq"))

        init_kiosk("b")

    End Sub
    Private Sub b_btnTakeout_Click(sender As Object, e As EventArgs) Handles b_btnTakeout.Click
        Takeout("b")
    End Sub
#End Region
#Region "c  PD350400 Assy F2A Cargo Start"
    Private Sub c_btnSearch_Click(sender As Object, e As EventArgs) Handles c_btnSearch.Click
        Open("pdk000_c10")
    End Sub

    Private Sub c_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles c_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_c10")
        End If
    End Sub

    Private Sub c_btnExec_Click(sender As Object, e As EventArgs) Handles c_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", c_kiosk.Text)

        p.Add("@job_no", c10.Text("job_no"))
        p.Add("@set_line", c10.Text("set_line"))
        p.Add("@line_cd", c10.Text("line_cd"))
        p.Add("@line_sq", c10.Text("line_sq"))

        p.Add("@mission_no", c_missionNo.Text)
        p.Add("@key_no", c_keyNo.Text)
        p.Add("@cabin_no", c_cabinNo.Text)
        p.Add("@cargo_no", c_cargoNo.Text)
        p.Add("@airbag_no", c_airbagNo.Text)
        p.Add("@rearaxle_no", c_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", c10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(c_Kiosk.Text, c10.Text("line_cd"), c10.Text("vin_no"), c10.Text("job_no"), 1, c10.Text("line_sq"))

        init_kiosk("c")

    End Sub
    Private Sub c_btnTakeout_Click(sender As Object, e As EventArgs) Handles c_btnTakeout.Click
        Takeout("c")
    End Sub
#End Region
#Region "d  PD350410 Assy F2A Cargo End"
    Private Sub d_btnSearch_Click(sender As Object, e As EventArgs) Handles d_btnSearch.Click
        Open("pdk000_d10")
    End Sub

    Private Sub d_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles d_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_d10")
        End If
    End Sub

    Private Sub d_btnExec_Click(sender As Object, e As EventArgs) Handles d_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", d_Kiosk.Text)

        p.Add("@job_no", d10.Text("job_no"))
        p.Add("@set_line", d10.Text("set_line"))
        p.Add("@line_cd", d10.Text("line_cd"))
        p.Add("@line_sq", d10.Text("line_sq"))

        p.Add("@mission_no", d_missionNo.Text)
        p.Add("@key_no", d_keyNo.Text)
        p.Add("@cabin_no", d_cabinNo.Text)
        p.Add("@cargo_no", d_cargoNo.Text)
        p.Add("@airbag_no", d_airbagNo.Text)
        p.Add("@rearaxle_no", d_rearaxleNo.Text)

        p.Add("@ref1", "")
        p.Add("@ref2", "")
        p.Add("@ref3", "")
        p.Add("@ref4", "")
        p.Add("@ref5", "")
        p.Add("@ref6", "")
        p.Add("@ref7", "")
        p.Add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", d10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(d_Kiosk.Text, d10.Text("line_cd"), d10.Text("vin_no"), d10.Text("job_no"), 1, d10.Text("line_sq"))

        init_kiosk("d")

    End Sub
    Private Sub d_btnTakeout_Click(sender As Object, e As EventArgs) Handles d_btnTakeout.Click
        Takeout("d")
    End Sub
#End Region
#Region "e  PD350420 Assy F2A Door"
    Private Sub e_btnSearch_Click(sender As Object, e As EventArgs) Handles e_btnSearch.Click
        Open("pdk000_e10")
    End Sub

    Private Sub e_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles e_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_e10")
        End If
    End Sub

    Private Sub e_btnExec_Click(sender As Object, e As EventArgs) Handles e_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", e_kiosk.Text)

        p.Add("@job_no", e10.Text("job_no"))
        p.Add("@set_line", e10.Text("set_line"))
        p.Add("@line_cd", e10.Text("line_cd"))
        p.Add("@line_sq", e10.Text("line_sq"))

        p.Add("@mission_no", e_missionNo.Text)
        p.Add("@key_no", e_keyNo.Text)
        p.Add("@cabin_no", e_cabinNo.Text)
        p.Add("@cargo_no", e_cargoNo.Text)
        p.Add("@airbag_no", e_airbagNo.Text)
        p.Add("@rearaxle_no", e_rearaxleNo.Text)

        p.Add("@ref1", "")
        p.Add("@ref2", "")
        p.Add("@ref3", "")
        p.Add("@ref4", "")
        p.Add("@ref5", "")
        p.Add("@ref6", "")
        p.Add("@ref7", "")
        p.Add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", e10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(e_Kiosk.Text, e10.Text("line_cd"), e10.Text("vin_no"), e10.Text("job_no"), 1, e10.Text("line_sq"))

        init_kiosk("e")

    End Sub
    Private Sub e_btnTakeout_Click(sender As Object, e As EventArgs) Handles e_btnTakeout.Click
        Takeout("e")
    End Sub
#End Region
#Region "f  PD350430 Assy F2A Body End"
    Private Sub f_btnSearch_Click(sender As Object, e As EventArgs) Handles f_btnSearch.Click
        Open("pdk000_f10")
    End Sub

    Private Sub f_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles f_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_f10")
        End If
    End Sub

    Private Sub f_btnExec_Click(sender As Object, e As EventArgs) Handles f_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", f_kiosk.Text)

        p.Add("@job_no", f10.Text("job_no"))
        p.Add("@set_line", f10.Text("set_line"))
        p.Add("@line_cd", f10.Text("line_cd"))
        p.Add("@line_sq", f10.Text("line_sq"))

        p.Add("@mission_no", f_missionNo.Text)
        p.Add("@key_no", f_keyNo.Text)
        p.Add("@cabin_no", f_cabinNo.Text)
        p.Add("@cargo_no", f_cargoNo.Text)
        p.Add("@airbag_no", f_airbagNo.Text)
        p.Add("@rearaxle_no", f_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", f10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(f_Kiosk.Text, f10.Text("line_cd"), f10.Text("vin_no"), f10.Text("job_no"), 1, f10.Text("line_sq"))

        init_kiosk("f")

    End Sub
    Private Sub f_btnTakeout_Click(sender As Object, e As EventArgs) Handles f_btnTakeout.Click
        Takeout("f")
    End Sub
#End Region
#Region "g  PD350440 Assy F2A END"
    Private Sub g_btnSearch_Click(sender As Object, e As EventArgs) Handles g_btnSearch.Click
        Open("pdk000_g10")
    End Sub

    Private Sub g_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles g_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_g10")
        End If
    End Sub

    Private Sub g_btnExec_Click(sender As Object, e As EventArgs) Handles g_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", g_kiosk.Text)

        p.Add("@job_no", g10.Text("job_no"))
        p.Add("@set_line", g10.Text("set_line"))
        p.Add("@line_cd", g10.Text("line_cd"))
        p.Add("@line_sq", g10.Text("line_sq"))

        p.Add("@mission_no", g_missionNo.Text)
        p.Add("@key_no", g_keyNo.Text)
        p.Add("@cabin_no", g_cabinNo.Text)
        p.Add("@cargo_no", g_cargoNo.Text)
        p.Add("@airbag_no", g_airbagNo.Text)
        p.Add("@rearaxle_no", g_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", g10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(g_Kiosk.Text, g10.Text("line_cd"), g10.Text("vin_no"), g10.Text("job_no"), 1, g10.Text("line_sq"))

        init_kiosk("g")

    End Sub
    Private Sub g_btnTakeout_Click(sender As Object, e As EventArgs) Handles g_btnTakeout.Click
        Takeout("g")
    End Sub
#End Region
#Region "h  PD350450 Assy F2A Sub Engine"
    Private Sub h_btnSearch_Click(sender As Object, e As EventArgs) Handles h_btnSearch.Click
        Open("pdk000_h10")
    End Sub

    Private Sub h_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles h_FvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_h10")
        End If
    End Sub

    Private Sub h_btnExec_Click(sender As Object, e As EventArgs) Handles h_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", h_kiosk.Text)

        p.Add("@job_no", h10.Text("job_no"))
        p.Add("@set_line", h10.Text("set_line"))
        p.Add("@line_cd", h10.Text("line_cd"))
        p.Add("@line_sq", h10.Text("line_sq"))

        p.Add("@mission_no", h_missionNo.Text)
        p.Add("@key_no", h_keyNo.Text)
        p.Add("@cabin_no", h_cabinNo.Text)
        p.Add("@cargo_no", h_cargoNo.Text)
        p.Add("@airbag_no", h_airbagNo.Text)
        p.Add("@rearaxle_no", h_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", h10.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(h_Kiosk.Text, h10.Text("line_cd"), h10.Text("vin_no"), h10.Text("job_no"), 1, h10.Text("line_sq"))

        init_kiosk("h")



    End Sub

    Private Sub h_btnTakeout_Click(sender As Object, e As EventArgs) Handles h_btnTakeout.Click
        Takeout("h")
    End Sub
#End Region

#Region "aa PD350260 Assy F1A Frame In"
    Private Sub aa_btnSearch_Click(sender As Object, e As EventArgs) Handles aa_btnSearch.Click
        Open("pdk000_aa1")
    End Sub

    Private Sub aa_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles aa_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_aa1")
        End If
    End Sub

    Private Sub aa_btnExec_Click(sender As Object, e As EventArgs) Handles aa_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", aa_kiosk.Text)

        p.Add("@job_no", aa1.Text("job_no"))
        p.Add("@set_line", aa1.Text("set_line"))
        p.Add("@line_cd", aa1.Text("line_cd"))
        p.Add("@line_sq", aa1.Text("line_sq"))

        p.Add("@mission_no", aa_missionNo.Text)
        p.Add("@key_no", aa_keyNo.Text)
        p.Add("@cabin_no", aa_cabinNo.Text)
        p.Add("@cargo_no", aa_cargoNo.Text)
        p.Add("@airbag_no", aa_airbagNo.Text)
        p.Add("@rearaxle_no", aa_rearaxleNo.Text)

        p.Add("@ref1", "")
        p.Add("@ref2", "")
        p.Add("@ref3", "")
        p.Add("@ref4", "")
        p.Add("@ref5", "")
        p.Add("@ref6", "")
        p.Add("@ref7", "")
        p.Add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", aa1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(aa_kiosk.Text, aa1.Text("line_cd"), aa1.Text("vin_no"), aa1.Text("job_no"), 1, aa1.Text("line_sq"))

        init_kiosk("aa")
    End Sub

    Private Sub aa_btnTakeout_Click(sender As Object, e As EventArgs) Handles aa_btnTakeout.Click
        Takeout("aa")
    End Sub
#End Region
#Region "ab PD350270 Assy F1A Engine Ship "
    Private Sub ab_btnSearch_Click(sender As Object, e As EventArgs) Handles ab_btnSearch.Click
        Open("pdk000_ab1")
    End Sub

    Private Sub ab_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ab_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ab1")
        End If
    End Sub

    Private Sub ab_checkEngine_KeyDown(sender As Object, e As KeyEventArgs) Handles ab_checkEngine.KeyDown
        If e.KeyCode = 13 Then
            If ab_chkbeforeSave() Then
                ab_btnExec_save()
                init_kiosk("ab")
            Else
                ab_checkEngine.Text = "Engine# Error"
                selectAllText(ab_checkEngine)
            End If
        End If
    End Sub

    Private Sub ab_btnExec_Click(sender As Object, e As EventArgs) Handles ab_btnExec.Click
        If ab_chkbeforeSave() Then
            ab_btnExec_save()
            init_kiosk("ab")
        Else
            ab_checkEngine.Text = "Engine# Error"
            selectAllText(ab_checkEngine)
        End If
    End Sub

    Private Sub ab_btnExec_save()

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ab_kiosk.Text)

        p.Add("@job_no", ab1.Text("job_no"))
        p.Add("@set_line", ab1.Text("set_line"))
        p.Add("@line_cd", ab1.Text("line_cd"))
        p.Add("@line_sq", ab1.Text("line_sq"))

        p.Add("@mission_no", ab_missionNo.Text)
        p.Add("@key_no", ab_keyNo.Text)
        p.Add("@cabin_no", ab_cabinNo.Text)
        p.Add("@cargo_no", ab_cargoNo.Text)
        p.Add("@airbag_no", ab_airbagNo.Text)
        p.Add("@rearaxle_no", ab_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", ab1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)

        'saveWork(ab_kiosk.Text, ab1.Text("line_cd"), ab1.Text("vin_no"), ab1.Text("job_no"), 1, ab1.Text("line_sq"))

    End Sub

    Private Function ab_chkbeforeSave() As Boolean

        Dim p As OpenParameters = New OpenParameters

        p.Add("@job_no", ab1.Text("job_no"))
        p.Add("@engine_no", ab_checkEngine.Text)

        Dim dset As DataSet = OpenDataSet("pdk000_abCheckengine", p)

        If IsEmpty(dset) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ab_btnTakeout_Click(sender As Object, e As EventArgs) Handles ab_btnTakeout.Click
        Takeout("ab")
    End Sub

    Private Sub ab1_AfterMoveRow(sender As Object, PrevRowIndex As Integer, RowIndex As Integer) Handles ab1.AfterMoveRow
        ab_checkEngine.Text = ""
        selectAllText(ab_checkEngine)
    End Sub

#End Region
#Region "ac PD350280 Assy F1A Cabin Ship "
    Private Sub ac_btnSearch_Click(sender As Object, e As EventArgs) Handles ac_btnSearch.Click
        Open("pdk000_ac1")
    End Sub

    Private Sub ac_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ac_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ac1")
        End If
    End Sub

    Private Sub ac_btnExec_Click(sender As Object, e As EventArgs) Handles ac_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ac_kiosk.Text)

        p.Add("@job_no", ac1.Text("job_no"))
        p.Add("@set_line", ac1.Text("set_line"))
        p.Add("@line_cd", ac1.Text("line_cd"))
        p.Add("@line_sq", ac1.Text("line_sq"))

        p.Add("@mission_no", ac_missionNo.Text)
        p.Add("@key_no", ac_keyNo.Text)
        p.Add("@cabin_no", ac_cabinNo.Text)
        p.Add("@cargo_no", ac_cargoNo.Text)
        p.Add("@airbag_no", ac_airbagNo.Text)
        p.Add("@rearaxle_no", ac_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", ac1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(ac_kiosk.Text, ac1.Text("line_cd"), ac1.Text("vin_no"), ac1.Text("job_no"), 1, ac1.Text("line_sq"))

        init_kiosk("ac")

    End Sub
    Private Sub ac_btnTakeout_Click(sender As Object, e As EventArgs) Handles ac_btnTakeout.Click
        Takeout("ac")
    End Sub
#End Region
#Region "ad PD350290 Assy F1A Frame out"
    Private Sub ad_btnSearch_Click(sender As Object, e As EventArgs) Handles ad_btnSearch.Click
        Open("pdk000_ad1")
    End Sub

    Private Sub ad_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ad_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ad1")
        End If
    End Sub

    Private Sub ad_btnExec_Click(sender As Object, e As EventArgs) Handles ad_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ad_kiosk.Text)

        p.Add("@job_no", ad1.Text("job_no"))
        p.Add("@set_line", ad1.Text("set_line"))
        p.Add("@line_cd", ad1.Text("line_cd"))
        p.Add("@line_sq", ad1.Text("line_sq"))

        p.Add("@mission_no", ad_missionNo.Text)
        p.Add("@key_no", ad_keyNo.Text)
        p.Add("@cabin_no", ad_cabinNo.Text)
        p.Add("@cargo_no", ad_cargoNo.Text)
        p.Add("@airbag_no", ad_airbagNo.Text)
        p.Add("@rearaxle_no", ad_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", ad1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(ad_kiosk.Text, ad1.Text("line_cd"), ad1.Text("vin_no"), ad1.Text("job_no"), 1, ad1.Text("line_sq"))

        init_kiosk("ad")

    End Sub
    Private Sub ad_btnTakeout_Click(sender As Object, e As EventArgs) Handles ad_btnTakeout.Click
        Takeout("ad")
    End Sub
#End Region
#Region "ae PD350300 Assy F1A Cargo In "
    Private Sub ae_btnSearch_Click(sender As Object, e As EventArgs) Handles ae_btnSearch.Click
        Open("pdk000_ae1")
    End Sub

    Private Sub ae_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ae_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ae1")
        End If
    End Sub

    Private Sub ae_btnExec_Click(sender As Object, e As EventArgs) Handles ae_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ae_kiosk.Text)

        p.Add("@job_no", ae1.Text("job_no"))
        p.Add("@set_line", ae1.Text("set_line"))
        p.Add("@line_cd", ae1.Text("line_cd"))
        p.Add("@line_sq", ae1.Text("line_sq"))

        p.Add("@mission_no", ae_missionNo.Text)
        p.Add("@key_no", ae_keyNo.Text)
        p.Add("@cabin_no", ae_cabinNo.Text)
        p.Add("@cargo_no", ae_cargoNo.Text)
        p.Add("@airbag_no", ae_airbagNo.Text)
        p.Add("@rearaxle_no", ae_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", ae1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(ae_kiosk.Text, ae1.Text("line_cd"), ae1.Text("vin_no"), ae1.Text("job_no"), 1, ae1.Text("line_sq"))

        init_kiosk("ae")

    End Sub
    Private Sub ae_btnTakeout_Click(sender As Object, e As EventArgs) Handles ae_btnTakeout.Click
        Takeout("ae")
    End Sub
#End Region
#Region "af PD350310 Assy F1A Cargo Out "
    Private Sub af_btnSearch_Click(sender As Object, e As EventArgs) Handles af_btnSearch.Click
        Open("pdk000_af1")
    End Sub

    Private Sub af_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles af_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_af1")
        End If
    End Sub

    Private Sub af_btnExec_Click(sender As Object, e As EventArgs) Handles af_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", af_kiosk.Text)

        p.Add("@job_no", af1.Text("job_no"))
        p.Add("@set_line", af1.Text("set_line"))
        p.Add("@line_cd", af1.Text("line_cd"))
        p.Add("@line_sq", af1.Text("line_sq"))

        p.Add("@mission_no", af_missionNo.Text)
        p.Add("@key_no", af_keyNo.Text)
        p.Add("@cabin_no", af_cabinNo.Text)
        p.Add("@cargo_no", af_cargoNo.Text)
        p.Add("@airbag_no", af_airbagNo.Text)
        p.Add("@rearaxle_no", af_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", af1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(af_kiosk.Text, af1.Text("line_cd"), af1.Text("vin_no"), af1.Text("job_no"), 1, af1.Text("line_sq"))

        init_kiosk("af")

    End Sub
    Private Sub af_btnTakeout_Click(sender As Object, e As EventArgs) Handles af_btnTakeout.Click
        Takeout("af")
    End Sub
#End Region
#Region "ag PD350320 Assy F1A END"
    Private Sub ag_btnSearch_Click(sender As Object, e As EventArgs) Handles ag_btnSearch.Click
        Open("pdk000_ag1")
    End Sub

    Private Sub ag_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ag_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ag1")
        End If
    End Sub

    Private Sub ag_btnExec_Click(sender As Object, e As EventArgs) Handles ag_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ag_kiosk.Text)

        p.Add("@job_no", ag1.Text("job_no"))
        p.Add("@set_line", ag1.Text("set_line"))
        p.Add("@line_cd", ag1.Text("line_cd"))
        p.Add("@line_sq", ag1.Text("line_sq"))

        p.Add("@mission_no", ag_missionNo.Text)
        p.Add("@key_no", ag_keyNo.Text)
        p.Add("@cabin_no", ag_cabinNo.Text)
        p.Add("@cargo_no", ag_cargoNo.Text)
        p.Add("@airbag_no", ag_airbagNo.Text)
        p.Add("@rearaxle_no", ag_rearaxleNo.Text)

        p.add("@ref1", "")
        p.add("@ref2", "")
        p.add("@ref3", "")
        p.add("@ref4", "")
        p.add("@ref5", "")
        p.add("@ref6", "")
        p.add("@ref7", "")
        p.add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", ag1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(ag_kiosk.Text, ag1.Text("line_cd"), ag1.Text("vin_no"), ag1.Text("job_no"), 1, ag1.Text("line_sq"))

        init_kiosk("ag")

    End Sub
    Private Sub ag_btnTakeout_Click(sender As Object, e As EventArgs) Handles ag_btnTakeout.Click
        Takeout("ag")
    End Sub
#End Region

#Region "pa PD350340 Assy F1A Sub Cabin"
    Private Sub pa_btnSearch_Click(sender As Object, e As EventArgs) Handles pa_btnSearch.Click
        Open("pdk000_pa1")
    End Sub

    Private Sub pa_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pa_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pa1")
        End If
    End Sub

    Private Sub pa_btnExec_Click(sender As Object, e As EventArgs) Handles pa_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pa_kiosk.Text)

        p.Add("@job_no", pa1.Text("job_no"))
        p.Add("@set_line", pa1.Text("set_line"))
        p.Add("@line_cd", pa1.Text("line_cd"))
        p.Add("@line_sq", pa1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pa_ref1.Text)
        p.Add("@ref2", pa_ref2.Text)
        p.Add("@ref3", pa_ref3.Text)
        p.Add("@ref4", pa_ref4.Text)
        p.Add("@ref5", pa_ref5.Text)
        p.Add("@ref6", pa_ref6.Text)
        p.Add("@ref7", pa_ref7.Text)
        p.Add("@ref8", pa_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pa1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pa_kiosk.Text, pa1.Text("line_cd"), pa1.Text("vin_no"), pa1.Text("job_no"), 1, pa1.Text("line_sq"))

        init_kiosk("pa")

    End Sub

    Private Sub pa_btnTakeout_Click(sender As Object, e As EventArgs) Handles pa_btnTakeout.Click
        Takeout("pa")
    End Sub
#End Region
#Region "pb PD350350 Assy F1A Sub Cargo"
    Private Sub pb_btnSearch_Click(sender As Object, e As EventArgs) Handles pb_btnSearch.Click
        Open("pdk000_pb1")
    End Sub

    Private Sub pb_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pb_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pb1")
        End If
    End Sub

    Private Sub pb_btnExec_Click(sender As Object, e As EventArgs) Handles pb_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pb_kiosk.Text)

        p.Add("@job_no", pb1.Text("job_no"))
        p.Add("@set_line", pb1.Text("set_line"))
        p.Add("@line_cd", pb1.Text("line_cd"))
        p.Add("@line_sq", pb1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pb_ref1.Text)
        p.Add("@ref2", pb_ref2.Text)
        p.Add("@ref3", pb_ref3.Text)
        p.Add("@ref4", pb_ref4.Text)
        p.Add("@ref5", pb_ref5.Text)
        p.Add("@ref6", pb_ref6.Text)
        p.Add("@ref7", pb_ref7.Text)
        p.Add("@ref8", pb_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pb1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pb_kiosk.Text, pb1.Text("line_cd"), pb1.Text("vin_no"), pb1.Text("job_no"), 1, pb1.Text("line_sq"))

        init_kiosk("pb")

    End Sub

    Private Sub pb_btnTakeout_Click(sender As Object, e As EventArgs) Handles pb_btnTakeout.Click
        Takeout("pb")
    End Sub
#End Region
#Region "pc PD350360 Assy F1A Sub Rearaxle"
    Private Sub pc_btnSearch_Click(sender As Object, e As EventArgs) Handles pc_btnSearch.Click
        Open("pdk000_pc1")
    End Sub

    Private Sub pc_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pc_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pc1")
        End If
    End Sub

    Private Sub pc_btnExec_Click(sender As Object, e As EventArgs) Handles pc_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pc_kiosk.Text)

        p.Add("@job_no", pc1.Text("job_no"))
        p.Add("@set_line", pc1.Text("set_line"))
        p.Add("@line_cd", pc1.Text("line_cd"))
        p.Add("@line_sq", pc1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pc_ref1.Text)
        p.Add("@ref2", pc_ref2.Text)
        p.Add("@ref3", pc_ref3.Text)
        p.Add("@ref4", pc_ref4.Text)
        p.Add("@ref5", pc_ref5.Text)
        p.Add("@ref6", pc_ref6.Text)
        p.Add("@ref7", pc_ref7.Text)
        p.Add("@ref8", pc_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pc1.Text("job_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pc_kiosk.Text, pc1.Text("line_cd"), pc1.Text("vin_no"), pc1.Text("job_no"), 1, pc1.Text("line_sq"))


        init_kiosk("pc")

    End Sub

    Private Sub pc_btnTakeout_Click(sender As Object, e As EventArgs) Handles pc_btnTakeout.Click
        Takeout("pc")
    End Sub
#End Region
#Region "pd PD350330 Assy F1A Sub Engine"
    Private Sub pd_btnSearch_Click(sender As Object, e As EventArgs) Handles pd_btnSearch.Click
        Open("pdk000_pd1")
    End Sub

    Private Sub pd_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pd_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pd1")
        End If
    End Sub

    Private Sub pd_btnExec_Click(sender As Object, e As EventArgs) Handles pd_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pd_kiosk.Text)

        p.Add("@job_no", pd1.Text("job_no"))
        p.Add("@set_line", pd1.Text("set_line"))
        p.Add("@line_cd", pd1.Text("line_cd"))
        p.Add("@line_sq", pd1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pd_ref1.Text)
        p.Add("@ref2", pd_ref2.Text)
        p.Add("@ref3", pd_ref3.Text)
        p.Add("@ref4", pd_ref4.Text)
        p.Add("@ref5", pd_ref5.Text)
        p.Add("@ref6", pd_ref6.Text)
        p.Add("@ref7", pd_ref7.Text)
        p.Add("@ref8", pd_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pd1.Text("job_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pd_kiosk.Text, pd1.Text("line_cd"), pd1.Text("vin_no"), pd1.Text("job_no"), 1, pd1.Text("line_sq"))

        init_kiosk("pd")

    End Sub

    Private Sub pd_btnTakeout_Click(sender As Object, e As EventArgs) Handles pd_btnTakeout.Click
        Takeout("pd")
    End Sub
#End Region
#Region "pe PD350370 Assy F1A Sub Tire"
    Private Sub pe_btnSearch_Click(sender As Object, e As EventArgs) Handles pe_btnSearch.Click
        Open("pdk000_pe1")
    End Sub

    Private Sub pe_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pe_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pe1")
        End If
    End Sub

    Private Sub pe_btnExec_Click(sender As Object, e As EventArgs) Handles pe_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pe_kiosk.Text)

        p.Add("@job_no", pe1.Text("job_no"))
        p.Add("@set_line", pe1.Text("set_line"))
        p.Add("@line_cd", pe1.Text("line_cd"))
        p.Add("@line_sq", pe1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pe_ref1.Text)
        p.Add("@ref2", pe_ref2.Text)
        p.Add("@ref3", pe_ref3.Text)
        p.Add("@ref4", pe_ref4.Text)
        p.Add("@ref5", pe_ref5.Text)
        p.Add("@ref6", pe_ref6.Text)
        p.Add("@ref7", pe_ref7.Text)
        p.Add("@ref8", pe_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pe1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pe_kiosk.Text, pe1.Text("line_cd"), pe1.Text("vin_no"), pe1.Text("job_no"), 1, pe1.Text("line_sq"))

        init_kiosk("pe")

    End Sub

    Private Sub pe_btnTakeout_Click(sender As Object, e As EventArgs) Handles pe_btnTakeout.Click
        Takeout("pe")
    End Sub
#End Region

#Region "pf PD350500  Wheel Alignment"
    Private Sub pf_btnSearch_Click(sender As Object, e As EventArgs) Handles pf_btnSearch.Click
        Open("pdk000_pf1")
    End Sub

    Private Sub pf_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pf_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pf1")
        End If
    End Sub

    Private Sub pf_btnExec_Click(sender As Object, e As EventArgs) Handles pf_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pf_kiosk.Text)

        p.Add("@job_no", pf1.Text("job_no"))
        p.Add("@set_line", pf1.Text("set_line"))
        p.Add("@line_cd", pf1.Text("line_cd"))
        p.Add("@line_sq", pf1.Text("line_sq"))

        p.Add("@mission_no", pf_missionNo.Text)
        p.Add("@key_no", pf_keyNo.Text)
        p.Add("@cabin_no", pf_cabinNo.Text)
        p.Add("@cargo_no", pf_cargoNo.Text)
        p.Add("@airbag_no", pf_airbagNo.Text)
        p.Add("@rearaxle_no", pf_rearaxleNo.Text)

        p.Add("@ref1", "")
        p.Add("@ref2", "")
        p.Add("@ref3", "")
        p.Add("@ref4", "")
        p.Add("@ref5", "")
        p.Add("@ref6", "")
        p.Add("@ref7", "")
        p.Add("@ref8", "")

        p.Add("@qty", 1)

        p.Add("@unique_no", pf1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjobQC", p)
        'saveWork(pf_kiosk.Text, pf1.Text("line_cd"), pf1.Text("vin_no"), pf1.Text("job_no"), 1, pf1.Text("line_sq"))

        init_kiosk("pf")

    End Sub

    Private Sub pf_btnTakeout_Click(sender As Object, e As EventArgs) Handles pf_btnTakeout.Click
        Takeout("pf")
    End Sub
#End Region

#Region "pg PD350210  Paint Polishing"
    Private Sub pg_btnSearch_Click(sender As Object, e As EventArgs) Handles pg_btnSearch.Click
        Open("pdk000_pg1")
    End Sub

    Private Sub pg_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pg_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pg1")
        End If
    End Sub

    Private Sub pg_btnExec_Click(sender As Object, e As EventArgs) Handles pg_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pg_kiosk.Text)

        p.Add("@job_no", pg1.Text("job_no"))
        p.Add("@set_line", pg1.Text("set_line"))
        p.Add("@line_cd", pg1.Text("line_cd"))
        p.Add("@line_sq", pg1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pg_ref1.Text)
        p.Add("@ref2", pg_ref2.Text)
        p.Add("@ref3", pg_ref3.Text)
        p.Add("@ref4", pg_ref4.Text)
        p.Add("@ref5", pg_ref5.Text)
        p.Add("@ref6", pg_ref6.Text)
        p.Add("@ref7", pg_ref7.Text)
        p.Add("@ref8", pg_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pg1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pg_kiosk.Text, pg1.Text("line_cd"), pg1.Text("vin_no"), pg1.Text("job_no"), 1, pg1.Text("line_sq"))

        init_kiosk("pg")

    End Sub

    Private Sub pg_btnTakeout_Click(sender As Object, e As EventArgs) Handles pg_btnTakeout.Click
        Takeout("pg")
    End Sub
#End Region
#Region "ph PD350220  Paint QC"
    Private Sub ph_btnSearch_Click(sender As Object, e As EventArgs) Handles ph_btnSearch.Click
        Open("pdk000_ph1")
    End Sub

    Private Sub ph_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles ph_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_ph1")
        End If
    End Sub

    Private Sub ph_btnExec_Click(sender As Object, e As EventArgs) Handles ph_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", ph_kiosk.Text)

        p.Add("@job_no", ph1.Text("job_no"))
        p.Add("@set_line", ph1.Text("set_line"))
        p.Add("@line_cd", ph1.Text("line_cd"))
        p.Add("@line_sq", ph1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pg_ref1.Text)
        p.Add("@ref2", pg_ref2.Text)
        p.Add("@ref3", pg_ref3.Text)
        p.Add("@ref4", pg_ref4.Text)
        p.Add("@ref5", pg_ref5.Text)
        p.Add("@ref6", pg_ref6.Text)
        p.Add("@ref7", pg_ref7.Text)
        p.Add("@ref8", pg_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", ph1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(ph_kiosk.Text, ph1.Text("line_cd"), ph1.Text("vin_no"), ph1.Text("job_no"), 1, ph1.Text("line_sq"))

        init_kiosk("ph")

    End Sub

    Private Sub ph_btnTakeout_Click(sender As Object, e As EventArgs) Handles ph_btnTakeout.Click
        Takeout("ph")
    End Sub
#End Region
#Region "pi PD350230  Paint Out"
    Private Sub pi_btnSearch_Click(sender As Object, e As EventArgs) Handles pi_btnSearch.Click
        Open("pdk000_pi1")
    End Sub

    Private Sub pi_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pi_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pi1")
        End If
    End Sub

    Private Sub pi_btnExec_Click(sender As Object, e As EventArgs) Handles pi_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pi_kiosk.Text)

        p.Add("@job_no", pi1.Text("job_no"))
        p.Add("@set_line", pi1.Text("set_line"))
        p.Add("@line_cd", pi1.Text("line_cd"))
        p.Add("@line_sq", pi1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pi_ref1.Text)
        p.Add("@ref2", pi_ref2.Text)
        p.Add("@ref3", pi_ref3.Text)
        p.Add("@ref4", pi_ref4.Text)
        p.Add("@ref5", pi_ref5.Text)
        p.Add("@ref6", pi_ref6.Text)
        p.Add("@ref7", pi_ref7.Text)
        p.Add("@ref8", pi_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pi1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pi_kiosk.Text, pi1.Text("line_cd"), pi1.Text("vin_no"), pi1.Text("job_no"), 1, pi1.Text("line_sq"))

        init_kiosk("pi")

    End Sub

    Private Sub pi_btnTakeout_Click(sender As Object, e As EventArgs) Handles pi_btnTakeout.Click
        Takeout("pi")
    End Sub
#End Region

#Region "pj PD350250  Paint Small Parts"
    Private Sub pj_btnSearch_Click(sender As Object, e As EventArgs) Handles pj_btnSearch.Click
        Open("pdk000_pj1")
    End Sub

    Private Sub pj_vinNo_KeyDown(sender As Object, e As KeyEventArgs) Handles pj_fvinNo.KeyDown
        If e.KeyCode = 13 Then
            Open("pdk000_pj1")
        End If
    End Sub

    Private Sub pj_btnExec_Click(sender As Object, e As EventArgs) Handles pj_btnExec.Click

        Dim p As OpenParameters = New OpenParameters

        p.Add("@co_cd", "01")
        p.Add("@kiosk_cd", pj_kiosk.Text)

        p.Add("@job_no", pj1.Text("job_no"))
        p.Add("@set_line", pj1.Text("set_line"))
        p.Add("@line_cd", pj1.Text("line_cd"))
        p.Add("@line_sq", pj1.Text("line_sq"))

        p.Add("@mission_no", "")
        p.Add("@key_no", "")
        p.Add("@cabin_no", "")
        p.Add("@cargo_no", "")
        p.Add("@airbag_no", "")
        p.Add("@rearaxle_no", "")

        p.Add("@ref1", pj_ref1.Text)
        p.Add("@ref2", pj_ref2.Text)
        p.Add("@ref3", pj_ref3.Text)
        p.Add("@ref4", pj_ref4.Text)
        p.Add("@ref5", pj_ref5.Text)
        p.Add("@ref6", pj_ref6.Text)
        p.Add("@ref7", pj_ref7.Text)
        p.Add("@ref8", pj_ref8.Text)

        p.Add("@qty", 1)

        p.Add("@unique_no", pj1.Text("vin_no"))
        p.Add("@z_workers", z_workers.Text)
        p.Add("@dt", dt.Text)

        Open("pdk000_nextjob", p)
        'saveWork(pj_kiosk.Text, pj1.Text("line_cd"), pj1.Text("vin_no"), pj1.Text("job_no"), 1, pj1.Text("line_sq"))

        init_kiosk("pj")

    End Sub

    Private Sub pj_btnTakeout_Click(sender As Object, e As EventArgs) Handles pj_btnTakeout.Click
        Takeout("pj")
    End Sub
#End Region

#Region "Common Function"

#Region "z_Worker"
    Private Sub z_btnSearch_Click(sender As Object, e As EventArgs) Handles z_btnSearch.Click
        init_kiosk("z")
    End Sub

    Dim workerCnt As Integer = 0
    Dim workerCur As Integer = 0

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        For i = 0 To z02.RowCount - 1
            If z02.Text("emp_no", i) = z01.Text("emp_no", workerCur) Then
                Exit Sub
            End If
        Next

        z02.AllowAddRows = True
        z02.AddNewRow()
        z02.Text("emp_no") = z01.Text("emp_no", workerCur)

        z_workers.Text = ""
        For i = 0 To z02.RowCount - 1
            If i = z02.RowCount - 1 Then
                z_workers.Text = z_workers.Text + z02.Text("emp_no", i)
            Else
                z_workers.Text = z_workers.Text + z02.Text("emp_no", i) + ","
            End If
        Next
        z02.AllowAddRows = False

        workerCnt = z02.RowCount
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        workerCnt = 0
        z_workers.Text = ""
        Open("pdk000_z02")
    End Sub

    Private Sub z01_AfterMoveRow_1(sender As Object, PrevRowIndex As Integer, RowIndex As Integer) Handles z01.AfterMoveRow
        workerCur = RowIndex
    End Sub

#End Region

    Private Sub Takeout(str As String)
        Dim p As OpenParameters = New OpenParameters

        Select Case str
            Case "a"
                p.Add("@job_no", a10.Text("job_no"))
                p.Add("@kiosk_cd", a_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "b"
                p.Add("@job_no", b10.Text("job_no"))
                p.Add("@kiosk_cd", b_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "c"
                p.Add("@job_no", c10.Text("job_no"))
                p.Add("@kiosk_cd", c_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "d"
                p.Add("@job_no", d10.Text("job_no"))
                p.Add("@kiosk_cd", d_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "e"
                p.Add("@job_no", e10.Text("job_no"))
                p.Add("@kiosk_cd", e_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "f"
                p.Add("@job_no", f10.Text("job_no"))
                p.Add("@kiosk_cd", f_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "g"
                p.Add("@job_no", g10.Text("job_no"))
                p.Add("@kiosk_cd", g_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "h"
                p.Add("@job_no", h10.Text("job_no"))
                p.Add("@kiosk_cd", h_Kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "aa"
                p.Add("@job_no", aa1.Text("job_no"))
                p.Add("@kiosk_cd", aa_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ab"
                p.Add("@job_no", ab1.Text("job_no"))
                p.Add("@kiosk_cd", ab_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ac"
                p.Add("@job_no", ac1.Text("job_no"))
                p.Add("@kiosk_cd", ac_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ad"
                p.Add("@job_no", ad1.Text("job_no"))
                p.Add("@kiosk_cd", ad_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ae"
                p.Add("@job_no", ae1.Text("job_no"))
                p.Add("@kiosk_cd", ae_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "af"
                p.Add("@job_no", af1.Text("job_no"))
                p.Add("@kiosk_cd", af_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ag"
                p.Add("@job_no", ag1.Text("job_no"))
                p.Add("@kiosk_cd", ag_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pa"
                p.Add("@job_no", pa1.Text("job_no"))
                p.Add("@kiosk_cd", pa_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pb"
                p.Add("@job_no", pb1.Text("job_no"))
                p.Add("@kiosk_cd", pb_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pc"
                p.Add("@job_no", pc1.Text("job_no"))
                p.Add("@kiosk_cd", pc_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pd"
                p.Add("@job_no", pd1.Text("job_no"))
                p.Add("@kiosk_cd", pd_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pe"
                p.Add("@job_no", pe1.Text("job_no"))
                p.Add("@kiosk_cd", pe_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pf"
                p.Add("@job_no", pf1.Text("job_no"))
                p.Add("@kiosk_cd", pf_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pg"
                p.Add("@job_no", pg1.Text("job_no"))
                p.Add("@kiosk_cd", pg_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "ph"
                p.Add("@job_no", ph1.Text("job_no"))
                p.Add("@kiosk_cd", ph_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pi"
                p.Add("@job_no", pi1.Text("job_no"))
                p.Add("@kiosk_cd", pi_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case "pj"
                p.Add("@job_no", pj1.Text("job_no"))
                p.Add("@kiosk_cd", pj_kiosk.Text)
                Open("pdk000_xtakeout", p)
                init_kiosk(str)
            Case Else
                Exit Sub
        End Select
    End Sub
    Private Sub init_kiosk(str As String)
        If workerCnt = 0 And str <> "z" Then
            XtraTabControl1.SelectedTabPageIndex = 0
            str = "z"
            MsgBox("User selection first.")
        End If

        Select Case str
            Case "z"
                z01.RowHeight = 50
                z01.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_z01")
            Case "a"
                a10.RowHeight = 50
                a10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_aBasis")
                Open("pdk000_a10")
            Case "b"
                b10.RowHeight = 50
                b10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_bBasis")
                Open("pdk000_b10")
            Case "c"
                c10.RowHeight = 50
                c10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_cBasis")
                Open("pdk000_c10")
            Case "d"
                d10.RowHeight = 50
                d10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_dBasis")
                Open("pdk000_d10")
            Case "e"
                e10.RowHeight = 50
                e10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_eBasis")
                Open("pdk000_e10")
            Case "f"
                f10.RowHeight = 50
                f10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_fBasis")
                Open("pdk000_f10")
            Case "g"
                g10.RowHeight = 50
                g10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_gBasis")
                Open("pdk000_g10")

            Case "h"
                h10.RowHeight = 50
                h10.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_hBasis")
                Open("pdk000_h10")

            Case "aa"
                aa1.RowHeight = 50
                aa1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_aaBasis")
                Open("pdk000_aa1")
            Case "ab"
                ab1.RowHeight = 50
                ab1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_abBasis")
                Open("pdk000_ab1")

                selectAllText(ab_checkEngine)
            Case "ac"
                ac1.RowHeight = 50
                ac1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_acBasis")
                Open("pdk000_ac1")
            Case "ad"
                ad1.RowHeight = 50
                ad1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_adBasis")
                Open("pdk000_ad1")
            Case "ae"
                ae1.RowHeight = 50
                ae1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_aeBasis")
                Open("pdk000_ae1")
            Case "af"
                af1.RowHeight = 50
                af1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_afBasis")
                Open("pdk000_af1")
            Case "ag"
                ag1.RowHeight = 50
                ag1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_agBasis")
                Open("pdk000_ag1")
            Case "pa"
                pa1.RowHeight = 50
                pa1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_paBasis")
                Open("pdk000_pa1")
            Case "pb"
                pb1.RowHeight = 50
                pb1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pbBasis")
                Open("pdk000_pb1")
            Case "pc"
                pc1.RowHeight = 50
                pc1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pcBasis")
                Open("pdk000_pc1")
            Case "pd"
                pd1.RowHeight = 50
                pd1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pdBasis")
                Open("pdk000_pd1")
            Case "pe"
                pe1.RowHeight = 50
                pe1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_peBasis")
                Open("pdk000_pe1")
            Case "pf"
                pf1.RowHeight = 50
                pf1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pfBasis")
                Open("pdk000_pf1")
            Case "pg"
                pg1.RowHeight = 50
                pg1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pgBasis")
                Open("pdk000_pg1")
            Case "ph"
                ph1.RowHeight = 50
                ph1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_phBasis")
                Open("pdk000_ph1")
            Case "pi"
                pi1.RowHeight = 50
                pi1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_piBasis")
                Open("pdk000_pi1")
            Case "pj"
                pj1.RowHeight = 50
                pj1.Font = New Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Point)

                Open("pdk000_pjBasis")
                Open("pdk000_pj1")
            Case Else
        End Select

    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            init_kiosk("z")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            init_kiosk("a")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
            init_kiosk("b")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
            init_kiosk("c")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
            init_kiosk("d")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
            init_kiosk("e")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 6 Then
            init_kiosk("f")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 7 Then
            init_kiosk("g")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 8 Then
            init_kiosk("h")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 9 Then
            init_kiosk("aa")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 10 Then
            init_kiosk("ab")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 11 Then
            init_kiosk("ac")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 12 Then
            init_kiosk("ad")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 13 Then
            init_kiosk("ae")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 14 Then
            init_kiosk("af")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 15 Then
            init_kiosk("ag")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 16 Then
            init_kiosk("pa")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 17 Then
            init_kiosk("pb")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 18 Then
            init_kiosk("pc")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 19 Then
            init_kiosk("pd")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 20 Then
            init_kiosk("pe")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 21 Then
            init_kiosk("pf")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 22 Then
            init_kiosk("pg")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 23 Then
            init_kiosk("ph")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 24 Then
            init_kiosk("pi")
        ElseIf XtraTabControl1.SelectedTabPageIndex = 25 Then
            init_kiosk("pj")

        End If
    End Sub

#Region "selectAllText"

    Private Sub selectAllText(tb As eText)
        If Len(tb.Text) = 0 Then
            tb.Focus()
        Else
            tb.SelectionStart = 0
            tb.SelectionLength = Len(tb.Text)
            tb.Focus()
        End If
    End Sub
    Private Sub selectAllText(tb As TextBox)
        If Len(tb.Text) = 0 Then
            tb.Focus()
        Else
            tb.SelectionStart = 0
            tb.SelectionLength = Len(tb.Text)
        End If
    End Sub
#End Region

#End Region

    Private Sub ab_engineNo_DoubleClick(sender As Object, e As EventArgs) Handles ab_engineNo.DoubleClick
        ab_checkEngine.Text = ab_engineNo.Text
    End Sub
End Class
