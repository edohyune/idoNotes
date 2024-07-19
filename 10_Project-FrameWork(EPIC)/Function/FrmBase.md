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

Save와 마찬가지로 this.Open(); 을 호출하면 해당 비지니스 폼에 등록된 모든 워크셋을 오픈한다.

Open에 포함되는 개념

1. Open순서

WorkSet은 WrkGet, WrkSet, WrkRef 때문에 연관되어 있다. 
따라서 WorkSet에는 순서가 있다. 마스터 데이터가 만들어지고 디테일 정보가 만들어져야한다. 

2. Open트리거

그리드의 경우 포커스로우가 변경되면 해당 정보를 기초로 다른 워크셋의 데이터를 재설정해줘야 한다. 
Open순서 중간에 정보변경이 있다면 변경내용 이후 순서의 워크셋은 다시 열어야 한다. 
WrkGet데이터를 기준으로 Get하는 데이터가 변경되면 해당 트리거가 발동 되어야 한다. Get은 그리드셋 필드셋 둘다 해당이 되는데 첨고하고 있는 데이터가 변경이 되면 자신과 이후의 WorkSet에 트리거를 발동 시킨다. 
WrkSet데이터를 기준으로 Set하는 데이터가 변경되면 해당 트리거가 발동한다. Set은 필드 대상이므로 해당 필드는 change Event에서 트리거를 발동시킨다. 


###### 동적 모델생성
모델의 변경감지()

#### Manifestation

#### Integration

###### REFERENCE
