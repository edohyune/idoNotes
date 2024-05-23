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

#### Manifestation
```C#
            ucPanel4.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Save", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Delete", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            ucPanel4.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            ucPanel4.CustomButtonClick += ucPanel4_CustomButtonClick;

private void ucPanel4_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
{
	if (e.Button.Properties.Caption == "New")
	{
		Common.gMsg = "Mew";
	}
	else if (e.Button.Properties.Caption == "Save")
	{
		if (txtChange.Text == MdlState.Inserted.ToString())
		{
			Common.gMsg = "Insert";
		}
		else 
		{
			Common.gMsg = "Update";
		}
	}
	else if (e.Button.Properties.Caption == "Delete")
	{
		Common.gMsg = "Delete";
	}
}
```
#### Integration

```C#
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
                    frmCtrlRepo.Delete(frmCtrl.FrwId, frmCtrl.FrmId, frmCtrl.CtrlNm);
                }
            }
```
###### REFERENCE
            pnlModel.Controls.Add(rtxtModel);
            pnlModel.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] { new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Generate", true, buttonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Save", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1), new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Delete", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1) });
            pnlModel.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.BeforeText;
            pnlModel.Dock = DockStyle.Fill;
            pnlModel.EditYn = true;
            pnlModel.Location = new Point(0, 0);
            pnlModel.Name = "pnlModel";
            pnlModel.ShowYn = true;
            pnlModel.Size = new Size(713, 425);
            pnlModel.TabIndex = 3;
            pnlModel.Title = "UCPanel";
            pnlModel.TitleAlignment = DevExpress.Utils.HorzAlignment.Default;