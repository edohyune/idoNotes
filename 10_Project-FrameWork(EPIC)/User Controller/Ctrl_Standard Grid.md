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
EmbeddedNavigator 관련
```C#
            gcForms.UseEmbeddedNavigator = true;
            gcForms.EmbeddedNavigator.ButtonClick += gdForms_EmbeddedNavigator_ButtonClick;
            gcForms.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.First.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.Last.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.Next.Enabled = false;
            gcForms.EmbeddedNavigator.Buttons.Next.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            gcForms.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.Prev.Enabled = false;
            gcForms.EmbeddedNavigator.Buttons.Prev.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            gcForms.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            gcForms.EmbeddedNavigator.Buttons.Remove.Visible = false;
```

```C#
               private void gdForms_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            var view = gvForms;
            var data = gcForms.DataSource as List<FrmMst>;

            switch (e.Button.ButtonType)
            {
                case NavigatorButtonType.Append:
                    break;
                case NavigatorButtonType.Remove:
                    var idToRemove = view.GetFocusedRowCellValue("FrmId");
                    if (idToRemove != null)
                    {
                        frmMstRepo.Delete((string)idToRemove);
                        data.Remove(data.Find(x => x.FrmId == (string)idToRemove));
                        view.RefreshData();
                    }
                    break;
                case NavigatorButtonType.Edit:
                    view.ShowEditor();
                    break;
                case NavigatorButtonType.EndEdit:
                    if (view.IsEditing)
                    {
                        view.CloseEditor();
                        view.UpdateCurrentRow();
                    }

                    var updatedRow = view.GetFocusedRow() as FrmMst;

                    if (updatedRow != null)
                    {
                        if (updatedRow.ChangedFlag == MdlState.Inserted)
                        {
                            // 새 행을 추가합니다.
                            frmMstRepo.Add(updatedRow);
                        }
                        else
                        {
                            // 기존 행을 업데이트합니다.
                            frmMstRepo.Update(updatedRow);
                        }
                        view.RefreshData();
                    }
                    break;
            }
        }
```

#### Integration

###### REFERENCE
