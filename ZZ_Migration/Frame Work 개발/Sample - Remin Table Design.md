```SQL
CREATE TABLE [Reminder] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [Deleted] bigint DEFAULT(0) NOT NULL, 
  [Name] text NOT NULL, 
  [Date] text NOT NULL, 
  [RepeatType] text NOT NULL, 
  [Note] text NOT NULL, 
  [Enabled] bigint NOT NULL, 
  [DayOfMonth]bigint NULL, 
  [EveryXCustom] bigint NULL, 
  [RepeatDays] text NULL, 
  [SoundFilePath] text NULL, 
  [PostponeDate] text NULL, 
  [Hide] bigint DEFAULT(0) NULL, 
  [Corrupted] bigint DEFAULT(0) NULL, 
  [EnableAdvancedReminder] bigint DEFAULT(1) NOT NULL, 
  [UpdateTime] bigint DEFAULT(0) NOT NULL
)

CREATE TABLE [Settings] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [PopupType] text NOT NULL,  
  [StickyForm] bigint NOT NULL, 
  [EnableReminderCountPopup] bigint DEFAULT(1) NOT NULL, 
  [EnableHourBeforeReminder] bigint DEFAULT(1) NOT NULL, 
  [HideReminderConfirmation] bigint DEFAULT(0) NULL, 
  [EnableQuickTimer] bigint DEFAULT(1) NOT NULL, 
  [LastVersion] text NULL, 
  [DefaultTimerSound] text NULL, 
  [EnableAdvancedReminders] bigint NULL, 
  [UniqueString] text NULL,  
  [RemindMeTheme] text DEFAULT('Default') NOT NULL, 
  [DrawerUseColors] bigint DEFAULT(0) NULL, 
  [DrawerHighlight] bigint DEFAULT(1) NULL, 
  [DrawerBackground] bigint DEFAULT(0) NULL, 
  [CurrentTheme] bigint DEFAULT(-1) NULL,
  [MaterialDesign] bigint DEFAULT(1) NULL, 
  [AutoUpdate] bigint DEFAULT(1) NOT NULL, 
  [TimerVolume] bigint DEFAULT(100) NOT NULL
)

CREATE TABLE [Songs] ( 
  [Id] INTEGER NOT NULL, 
  [SongFileName]text NOT NULL, 
  [SongFilePath]text NOT NULL, 
  CONSTRAINT[sqlite_master_PK_Songs] PRIMARY KEY([Id])
)

CREATE TABLE [PopupDimensions] (
  [Id] INTEGER NOT NULL, 
  [FormWidth]bigint NOT NULL, 
  [FormHeight]bigint NOT NULL, 
  [FontTitleSize]bigint NOT NULL, 
  [FontNoteSize]bigint NOT NULL, 
  CONSTRAINT[sqlite_master_PK_PopupDimensions] PRIMARY KEY([Id])
)

CREATE TABLE[ListviewColumnSizes] (
  [Id]INTEGER NOT NULL, 
  [Title]bigint NOT NULL, 
  [Date]bigint NOT NULL, 
  [Repeating]bigint NOT NULL, 
  [Enabled]bigint NOT NULL, 
  CONSTRAINT[sqlite_master_PK_ListviewColumnSizes] PRIMARY KEY([Id])
)

CREATE TABLE [Hotkeys] (
  [Id] INTEGER NOT NULL, 
  [Name]text NOT NULL, 
  [Key]text NOT NULL, 
  [Modifiers]text NOT NULL, 
  CONSTRAINT[sqlite_master_PK_Hotkeys] PRIMARY KEY([Id])
)

CREATE TABLE [AdvancedReminderProperties] (
  [Id] INTEGER NOT NULL, 
  [Remid]bigint NOT NULL, 
  [BatchScript]text NULL, 
  [ShowReminder] bigint DEFAULT 1  NULL, 
  CONSTRAINT[sqlite_master_PK_AdvancedReminderProperties] PRIMARY KEY([Id])
)

CREATE TABLE [AdvancedReminderFilesFolders] (
  [Id] INTEGER NOT NULL, 
  [Remid]bigint NOT NULL, 
  [Path]text NOT NULL, 
  [Action]text NOT NULL, 
  CONSTRAINT[sqlite_master_PK_AdvancedReminderFilesFolders] PRIMARY KEY([Id])
)

CREATE TABLE [ReadMessages] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [ReadMessageId] bigint NOT NULL, 
  [ReadDate] text NOT NULL, 
  [MessageText] text NULL
)

CREATE TABLE [ButtonSpaces] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [Reminders] bigint DEFAULT(5) NOT NULL, 
  [Timer] bigint DEFAULT(5) NOT NULL, 
  [BackupImport] bigint DEFAULT(5) NOT NULL, 
  [Settings] bigint DEFAULT(5) NOT NULL, 
  [SoundEffects] bigint DEFAULT(5) NOT NULL, 
  [ResizePopup] bigint DEFAULT(5) NOT NULL, 
  [MessageCenter] bigint DEFAULT(5) NOT NULL, 
  [DebugMode] bigint DEFAULT(5) NOT NULL
)

CREATE TABLE [Themes] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [Primary] bigint DEFAULT(0) NOT NULL, 
  [DarkPrimary] bigint DEFAULT(0) NOT NULL, 
  [LightPrimary] bigint DEFAULT(0) NOT NULL, 
  [Accent] bigint DEFAULT(0) NOT NULL, 
  [TextShade] bigint DEFAULT(0) NOT NULL, 
  [Mode] bigint DEFAULT(0) NULL, 
  [ThemeName] text NULL, 
  [IsDefault] bigint DEFAULT(0) NOT NULL
)

CREATE TABLE [HttpRequests] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [reminderId] bigint NOT NULL, 
  [URL] text NOT NULL, 
  [Type] text NOT NULL, 
  [AcceptHeader] text NOT NULL, 
  [ContentTypeHeader] text NOT NULL, 
  [OtherHeaders] text NOT NULL, 
  [Body] text NOT NULL, 
  [Interval] bigint NOT NULL, 
  [AfterPopup] text DEFAULT('Stop') NOT NULL
)

CREATE TABLE [HttpRequestCondition] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
  [RequestId] bigint NOT NULL, 
  [Condition] text NOT NULL, 
  [DataType] text NOT NULL, 
  [Property] text NOT NULL, 
  [Operator] text NOT NULL, 
  [Value] text NOT NULL
)
```