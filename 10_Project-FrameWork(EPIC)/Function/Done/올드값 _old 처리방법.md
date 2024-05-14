---
Start Date: 2024-05-09
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-05-09
tags: 
CDT: 2024-05-07 19:56
MDT: 2024-05-09 13:19
---
---
#### Prologue / Concept
올드 값이 필요한 경우 
'[field_name] _ old를 만들어 처리할것`
#### Manifestation
기술적으로 리플렉션을 통해 Select할때마다 `_old`개체를 생성해야한다. 
#### Integration
`_old`개체는 GridSet과 FieldSet에서 필요한데, 
필요한 경우 Select에 포함하여 처리한다.
```
select Id, Id_old
  from TEST
```

- [ ] 리플렉션을 이용한 `_old`개체를 동적으로 생성한다. #someday [link](https://todoist.com/app/task/7974280580) #todoist %%[todoist_id:: 7974280580]%% 
###### REFERENCE

[리플렉션을 이용한 `_old`개체 동적생성](https://chat.openai.com/share/c807fad8-ca03-485c-b46c-254d7d75ab90)


