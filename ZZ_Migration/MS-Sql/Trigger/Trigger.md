  

[[Closing]]

[[Summarize to Other Table]]

[[Make the History Table]]

[[Maintain referential relationships with other tables]]

```SQL
--Trigger 생성
create TRIGGER [dbo].[TriggerName] ON [dbo].[TableName]
for insert, update, delete
as
begin 
    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

    declare @action char(1)
    set @action = 'I'

    if exists(select * from deleted)
    begin
        set @action = (case when exists(select * from inserted) then 'U' else 'D' End)
    end

    If @action = 'I'
    begin
        set @action = 'I'
    end else If @action = 'U'
    begin 
        set @action = 'U'
    end else If @action = 'D'
    begin
        set @action = 'D'
    end
end;
```