```C#
using Lib;
using Lib.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrls.FrmBase.Models;
using Dapper;
using Ctrls;
using System.Dynamic;
using DevExpress.XtraTab;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraEditors;

namespace Frms
{
    public class FrmBase : UserControl
    {
        #region Properties Browseable(false) ----------------------------------------------------------
        [Browsable(false)]
        private string frwId { get; set; }
        [Browsable(false)]
        private string frmId { get; set; }
        public bool ActivateAllTabsOnLoad { get; set; }

        private object OSearchParam;
        private DynamicParameters DSearchParam;

        private List<UCFieldSet> fieldSets;
        private List<UCGridNav> gridSets;
        private List<FrmWrk> wrkSetsOrderby;

        #endregion
        #region Properties Browseable(true) -----------------------------------------------------------

        #endregion
        public FrmBase()
        {
            Load += FrmBase_Load;
            fieldSets = new List<UCFieldSet>();
            gridSets = new List<UCGridNav>(); 
            Common.gMsg = "--------------------------------------------------------------------";
            Common.gMsg = "-- Field Set Start -------------------------------------------------";
            Common.gMsg = "--------------------------------------------------------------------";
        }

        private void FrmBase_Load(object? sender, EventArgs e)
        {
            frwId = Lib.Common.GetValue("gFrameWorkId");

            Form? form = this.FindForm();
            frmId = form != null ? form.Name : "Unknown";

            if (!string.IsNullOrEmpty(frwId))
            {
                //Initialize FieldSet registered in Form
                ResetWorkSet();
            }
        }

        private void ResetWorkSet()
        {
            //WrkId, FrwId, FrmId, CtrlNm, WrkNm, WrkCd, UseYn, Memo 모두 string 제외 useYn은 bool
            wrkSetsOrderby = new FrmWrkRepo().GetByWorkSetsOpenOrderby(frwId, frmId);

            if (wrkSetsOrderby.Count > 0)
            {
                foreach (FrmWrk frmWrk in wrkSetsOrderby)
                {
                    Common.gMsg = "--------------------------------------------------------------------";
                    Common.gMsg = frmWrk.WrkId;
                    Common.gMsg = "--------------------------------------------------------------------";

                    if (frmWrk.WrkCd=="FieldSet")
                    {
                        UCFieldSet fieldSet = new UCFieldSet(frwId, frmId, frmWrk.WrkId);
                        if (fieldSet != null)
                        {
                            fieldSets.Add(fieldSet);
                            this.Controls.Add(fieldSet);
                            fieldSet.InitializeField();
                            fieldSet.DataChanged += WorkSet_DataChanged;
                        }
                    }
                    else if (frmWrk.WrkCd == "GridSet")
                    {
                        UCGridNav gridSet = CtrlHelper.FindControlRecursive<UCGridNav>(this, frmWrk.WrkId);
                        if (gridSet != null)
                        {
                            gridSets.Add(gridSet);
                        }
                    }
                }
            }
        }

        #region this.Open() ----------------------------------------------------------
        //전체오픈
        protected void Open()
        {
            var openOrderby = wrkSetsOrderby.OrderBy(wrk => wrk.OpenSq).ToList();

            foreach (var wrkSet in openOrderby)
            {
                var fieldSet = fieldSets.Find(fs => fs.thisNm == wrkSet.WrkId);
                if (fieldSet != null)
                {
                    fieldSet.Open();
                    Common.gMsg= $"FieldSet Open : {wrkSet.WrkId} ==================================";
                }

                var gridSet = gridSets.Find(gs => gs.Name == wrkSet.WrkId);
                if (gridSet != null)
                {
                    gridSet.Open();
                    Common.gMsg = $"GridSet Open : {wrkSet.WrkId} ==================================";
                }
            }
        }
        #endregion

        #region this.Save() ----------------------------------------------------------
        private void Save()
        {
            var saveOrderby = wrkSetsOrderby.OrderBy(wrk => wrk.SaveSq).ToList();

            foreach (var wrkSet in saveOrderby)
            {
                var fieldSet = fieldSets.Find(fs => fs.thisNm == wrkSet.WrkId);
                if (fieldSet != null)
                {
                    fieldSet.Save();
                    Common.gMsg = $"FieldSet Save : {wrkSet.WrkId} ==================================";
                }

                var gridSet = gridSets.Find(gs => gs.Name == wrkSet.WrkId);
                if (gridSet != null)
                {
                    gridSet.Save();
                    Common.gMsg = $"GridSet Save : {wrkSet.WrkId} ==================================";
                }
            }
        }
        #endregion

        private void SaveWorkSetData(MdlWrkSet model)
        {
            //frwId, frmId, wrkId를 알면 해당 워크셋에서 사용하는 insert update, delete 쿼리를 가지고 올 수 있다. 
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void WorkSet_DataChanged(object sender, DataChangedEventArgs e)
        {
            // 데이터가 변경되면 해당 필드셋과 이후의 모든 필드셋을 다시 엽니다.
            bool reopen = false;
            foreach (UCFieldSet fieldSet in fieldSets)
            {
                if (fieldSet == sender)
                {
                    reopen = true;
                }

                if (reopen)
                {
                    fieldSet.Open();
                }
            }

            foreach (UCGridNav gridSet in gridSets)
            {
                if (gridSet == sender)
                {
                    reopen = true;
                }

                if (reopen)
                {
                    gridSet.Open();
                }
            }
        }

        #region 탭페이지 활성화 ---------------------------------------------------
        public void ActivateAllTabs()
        {
            ActivateTabPages(this); // this는 현재 폼(FrmBase)을 나타냅니다.
            ResetSelectedTabPages(this); // 활성화 후 원래 선택된 탭 페이지로 복구
        }
        protected void ActivateTabPages(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is UCTab ucTab)
                {
                    foreach (XtraTabPage tabPage in ucTab.TabPages)
                    {
                        ucTab.SelectedTabPage = tabPage;
                        ActivateTabPages(tabPage); // 탭 페이지 내의 컨트롤도 확인
                    }
                }
                else
                {
                    ActivateTabPages(control); // 다른 컨트롤 내부도 재귀적으로 확인
                }
            }
        }

        protected void ResetSelectedTabPages(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control is UCTab ucTab)
                {
                    var originalSelectedPage = ucTab.SelectedTabPage;
                    ucTab.SelectedTabPage = originalSelectedPage; // 원래 선택된 탭 페이지로 복구
                    ResetSelectedTabPages(ucTab);
                }
                else
                {
                    ResetSelectedTabPages(control); // 다른 컨트롤 내부도 재귀적으로 확인
                }
            }
        }
        #endregion
    }
}


```