---
Title: Server Connection Template
Owner: Server
Type: Connection
IP: 192.168.1.9
Port: 
ID: sa
Password: Password01
Discriptions:
---
---

```
$"Data Source=192.168.1.9;" + //$"Data Source={Common.gSVR};" +
$"Initial Catalog=SELENE;" + //$"Initial Catalog={Common.gSolution};" +
$"Persist Security Info=True;" +
$"User ID=sa;" +
$"Password=Password01";
```