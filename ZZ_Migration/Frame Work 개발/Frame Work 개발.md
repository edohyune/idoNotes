[[ZZ_Migration/Frame Work 개발/Dictionary]]

```Plain

/*
FrameWork이란 시스템에 연결되는 아래 4가지를 말한다.

1. FormMaker(FormMapping Frms 메타데이터 편집기)
개발 툴은 독립적인 환경에서 개발하고 배포함
#   - SignIn
#   - Main Form 
#   - 기능폼 ( Mapping-Development )
FormMaker를 구성하는 CoreFrms는 현재의 시스템에서 개발하고 배포함
        
FormMaker는 단위(시스템)별 구분하여 개발환경을 제공한다.
FormMaker는 FormMain에서 사용할 Controller를 단위(시스템)별로 지정한다. 
FormMaker(Designer)는 개발자별 인터페이스를 제공한다.
FormMaker(Community)는 사용자는 벨트(그룹)별 자료를 공유한다.

2. Frms(FormMaker에서 생산되는 사용자 화면)
FormMain를 구성하는 UserFrms는 FormMaker에서 개발하고 배포함
       
3. Ctrls(사용자 화면에서 사용되는 컨트롤)
Controller는 FrameWork별로 제공된다. 
Table - USERCTRL - 사용중인 컨트롤러 

4. FormMain(사용자 포털 화면 - Frms 실행 인터페이스)
사용자 포털은 독립적인 환경에서 개발하고 배포함
FormMain은 단위(시스템)별로 제공된다.
FormMain의 FORM(비지니스로직)은 FormMaker를 통해 개발한다. 
FormMain은 사용할 Controller를 지정한다. 



System이란 단위(시스템) - 
    BJCHI ERP, BJC GroupWare, 라오스 Sales Program 등 이다.  
    하나의 시스템에는 여러개의 프레임 워크를 가질수 있다. 
    시스템은 프레임워크의 구성요소를 묶는 역할을 한다. 

    기능 - 등록(이름)
    SYSMST - 시스템 생성
    SYSDTL - FrameWork 생성
    

FrameWork이란 시스템에 연결되는 아래 4가지를 말한다.

    1. FormMaker(FormMapping Frms 메타데이터 편집기)
        개발 툴은 독립적인 환경에서 개발하고 배포함
           #   - SignIn
           #   - Main Form 
           #   - 기능폼 ( Mapping-Development )
        FormMaker를 구성하는 CoreFrms는 현재의 시스템에서 개발하고 배포함
        
        FormMaker는 단위(시스템)별 구분하여 개발환경을 제공한다.
        FormMaker는 FormMain에서 사용할 Controller를 단위(시스템)별로 지정한다. 
        FormMaker(Designer)는 개발자별 인터페이스를 제공한다.
        FormMaker(Community)는 사용자는 벨트(그룹)별 자료를 공유한다.

    2. Frms(FormMaker에서 생산되는 사용자 화면)
       FormMain를 구성하는 UserFrms는 FormMaker에서 개발하고 배포함
       
    3. Ctrls(사용자 화면에서 사용되는 컨트롤)
        Controller는 FrameWork별로 제공된다. 
        Table - USERCTRL - 사용중인 컨트롤러 

    4. FormMain(사용자 포털 화면 - Frms 실행 인터페이스)
        사용자 포털은 독립적인 환경에서 개발하고 배포함
        FormMain은 단위(시스템)별로 제공된다.
        FormMain의 FORM(비지니스로직)은 FormMaker를 통해 개발한다. 
        FormMain은 사용할 Controller를 지정한다. 
*/
```

해야할것

- 상속받은 컴포넌트에 다른 컨트롤러 추가하기

프로그램 전역변수 할당 및 등록

- WorkSet BCA200 ⇒ SC210 system_manager_user_param 워크셋 참고하기

  

그리드

그리드 구성 - 컬럼, 속성 셋팅

- 그리드 컬럼의 속성
    
    (ATZ310) 코드, 이름, 정렬
    

  

GaiaHelper(싱글톤)를 구성하고 Repository에서 구현

모델이 필요한데 모델을 동적으로 생성 (셈플 : CDA200)

JSON파일 만들기

- 우선 : 수동생성후 TEST하고 적당한 위치에 함수 만들기
- 함수 : GAIA의 데이터를 이용해서 JSON 생성

- JSON을 이용하여 모델생성

그리드에 비지니스 데이터 올리기

- SeleneHelper(공유리소스클래스)로 구성하고 동적모델을 이용하여 처리

  

  

  

  

[[환경설정]]

  

[[SQL Editor]]

[[MdiParent]]

[[C Book]]

[[동적 Class 생성]]

[[Sample - Remin Table Design]]

[[Calendar - TIMENDAY]]

[[LISTT - COMBOBOX]]

[[ACE 캡쳐]]

page

[[Code Basis]]

```Plain
너는 Frame9.Tools라는 라이브러리를 개발하려고 하는 개발자야

해당 라이브러리는 .NET Framework v4를 기반으로 하고 있으며, 
다양한 사용자 정의 컨트롤과 유틸리티 클래스를 포함하고 있다. 
여기에는 UI 컴포넌트(eButton, eCheck, eCombo, 등), 
데이터 처리 컴포넌트(WorkSet, Join, FilterCondition, 등), 
그리고 UI와 데이터를 연결하는 인터페이스(intField, intControl, intGrid, 등)가 포함되어 있있다.

너는 시니어 개발자이고 이 프로젝트의 PM이다. 
너에게는 IDO라고 하는 주니어 개발자가 있다. 
IDO의 너의 지시를 수행하는 정도이다.

```

  

[[SignIn]]

[[Creatrix 개발]]