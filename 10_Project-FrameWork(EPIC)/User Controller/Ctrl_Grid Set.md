---
Start Date:
Status:
Concept: false
Manifestation: false
Integration: false
Done:
tags:
CDT: <% tp.file.creation_date() %>
---
---
#### Prologue / Concept
###### UCGrid0619
- EmbeddedNavigator
- NewRow시 2 Line 생성으로 백업 및 다음 버전으로 들어감
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
        [Browsable(false)]
        private object OSearchParam;
        [Browsable(false)]
        private DynamicParameters DSearchParam;
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
        //public delegate void delEvent(object sender, EventArgs e);   // delegate 선언
        //public event delEvent UCBeforeLeaveRow;
        //private void gvCtrl_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        //{
        //    UCBeforeLeaveRow?.Invoke(sender, e);
        //}
        //public delegate void delEventSelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e);   // delegate 선언
        //public event delEventSelectionChanged UCSelectionChanged;
        //private void gvCtrl_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    UCSelectionChanged?.Invoke(sender, e);
        //}
        /*
        gvCtrl.SelectionChanged += gvForms_SelectionChanged;
        private void gvForms_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

        }
         */
        public delegate void delEventInitNewRow(object sender, InitNewRowEventArgs e);   // delegate 선언
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
                    defaultTxt = GenFunc.ReplaceGPatternVariable(defaultTxt);
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

        public delegate void delEventFocusedRowChanged(object sender, int preIndex, int rowIndex, FocusedRowChangedEventArgs e);   // delegate 선언
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
            #region Old Code
            //    var ctrl = uc.Controls.Find(ctrlNm, true).FirstOrDefault();
            //    if (ctrl != null)
            //    {
            //        switch (toolNm.ToLower())
            //        {
            //            case "uctextbox":
            //            case "uctext":
            //                UCTextBox uctxt = ctrl as UCTextBox;
            //                if (uctxt != null)
            //                {
            //                    uctxt.BindText = value.ToString();
            //                }
            //                break;
            //            case "ucdatebox":
            //            case "ucdate":
            //                UCDateBox ucdate = ctrl as UCDateBox;
            //                if (ucdate != null)
            //                {
            //                    ucdate.BindText = value.ToString();
            //                }
            //                break;
            //            case "uccombo":
            //                UCLookUp uccombo = ctrl as UCLookUp;
            //                if (uccombo != null)
            //                {
            //                    uccombo.BindText = value.ToString();
            //                }
            //                break;
            //            case "uccheckbox":
            //                UCCheckBox uccheckbox = ctrl as UCCheckBox;
            //                if (uccheckbox != null)
            //                {
            //                    uccheckbox.BindValue = value;
            //                }
            //                break;
            //            case "ucmemo":
            //                UCMemo ucmemo = ctrl as UCMemo;
            //                if (ucmemo != null)
            //                {
            //                    ucmemo.BindText = value.ToString();
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            #endregion
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
            //this.gvCtrl.BeforeLeaveRow += gvCtrl_BeforeLeaveRow;
            //this.gvCtrl.SelectionChanged += gvCtrl_SelectionChanged;
            this.gvCtrl.InitNewRow += gvCtrl_InitNewRow;
            this.gvCtrl.RowDeleting += gvCtrl_RowDeleting;
            this.gvCtrl.CellValueChanged += gvCtrl_CellValueChanged;
            this.gvCtrl.CellValueChanging += gvCtrl_CellValueChanging;

            // 클립보드 관련 이벤트 핸들러 추가
            //this.gvCtrl.ClipboardRowPasting += gvCtrl_ClipboardRowPasting;
            this.gvCtrl.KeyDown += gvCtrl_KeyDown;

            this.EmbeddedNavigator.ButtonClick += gcCtrls_EmbeddedNavigator_ButtonClick;


            //this.gvCtrl.MouseDown += gvCtrl_MouseDown;
            //this.gvCtrl.MouseMove += gvCtrl_MouseMove;
            //this.DragDrop += gcGrid_DragDrop;
            //this.DragEnter += gcGrid_DragEnter;
            //this.EmbeddedNavigator.Buttons.CustomButtons.Clear();
            //this.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] { new DevExpress.XtraEditors.NavigatorCustomButton(-1, 11, true, true, "", "Query") });
        }

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
            //클리보드의 내용을 그리드에 붙여넣기

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
        #region PreView<T>() - Preview Form -----------------------------------------------------------
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
        #region Open<T>() - Open Form -----------------------------------------------------------------
        public void Open()
        {
            if (thisNm==null)
            {
                return;
            }
            #region Model Type 설정으로 Open<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";

            // 1. 모델 타입을 동적으로 설정
            //string assemblyName = typeof(UCGridNav).Assembly.FullName;
            //Type modelType = Type.GetType($"{modelName}, {assemblyName}");
            //if (modelType == null)
            //{
            //    throw new InvalidOperationException($"Model type '{modelName}' not found.");
            //}

            // 2. 어셈블리를 명시적으로 로드하고 타입을 검색
            Type modelType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                modelType = assembly.GetType(modelName);
                if (modelType != null)
                {
                    break;
                }
            }

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
            Common.gMsg = $"{Environment.NewLine}-- {thisNm}.Open<T>() ------------------------>>";

            WrkGetRepo wrkGetRepo = new WrkGetRepo();
            List<WrkGet> wrkGets = wrkGetRepo.GetPullFlds(frwId, frmId, thisNm);
            DSearchParam = new DynamicParameters();

            foreach (var wrkGet in wrkGets)
            {
                string tmp = GetParamValue(this.FindForm().Controls, wrkGet);
                DSearchParam.Add(wrkGet.FldNm, tmp);
                Common.gMsg = $"Declare {wrkGet.FldNm} varchar ='{tmp}'";
            }
            OpenWrk<T>();
        }
        private void OpenWrk<T>()
        {
            this.DataSource = null;
            gvCtrl.Columns.Clear();
            //1. GridControl Configuration
            GridDefine();
            //2. Grid Column Configuration
            try
            {
                WrkFldRepo wrkFldRepo = new WrkFldRepo();
                List<WrkFld> colProperties = wrkFldRepo.GetColumnProperties(frwId, frmId, thisNm);
                gvCtrl.Columns.Clear();
                if (colProperties != null)
                {
                    foreach (var column in colProperties)
                    {
                        AddGridColumn(gvCtrl, GetGridColumn(column) as GridColumn);// Text Color
                        Common.gMsg = $"Added column: {column.FldNm}";
                    }
                    gvCtrl.RefreshData();
                    Common.gMsg = "Columns refreshed.";

                    //3. Data Source Binding
                    Common.gMsg = $"-- {thisNm}.Select Query ------------------------>>";
                    var sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = "R" });
                    sql = GenFunc.ReplaceGPatternVariable(sql);

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

                    Common.gMsg = $"-- {thisNm}.End Select Query -------------------->>";
                    foreach (dynamic item in lists)
                    {
                        item.ChangedFlag = MdlState.None;
                    }

                    this.DataSource = new BindingList<T>(lists);
                }
            }
            catch (Exception e)
            {
                Common.gMsg = $"-- {thisNm}. Exception ----------------------------->>";
                Common.gMsg = $"UCGridCode_OpenForm<T>() : {Environment.NewLine}--frwId : {frwId}{Environment.NewLine}-- frmId : {frmId}{Environment.NewLine}-- WorkSet : {thisNm}{Environment.NewLine}Exception :";
                Common.gMsg = $"{e.Message}";
                Common.gMsg = $"-- {thisNm}.End Exception -------------------------->>";
            }
        }
        #endregion
        #region Grid Column Configuration -------------------------------------------------------------
        private GridColumn GetGridColumn(WrkFld wrkFld)
        {
            GridColumn column = new GridColumn();

            column.Name = wrkFld.FldNm;
            column.FieldName = wrkFld.FldNm;
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
            chkbox.CheckStyle = CheckStyles.Radio; // CheckStyles.Standard;
            return chkbox;
        }
        private RepositoryItem SetColumnLookup(string pcode)
        {
            RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();

            //using (var db = new ACE.Lib.DbHelper())
            //{
            //}
            //List<MdlIdName> lists = new List<MdlIdName>();
            //lists = db.GetCodeNm(new { Grp = pcode });

            //lookUp.DataSource = lists;
            //lookUp.ValueMember = "Id";
            //lookUp.DisplayMember = "Nm";

            //lookUp.ShowHeader = false;
            //lookUp.ForceInitialize();
            //lookUp.PopulateColumns();
            //lookUp.Columns["Id"].Visible = true;
            //lookUp.Columns["Nm"].Visible = true;
            //lookUp.DropDownRows = 15; //dsLook.Tables[0].Rows.Count;
            //lookUp.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            //lookUp.AutoHeight = true;
            //lookUp.NullText = "";
            //lookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //lookUp.AutoSearchColumnIndex = 1;
            //lookUp.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            //lookUp.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            //lookUp.CaseSensitiveSearch = false;

            return lookUp;
        }
        private RepositoryItemLookUpEdit SetColumnLookup_Value(string pcode)
        {
            RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();

            //List<MdlIdName> lists = new List<MdlIdName>();
            //lists = db.GetNmNm(new { Grp = pcode });
            //lookUp.DataSource = lists;
            //lookUp.ValueMember = "Id";
            //lookUp.DisplayMember = "Nm";

            //lookUp.ShowHeader = false;
            //lookUp.ForceInitialize();
            //lookUp.PopulateColumns();
            //lookUp.Columns["Id"].Visible = false;
            //lookUp.Columns["Nm"].Visible = true;
            //lookUp.DropDownRows = 15; //dsLook.Tables[0].Rows.Count;
            //lookUp.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            //lookUp.AutoHeight = true;
            //lookUp.NullText = "";
            //lookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //lookUp.AutoSearchColumnIndex = 1;
            //lookUp.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            //lookUp.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            //lookUp.CaseSensitiveSearch = false;
            return lookUp;
        }
        private RepositoryItemLookUpEdit SetLookupCode(string grp, string opt)
        {
            RepositoryItemLookUpEdit lookup = new RepositoryItemLookUpEdit();
            //List<MdlIdName> lists;

            //if (opt == "0")
            //{
            //    lists = db.GetNmNm(new { Grp = grp }, "0"); //컬럼에서는 항상 ALL제외 
            //}
            //else
            //{
            //    lists = db.GetCodeNm(new { Grp = grp }, "0"); //컬럼에서는 항상 ALL제외 
            //}

            //lookup.DataSource = lists;
            //lookup.ValueMember = "Id";
            //lookup.DisplayMember = "Nm";
            //lookup.ShowHeader = false;
            //lookup.ForceInitialize();
            //lookup.PopulateColumns();
            //lookup.Columns["Id"].Visible = true;
            //lookup.Columns["Nm"].Visible = true;
            //lookup.DropDownRows = 10; //lookup.count
            //lookup.BestFitMode = BestFitMode.BestFitResizePopup;
            //lookup.AutoHeight = true;
            //lookup.NullText = "";
            //lookup.TextEditStyle = TextEditStyles.Standard;
            //lookup.AutoSearchColumnIndex = 1;
            //lookup.SearchMode = SearchMode.OnlyInPopup;
            //lookup.HeaderClickMode = HeaderClickMode.AutoSearch;
            //lookup.CaseSensitiveSearch = false;
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
                //gvCtrl.Appearance.FocusedRow.BackColor = Color.FromArgb(255, 255, 192);
                //gvCtrl.Appearance.SelectedRow.BackColor = Color.FromArgb(255, 255, 192);
                //gvCtrl.Appearance.SelectedRow.Options.UseBackColor = true;

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
        #region Save<T>() - Save Data -----------------------------------------------------------------
        public void Save()
        {
            #region Model Type 설정으로 Open<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";

            Type modelType = null;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                modelType = assembly.GetType(modelName);
                if (modelType != null)
                {
                    break;
                }
            }

            if (modelType == null)
            {
                throw new InvalidOperationException($"Model type '{modelName}' not found.");
            }

            MethodInfo method = this.GetType().GetMethods().First(m => m.Name == "Save" && m.IsGenericMethod);
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

            var list = (BindingList<T>)this.DataSource;
            if (list != null)
            {
                using (var db = new GaiaHelper())
                {
                    var changedItems = list.Where(item => IsChanged(item)).ToList();

                    foreach (dynamic item in changedItems)
                    {
                        WrkRefRepo wrkRefRepo = new WrkRefRepo();
                        List<WrkRef> wrkRefs = wrkRefRepo.RefDataFlds(frwId, frmId, thisNm);

                        string sql = string.Empty;
                        int returnValue = 0;

                        string operation = GetOperation(GetChangedFlag(item));

                        if (!string.IsNullOrEmpty(operation))
                        {
                            sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = operation });
                            if (string.IsNullOrWhiteSpace(sql))
                            {
                                Common.gMsg = $"쿼리가 존재하지 않습니다.";
                                return;
                            }
                            sql = GenFunc.ReplaceGPatternVariable(sql);
                            foreach (var wrkRef in wrkRefs)
                            {
                                Common.gMsg = $"--[{thisNm}] {operation}-------------------->>";
                                Common.gMsg = $"--Grid Save As Parameters------------------->>";
                                sql = sql.Replace($"{wrkRef.FldNm}", $"'{RefParamValue(this.FindForm().Controls, wrkRef)}'");
                            }
                            Common.gMsg = sql;
                            returnValue = db.OpenExecute(sql, item);
                            Common.gMsg = $"--[{thisNm}] END {operation}---------------->>";
                        }
                    }
                }
                OnSaveCompleted();
            }
        }

        private void OnSaveCompleted()
        {
            //OpenForm<T>();
            Common.gMsg = $"--[{thisNm}] END Delete--------------------{Environment.NewLine}";
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

        private void gcCtrls_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            var list = this.DataSource as IList;
            
            switch (e.Button.ButtonType)
            {
                //해당 로우만 그리드에서 삭제 (데이터베이스에서 삭제하지 않음)
                case NavigatorButtonType.Remove:
                    //list.RemoveAt(FocuseRowIndex); // 그리드에서만 항목 제거
                    //var item = list[FocuseRowIndex];
                    //포커스 된 로우의 ChangedFlag를 Deleted로 변경
                    if (list != null && FocuseRowIndex >= 0 && FocuseRowIndex < list.Count)
                    {
                        var itemRemove = list[FocuseRowIndex];
                        SetChangedFlag(itemRemove, MdlState.Deleted);
                        gvCtrl.RefreshData();
                    }
                    break;
                //해당 로우만 저장
                case NavigatorButtonType.EndEdit:
                    //SaveFocuseRow(); //true : 포커스된 로우만 저장
                    if (gvCtrl.IsEditing)
                    {
                        gvCtrl.CloseEditor(); //수정중인 작업을 완료한다. (수정중인 셀을 닫는다.)
                    }
                    if (gvCtrl.FocusedRowModified)
                    {
                        gvCtrl.UpdateCurrentRow(); //수정된 내용을 바인딩 데이터에 반영한다.
                    }

                    if (list != null)
                    {
                        using (var db = new GaiaHelper())
                        {
                            int returnValue = 0;
                            if (list != null && FocuseRowIndex >= 0 && FocuseRowIndex < list.Count)
                            {
                                var itemSave = list[FocuseRowIndex];
                                var state = GetChangedFlag(itemSave);

                                string sql = GenFunc.GetSql(new { FrwId = frwId, FrmId = frmId, WrkId = thisNm, CRUDM = GetOperation(state) });

                                sql = GenFunc.ReplaceGPatternVariable(sql);

                                WrkRefRepo wrkRefRepo = new WrkRefRepo();
                                List<WrkRef> wrkRefs = wrkRefRepo.RefDataFlds(frwId, frmId, thisNm);
                                foreach (var wrkRef in wrkRefs)
                                {
                                    sql = sql.Replace($"{wrkRef.FldNm}", $"'{RefParamValue(this.FindForm().Controls, wrkRef)}'");
                                }

                                if (!string.IsNullOrEmpty(sql))
                                {
                                    returnValue = db.OpenExecute(sql, itemSave);
                                }

                                SetChangedFlag(itemSave, MdlState.None); //변경된 상태를 초기화
                            }
                        }
                    }
                    break;
                //신규문서작업
                case NavigatorButtonType.Append:
                    AddNewDoc();
                    
                    break;
                //선택 문서의 입력 취소
                //그리드에서 삭제되지 않은데이터에 대해서 취소작업이 되는지 안된다면 MdlStat를 변경할것
                case NavigatorButtonType.CancelEdit:
                    
                    if (gvCtrl.IsEditing)
                    {
                        gvCtrl.CancelUpdateCurrentRow();
                    }
                    break;
                default:
                    if (e.Button.Tag.ToString() == "Query")
                    {
                        this.Open();
                    }
                    break;
            }
        }

        private void SetChangedFlag(object? item, MdlState state)
        {
            var prop = item.GetType().GetProperty("ChangedFlag");
            if (prop != null)
            {
                prop.SetValue(item, state);
            }
        }


        #region Delete<T>() - Temporarily Delete Data -------------------------------------------------
        public void Delete()
        {
            #region Model Type 설정으로 New<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";
            Type modelType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                modelType = assembly.GetType(modelName);
                if (modelType != null)
                {
                    break;
                }
            }

            if (modelType == null)
            {
                MessageBox.Show($"Model type '{modelName}' not found.{Environment.NewLine}Please define the model({modelName}) first");
                return;
            }

            MethodInfo method = this.GetType().GetMethods().First(m => m.Name == "Delete" && m.IsGenericMethod);
            MethodInfo genericMethod = method.MakeGenericMethod(modelType);
            genericMethod.Invoke(this, null);
            #endregion
        }
        public void Delete<T>()
        {
            var list = (BindingList<T>)this.DataSource;
            if (list != null && FocuseRowIndex >= 0 && FocuseRowIndex < list.Count)
            {
                list.RemoveAt(FocuseRowIndex); // 그리드에서만 항목 제거
            }
        }

        public void New() 
        {
            #region Model Type 설정으로 New<T>() 호출
            string modelName = $"{GenFunc.GetFormNamespace(frwId, frmId)}_{thisNm.ToUpper()}";
            Type modelType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                modelType = assembly.GetType(modelName);
                if (modelType != null)
                {
                    break;
                }
            }

            if (modelType == null)
            {
                MessageBox.Show($"Model type '{modelName}' not found.{Environment.NewLine}Please define the model({modelName}) first");
                return;
            }

            MethodInfo method = this.GetType().GetMethods().First(m => m.Name == "New" && m.IsGenericMethod);
            MethodInfo genericMethod = method.MakeGenericMethod(modelType);
            genericMethod.Invoke(this, null);
            #endregion
        }
        public void New<T>()
        {
            if (this.DataSource is BindingList<T> bindingList)
            {
                T newRow = Activator.CreateInstance<T>(); // 새로운 데이터 객체 생성
                bindingList.Add(newRow);
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
    }
}

```

내장 Navigator 기능을 활용하여 코드를 간소화 한다. 
Save(), New(), Delete()를 수행한다. 
EmbeddedNavigator에는 위의 기능과 연결할 수 있는 기능이 있다. 
EndEdit는 Save, 
Append는 New, 
Remove는 Delete로 대응 될 수 있다. 

EndEdit는 선택된 로우 Status Update
Append는 New
Remove는 선택된 로우 Status Delete 

this.Save() MdlState.None이 아닌것을 모두 처리하다. 
this.New() 는 EmbeddedNavigator의 Append
this.Delete() 는 는 EmbeddedNavigator의 Remove를 수행한다.

#### Manifestation
---
- [ ] [[Ctrl_Standard Grid]]
- [ ] [[Ctrl_Pivot Grid]]
- [ ] [[Ctrl_Tree Grid]]
---
###### UCGridSet Repository 처리
```C#
#region Repository Open Save ------------------------------------------------------------------
public void Open<T>(MdlRepo repo)
{
   var dataList = repo.Open<T>();
   Open(dataList);
}

public void Save<T>(MdlRepo repo)
{
   var dataSource = this.DataSource as BindingList<T>;
   if (dataSource == null) return;

   var dataList = dataSource.ToList();
   repo.Save(dataList);
}

public void Open<T>(List<T> dataList)
{
   gvCtrl.Columns.Clear();
   gvCtrl.OptionsBehavior.Editable = true;

   foreach (var prop in typeof(T).GetProperties())
   {
	   var column = new GridColumn
	   {
		   FieldName = prop.Name,
		   Caption = prop.Name,
		   Visible = true
	   };
	   gvCtrl.Columns.Add(column);
   }

   this.DataSource = new BindingList<T>(dataList);
}
#endregion
```




insert into wrkfld
      (FrwId, FrmId, CtrlNm, WrkId, FldNm,
       CtrlCls, FldTy, FldTitleWidth, FldTitle, TitleAlign)
select '2024FRW001', 'FormIni', 'grdIni.Section', 'grdIni', 'Section', 'Column', 'Text', 80, 'Section', 'Default' union all
select '2024FRW001', 'FormIni', 'grdIni.Key', 'grdIni', 'Key', 'Column', 'Text', 80, 'Key', 'Default' union all
select '2024FRW001', 'FormIni', 'grdIni.Value', 'grdIni', 'Value', 'Column', 'Text', 80, 'Value', 'Default'


select * from wrkfld order by id

insert into wrkfld
      (FrwId, FrmId, CtrlNm, WrkId, FldNm,
       CtrlCls, FldTy, FldTitleWidth, FldTitle, TitleAlign, ShowYn, EditYn)
select FrwId='2024FRW001', FrmId='FormIni', CtrlNm=concat('grdIni.',FldNm), WrkId='grdIni', FldNm,
       CtrlCls='Column', FldTy, FldTitleWidth=0, FldTitle, TitleAlign='Deflaut', ShowYn=0, EditYn=0
  from wrkfld
 where id in (100137,100136,100139,100138,100170)



update a
   set ToolNm = 'Repo'
  from wrkfld a
 where id in (100331,100332,100333,100334,100335,100336,100337,100338)

select *
  from wrkfld a
 where id in (100331,100332,100333,100334,100335,100336,100337,100338)

#### Integration


###### REFERENCE

###### 확인사항

###### Load
- [ ] `GRID 초기화
- [ ] `Set 기본정보
- [ ] `데이터베이스에서 컬럼 정보 읽어와서 셋팅하기
###### Open
- [ ] `현재 그리드를 Open할때 필요한 파라미터 정보
- [ ] `현재 그리드와 Open의 대상이 되는 Form
- [ ] `Open<T>(frm, wkset, searchObject)`
###### 그리드 속성 값 불러오기
###### 그리드 데이터 불러오기
###### Save
- [ ] `CU 문서 처리
- [ ] `D 문서 처리
###### Delete
- [ ] `D 등록(플레그)
###### New
- [ ] `신규 플레그 처리


##### Controller Class 
###### Properties
- [ ] `RowCount
- [ ] `Title
- [ ] `Title Width
- [ ] `Title Height
- [ ] `RowAutoHeigh
- [ ] `ColumnAutoWidth
- [ ] `ShowGroupPanel
- [ ] `ShowFindPanel
- [ ] `MultiSelect
- [ ] `GridMultiSelectMode
- [ ] `Sub Button Controller
- [ ] `SelectedRowIndex
- [ ] `Collection Properties
- [ ] `GetDocument<T>
- [ ] `GetSelectedRows[]
###### Event
- [ ] `BeforeLeaveRow
- [ ] `SelectionChanged
- [x] `InitNewRow
	- 신규추가시 DefaultText Data 셋팅하기
- [ ] `RowDeleting
- [x] `FocusedRowChanged
- [ ] `CellValueChanged
- [ ] `CellValueChanging
###### Method
- [ ] `GetText
- [ ] `GetColumnName
- [ ] `GetColumnIndex
- [ ] `SetText
- [ ] `AddNewDocument
- [ ] `SetGridData
- [ ] `Clear
- [ ] `FindRow

##### Basic Function For Grid - DevExpress
Pivot		
Excel		
Grouping		
Sorting		
Cell Formula		
	Formula	
	Format	
	Mask	
	Summary Type	
	Call PopUp	
	Merge	
	Title, Contents	
		align
조건별 색상		
다국어, 유니코드		
복사		
붙여넣기		
싱글타이틀		
검색타이틀		



```C#
private Point _mouseDownLocation;
private bool _isDragging = false;
private int _draggedRowHandle;

private void sourceGrid_MouseDown(object? sender, MouseEventArgs e)
{
    _mouseDownLocation = e.Location;
    _isDragging = false;

    if (g10.gvCtrl != null)
    {
        int rowHandle = g10.gvCtrl.CalcHitInfo(e.Location).RowHandle;
        _draggedRowHandle = rowHandle;
        //Common.gMsg = $"MouseDown: {e.Location}, RowHandle: {rowHandle}";
    }
}

private void sourceGrid_MouseMove(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        if (!_isDragging && (Math.Abs(e.X - _mouseDownLocation.X) > SystemInformation.DragSize.Width ||
                             Math.Abs(e.Y - _mouseDownLocation.Y) > SystemInformation.DragSize.Height))
        {
            _isDragging = true;
            //Common.gMsg = "MouseMove Start";

            if (_draggedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                //Common.gMsg = "Starting drag with the focused row.";
                this.DoDragDrop(_draggedRowHandle, DragDropEffects.Copy);
            }
        }
    }
}

private void targetGrid_DragEnter(object sender, DragEventArgs e)
{
    if (e.Data.GetDataPresent(typeof(int)))
    {
        e.Effect = DragDropEffects.Copy;
        Lib.Common.gMsg = "DragEnter: Data is present.";
    }
    else
    {
        e.Effect = DragDropEffects.None;
        Lib.Common.gMsg = "DragEnter: Data is not present.";
    }
}

private void targetGrid_DragDrop(object sender, DragEventArgs e)
{
   
    if (g20.gvCtrl != null)
    {
        int sourceRowHandle = (int)e.Data.GetData(typeof(int));
        if (sourceRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        {
            Lib.Common.gMsg = $"drop start";
            Point pt = g20.PointToClient(new Point(e.X, e.Y));
            GridHitInfo hitInfo = g20.gvCtrl.CalcHitInfo(pt);
            if (hitInfo.InRow)
            {
                int targetRowHandle = hitInfo.RowHandle;

                Lib.Common.gMsg = $"targetRowHandle : {targetRowHandle}.";

                if (targetRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    g20.SetText("PID", targetRowHandle, g10.GetText("ID", sourceRowHandle));
                    g20.SetText("Name", targetRowHandle, g10.GetText("Name", sourceRowHandle));
                }
            }
        }
    }
}
```



![[Pasted image 20240714112153.png]]
```SQL
INSERT INTO ColumnProperties (ColumnName, Band01, Band02, Band03, Band04, Band05, Band06, Band07, Band08, Band09, Band10, ColumnTitle, ColumnWidth, ShowYn, EditYn, TitleAlign, TextAlign, FormatStr, FldTy, Popup) VALUES ('bandedGridColumn1', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand1111', 'gridBand11111', 'gridBand111111', NULL, NULL, NULL, NULL, 'Column 1', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn2', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand1111', 'gridBand11111', 'gridBand111112', NULL, NULL, NULL, NULL, 'Column 2', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn3', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand1111', 'gridBand11112', NULL, NULL, NULL, NULL, NULL, 'Column 3', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn4', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand1112', NULL, NULL, NULL, NULL, NULL, NULL, 'Column 4', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn5', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand1112', NULL, NULL, NULL, NULL, NULL, NULL, 'Column 5', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn6', 'gridBand1', 'gridBand11', 'gridBand111', 'gridBand112', NULL, NULL, NULL, NULL, NULL, NULL, 'Column 6', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn7', 'gridBand1', 'gridBand11', 'gridBand112', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Column 7', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn8', 'gridBand1', 'gridBand12', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Column 8', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn9', 'gridBand1', 'gridBand12', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Column 9', 100, 1, 1, 'Center', 'Left', '', 'String', ''), ('bandedGridColumn10', 'gridBand1', 'gridBand12', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Column 10', 100, 1, 1, 'Center', 'Left', '', 'String', '');
```







그리드 기본형 및 밴드형
```C#

private GridControl grdFrmWrk;
private DevExpress.XtraGrid.Views.Grid.GridView gvFrmWrk;

// 
// grdFrmWrk
// 
grdFrmWrk.Dock = DockStyle.Fill;
grdFrmWrk.Location = new Point(0, 0);
grdFrmWrk.MainView = gvFrmWrk;
grdFrmWrk.Name = "grdFrmWrk";
grdFrmWrk.Size = new Size(1020, 486);
grdFrmWrk.TabIndex = 1;
grdFrmWrk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvFrmWrk });
// 
// gvFrmWrk
// 
gvFrmWrk.GridControl = grdFrmWrk;
gvFrmWrk.Name = "gvFrmWrk";

private GridControl grdFrmCtrl;
private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvFrmCtrl;

// 
// grdFrmCtrl
// 
grdFrmCtrl.Dock = DockStyle.Fill;
grdFrmCtrl.Location = new Point(0, 0);
grdFrmCtrl.MainView = gvFrmCtrl;
grdFrmCtrl.Name = "grdFrmCtrl";
grdFrmCtrl.Size = new Size(1020, 486);
grdFrmCtrl.TabIndex = 1;
grdFrmCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvFrmCtrl });
// 
// gvFrmCtrl
// 
gvFrmCtrl.GridControl = grdFrmCtrl;
gvFrmCtrl.Name = "gvFrmCtrl";

gvFrmCtrl.OptionsView.ShowColumnHeaders = false;
```