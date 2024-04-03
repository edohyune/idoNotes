```SQL
D:\BJC_DEV\FC\FC700R2

-- Grid Copy
exec	System_Copy_WorkSet_In_DB	
		'LCC820',
		'lcc820_g03',
		'LCCSLE',
		'lccsle_g03',
		'Collection',
		'Grid',
		'g03'

--SQL Copy
exec	System_Copy_WorkSet_In_DB	
		'LCC820',
		'lcc820_setting',
		'LCCSLE',
		'lccsle_setting',
		'setting warehouse',
		'SQL',
		''

--FreeForm
exec	System_Copy_WorkSet_In_DB	
		'LCC820',
		'lcc820_frm',
		'LCCSLE',
		'lccsle_frm',
		'Free form',
		'FreeForm',
		''
```