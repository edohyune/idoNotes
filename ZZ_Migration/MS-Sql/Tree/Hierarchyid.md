자식 노드의 갯수 - 자신을 포함,

```SQL
select count(*) 
  from COAMST 
 where buId=a.buId and HID.IsDescendantOf(b.HID)=1
   and HID <> b.HID --자신을 불포함

select PID=HID.GetAncestor(1)
select CID=HID.GetAncestor(0)
```