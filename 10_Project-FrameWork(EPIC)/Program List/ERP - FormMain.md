---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: <% tp.file.creation_date() %>
MDT: <% tp.file.last_modified_date() %>
---
---
#### Prologue / Concept
FormMain - Save, Query, Delete, New 등의 버튼을 가지고 있다 
Forms는 XtraTabbedMdiManager을 통해 나열되고 활성화 되어있는 탭의 폼에 FrameButtonAction을 통해 Form별 액션을 정의한다.
모든 Forms는 FrwBase를 상속받아 액션을 오버라이드하여 사용한다. 
#### Manifestation
FrameMain
```C#
    public delegate void FrameButtonHandler(string frm, string query);

    public partial class AceMainFrame : DevExpress.XtraEditors.XtraForm
    {
	    public static event FrameButtonHandler FrameButtonActive;
        private void barButtonOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
        FrameButtonAction(xtraTabbedMdiManager.SelectedPage.MdiChild.Name, e.Item.Caption);
        }
    }
```

Frms - 
```C#
    public class CDA100 : FrwBase
    {
        public override void _Query()
        {
            g01.Open<G01>();
        }
    }
```

FrwBase
```C#
    public class FrmBase : DevExpress.XtraEditors.XtraUserControl
    {
        private void FrameButtonAction(string frm, string query)
        {
            if (ThisFrm == frm)
            {
                switch (query)
                {
                    case "Query":
                        _MenuClickQuery();
                        break;
                    case "New":
                        _MenuClickNew();
                        break;
                }
            }
        }
        private void _MenuClickQuery()
        {
            Common.gMsg = "Query";
            _Query();
        }
        public virtual void _Query()
        {
            MessageBox.Show("Query : It can be used by override");
        }
    }
```
#### Integration
## 문제점 분석 및 개선 방향

제시된 코드는 WinForms 기반 애플리케이션에서 MDI(Multiple Document Interface) 환경을 구현하고, 각 MDI 자식 폼(Form)의 공통 동작을 `FrwBase` 클래스를 통해 처리하는 구조입니다. 하지만 몇 가지 문제점과 개선할 부분이 보입니다.

**문제점:**

1. **강한 결합 (Tight Coupling):** `AceMainFrame`와 `FrwBase` 간에 `FrameButtonActive` 이벤트를 통해 강하게 결합되어 있습니다. 이는 유지보수를 어렵게 하고, 재사용성을 떨어뜨립니다.
2. **중복 코드:** `FrameButtonAction` 메서드 내부의 `switch` 문은 각 폼마다 비슷하게 반복될 가능성이 높습니다. 이는 코드 중복을 야기하고, 새로운 버튼 추가 시 모든 폼을 수정해야 하는 불편함을 초래합니다.
3. **추상화 부족:** `_Query` 메서드는 `FrwBase`에서 기본 동작을 제공하지만, 실제 쿼리 로직은 각 폼에서 구현해야 합니다. 이는 `FrwBase`의 추상화 수준을 낮추고, 코드 재사용성을 제한합니다.

**개선 방향:**

1. **약한 결합 (Loose Coupling):** 이벤트 대신 인터페이스나 델리게이트를 활용하여 `AceMainFrame`와 `FrwBase` 간의 의존성을 줄이고, 유연성을 높입니다.
2. **메서드 분리:** `FrameButtonAction` 메서드를 각 버튼 동작에 해당하는 별도의 메서드로 분리하여 코드 중복을 줄이고 가독성을 향상시킵니다.
3. **추상화 강화:** `_Query` 메서드를 추상 메서드로 변경하고, 각 폼에서 쿼리 로직을 구현하도록 하여 `FrwBase`의 추상화 수준을 높입니다.

## 개선된 코드 예시

**AceMainFrame:**

C#

```
public partial class AceMainFrame : DevExpress.XtraEditors.XtraForm
{
    private void barButtonOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        var childForm = xtraTabbedMdiManager.SelectedPage.MdiChild as FrwBase;
        if (childForm != null)
        {
            switch (e.Item.Caption)
            {
                case "Query":
                    childForm.OnQuery();
                    break;
                case "New":
                    childForm.OnNew();
                    break;
                // ...
            }
        }
    }
}
```

**FrwBase:**

C#

```
public abstract class FrwBase : DevExpress.XtraEditors.XtraUserControl
{
    public virtual void OnQuery()
    {
        MessageBox.Show("Query : It can be used by override");
    }

    public virtual void OnNew()
    {
        // ...
    }

    // ...
}
```

**CDA100:**

C#

```
public class CDA100 : FrwBase
{
    public override void OnQuery()
    {
        g01.Open<G01>();
    }
}
```

**설명:**

- `AceMainFrame`는 더 이상 `FrameButtonActive` 이벤트를 발생시키지 않고, 직접 활성화된 자식 폼의 메서드를 호출합니다.
- `FrwBase`는 각 버튼 동작에 대응하는 `OnQuery`, `OnNew` 등의 메서드를 정의하고, 기본 동작을 제공하거나 추상 메서드로 남겨둡니다.
- 각 폼은 `FrwBase`를 상속받아 필요한 메서드를 오버라이드하여 자신만의 동작을 구현합니다.

이러한 개선을 통해 코드의 결합도를 낮추고, 재사용성과 유지보수성을 향상시킬 수 있습니다. 또한, 새로운 버튼 추가 시 각 폼에서 해당 버튼에 대한 메서드만 구현하면 되므로 확장성도 높아집니다.
###### REFERENCE

