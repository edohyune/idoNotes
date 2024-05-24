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

여기서부터는 컬럼 타입에 따른 ColumnEdit 설정 예시입니다.
실제 ColumnEdit 설정은 컨트롤 타입에 따라 다양하게 구현될 수 있습니다.
예를 들어 RepositoryItemTextEdit, RepositoryItemDateEdit, RepositoryItemCheckEdit 등이 있습니다.
```C#
column.ColumnEdit = null;  // Make Function fn(ctrl_ty) to set column.ColumnEdit
switch (fldTy)
{
	case "Text":
		// 텍스트 편집 컨트롤 설정
		column.ColumnEdit = new RepositoryItemTextEdit();
		break;
	case "Date":
		// 날짜 편집 컨트롤 설정
		column.ColumnEdit = new RepositoryItemDateEdit();
		break;
	case "Int":
		// 정수 편집 컨트롤 설정
		var edit = new RepositoryItemTextEdit();
		edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
		edit.Mask.EditMask = "d";
		column.ColumnEdit = edit;
		break;
	case "Decimal":
		// 십진수 편집 컨트롤 설정
		var editDecimal = new RepositoryItemTextEdit();
		editDecimal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
		editDecimal.Mask.EditMask = "n2";
		column.ColumnEdit = editDecimal;
		break;
		// ... 기타 컨트롤 타입에 대한 설정
}
```

##### FldTy
CheckBox
Code
Code
Combo
Combo
PopUp
Memo









###### REFERENCE
