```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Lib;
using Lib.Repo;
using System.IO;
using Repo;
using DevExpress.XtraEditors.ButtonsPanelControl;
using Ctrls;
using Frms;
using DevExpress.XtraEditors;

namespace GAIA
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        [STAThread]
        static void Main()
        {
            sign = new SignIn();
            Application.Run(sign);

            if (signin)
            {
                FormMain m = new FormMain();
                sign.Close();
                Application.Run(m);
            }
            else
            {
                Application.Exit();
            }
        }

        static SignIn sign = null;
        internal static bool signin = false;

        public FormMain()
        {
            InitializeComponent();

            //menuCtrl.VisibleChanged += new EventHandler(menuCtrl_VisibleChanged);
            //msgCtrl.VisibleChanged += new EventHandler(msgCtrl_VisibleChanged);
            ucTab1.VisibleChanged += new EventHandler(ucTab1_VisibleChanged);
            Common.gMsgChanged += new EventHandler(AddGAIAMsg);

            ucTab1.Visible = false;
            ucTab1.SelectedTabPageIndex = 0;

            //msgCtrl.Visible = false;
            //menuCtrl.Visible = false;

            //FrameWork List
            List<PrjFrw> frmwrks = new PrjFrwRepo().GetAll();
            foreach (var frmWrk in frmwrks)
            {
                cmbForm.Properties.Items.Add(frmWrk);
            }

            //Tab 설정
            xtraTabbedMdiManager.AppearancePage.HeaderActive.ForeColor = System.Drawing.Color.BlueViolet;
            xtraTabbedMdiManager.AppearancePage.HeaderActive.BorderColor = System.Drawing.Color.Black;
            xtraTabbedMdiManager.AppearancePage.HeaderActive.Font = new System.Drawing.Font(xtraTabbedMdiManager.AppearancePage.HeaderActive.Font, System.Drawing.FontStyle.Bold);
            xtraTabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            //BarItem 설정
            Common.gMsg = "Ready";
            this.barStaticItemForm.Caption = "MainFrame";
            this.barStaticItemUser.Caption = $"{Common.GetValue("gFrameWorkNm")} | {Common.GetValue("gUserNm")} | {Common.GetValue("gDeptNm")}";
            this.barStaticItemSite.Caption = $"{Common.GetValue("gSysNm")}";
            this.barStaticItemTime.Caption = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void ucTab1_VisibleChanged(object sender, EventArgs e)
        {
            simpleButton1.Text = ucTab1.Visible ? "Hide" : "Show";
        }

        private void xtraTabbedMdiManager_SelectedPageChanged(object sender, EventArgs e)
        {
            //마지막 Tab 삭제될때 이벤트가 삭제보다 먼저 수행되어 에러 발생
            //xtraTabbedMdiManager.Pages.Count 사용 못하여 예외처리함
            try
            {
                if (xtraTabbedMdiManager.Pages.Count > 1)
                {
                    Common.gMsg = $"{xtraTabbedMdiManager.SelectedPage.MdiChild.Text} ({xtraTabbedMdiManager.SelectedPage.MdiChild.Name})";
                }
                //this.barStaticItemForm.Caption = xtraTabbedMdiManager.SelectedPage.MdiChild.Text + "(" + xtraTabbedMdiManager.SelectedPage.MdiChild.Name + ")";
            }
            catch (Exception)
            {
                Common.gMsg = $"Error";
                //this.barStaticItemForm.Caption = "";
            }
        }

        dynamic dynamicForm;
        private void cmbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox cmbForm = sender as ComboBox;
            PrjFrw frmWrk = cmbForm.SelectedItem as PrjFrw;
            Common.SetValue("gFrameWorkId", frmWrk.FrwId.ToString());

            menuCtrl.Items.Clear();
            menuCtrl.Visible = true;
            //From List by FrmaeWork
            List<FrwFrm> frms = new FrwFrmRepo().GetByOwnFrw(Common.GetValue("gRegId").ToInt(), Common.GetValue("gFrameWorkId"));
            foreach (var frm in frms)
            {
                menuCtrl.Items.Add(new IdObject { Txt = frm.FrmNm, Obj = frm });
            }
            menuCtrl.DisplayMember = "Txt";
            menuCtrl.ValueMember = "Obj";
        }

        private void menuCtrl_DoubleClick(object sender, EventArgs e)
        {
            //menuCtrl에서 선택된 리스트
            IdObject selectedItem = menuCtrl.SelectedItem as IdObject; // 안전한 형변환
            if (selectedItem == null)
                return;

            var mdi = (from c in MdiChildren
                       where c.Text.Contains(selectedItem.ToString())
                       select c).SingleOrDefault();

            if (mdi != null)
            {
                mdi.Activate();
            }
            else
            {
                FrwFrm frm = selectedItem.Obj as FrwFrm;
                if (frm != null)
                {
                    OpenFrm(frm);
                }
            }
        }

        private void OpenFrm(FrwFrm frm)
        {
            string frmFullPath = $"{frm.FilePath}\\{frm.FileNm}"; //@"C:\path\to\your\file.txt";

            if (File.Exists(frmFullPath))
            {
                Assembly assmbly = AppDomain.CurrentDomain.Load(File.ReadAllBytes(frmFullPath));

                string tyStr = $"{frm.NmSpace}";
                var ty = assmbly.GetType(tyStr);

                UserControl ucform = (UserControl)Activator.CreateInstance(ty);

                Form fb = new Form();
                fb.Controls.Add(ucform);
                ucform.Dock = System.Windows.Forms.DockStyle.Fill;
                fb.Name = frm.FrmId;
                fb.Text = frm.FrmNm;
                fb.MdiParent = this;

                fb.Show();

                // 폼이 표시된 후에 탭 페이지 활성화
                if (ucform is FrmBase frmBase)
                {
                    frmBase.ActivateAllTabsOnLoad = true;
                    frmBase.ActivateAllTabs();
                }
            }
            else
            {
                MessageBox.Show($"Form File not found.{frmFullPath}");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ucTab1.Visible = !(ucTab1.Visible);
            //menuCtrl.Visible = !(menuCtrl.Visible);
        }
        //private void menuCtrl_VisibleChanged(object sender, EventArgs e)
        //{
        //    //simpleButton1.Text = menuCtrl.Visible ? "Hide Menu": "Show Menu";
        //    simpleButton1.Text = ucTab1.Visible ? "Hide" : "Show";
        //}
        //private void msgCtrl_VisibleChanged(object sender, EventArgs e)
        //{
        //    barButtonShowMsg.Caption = msgCtrl.Visible ? "Hide Message": "Show Message";
        //}
        //private void barButtonShowMsg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    msgCtrl.Visible = !(msgCtrl.Visible);
        //}
        private void AddGAIAMsg(object sender, EventArgs e)
        {
            if (Common.gTrackMsg)
            {
                msgCtrl.Text += sender.ToString() + Environment.NewLine;
            }
        }

        private void ucPanel1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Common.gTrackMsg = true;
            (e.Button as GroupBoxButton).Caption = "Stop tracking";
        }

        private void ucPanel1_CustomButtonUnchecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            Common.gTrackMsg = false;
            (e.Button as GroupBoxButton).Caption = "Tracking";
        }
        private void ucPanel1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            switch (e.Button.Properties.Caption.Trim())
            {
                case "Clear":
                    msgCtrl.Text = string.Empty;
                    break;
                case "Copy":
                    Clipboard.SetText(msgCtrl.Text);
                    break;
                default:
                    break;
            }
        }

        public delegate void MyEventHandler(string frm, string action);
        public static event MyEventHandler BarButtonActive;

        public static void OnBarButtonActive(string frm, string action)
        {
            BarButtonActive?.Invoke(frm, action);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string selectedFormName = xtraTabbedMdiManager.SelectedPage.MdiChild.Name;
            string action = e.Item.Caption;
            OnBarButtonActive(selectedFormName, action);
        }


        private void barSubItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gMsg = "barSubItem1_ItemClick";
        }

        private void barBtnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gMsg = "barBtnOpen_ItemClick";
        }

        private void barBtnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gMsg = "barBtnNew_ItemClick";
        }

        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gMsg = "barBtnSave_ItemClick";
        }

        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gMsg = "barBtnDelete_ItemClick";
        }

        private void barBtnTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormPackaging fb = new FormPackaging();
            FormPackaging fb = new FormPackaging(Common.GetValue("gOpenFrm"));
            ////Form Mapping 작업중인 폼을 템플릿 폼에 올려서 열기
            ////로그를 활성화하여 모든 로그를 본다. 
            //Panel pn = fb.Controls.Find("Contents", true).FirstOrDefault() as Panel;
            //pn.Controls.Add(dynamicForm);
            //dynamicForm.Dock = System.Windows.Forms.DockStyle.Fill;
            fb.Text = Common.GetValue("gOpenFrm");
            fb.Name = Common.GetValue("gOpenFrm");
            fb.Show();
        }

        private void FormMain_ClientSizeChanged(object sender, EventArgs e)
        {
            this.barStaticItemMessage.Width = (int)(this.Width * 0.2);
            this.barStaticItemForm.Width = (int)(this.Width * 0.1);
            this.barStaticItemUser.Width = (int)(this.Width * 0.2);
            this.barStaticItemSite.Width = (int)(this.Width * 0.1);
            this.barStaticItemTime.Width = (int)(this.Width * 0.2);
        }
    }
}

```

```C#
using DevExpress.XtraEditors;
using Lib;
using Lib.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace GAIA
{
    public partial class SignIn : DevExpress.XtraEditors.XtraForm
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new SignIn());
        }
        public SignIn()
        {
            InitializeComponent();
            Common.SetValue("gUserProfilePath", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Common.SetValue("gIniFilePath", Path.Combine(Common.GetValue("gUserProfilePath"), "GAIA", "Setting.ini"));
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //IDO Login Process
            SignInProcess();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Others Login Process
            SignInProcess();
        }
        private void SignInProcess()
        {
            string id = txtId.Text;
            string pwd = txtPwd.Text;

            var repo = new UsrRepo();
            var usr = repo.CheckSignIn(id, pwd);

            if (usr == null)
            {
                lblResult.Text = $"A record with the code {id} was not found.";
            }
            else
            {
                Common.SetValue("gId", usr.UsrId);
                Common.SetValue("gRegId", usr.UsrRegId.ToString());
                Common.SetValue("gNm", usr.UsrNm);
                Common.SetValue("gCls", usr.Cls);

                FormMain.signin = true;
                this.Close();
            }
        }
    }
}
```

```C#
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace GAIA
{
    public partial class FormPackaging : DevExpress.XtraEditors.XtraForm
    {
        public FormPackaging()
        {
            InitializeComponent();
            //groupControl1의 capture라는 CheckButton의 값은 gTrackLog의 값으로 초기화한다. 
            groupControl1.CustomHeaderButtons[2].Properties.Checked = Common.gTrackLog;
        }
        public string openFrm { get; set; }
        dynamic dyForm;
        public FormPackaging(string frm)
        {
            InitializeComponent();
            //groupControl1의 capture라는 CheckButton의 값은 gTrackLog의 값으로 초기화한다. 
            groupControl1.CustomHeaderButtons[2].Properties.Checked = Common.gTrackLog;

            openFrm = frm;

            //string frmPath = $"{Common.gDirRoot}{Common.gDirWork}\\{frm}.dll";
            string frmPath = $"F:\\20_EpicFrameWork\\Controller\\TestFromTextBox\\bin\\Debug\\net8.0-windows\\TestFromTextBox.dll";

            if (FileSystem.FileExists(frmPath))
            {
                Assembly assmbly = AppDomain.CurrentDomain.Load(File.ReadAllBytes(frmPath));
                //string tyStr = $"{Common.gSolution}.Frms.{frm}";
                string tyStr = $"Frms.TestFromTextBox";
                var ty = assmbly.GetType(tyStr);
                UserControl ucform = (UserControl)Activator.CreateInstance(ty);
                Contents.Controls.Add(ucform);
                ucform.Dock = System.Windows.Forms.DockStyle.Fill;
                dyForm = ucform;
            }
        }

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //Capture 라는 CheckButton을 누르면 Capture + Checked값을 출력되도록 수정하세요.
            if (e.Button.Properties.Caption == "Clear")
            {
                memoLog.Text = string.Empty;
            }
            else if (e.Button.Properties.Caption == "Copy")
            {
                if (!string.IsNullOrWhiteSpace(memoLog.Text))
                {
                    Clipboard.SetText(memoLog.Text);
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Common.gLog = "1234";
        }

        private void groupControl1_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Log Capture")
            {
                if (e.Button.Properties.Checked)
                {
                    Common.gTrackLog = true;
                }
                else
                {
                    Common.gTrackLog = false;
                }
            }
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void FormPackaging_Load(object sender, EventArgs e)
        {

            Common.gLogChanged += new EventHandler(ShowLog);
        }

        private void ShowLog(object sender, EventArgs e)
        {
            if (Common.gTrackLog)
            {
                memoLog.Text += sender.ToString() + Environment.NewLine;
            }
        }
    }
}
```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```

```C#

```
