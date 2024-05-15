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

###### REFERENCE
