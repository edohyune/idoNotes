#### WRKFLD - FieldSet 셋팅
```C#
using DevExpress.Office.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.Services;
using Lib;
using Lib.Repo;
using Lib.Syntax;
using System.ComponentModel;
using System.Data;

namespace Frms
{
    public partial class WRKFLD : FrmBase
    {
        public WRKFLD()
        {
            InitializeComponent();
        }
        private void cmbFrm_UCSelectedIndexChanged(object sender, EventArgs e)
        {
            this.Open();
        }

        #region OPEN --------------------------------------------------------------->>
        #endregion

        #region CustomButtonClick -------------------------------------------------->>
        private void pnlWorkSet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Open")
            {
                return;
            }
        }
        private void pnlSelect_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Generate")
            {
                return;
            }
        }
        private string GetFieldType(Type dataType)
        {
            return dataType.Name switch
            {
                "Int32" => "Int",
                "String" => "Text",
                "DateTime" => "DateTime",
                "Date" => "Date",
                "Decimal" => "Decimal",
                "Double" => "Decimal",
                "Boolean" => "bool",
                _ => "Text",
            };
        }
        private void pnlInsert_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
        }
        private void pnlUpdate_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Make Reference Data")
            {
                return;
            }
        }
        private void pnlDelete_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Generate")
            {
                return;
            }
        }
        private void pnlModel_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Generate")
            {
                return;
            }
        }
        private void pnlColumn_CustomButtonClick(object Sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                return;
                //foreach (var wrkFld in wrkFldbs)
                //{
                //    if (wrkFld.ChangedFlag == MdlState.Inserted)
                //    {
                //        wrkFldRepo.Add(wrkFld);
                //    }
                //    else if (wrkFld.ChangedFlag == MdlState.Updated)
                //    {
                //        wrkFldRepo.Update(wrkFld);
                //    }
                //    else if (wrkFld.ChangedFlag == MdlState.None)
                //    {
                //        continue;
                //    }
                //}
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                return;
                //GridView view = t10.MainView as GridView;
                //if (view != null)
                //{
                //    var selectedRows = view.GetFocusedRow() as WrkFld;
                //    Common.gMsg = selectedRows.Id.ToString();

                //    if (selectedRows != null)
                //    {
                //        wrkFldbs.Remove(selectedRows);
                //        wrkFldRepo.Delete(selectedRows);
                //    }
                //}
            }
            else if (e.Button.Properties.Caption == "Numbering")
            {
                return;
                //int i = 1;
                //foreach (var wrkFld in wrkFldbs)
                //{
                //    wrkFld.Seq = i * 10;
                //    i++;
                //}
            }
            else if (e.Button.Properties.Caption == "Make Columns")
            {
                return;
                //if (string.IsNullOrWhiteSpace(GenFunc.GetSql(new { FrwId = selectedDoc.FrwId, FrmId = selectedDoc.FrmId, WrkId = selectedDoc.WrkId, CRUDM = "R" })))
                //{
                //    MessageBox.Show("Select 쿼리를 먼저 입력하세요.");
                //    return;
                //}

                //using (var db = new GaiaHelper())
                //{
                //    DataSet dSet = db.GetGridColumns(new { FrwId = selectedDoc.FrwId, FrmId = selectedDoc.FrmId, WrkId = selectedDoc.WrkId, CRUDM = "R" });
                //    if (dSet != null)
                //    {
                //        foreach (DataColumn cols in dSet.Tables[0].Columns)
                //        {
                //            var wrkFld = wrkFldbs.Where(x => x.CtrlNm == $"{selectedDoc.CtrlNm}.{cols.ColumnName}").FirstOrDefault();

                //            if (wrkFld != null)
                //            {
                //                wrkFld.FrwId = selectedDoc.FrwId;
                //                wrkFld.FrmId = selectedDoc.FrmId;
                //                wrkFld.WrkId = selectedDoc.WrkId;
                //                wrkFld.CtrlCls = "Column";
                //                wrkFld.CtrlNm = $"{selectedDoc.CtrlNm}.{cols.ColumnName}";
                //                wrkFld.FldNm = cols.ColumnName;
                //                wrkFld.FldTy = GetFieldType(cols.DataType);
                //                //wrkFld.FldTitle = cols.ColumnName;
                //                wrkFld.ChangedFlag = MdlState.Updated;
                //            }
                //            else
                //            {
                //                wrkFldbs.Add(new WrkFld
                //                {
                //                    FrwId = selectedDoc.FrwId,
                //                    FrmId = selectedDoc.FrmId,
                //                    WrkId = selectedDoc.WrkId,
                //                    CtrlCls = "Column",
                //                    CtrlNm = $"{selectedDoc.CtrlNm}.{cols.ColumnName}",
                //                    FldNm = cols.ColumnName,
                //                    FldTy = GetFieldType(cols.DataType),
                //                    FldTitle = cols.ColumnName,
                //                    ShowYn = true,
                //                    EditYn = true,
                //                    ChangedFlag = MdlState.Inserted
                //                });
                //            }
                //        }
                //        t10.DataSource = wrkFldbs;
                //    }
                //}
            }
        }
        private void pnlGet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Make GetParameters Data")
            {
                return;
                //SyntaxExtractor extractor = new SyntaxExtractor();
                //SyntaxMatch cvariables = extractor.ExtractVariables(rtSelect.Text);

                //foreach (var kvp in cvariables.OPatternMatch)
                //{
                //    //wrkGets에 있으면 update 없으면 insert
                //    var wrkGet = wrkGetbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                //    if (wrkGet == null)
                //    {
                //        wrkGet = wrkGetbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                //        if (wrkGet != null)
                //        {
                //            wrkGet.FldNm = kvp.Key;
                //            wrkGet.ChangedFlag = MdlState.Updated;
                //        }
                //        else
                //        {
                //            wrkGetbs.Add(new WrkGet
                //            {
                //                FrwId = selectedDoc.FrwId,
                //                FrmId = selectedDoc.FrmId,
                //                WrkId = selectedDoc.WrkId,
                //                FldNm = kvp.Key,
                //                ChangedFlag = MdlState.Inserted
                //            });
                //        }
                //    }
                //}
            }
        }
        private void pnlSet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Import Target List")
            {
                return;
                //var addSetbs = new WrkSetRepo().GetTargetList(selectedDoc.FrwId, selectedDoc.FrmId);
                //foreach (var wrkSet in addSetbs)
                //{
                //    wrkSetbs.Add(new WrkSet
                //    {
                //        FrwId = wrkSet.FrwId,
                //        FrmId = wrkSet.FrmId,
                //        SetWrkId = wrkSet.SetWrkId,
                //        SetFldNm = wrkSet.SetFldNm,
                //        ChangedFlag = MdlState.Inserted
                //    });
                //}
            }
        }
        private void pnlRef_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Make Reference Data")
            {
                return;
                //SyntaxExtractor extractor = new SyntaxExtractor();
                //SyntaxMatch cvariables = extractor.ExtractVariables(rtUpdate.Text);

                //foreach (var kvp in cvariables.OPatternMatch)
                //{
                //    //wrkRefs에 있으면 update 없으면 insert
                //    var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                //    if (wrkRef == null)
                //    {
                //        wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                //        if (wrkRef != null)
                //        {
                //            wrkRef.FldNm = kvp.Key;
                //            wrkRef.ChangedFlag = MdlState.Updated;
                //        }
                //        else
                //        {
                //            wrkRefbs.Add(new WrkRef
                //            {
                //                FrwId = selectedDoc.FrwId,
                //                FrmId = selectedDoc.FrmId,
                //                WrkId = selectedDoc.WrkId,
                //                FldNm = kvp.Key,
                //                ChangedFlag = MdlState.Inserted
                //            });
                //        }
                //    }
                //}

                //cvariables = extractor.ExtractVariables(rtInsert.Text);

                //foreach (var kvp in cvariables.OPatternMatch)
                //{
                //    var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                //    if (wrkRef == null)
                //    {
                //        wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                //        if (wrkRef != null)
                //        {
                //            wrkRef.FldNm = kvp.Key;
                //            wrkRef.ChangedFlag = MdlState.Updated;
                //        }
                //        else
                //        {
                //            wrkRefbs.Add(new WrkRef
                //            {
                //                FrwId = selectedDoc.FrwId,
                //                FrmId = selectedDoc.FrmId,
                //                WrkId = selectedDoc.WrkId,
                //                FldNm = kvp.Key,
                //                ChangedFlag = MdlState.Inserted
                //            });
                //        }
                //    }
                //}
            }
        }
        #endregion
        #region Grid to Grid Drag and Drop ------------------------------------------>>

        private Point _mouseDownLocation;
        private bool _isDragging = false;
        private int _draggedRowHandle;

        private void sourceGrid_MouseDown(object? sender, MouseEventArgs e)
        {
            _mouseDownLocation = e.Location;
            _isDragging = false;

            if (grdFrmCtrl.gvCtrl != null)
            {
                int rowHandle = grdFrmCtrl.gvCtrl.CalcHitInfo(e.Location).RowHandle;
                _draggedRowHandle = rowHandle;
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
                    if (_draggedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    {
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
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void targetGrid_DragDrop(object sender, DragEventArgs e)
        {

            if (grdWrkFld.gvCtrl != null)
            {
                int sourceRowHandle = (int)e.Data.GetData(typeof(int));
                if (sourceRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    Point pt = grdWrkFld.PointToClient(new Point(e.X, e.Y));
                    GridHitInfo hitInfo = grdWrkFld.gvCtrl.CalcHitInfo(pt);
                    if (hitInfo.InRow)
                    {
                        int targetRowHandle = hitInfo.RowHandle;

                        if (targetRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        {
                            grdWrkFld.SetText("PID", targetRowHandle, grdFrmCtrl.GetText("ID", sourceRowHandle));
                            grdWrkFld.SetText("Name", targetRowHandle, grdFrmCtrl.GetText("Name", sourceRowHandle));
                        }
                    }
                }
            }
        }
        #endregion

        private void ucPanel1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Expanding")
            {
                ucSplit2.SplitterDistance = ucSplit2.Parent.Width;
                //ExpandYn = true;
                e.Button.Properties.Caption = "Collapsing"; // 캡션 변경
            }
            else if (e.Button.Properties.Caption == "Collapsing")
            {
                ucSplit2.SplitterDistance = 250;
                //ExpandYn = false;
                e.Button.Properties.Caption = "Expanding"; // 캡션 변경
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                return;
            }
            else if (e.Button.Properties.Caption == "Open")
            {
                return;
            }
        }
    }
}
namespace Frms
{
    public class WRKFLD_GRDFRMCTRL : Lib.MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _ToolNm;
        public string ToolNm
        {
            get => _ToolNm;
            set => Set(ref _ToolNm, value);
        }

        private int _CtrlW;
        public int CtrlW
        {
            get => _CtrlW;
            set => Set(ref _CtrlW, value);
        }

        private int _CtrlH;
        public int CtrlH
        {
            get => _CtrlH;
            set => Set(ref _CtrlH, value);
        }

        private int _CtrlX;
        public int CtrlX
        {
            get => _CtrlX;
            set => Set(ref _CtrlX, value);
        }

        private int _CtrlY;
        public int CtrlY
        {
            get => _CtrlY;
            set => Set(ref _CtrlY, value);
        }

        private string _TitleText;
        public string TitleText
        {
            get => _TitleText;
            set => Set(ref _TitleText, value);
        }

        private int _TitleWidth;
        public int TitleWidth
        {
            get => _TitleWidth;
            set => Set(ref _TitleWidth, value);
        }

        private string _TitleAlign;
        public string TitleAlign
        {
            get => _TitleAlign;
            set => Set(ref _TitleAlign, value);
        }

        private string _DefaultText;
        public string DefaultText
        {
            get => _DefaultText;
            set => Set(ref _DefaultText, value);
        }

        private string _TextAlign;
        public string TextAlign
        {
            get => _TextAlign;
            set => Set(ref _TextAlign, value);
        }

        private bool _ShowYn;
        public bool ShowYn
        {
            get => _ShowYn;
            set => Set(ref _ShowYn, value);
        }

        private bool _EditYn;
        public bool EditYn
        {
            get => _EditYn;
            set => Set(ref _EditYn, value);
        }
    }

    public class WRKFLD_GRDFRMWRK : Lib.MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _WrkNm;
        public string WrkNm
        {
            get => _WrkNm;
            set => Set(ref _WrkNm, value);
        }

        private string _WrkCd;
        public string WrkCd
        {
            get => _WrkCd;
            set => Set(ref _WrkCd, value);
        }

        private string _SelectMode;
        public string SelectMode
        {
            get => _SelectMode;
            set => Set(ref _SelectMode, value);
        }

        private bool _MultiSelect;
        public bool MultiSelect
        {
            get => _MultiSelect;
            set => Set(ref _MultiSelect, value);
        }

        private bool _UseYn;
        public bool UseYn
        {
            get => _UseYn;
            set => Set(ref _UseYn, value);
        }

        private bool _NavAdd;
        public bool NavAdd
        {
            get => _NavAdd;
            set => Set(ref _NavAdd, value);
        }

        private bool _NavDelete;
        public bool NavDelete
        {
            get => _NavDelete;
            set => Set(ref _NavDelete, value);
        }

        private bool _NavSave;
        public bool NavSave
        {
            get => _NavSave;
            set => Set(ref _NavSave, value);
        }

        private bool _NavCancel;
        public bool NavCancel
        {
            get => _NavCancel;
            set => Set(ref _NavCancel, value);
        }

        private int _SaveSq;
        public int SaveSq
        {
            get => _SaveSq;
            set => Set(ref _SaveSq, value);
        }

        private int _OpenSq;
        public int OpenSq
        {
            get => _OpenSq;
            set => Set(ref _OpenSq, value);
        }

        private string _OpenTrg;
        public string OpenTrg
        {
            get => _OpenTrg;
            set => Set(ref _OpenTrg, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

    }
}
```

#### WRKGRD - GridSet 셋팅
```C#
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit;
using Lib;
using DevExpress.Office.Utils;
using Lib.Repo;
using System.Runtime.Intrinsics.Arm;
using System.Data;
using Frms.Models.WrkRepo;
using DevExpress.XtraTreeList.Printing;
using DevExpress.Data.Filtering.Helpers;
using System.ComponentModel;
using DevExpress.Pdf.Native;
using DevExpress.XtraGrid.Views.Grid;
using Lib.Syntax;
using DevExpress.PivotGrid.Criteria;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Frms
{
    public partial class WRKGRD : UserControl
    {
        private DevExpress.XtraRichEdit.RichEditControl rtSelect;
        private DevExpress.XtraRichEdit.RichEditControl rtInsert;
        private DevExpress.XtraRichEdit.RichEditControl rtUpdate;
        private DevExpress.XtraRichEdit.RichEditControl rtDelete;
        private DevExpress.XtraRichEdit.RichEditControl rtModel;

        public FrwFrm frwFrm { get; set; }
        private FrmCtrlRepo frmCtrlRepo { get; set; }
        public BindingList<FrmCtrl> frmCtrls { get; set; }
        private FrmWrk selectedDoc { get; set; }
        private FrmWrkRepo frmWrkRepo { get; set; }
        private BindingList<FrmWrk> frmWrks { get; set; }
        public WrkFld wrkFld { get; set; }
        private WrkFldRepo wrkFldRepo { get; set; }
        private BindingList<WrkFld> wrkFldbs { get; set; }
        private WrkGetRepo wrkGetRepo { get; set; }
        private BindingList<WrkGet> wrkGetbs { get; set; }
        private WrkSetRepo wrkSetRepo { get; set; }
        private BindingList<WrkSet> wrkSetbs { get; set; }
        private WrkRefRepo wrkRefRepo { get; set; }
        private BindingList<WrkRef> wrkRefbs { get; set; }
        private WrkSqlRepo wrkSqlRepo { get; set; }
        public WrkSql wrkSql { get; set; }


        public WRKGRD()
        {
            InitializeComponent();

            List<FrwFrm> frwFrms = new FrwFrmRepo().GetAll();
            foreach (var frwFrm in frwFrms)
            {
                cmbForm.Properties.Items.Add(frwFrm);
            }

            InitializeRichTextEditor();
            //tabQueryField.SelectedTabPageIndex = 1;
            //tabQueryField.SelectedTabPageIndex = 0;
            //tabCRUDM.SelectedTabPageIndex = 4;
            //tabCRUDM.SelectedTabPageIndex = 3;
            //tabCRUDM.SelectedTabPageIndex = 2;
            //tabCRUDM.SelectedTabPageIndex = 1;
            //tabCRUDM.SelectedTabPageIndex = 0;
            //tabWrk.SelectedTabPageIndex = 1;
            //tabWrk.SelectedTabPageIndex = 0;
            //tabParam.SelectedTabPageIndex = 2;
            //tabParam.SelectedTabPageIndex = 1;
            //tabParam.SelectedTabPageIndex = 0;
        }

        private void InitializeRichTextEditor()
        {
            rtSelect = new DevExpress.XtraRichEdit.RichEditControl();
            rtInsert = new DevExpress.XtraRichEdit.RichEditControl();
            rtUpdate = new DevExpress.XtraRichEdit.RichEditControl();
            rtDelete = new DevExpress.XtraRichEdit.RichEditControl();
            rtModel = new DevExpress.XtraRichEdit.RichEditControl();

            rtSelect.Dock = DockStyle.Fill;
            rtSelect.Name = "rtSelect";
            rtSelect.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            rtSelect.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            rtSelect.Modified = true;
            rtSelect.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            rtSelect.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            rtSelect.ReplaceService<ISyntaxHighlightService>(new Lib.Syntax.SQL_Syntax(rtSelect.Document));
            rtSelect.Options.Search.RegExResultMaxGuaranteedLength = 500;
            rtSelect.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            rtInsert.Dock = DockStyle.Fill;
            rtInsert.Name = "rtInsert";
            rtInsert.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            rtInsert.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            rtInsert.Modified = true;
            rtInsert.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            rtInsert.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            rtInsert.ReplaceService<ISyntaxHighlightService>(new Lib.Syntax.SQL_Syntax(rtInsert.Document));
            rtInsert.Options.Search.RegExResultMaxGuaranteedLength = 500;
            rtInsert.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            rtUpdate.Dock = DockStyle.Fill;
            rtUpdate.Name = "rtUpdate";
            rtUpdate.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            rtUpdate.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            rtUpdate.Modified = true;
            rtUpdate.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            rtUpdate.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            rtUpdate.ReplaceService<ISyntaxHighlightService>(new Lib.Syntax.SQL_Syntax(rtUpdate.Document));
            rtUpdate.Options.Search.RegExResultMaxGuaranteedLength = 500;
            rtUpdate.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            rtDelete.Dock = DockStyle.Fill;
            rtDelete.Name = "rtDelete";
            rtDelete.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            rtDelete.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            rtDelete.Modified = true;
            rtDelete.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            rtDelete.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            rtDelete.ReplaceService<ISyntaxHighlightService>(new Lib.Syntax.SQL_Syntax(rtDelete.Document));
            rtDelete.Options.Search.RegExResultMaxGuaranteedLength = 500;
            rtDelete.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            rtModel.Dock = DockStyle.Fill;
            rtModel.Name = "rtModel";
            rtModel.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            rtModel.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            rtModel.Modified = true;
            rtModel.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            rtModel.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            rtModel.ReplaceService<ISyntaxHighlightService>(new Lib.Syntax.CS_Syntax(rtModel.Document));
            rtModel.Options.Search.RegExResultMaxGuaranteedLength = 500;
            rtModel.Document.Sections[0].Page.Width = Units.InchesToDocumentsF(300f);

            pnlSelect.Controls.Add(rtSelect);
            pnlInsert.Controls.Add(rtInsert);
            pnlUpdate.Controls.Add(rtUpdate);
            pnlDelete.Controls.Add(rtDelete);
            pnlModel.Controls.Add(rtModel);
        }
        private void cmbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            frwFrm = cmbForm.SelectedItem as FrwFrm;

            frmWrkRepo = new FrmWrkRepo();
            frmWrks = new BindingList<FrmWrk>(frmWrkRepo.GetByWorkSetsOpenOrderby(frwFrm.FrwId, frwFrm.FrmId));

            if (frwFrm != null)
            {
                g10OpenGrid();
                g10_UCFocusedRowChanged(g10, 0, 0, null);
            }
        }
        private void g10_UCFocusedRowChanged(object sender, int preIndex, int rowIndex, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            selectedDoc = view.GetFocusedRow() as FrmWrk;

            frmCtrlRepo = new FrmCtrlRepo();
            frmCtrls = new BindingList<FrmCtrl>(frmCtrlRepo.GetByFrwFrm(selectedDoc.FrwId, selectedDoc.FrmId));

            wrkFldRepo = new WrkFldRepo();
            wrkFldbs = new BindingList<WrkFld>(wrkFldRepo.GetColumnProperties(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId));

            wrkGetRepo = new WrkGetRepo();
            wrkGetbs = new BindingList<WrkGet>(wrkGetRepo.GetPullFlds(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId));

            wrkSetRepo = new WrkSetRepo();
            wrkSetbs = new BindingList<WrkSet>(wrkSetRepo.SetPushFlds(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId));

            wrkRefRepo = new WrkRefRepo();
            wrkRefbs = new BindingList<WrkRef>(wrkRefRepo.RefDataFlds(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId));

            if (selectedDoc != null)
            {
                g20OpenGrid();
                SetWrkSQL();
                t10OpenGrid();
                grdGetParamGrid();
                grdSetparamGrid();
                grdRefDataGrid();
            }
        }

        #region OPEN ------------------------------------------------------>>
        private void g10OpenGrid()
        {
            Common.gMsg = "g10OpenGrid";
            //g10.Clear();
            g10.DataSource = frmWrks;
        }
        private void g20OpenGrid()
        {
            Common.gMsg = "g20OpenGrid";
            //g20.Clear();
            tabWrk.SelectedTabPageIndex = 1;
            g20.DataSource = frmCtrls;
            tabWrk.SelectedTabPageIndex = 0;
        }
        private void t10OpenGrid()
        {
            Common.gMsg = "t10OpenGrid";
            //t10.Clear();
            t10.DataSource = wrkFldbs;
        }
        private void grdGetParamGrid()
        {
            grdGetParam.DataSource = wrkGetbs;
        }
        private void grdSetparamGrid()
        {
            grdSetParam.DataSource = wrkSetbs;
        }
        private void grdRefDataGrid()
        {
            grdRefData.DataSource = wrkRefbs;
        }
        private void SetWrkSQL()
        {
            rtDelete.Text = GenFunc.GetSql(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId, "D").Query;
            rtInsert.Text = GenFunc.GetSql(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId, "C").Query;
            rtSelect.Text = GenFunc.GetSql(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId, "R").Query;
            rtModel.Text = GenFunc.GetSql(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId, "M").Query;
            rtUpdate.Text = GenFunc.GetSql(selectedDoc.FrwId, selectedDoc.FrmId, selectedDoc.WrkId, "U").Query;
        }
        #endregion

        #region CustomButtonClick -------------------------------------------------->>
        private void pnlWorkSet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Open")
            {
                if (selectedDoc != null)
                {
                    g20OpenGrid();
                    SetWrkSQL();
                    t10OpenGrid();
                    grdGetParamGrid();
                    grdSetparamGrid();
                    grdRefDataGrid();
                }
            }
            else if (e.Button.Properties.Caption == "Expanding")
            {
                ucSplit2.SplitterDistance = ucSplit2.Parent.Width;
                //ExpandYn = true;
                e.Button.Properties.Caption = "Collapsing"; // 캡션 변경
            }
            else if (e.Button.Properties.Caption == "Collapsing")
            {
                ucSplit2.SplitterDistance = 250;
                //ExpandYn = false;
                e.Button.Properties.Caption = "Expanding"; // 캡션 변경
            }
        }
        private void pnlSelect_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                wrkSqlRepo.Delete(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "R",
                    Query = rtSelect.Text
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "R",
                    Query = rtSelect.Text
                });
            }
            else if (e.Button.Properties.Caption == "Generate")
            {

            }
        }
        private string GetFieldType(Type dataType)
        {
            return dataType.Name switch
            {
                "Int32" => "Int",
                "String" => "Text",
                "DateTime" => "DateTime",
                "Date" => "Date",
                "Decimal" => "Decimal",
                "Double" => "Decimal",
                "Boolean" => "bool",
                _ => "Text",
            };
        }
        private void pnlInsert_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                wrkSqlRepo.Delete(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "C",
                    Query = rtInsert.Text
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "C",
                    Query = rtInsert.Text
                });
            }
        }
        private void pnlUpdate_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                wrkSqlRepo.Delete(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "U",
                    Query = rtUpdate.Text
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "U",
                    Query = rtUpdate.Text
                });
            }
            else if (e.Button.Properties.Caption == "Make Reference Data")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "U",
                    Query = rtUpdate.Text
                });
            }
        }
        private void pnlDelete_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                wrkSqlRepo.Delete(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "D",
                    Query = rtDelete.Text
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "D",
                    Query = rtDelete.Text
                });
            }
            else if (e.Button.Properties.Caption == "Generate")
            {

            }
        }
        private void pnlModel_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var wrkSqlRepo = new WrkSqlRepo();
            if (e.Button.Properties.Caption == "Delete")
            {
                wrkSqlRepo.Delete(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "M",
                    Query = rtModel.Text
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                wrkSqlRepo.Save(new WrkSql
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    CRUDM = "M",
                    Query = rtModel.Text
                });
            }
            else if (e.Button.Properties.Caption == "Generate")
            {

            }
        }
        private void pnlColumn_CustomButtonClick(object Sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                foreach (var wrkFld in wrkFldbs)
                {
                    if (wrkFld.ChangedFlag == MdlState.Inserted)
                    {
                        wrkFldRepo.Add(wrkFld);
                    }
                    else if (wrkFld.ChangedFlag == MdlState.Updated)
                    {
                        wrkFldRepo.Update(wrkFld);
                    }
                    else if (wrkFld.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                GridView view = t10.MainView as GridView;
                if (view != null)
                {
                    var selectedRows = view.GetFocusedRow() as WrkFld;
                    Common.gMsg = selectedRows.Id.ToString();

                    if (selectedRows != null)
                    {
                        wrkFldbs.Remove(selectedRows);
                        wrkFldRepo.Delete(selectedRows);
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Numbering")
            {
                int i = 1;
                foreach (var wrkFld in wrkFldbs)
                {
                    wrkFld.Seq = i * 10;
                    i++;
                }
            }
            else if (e.Button.Properties.Caption == "Make Columns")
            {
                if (string.IsNullOrWhiteSpace(GenFunc.GetSql(new { FrwId = selectedDoc.FrwId, FrmId = selectedDoc.FrmId, WrkId = selectedDoc.WrkId, CRUDM = "R" })))
                {
                    MessageBox.Show("Select 쿼리를 먼저 입력하세요.");
                    return;
                }

                using (var db = new GaiaHelper())
                {
                    DataSet dSet = db.GetGridColumns(new { FrwId = selectedDoc.FrwId, FrmId = selectedDoc.FrmId, WrkId = selectedDoc.WrkId, CRUDM = "R" });
                    if (dSet != null)
                    {
                        foreach (DataColumn cols in dSet.Tables[0].Columns)
                        {
                            var wrkFld = wrkFldbs.Where(x => x.CtrlNm == $"{selectedDoc.CtrlNm}.{cols.ColumnName}").FirstOrDefault();

                            if (wrkFld != null)
                            {
                                wrkFld.FrwId = selectedDoc.FrwId;
                                wrkFld.FrmId = selectedDoc.FrmId;
                                wrkFld.WrkId = selectedDoc.WrkId;
                                wrkFld.CtrlCls = "Column";
                                wrkFld.CtrlNm = $"{selectedDoc.CtrlNm}.{cols.ColumnName}";
                                wrkFld.FldNm = cols.ColumnName;
                                wrkFld.FldTy = GetFieldType(cols.DataType);
                                //wrkFld.FldTitle = cols.ColumnName;
                                wrkFld.ChangedFlag = MdlState.Updated;
                            }
                            else
                            {
                                wrkFldbs.Add(new WrkFld
                                {
                                    FrwId = selectedDoc.FrwId,
                                    FrmId = selectedDoc.FrmId,
                                    WrkId = selectedDoc.WrkId,
                                    CtrlCls = "Column",
                                    CtrlNm = $"{selectedDoc.CtrlNm}.{cols.ColumnName}",
                                    FldNm = cols.ColumnName,
                                    FldTy = GetFieldType(cols.DataType),
                                    FldTitle = cols.ColumnName,
                                    ShowYn = true,
                                    EditYn = true,
                                    ChangedFlag = MdlState.Inserted
                                });
                            }
                        }
                        t10.DataSource = wrkFldbs;
                    }
                }
            }
        }
        private void pnlGet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                wrkGetbs.Add(new WrkGet
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    ChangedFlag = MdlState.Inserted
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                foreach (var wrkGet in wrkGetbs)
                {
                    if (string.IsNullOrWhiteSpace(wrkGet.GetWrkId) && string.IsNullOrWhiteSpace(wrkGet.GetFldNm) && string.IsNullOrWhiteSpace(wrkGet.GetDefalueValue))
                    {
                        continue;
                    }
                    if (wrkGet.ChangedFlag == MdlState.Inserted)
                    {
                        wrkGetRepo.Add(wrkGet);
                    }
                    else if (wrkGet.ChangedFlag == MdlState.Updated)
                    {
                        wrkGetRepo.Update(wrkGet);
                    }
                    else if (wrkGet.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                GridView view = grdGetParam.MainView as GridView;
                if (view != null)
                {
                    var selectedRows = view.GetFocusedRow() as WrkGet;
                    if (selectedRows != null)
                    {
                        wrkGetbs.Remove(selectedRows);
                        wrkGetRepo.Delete(selectedRows);
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Make GetParameters Data")
            {
                SyntaxExtractor extractor = new SyntaxExtractor();
                SyntaxMatch cvariables = extractor.ExtractVariables(rtSelect.Text);

                foreach (var kvp in cvariables.OPatternMatch)
                {
                    //wrkGets에 있으면 update 없으면 insert
                    var wrkGet = wrkGetbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                    if (wrkGet == null)
                    {
                        wrkGet = wrkGetbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                        if (wrkGet != null)
                        {
                            wrkGet.FldNm = kvp.Key;
                            wrkGet.ChangedFlag = MdlState.Updated;
                        }
                        else
                        {
                            wrkGetbs.Add(new WrkGet
                            {
                                FrwId = selectedDoc.FrwId,
                                FrmId = selectedDoc.FrmId,
                                WrkId = selectedDoc.WrkId,
                                FldNm = kvp.Key,
                                ChangedFlag = MdlState.Inserted
                            });
                        }
                    }
                }
            }
        }
        private void pnlSet_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                wrkSetbs.Add(new WrkSet
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    ChangedFlag = MdlState.Inserted
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                foreach (var wrkSet in wrkSetbs)
                {
                    if (string.IsNullOrWhiteSpace(wrkSet.WrkId) && string.IsNullOrWhiteSpace(wrkSet.FldNm))
                    {
                        continue;
                    }
                    if (wrkSet.ChangedFlag == MdlState.Inserted)
                    {
                        wrkSetRepo.Add(wrkSet);
                    }
                    else if (wrkSet.ChangedFlag == MdlState.Updated)
                    {
                        wrkSetRepo.Update(wrkSet);
                    }
                    else if (wrkSet.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                GridView view = grdSetParam.MainView as GridView;
                if (view != null)
                {
                    var selectedRows = view.GetSelectedRows();
                    foreach (var rowHandle in selectedRows)
                    {
                        var selectedRow = view.GetRow(rowHandle) as WrkSet;
                        if (selectedRow != null)
                        {
                            wrkSetbs.Remove(selectedRow);
                            wrkSetRepo.Delete(selectedRow);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Import Target List")
            {
                var addSetbs = new WrkSetRepo().GetTargetList(selectedDoc.FrwId, selectedDoc.FrmId);
                foreach (var wrkSet in addSetbs)
                {
                    wrkSetbs.Add(new WrkSet
                    {
                        FrwId = wrkSet.FrwId,
                        FrmId = wrkSet.FrmId,
                        SetWrkId = wrkSet.SetWrkId,
                        SetFldNm = wrkSet.SetFldNm,
                        ChangedFlag = MdlState.Inserted
                    });
                }
            }
        }
        private void pnlRef_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                wrkRefbs.Add(new WrkRef
                {
                    FrwId = selectedDoc.FrwId,
                    FrmId = selectedDoc.FrmId,
                    WrkId = selectedDoc.WrkId,
                    FldNm = "RefFldNm",
                    ChangedFlag = MdlState.Inserted
                });
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                foreach (var wrkRef in wrkRefbs)
                {
                    //만일 wrkRef.RefWrkId, wrkRef.RefFldNm, wrkRef.RefDefalueValue가 공백이거나 null이면 저장에서 제외한다.
                    if (string.IsNullOrWhiteSpace(wrkRef.RefWrkId) && string.IsNullOrWhiteSpace(wrkRef.RefFldNm) && string.IsNullOrWhiteSpace(wrkRef.RefDefalueValue))
                    {
                        continue;
                    }
                    if (wrkRef.ChangedFlag == MdlState.Inserted)
                    {
                        wrkRefRepo.Add(wrkRef);
                    }
                    else if (wrkRef.ChangedFlag == MdlState.Updated)
                    {
                        wrkRefRepo.Update(wrkRef);
                    }
                    else if (wrkRef.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                GridView view = grdRefData.MainView as GridView;
                if (view != null)
                {
                    var selectedRows = view.GetSelectedRows();
                    foreach (var rowHandle in selectedRows)
                    {
                        var selectedRow = view.GetRow(rowHandle) as WrkRef;
                        if (selectedRow != null)
                        {
                            wrkRefbs.Remove(selectedRow);
                            wrkRefRepo.Delete(selectedRow);
                        }
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Make Reference Data")
            {
                SyntaxExtractor extractor = new SyntaxExtractor();
                SyntaxMatch cvariables = extractor.ExtractVariables(rtUpdate.Text);

                foreach (var kvp in cvariables.OPatternMatch)
                {
                    //wrkRefs에 있으면 update 없으면 insert
                    var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                    if (wrkRef == null)
                    {
                        wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                        if (wrkRef != null)
                        {
                            wrkRef.FldNm = kvp.Key;
                            wrkRef.ChangedFlag = MdlState.Updated;
                        }
                        else
                        {
                            wrkRefbs.Add(new WrkRef
                            {
                                FrwId = selectedDoc.FrwId,
                                FrmId = selectedDoc.FrmId,
                                WrkId = selectedDoc.WrkId,
                                FldNm = kvp.Key,
                                ChangedFlag = MdlState.Inserted
                            });
                        }
                    }
                }

                cvariables = extractor.ExtractVariables(rtInsert.Text);

                foreach (var kvp in cvariables.OPatternMatch)
                {
                    var wrkRef = wrkRefbs.Where(x => x.FldNm == kvp.Key).FirstOrDefault();
                    if (wrkRef == null)
                    {
                        wrkRef = wrkRefbs.Where(x => x.FldNm.ToLower() == kvp.Key.ToLower()).FirstOrDefault();
                        if (wrkRef != null)
                        {
                            wrkRef.FldNm = kvp.Key;
                            wrkRef.ChangedFlag = MdlState.Updated;
                        }
                        else
                        {
                            wrkRefbs.Add(new WrkRef
                            {
                                FrwId = selectedDoc.FrwId,
                                FrmId = selectedDoc.FrmId,
                                WrkId = selectedDoc.WrkId,
                                FldNm = kvp.Key,
                                ChangedFlag = MdlState.Inserted
                            });
                        }
                    }
                }
            }
        }
        #endregion
    }
}
namespace Frms.Models.WrkRepo
{
    public class wrkList
    {
        public string WrkId { get; set; }
        public string FrwId { get; set; }
        public string FrmId { get; set; }
    }
}

```

#### FRMLOD - Controller 셋팅
```C#
using DevExpress.ChartRangeControlClient.Core;
using DevExpress.Data.Helpers;
using DevExpress.Mvvm.POCO;
using DevExpress.RichEdit.Export;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTab;
using Lib;
using Lib.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static DevExpress.Accessibility.LookupPopupAccessibleObject;

namespace Frms
{
    [ToolboxItem(false)]
    public partial class FRMLOD : UserControl
    {
        private FrwFrm selectedFrwFrm { get; set; }
        private BindingList<FrwFrm> frmMstbs { get; set; }
        private FrwFrmRepo frmMstRepo { get; set; }


        private FrmCtrl frmCtrl { get; set; }
        private BindingList<FrmCtrl> frmCtrlbs { get; set; }
        private FrmCtrlRepo frmCtrlRepo { get; set; }


        private CtrlMst ctrlCls { get; set; }
        private List<CtrlMst> ctrlClss { get; set; }
        private BindingList<CtrlMst> ctrlClsbs { get; set; }
        private CtrlMstRepo ctrlClsRepo { get; set; }

        public FrmWrk frmWrk { get; set; }
        public List<FrmWrk> frmWrks { get; set; }
        public BindingList<FrmWrk> frmWrkbs { get; set; }
        public FrmWrkRepo frmWrkRepo { get; set; }

        public FRMLOD()
        {
            InitializeComponent();
            txtFilePath.ButtonVisiable = true;
            cmbStatus.DataSource = Enum.GetValues(typeof(MdlState));
        }

        private void gvForms_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            selectedFrwFrm = view.GetFocusedRow() as FrwFrm;

            if (selectedFrwFrm != null)
            {
                SetFrwFrmFreeForm();
            }

        }

        private void SetFrwFrmFreeForm()
        {
            AddBindingIfNotExists(txtFilePath, "BindText", selectedFrwFrm, "FilePath");
            AddBindingIfNotExists(txtFileNm, "BindText", selectedFrwFrm, "FileNm");
            AddBindingIfNotExists(txtFrmId, "BindText", selectedFrwFrm, "FrmId");
            AddBindingIfNotExists(txtFrmNm, "BindText", selectedFrwFrm, "FrmNm");
            AddBindingIfNotExists(txtUsrRegId, "BindText", selectedFrwFrm, "UsrRegId");
            AddBindingIfNotExists(txtFrwId, "BindText", selectedFrwFrm, "FrwId");
            AddBindingIfNotExists(txtNmSpace, "BindText", selectedFrwFrm, "NmSpace");
            AddBindingIfNotExists(chkFld, "EditValue", selectedFrwFrm, "FldYn");
            AddBindingIfNotExists(cmbStatus, "SelectedItem", selectedFrwFrm, "ChangedFlag");

        }
        private System.Windows.Forms.BindingSource bindingSource = new System.Windows.Forms.BindingSource();
        private void AddBindingIfNotExists(Control control, string propertyName, object dataSource, string dataMember)
        {
            bindingSource.DataSource = dataSource;
            control.DataBindings.Clear();
            control.DataBindings.Add(propertyName, bindingSource, dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void txtFrmId_EditValueChanged(object Sender, Control control)
        {
            OpengridControls();
        }

        private void OpengridControls()
        {
            if (selectedFrwFrm != null)
            {
                frmCtrl = new FrmCtrl();
                frmCtrlRepo = new FrmCtrlRepo();
                frmCtrlbs = new BindingList<FrmCtrl>(frmCtrlRepo.GetByFrwFrm(selectedFrwFrm.FrwId, selectedFrwFrm.FrmId));
                gridControls.DataSource = frmCtrlbs;

                frmWrk = new FrmWrk();
                frmWrkRepo = new FrmWrkRepo();
                frmWrkbs = new BindingList<FrmWrk>(frmWrkRepo.GetByWorkSetsOpenOrderby(selectedFrwFrm.FrwId, selectedFrwFrm.FrmId));
                gridWorkset.DataSource = frmWrkbs;
            }
        }

        private void ucPenel2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Open")
            {
                OpengridFrms();
            }

        }
        private void OpengridFrms()
        {
            frmMstRepo = new FrwFrmRepo();
            frmMstbs = new BindingList<FrwFrm>(frmMstRepo.GetAll());
            gcForms.DataSource = frmMstbs;
        }

        private void ucPanel3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "New")
            {
                selectedFrwFrm = new FrwFrm();
                selectedFrwFrm.ChangedFlag = MdlState.Inserted;
                SetFrwFrmFreeForm();
            }
            else if (e.Button.Properties.Caption == "Save")
            {
                if (selectedFrwFrm.ChangedFlag == MdlState.Inserted)
                {
                    frmMstRepo.Add(selectedFrwFrm);
                }
                else
                {
                    frmMstRepo.Update(selectedFrwFrm);
                }

            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                if (selectedFrwFrm.ChangedFlag == MdlState.Inserted)
                {
                    selectedFrwFrm = new FrwFrm();
                    cmbStatus.Text = MdlState.Inserted.ToString();
                    SetFrwFrmFreeForm();
                }
                else
                {
                    frmMstRepo.Delete(selectedFrwFrm.FrmId);
                }
            }
        }

        private void txtDllpath_UCButtonClick(object Sender, Control control)
        {
            if (selectedFrwFrm == null) return;

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "DLL Files|*.dll|EXE Files|*.exe";

            if (string.IsNullOrEmpty(selectedFrwFrm.FilePath))
            {
                openFileDialog1.InitialDirectory = @"C:\";
            }
            else
            {
                openFileDialog1.InitialDirectory = selectedFrwFrm.FilePath;
            }
            openFileDialog1.ShowDialog();

            selectedFrwFrm.FilePath = Path.GetDirectoryName(openFileDialog1.FileName);
            selectedFrwFrm.FileNm = openFileDialog1.SafeFileName;
            selectedFrwFrm.FrmId = selectedFrwFrm.FileNm.Substring(0, selectedFrwFrm.FileNm.Length - 4);
            selectedFrwFrm.UsrRegId = Common.GetValue("gRegId").ToInt();
            selectedFrwFrm.FrwId = Common.GetValue("gFrameWorkId");
            selectedFrwFrm.NmSpace = $"Frms.{selectedFrwFrm.FrmId}";
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (selectedFrwFrm == null) return;

            //frm.FilePath, frm.NmSpace의 값이 없으면 아무것도 하지 않는다.
            if (string.IsNullOrEmpty(selectedFrwFrm.FilePath) || string.IsNullOrEmpty(selectedFrwFrm.NmSpace))
            {
                return;
            }

            //UserControl정보를 넣는 변수 ucform을 선언한다.
            UserControl ucform = null;
            //추가 정보을 읽어 올 파일의 목록
            ctrlClsRepo = new CtrlMstRepo();
            ctrlClss = ctrlClsRepo.GetAll();
            //frmCtrlbs.Clear();

            Assembly assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes($"{selectedFrwFrm.FilePath}\\{selectedFrwFrm.FileNm}"));
            var ty = assembly.GetType(selectedFrwFrm.NmSpace);
            ucform = (UserControl)Activator.CreateInstance(ty);

            // 1. Non-Visual Components Tray Area 컨트롤 확인
            Common.gMsg = "1. Non-Visual Components Tray Area 컨트롤 확인";
            FieldInfo[] fields = ucform.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // BindingList, Repo, IContainer 타입 제외
                if (field.FieldType.FullName.Contains("BindingList") ||
                    field.FieldType.FullName.Contains("Repo") ||
                    field.FieldType.FullName.Contains("IContainer"))
                {
                    continue;
                }
                // UCField 또는 OpenFileDialog 타입인지 확인
                if (field.FieldType.FullName.Contains("Ctrls.UCField")) // 사용안함 field.FieldType.FullName.Contains("OpenFileDialog")
                {
                    // frmCtrls에 추가 (FrmCtrl 객체 생성 및 frmCtrlbs에 추가)
                    var ctrlNm = field.Name;
                    var toolNm = field.FieldType.Name;

                    // frmCtrlbs 에 데이터가 있으면 업데이트, 없으면 추가.
                    var frmCtrl = frmCtrlbs.FirstOrDefault(c => c.CtrlNm == ctrlNm);
                    if (frmCtrl != null)
                    {
                        frmCtrl.FrwId = Common.GetValue("gFrameWorkId");
                        frmCtrl.FrmId = selectedFrwFrm.FrmId;
                        frmCtrl.CtrlNm = ctrlNm;
                        frmCtrl.ToolNm = toolNm;
                        frmCtrl.ChangedFlag = MdlState.Updated;
                    }
                    else
                    {
                        frmCtrlbs.Add(new FrmCtrl
                        {
                            FrwId = Common.GetValue("gFrameWorkId"),
                            FrmId = selectedFrwFrm.FrmId,
                            CtrlNm = ctrlNm,
                            ToolNm = toolNm,
                            ChangedFlag = MdlState.Inserted
                            //사용하지 않는 데이터는 Null로 처리
                            //CtrlW = 0,CtrlH = 0,TitleText = "",TitleAlign = "",VisibleYn = false,ReadonlyYn = false
                        }); ;
                    }
                }
                else if (field.FieldType.FullName.Contains("UCGridSet"))
                {
                    var ctrlNm = field.Name;
                    var toolNm = field.FieldType.Name;
                    var frmCtrl = frmCtrlbs.FirstOrDefault(c => c.CtrlNm == ctrlNm);

                    if (frmCtrl != null)
                    {
                        frmCtrl.FrwId = Common.GetValue("gFrameWorkId");
                        frmCtrl.FrmId = selectedFrwFrm.FrmId;
                        frmCtrl.CtrlNm = ctrlNm;
                        frmCtrl.ToolNm = toolNm;
                        frmCtrl.ChangedFlag = MdlState.Updated;
                    }
                    else
                    {
                        frmCtrlbs.Add(new FrmCtrl
                        {
                            FrwId = Common.GetValue("gFrameWorkId"),
                            FrmId = selectedFrwFrm.FrmId,
                            CtrlNm = ctrlNm,
                            ToolNm = toolNm,
                            ChangedFlag = MdlState.Inserted
                        });
                    }
                }
            }

            // 2. 폼에 등록된 컨트롤 확인 (ucform.Controls)
            Common.gMsg = "2. 폼에 등록된 컨트롤 확인 (ucform.Controls)";
            FindUCControlsRecursive(ucform);

            // 3. WorkSet 등록 (UCField, UCGrid)
            Common.gMsg = "3. WorkSet 등록 (UCField, UCGrid, UCGridSet)";
            foreach (var item in frmCtrlbs)
            {
                if (item.ToolNm == "UCField")
                {
                    //frmWrks에 데이터가 있으면 업데이트, 없으면 추가.
                    var frmWrk = frmWrkbs.FirstOrDefault(c => c.CtrlNm == item.CtrlNm);
                    if (frmWrk != null)
                    {
                        frmWrk.FrwId = item.FrwId;
                        frmWrk.FrmId = item.FrmId;
                        frmWrk.CtrlNm = item.CtrlNm;
                        frmWrk.WrkCd = "FieldSet";
                        frmWrk.UseYn = true;
                        frmWrk.ChangedFlag = MdlState.Updated;
                    }
                    else
                    {
                        frmWrkbs.Add(new FrmWrk
                        {
                            FrwId = item.FrwId,
                            FrmId = item.FrmId,
                            CtrlNm = item.CtrlNm,
                            WrkCd = "FieldSet",
                            UseYn = true,
                            ChangedFlag = MdlState.Inserted
                        });
                    }
                }
                else if (item.ToolNm == "UCGrid" || item.ToolNm == "UCGridSet" || item.ToolNm == "UCGridNav")
                {   //frmWrks에 데이터가 있으면 업데이트, 없으면 추가.
                    var frmWrk = frmWrkbs.FirstOrDefault(c => c.CtrlNm == item.CtrlNm);
                    if (frmWrk != null)
                    {
                        frmWrk.FrwId = item.FrwId;
                        frmWrk.FrmId = item.FrmId;
                        frmWrk.CtrlNm = item.CtrlNm;
                        frmWrk.WrkCd = "GridSet";
                        frmWrk.UseYn = true;
                        frmWrk.ChangedFlag = MdlState.Updated;
                    }
                    else
                    {
                        frmWrkbs.Add(new FrmWrk
                        {
                            FrwId = item.FrwId,
                            FrmId = item.FrmId,
                            CtrlNm = item.CtrlNm,
                            WrkCd = "GridSet",
                            UseYn = true,
                            ChangedFlag = MdlState.Inserted
                        });
                    }
                }
            }

            gridControls.DataSource = frmCtrlbs;
            gridWorkset.DataSource = frmWrkbs;
        }

        private void FindUCControlsRecursive(Control parentControl)
        {
            foreach (Control ctrl in parentControl.Controls)
            {
                if (!ctrlClss.Any(c => c.CtrlRegNm == ctrl.GetType().FullName))
                {
                    ctrlCls = new CtrlMst
                    {
                        CtrlNm = ctrl.GetType().Name,
                        CtrlRegNm = ctrl.GetType().FullName,
                        Memo = ctrl.HasChildren ? "HasChildren" : "NoChildren"
                    };
                    ctrlClsRepo.Add(ctrlCls);
                }
                else
                {
                    ctrlCls = new CtrlMst
                    {
                        CtrlNm = ctrl.GetType().Name,
                        CtrlRegNm = ctrl.GetType().FullName,
                        Memo = ctrl.HasChildren ? "HasChildren" : "NoChildren"
                    };
                    ctrlClsRepo.Update(ctrlCls);
                }

                //수정 ctrlClss.Any(c => c.CtrlRegNm == ctrl.GetType().FullName) 이면서 UseYn = true 인 경우만 frmCtrlbs에 추가
                if (ctrlClss.Any(c => c.CtrlRegNm == ctrl.GetType().FullName && c.UseYn))
                {
                    if (ctrlClss.Any(c => c.CtrlRegNm == ctrl.GetType().FullName && c.CustomYn))
                    {
                        //frmCtrlbs에 데이터가 있으면 업데이트, 없으면 추가.
                        var frmCtrl = frmCtrlbs.FirstOrDefault(c => c.CtrlNm == ctrl.Name);
                        if (frmCtrl != null)
                        {
                            frmCtrl.FrwId = Common.GetValue("gFrameWorkId");
                            frmCtrl.FrmId = selectedFrwFrm.FrmId;
                            frmCtrl.CtrlNm = ctrl.Name;
                            frmCtrl.ToolNm = ctrl.GetType().Name;
                            frmCtrl.CtrlW = GetWidth(ctrl);
                            frmCtrl.CtrlH = GetHeight(ctrl);
                            frmCtrl.CtrlX = ctrl.Location.X;
                            frmCtrl.CtrlY = ctrl.Location.Y;
                            frmCtrl.TitleText = GetTitleText(ctrl);
                            frmCtrl.TitleWidth = GetTitleWidth(ctrl);
                            frmCtrl.TitleAlign = GetTitleAlign(ctrl);
                            frmCtrl.DefaultText = GetText(ctrl);
                            frmCtrl.TextAlign = GetTextAlign(ctrl);
                            frmCtrl.ShowYn = ctrl.Visible;
                            frmCtrl.EditYn = GetEditYn(ctrl);
                            frmCtrl.ChangedFlag = MdlState.Updated;
                        }
                        else
                        {
                            frmCtrlbs.Add(new FrmCtrl
                            {
                                FrwId = Common.GetValue("gFrameWorkId"),
                                FrmId = selectedFrwFrm.FrmId,
                                CtrlNm = ctrl.Name,
                                ToolNm = ctrl.GetType().Name,
                                CtrlW = GetWidth(ctrl),
                                CtrlH = GetHeight(ctrl),
                                CtrlX = ctrl.Location.X,
                                CtrlY = ctrl.Location.Y,
                                TitleText = GetTitleText(ctrl),
                                TitleWidth = GetTitleWidth(ctrl),
                                TitleAlign = GetTitleAlign(ctrl),
                                DefaultText = GetText(ctrl),
                                TextAlign = GetTextAlign(ctrl),
                                ShowYn = ctrl.Visible,
                                EditYn = GetEditYn(ctrl),
                                ChangedFlag = MdlState.Inserted
                            });
                        }
                    }

                    if (ctrlClss.Any(c => c.CtrlRegNm == ctrl.GetType().FullName && c.ContainYn))
                    {
                        FindUCControlsRecursive(ctrl); // 재귀 호출
                    }
                }
            }
        }

        #region Get Control Properties
        private string GetTextAlign(Control ctrl)
        {
            if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.TextAlignment.ToString();
            //else if (ctrl is Ctrls.UCField ucField)
            //else if (ctrl is Ctrls.UCGrid ucGrid)
            //else if (ctrl is Ctrls.UCLookUp ucLookUp)
            //    return ucLookUp.TextAlignment.ToString();
            //else if (ctrl is Ctrls.UCPanel ucPanel)
            //else if (ctrl is Ctrls.UCSplit ucSplit)
            //else if (ctrl is Ctrls.UCDateBox ucDateBox)
            //    return ucLookUp.TextAlignment.ToString();
            else if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.TextAlignment.ToString();
            //else if (ctrl is Ctrls.UCTab ucTab)
            else
                return string.Empty;
        }

        private string GetText(Control ctrl)
        {
            if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.Text;
            else if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.Text;
            //else if (ctrl is Ctrls.UCMemo ucMemo)
            //    return ucLookUp.TitleWidth;
            //else if (ctrl is Ctrls.UCDateBox ucDateBox)
            //    return ucLookUp.TitleWidth;
            //else if (ctrl is Ctrls.UCCheckBox ucCheckBox)
            //    return ucLookUp.TitleWidth;
            else
                return "";
        }

        private int GetTitleWidth(Control ctrl)
        {
            if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.TitleWidth;
            else if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.TitleWidth;
            //else if (ctrl is Ctrls.UCMemo ucMemo)
            //    return ucLookUp.TitleWidth;
            //else if (ctrl is Ctrls.UCDateBox ucDateBox)
            //    return ucLookUp.TitleWidth;
            //else if (ctrl is Ctrls.UCCheckBox ucCheckBox)
            //    return ucLookUp.TitleWidth;
            else
                return 0;
        }

        private int GetWidth(Control ctrl)
        {
            if (ctrl is DevExpress.XtraTab.XtraTabPage tabPage)
                return tabPage.TabPageWidth;
            //else if (ctrl is Ctrls.UCButton ucButton)
            //else if (ctrl is Ctrls.UCField ucField)
            //else if (ctrl is Ctrls.UCGrid ucGrid)
            //else if (ctrl is Ctrls.UCLookUp ucLookUp)
            //else if (ctrl is Ctrls.UCPanel ucPanel)
            //else if (ctrl is Ctrls.UCSplit ucSplit)
            //else if (ctrl is Ctrls.UCTextBox ucTextBox)
            //else if (ctrl is Ctrls.UCTab ucTab)
            else
                return ctrl.Width;
        }
        private int GetHeight(Control ctrl)
        {
            if (ctrl is DevExpress.XtraTab.XtraTabPage tabPage)
                return ctrl.TabIndex;
            //else if (ctrl is Ctrls.UCButton ucButton)
            //else if (ctrl is Ctrls.UCField ucField)
            //else if (ctrl is Ctrls.UCGrid ucGrid)
            //else if (ctrl is Ctrls.UCLookUp ucLookUp)
            //else if (ctrl is Ctrls.UCPanel ucPanel)
            //else if (ctrl is Ctrls.UCSplit ucSplit)
            //else if (ctrl is Ctrls.UCTextBox ucTextBox)
            //else if (ctrl is Ctrls.UCTab ucTab)
            else
                return ctrl.Height;
        }

        private string GetTitleText(Control ctrl)
        {
            if (ctrl is Ctrls.UCButton ucButton)
                return ucButton.Text;
            //else if (ctrl is Ctrls.UCField ucField)
            //else if (ctrl is Ctrls.UCGrid ucGrid)
            else if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.Title;
            else if (ctrl is Ctrls.UCPanel ucPanel)
                return ucPanel.Text;
            else if (ctrl is Ctrls.UCSplit ucSplit)
                return ucSplit.Text;
            else if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.Title;
            //else if (ctrl is Ctrls.UCTab ucTab)
            else if (ctrl is DevExpress.XtraTab.XtraTabPage tabPage)
                return tabPage.Text;
            else
                return string.Empty;
        }

        private string GetTitleAlign(Control ctrl)
        {
            if (ctrl is Ctrls.UCButton ucButton)
                return ucButton.TitleAlignment.ToString();
            //else if (ctrl is Ctrls.UCField ucField)
            //else if (ctrl is Ctrls.UCGrid ucGrid)
            else if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.TitleAlignment.ToString();
            //else if (ctrl is Ctrls.UCPanel ucPanel)
            //else if (ctrl is Ctrls.UCSplit ucSplit)
            else if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.TitleAlignment.ToString();
            //else if (ctrl is Ctrls.UCTab ucTab)
            else
                return string.Empty;
        }

        private bool GetEditYn(Control ctrl)
        {
            if (ctrl is Ctrls.UCButton ucButton)
                return ucButton.EditYn;
            //else if (ctrl is Ctrls.UCField ucField)
            else if (ctrl is Ctrls.UCGrid ucGrid)
                if (ucGrid.gvCtrl != null)
                    return ucGrid.gvCtrl.Editable;
                else
                    return false; // or throw new Exception("gvCtrl is not initialized");
            else if (ctrl is Ctrls.UCLookUp ucLookUp)
                return ucLookUp.EditYn;
            else if (ctrl is Ctrls.UCPanel ucPanel)
                return ucPanel.EditYn;
            //else if (ctrl is Ctrls.UCSplit ucSplit)
            else if (ctrl is Ctrls.UCTextBox ucTextBox)
                return ucTextBox.EditYn;
            //else if (ctrl is Ctrls.UCTab ucTab)
            else
                return false;
        }
        #endregion

        private void ucPanel4_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                foreach (var frmCtrl in frmCtrlbs)
                {
                    if (frmCtrl.ChangedFlag == MdlState.Inserted)
                    {
                        frmCtrlRepo.Add(frmCtrl);
                    }
                    else if (frmCtrl.ChangedFlag == MdlState.Updated)
                    {
                        frmCtrlRepo.Update(frmCtrl);
                    }
                    else if (frmCtrl.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                var frmCtrl = gvControls.GetFocusedRow() as FrmCtrl;
                if (frmCtrl != null)
                {
                    frmCtrlbs.Remove(frmCtrl);
                    frmCtrlRepo.Delete(frmCtrl.FrwId, frmCtrl.FrmId, frmCtrl.CtrlNm);
                }
            }
        }
        private void ucPanel5_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                foreach (var frmWrk in frmWrkbs)
                {
                    if (frmWrk.ChangedFlag == MdlState.None)
                    {
                        continue;
                    }
                    else if (frmWrk.ChangedFlag == MdlState.Inserted)
                    {
                        frmWrkRepo.Add(frmWrk);
                    }
                    else if (frmWrk.ChangedFlag == MdlState.Updated)
                    {
                        frmWrkRepo.Update(frmWrk);
                    }
                }
            }
            else if (e.Button.Properties.Caption == "Delete")
            {
                var frmWrk = gvWorkset.GetFocusedRow() as FrmWrk;
                if (frmWrk != null)
                {
                    frmWrkbs.Remove(frmWrk);
                    frmWrkRepo.Delete(frmWrk.WrkId);
                }
            }
            else if (e.Button.Properties.Caption == "New")
            {
                //입력을 위한 새로운 line을 준비한다. 저장은 Save 버튼을 눌러야 한다.
                frmWrkbs.Add(new FrmWrk
                {
                    FrwId = Common.GetValue("gFrameWorkId"),
                    FrmId = selectedFrwFrm.FrmId,
                    CtrlNm = "",
                    WrkCd = "",
                    UseYn = true,
                    ChangedFlag = MdlState.Inserted
                });
            }
        }

        private void btnFTPUpload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FTP Upload");
        }
    }
}

```