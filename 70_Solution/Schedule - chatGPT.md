  

### Create a schedule management program table with MSSQL

This table, named "Schedule," has the following columns:  
Here is an example of a table for a schedule management program in MSSQL:  

```SQL
CREATE TABLE Schedule (
  ScheduleID INT PRIMARY KEY IDENTITY(1,1),
  StartTime DATETIME NOT NULL,
  EndTime DATETIME NOT NULL,
  Activity VARCHAR(100) NOT NULL,
  Location VARCHAR(100) NOT NULL,
  Notes VARCHAR(500)
);
```

  
  
`**ScheduleID**`: An auto-incrementing primary key that uniquely identifies each schedule entry.  
  
`**StartTime**`: The starting time of the schedule entry.  
  
`**EndTime**`: The ending time of the schedule entry.  
  
`**Activity**`: A brief description of the activity for the schedule entry.  
  
`**Location**`: The location where the activity will take place.  
  
`**Notes**`: Optional notes for the schedule entry.  
  
This table is designed to store scheduling information for events, appointments, or other activities. The  
`**StartTime**` and `**EndTime**` columns use the `**DATETIME**` data type to store the date and time of each event. The other columns use the `**VARCHAR**` data type to store text information, such as the activity description, location, and any notes.

### Recursion Function in MSSQL

Recursion in MSSQL is the process of defining a function that calls itself in a repeated manner to accomplish a specific task. This technique is used to traverse hierarchical data structures such as trees or graphs, or to perform operations such as calculating factorials, generating fibonacci series, or finding all possible combinations of elements in a set.

Here is an example of a recursive function in MSSQL that calculates the factorial of a number:

```SQL
CREATE FUNCTION dbo.Factorial (@number INT)
RETURNS INT
AS
BEGIN
    DECLARE @result INT;
    SET @result = 1;

    IF (@number > 1)
        SET @result = @number * dbo.Factorial(@number - 1);

    RETURN @result;
END;
```

This function can be called by passing an integer argument:

```SQL
SELECT dbo.Factorial(5);
```

The result of this query will be 120, which is the factorial of 5.

It is important to note that while recursion can be a powerful tool, it can also consume a lot of system resources and cause performance issues if not used carefully. It is important to ensure that the recursion has a well-defined termination condition, and to limit the depth of recursion to prevent stack overflow errors.

  

### 조직TABLE

```SQL
CREATE TABLE [dbo].[WBSMST](
CID varchar(20) NOT NULL,
PID varchar(20) NULL,
ordSq varchar(10)
);
```

아래 부서 조회

```SQL
;With x as
(select CID, PID, ordSq, lvl=0, HID=convert(varchar(max),concat(ROW_NUMBER() over(order by ordSq),'/'))
   from WBSMST
  where CID in (@CID)
  union all
 select a.CID, a.PID, a.ordSq, lvl=x.lvl+1, 
        HID=cast(concat(x.HID,ROW_NUMBER() over(partition by x.lvl order by a.ordSq),'/') as varchar(max))
   from x
   join WBSMST a on x.CID=a.PID
) select x.CID, x.PID, x.ordSq, x.lvl, x.HID
    from x
   order by HID
```

  

In this example, the CTE SubDepartments is defined to select the row with the given orgId from the ORG100 table and all of its sub-departments by joining the ORG100 table with itself recursively. The final SELECT statement retrieves the orgId of all departments in the SubDepartments CTE.

상위부서조회

```SQL
CREATE FUNCTION dbo.GetParents (@orgId BIGINT)
RETURNS @ResultTable TABLE (
    orgId BIGINT
)
AS
BEGIN
    DECLARE @pid BIGINT;

    SELECT @pid = orgPid FROM ORG100 WHERE orgId = @orgId;

    WHILE (@pid IS NOT NULL)
    BEGIN
        INSERT INTO @ResultTable (orgId)
        SELECT @pid;

        SELECT @pid = orgPid FROM ORG100 WHERE orgId = @pid;
    END;

    RETURN;
END;
```

SELECT orgId  
FROM dbo.GetParents(@orgId);  

  

### Schedule

**CronMaker를 이용하여 스케쥴을 등록할것.**

![[58._Quartz_Scheduler_(%EC%BF%BC%EC%B8%A0_%EC%8A%A4%EC%BC%80%EC%A4%84%EB%9F%AC).pdf]]

  

Here's a sample table design for a schedule management program:

|   |   |   |   |
|---|---|---|---|
|**Column Name**|**Data Type**|**Constraints**|**Description**|
|Schedule ID|Integer|Primary Key|Unique identifier for each schedule|
|Schedule Type|Varchar|Not Null|Specifies whether the schedule is a one-time or recurring schedule|
|Start Time|Datetime|Not Null|The start time of the schedule|
|End Time|Datetime|Not Null|The end time of the schedule|
|Description|Varchar||A brief description of the schedule|
|Recurrence Pattern|Varchar||Specifies the recurrence pattern if the schedule is recurring (e.g. daily, weekly, monthly, etc.)|
|Repeat Until|Datetime||The end date of the recurring schedule, if specified|

The `**Schedule Type**` column can have two possible values: "One-time" or "Recurring". If the schedule is recurring, the `**Recurrence Pattern**` and `**Repeat Until**` columns will have values, otherwise they will be left blank. The `**Start Time**` and `**End Time**` columns specify the time frame for the schedule. The `**Description**` column can be used to add additional information about the schedule.

```SQL
CREATE TABLE schedules (
  ScheduleID INT PRIMARY KEY,
  ScheduleType VARCHAR(10) NOT NULL,
  ScheduleOwner VARCHAR(10) NOT NULL,
  StartTime DATETIME NOT NULL,
  EndTime DATETIME NOT NULL,
  Description VARCHAR(255),
  RecurrencePattern VARCHAR(10),
  RepeatUntil DATETIME
);
INSERT INTO schedules (ScheduleID, ScheduleType, ScheduleOwner, StartTime, EndTime, Description, RecurrencePattern, RepeatUntil)
VALUES (1, 'One-time', 'IDO', '2023-02-20 09:00:00', '2023-02-20 11:00:00', 'Meeting with CEO');

INSERT INTO schedules (ScheduleID, ScheduleType, ScheduleOwner, StartTime, EndTime, Description, RecurrencePattern, RepeatUntil)
VALUES (2, 'Recurring', 'IDO', '2023-02-06 07:00:00', '2023-02-06 09:00:00', 'Weekly team meeting', 'Weekly', '2023-12-31');

CREATE TABLE calendar (
  CalendarDate DATE PRIMARY KEY,
  DayOfWeek VARCHAR(9),
  Month VARCHAR(9),
  Year INT
);
```

How should I code the SQL to show 'Recurrence Pattern' on the calendar? For example, if you register a schedule called 'Meeting every Monday at 7:00’

```SQL
SELECT a.CalendarDate, a.DayOfWeek, a.StartTime, a.Description, 
       b.RecurrencePattern
  FROM calendar a
  JOIN schedules b on a.CalendarDate = DATE(b.StartTime)
 WHERE schedules.RecurrencePattern = 'Weekly'
   AND DAYNAME(calendar.CalendarDate) = 'Monday';
```