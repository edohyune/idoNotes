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


Class FrmWrk - Table FRMWRK
WorkSet 속성

Class WrkFld - Table WRKFLD
컬럼, 컨트롤러 속성

Class Diagram
#### Manifestation

#### Integration


│ ├── Interfaces 
│       ├── IWorkSet.cs 
│       ├── IGridSet.cs 
│       ├── IFieldSet.cs 
│       ├── ITextBased.cs 
│       ├── IAlignment.cs 
│       ├── ISizeable.cs 
│       └── ETC.
│ ├── Models 
│       ├── WrkFld.cs 
│       ├── Column.cs 
│       └── Field.cs 
│ ├── Repositories 
│       ├── IWrkFldRepo.cs 
│       └── WrkFldRepo.cs 
└── WorkSets 
│       ├── GridWorkSet.cs 
│       ├── DataWorkSet.cs 
│       └── FieldWorkSet.cs
└── Controls
    ├── UCTextBox.cs
    └── UCCheckBox.cs



###### REFERENCE
