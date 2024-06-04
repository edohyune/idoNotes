---
Start Date: 2024-06-03
Status: Done
Concept: true
Manifestation: true
Integration: true
Done: 2024-06-03
tags: 
CDT: 2024-06-03 21:07
MDT: 2024-06-03 21:08
---
---
#### Prologue / Concept

#### Manifestation

#### Integration
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class AaaAaa : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }
    }
    public interface IAaaAaaRepo
    {
        List<AaaAaa> GetAaaAaas(string frwId, string cd);
        AaaAaa GetAaaAaa(string frwId, string cd, string refNo);
        void Add(AaaAaa aaaAaa);
        void Update(AaaAaa aaaAaa);
        void Delete(AaaAaa aaaAaa);
    }
    public class AaaAaaRepo : IAaaAaaRepo
    {
        public List<AaaAaa> GetAaaAaas(string frwId, string cd)
        {
            string sql = @"
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<AaaAaa>(sql, new { FrwId = frwId, Cd = cd }).ToList();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in result)
                    {
                        item.ChangedFlag = MdlState.None;
                    }
                    return result;
                }
            }
        }
        public AaaAaa GetAaaAaa(string frwId, string cd, string refNo)
        {
            string sql = @"
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<AaaAaa>(sql, new { FrwId = frwId, Cd = cd, refNo = refNo }).SingleOrDefault();

                if (result == null)
                {
                    return null;
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }
        public void Add(AaaAaa aaaAaa)
        {
            string sql = @"
       " + Common.gRegId + @", getdate(), " + Common.gRegId + @", getdate()

";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
        public void Update(AaaAaa aaaAaa)
        {
            string sql = @"
       MId= "" + Common.gRegId + @"",
       MDt= getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
        public void Delete(AaaAaa aaaAaa)
        {
            string sql = @"
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, aaaAaa);
            }
        }
    }
}

```
###### REFERENCE
