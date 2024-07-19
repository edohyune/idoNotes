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


###### Binding Controller
UCCheckBox, UCDateBox, UCLookUp, UCMemo, UCRichText, UCTextBox
###### Non Binding Controller
UCPanel, UCSplit, UCTab 




내가 만든 컨트롤러 파일을 올려 줄꺼야 보고 ICtrls 인터페이스를 만들어줘
#### Manifestation

###### IWorkSet 인터페이스 
```C#
public interface IWorkSet
{
    void Load();
    void Save();
    void Delete();
    void SetParameters(Dictionary<string, object> parameters);
    Dictionary<string, object> GetParameters();
}
```
###### 컨트롤러의 기능과 역할로 인터페이스를 구현
- 속성과 메서드 명확하게 구현 
- 역할 기반으로 시스템을 설계
- 각 인터페이스는 컨트롤러의 특정 역할을 정의
```C#
public interface IVisible
{
    bool ShowYn { get; set; }
}
public interface IEditable
{
    bool EditYn { get; set; }
}
public interface IBindable : IVisible, IEditable
{
    string BindText { get; set; }
    event PropertyChangedEventHandler PropertyChanged;
}
public interface IWrkFldLoadable
{
    void LoadWrkFld(WrkFld wrkFld);
}
public interface ITextBased
{
    string Text { get; set; }
}
public interface IAlignment
{
    DevExpress.Utils.HorzAlignment TitleAlignment { get; set; }
    DevExpress.Utils.HorzAlignment TextAlignment { get; set; }
}
public interface ISizeable
{
    int ControlWidth { get; set; }
    int TitleWidth { get; set; }
}
public interface IUCTextBox : IBindable, IWrkFldLoadable, ITextBased, IAlignment, ISizeable
{
    bool ButtonVisiable { get; set; }
}
public interface IUCCheckBox : IBindable, IWrkFldLoadable
{
    bool Checked { get; set; }
}
public interface IUCLookUp : IBindable, IWrkFldLoadable, ITextBased, IAlignment, ISizeable
{
    string Title { get; set; }
}
public interface IUCMemo : IBindable, IWrkFldLoadable, ITextBased, IAlignment, ISizeable
{
    string Title { get; set; }
}
public interface IUCPanel : IVisible, IWrkFldLoadable
{
    string Text { get; set; }
}
public interface IUCSplit : IVisible, IWrkFldLoadable
{
    int TitleWidth { get; set; }
}
public interface IUCTab : IVisible, IWrkFldLoadable
{
    string Title { get; set; }
}

```


#### Integration

###### REFERENCE
