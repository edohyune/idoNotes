---
Start Date: 2024-06-04
Status: Prologue
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-04
tags: 
CDT: 2024-06-04 11:52
MDT: 2024-06-04 15:32
---
---
#### Prologue / Concept

#### Manifestation

#### Integration

##### REFERENCE

###### Delegate Event 정의

```C#
public delegate void delEventInitNewRow(object sender, InitNewRowEventArgs e);   
public event delEventInitNewRow UCInitNewRow;

private void gvCtrl_InitNewRow(object sender, InitNewRowEventArgs e)
{
    // WrkFld 기본값 설정
    GridView view = sender as GridView;
    if (view != null)
    {
        view.SetRowCellValue(e.RowHandle, view.Columns["WrkFld"], "기본값");
    }
    
    // UCInitNewRow 이벤트 호출
    UCInitNewRow?.Invoke(sender, e);
}
```

```C#
public delegate void delEventInitNewRow(object sender, InitNewRowEventArgs e);
```
델리게이트는 특정 메서드 시그니처(메서드의 반환 타입과 매개 변수 리스트)를 정의
`object sender, InitNewRowEventArgs e` 매개 변수를 가지는 메서드

```C#
public event delEventInitNewRow UCInitNewRow;
```
델리게이트의 인스턴스
`UCInitNewRow`는 `delEventInitNewRow` 델리게이트 타입의 이벤트

```C#
private void gvCtrl_InitNewRow(object sender, InitNewRowEventArgs e)
{
    // WrkFld 기본값 설정
    GridView view = sender as GridView;
    if (view != null)
    {
        view.SetRowCellValue(e.RowHandle, view.Columns["WrkFld"], "기본값");
    }

    // UCInitNewRow 이벤트 호출
    UCInitNewRow?.Invoke(sender, e);
}
```
`gvCtrl_InitNewRow` 메서드는 새로운 행이 초기화될 때 호출

```C#
UCInitNewRow?.Invoke(sender, e);
```
`UCInitNewRow` 이벤트가 `null`이 아닌 경우에만 `Invoke` 메서드를 호출
`Invoke` 메서드는 이벤트에 연결된 모든 핸들러를 순차적으로 호출합니다. 여기서 `sender`는 이벤트를 발생시킨 객체 (`GridView`)를 나타내고, `e`는 이벤트 데이터 (`InitNewRowEventArgs`)

###### Handler 핸들러의 순차적인 호출
EventHandler : 이벤트 핸들러는 이벤트가 발생했을 때 호출될 메서드

```C#
gridName.UCInitNewRow += new delEventInitNewRow(MyInitNewRowHandler);
```
OR
```C#
gridName.UCInitNewRow += gridname_UCInitNewRow;
```
`MyInitNewRowHandler`는 `delEventInitNewRow` 델리게이트와 동일한 시그니처를 가지는 메서드 (실행 메서드의 이름)

```C#
gridName.UCInitNewRow += gridname_UCInitNewRow01;
gridName.UCInitNewRow += gridname_UCInitNewRow02;
gridName.UCInitNewRow += gridname_UCInitNewRow03;
```
동일한 이벤트에 여러개의 이벤트 핸들러를 정의할 수 있다. 
실행은 정의된 순서에 의해 실행된다. 

```C#
UCInitNewRow?.Invoke(sender, e);
```
이벤트에 등록된 핸들러가 있으면 `Invoke` 메서드가 해당 핸들러를 호출


델리게이트 : 
차원과 차원의 연결 통로
다른 종류의 런타임 개체를 연결하는 통로

이벤트핸들러 : 
연결 통로의 문을 여는 행위를 정의
다른 종류의 런타임 개체에서 수행하는 행위를 정의

수행을 하는 명령 : 
Invoke 
CallbackDelegate
등이다. 

1. **델리게이트**: 차원과 차원의 연결 통로
	- 델리게이트는 메서드 참조를 캡슐화하고, 서로 다른 메서드를 연결할 수 있는 통로를 제공합니다.
1. **이벤트 핸들러**: 연결 통로의 문을 여는 행위
	- 이벤트 핸들러는 특정 이벤트가 발생했을 때 실행되는 메서드를 정의합니다. 이벤트가 발생하면, 해당 핸들러는 문을 열고 정의된 동작을 수행합니다.
2. **수행을 하는 명령**: Invoke, CallbackDelegate 등
	- `Invoke`와 같은 명령은 델리게이트나 이벤트를 호출하여 등록된 메서드를 실행합니다. 이는 실제로 연결된 메서드를 호출하는 역할을 합니다.

