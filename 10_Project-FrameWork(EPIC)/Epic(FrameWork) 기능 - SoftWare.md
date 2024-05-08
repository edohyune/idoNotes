---
Start Date:
Status:
Concept: false
Manifestation: false
Integration: false
Done:
tags:
CDT: 2024-04-30 17:04
---
---
## idoMaker(EpicPrologue)
FrameWork Designer 이다.
Gais에서 Designer 메타정보를 받아온다.
Astroid 메타데이터를 개발, 저장하고 SELENE에서 게시한다.
## Astroid
### Certification
#### 성공



##### 전역변수 셋팅
###### Input & Setting
	로그인 정보를 기초로 프로그렘에 필요한 정보를 셋팅한다.
##### Program 호출
###### idoMaker
- FormDesigner (FormMaker)
- DLL Preview - 사용자 확인
	미리보기의 경우 세션정보가 필요하다.
	세션정보는 FrameWork에서 참고하여 가져온다.
	Designer 에서 해당 프레임워크에 접근하는 방법
		SignIn 처리를 Designer에서 처리한다.
		MainFrameWork에서 SignIn은 Lib에서 상속
###### Asteroid
 - FormDesigner (FormMaker)
	 - Publish
		 - Library Version 
			 - Livrary / Function
			 - Sesseion Function ??
			 - Database Helper
			 - General Function
		 - Custome User Controll Setting
	 - Application
		 - Designer
			 - Session
			 - Licence
		 - Libary
	 - DLL Preview - 사용자 확인
		 - Priview
		 - Session 
			 - [ ] FrameWork Session 가져오기 #obs 
- Belt
- Project Management
###### FormMain
- FormMain 실행
	- Form Basic Control 
	- CRUD
	- Interface Properties

##### Hide Sign In
#### 실패
##### 실패메시지
##### Hide Sign In
##### Program Close
### FormDesigner (FormMaker)
게시된 SELENE에서 업데이트 정보와 프로그램 메타데이터를 받아온다.

#### DLL
##### Form(DLL) Registration
###### DLL, Form Registration
###### DLL Load
###### DLL Preview
##### DLL Upload
###### DLL Version Check
###### FTP
### Business Forms - Module
Selene에서 다운받아서 수동으로 셋팅
- [ ]  Business Form 모듈화 #someday [link](https://todoist.com/app/task/7956083237) #todoist  %%[todoist_id:: 7956083237]%%
#### Controll Properties(User Controller)
#### WorkSet Properties
- CRUD
- Model
- Pull Parameters
- Push Parameters
- Save Reference
##### Field Set
- Field Properties
- Field Set Apply Value
##### Grid Set
- Columns Properties
##### Data Set(SQL Set)
- Query
- Field Set Apply Value - 개발할 건지 아직 
#### Function
##### CRUD, Print, Capture     
Project(FrameWork)의 세션  정보를 확인하고 로그인 처리한다. 
#### Astoid Sign In
### Belt
### Project Management
## FormMain
### FormMain
- [ ] Libaray List Check #obs
	- [ ] Lib Version Check #obs 
		- [ ] Download #obs
		- [ ] Update #obs
		- [ ] Excute #obs
- [ ] Version Check #obs
	- [ ] Update #obs
	- [ ] Excute #obs
### Business Forms
- [ ] Select Business From #obs
	- [ ] Request - Client에서 Mars에 요청한다.  #obs
	- [ ] Response - Mars가 응답한다. #obs
## User Controller

#### Prologue / Concept
`[Name][Version]` - UCText01000
#### Manifestation
[[Ctrl_Text Box]]
[[Ctrl_Date]]
[[Ctrl_Combo Box]]
[[Ctrl_Check Combo Box]]
[[Ctrl_Check Box]]
[[Ctrl_Radio Button]]
[[Ctrl_Panel]]
[[Ctrl_Button]]

[[Ctrl_Grid Set]]
	[[Ctrl_Standard Grid]]
	[[Ctrl_Pivot Grid]]
	[[Ctrl_Tree Grid]]
	
[[Ctrl_Field Set]]

[[Ctrl_Attachment Set]]
	[[Ctrl_Image]]
	[[Ctrl_File]]

