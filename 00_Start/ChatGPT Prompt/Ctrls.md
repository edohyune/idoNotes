#### UCGridNav
```C#
using Dapper;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.ComponentModel;
using Lib;
using Lib.Repo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Charts.Native;
using DevExpress.XtraRichEdit.Model;
using System.Windows.Documents;
using DevExpress.XtraVerticalGrid;
using System.Windows.Forms;
using DevExpress.XtraEditors.Senders;
using System.Reflection;
using System.Collections;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using System.Windows.Controls;

namespace Ctrls
{
    public class UCGridNav : DevExpress.XtraGrid.GridControl
    {
        #region Properties Browseable(false) ----------------------------------------------------------
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DevExpress.XtraGrid.Views.Grid.GridView gvCtrl { get; private set; }
        [Browsable(false)]
        private string frwId { get; set; }
        [Browsable(false)]
        private string frmId { get; set; }
        [Browsable(false)]
        private string nmSpace { get; set; }
        [Browsable(false)]
        private string thisNm { get; set; }
        private WrkFldRepo wrkFldRepo { get; set; }
        private List<WrkFld> wrkFlds { get; set; }
        #endregion
        #region Properties Browseable(true) -----------------------------------------------------------
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int RowCount { get => gvCtrl.RowCount; }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int FocuseRowIndex
        {
            get => gvCtrl.FocusedRowHandle;
            set => gvCtrl.FocusedRowHandle = value;
        }
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public DataRow GetForcuseDataRow 
        //{ 
        //    get 
        //    {
        //        return this.gvCtrl.GetFocusedDataRow(); 
        //    } 
        //}
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public DataRow GetDataRow(int rowHandle) 
        //{ 
        //    return this.gvCtrl.GetDataRow(rowHandle); 
        //}
        [Category("A UserController Property"), Description("RowAutoHeigh")]
        public bool RowAutoHeigh
        {
            get => gvCtrl.OptionsView.RowAutoHeight;
            set => gvCtrl.OptionsView.RowAutoHeight = value;
        }
        [Category("A UserController Property"), Description("ColumnAutoWidth")]
        public bool ColumnAutoWidth
        {
            get => gvCtrl.OptionsView.ColumnAutoWidth;
            set => gvCtrl.OptionsView.ColumnAutoWidth = value;
        }
        [Category("A UserController Property"), Description("ShowGroupPanel")]
        public bool ShowGroupPanel
        {
            get => gvCtrl.OptionsView.ShowGroupPanel;
            set => gvCtrl.OptionsView.ShowGroupPanel = value;
        }
        [Category("A UserController Property"), Description("ShowFindPanel")]
        public bool ShowFindPanel
        {
            get => gvCtrl.OptionsFind.AlwaysVisible;
            set => gvCtrl.OptionsFind.AlwaysVisible = value;
        }
        [Category("A UserController Property"), Description("MultiSelect")]
        public bool MultiSelect
        {
            get => gvCtrl.OptionsSelection.MultiSelect;
            set => gvCtrl.OptionsSelection.MultiSelect = value;
        }
        [Category("A UserController Property"), Description("MultiSelectMode")]
        public GridMultiSelectMode MultiSelectMode
        {
            get => gvCtrl.OptionsSelection.MultiSelectMode;
            set => gvCtrl.OptionsSelection.MultiSelectMode = value;
        }
        #endregion
        #region Methods Documentation -----------------------------------------------------------------
        [EditorBrowsable(EditorBrowsableState.Always)]
        public T GetDoc<T>(int rowIndex)
        {
            T doc = (T)gvCtrl.GetRow(rowIndex);
            return doc;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public List<T> GetDocs<T>()
        {
            var list = (List<T>)this.DataSource;
            return list;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void AddNewDoc()
        {
            if (gvCtrl.RowCount == 0 || gvCtrl.GetFocusedDataSourceRowIndex() == gvCtrl.RowCount - 1)
            {
                this.New();
            }
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void SetDataSource<T>(List<T> lists)
        {
            this.DataSource = new BindingList<T>(lists);
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void Clear()
        {
            this.DataSource = null;
        }
        #endregion
        #region Methods Cell Value --------------------------------------------------------------------
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetText()
        {
            //focus된 Cell의 값을 가져온다.
            string rtn;
            if (gvCtrl.GetFocusedRowCellValue(gvCtrl.FocusedColumn.FieldName) == null)
            {
                rtn = string.Empty;
            }
            else
            {
                rtn = gvCtrl.GetFocusedRowCellValue(gvCtrl.FocusedColumn.FieldName).ToString();
            }
            return rtn;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetText(string columnName)
        {
            string rtn;
            if (gvCtrl.GetFocusedRowCellValue(columnName) == null)
            {
                rtn = string.Empty;
            }
            else
            {
                rtn = gvCtrl.GetFocusedRowCellValue(columnName).ToString();
            }
            return rtn;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetText(int columnIndex)
        {
            string columnName = GetColumnName(columnIndex);
            string rtn;
            if (gvCtrl.GetFocusedRowCellValue(columnName) == null)
            {
                rtn = string.Empty;
            }
            else
            {
                rtn = gvCtrl.GetFocusedRowCellValue(columnName).ToString();
            }
            return rtn;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetText(string columnName, int rowIndex)
        {
            if (rowIndex < 0)
            {
                return string.Empty;
            }
            else
            {
                string rtn;
                if (gvCtrl.GetRowCellValue(rowIndex, columnName) == null)
                {
                    rtn = string.Empty;
                }
                else
                {
                    rtn = gvCtrl.GetRowCellValue(rowIndex, columnName).ToString();
                }
                return rtn;
            }
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string GetText(int columnIndex, int rowIndex)
        {
            if (rowIndex < 0)
            {
                return string.Empty;
            }
            else
            {
                string columnName = GetColumnName(columnIndex);
                string rtn;

                if (gvCtrl.GetRowCellValue(rowIndex, columnName) == null)
                {
                    rtn = string.Empty;
                }
                else
                {
                    rtn = gvCtrl.GetRowCellValue(rowIndex, columnName).ToString();
                }
                return rtn;
            }
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        private string GetColumnName(int columnIndex)
        {
            string columnName = null;
            foreach (GridColumn item in gvCtrl.Columns)
            {
                if (item.FieldName == gvCtrl.Columns[columnIndex].FieldName)
                {
                    columnName = item.FieldName;
                }
            }
            return columnName;
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void SetText(string columnName, dynamic value)
        {
            int rowIndex = gvCtrl.GetFocusedDataSourceRowIndex();
            gvCtrl.SetRowCellValue(rowIndex, columnName, value);
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void SetText(int columnIndex, dynamic value)
        {
            int rowIndex = gvCtrl.GetFocusedDataSourceRowIndex();
            string columnName = GetColumnName(columnIndex);
            gvCtrl.SetRowCellValue(rowIndex, columnName, value);
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void SetText(string columnName, int rowIndex, dynamic value)
        {
            gvCtrl.SetRowCellValue(rowIndex, columnName, value);
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        public void SetText(int columnIndex, int rowIndex, dynamic value)
        {
            string columnName = GetColumnName(columnIndex);
            gvCtrl.SetRowCellValue(rowIndex, columnName, value);
        }
        #endregion
        #region EVENT ---------------------------------------------------------------------------------
        public delegate void delEventInitNewRow(object sender, InitNewRowEventArgs e);
        public event delEventInitNewRow UCInitNewRow;
        private void gvCtrl_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            WrkFldRepo wrkFldRepo = new WrkFldRepo();
            var wrkFlds = wrkFldRepo.GetColumnProperties(frwId, frmId, thisNm);
            foreach (var wrkFld in wrkFlds)
            {
                if (wrkFld.DefaultText != null)
                {
                    string defaultTxt = wrkFld.DefaultText;
                    defaultTxt = GenFunc.ReplaceGPatternQuery(defaultTxt);
                    using (var db = new GaiaHelper())
                    {
                        defaultTxt = db.ReplaceGVariables(defaultTxt);
                        gvCtrl.SetRowCellValue(e.RowHandle, wrkFld.FldNm, defaultTxt);
                    }
                }
            }
            UCInitNewRow?.Invoke(sender, e);
        }
        public delegate void delEvent4(object sender, RowDeletingEventArgs e);
        public event delEvent4 UCRowDeleting;
        private void gvCtrl_RowDeleting(object sender, RowDeletingEventArgs e)
        {
            UCRowDeleting?.Invoke(sender, e);
        }
        public delegate void delEventFocusedRowChanged(object sender, int preIndex, int rowIndex, FocusedRowChangedEventArgs e);
        public event delEventFocusedRowChanged UCFocusedRowChanged;
        public void gvCtrl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.PrevFocusedRowHandle >= 0 && gvCtrl.IsNewItemRow(e.PrevFocusedRowHandle))
            {
                gvCtrl.UpdateCurrentRow(); // 포커스 이동 시 행 추가 확정
            }

            if (UCFocusedRowChanged != null && e.FocusedRowHandle >= 0)
            {
                List<WrkSet> ctrls = new WrkSetRepo().SetPushFlds(frwId, frmId, thisNm);
                if (ctrls != null)
                {
                    var fieldInfo = ctrls.ToDictionary(x => x.FldNm, x => x.ToolNm);
                    var mapping = ctrls.ToDictionary(x => x.FldNm, x => x.SetFldNm);

                    foreach (var item in fieldInfo)
                    {
                        // item.Key에 해당하는 매핑된 컬럼 이름을 가져옴
                        string columnName = mapping.ContainsKey(item.Key) ? item.Key : null;
                        if (columnName != null)
                        {
                            // GetText 메서드를 사용하여 값을 가져옴
                            var fieldValue = this.GetText(columnName);
                            if (fieldValue != null)
                            {
                                Common.gMsg = $"Enter Value({fieldValue}) into Control({mapping[item.Key]})";
                                SetControlValue(this.FindForm(), mapping[item.Key], item.Value, fieldValue);
                            }
                            else
                            {
                                Common.gMsg = $"Value for column {columnName} is null.";
                            }
                        }
                        else
                        {
                            Common.gMsg = $"Column name for key {item.Key} is null.";
                        }
                    }
                }
                UCFocusedRowChanged(sender, e.PrevFocusedRowHandle, e.FocusedRowHandle, e);
            }
        }
        public void SetControlValue(Form uc, string ctrlNm, string toolNm, dynamic value)
        {
            var ctrl = uc.Controls.Find(ctrlNm, true).FirstOrDefault(); if (ctrl != null)
            {
                var controlType = toolNm.ToLower(); 
                var bindValue = value.ToString(); // 각 컨트롤 유형별로 바인드할 속성 정보를 정의합니다.
                var bindPropertyMapping = new CtrlMstRepo().GetBindPropertyMapping();
                if (bindPropertyMapping.ContainsKey(controlType))
                {
                    var propertyName = bindPropertyMapping[controlType];
                    var property = ctrl.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        var convertedValue = Convert.ChangeType(bindValue, property.PropertyType);
                        property.SetValue(ctrl, convertedValue);
                    }
                }
            }
        }
        public delegate void delEventCellValueChanged(object sender, CellValueChangedEventArgs e);
        public event delEventCellValueChanged UCCellValueChanged;
        private void gvCtrl_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            UCCellValueChanged?.Invoke(sender, e);
        }

        public delegate void delEventCellValueChanging(object sender, CellValueChangedEventArgs e);
        public event delEventCellValueChanging UCCellValueChanging;
        private void gvCtrl_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            UCCellValueChanging?.Invoke(sender, e);
        }
        #endregion

        private object OSearchParam;
        private DynamicParameters DSearchParam;
        private bool ColumnsInitYn = false;

        public UCGridNav()
        {
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvCtrl = new DevExpress.XtraGrid.Views.Grid.GridView(this);
            this.MainView = this.gvCtrl;
            this.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gvCtrl });
            this.gvCtrl.GridControl = this;
            this.gvCtrl.Name = "gvCtrl";

            this.Load += ucGridSet_Load;

            this.gvCtrl.FocusedRowChanged += gvCtrl_FocusedRowChanged;
            this.gvCtrl.InitNewRow += gvCtrl_InitNewRow;
            this.gvCtrl.RowDeleting += gvCtrl_RowDeleting;
            this.gvCtrl.CellValueChanged += gvCtrl_CellValueChanged;
            this.gvCtrl.CellValueChanging += gvCtrl_CellValueChanging;

            // 클립보드 관련 이벤트 핸들러 추가
            this.gvCtrl.KeyDown += gvCtrl_KeyDown;
            this.EmbeddedNavigator.ButtonClick += gcCtrls_EmbeddedNavigator_ButtonClick;
        }

        private void ucGridSet_Load(object? sender, EventArgs e)
        {
            frwId = Common.GetValue("gFrameWorkId");

            Form? form = this.FindForm();
            if (form != null)
            {
                frmId = form.Name;
            }
            else
            {
                frmId = "Unknown";
            }

            thisNm = this.Name;

            if (frwId != string.Empty)
            {
                ResetColumns();
            }
        }
        #region ResetColumns() - Preview Form ---------------------------------------------------------
        private void ResetColumns()
        {
            WrkFldRepo wrkFldRepo = new WrkFldRepo();
            List<WrkFld> colProperties = wrkFldRepo.GetColumnProperties(frwId, frmId, thisNm);
            gvCtrl.Columns.Clear();
            GridDefine();
            if (colProperties != null)
            {
                foreach (var column in colProperties)
                {
                    AddGridColumn(gvCtrl, GetTempColumn(column) as GridColumn);
                }
                gvCtrl.RefreshData();
            }
        }

        private GridColumn GetTempColumn(WrkFld column)
        {
            //column의 내용으로 GridColumn을 생성하여 반환, TitleWidth Caption, TitleAlign, TextAlign, ShowYn, FixYn의 내용만 이용하면 된다. 
            GridColumn gridColumn = new GridColumn();
            gridColumn.Name = column.FldNm;
            gridColumn.FieldName = column.FldNm;
            gridColumn.Caption = column.FldTitle;
            gridColumn.Width = column.FldTitleWidth;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = GenFunc.StrToAlign(column.TitleAlign);
            gridColumn.Visible = column.ShowYn;
            gridColumn.Fixed = (column.FixYn ? DevExpress.XtraGrid.Columns.FixedStyle.None : DevExpress.XtraGrid.Columns.FixedStyle.Left);

            if (!string.IsNullOrEmpty(column.Band1))
            {
                // 밴드 설정은 그리드 뷰에 밴드가 있는 경우에만 유효합니다.
                // 이 부분은 그리드에 밴드가 구성되어 있을 때 추가적인 로직이 필요합니다.
                // 예: column.OwnerBand = gridView.Bands[band1];
            }
            if (!string.IsNullOrEmpty(column.Band2))
            {
                // 예: column.OwnerBand = gridView.Bands[band2];
            }
            return gridColumn;
        }
        #endregion
        #region Open() - Open Form --------------------------------------------------------------------
        public void Open()
        {
            if (thisNm==null){ return; }

            #region Model Type 설정으로 Open<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";

            Type modelType = GetModelType(modelName);

            if (modelType == null)
            {
                MessageBox.Show($"Model type '{modelName}' not found.{Environment.NewLine}Please define the model({modelName}) first");
                return;
            }

            MethodInfo method = this.GetType().GetMethods().First(m => m.Name == "Open" && m.IsGenericMethod);
            MethodInfo genericMethod = method.MakeGenericMethod(modelType);
            genericMethod.Invoke(this, null);
            #endregion
        }

        public void Open<T>()
        {
            WrkGetRepo wrkGetRepo = new WrkGetRepo();
            List<WrkGet> wrkGets = wrkGetRepo.GetPullFlds(frwId, frmId, thisNm);
            DSearchParam = new DynamicParameters();

            foreach (var wrkGet in wrkGets)
            {
                string value = GetParamValue(this.FindForm().Controls, wrkGet);
                DSearchParam.Add(wrkGet.FldNm, value);
            }
            OpenWrk<T>();
        }

        private void OpenWrk<T>()
        {
            this.DataSource = null;
            //1. GridControl Configuration  GridDefine(); 위에서 이미처리 해서 생략
            //2. Grid Column Configuration
            if (!ColumnsInitYn)
            {
                gvCtrl.Columns.Clear();
                try
                {
                    WrkFldRepo wrkFldRepo = new WrkFldRepo();
                    List<WrkFld> colProperties = wrkFldRepo.GetColumnProperties(frwId, frmId, thisNm);

                    if (colProperties != null)
                    {
                        foreach (var column in colProperties)
                        {
                            AddGridColumn(gvCtrl, GetGridColumn(column) as GridColumn);
                        }
                        gvCtrl.RefreshData();
                        ColumnsInitYn = true;  // 컬럼이 초기화되었음을 플래그로 표시
                    }

                    //3. Data Source Binding
                    Common.gMsg = $"-- Select Query : {frwId}.{frmId}.{thisNm} ------------------------";
                    var sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = "R" });
                    sql = GenFunc.ReplaceGPatternQuery(sql);

                    List<T> lists = new List<T>();

                    using (var db = new GaiaHelper())
                    {
                        if (DSearchParam != null)
                        {
                            lists = db.Query<T>(sql, DSearchParam);
                        }
                        else if (OSearchParam != null)
                        {
                            lists = db.Query<T>(sql, OSearchParam);
                        }
                        else
                        {
                            lists = db.Query<T>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = thisNm });
                        }
                    }
                    Common.gMsg = $"-- End Select Query : {frwId}.{frmId}.{thisNm} ---------------------";
                    foreach (dynamic item in lists)
                    {
                        item.ChangedFlag = MdlState.None;
                    }

                    this.DataSource = new BindingList<T>(lists);
                }
                catch (Exception e)
                {
                    Common.gMsg = $"-- Exception -------------------------------";
                    Common.gMsg = $"UCGridNav_OpenWrk<T>() : {frwId}.{frmId}.{thisNm} {Environment.NewLine} Exception :";
                    Common.gMsg = $"{e.Message}";
                    Common.gMsg = $"-- End Exception ---------------------------";
                    return;
                }
            }
        }
        #endregion
        #region Save - Save Data ----------------------------------------------------------------------
        public void Save()
        {
            if (thisNm == null) { return; }

            #region Model Type 설정으로 Save<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";

            Type modelType = GetModelType(modelName);

            if (modelType == null)
            {
                MessageBox.Show($"Model type '{modelName}' not found.{Environment.NewLine}Please define the model({modelName}) first");
                return;
            }

            MethodInfo method = this.GetType().GetMethods().First(m => m.Name == "Open" && m.IsGenericMethod);
            MethodInfo genericMethod = method.MakeGenericMethod(modelType);
            genericMethod.Invoke(this, null);
            #endregion
        }
        public void Save<T>()
        {
            if (gvCtrl.IsEditing)
            {
                gvCtrl.CloseEditor();
            }
            if (gvCtrl.FocusedRowModified)
            {
                gvCtrl.UpdateCurrentRow();
            }

            WrkRefRepo wrkRefRepo = new WrkRefRepo();
            List<WrkRef> wrkRefs = wrkRefRepo.RefDataFlds(frwId, frmId, thisNm);

            var list = (BindingList<T>)this.DataSource;
            if (list != null)
            {
                using (var db = new GaiaHelper())
                {
                    var changedItems = list.Where(item => IsChanged(item)).ToList();

                    foreach (dynamic item in changedItems)
                    {
                        string sql = string.Empty;
                        int returnValue = 0;

                        string operation = GetOperation(GetChangedFlag(item));

                        if (!string.IsNullOrEmpty(operation))
                        {
                            sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = operation });

                            if (string.IsNullOrWhiteSpace(sql))
                            {
                                Common.gMsg = $"'{operation}'쿼리가 존재하지 않습니다.";
                                return;
                            }

                            sql = GenFunc.ReplaceGPatternQuery(sql);

                            foreach (var wrkRef in wrkRefs)
                            {
                                sql = sql.Replace($"{wrkRef.FldNm}", $"'{RefParamValue(this.FindForm().Controls, wrkRef)}'");
                            }
                            returnValue = db.OpenExecute(sql, item);
                            Common.gMsg = $"--[{thisNm}] END {operation}---------------->>";
                        }
                    }
                }
                //OnSaveCompleted(); 저장후 처리할 이벤트를 발동 시킬수 있음 - Documemt개체와 연동하여 처리할 수 있음
            }
        }
        private string GetOperation(MdlState state)
        {
            switch (state)
            {
                case MdlState.Inserted:
                    return "C";
                case MdlState.Updated:
                    return "U";
                case MdlState.Deleted:
                    return "D";
                default:
                    return string.Empty;
            }
        }
        private bool IsChanged<T>(T item)
        {
            var prop = typeof(T).GetProperty("ChangedFlag");
            if (prop != null)
            {
                var value = (MdlState)prop.GetValue(item);
                return value != MdlState.None;
            }
            return false;
        }
        private MdlState GetChangedFlag(object item)
        {
            var prop = item.GetType().GetProperty("ChangedFlag");
            if (prop != null)
            {
                return (MdlState)prop.GetValue(item);
            }
            return MdlState.None;
        }
        #endregion
        private void SetChangedFlag(object? item, MdlState state)
        {
            var prop = item.GetType().GetProperty("ChangedFlag");
            if (prop != null)
            {
                prop.SetValue(item, state);
            }
        }
        #region New() Delete() - Temporarily Delete Data ----------------------------------------------
        private void gcCtrls_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                //해당 로우만 그리드에서 삭제 (데이터베이스에서 삭제하지 않음)
                case NavigatorButtonType.Remove:
                    e.Handled= true;
                    Delete();
                    break;
                //해당 로우만 저장
                case NavigatorButtonType.EndEdit:
                    gvCtrl.UpdateCurrentRow(); //수정된 내용을 바인딩 데이터에 반영한다.
                    break;
                case NavigatorButtonType.Append:
                    e.Handled = true;
                    New();
                    break;
                case NavigatorButtonType.CancelEdit:
                    gvCtrl.CancelUpdateCurrentRow();
                    break;
                case NavigatorButtonType.Custom:
                    if (e.Button.Tag.ToString() == "Query")
                    {
                        this.Open();
                    }
                    break;
            }
        }

        public void Delete()
        {
            this.SetText("ChangedFlag", MdlState.Deleted);
            gvCtrl.DeleteRow(gvCtrl.FocusedRowHandle);
        }

        public void New() 
        {
            gvCtrl.AddNewRow();
        }
        #endregion

        #region Copy Paste Clipboard ------------------------------------------------------------------
        private void gvCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedRowsToClipboard();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardData();
                e.Handled = true;
            }
        }
        private void CopySelectedRowsToClipboard()
        {
            gvCtrl.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            gvCtrl.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;

            gvCtrl.CopyToClipboard();
        }
        private void PasteClipboardData()
        {
            try
            {
                var clipboardText = Clipboard.GetText().TrimEnd('\r', '\n');
                var rows = clipboardText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int startRowHandle = gvCtrl.FocusedRowHandle;
                if (startRowHandle == GridControl.InvalidRowHandle)
                {
                    startRowHandle = 0; // 포커스된 행이 없으면 0번째 행부터 시작
                }
                int startColumnIndex = gvCtrl.FocusedColumn?.VisibleIndex ?? 0; // 포커스된 열이 없으면 0번째 열부터 시작

                foreach (var row in rows)
                {
                    var columns = row.Split(new[] { '\t' }, StringSplitOptions.None); // 탭으로 셀 분리

                    // 빈 행은 건너뛰기
                    if (columns.Length == 1 && string.IsNullOrWhiteSpace(columns[0]))
                    {
                        continue;
                    }

                    // 필요한 경우 새 행 추가
                    if (startRowHandle >= gvCtrl.DataRowCount)
                    {
                        gvCtrl.AddNewRow();
                        startRowHandle = gvCtrl.GetRowHandle(gvCtrl.DataRowCount - 1);
                    }

                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (startColumnIndex + i >= gvCtrl.VisibleColumns.Count) break; // 보이는 컬럼 수보다 많으면 중단

                        // 셀 값 설정 (데이터 형식 변환 필요 시 추가)
                        gvCtrl.SetRowCellValue(startRowHandle, gvCtrl.VisibleColumns[startColumnIndex + i], columns[i]);
                    }
                    startRowHandle++;
                }

                gvCtrl.UpdateCurrentRow();
                gvCtrl.RefreshData(); // 데이터 변경 사항 반영
            }
            catch (Exception ex)
            {
                MessageBox.Show($"붙여넣기 중 오류가 발생했습니다: {ex.Message}");
            }
        }
        #endregion
        #region GridDefine() - Grid Control Configuration (Options, Navigator, etc.) ------------------
        private void GridDefine()
        {
            FrmWrkRepo frmWrkRepo = new FrmWrkRepo();
            FrmWrk ucInfo = frmWrkRepo.GetByWorkSet(frwId, frmId, thisNm);

            if (ucInfo != null)
            {
                //gvCtrl.OptionsFind.AlwaysVisible = (ucInfo.OptionsFind_chk == "0" ? false : true);
                gvCtrl.OptionsFind.AlwaysVisible = true;
                gvCtrl.OptionsFind.AllowFindPanel = true;
                gvCtrl.OptionsFind.ShowCloseButton = true;
                gvCtrl.OptionsFind.ShowClearButton = true;
                gvCtrl.OptionsFind.ShowFindButton = true;

                //gvCtrl.OptionsView.ShowGroupPanel = (ucInfo.ShowGroupPanel_chk == "0" ? false : true);
                //gvCtrl.OptionsView.ShowFooter = (ucInfo.ShowFooter_chk == "0" ? false : true);
                //gvCtrl.OptionsView.ColumnAutoWidth = (ucInfo.ColumnAutoWidth_chk == "0" ? false : true);
                //gvCtrl.OptionsView.EnableAppearanceEvenRow = (ucInfo.EvenRow_chk == "0" ? false : true);
                gvCtrl.OptionsView.ShowGroupPanel = true;
                gvCtrl.OptionsView.ShowFooter = true;
                gvCtrl.OptionsView.ColumnAutoWidth = true;
                gvCtrl.OptionsView.EnableAppearanceEvenRow = true;

                gvCtrl.OptionsView.ShowIndicator = true;
                gvCtrl.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                gvCtrl.OptionsView.RowAutoHeight = true;
                gvCtrl.OptionsView.ColumnAutoWidth = false; // 컬럼 너비 자동 조정


                //gvCtrl.OptionsBehavior.Editable = (ucInfo.Edit_chk == "0" ? false : true);
                gvCtrl.OptionsBehavior.Editable = true;
                gvCtrl.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;

                gvCtrl.OptionsCustomization.AllowColumnMoving = true;
                gvCtrl.OptionsCustomization.AllowColumnResizing = true;
                gvCtrl.OptionsCustomization.AllowFilter = true;
                gvCtrl.OptionsCustomization.AllowSort = true;


                gvCtrl.OptionsSelection.MultiSelect = ucInfo.MultiSelect;
                gvCtrl.OptionsSelection.MultiSelectMode = GenFunc.StrToSelectMode(ucInfo.SelectMode);

                gvCtrl.OptionsSelection.EnableAppearanceFocusedCell = true;
                gvCtrl.OptionsSelection.EnableAppearanceFocusedRow = true;

                gvCtrl.FocusRectStyle = DrawFocusRectStyle.RowFocus;

                gvCtrl.OptionsMenu.EnableColumnMenu = false;
                gvCtrl.OptionsMenu.EnableFooterMenu = false;
                gvCtrl.OptionsMenu.EnableGroupPanelMenu = false;
                gvCtrl.OptionsMenu.ShowAddNewSummaryItem = DefaultBoolean.True;
                gvCtrl.OptionsMenu.ShowAutoFilterRowItem = true;
                gvCtrl.OptionsMenu.ShowDateTimeGroupIntervalItems = true;
                gvCtrl.OptionsMenu.ShowGroupSortSummaryItems = true;
                gvCtrl.OptionsMenu.ShowGroupSummaryEditorItem = true;
                gvCtrl.OptionsMenu.ShowSplitItem = true;

                gvCtrl.OptionsNavigation.AutoFocusNewRow = true;

                gvCtrl.Appearance.HeaderPanel.Options.UseTextOptions = true;
                gvCtrl.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                gvCtrl.DetailHeight = 300;
                gvCtrl.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                gvCtrl.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
                gvCtrl.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.VisualAndText;
                gvCtrl.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
                gvCtrl.OptionsPrint.AllowMultilineHeaders = true;

                GridNavigator(this, ucInfo.NavAdd, ucInfo.NavDelete, ucInfo.NavSave, ucInfo.NavCancel);
            }
        }

        private void GridNavigator(GridControl gc, bool navAdd, bool navDelete, bool navSave, bool navCancel)
        {
            ControlNavigator navigator = gc.EmbeddedNavigator;
            navigator.Buttons.BeginUpdate();
            navigator.Buttons.CustomButtons.Clear();
            try
            {
                navigator.Buttons.Append.Visible = navAdd;
                navigator.Buttons.Remove.Visible = navDelete;
                navigator.Buttons.EndEdit.Visible = navSave;
                navigator.Buttons.CancelEdit.Visible = navCancel;

                navigator.Buttons.Edit.Enabled = false;
                navigator.Buttons.Edit.Visible = false;
                navigator.Buttons.First.Enabled = false;
                navigator.Buttons.First.Visible = false;
                navigator.Buttons.Last.Enabled = false;
                navigator.Buttons.Last.Visible = false;
                navigator.Buttons.Next.Enabled = false;
                navigator.Buttons.Next.Visible = false;
                navigator.Buttons.NextPage.Enabled = false;
                navigator.Buttons.NextPage.Visible = false;
                navigator.Buttons.Prev.Enabled = false;
                navigator.Buttons.Prev.Visible = false;
                navigator.Buttons.PrevPage.Enabled = false;
                navigator.Buttons.PrevPage.Visible = false;
                navigator.ShowToolTips = false;
                navigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
                navigator.Buttons.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] { new DevExpress.XtraEditors.NavigatorCustomButton(-1, 11, true, true, "", "Query") });
            }
            finally
            {
                navigator.Buttons.EndUpdate();
            }
            gc.UseEmbeddedNavigator = true;
        }
        #endregion
        #region Grid Column Configuration -------------------------------------------------------------
        private GridColumn GetGridColumn(WrkFld wrkFld)
        {
            GridColumn column = new GridColumn();

            column.Name = wrkFld.FldNm;
            column.FieldName = wrkFld.FldNm;
            column.Tag = wrkFld.NeedYn;
            column.Caption = wrkFld.FldTitle;
            column.Width = wrkFld.FldTitleWidth;
            column.Visible = wrkFld.ShowYn;
            column.OptionsColumn.AllowEdit = wrkFld.EditYn;
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            column.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            column.Fixed = (wrkFld.FixYn ? DevExpress.XtraGrid.Columns.FixedStyle.None : DevExpress.XtraGrid.Columns.FixedStyle.Left);
            column.UnboundType = DevExpress.Data.UnboundColumnType.String;

            column.AppearanceHeader.TextOptions.HAlignment = GenFunc.StrToAlign(wrkFld.TitleAlign);
            column.AppearanceCell.TextOptions.HAlignment = GenFunc.StrToAlign(wrkFld.TextAlign);


            if (wrkFld.FormatStr == "")
                column.DisplayFormat.FormatType = FormatType.None;
            else
            {
                switch (wrkFld.FldTy)
                {
                    case "Decimal":
                        column.DisplayFormat.FormatType = FormatType.Numeric;
                        break;
                    case "Int":
                        column.DisplayFormat.FormatType = FormatType.Numeric;
                        break;
                    case "Date":
                        column.DisplayFormat.FormatType = FormatType.DateTime;
                        break;
                    case "DateTime":
                        column.DisplayFormat.FormatType = FormatType.DateTime;
                        break;
                    default:
                        column.DisplayFormat.FormatType = FormatType.Custom;
                        break;
                }
                column.DisplayFormat.FormatString = wrkFld.FormatStr;
            }

            switch (wrkFld.FldTy.ToUpper())
            {
                case "CHECKBOX":
                    column.ColumnEdit = SetCheckBox();
                    break;
                case "CODE":
                    column.ColumnEdit = SetColumnLookup(wrkFld.Popup);//SetLookupCode(popup, 1);
                    break;
                case "CODE2":
                    column.ColumnEdit = SetColumnLookup_Value(wrkFld.Popup); //SetLookupCode(popup, 2);
                    break;
                case "COMBO":
                    column.ColumnEdit = SetLookupCombo(wrkFld.Popup, 1);
                    break;
                case "COMBO2":
                    column.ColumnEdit = SetLookupCombo(wrkFld.Popup, 2);
                    break;
                case "POPUP":
                    RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
                    buttonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
                    buttonEdit.Buttons[0].Caption = "Select";
                    buttonEdit.ButtonClick += (s, e) =>
                    {
                        // 팝업 창을 열거나 원하는 동작을 수행
                        MessageBox.Show("팝업을 표시합니다.");
                    };
                    column.ColumnEdit = buttonEdit;
                    break;
                case "MEMO":
                    RepositoryItemMemoEdit MemoEdit = new RepositoryItemMemoEdit();
                    MemoEdit.AutoHeight = true;
                    MemoEdit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    MemoEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    MemoEdit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
                    column.ColumnEdit = MemoEdit;
                    break;
                default:
                    break;
            }
            return column;
        }
        private RepositoryItemCheckEdit SetCheckBox()
        {
            RepositoryItemCheckEdit chkbox = new RepositoryItemCheckEdit();
            chkbox.NullText = "0";
            chkbox.ValueChecked = "1";
            chkbox.ValueUnchecked = "0";
            chkbox.ValueGrayed = "0";
            chkbox.NullStyle = StyleIndeterminate.Unchecked;
            chkbox.CheckStyle = CheckStyles.Radio;
            return chkbox;
        }
        private RepositoryItem SetColumnLookup(string pcode)
        {
            RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();
            return lookUp;
        }
        private RepositoryItemLookUpEdit SetColumnLookup_Value(string pcode)
        {
            RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();
            return lookUp;
        }
        private RepositoryItemLookUpEdit SetLookupCode(string grp, string opt)
        {
            RepositoryItemLookUpEdit lookup = new RepositoryItemLookUpEdit();
            return lookup;
        }
        private RepositoryItemLookUpEdit SetLookupCombo(object pcode, int opt)
        {
            RepositoryItemLookUpEdit LookUp = new RepositoryItemLookUpEdit();

            LookUp.DataSource = null;
            LookUp.ValueMember = "Code";
            LookUp.DisplayMember = "Code";

            LookUp.ShowHeader = false;
            LookUp.ForceInitialize();
            LookUp.PopulateColumns();
            LookUp.Columns["Code"].Visible = false;

            LookUp.DropDownRows = 10;// dsLook.Tables[0].Rows.Count;
            LookUp.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            LookUp.AutoHeight = true;
            LookUp.NullText = "";
            LookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            LookUp.AutoSearchColumnIndex = 1;
            LookUp.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            LookUp.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            LookUp.CaseSensitiveSearch = false;

            return LookUp;
        }

        private void AddGridColumn(DevExpress.XtraGrid.Views.Grid.GridView gridV, GridColumn gridC)
        {
            if (gridC != null)
            {
                gridV.Columns.Add(gridC);
            }
        }
        #endregion

        #region Grid Control Get/Reference Parmameters ------------------------------------------------
        private string GetParamValue(ControlCollection frm, WrkGet wrkGet)
        {
            string str = string.Empty;

            if (string.IsNullOrEmpty(wrkGet.GetWrkId))
            {
                if (string.IsNullOrEmpty(wrkGet.GetFldNm))
                {
                    str = wrkGet.GetDefalueValue;
                }
                else
                {
                    dynamic tbx = frm.Find(wrkGet.GetFldNm, true).FirstOrDefault();
                    str = tbx.Text;
                }
            }
            else
            {
                dynamic tbx = frm.Find(wrkGet.GetWrkId, true).FirstOrDefault();
                str = tbx.GetText(wrkGet.GetFldNm);
            }
            return str;
        }
        private string RefParamValue(ControlCollection frm, WrkRef wrkRef)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(wrkRef.RefWrkId))
            {
                if (string.IsNullOrEmpty(wrkRef.RefFldNm))
                {
                    str = wrkRef.RefDefalueValue;
                }
                else
                {
                    dynamic tbx = frm.Find(wrkRef.RefFldNm, true).FirstOrDefault();
                    str = tbx.Text;
                }
            }
            else
            {
                dynamic tbx = frm.Find(wrkRef.RefWrkId, true).FirstOrDefault();
                str = tbx.GetText(wrkRef.RefFldNm);
            }
            return str;
        }
        #endregion

        private Type GetModelType(string modelName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var modelType = assembly.GetType(modelName);
                if (modelType != null)
                {
                    return modelType;
                }
            }
            return null;
        }
    }
}

```
#### UCCodeBox
```C#
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib;
using Lib.Repo;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraBars;
using static DevExpress.Accessibility.LookupPopupAccessibleObject;
using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid.Native;

namespace Ctrls
{
    [System.ComponentModel.DefaultBindingProperty("Code")]
    [System.ComponentModel.DefaultEvent("UCSelectedIndexChanged")]
    public partial class UCCodeBox : DevExpress.XtraEditors.XtraUserControl, INotifyPropertyChanged
    {
        #region [Browsable(false)] --------------------------------------------------------->>
        [Browsable(false)]
        private string frwId { get; set; }
        [Browsable(false)]
        private string frmId { get; set; }
        [Browsable(false)]
        private string thisNm { get; set; }
        [Browsable(false)]
        private string FldTy { get; set; }
        [Browsable(false)]
        public FrwCde CodeZip
        {
            get
            {
                if (cmbCtrl.SelectedItem is FrwCde selectedItem)
                {
                    return selectedItem;
                }
                return null;
            }
        }
        [Browsable(false)]
        public string Code
        {
            get
            {
                if (cmbCtrl.SelectedItem is FrwCde selectedItem)
                {
                    if (this.FldTy == "SubCd")
                    {
                        return selectedItem.SubCd;
                    }
                    else
                    { 
                        return selectedItem.Cd;
                    }
                }
                return null;
            }
            set
            {
                foreach (FrwCde item in cmbCtrl.Properties.Items)
                {
                    if (this.FldTy == "SubCd")
                    {
                        if (item.SubCd == value)
                        {
                            cmbCtrl.SelectedItem = item;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Cd == value)
                        {
                            cmbCtrl.SelectedItem = item;
                            break;
                        }
                    }
                }
                OnPropertyChanged();
            }
        }
        #endregion
        #region Properties Browsable(true) ------------------------------------------------->>
        [Category("A UserController Property"), Description("Height")]
        public int ControlHeight
        {
            get
            {
                return this.Height;
            }
            set
            {
                this.Height = value;
            }
        }
        [Category("A UserController Property"), Description("Width")] //chk
        public int ControlWidth
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }
        [Category("A UserController Property"), Description("Title Width")] //chk
        public int TitleWidth
        {
            get
            {
                return splitCtrl.SplitterDistance;
            }
            set
            {
                this.splitCtrl.SplitterDistance = value;
            }
        }
        [Category("A UserController Property"), Description("Title")] //CHK
        public string Title
        {
            get
            {
                return this.labelCtrl.Text;
            }
            set
            {
                this.labelCtrl.Text = value;
            }
        }
        [Category("A UserController Property"), Description("Title Alignment")] //chk
        public DevExpress.Utils.HorzAlignment TitleAlignment
        {
            get
            {
                return this.labelCtrl.Appearance.TextOptions.HAlignment;
            }
            set
            {
                this.labelCtrl.Appearance.TextOptions.HAlignment = value;
            }
        }
        [Category("A UserController Property"), Description("Default Text"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                string str = (this.cmbCtrl.Text == null) ? string.Empty : this.cmbCtrl.Text.ToString();
                return str;
            }
            set
            {
                this.cmbCtrl.Text = value;
            }
        }
        //fontFace랑 fontSize 추가
        [Category("A UserController Property"), Description("Font Face")] //chk
        public string FontFace
        {
            get
            {
                return this.cmbCtrl.Font.Name;
            }
            set
            {
                this.labelCtrl.Font = new Font(value, this.cmbCtrl.Font.Size);
                this.cmbCtrl.Font = new Font(value, this.cmbCtrl.Font.Size);
            }
        }
        [Category("A UserController Property"), Description("Font Size")] //chk
        public float FontSize
        {
            get
            {
                return this.cmbCtrl.Font.Size;
            }
            set
            {
                this.cmbCtrl.Font = new Font(this.cmbCtrl.Font.Name, value);
                this.labelCtrl.Font = new Font(this.labelCtrl.Font.Name, value);
            }
        }
        [Category("A UserController Property"), Description("Text Alignment")] //chk
        public DevExpress.Utils.HorzAlignment TextAlignment
        {
            get
            {
                return this.cmbCtrl.Properties.Appearance.TextOptions.HAlignment;
            }
            set
            {
                this.cmbCtrl.Properties.Appearance.TextOptions.HAlignment = value;
            }
        }
        [Category("A UserController Property"), Description("Necessary")] //chk
        private bool NeedYn { get; set; }
        [Category("A UserController Property"), Description("Editable=Enable=Not ReadOnly")] //chk
        public bool EditYn
        {
            get
            {
                return !(cmbCtrl.ReadOnly);
            }
            set
            {
                cmbCtrl.ReadOnly = !value;
            }
        }
        [Category("A UserController Property"), Description("Visiable")] //chk
        public bool ShowYn
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }
        //[Category("A UserController Property"), Description("EditValue")] //chk
        //public object EditValue
        //{
        //    get
        //    {
        //        return this.cmbCtrl.EditValue;
        //    }
        //    set
        //    {
        //        this.cmbCtrl.EditValue = value;
        //    }
        //}
        #endregion
        public UCCodeBox()
        {
            InitializeComponent();
        }
        private void UCCodeBox_Load(object sender, EventArgs e)
        {
            frwId = Common.GetValue("gFrameWorkId");

            Form? form = this.FindForm();
            frmId = form != null ? form.Name : "Unknown";
            thisNm = this.Name;

            if (!string.IsNullOrEmpty(frwId))
            {
                ResetCtrl();
            }
            else
            {
                this.ControlHeight = 21;
                this.Title = this.Name;
                this.Text = "";
            }
        }
        private void ResetCtrl()
        {
            try
            {
                this.ControlHeight = 21;
                var wrkFldRepo = new WrkFldRepo();
                var wrkFld = wrkFldRepo.GetFldProperties(frwId, frmId, thisNm);
                if (wrkFld != null)
                {
                    this.FldTy = wrkFld.FldTy;
                    //wrkFld.FldX와 wrkFld.FldY를 사용하여 위치 설정
                    this.Location = new Point(wrkFld.FldX, wrkFld.FldY);
                    this.ControlWidth = wrkFld.FldWidth;
                    this.TitleWidth = wrkFld.FldTitleWidth;
                    this.Title = wrkFld.FldTitle;
                    this.TitleAlignment = GenFunc.StrToAlign(wrkFld.TitleAlign);
                    this.Code = wrkFld.DefaultText;
                    this.TextAlignment = GenFunc.StrToAlign(wrkFld.TextAlign);
                    //this. = wrkFld.FixYn;
                    //this. = wrkFld.GroupYn;
                    this.ShowYn = wrkFld.ShowYn;
                    this.NeedYn = wrkFld.NeedYn;
                    this.EditYn = wrkFld.EditYn;
                    //this.ButtonVisiable = wrkFld.EditYn;
                    //this. = wrkFld.Band1;
                    //this. = wrkFld.Band2;
                    //this. = wrkFld.FuncStr;
                    //SetFuncStr(wrkFld.FuncStr);
                    //this. = wrkFld.FormatStr;
                    //SetFormatStr(wrkFld.FuncStr);
                    //this. = wrkFld.ColorFont;
                    //this. = wrkFld.ColorBg;
                    //this. = wrkFld.Seq;

                    if (wrkFld.Popup != "")
                    {
                        List<FrwCde> frwCdes = new FrwCdeRepo().GetFrwCdesForCodeBox(frwId, wrkFld.Popup);
                        foreach (FrwCde frwCde in frwCdes)
                        {
                            cmbCtrl.Properties.Items.Add(frwCde);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.Common.gMsg = $"UCCodeBox_Load>>ResetCtrl{Environment.NewLine}Exception : {ex.Message}";
            }
        }

        #region Event ---------------------------------------------------------------->>
        public delegate void delEventSelectedIndexChanged(object sender, EventArgs e);
        public event delEventSelectedIndexChanged UCSelectedIndexChanged;
        private void cmbCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCSelectedIndexChanged?.Invoke(sender, e);
        }
        public delegate void delEventSelectedValueChanged(object sender, EventArgs e);
        public event delEventSelectedValueChanged UCSelectedValueChanged;
        private void cmbCtrl_SelectedValueChanged(object sender, EventArgs e)
        {
            UCSelectedValueChanged?.Invoke(sender, e);
        }
        #endregion

        #region INotifyPropertyChanged ----------------------------------------------->>
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
```
#### UCTextBox
```C#
using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.Drawing.Printing.Internal;
using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.Utils.DirectXPaint;
using Lib;
using Lib.Repo;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Windows.Controls.Primitives;
using DevExpress.Utils.Implementation;

namespace Ctrls
{
    [System.ComponentModel.DefaultBindingProperty("BindText")]
    [System.ComponentModel.DefaultEvent("UCEditValueChanged")]
    public partial class UCTextBox : UserControl, INotifyPropertyChanged
    {
        [Browsable(false)]
        private string frwId { get; set; }
        private string frmId { get; set; }
        private string ctrlNm { get; set; }

        #region Properties Browsable(true)
        [Category("A UserController Property"), Description("Width")] //chk
        public int ControlWidth
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }
        [Category("A UserController Property"), Description("Height")]
        public int ControlHeight
        {
            get 
            {
                return this.Height;
            }
            set
            {
                this.Height = value;
            }
        }
        [Category("A UserController Property"), Description("Title Width")] //chk
        public int TitleWidth
        {
            get
            {
                return splitCtrl.SplitterDistance;
            }
            set
            {
                this.splitCtrl.SplitterDistance = value;
            }
        }
        [Category("A UserController Property"), Description("Title")] //CHK
        public string Title
        {
            get
            {
                return this.labelCtrl.Text;
            }
            set
            {
                this.labelCtrl.Text = value;
            }
        }
        [Category("A UserController Property"), Description("Title Alignment")] //chk
        public DevExpress.Utils.HorzAlignment TitleAlignment
        {
            get
            {
                return this.labelCtrl.Appearance.TextOptions.HAlignment;
            }
            set
            {
                this.labelCtrl.Appearance.TextOptions.HAlignment = value;
            }
        }
        [Category("A UserController Property"), Description("Default Text"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                string str = (this.textCtrl.Text == null) ? string.Empty : this.textCtrl.Text;
                return str;
            }
            set
            {
                this.textCtrl.Text = value;
                this.BindText = value;  // Text가 업데이트 될 때 BindText도 업데이트
            }
        }
        //fontFace랑 fontSize 추가
        [Category("A UserController Property"), Description("Font Face")] //chk
        public string FontFace
        {
            get
            {
                return this.textCtrl.Font.Name;
            }
            set
            {
                this.labelCtrl.Font = new Font(value, this.textCtrl.Font.Size);
                this.textCtrl.Font = new Font(value, this.textCtrl.Font.Size);
            }
        }
        [Category("A UserController Property"), Description("Font Size")] //chk
        public float FontSize
        {
            get
            {
                return this.textCtrl.Font.Size;
            }
            set
            {
                this.textCtrl.Font = new Font(this.textCtrl.Font.Name, value);
                this.labelCtrl.Font = new Font(this.labelCtrl.Font.Name, value);
            }
        }
        [Category("A UserController Property"), Description("Text Alignment")] //chk
        public DevExpress.Utils.HorzAlignment TextAlignment
        {
            get
            {
                return this.textCtrl.Properties.Appearance.TextOptions.HAlignment;
            }
            set
            {
                this.textCtrl.Properties.Appearance.TextOptions.HAlignment = value;
            }
        }
        [Category("A UserController Property"), Description("Necessary")] //chk
        private bool NeedYn { get; set; }
        [Category("A UserController Property"), Description("Editable=Enable=Not ReadOnly")] //chk
        public bool EditYn
        {
            get
            {
                return !(textCtrl.ReadOnly);
            }
            set
            {
                textCtrl.ReadOnly = !value;
            }
        }
        [Category("A UserController Property"), Description("Text Button Visiable")]
        public bool ButtonVisiable
        {
            get
            {
                bool result = textCtrl.Properties.Buttons[0].Visible;
                return result;
            }
            set
            {
                textCtrl.Properties.Buttons[0].Visible = value;
            }
        }
        [Category("A UserController Property"), Description("Visiable")] //chk
        public bool ShowYn
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }
        #endregion
        #region Properties Browsable(false)
        [Category("A UserController Property"), Description("Bind Text"), Browsable(false)]
        public string BindText
        {
            get
            {
                string str = (this.textCtrl.Text == null) ? string.Empty : this.textCtrl.Text;
                return str;
            }
            set
            {
                this.textCtrl.Text = value;
                OnPropertyChanged("BindText");
                UCEditValueChanged?.Invoke(this, textCtrl);
            }
        }
        #endregion

        #region Event Delegate
        public delegate void delEventButtonClick(object Sender, Control control);
        public event delEventButtonClick UCButtonClick;
        private void textCtrl_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                string strText = string.Empty;
                strText = textCtrl.Text;
                if (UCButtonClick != null)
                {
                    UCButtonClick(this, textCtrl);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        public UCTextBox()
        {
            InitializeComponent();
        }

        private void UCTextBox_Load(object sender, EventArgs e)
        {
            frwId = Common.GetValue("gFrameWorkId");

            Form? form = this.FindForm();
            frmId = form != null ? form.Name : "Unknown";
            ctrlNm = this.Name;

            if (!string.IsNullOrEmpty(frwId))
            {
                ResetCtrl();
            }
        }

        private void ResetCtrl()
        {
            try
            {
                this.ControlHeight = 21;

                var wrkFldRepo = new WrkFldRepo();
                var wrkFld = wrkFldRepo.GetFldProperties(frwId, frmId, ctrlNm);
                if (wrkFld != null)
                {
                    //wrkFld.FldX와 wrkFld.FldY를 사용하여 위치 설정
                    this.Location = new Point(wrkFld.FldX, wrkFld.FldY);
                    this.ControlWidth = wrkFld.FldWidth;
                    this.ControlHeight = wrkFld.FldHeight;
                    this.TitleWidth = wrkFld.FldTitleWidth;
                    this.Title = wrkFld.FldTitle;
                    this.TitleAlignment = GenFunc.StrToAlign(wrkFld.TitleAlign);
                    //this. = wrkFld.Popup;
                    this.Text = wrkFld.DefaultText;
                    this.TextAlignment = GenFunc.StrToAlign(wrkFld.TextAlign);
                    //this. = wrkFld.FixYn;
                    //this. = wrkFld.GroupYn;
                    this.ShowYn = wrkFld.ShowYn;
                    this.NeedYn = wrkFld.NeedYn;
                    this.EditYn = wrkFld.EditYn;
                    this.ButtonVisiable = wrkFld.EditYn;
                    //this. = wrkFld.Band1;
                    //this. = wrkFld.Band2;
                    //this. = wrkFld.FuncStr;
                    SetFuncStr(wrkFld.FuncStr);
                    //this. = wrkFld.FormatStr;
                    SetFormatStr(wrkFld.FuncStr);
                    //this. = wrkFld.ColorFont;
                    //this. = wrkFld.ColorBg;
                    //this. = wrkFld.Seq;

                    //추가설정
                    if (wrkFld.FldTy == "TextButton")
                    {
                        this.ButtonVisiable = true;
                    }
                    else
                    {
                        this.ButtonVisiable = false;
                    }
                }
                else
                {
                    this.Text = string.Empty;
                    this.ShowYn = true;
                    this.EditYn = true;
                }

            }
            catch (Exception ex)
            {
                Lib.Common.gMsg = $"UCTextBox_Load>>ResetCtrl{Environment.NewLine}Exception : {ex.Message}";
            }
        }

        private void SetFormatStr(string funcStr)
        {
            Lib.Common.gMsg = $"SetFormatStr";
        }

        private void SetFuncStr(string funcStr)
        {
            Lib.Common.gMsg = $"SetFuncStr";
        }

        #region FrameWork Value 전달을 위한 함수
        public string GetParamValue(ControlCollection frm, string param_name, string wkset, string field)
        {
            string str = string.Empty;
            if (wkset != "Field")
            {
                dynamic tbx = frm.Find(wkset, true).FirstOrDefault();
                str = tbx.GetText(field);
            }
            else
            {
                dynamic tbx = frm.Find(field, true).FirstOrDefault();
                str = tbx.BindText;
            }
            return str;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public delegate void delEventEditValueChanged(object Sender, Control control);   // delegate 선언
        public event delEventEditValueChanged UCEditValueChanged;   // event 선언
        private void textCtrl_EditValueChanged(object sender, EventArgs e)
        {
            if (UCEditValueChanged != null)  // 부모가 Event를 생성하지 않았을 수 있으므로 생성 했을 경우에만 Delegate를 호출
            {
                UCEditValueChanged(this, textCtrl);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void textCtrl_TextChanged(object sender, EventArgs e)
        {
            BindText = textCtrl.Text;
        }
        #endregion
    }
}

```
#### UCRichText
```C#
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Office.Utils;
using Lib.Repo;
using static DevExpress.Accessibility.LookupPopupAccessibleObject;

namespace Ctrls
{
    [System.ComponentModel.DefaultBindingProperty("BindText")]
    [System.ComponentModel.DefaultEvent("UCContentChanged")]
    public class UCRichText : DevExpress.XtraRichEdit.RichEditControl, INotifyPropertyChanged
    {
        #region Properties Browsable(flase) -------------------------------------------------------->>
        [Browsable(false)]
        private string frwId { get; set; }
        [Browsable(false)]
        private string frmId { get; set; }
        [Browsable(false)]
        private string thisNm { get; set; }
        [Category("A UserController Property"), Description("Bind Text"), Browsable(false)]
        public string BindText
        {
            get
            {
                string str = (this.Text == null) ? string.Empty : this.Text;
                return str;
            }
            set
            {
                if (this.Text != value)
                {
                    this.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
        #region Properties Browsable(true) --------------------------------------------------------->>
        [Category("A UserController Property"), Description("Default Text")] //chk
        public override string Text
        {
            get
            {
                string str = (base.Text == null) ? string.Empty : base.Text;
                return str;
            }
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    this.BindText = value;  // Text가 업데이트 될 때 BindText도 업데이트
                }   
            }
        }
        [Category("A UserController Property"), Description("Necessary")] //chk
        private bool NeedYn { get; set; }
        [Category("A UserController Property"), Description("Editable=Enable=Not ReadOnly")] //chk
        public bool EditYn
        {
            get
            {
                return !(this.ReadOnly);
            }
            set
            {
                this.ReadOnly = !value;
            }
        }
        [Category("A UserController Property"), Description("Visiable")] //chk
        public bool ShowYn
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }
        #endregion

        public UCRichText(ISyntaxHighlightService syntax)
        {
            Initialize();
            this.ReplaceService<ISyntaxHighlightService>(syntax);
        }
        public UCRichText()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.Dock = DockStyle.Fill;
            this.Name = "rtSelect";
            this.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            this.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            this.Modified = true;
            this.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            this.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;

            this.Options.Search.RegExResultMaxGuaranteedLength = 500;
            this.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            this.ContentChanged += UCRichText_ContentChanged;
            this.TextChanged += UCRichText_TextChanged;

            HandleCreated += UCRichText_HandleCreated;
        }

        private void UCRichText_HandleCreated(object? sender, EventArgs e)
        {
            frwId = Common.GetValue("gFrameWorkId");

            Form? form = this.FindForm();
            frmId = form != null ? form.Name : "Unknown";
            thisNm = this.Name;

            if (!string.IsNullOrEmpty(frwId))
            {
                ResetCtrl();
            }
        }

        private void ResetCtrl()
        {
            try
            {
                var wrkFldRepo = new WrkFldRepo();
                var wrkFld = wrkFldRepo.GetFldProperties(frwId, frmId, thisNm);
                if (wrkFld != null)
                {
                    this.ShowYn = wrkFld.ShowYn;
                    this.NeedYn = wrkFld.NeedYn;
                    this.EditYn = wrkFld.EditYn;
                    this.Text = wrkFld.DefaultText;
                }
                else 
                {
                    this.ShowYn = true;
                    this.EditYn = true;
                    this.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Lib.Common.gMsg = $"UCRichText_HandleCreated>>ResetCtrl{Environment.NewLine}Exception : ";
                Lib.Common.gMsg = $"{ex.Message}";
                throw;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public delegate void delEventContentChanged(object Sender, EventArgs e);   // delegate 선언
        public event delEventContentChanged UCContentChanged;   // event 선언
        private void UCRichText_ContentChanged(object? sender, EventArgs e)
        {
            if (UCContentChanged != null)  // 부모가 Event를 생성하지 않았을 수 있으므로 생성 했을 경우에만 Delegate를 호출
            {
                UCContentChanged.Invoke(this, e);
            }
        }
        private void UCRichText_TextChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(BindText));
            UCContentChanged?.Invoke(this, e);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}

```
