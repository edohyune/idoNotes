---
Start Date: 2024-06-03
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-03
tags: 
CDT: 2024-06-03 21:07
MDT: 2024-06-03 21:08
---
---
#### Prologue / Concept

#### Manifestation

#### Integration
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class AaaAaa : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }
    }
    public interface IAaaAaaRepo
    {
        List<AaaAaa> GetAaaAaas(string frwId, string cd);
        AaaAaa GetAaaAaa(string frwId, string cd, string refNo);
        void Add(AaaAaa aaaAaa);
        void Update(AaaAaa aaaAaa);
        void Delete(AaaAaa aaaAaa);
    }
    public class AaaAaaRepo : IAaaAaaRepo
    {
        public List<AaaAaa> GetAaaAaas(string frwId, string cd)
        {
            string sql = @"
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<AaaAaa>(sql, new { FrwId = frwId, Cd = cd }).ToList();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in result)
                    {
                        item.ChangedFlag = MdlState.None;
                    }
                    return result;
                }
            }
        }
        public AaaAaa GetAaaAaa(string frwId, string cd, string refNo)
        {
            string sql = @"
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<AaaAaa>(sql, new { FrwId = frwId, Cd = cd, refNo = refNo }).SingleOrDefault();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }
        public void Add(AaaAaa aaaAaa)
        {
            string sql = @"
       " + Common.gRegId + @", getdate(), " + Common.gRegId + @", getdate()

";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
        public void Update(AaaAaa aaaAaa)
        {
            string sql = @"
       MId= "" + Common.gRegId + @"",
       MDt= getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
        public void Delete(AaaAaa aaaAaa)
        {
            string sql = @"
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
    }
}

```
###### REFERENCE

###### CTRL #Controller 

```C#

namespace Ctrls
{
    [System.ComponentModel.DefaultBindingProperty("BindText")]
    [System.ComponentModel.DefaultEvent("UCEditValueChanged")]
    public class UCMemo : DevExpress.XtraEditors.MemoEdit, INotifyPropertyChanged
    {
        #region Properties Browsable(true) ------------------------------------------------>>
        [Browsable(false)]
        private string frwId { get; set; }
        private string frmId { get; set; }
        private string ctrlNm { get; set; }        
        #endregion
        
        #region Properties Browsable(false) ----------------------------------------------->>
        #endregion
        #region Method - Value Get Set ---------------------------------------------------->>
        #endregion
        #region Event --------------------------------------------------------------------->>
        #endregion
        #region Properties Browsable(true) ------------------------------------------------>>
        #endregion
        [Category("A UserController Property"), Description("Default Text")] //chk
        public override string Text
        {
            get
            {
                string str = (this.memoCtrl.Text == null) ? string.Empty : this.memoCtrl.Text;
                return str;
            }
            set
            {
                this.memoCtrl.Text = value;
                this.BindText = value;  // Text가 업데이트 될 때 BindText도 업데이트
            }
        }
        [Category("A UserController Property"), Description("Bind Text"), Browsable(false)]
        public string BindText
        {
            get
            {
                string str = (this.memoCtrl.Text == null) ? string.Empty : this.memoCtrl.Text;
                return str;
            }
            set
            {
                this.memoCtrl.Text = value;
                OnPropertyChanged("BindText");
                UCEditValueChanged?.Invoke(this, memoCtrl);
            }
        }



        public DevExpress.XtraEditors.MemoEdit memoCtrl;
        public DevExpress.XtraEditors.LabelControl labelCtrl;
        public SplitContainer splitCtrl;
                
        public UCMemo()
        {
            this.Width = 210;
            this.Height = 40;
            
            memoCtrl = new DevExpress.XtraEditors.MemoEdit();
            memoCtrl = new DevExpress.XtraEditors.MemoEdit();
            memoCtrl = new DevExpress.XtraEditors.MemoEdit();

            memoCtrl.Text = "UCMemo";
            memoCtrl.EditValueChanged += memoCtrl_EditValueChanged;
            memoCtrl.TextChanged += memoCtrl_TextChanged;

            HandleCreated += UCMemo_HandleCreated;
        }

        private void UCMemo_HandleCreated(object? sender, EventArgs e)
        {
            frwId = Common.gFrameWorkId;

            Form? form = this.FindForm();
            if (form != null)
            {
                frmId = form.Name;
            }
            else
            {
                frmId = "Unknown";
            }
            ctrlNm = this.Name;

            //Design모드에서 DB에서 설정값을 가져오지 않기
            if (frwId != string.Empty)
            {
                ResetCtrl();
            }
        }

        private void ResetCtrl()
        {
            Common.gMsg = "UCMemo_HandleCreated";
            try
            {

            }
            catch (Exception ex)
            {
                Common.gMsg = $"UCMemo_HandleCreated>>ResetCtrl{Environment.NewLine}Exception : {ex.Message}";
            }
        }

        #region INotifyPropertyChanged
        public delegate void delEventEditValueChanged(object Sender, Control control);   // delegate 선언
        public event delEventEditValueChanged UCEditValueChanged;   // event 선언
        public event PropertyChangedEventHandler? PropertyChanged;

        private void memoCtrl_EditValueChanged(object sender, EventArgs e)
        {
            string strDate = string.Empty;
            strDate = memoCtrl.Text;
            if (UCEditValueChanged != null)  // 부모가 Event를 생성하지 않았을 수 있으므로 생성 했을 경우에만 Delegate를 호출
            {
                UCEditValueChanged(this, memoCtrl);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void memoCtrl_TextChanged(object sender, EventArgs e)
        {
            BindText = memoCtrl.Text;
        }
        #endregion

    }
}

```

```C#

```