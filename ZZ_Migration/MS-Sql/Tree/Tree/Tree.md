With

```SQL
;With x as 
    (select CID, PID, lvl=2, 
            HID=cast(concat('/',@intbuId,'/',@intyymm,'/') as varchar(max))
            --HID=cast('/' as varchar(max))
       from SLPTRI
      where buId=@buId
        and yymm=@yymm
        and isnull(PID,'')=''
      union all
     select a.CID, a.PID, lvl=x.lvl+1, 
            HID=cast(concat(x.HID,ROW_NUMBER() over(partition by x.lvl order by a.CID),'/') as varchar(max))
       from x
       join SLPTRI a on x.CID=a.PID
      where a.buId=@buId
        and a.yymm=@yymm
    ) select x.CID, x.PID, x.lvl, x.HID
        from x
       order by HID
```

  

[[ZZ_Migration/MS-Sql/Tree/Tree/hierarchyid]]