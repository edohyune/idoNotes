---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: 2024-05-08 14:17
MDT: 2024-05-08 14:36
---
---
#### Prologue / Concept

#### Manifestation

```C#
using DevExpress.XtraEditors;

public Form1()
{
    InitializeComponent();

    TokenEdit tokenEdit = new TokenEdit();
    tokenEdit.Properties.Separators.Add(',');
    tokenEdit.Properties.Tokens.Add(new TokenEditToken("Tag1", "1"));
    tokenEdit.Properties.Tokens.Add(new TokenEditToken("Tag2", "2"));
    tokenEdit.Properties.Tokens.Add(new TokenEditToken("Tag3", "3"));
    tokenEdit.EditValueChanged += TokenEdit_EditValueChanged;

    this.Controls.Add(tokenEdit);
}

private void TokenEdit_EditValueChanged(object sender, EventArgs e)
{
    // 이벤트 핸들러 내에서 토큰의 변경 사항 처리
}

```

```SQL
CREATE TABLE Posts (
    PostID INT PRIMARY KEY,
    Author VARCHAR(100),
    PostDate DATETIME,
    Content TEXT
);

CREATE TABLE Hashtags (
    HashtagID INT PRIMARY KEY,
    TagText VARCHAR(50) UNIQUE
);

CREATE TABLE PostHashtags (
    PostID INT,
    PostType VARCHAR(50),
    HashtagID INT,
    FOREIGN KEY (HashtagID) REFERENCES Hashtags(HashtagID),
    PRIMARY KEY (PostID, PostType, HashtagID)
);

```
### **폴리모픽 연관(Polymorphic Association) 사용**
이 방법은 하나의 `PostHashtags` 테이블에서 여러 타입의 게시물을 참조할 수 있도록 합니다. 게시물 ID와 게시물 타입(예: 'Posts', 'Posts_other')을 저장하여 어떤 테이블의 게시물인지 구분할 수 있습니다.

```SQL
INSERT INTO Posts (Title, Content) VALUES ('Example Title', 'Example content');
INSERT INTO Tags (TagName) VALUES ('Technology');
INSERT INTO PostTags (PostID, TagID) VALUES (1, 1);  -- 여기서 1은 예시 ID

SELECT t.TagName
FROM Tags t
INNER JOIN PostTags pt ON t.TagID = pt.TagID
WHERE pt.PostID = 1;

SELECT p.Title, p.Content
FROM Posts p
INNER JOIN PostTags pt ON p.PostID = pt.PostID
INNER JOIN Tags t ON t.TagID = pt.TagID
WHERE t.TagName = 'Technology';

UPDATE Posts
SET Title = 'Updated Title', Content = 'Updated content'
WHERE PostID = 1;

UPDATE Tags
SET TagName = 'Tech'
WHERE TagID = 1;

DELETE FROM PostTags WHERE PostID = 1;
DELETE FROM Posts WHERE PostID = 1;

DELETE FROM PostTags WHERE TagID = 1;
DELETE FROM Tags WHERE TagID = 1;

```
#### Integration

###### REFERENCE
