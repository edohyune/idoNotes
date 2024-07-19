#### RegexStr
```C#
namespace Lib
{
    public enum RegexStr
    {
        OPattern,
        DPattern,
        GPattern,
        CPattern
    }
    public static class RegexStrs
    {
        public static readonly Dictionary<RegexStr, string> Lists = new Dictionary<RegexStr, string>
        {
            { RegexStr.OPattern, @"@\w+" },
            { RegexStr.DPattern, @"@_\w+" },
            { RegexStr.GPattern, @"<\$(\w+)>" },
            { RegexStr.CPattern, @"andif\s+(.+?)\s+endif" }
        };
    }

    public enum LookUpType
    {
        None,
        Code,
        SubCode,
        PopUp
    }
    public enum MdlState
    {
        None,
        Inserted,
        Updated,
        Deleted
    }
    public enum UserType
    {
        None = 0,
        RealUser = 1,
        DeputyUser = 2
    }
    public enum UserClass
    {
        SuperVisorOfGAIA,
        GAIADEV,
        Tourist
    }
    public enum FrmType
    {
        Development, 
        operation
    }
}
```
#### DynamicModel
```C#
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Lib
{
    public class DynamicModel : DynamicObject, INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public MdlState ChangedFlag { get; set; } = MdlState.Inserted;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value)) return false;

            backingField = value;
            if (this.ChangedFlag != MdlState.Inserted)
            {
                this.ChangedFlag = MdlState.Updated;
            }
            OnPropertyChanged(propertyName);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_properties.TryGetValue(binder.Name, out var existingValue))
            {
                if (Equals(existingValue, value)) return true;
            }

            _properties[binder.Name] = value;
            if (this.ChangedFlag != MdlState.Inserted)
            {
                this.ChangedFlag = MdlState.Updated;
            }
            OnPropertyChanged(binder.Name);
            return true;
        }

        public void SetDynamicProperty(string propertyName, object value)
        {
            if (_properties.ContainsKey(propertyName))
            {
                _properties[propertyName] = value;
            }
            else
            {
                _properties.Add(propertyName, value);
            }
            if (this.ChangedFlag != MdlState.Inserted)
            {
                this.ChangedFlag = MdlState.Updated;
            }
            OnPropertyChanged(propertyName);
        }

        public object GetDynamicProperty(string propertyName)
        {
            return _properties.TryGetValue(propertyName, out var value) ? value : null;
        }
                // 새롭게 추가된 메서드: 모든 동적 속성을 가져오는 메서드
        public IDictionary<string, object> GetDynamicProperties()
        {
            return _properties;
        }
    }
}

```
#### MdlBase
```C#
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lib
{
    public class MdlBase : INotifyPropertyChanged
    {
        //[Display(AutoGenerateField = false)]
        public MdlState ChangedFlag { get; set; } = MdlState.Inserted;

        private int _CId;
        //[Display(AutoGenerateField = false)]
        public int CId
        {
            get => _CId;
            set => Set(ref _CId, value);
        }
        private DateTime _CDt;
        //[Display(AutoGenerateField = false)]
        public DateTime CDt
        {
            get => _CDt;
            set
            {
                if (value == default)
                    throw new ArgumentException("Creation date cannot be default.");
                Set(ref _CDt, value);
            }
        }


        private int _MId;
        //[Display(AutoGenerateField = false)]
        public int MId
        {
            get => _MId;
            set => Set(ref _MId, value);
        }

        private DateTime _MDt;
        //[Display(AutoGenerateField = false)]
        public DateTime MDt
        {
            get => _MDt;
            set
            {
                if (value == default)
                    throw new ArgumentException("Modification date cannot be default.");
                Set(ref _MDt, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Set<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            backingField = value;
            if (this.ChangedFlag != MdlState.Inserted)
            {
                this.ChangedFlag = MdlState.Updated;
            }
            OnPropertyChanged(propertyName);
        }
    }
}

```
#### GenFunc
```C#
using Lib.Repo;
using System.Text.RegularExpressions;

namespace Lib
{
    public static class GenFunc
    {
        #region Conversion --------------------------------------------------------
        public static int ToInt(this string value, int defaultValue = 0)
        {
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        public static bool ToBool(this string value, bool defaultValue = false)
        {
            return bool.TryParse(value, out bool result) ? result : defaultValue;
        }
        public static DateTime ToDateTime(this string value, DateTime defaultValue)
        {
            return DateTime.TryParse(value, out DateTime result) ? result : defaultValue;
        }

        public static DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode StrToSelectMode(string selectMode)
        {
            DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode mode = new DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode();
            switch (selectMode)
            {
                case "RowSelect": mode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; break;
                case "CheckBoxRowSelect": mode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect; break;
                //case "CellSelect": mode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect; break;
                default:
                    mode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect; 
                    break;
            }
            return mode;

        }
        public static DevExpress.Utils.HorzAlignment StrToAlign(string align)
        {
            DevExpress.Utils.HorzAlignment horz = new DevExpress.Utils.HorzAlignment();
            switch (align)
            {
                //case "0": horz = DevExpress.Utils.HorzAlignment.Default; break;
                case "1": horz = DevExpress.Utils.HorzAlignment.Near; break;
                case "2": horz = DevExpress.Utils.HorzAlignment.Center; break;
                case "3": horz = DevExpress.Utils.HorzAlignment.Far; break;
                //case "Default": horz = DevExpress.Utils.HorzAlignment.Default; break;
                case "Left": horz = DevExpress.Utils.HorzAlignment.Near; break;
                case "Center": horz = DevExpress.Utils.HorzAlignment.Center; break;
                case "Right": horz = DevExpress.Utils.HorzAlignment.Far; break;
                default:
                    horz = DevExpress.Utils.HorzAlignment.Default;
                    break;
            }
            return horz;
        }
        public static System.Drawing.Color StrToColor(string colorName)
        {
            return System.Drawing.Color.FromName(colorName);
        }
        #endregion
        #region Null Process ------------------------------------------------------
        public static bool IsNull(this string a) => string.IsNullOrWhiteSpace(a);
        //if(value1.IsNull()) value1 = value2;
        public static string IsNull(this string a, string b) => string.IsNullOrWhiteSpace(a) ? b : a;
        //string val = value1.IsNull("Default Value")
        #endregion
        #region Replace Syntax Patten ---------------------------------------------
        public static string ReplaceGPatternQuery(string input)
        {
            var components = Lib.Syntax.SyntaxExtractor.ExtractPattern(input, match => match.GPatternMatch);

            foreach (var component in components)
            {
                string componentNm = component.Key;

                string pattern = RegexStrs.Lists[RegexStr.GPattern];//@"<\$(.*?)>";

                Match match = Regex.Match(componentNm, pattern);

                if (match.Success)
                {
                    string gVariableValue = Common.GetValue(match.Groups[1].Value); // Lib.Common에서 값 가져오기
                    //input = input.Replace(component.ToString(), gVariableValue);
                    input = input.Replace(componentNm, $"'{gVariableValue}'");
                }
                else
                {
                    input = input.Replace(componentNm, @"''");
                }
            }
            return input;
        }
        public static string ReplaceGPatternWord(string input)
        {
            string pattern = RegexStrs.Lists[RegexStr.GPattern];//@"<\$(.*?)>";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string gVariableValue = Common.GetValue(match.Groups[1].Value); // Lib.Common에서 값 가져오기
                input = input.Replace(input, $"'{gVariableValue}'");
            }
            else
            {
                input = input.Replace(input, @"''");
            }
            return input;
        }
        #endregion
        #region GetSql() GetPopUpSql()---------------------------------------------
        public static WrkSql GetSql(string frwId, string frmId, string wrkId, string crudm)
        {
            string sql = @"
select a.Query
  from WRKSQL a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
   and a.CRUDM = @CRUDM
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkSql>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId, CRUDM = crudm }).SingleOrDefault();

                if (result == null)
                {
                    result = new WrkSql();
                    result.Query = "";
                    return result;
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }

        public static string GetSql(object param)
        {
            string sql = @"
select a.Query
  from WRKSQL a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkId = @WrkId
   and a.CRUDM = @CRUDM
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.OpenQuery(sql, param);
                if (result == null)
                {
                    return "";
                }
                else
                {
                    return result;
                }
            }
        }
        public static string GetPopSql(object param)
        {
            string sql = @"
select a.Query
  from POPSQL a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.PopId = @PopId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.OpenQuery(sql, param);
                if (result == null)
                {
                    return "";
                }
                else
                {
                    return result;
                }
            }
        }
        #endregion
        #region GetIni(), SetIni() ------------------------------------------------
        public static void SetIni(string key, string value = null)
        {
            string iniFilePath = Common.GetValue("gIniFilePath");
            if (string.IsNullOrEmpty(iniFilePath))
            {
                throw new Exception("환경설정 파일이 지정되지 않았습니다.");
            }

            Dictionary<string, string> settings = new Dictionary<string, string>();
            if (File.Exists(iniFilePath))
            {
                var lines = File.ReadAllLines(iniFilePath);
                foreach (var line in lines)
                {
                    var keyValue = line.Split(new char[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        settings[keyValue[0]] = keyValue[1];
                    }
                }
            }

            // 새로운 값으로 설정을 업데이트하거나 추가합니다.
            settings[key] = value;

            string directoryPath = Path.GetDirectoryName(iniFilePath);

            // 해당 폴더가 존재하지 않으면 생성합니다.
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // 설정을 파일에 다시 씁니다.
            using (var sw = new StreamWriter(iniFilePath))
            {
                foreach (var setting in settings)
                {
                    sw.WriteLine($"{setting.Key}={setting.Value}");
                }
            }
        }

        public static string GetIni(string settingName)
        {
            string iniFilePath = Common.GetValue("gIniFilePath");
            // 파일이 존재하지 않으면 null을 반환
            if (!File.Exists(iniFilePath))
            {
                return null;
            }

            // 파일에서 모든 라인을 읽어온다
            var lines = File.ReadAllLines(iniFilePath);
            foreach (var line in lines)
            {
                // 라인을 '=' 기준으로 나눈다
                var keyValue = line.Split(new char[] { '=' }, 2);
                if (keyValue.Length == 2)
                {
                    // 첫 번째 파트가 설정 이름과 일치하면, 두 번째 파트(값)를 반환
                    if (string.Equals(keyValue[0].Trim(), settingName, StringComparison.OrdinalIgnoreCase))
                    {
                        return keyValue[1].Trim();
                    }
                }
            }

            // 설정 이름에 해당하는 값이 없으면 null을 반환
            return null;
        }
        #endregion

        //문자열 함수 
        public static string GetLastSubstring(string input, char delimiter)
        {
            string[] parts = input.Split(delimiter);
            return parts[^1]; // C# 8.0 이상에서 사용 가능한 인덱스 from end 연산자
        }
        //Form NameSpace를 FrwFrm과 FrmWrk에서 가져오는 함수 
        public static string GetFormNamespace(string frwId, string frmId)
        {
            FrwFrm frwFrm = new FrwFrmRepo().GetByFrmId(frwId, frmId);
            if (frwFrm == null)
            {
                return "";
            }
            return frwFrm.NmSpace;
        }
    }
}


```
#### GaiaHelper
```C#
using Dapper;
using DevExpress.CodeParser;
using DevExpress.Data.Filtering.Helpers;
using Lib.Repo;
using Lib.Syntax;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib
{
    public class GaiaHelper : IDisposable
    {
        private static readonly Lazy<GaiaHelper> _instance = new Lazy<GaiaHelper>(() => new GaiaHelper(), true);
        public static GaiaHelper Instance => _instance.Value;

        private readonly string _connectionString;
        //MSSQL - If USE Other DB. Using(IDbConnection, IDbTransaction) 
        private SqlConnection? _conn;
        private SqlTransaction? _tran;

        private bool _disposed = false;

        static GaiaHelper()
        {
            // BoolCharTypeHandler 클래스가 ITypeHandler 인터페이스를 구현한다고 가정
            //SqlMapper.AddTypeHandler(typeof(bool), new BoolCharTypeHandler());
        }

        public GaiaHelper()
        {
            _connectionString = $"Data Source=192.168.1.2;" + //$"Data Source={Common.gSVR};" +
                                $"Initial Catalog=GAIAV00;" + //$"Initial Catalog={Common.gSolution};" +
                                $"Persist Security Info=True;" +
                                $"User ID=sa;" +
                                $"Password=Password01";
            _conn = new SqlConnection(_connectionString);
        }

        public void BeginTransaction()
        {
            //To Do : _conn null check
            //if (_tran == null) { }
            //if (_conn == null) { }
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            _tran = _conn.BeginTransaction();
        }

        public void Commit()
        {
            _tran.Commit();
            _conn.Close();
            _tran = null;
        }

        public void RollBack()
        {
            _tran.Rollback();
            _conn.Close();
            _tran = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    _tran?.Dispose();
                    _conn?.Dispose();
                }

                // Set large fields to null.
                _tran = null;
                _conn = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #region Dapper QueryExecute
        //1. 쿼리 실행 및 단일 결과 반환
        //단일 행 쿼리 및 결과 반환(IDictionary 사용)
        public IDictionary<string, object> QueryKeyValue(string sql, object param=null)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return SqlMapper.Query(_conn, sql, param, _tran).Select(x => x as IDictionary<string, object>).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }
        public IDictionary<string, string> QueryDictionary(string sql, object param = null)
        {
            try
            {
                var result = SqlMapper.Query(_conn, sql, null, _tran)
                                      .Select(row => (IDictionary<string, object>)row)
                                      .FirstOrDefault();

                if (result != null)
                {
                    return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty);
                }

                return new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        //2. 쿼리 실행 및 다중 결과 반환
        //동적 타입 결과 반환
        public IEnumerable<dynamic> Query(string sql, object param)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return SqlMapper.Query(_conn, sql, param, _tran);
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        //제네릭 타입 결과 반환(파라미터 없음)
        public List<T> Query<T>(string sql)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, null);
                return SqlMapper.Query<T>(_conn, sql, null, _tran).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }
        //제네릭 타입 결과 반환 (오브젝트 파라미터)
        public List<T> Query<T>(string sql, object param)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return SqlMapper.Query<T>(_conn, sql, param, _tran).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }
        //제네릭 타입 결과 반환 (DynamicParameters 파라미터)
        public List<T> Query<T>(string sql, DynamicParameters param)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return SqlMapper.Query<T>(_conn, sql, param, _tran).ToList();

            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        //3. 쿼리 실행(업데이트/인서트/삭제)
        //실행 후 영향 받은 행의 수 반환(파라미터 없음)
        public int OpenExecute(string sql)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, null);
                return SqlMapper.Execute(_conn, sql, null, _tran);
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }

        }
        //실행 후 영향 받은 행의 수 반환 (오브젝트 파라미터)
        public int OpenExecute(string sql, object param)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return SqlMapper.Execute(_conn, sql, param, _tran);

            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        //4. 스칼라 값 반환 쿼리 실행
        //단일 값 반환(파라미터 없음)
        public string OpenQuery(string sql, object param = null)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, param);
                return _conn.ExecuteScalar<string>(sql, param, _tran);
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        public T OpenQuery<T>(string sql, object param = null)
        {
            try
            {
                sql = GenFunc.ReplaceGPatternQuery(sql);
                sql = ProcessQuery(sql, null);
                return _conn.ExecuteScalar<T>(sql, param, _tran);
            }
            catch (Exception ex)
            {
                LogException(ex, sql);
                throw;
            }
            finally
            {
                if (Common.gTrackMsg)
                {
                    string debugSql = GenerateDebugSql(sql, param);
                    Common.gMsg = "Debug SQL: " + Environment.NewLine + debugSql;
                }
                _conn.Close();
            }
        }

        public DataSet GetGridColumns(object param)
        {
            var pullFlds = new WrkGetRepo().GetPullFlds(param);
            //WrkGetRepo wrkGetRepo = new WrkGetRepo();
            //var paramlist = wrkGetRepo.GetPullFlds(prm);
            string[] fld = (from a in pullFlds
                            orderby a.Id
                            select a.Id).ToArray();
            string[] val = (from a in pullFlds
                            orderby a.Id
                            select a.Nm).ToArray();

            DataSet dataSet = GetSelectSql(param, fld, val);

            return dataSet;
        }

        private DataSet GetSelectSql(object prm, string[] param, string[] value)
        {
            DataSet dsSelect = new DataSet();

            string sqltxt = GenFunc.GetSql(prm);
            sqltxt = ReplaceOPatternMatch(sqltxt, param, value);
            sqltxt = RemoveConditionalClauses(sqltxt);

            SqlConnection SqlCon = new SqlConnection(_connectionString);
            SqlCommand SqlCmd = new SqlCommand(sqltxt, SqlCon);

            for (int i = 0; i <= param.Length - 1; i++)
            {
                if (param[i] != null && value[i] != null)
                {
                    SqlCmd.Parameters.AddWithValue(param[i], value[i]);
                }
                else
                {
                    Common.gMsg = $"Parameter {param[i]} or its value is null";
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(SqlCmd);
            adapter.SelectCommand = SqlCmd;
            adapter.Fill(dsSelect);
            return dsSelect;
        }
        #endregion


        #region About Log
        private string GenerateDebugSql(string sql, object param = null)
        {
            StringBuilder debugSql = new StringBuilder(sql);

            if (param == null) return debugSql.ToString();

            foreach (var prop in param.GetType().GetProperties())
            {
                string placeholder = "@" + prop.Name;
                string value = prop.GetValue(param)?.ToString();

                if (prop.PropertyType == typeof(string))
                {
                    value = $"'{value}'";
                }

                debugSql.Replace(placeholder, value);
            }
            return debugSql.ToString();
        }
        private void LogException(Exception ex, string sql = null)
        {
            Lib.Common.gMsg = $"Exception : {ex}";
            Lib.Common.gMsg = $"Query : {sql}";
        }
        #endregion

        #region SQL Query Replace
        private string ProcessQuery(string sql, object param)
        {
            if (param != null)
            {
                sql = ReplaceConditionalClauses(sql, param);
            }
            sql = ReplaceGVariables(sql);
            return sql;
        }

        public string ReplaceGVariables(string sql)
        {
            var gVariables = GetGVariables();
            foreach (var variable in gVariables)
            {
                sql = sql.Replace(variable.Key, variable.Value);
                //sql = Regex.Replace(sql, Regex.Escape(variable.Key), variable.Value, RegexOptions.IgnoreCase);
            }
            return sql;
        }
        private string ReplaceOPatternMatch(string sql, string[] param, string[] value)
        {
            SyntaxExtractor extractor = new SyntaxExtractor();
            SyntaxMatch variables = extractor.ExtractVariables(sql);

            //OPatternMatch에 있는 변수에 value값을 넣는다.
            for (int i = 0; i <= param.Length - 1; i++)
            {
                if (variables.OPatternMatch.ContainsKey(param[i]))
                {
                    sql = sql.Replace(param[i], string.IsNullOrWhiteSpace(value[i]) ? "''" : $"'{value[i]}'");
                }
            }
            //OPatternMatch중에서 value가 null인 경우 sql에서 ''으로 대체
            foreach (var item in variables.OPatternMatch)
            {
                sql = sql.Replace(item.Key, string.IsNullOrWhiteSpace(item.Value) ? "''" : $"'{item.Value}'");
            }
            
            return sql;
        }
        private string RemoveConditionalClauses(string sql)
        {
            var conditionalPattern = new Regex(@"andif\s+(.+?)\s+endif");
            //var conditionalPattern = new Regex(@"andif\s+(.+?)\s+endif", RegexOptions.IgnoreCase);
            return conditionalPattern.Replace(sql, string.Empty);
        }

        private string ProcessGPatternMatch(string input)
        {
            var matches = Regex.Matches(input, @"<\$(\w+)>");

            foreach (Match match in matches)
            {
                string variableName = match.Groups[1].Value;
                string variableValue = Lib.Common.GetValue(variableName); // Lib.Common에서 값 가져오기

                input = input.Replace(match.Value, variableValue);
            }
            
            return input;
        }

        private Dictionary<string, string> GetGVariables()
        {
            var globalVariables = new Dictionary<string, string>();
            var fields = typeof(Common).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                var value = field.GetValue(null)?.ToString();
                if (value != null)
                {
                    globalVariables.Add($"<${field.Name}>", value);
                }
            }
            return globalVariables;
        }
        private string ReplaceConditionalClauses(string sql, object param)
        {
            var conditionalPattern = new Regex(@"andif\s+(.+?)\s+endif");
            //var conditionalPattern = new Regex(@"andif\s+(.+?)\s+endif", RegexOptions.IgnoreCase);
            var dynamicParams = param as DynamicParameters;
            var paramNames = dynamicParams?.ParameterNames.ToHashSet(StringComparer.OrdinalIgnoreCase)
                             ?? param.GetType().GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            return conditionalPattern.Replace(sql, match =>
            {
                var condition = match.Groups[1].Value;
                var paramName = Regex.Match(condition, @"@\w+").Value;

                if (paramNames.Contains(paramName.Trim('@')))
                {
                    return $"AND {condition}";
                }

                return string.Empty;
            });
        }
        #endregion
    }
}
```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Common
    {
        #region About GPatternVariable ----------------------------------------------------------
        #endregion
        private static Dictionary<string, string> commonValues = new Dictionary<string, string>();
        private static DateTime lastCacheUpdateTime;
        private static TimeSpan cacheDuration = TimeSpan.FromMinutes(5); // 캐시 유효 기간 설정

        // 공통 변수 전체를 가져오는 메서드
        public static Dictionary<string, string> GetAllValues()
        {
            if (commonValues.Count == 0 || DateTime.Now - lastCacheUpdateTime > cacheDuration)
            {
                RefreshCache(); // 캐시가 비어있거나 유효 기간이 지난 경우 새로 고침
            }
            return new Dictionary<string, string>(commonValues);
        }

        private static void InitGlobalVariable()
        {
            commonValues["gId"] = string.Empty;
            commonValues["gRegId"] = string.Empty;
            commonValues["gNm"] = string.Empty;
            commonValues["gCls"] = string.Empty;
            commonValues["gFrameWorkId"] = string.Empty;
            commonValues["gUserProfilePath"] = string.Empty;
            commonValues["gIniFilePath"] = string.Empty;
            commonValues["gOpenFrm"] = string.Empty;
            commonValues["today"] = DateTime.Today.ToString("d");
            // 필요한 경우 여기에서 DB나 다른 소스로부터 값을 가져오는 로직 추가

            lastCacheUpdateTime = DateTime.Now; // 캐시 업데이트 시간 기록
        }

        private static void RefreshCache()
        {

            commonValues["today"] = DateTime.Today.ToString("d");
            // 필요한 경우 여기에서 DB나 다른 소스로부터 값을 가져오는 로직 추가

            lastCacheUpdateTime = DateTime.Now; // 캐시 업데이트 시간 기록
        }

        // 공통 변수 값을 가져오는 메서드
        public static string GetValue(string key)
        {
            if (commonValues.Count == 0 || DateTime.Now - lastCacheUpdateTime > cacheDuration)
            {
                RefreshCache(); // 캐시가 비어있거나 유효 기간이 지난 경우 새로 고침
            }
            return commonValues.TryGetValue(key, out var value) ? value : string.Empty;
        }

        // 공통 변수 값을 설정하는 메서드
        public static void SetValue(string key, string value)
        {
            commonValues[key] = value;
            lastCacheUpdateTime = DateTime.Now; // 캐시 업데이트 시간 기록
        }

        //BarItemMessage
        public static bool gTrackMsg = false;
        public static event EventHandler gMsgChanged;
        private static string _gMsg;
        public static string gMsg
        {
            get { return _gMsg; }
            set
            {
                if (value != _gMsg)
                {
                    _gMsg = value;

                    if (gMsgChanged != null)
                    {
                        gMsgChanged(value, EventArgs.Empty);
                    }

                }
            }
        }

        //FormDesigner Log
        public static bool gTrackLog = false;
        public static event EventHandler gLogChanged;
        private static string _gLog;
        public static string gLog
        {
            get { return _gLog; }
            set
            {
                if (value != _gLog)
                {
                    _gLog = value;

                    if (gLogChanged != null)
                    {
                        gLogChanged(value, EventArgs.Empty);
                    }

                }
            }
        }
    }
}

```

```C#
using System.Text.RegularExpressions;
using static DevExpress.Utils.MVVM.Internal.ILReader;
using Match = System.Text.RegularExpressions.Match;

namespace Lib.Syntax
{
    public class SyntaxMatch
    {
        public Dictionary<string, string> OPatternMatch { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DPatternMatch { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> GPatternMatch { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> CPatternMatch { get; set; } = new Dictionary<string, string>();
    }

    public class SyntaxExtractor
    {
        public SyntaxMatch ExtractVariables(string query)
        {
            SyntaxMatch variables = new SyntaxMatch();

            //Regex oPattern = new Regex(@"@\w+");
            //Regex dPattern = new Regex(@"@_\w+");
            //Regex gPattern = new Regex(@"<\$(\w+)>");
            //Regex cPattern = new Regex(@"andif\s+(.+?)\s+endif");

            Regex oPattern = new Regex(RegexStrs.Lists[RegexStr.OPattern]);
            Regex dPattern = new Regex(RegexStrs.Lists[RegexStr.DPattern]);
            Regex gPattern = new Regex(RegexStrs.Lists[RegexStr.GPattern]);
            Regex cPattern = new Regex(RegexStrs.Lists[RegexStr.CPattern], RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match match in dPattern.Matches(query))
            {
                string variableName = match.Value;
                if (!variables.DPatternMatch.ContainsKey(variableName))
                {
                    variables.DPatternMatch[variableName] = null;
                }
            }

            foreach (Match match in oPattern.Matches(query))
            {
                string variableName = match.Value;
                if (!variables.OPatternMatch.ContainsKey(variableName) && !variables.DPatternMatch.ContainsKey(variableName))
                {
                    variables.OPatternMatch[variableName] = null;
                }
            }

            foreach (Match match in gPattern.Matches(query))
            {
                string variableName = match.Value;
                if (!variables.GPatternMatch.ContainsKey(variableName))
                {
                    variables.GPatternMatch[variableName] = null;
                }
            }

            foreach (Match match in cPattern.Matches(query))
            {
                string variableName = match.Value;
                if (!variables.CPatternMatch.ContainsKey(variableName))
                {
                    variables.CPatternMatch[variableName] = null;
                }
            }

            return variables;
        }

        /// <summary>
        /// Extract Pattern
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="patternSelector">match=>match.OPatternMatch</param>
        /// <returns></returns>
        public static Dictionary<string, string> ExtractPattern(string text, Func<SyntaxMatch, Dictionary<string, string>> patternSelector) 
        { 
            SyntaxExtractor extractor = new SyntaxExtractor(); 
            SyntaxMatch variables = extractor.ExtractVariables(text); 
            return patternSelector(variables); 
        }

        /// <summary>
        /// Remove Pattern from text
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="patternStr">RegexStr.CPattern</param>    
        /// <returns>Modified SQL text</returns>
        public static string RemovePattern(string text, RegexStr patternStr)
        {
            string patternString = RegexStrs.Lists[patternStr];
            Regex regexPattern;// = new Regex(patternString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (patternStr == RegexStr.CPattern)
            {
                regexPattern = new Regex(patternString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                text = regexPattern.Replace(text, string.Empty);
            }
            if (patternStr == RegexStr.OPattern)
            {
                regexPattern = new Regex(patternString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                text = regexPattern.Replace(text, "''");
            }
            return text;
        }
    }
}



```

```C#
using System.Drawing;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using System.Text.RegularExpressions;

namespace Lib.Syntax
{
    public class SQL_Syntax : ISyntaxHighlightService
    {
        readonly Document document;

        Regex _keywords;
        Regex _quotedString = new Regex(@"'([^']|'')*'");
        Regex _commentedString = new Regex(@"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)");
        //Regex _customPattern = new Regex(@"<\$[^>]+>");  // <$로 시작해서 >로 끝나는 패턴
        Regex _customPattern = new Regex(@"@_\w+"); // @_로 시작하는 단어 패턴

        public SQL_Syntax(Document document)
        {
            this.document = document;
            string[] keywords = { "INSERT", "SELECT", "UPDATE", "DELETE", "CREATE",
                                  "TABLE", "USE", "IDENTITY", "ON", "OFF",
                                  "NOT", "NULL", "WITH", "SET", "GO",
                                  "DECLARE", "EXECUTE", "EXEC", "NVARCHAR", "FROM",
                                  "INTO", "VALUES", "WHERE", "AND" };
            _keywords = new Regex(@"\b(" + string.Join("|", keywords.Select(w => Regex.Escape(w))) + @")\b", RegexOptions.IgnoreCase);
        }
        public void ForceExecute()
        {
            Execute();
        }
        public void Execute()
        {
            List<SyntaxHighlightToken> tSqltokens = ParseTokens();
            document.ApplySyntaxHighlight(tSqltokens);
        }

        private List<SyntaxHighlightToken> ParseTokens()
        {
            List<SyntaxHighlightToken> tokens = new List<SyntaxHighlightToken>();

            // search for quoted strings
            DocumentRange[] ranges = document.FindAll(_quotedString).GetAsFrozen() as DocumentRange[];
            for (int i = 0; i < ranges.Length; i++)
            {
                tokens.Add(CreateToken(ranges[i].Start.ToInt(), ranges[i].End.ToInt(), Color.Red));
            }

            //Extract all keywords
            ranges = document.FindAll(_keywords).GetAsFrozen() as DocumentRange[];
            for (int j = 0; j < ranges.Length; j++)
            {
                if (!IsRangeInTokens(ranges[j], tokens))
                    tokens.Add(CreateToken(ranges[j].Start.ToInt(), ranges[j].End.ToInt(), Color.Blue));
            }

            //Find all comments
            ranges = document.FindAll(_commentedString).GetAsFrozen() as DocumentRange[];
            for (int j = 0; j < ranges.Length; j++)
            {
                if (!IsRangeInTokens(ranges[j], tokens))
                    tokens.Add(CreateToken(ranges[j].Start.ToInt(), ranges[j].End.ToInt(), Color.Green));
            }

            //Find all custom patterns
            ranges = document.FindAll(_customPattern).GetAsFrozen() as DocumentRange[];
            for (int j = 0; j < ranges.Length; j++)
            {
                if (!IsRangeInTokens(ranges[j], tokens))
                    tokens.Add(CreateToken(ranges[j].Start.ToInt(), ranges[j].End.ToInt(), Color.Magenta)); // 원하는 색상으로 변경
            }

            // order tokens by their start position
            tokens.Sort(new SyntaxHighlightTokenComparer());

            // fill in gaps in document coverage
            tokens = CombineWithPlainTextTokens(tokens);

            document.BeginUpdate();
            document.DefaultCharacterProperties.FontName = "Cascadia Code Light"; // "Courier New";
            document.DefaultCharacterProperties.FontSize = 10;
            document.EndUpdate();

            return tokens;
        }

        //Parse the remaining text into tokens:
        List<SyntaxHighlightToken> CombineWithPlainTextTokens(List<SyntaxHighlightToken> tokens)
        {
            List<SyntaxHighlightToken> result = new List<SyntaxHighlightToken>(tokens.Count * 2 + 1);
            int documentStart = document.Range.Start.ToInt();
            int documentEnd = document.Range.End.ToInt();
            if (tokens.Count == 0)
                result.Add(CreateToken(documentStart, documentEnd, Color.Black));
            else
            {
                SyntaxHighlightToken firstToken = tokens[0];
                if (documentStart < firstToken.Start)
                    result.Add(CreateToken(documentStart, firstToken.Start, Color.Black));
                result.Add(firstToken);
                for (int i = 1; i < tokens.Count; i++)
                {
                    SyntaxHighlightToken token = tokens[i];
                    SyntaxHighlightToken prevToken = tokens[i - 1];
                    if (prevToken.End != token.Start)
                        result.Add(CreateToken(prevToken.End, token.Start, Color.Black));
                    result.Add(token);
                }
                SyntaxHighlightToken lastToken = tokens[tokens.Count - 1];
                if (documentEnd > lastToken.End)
                    result.Add(CreateToken(lastToken.End, documentEnd, Color.Black));
            }
            return result;
        }

        //Create a token from the retrieved range and specify its forecolor
        SyntaxHighlightToken CreateToken(int start, int end, Color foreColor)
        {
            SyntaxHighlightProperties properties = new SyntaxHighlightProperties();
            properties.ForeColor = foreColor;
            return new SyntaxHighlightToken(start, end - start, properties);
        }

        //Check whether tokens intersect each other
        private bool IsRangeInTokens(DocumentRange range, List<SyntaxHighlightToken> tokens)
        {
            return tokens.Any(t => IsIntersect(range, t));
        }
        bool IsIntersect(DocumentRange range, SyntaxHighlightToken token)
        {
            int start = range.Start.ToInt();
            if (start >= token.Start && start < token.End)
                return true;
            int end = range.End.ToInt() - 1;
            if (end >= token.Start && end < token.End)
                return true;
            if (start < token.Start && end >= token.End)
                return true;
            return false;
        }
    }

    //Compare token's initial positions to sort them
    public class SyntaxHighlightTokenComparer : IComparer<SyntaxHighlightToken>
    {
        public int Compare(SyntaxHighlightToken x, SyntaxHighlightToken y)
        {
            return x.Start - y.Start;
        }
    }
}

```

```C#
using System.Drawing;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using System.Text.RegularExpressions;

namespace Lib.Syntax
{
    public class CS_Syntax : ISyntaxHighlightService
    {
        readonly Document document;

        Regex _keywords;
        Regex _quotedString;
        Regex _commentedString;
        public CS_Syntax(Document document)
        {
            this.document = document;

            // C# Keywords
            string[] keywords = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
                "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
                "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
                "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator",
                "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
                "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try",
                "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while" };
            _keywords = new Regex(@"\b(" + string.Join("|", keywords.Select(w => Regex.Escape(w))) + @")\b");

            // Strings
            _quotedString = new Regex(@"@?""([^""\\]|\\.)*""|'([^'\\]|\\.)*'");

            // Comments
            _commentedString = new Regex(@"//.*|/\*[\s\S]*?\*/");
        }
        public void Execute()
        {
            List<SyntaxHighlightToken> cSharpTokens = ParseTokens();
            document.ApplySyntaxHighlight(cSharpTokens);
        }
        public void ForceExecute()
        {
            Execute();
        }
        private List<SyntaxHighlightToken> ParseTokens()
        {
            List<SyntaxHighlightToken> tokens = new List<SyntaxHighlightToken>();

            // search for quoted strings
            DocumentRange[] ranges = document.FindAll(_quotedString).GetAsFrozen() as DocumentRange[];
            for (int i = 0; i < ranges.Length; i++)
            {
                tokens.Add(CreateToken(ranges[i].Start.ToInt(), ranges[i].End.ToInt(), Color.Red));
            }

            //Extract all keywords
            ranges = document.FindAll(_keywords).GetAsFrozen() as DocumentRange[];
            for (int j = 0; j < ranges.Length; j++)
            {
                if (!IsRangeInTokens(ranges[j], tokens))
                    tokens.Add(CreateToken(ranges[j].Start.ToInt(), ranges[j].End.ToInt(), Color.Blue));
            }

            //Find all comments
            ranges = document.FindAll(_commentedString).GetAsFrozen() as DocumentRange[];
            for (int j = 0; j < ranges.Length; j++)
            {
                if (!IsRangeInTokens(ranges[j], tokens))
                    tokens.Add(CreateToken(ranges[j].Start.ToInt(), ranges[j].End.ToInt(), Color.Green));
            }

            // order tokens by their start position
            tokens.Sort(new SyntaxHighlightTokenComparer());

            // fill in gaps in document coverage
            tokens = CombineWithPlainTextTokens(tokens);

            document.BeginUpdate();
            document.DefaultCharacterProperties.FontName = "Cascadia Code Light"; // "Courier New";
            document.DefaultCharacterProperties.FontSize = 10;
            document.EndUpdate();

            return tokens;
        }

        //Parse the remaining text into tokens:
        List<SyntaxHighlightToken> CombineWithPlainTextTokens(List<SyntaxHighlightToken> tokens)
        {
            List<SyntaxHighlightToken> result = new List<SyntaxHighlightToken>(tokens.Count * 2 + 1);
            int documentStart = document.Range.Start.ToInt();
            int documentEnd = document.Range.End.ToInt();
            if (tokens.Count == 0)
                result.Add(CreateToken(documentStart, documentEnd, Color.Black));
            else
            {
                SyntaxHighlightToken firstToken = tokens[0];
                if (documentStart < firstToken.Start)
                    result.Add(CreateToken(documentStart, firstToken.Start, Color.Black));
                result.Add(firstToken);
                for (int i = 1; i < tokens.Count; i++)
                {
                    SyntaxHighlightToken token = tokens[i];
                    SyntaxHighlightToken prevToken = tokens[i - 1];
                    if (prevToken.End != token.Start)
                        result.Add(CreateToken(prevToken.End, token.Start, Color.Black));
                    result.Add(token);
                }
                SyntaxHighlightToken lastToken = tokens[tokens.Count - 1];
                if (documentEnd > lastToken.End)
                    result.Add(CreateToken(lastToken.End, documentEnd, Color.Black));
            }
            return result;
        }

        //Create a token from the retrieved range and specify its forecolor
        SyntaxHighlightToken CreateToken(int start, int end, Color foreColor)
        {
            SyntaxHighlightProperties properties = new SyntaxHighlightProperties();
            properties.ForeColor = foreColor;
            return new SyntaxHighlightToken(start, end - start, properties);
        }

        //Check whether tokens intersect each other
        private bool IsRangeInTokens(DocumentRange range, List<SyntaxHighlightToken> tokens)
        {
            return tokens.Any(t => IsIntersect(range, t));
        }
        bool IsIntersect(DocumentRange range, SyntaxHighlightToken token)
        {
            int start = range.Start.ToInt();
            if (start >= token.Start && start < token.End)
                return true;
            int end = range.End.ToInt() - 1;
            if (end >= token.Start && end < token.End)
                return true;
            if (start < token.Start && end >= token.End)
                return true;
            return false;
        }
    }
}

```

```C#
namespace Lib.Repo
{
    public class CdeMst : MdlBase
    {
        private string _Id;
        public string Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private string _PId;
        public string PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }

        private string _SubId;
        public string SubId
        {
            get => _SubId;
            set => Set(ref _SubId, value);
        }

        private string _Nm;
        public string Nm
        {
            get => _Nm;
            set => Set(ref _Nm, value);
        }

        private bool _UseYn;
        public bool UseYn
        {
            get => _UseYn;
            set => Set(ref _UseYn, value);
        }

        private string _Ref01;
        public string Ref01
        {
            get => _Ref01;
            set => Set(ref _Ref01, value);
        }

        private string _Ref02;
        public string Ref02
        {
            get => _Ref02;
            set => Set(ref _Ref02, value);
        }

        private string _Ref03;
        public string Ref03
        {
            get => _Ref03;
            set => Set(ref _Ref03, value);
        }

        private string _Ref04;
        public string Ref04
        {
            get => _Ref04;
            set => Set(ref _Ref04, value);
        }

        private string _Ref05;
        public string Ref05
        {
            get => _Ref05;
            set => Set(ref _Ref05, value);
        }

        private string _Ref06;
        public string Ref06
        {
            get => _Ref06;
            set => Set(ref _Ref06, value);
        }

        private string _Ref07;
        public string Ref07
        {
            get => _Ref07;
            set => Set(ref _Ref07, value);
        }

        private string _Ref08;
        public string Ref08
        {
            get => _Ref08;
            set => Set(ref _Ref08, value);
        }

        private string _Ref09;
        public string Ref09
        {
            get => _Ref09;
            set => Set(ref _Ref09, value);
        }

        private string _Ref10;
        public string Ref10
        {
            get => _Ref10;
            set => Set(ref _Ref10, value);
        }

        private string _Ref11;
        public string Ref11
        {
            get => _Ref11;
            set => Set(ref _Ref11, value);
        }

        private string _Ref12;
        public string Ref12
        {
            get => _Ref12;
            set => Set(ref _Ref12, value);
        }

        private string _Ref13;
        public string Ref13
        {
            get => _Ref13;
            set => Set(ref _Ref13, value);
        }

        private string _Ref14;
        public string Ref14
        {
            get => _Ref14;
            set => Set(ref _Ref14, value);
        }

        private string _Ref15;
        public string Ref15
        {
            get => _Ref15;
            set => Set(ref _Ref15, value);
        }

        private string _Ref16;
        public string Ref16
        {
            get => _Ref16;
            set => Set(ref _Ref16, value);
        }

        private string _Ref17;
        public string Ref17
        {
            get => _Ref17;
            set => Set(ref _Ref17, value);
        }

        private string _Ref18;
        public string Ref18
        {
            get => _Ref18;
            set => Set(ref _Ref18, value);
        }

        private string _Ref19;
        public string Ref19
        {
            get => _Ref19;
            set => Set(ref _Ref19, value);
        }

        private string _Ref20;
        public string Ref20
        {
            get => _Ref20;
            set => Set(ref _Ref20, value);
        }
    }

    public interface ICdeMstRepo
    {
        List<CdeMst> GetByPId(string pid);
        CdeMst GetById(string id);
        void Add(CdeMst cdeMst);
        void Update(CdeMst cdeMst);
        void Delete(string id);
    }

    public class CdeMstRepo : ICdeMstRepo
    {
        public List<CdeMst> GetByPId(string pid)
        {
            string sql = @"
select a.Id, a.PId, a.SubId, a.Nm, a.UseYn,
       a.Ref01, a.Ref02, a.Ref03, a.Ref04, a.Ref05,
       a.Ref06, a.Ref07, a.Ref08, a.Ref09, a.Ref10,
       a.Ref11, a.Ref12, a.Ref13, a.Ref14, a.Ref15,
       a.Ref16, a.Ref17, a.Ref18, a.Ref19, a.Ref20,
       a.CId, a.CDt, a.MId, a.MDt
  from CDEMST a
 where 1=1
   and a.PId = @PId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CdeMst>(sql, new { PId = pid }).ToList();

                foreach (var item in result)
                {
                    item.ChangedFlag = MdlState.None;
                }

                return result;
            }
        }

        public CdeMst GetById(string id)
        {
            string sql = @"
select a.Id, a.PId, a.SubId, a.Nm, a.UseYn,
       a.Ref01, a.Ref02, a.Ref03, a.Ref04, a.Ref05,
       a.Ref06, a.Ref07, a.Ref08, a.Ref09, a.Ref10,
       a.Ref11, a.Ref12, a.Ref13, a.Ref14, a.Ref15,
       a.Ref16, a.Ref17, a.Ref18, a.Ref19, a.Ref20,
       a.CId, a.CDt, a.MId, a.MDt
  from CDEMST a
 where 1=1
   and a.Id = @Id
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CdeMst>(sql, new { Id = id }).FirstOrDefault();
                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the ID {id} was not found.");
                }
                result.ChangedFlag = MdlState.None;

                return result;
            }
        }

        public void Add(CdeMst cdeMst)
        {
            string sql = @"
insert into CDEMST
      (PId, SubId, Nm, UseYn,
       Ref01, Ref02, Ref03, Ref04, Ref05,
       Ref06, Ref07, Ref08, Ref09, Ref10,
       Ref11, Ref12, Ref13, Ref14, Ref15,
       Ref16, Ref17, Ref18, Ref19, Ref20,
       CId, CDt, MId, MDt)
select @PId, @SubId, @Nm, @UseYn,
       @Ref01, @Ref02, @Ref03, @Ref04, @Ref05,
       @Ref06, @Ref07, @Ref08, @Ref09, @Ref10,
       @Ref11, @Ref12, @Ref13, @Ref14, @Ref15,
       @Ref16, @Ref17, @Ref18, @Ref19, @Ref20,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, cdeMst);
            }
        }

        public void Update(CdeMst cdeMst)
        {
            string sql = @"
update a
   set PId= @PId,
       SubId= @SubId,
       Nm= @Nm,
       UseYn= @UseYn,
       Ref01= @Ref01,
       Ref02= @Ref02,
       Ref03= @Ref03,
       Ref04= @Ref04,
       Ref05= @Ref05,
       Ref06= @Ref06,
       Ref07= @Ref07,
       Ref08= @Ref08,
       Ref09= @Ref09,
       Ref10= @Ref10,
       Ref11= @Ref11,
       Ref12= @Ref12,
       Ref13= @Ref13,
       Ref14= @Ref14,
       Ref15= @Ref15,
       Ref16= @Ref16,
       Ref17= @Ref17,
       Ref18= @Ref18,
       Ref19= @Ref19,
       Ref20= @Ref20,
       MId= @MId,
       MDt= getdate()
  from CDEMST a
 where 1=1
   and Id = @Id
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, cdeMst);
            }
        }

        public void Delete(string id)
        {
            string sql = @"
delete
  from CDEMST
 where 1=1
   and Id = @Id
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, new { Id = id });
            }
        }
    }
}

```

```C#
namespace Lib.Repo
{
    public class CdeRef : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _Cd;
        public string Cd
        {
            get => _Cd;
            set => Set(ref _Cd, value);
        }

        private string _RefNo;
        public string RefNo
        {
            get => _RefNo;
            set => Set(ref _RefNo, value);
        }

        private string _FldTy;
        public string FldTy
        {
            get => _FldTy;
            set => Set(ref _FldTy, value);
        }

        private string _FldTitle;
        public string FldTitle
        {
            get => _FldTitle;
            set => Set(ref _FldTitle, value);
        }

        private string _Popup;
        public string Popup
        {
            get => _Popup;
            set => Set(ref _Popup, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }
    }
    public class CdeRefRepo
    {
 
    }
}

```

```C#
namespace Lib.Repo
{
    public class CtrlMst : MdlBase
    {
        private int _CtrlId;
        public int CtrlId
        {
            get => _CtrlId;
            set => Set(ref _CtrlId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _CtrlGrpCd;
        public string CtrlGrpCd
        {
            get => _CtrlGrpCd;
            set => Set(ref _CtrlGrpCd, value);
        }

        private string _CtrlRegNm;
        public string CtrlRegNm
        {
            get => _CtrlRegNm;
            set => Set(ref _CtrlRegNm, value);
        }

        private bool _ContainYn;
        public bool ContainYn
        {
            get => _ContainYn;
            set => Set(ref _ContainYn, value);
        }

        private bool _CustomYn;
        public bool CustomYn
        {
            get => _CustomYn;
            set => Set(ref _CustomYn, value);
        }

        private bool _UseYn;
        public bool UseYn
        {
            get => _UseYn;
            set => Set(ref _UseYn, value);
        }

        private string _Rnd;
        public string Rnd
        {
            get => _Rnd;
            set => Set(ref _Rnd, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

        private int _PId;
        public int PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }
    }
    public interface ICtrlMstRepo
    {
        Dictionary<string, string> GetBindPropertyMapping();
        bool ChkByCtrlNm(string ctrlNm);
        CtrlMst GetByCtrlNm(string ctrlNm);
        List<CtrlMst> GetAll();
        List<CtrlMst> GetUCCtrl();
        void Add(CtrlMst uctrl);
        void Update(CtrlMst uctrl);
        void Delete(int Id);

    }
    public class  CtrlMstRepo : ICtrlMstRepo
    {
        public bool ChkByCtrlNm(string ctrlNm)
        {
            string sql = @"
select a.CtrlId, a.CtrlNm, a.CtrlGrpCd, a.CtrlRegNm, a.ContainYn, a.UseYn,
       a.CustomYn, a.Rnd, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from CTRLMST a
 where 1=1
   and a.CtrlNm = @CtrlNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CtrlMst>(sql, new { CtrlNm = ctrlNm }).FirstOrDefault();
                return result == null;
            }
        }

        public CtrlMst GetByCtrlNm(string ctrlNm)
        {
            string sql = @"
select a.CtrlId, a.CtrlNm, a.CtrlGrpCd, a.CtrlRegNm, a.ContainYn, a.UseYn,
       a.CustomYn, a.Rnd, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from CTRLMST a
 where 1=1
   and a.CtrlNm = @CtrlNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CtrlMst>(sql, new { CtrlNm = ctrlNm }).FirstOrDefault();
                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {ctrlNm} was not found.");
                }
                result.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정

                return result;
            }
        }
        public List<CtrlMst> GetAll()
        {
            string sql = @"
select a.CtrlId, a.CtrlNm, a.CtrlGrpCd, a.CtrlRegNm, a.ContainYn, a.UseYn,
       a.CustomYn, a.Rnd, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from CTRLMST a
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CtrlMst>(sql).ToList();

                foreach (var item in result)
                {
                    item.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정
                }

                return result;
            }

        }

        public void Add(CtrlMst uctrl)
        {
            string sql = @"
insert into CTRLMST
      (CtrlNm, CtrlGrpCd, CtrlRegNm, ContainYn, UseYn,
       CustomYn, Rnd, Memo, PId, CId,
       CDt, MId, MDt)
select @CtrlNm, @CtrlGrpCd, @CtrlRegNm, @ContainYn, @UseYn,
       @CustomYn, @Rnd, @Memo, @PId, @CId,
       getdate(), @MId, getdate()
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, uctrl);
            }
        }

        public void Update(CtrlMst uctrl)
        {
            string sql = @"
update a
   set CtrlNm= @CtrlNm,
       CtrlGrpCd= @CtrlGrpCd,
       CtrlRegNm= @CtrlRegNm,
       ContainYn= @ContainYn,
       CustomYn= @CustomYn,
       UseYn= @UseYn,
       Rnd= @Rnd,
       Memo= @Memo,
       PId= @PId,
       MId= @MId,
       MDt= getdate()
  from CTRLMST a
 where 1=1
   and CtrlId = @CtrlId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, uctrl);
            }
        }

        public void Delete(int Id)
        {
            string sql = @"
delete
  from CTRLMST
 where 1=1
   and CtrlId = @CtrlId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, new { CtrlId = Id });
            }
        }

        public List<CtrlMst> GetUCCtrl()
        {
            string sql = @"
select a.CtrlId, a.CtrlNm, a.CtrlGrpCd, a.CtrlRegNm, a.ContainYn, a.UseYn,
       a.CustomYn, a.Rnd, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from CTRLMST a
 where 1=1
   and a.UseYn = '1'
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<CtrlMst>(sql).ToList();

                foreach (var item in result)
                {
                    item.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정
                }

                return result;
            }
        }

        public Dictionary<string, string> GetBindPropertyMapping()
        {
            string sql = @"
select a.CtrlNm, a.Binding
  from CTRLMST
 where a.CtrlGrpCd = 'Bind'
";
            using (var db = new GaiaHelper())
            {
                return db.QueryDictionary(sql).ToDictionary();
            }
        }
    }
}

```

```C#
namespace Lib.Repo
{
    public class FrmCtrl : Lib.MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _ToolNm;
        public string ToolNm
        {
            get => _ToolNm;
            set => Set(ref _ToolNm, value);
        }

        private int _CtrlW;
        public int CtrlW
        {
            get => _CtrlW;
            set => Set(ref _CtrlW, value);
        }

        private int _CtrlH;
        public int CtrlH
        {
            get => _CtrlH;
            set => Set(ref _CtrlH, value);
        }
        private int _CtrlX;
        public int CtrlX
        {
            get => _CtrlX;
            set => Set(ref _CtrlX, value);
        }
        private int _CtrlY;
        public int CtrlY
        {
            get => _CtrlY;
            set => Set(ref _CtrlY, value);
        }
        private string _TitleText;
        public string TitleText
        {
            get => _TitleText;
            set => Set(ref _TitleText, value);
        }

        private int _TitleWidth;
        public int TitleWidth
        {
            get => _TitleWidth;
            set => Set(ref _TitleWidth, value);
        }

        private string _TitleAlign;
        public string TitleAlign
        {
            get => _TitleAlign;
            set => Set(ref _TitleAlign, value);
        }

        private string _DefaultText;
        public string DefaultText
        {
            get => _DefaultText;
            set => Set(ref _DefaultText, value);
        }

        private string _TextAlign;
        public string TextAlign
        {
            get => _TextAlign;
            set => Set(ref _TextAlign, value);
        }

        private bool _ShowYn;
        public bool ShowYn
        {
            get => _ShowYn;
            set => Set(ref _ShowYn, value);
        }

        private bool _EditYn;
        public bool EditYn
        {
            get => _EditYn;
            set => Set(ref _EditYn, value);
        }
    }
    public interface IFrmCtrlRepo
    {
        List<FrmCtrl> GetByFrwFrm(string frwId, string frmId);
        void Add(FrmCtrl frmCtrl);
        void Update(FrmCtrl frmCtrl);
        void Delete(string frwId, string frmid, string ctrlnm);

    }
    public class FrmCtrlRepo : IFrmCtrlRepo
    {
        public void Add(FrmCtrl frmCtrl)
        {
            string sql = @"
insert into FRMCTRL
      (FrwId, FrmId, CtrlNm, ToolNm, CtrlW,
       CtrlH, CtrlX, CtrlY, TitleText, TitleWidth,
       TitleAlign, DefaultText, TextAlign, ShowYn, EditYn,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @CtrlNm, @ToolNm, @CtrlW,
       @CtrlH, @CtrlX, @CtrlY, @TitleText, @TitleWidth,
       @TitleAlign, @DefaultText, @TextAlign, @ShowYn, @EditYn,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frmCtrl);
            }
        }

        public void Delete(string frwId, string frmId, string ctrlnm)
        {
            string sql = @"
delete
  from FRMCTRL
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId 
   and CtrlNm = @CtrlNm
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, new { FrwId = frwId, FrmId = frmId, CtrlNm = ctrlnm });
            }
        }

        public List<FrmCtrl> GetByFrwFrm(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.ToolNm, a.CtrlW,
       a.CtrlH, a.CtrlX, a.CtrlY, a.TitleText, a.TitleWidth,
       a.TitleAlign, a.DefaultText, a.TextAlign, a.ShowYn, a.EditYn,
       a.CId, a.CDt, a.MId, a.MDt
  from FRMCTRL a
 where 1 = 1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmCtrl>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {frmId} was not found.");
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

        public void Update(FrmCtrl frmCtrl)
        {
            string sql = @"
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       CtrlNm= @CtrlNm,
       ToolNm= @ToolNm,
       CtrlW= @CtrlW,
       CtrlH= @CtrlH,
       CtrlX= @CtrlX,
       CtrlY= @CtrlY,
       TitleText= @TitleText,
       TitleWidth= @TitleWidth,
       TitleAlign= @TitleAlign,
       DefaultText= @DefaultText,
       TextAlign= @TextAlign,
       ShowYn= @ShowYn,
       EditYn= @EditYn,
       MId= @MId,
       MDt= getdate()
  from FRMCTRL a
 where 1=1
   and FrwId= @FrwId
   and FrmId= @FrmId
   and CtrlNm= @CtrlNm
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frmCtrl);
            }
        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class FrmWrk : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _WrkNm;
        public string WrkNm
        {
            get => _WrkNm;
            set => Set(ref _WrkNm, value);
        }

        private string _WrkCd;
        public string WrkCd
        {
            get => _WrkCd;
            set => Set(ref _WrkCd, value);
        }

        private string _SelectMode;
        public string SelectMode
        {
            get => _SelectMode;
            set => Set(ref _SelectMode, value);
        }

        private bool _MultiSelect;
        public bool MultiSelect
        {
            get => _MultiSelect;
            set => Set(ref _MultiSelect, value);
        }

        private bool _UseYn;
        public bool UseYn
        {
            get => _UseYn;
            set => Set(ref _UseYn, value);
        }

        private bool _NavAdd;
        public bool NavAdd
        {
            get => _NavAdd;
            set => Set(ref _NavAdd, value);
        }

        private bool _NavDelete;
        public bool NavDelete
        {
            get => _NavDelete;
            set => Set(ref _NavDelete, value);
        }

        private bool _NavSave;
        public bool NavSave
        {
            get => _NavSave;
            set => Set(ref _NavSave, value);
        }

        private bool _NavCancel;
        public bool NavCancel
        {
            get => _NavCancel;
            set => Set(ref _NavCancel, value);
        }

        private int _SaveSq;
        public int SaveSq
        {
            get => _SaveSq;
            set => Set(ref _SaveSq, value);
        }

        private int _OpenSq;
        public int OpenSq
        {
            get => _OpenSq;
            set => Set(ref _OpenSq, value);
        }

        private string _OpenTrg;
        public string OpenTrg
        {
            get => _OpenTrg;
            set => Set(ref _OpenTrg, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }
    }
    public interface IFrmWrkRepo
    {
        List<FrmWrk> GetByWorkSetsOpenOrderby(string frwId, string frmId);
        List<FrmWrk> GetByWorkSetsSaveOrderby(string frwId, string frmId);
        List<FrmWrk> GetByFieldSets(string frwId, string frmId);
        List<FrmWrk> GetByDataSets(string frwId, string frmId);
        List<FrmWrk> GetByGridSets(string frwId, string frmId);
        FrmWrk GetByWorkSet(string frwId, string frmId, string ctrlNm);
        void Add(FrmWrk frmWrk);
        void Update(FrmWrk frmWrk);
        void Delete(string wrkId);
    }
    public class FrmWrkRepo : IFrmWrkRepo
    {
        public List<FrmWrk> GetByWorkSetsOpenOrderby(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.OpenSq > 0
 order by a.OpenSq 
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

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
        public List<FrmWrk> GetByWorkSetsSaveOrderby(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
 order by a.SaveSq 
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

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

        public List<FrmWrk> GetByFieldSets(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkCd = 'FieldSet'
 order by a.OpenSq 
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

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

        public List<FrmWrk> GetByDataSets(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkCd = 'DataSet'
 order by a.OpenSq 
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

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

        public List<FrmWrk> GetByGridSets(string frwId, string frmId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkCd = 'GridSet'
 order by a.OpenSq 
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

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

        public FrmWrk GetByWorkSet(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CtrlNm, a.WrkNm,
       a.WrkCd, a.SelectMode, a.MultiSelect, a.UseYn, a.NavAdd,
       a.NavDelete, a.NavSave, a.NavCancel, a.SaveSq, a.OpenSq,
       a.OpenTrg, a.Memo, 
       a.CId, a.CDt, a.MId, a.MDt
  from FRMWRK a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkId = @WrkId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrmWrk>(sql, new {FrwId = frwId, FrmId = frmId, WrkId = wrkId }).FirstOrDefault();
                return result;
            }
        }

        public void Update(FrmWrk frmWrk)
        {
            string sql = @"
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       WrkId= @WrkId,
       CtrlNm= @CtrlNm,
       WrkNm= @WrkNm,
       WrkCd= @WrkCd,
       SelectMode= @SelectMode,
       MultiSelect= @MultiSelect,
       UseYn= @UseYn,
       NavAdd= @NavAdd,
       NavDelete= @NavDelete,
       NavSave= @NavSave,
       NavCancel= @NavCancel,
       SaveSq= @SaveSq,
       OpenSq= @OpenSq,
       OpenTrg= @OpenTrg,
       Memo= @Memo,
       MId= " + Common.GetValue("gRegId") + @",
       MDt= getdate()
  from FRMWRK a
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId
   and WrkId = @WrkId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, frmWrk);
            }
        }
        public void Add(FrmWrk frmWrk)
        {
            string sql = @"
insert into FRMWRK
      (FrwId, FrmId, WrkId, CtrlNm, WrkNm,
       WrkCd, SelectMode, MultiSelect, UseYn, NavAdd,
       NavDelete, NavSave, NavCancel, SaveSq, OpenSq,
       OpenTrg, Memo, 
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @CtrlNm, @WrkNm,
       @WrkCd, @SelectMode, @MultiSelect, @UseYn, @NavAdd,
       @NavDelete, @NavSave, @NavCancel, @SaveSq, @OpenSq,
       @OpenTrg, @Memo, 
       " + Common.GetValue("gRegId") + @", getdate(), " + Common.GetValue("gRegId") + @", getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frmWrk);
            }
        }

        public void Delete(string wrkId)
        {
            string sql = @"
delete
  from FRMWRK
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId
   and WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, new { WrkId = wrkId });
            }
        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class FrwCde : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _Cd;
        public string Cd
        {
            get => _Cd;
            set => Set(ref _Cd, value);
        }

        private string _PCd;
        public string PCd
        {
            get => _PCd;
            set => Set(ref _PCd, value);
        }

        private string _SubCd;
        public string SubCd
        {
            get => _SubCd;
            set => Set(ref _SubCd, value);
        }

        private string _Nm;
        public string Nm
        {
            get => _Nm;
            set => Set(ref _Nm, value);
        }

        private bool _UseYn;
        public bool UseYn
        {
            get => _UseYn;
            set => Set(ref _UseYn, value);
        }

        private string _Ref01;
        public string Ref01
        {
            get => _Ref01;
            set => Set(ref _Ref01, value);
        }

        private string _Ref02;
        public string Ref02
        {
            get => _Ref02;
            set => Set(ref _Ref02, value);
        }

        private string _Ref03;
        public string Ref03
        {
            get => _Ref03;
            set => Set(ref _Ref03, value);
        }

        private string _Ref04;
        public string Ref04
        {
            get => _Ref04;
            set => Set(ref _Ref04, value);
        }

        private string _Ref05;
        public string Ref05
        {
            get => _Ref05;
            set => Set(ref _Ref05, value);
        }

        private string _Ref06;
        public string Ref06
        {
            get => _Ref06;
            set => Set(ref _Ref06, value);
        }

        private string _Ref07;
        public string Ref07
        {
            get => _Ref07;
            set => Set(ref _Ref07, value);
        }

        private string _Ref08;
        public string Ref08
        {
            get => _Ref08;
            set => Set(ref _Ref08, value);
        }

        private string _Ref09;
        public string Ref09
        {
            get => _Ref09;
            set => Set(ref _Ref09, value);
        }

        private string _Ref10;
        public string Ref10
        {
            get => _Ref10;
            set => Set(ref _Ref10, value);
        }

        private string _Ref11;
        public string Ref11
        {
            get => _Ref11;
            set => Set(ref _Ref11, value);
        }

        private string _Ref12;
        public string Ref12
        {
            get => _Ref12;
            set => Set(ref _Ref12, value);
        }

        private string _Ref13;
        public string Ref13
        {
            get => _Ref13;
            set => Set(ref _Ref13, value);
        }

        private string _Ref14;
        public string Ref14
        {
            get => _Ref14;
            set => Set(ref _Ref14, value);
        }

        private string _Ref15;
        public string Ref15
        {
            get => _Ref15;
            set => Set(ref _Ref15, value);
        }

        private string _Ref16;
        public string Ref16
        {
            get => _Ref16;
            set => Set(ref _Ref16, value);
        }

        private string _Ref17;
        public string Ref17
        {
            get => _Ref17;
            set => Set(ref _Ref17, value);
        }

        private string _Ref18;
        public string Ref18
        {
            get => _Ref18;
            set => Set(ref _Ref18, value);
        }

        private string _Ref19;
        public string Ref19
        {
            get => _Ref19;
            set => Set(ref _Ref19, value);
        }

        private string _Ref20;
        public string Ref20
        {
            get => _Ref20;
            set => Set(ref _Ref20, value);
        }

        public override string ToString()
        {
            return Nm;
        }
    }
    public interface IFrwCdeRepo
    {
        List<FrwCde> GetFrwCdes(string frwId, string div);
        List<FrwCde> GetFrwCdes(string frwId, string pCd, string div);
        void Add(FrwCde frwCde);
        void Update(FrwCde frwCde);
        void Delete(FrwCde frwCde);
    }

    public class FrwCdeRepo : IFrwCdeRepo
    {
        public List<FrwCde> GetCdes(string frwId)
        {
            string sql = @"
select a.FrwId, a.Cd, a.PCd, a.SubCd, a.Nm,
       a.UseYn, a.Ref01, a.Ref02, a.Ref03, a.Ref04,
       a.Ref05, a.Ref06, a.Ref07, a.Ref08, a.Ref09,
       a.Ref10, a.Ref11, a.Ref12, a.Ref13, a.Ref14,
       a.Ref15, a.Ref16, a.Ref17, a.Ref18, a.Ref19,
       a.Ref20
  from FRWCDE a
 where 1=1
   and a.Cd = @Cd
   and a.FrwId = @FrwId
   and isnull(a.SubCd,'') = ''
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwCde>(sql, new { FrwId = frwId }).ToList();

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

        public List<FrwCde> GetFrwCdes(string frwId)
        {
            string sql = @"
select a.FrwId, a.Cd, a.PCd, a.SubCd, a.Nm,
       a.UseYn, a.Ref01, a.Ref02, a.Ref03, a.Ref04,
       a.Ref05, a.Ref06, a.Ref07, a.Ref08, a.Ref09,
       a.Ref10, a.Ref11, a.Ref12, a.Ref13, a.Ref14,
       a.Ref15, a.Ref16, a.Ref17, a.Ref18, a.Ref19,
       a.Ref20
  from FRWCDE a
 where 1=1
   and a.FrwId = @FrwId
   and a.Cd = @Cd
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwCde>(sql, new { FrwId = frwId}).ToList();

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
        public List<FrwCde> GetFrwCdes(string frwId, string pCd, string div)
        {
            string sql;
            if (div == "SubCd")
            {
                sql = @"
select a.SubCd, a.Nm
  from FRWCDE a
 where 1=1
   and a.FrwId = @FrwId
   and a.PCd = @PCd
";
            }
            else
            {
                sql = @"
select a.Cd, a.Nm
  from FRWCDE a
 where 1=1
   and a.FrwId = @FrwId
   and a.PCd = @PCd
";
            }
            
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwCde>(sql, new { FrwId = frwId, PCd = pCd }).ToList();

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

        public void Add(FrwCde frwCde)
        {
            string sql = @"
insert into FRWCDE
      (FrwId, Cd, PCd, SubCd, Nm,
       UseYn, Ref01, Ref02, Ref03, Ref04,
       Ref05, Ref06, Ref07, Ref08, Ref09,
       Ref10, Ref11, Ref12, Ref13, Ref14,
       Ref15, Ref16, Ref17, Ref18, Ref19,
       Ref20, CId, CDt, MId, MDt)
select @FrwId, @Cd, @PCd, @SubCd, @Nm,
       @UseYn, @Ref01, @Ref02, @Ref03, @Ref04,
       @Ref05, @Ref06, @Ref07, @Ref08, @Ref09,
       @Ref10, @Ref11, @Ref12, @Ref13, @Ref14,
       @Ref15, @Ref16, @Ref17, @Ref18, @Ref19,
       @Ref20, 
       " + Common.GetValue("gRegId") + @", getdate(), " + Common.GetValue("gRegId") + @", getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frwCde);
            }
        }

        public void Delete(FrwCde frwCde)
        {
            string sql = @"
delete
  from FRWCDE
 where 1=1
   and FrwId = @FrwId
   and Cd = @Cd
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frwCde);
            }
        }


        public void Update(FrwCde frwCde)
        {
            string sql = @"

update a
   set FrwId= @FrwId,
       Cd= @Cd,
       PCd= @PCd,
       SubCd= @SubCd,
       Nm= @Nm,
       UseYn= @UseYn,
       Ref01= @Ref01,
       Ref02= @Ref02,
       Ref03= @Ref03,
       Ref04= @Ref04,
       Ref05= @Ref05,
       Ref06= @Ref06,
       Ref07= @Ref07,
       Ref08= @Ref08,
       Ref09= @Ref09,
       Ref10= @Ref10,
       Ref11= @Ref11,
       Ref12= @Ref12,
       Ref13= @Ref13,
       Ref14= @Ref14,
       Ref15= @Ref15,
       Ref16= @Ref16,
       Ref17= @Ref17,
       Ref18= @Ref18,
       Ref19= @Ref19,
       Ref20= @Ref20,
       MId= " + Common.GetValue("gRegId") + @",
       MDt= getdate()
  from FRWCDE a
 where 1=1
   and FrwId = @FrwId
   and Cd = @Cd
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, frwCde);
            }
        }

        public List<FrwCde> GetCdNm(string frwId, string pCd)
        {
            throw new NotImplementedException();
        }

        public List<FrwCde> GetSubCdNm(string frwId, string pCd)
        {
            throw new NotImplementedException();
        }

        public List<FrwCde> GetFrwCdes(string frwId, string div)
        {
            throw new NotImplementedException();
        }
        public List<FrwCde> GetFrwCdesForCodeBox(string frwId, string pCd)
        {
            string sql = @"
select a.FrwId, a.Cd, a.PCd, a.SubCd, a.Nm,
       a.UseYn, a.Ref01, a.Ref02, a.Ref03, a.Ref04,
       a.Ref05, a.Ref06, a.Ref07, a.Ref08, a.Ref09,
       a.Ref10, a.Ref11, a.Ref12, a.Ref13, a.Ref14,
       a.Ref15, a.Ref16, a.Ref17, a.Ref18, a.Ref19,
       a.Ref20
  from FRWCDE a
 where 1=1
   and a.FrwId = @FrwId
   and a.PCd = @PCd
";
            using (var db = new GaiaHelper())
            {
                List<FrwCde>? result = db.Query<FrwCde>(sql, new { FrwId = frwId, PCd = pCd }).ToList();
                return (result == null)? null : result;

                //if (result == null)
                //{
                //    return null;
                //}
                //else
                //{
                //    return result;
                //}
            }
        }
    }
}

```

```C#
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class FrwFrm : MdlBase
    {
        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _FrmNm;
        public string FrmNm
        {
            get => _FrmNm;
            set => Set(ref _FrmNm, value);
        }

        private int _UsrRegId;
        public int UsrRegId
        {
            get => _UsrRegId;
            set => Set(ref _UsrRegId, value);
        }

        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        // 이 부분은 사용되지 않는 코드입니다. 작업환경이 자주 달라지는 경우 DLL File Path가 달라져 문제가 발생할 수 있다.
        // D:\00_WorkSpace\EpicPrologue\Frms\CTRLMST\bin\Debug\net8.0-windows
        // WorkPath = D:\00_WorkSpace\
        // FilePath = EpicPrologue\Frms\CTRLMST\bin\Debug\net8.0-windows
        // 위와 같이 분리하면 사용자는 작업공간을 선택하여 쉽게 작업이 가능하다.
        //private string _WorkPath; 
        //public string WorkPath
        //{
        //    get => _WorkPath;
        //    set => Set(ref _WorkPath, value);
        //}

        private string _FilePath;
        public string FilePath
        {
            get => _FilePath;
            set => Set(ref _FilePath, value);
        }

        private string _FileNm;
        public string FileNm
        {
            get => _FileNm;
            set => Set(ref _FileNm, value);
        }

        private string _NmSpace;
        public string NmSpace
        {
            get => _NmSpace;
            set => Set(ref _NmSpace, value);
        }

        private bool _FldYn;
        public bool FldYn
        {
            get => _FldYn;
            set => Set(ref _FldYn, value);
        }

        private string _PId;
        public string PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }
        //-------------------------------------------
        //-------------------------------------------
        public override string ToString()
        {
            return FrmNm;
        }
    }
    public interface IFrwFrmRepo
    {
        bool ChkByFrm(string frmId);
        FrwFrm GetByFrmId(string frwId,string frmId);
        List<FrwFrm> GetAll();
        void Add(FrwFrm frmMst);
        void Update(FrwFrm frmMst);
        void Delete(string frmId);

    }
    public class FrwFrmRepo : IFrwFrmRepo
    {
        public bool ChkByFrm(string frmId)
        {
            string sql = @"
select a.FrmId, a.FrmNm, a.UsrRegId, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt
  from FRWFRM a
 where 1=1
   and a.FrmId = @FrmId
 order by FrmNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwFrm>(sql, new { FrmId = frmId }).FirstOrDefault();
                return result == null;
            }
        }
        public List<MdlNmSpace> GetMdlNmSpace(string frwId)
        {
            string sql = @"
select a.FrmId, NmSpace=concat(a.NmSpace,'_',upper(b.WrkId))
  from FRWFRM a
  join FRMWRK b on a.FrwId=b.FrwId and a.FrmId=b.FrmId
 where 1=1
   and a.FrwId = @FrwId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<MdlNmSpace>(sql, new { FrwId = frwId }).ToList();
                return result;
            }
        }

        public List<FrwFrm> GetByOwnFrw(int ownId, string frwId)
        {
            string sql = @"
select a.FrmId, a.FrmNm, a.UsrRegId, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt
  from FRWFRM a
 where 1=1
   and a.UsrRegId = @UsrRegId
   and a.FrwId = @FrwId
 order by FrmNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwFrm>(sql, new { FrwId = frwId, UsrRegId = ownId }).ToList();
                foreach (var item in result)
                {
                    item.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정
                }
                return result;
            }
        }

        public FrwFrm GetByFrmId(string frwId, string frmId)
        {
            string sql = @"
select a.FrmId, a.FrmNm, a.UsrRegId, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt
  from FRWFRM a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwFrm>(sql, new { FrwId = frwId, FrmId = frmId }).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                else 
                {
                    result.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정
                    return result;
                }
            }
        }
        public List<FrwFrm> GetAll()
        {
            string sql = @"
select a.FrmId, a.FrmNm, a.UsrRegId, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt
  from FRWFRM a
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<FrwFrm>(sql).ToList();

                foreach (var item in result)
                {
                    item.ChangedFlag = MdlState.None;  // 객체 상태를 None으로 설정
                }
                return result;
            }
        }

        public void Add(FrwFrm frmMst)
        {
            string sql = @"
insert into FRWFRM
      (FrmId, FrmNm, UsrRegId, FrwId, FilePath,
       FileNm, NmSpace, FldYn, PId, Memo,
       CId, CDt, MId, MDt)
select @FrmId, @FrmNm, @UsrRegId, @FrwId, @FilePath,
       @FileNm, @NmSpace, @FldYn, @PId, @Memo,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, frmMst);
            }
        }

        public void Update(FrwFrm frmMst)
        {
            string sql = @"
update a
   set FrmId= @FrmId,
       FrmNm= @FrmNm,
       UsrRegId= @UsrRegId,
       FrwId= @FrwId,
       FilePath= @FilePath,
       FileNm= @FileNm,
       NmSpace= @NmSpace,
       FldYn= @FldYn,
       PId= @PId,
       Memo= @Memo,
       MId= @MId,
       MDt= getdate()
  from FRWFRM a
 where 1=1
   and FrmId = @FrmId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, frmMst);
            }
        }

        public void Delete(string frmId)
        {
            string sql = @"
delete
  from FRWFRM
 where 1=1
   and FrmId = @FrmId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, new { FrmId = frmId });
            }
        }
    }
}

```

```C#
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class IdNm
    {
        public string Id { get; set; }
        public string Nm { get; set; }

        public override string ToString()
        {
            return Nm;
        }
    }
    public class IdNmRepo
    {
        public List<IdNm> GetLookUp(string query)
        {
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<IdNm>(query).ToList();
                return result;
            }
        }
    }

    public class IdObject
    {
        public string Txt { get; set; }
        public object Obj { get; set; }

        public override string ToString()
        {
            return Txt;
        }
    }
    public interface IIdObjectRepo
    {
        string Txt { get; set; }
        object Obj { get; set; }
    }
    public class IdobjectRepo : IIdObjectRepo
    {
        public string Txt { get; set; }
        public object Obj { get; set; }
    }


    public class IdObject<T> : IdObject
    {
        public new T Obj { get; set; }
    }
    public class IdObject<T, V01> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
    }
    public class IdObject<T, V01, V02> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
    }
    public class IdObject<T, V01, V02, V03> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
    }
        public class IdObject<T, V01, V02, V03, V04, V05> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04, V05, V06> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
        public V06 Val06 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04, V05, V06, V07> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
        public V06 Val06 { get; set; }
        public V07 Val07 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04, V05, V06, V07, V08> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
        public V06 Val06 { get; set; }
        public V07 Val07 { get; set; }
        public V08 Val08 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04, V05, V06, V07, V08, V09> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
        public V06 Val06 { get; set; }
        public V07 Val07 { get; set; }
        public V08 Val08 { get; set; }
        public V09 Val09 { get; set; }
    }
    public class IdObject<T, V01, V02, V03, V04, V05, V06, V07, V08, V09, V10> : IdObject
    {
        public new T Obj { get; set; }
        public V01 Val01 { get; set; }
        public V02 Val02 { get; set; }
        public V03 Val03 { get; set; }
        public V04 Val04 { get; set; }
        public V05 Val05 { get; set; }
        public V06 Val06 { get; set; }
        public V07 Val07 { get; set; }
        public V08 Val08 { get; set; }
        public V09 Val09 { get; set; }
        public V10 Val10 { get; set; }
    }

}

```

```C#
using Lib;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Repo
{
    public class PrjFrw : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrwNm;
        public string FrwNm
        {
            get => _FrwNm;
            set => Set(ref _FrwNm, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

        private string _Ver;
        public string Ver
        {
            get => _Ver;
            set => Set(ref _Ver, value);
        }

        private string _PId;
        public string PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }

        public override string ToString()
        {
            return FrwNm;
        }
    }
    public interface IPrjFrwRepo
    {
        PrjFrw GetById(string id);
        List<PrjFrw> GetAll();
        void Add(PrjFrw frmWrk);
        void Update(PrjFrw frmWrk);
        void Delete(string id);
    }
    public class PrjFrwRepo : IPrjFrwRepo
    {
        public void Add(PrjFrw frmWrk)
        {
            string sql = @"
insert into PRJFRW
      (FrwId, FrwNm, Memo, Ver, PId,
       CId, CDt, MId, Mdt)
select @FrwId, @FrwNm, @Memo, @Ver, @PId,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, frmWrk);
            }
        }

        public void Delete(string id)
        {
            string sql = @"
delete
  from PRJFRW
 where 1=1
   and FrwId = @FrwId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, new { FrwId = id });
            }
        }

        public List<PrjFrw> GetAll()
        {
            string sql = @"
select a.FrwId, a.FrwNm, a.Memo, a.Ver, a.PId,
       a.CId, a.CDt, a.MId, a.Mdt
  from PRJFRW a
";
            using (var db = new GaiaHelper())
            {
                return db.Query<PrjFrw>(sql).ToList();
            }
        }

        public PrjFrw GetById(string id)
        {
            string sql = @"
select a.FrwId, a.FrwNm, a.Memo, a.Ver, a.PId,
       a.CId, a.CDt, a.MId, a.Mdt
  from PRJFRW a
 where 1=1
   and a.FrwId = @FrwId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<PrjFrw>(sql, new { FrwId = id }).FirstOrDefault();
                return result;
            }
        }

        public void Update(PrjFrw frmWrk)
        {
            string sql = @"
update a
   set FrwNm= @FrwNm,
       Memo= @Memo,
       Ver= @Ver,
       PId= @PId,
       MId= @MId,
       Mdt= getdate()
  from PRJFRW a
 where 1=1
   and FrwId = @FrwId_old
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, frmWrk);
            }

        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class PrjMst : MdlBase
    {
        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private string _Nm;
        public string Nm
        {
            get => _Nm;
            set => Set(ref _Nm, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

        private long _PId;
        public long PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }
    }
    public interface IPrjMstRepo
    {
        PrjMst GetByPrjId(long PrjId);
        List<PrjMst> GetAll();
        void Add(PrjMst prjMst);
        void Update(PrjMst prjMst);
        void Delete(PrjMst prjMst);
    }
    public class PrjMstRepo : IPrjMstRepo
    {
        public List<PrjMst> GetAll()
        {
            string sql = @"
select a.Id, a.Nm, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from PRJMST a
 ";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<PrjMst>(sql).ToList();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record was not found.");
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

        public PrjMst GetByPrjId(long prjId)
        {
            string sql = @"
select a.Id, a.Nm, a.Memo, a.PId, a.CId,
       a.CDt, a.MId, a.MDt
  from PRJMST a
 where 1=1
   and a.Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<PrjMst>(sql, new { PrjId = prjId}).SingleOrDefault();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {prjId} was not found.");
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }

        public void Add(PrjMst prjMst)
        {
            string sql = @"
insert into PRJMST
      (Id, Nm, Memo, PId, CId,
       CDt, MId, MDt)
select @Id, @Nm, @Memo, @PId,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, prjMst);
            }
        }

        public void Delete(PrjMst prjMst)
        {
            string sql = @"
delete
  from PRJMST
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, prjMst);
            }
        }

        public void Update(PrjMst prjMst)
        {
            string sql = @"
update a
   set Nm= @Nm,
       Memo= @Memo,
       PId= @PId,
       MId= @MId,
       MDt= getdate()
  from PRJMST a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, prjMst);
            }
        }
    }
}

```

```C#
using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class UsrMst : MdlBase
    {
        private int _UsrRegId;
        public int UsrRegId
        {
            get => _UsrRegId;
            set => Set(ref _UsrRegId, value);
        }

        private string _UsrId;
        public string UsrId
        {
            get => _UsrId;
            set => Set(ref _UsrId, value);
        }

        private string _UsrNm;
        public string UsrNm
        {
            get => _UsrNm;
            set => Set(ref _UsrNm, value);
        }

        private string _Pwd;
        public string Pwd
        {
            get => _Pwd;
            set => Set(ref _Pwd, value);
        }

        private string _Cls;
        public string Cls
        {
            get => _Cls;
            set => Set(ref _Cls, value);
        }

    }
    public interface IUsrRepo
    {
        UsrMst GetById(int uid);
        UsrMst GetByUsrId(string uid);
        UsrMst CheckSignIn(string id, string pwd);
        List<UsrMst> GetAll();
        void Add(UsrMst usr);
        void Update(UsrMst usr);
        void Delete(int uid);
    }
    public class UsrRepo : IUsrRepo
    {
        public void Add(UsrMst usr)
        {
            string sql = @"
insert into USRMST
      (UsrId, UsrNm, Pwd, Cls,
       CId, CDt, MId, MDt)
select @UsrId, @UsrNm, @Pwd, @Cls,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, usr);
            }
        }

        public UsrMst CheckSignIn(string id, string pwd)
        {
            string sql = @"
select a.UsrRegId, a.UsrId, a.UsrNm, a.Pwd, a.Cls,
       a.CId, a.CDt, a.MId, a.MDt
  from USRMST a
 where 1=1
   and a.UsrId = @UsrId
   and a.Pwd = @Pwd
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<UsrMst>(sql, new { UsrId = id, Pwd = pwd }).FirstOrDefault();
                return result;
            }
        }

        public void Delete(int uid)
        {
            string sql = @"
delete
  from USRMST
 where 1=1
   and UsrRegId = @id
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, new { id = uid });
            }
        }

        public List<UsrMst> GetAll()
        {
            string sql = @"
select a.UsrRegId, a.UsrId, a.UsrNm, a.Pwd, a.Cls,
       a.CId, a.CDt, a.MId, a.MDt
  from USRMST a
";
            using (var db = new GaiaHelper())
            {
                return db.Query<UsrMst>(sql).ToList();
            }
        }

        public UsrMst GetById(int uid)
        {
            string sql = @"
select a.UsrRegId, a.UsrId, a.UsrNm, a.Pwd, a.Cls,
       a.CId, a.CDt, a.MId, a.MDt
  from USRMST a
 where 1=1
   and a.UsrRegId = @UsrRegId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<UsrMst>(sql, new { UsrRegId = uid }).FirstOrDefault();
                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {uid} was not found.");
                }
                return result;
            }
        }

        public UsrMst GetByUsrId(string uid)
        {
            string sql = @"
select a.UsrRegId, a.UsrId, a.UsrNm, a.Pwd, a.Cls,
       a.CId, a.CDt, a.MId, a.MDt
  from USRMST a
 where 1=1
   and a.UsrId = @UId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<UsrMst>(sql, new { UsrId = uid }).FirstOrDefault();
                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {uid} was not found.");
                }
                return result;
            }
        }

        public void Update(UsrMst usr)
        {
            string sql = @"
update a
   set UsrId= @UsrId,
       UsrNm= @UsrNm,
       Pwd= @Pwd,
       Cls= @Cls,
       MId= @MId,
       MDt= getdate()
  from USRMST a
 where 1=1
   and UsrRegId = @UsrRegId
";
            using (var db = new GaiaHelper())
            {
                db.OpenExecute(sql, usr);
            }
        }
    }

}

```

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class WrkFld : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _CtrlNm;
        public string CtrlNm
        {
            get => _CtrlNm;
            set => Set(ref _CtrlNm, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _CtrlCls;
        public string CtrlCls
        {
            get => _CtrlCls;
            set => Set(ref _CtrlCls, value);
        }

        private string _FldNm;
        public string FldNm
        {
            get => _FldNm;
            set => Set(ref _FldNm, value);
        }

        private string _FldTy;
        public string FldTy
        {
            get => _FldTy;
            set => Set(ref _FldTy, value);
        }

        private int _FldX;
        public int FldX
        {
            get => _FldX;
            set => Set(ref _FldX, value);
        }

        private int _FldY;
        public int FldY
        {
            get => _FldY;
            set => Set(ref _FldY, value);
        }

        private int _FldWidth;
        public int FldWidth
        {
            get => _FldWidth;
            set => Set(ref _FldWidth, value);
        }

        private int _FldHeight;
        public int FldHeight
        {
            get => _FldHeight;
            set => Set(ref _FldHeight, value);
        }

        private int _FldTitleWidth;
        public int FldTitleWidth
        {
            get => _FldTitleWidth;
            set => Set(ref _FldTitleWidth, value);
        }

        private string _FldTitle;
        public string FldTitle
        {
            get => _FldTitle;
            set => Set(ref _FldTitle, value);
        }

        private string _TitleAlign;
        public string TitleAlign
        {
            get => _TitleAlign;
            set => Set(ref _TitleAlign, value);
        }

        private string _Popup;
        public string Popup
        {
            get => _Popup;
            set => Set(ref _Popup, value);
        }

        private string _DefaultText;
        public string DefaultText
        {
            get => _DefaultText;
            set => Set(ref _DefaultText, value);
        }

        private string _TextAlign;
        public string TextAlign
        {
            get => _TextAlign;
            set => Set(ref _TextAlign, value);
        }

        private bool _FixYn;
        public bool FixYn
        {
            get => _FixYn;
            set => Set(ref _FixYn, value);
        }

        private bool _GroupYn;
        public bool GroupYn
        {
            get => _GroupYn;
            set => Set(ref _GroupYn, value);
        }

        private bool _ShowYn;
        public bool ShowYn
        {
            get => _ShowYn;
            set => Set(ref _ShowYn, value);
        }

        private bool _NeedYn;
        public bool NeedYn
        {
            get => _NeedYn;
            set => Set(ref _NeedYn, value);
        }

        private bool _EditYn;
        public bool EditYn
        {
            get => _EditYn;
            set => Set(ref _EditYn, value);
        }

        private string _Band1;
        public string Band1
        {
            get => _Band1;
            set => Set(ref _Band1, value);
        }

        private string _Band2;
        public string Band2
        {
            get => _Band2;
            set => Set(ref _Band2, value);
        }

        private string _FuncStr;
        public string FuncStr
        {
            get => _FuncStr;
            set => Set(ref _FuncStr, value);
        }

        private string _FormatStr;
        public string FormatStr
        {
            get => _FormatStr;
            set => Set(ref _FormatStr, value);
        }

        private string _ColorFont;
        public string ColorFont
        {
            get => _ColorFont;
            set => Set(ref _ColorFont, value);
        }

        private string _ColorBg;
        public string ColorBg
        {
            get => _ColorBg;
            set => Set(ref _ColorBg, value);
        }

        private string _ToolNm;
        public string ToolNm
        {
            get => _ToolNm;
            set => Set(ref _ToolNm, value);
        }

        private int _Seq;
        public int Seq
        {
            get => _Seq;
            set => Set(ref _Seq, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }
        //----------------------------------------
        //----------------------------------------
        public override string ToString()
        {
            return $"{FldNm} / {FldTitle} / {CtrlCls} / {ToolNm}";
        }
    }
    public interface IWrkFldRepo
    {
        void Add(WrkFld wrkFld);
        void Update(WrkFld wrkFld);
        void Delete(WrkFld wrkFld);
    }
    public class WrkFldRepo : IWrkFldRepo
    {
        public WrkFld GetTabPageProperties(string frwId, string frmId, string tabPageNm)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.CtrlCls,
       a.FldNm, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight,
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.FldNm = @TabPageNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, TabPageNm = tabPageNm }).SingleOrDefault();

                if (result == null)
                {
                    return null;
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{tabPageNm} was not found.");
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }
        public List<WrkFld> GetCdeProperties(string frwId, string frmId, string wrkId, string cd)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.FldNm,
       a.CtrlCls, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight,
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.Memo, a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where FrwId = @FrwId
   and FrmId = @FrmId
   and WrkId = @WrkId
   and CtrlNm not like 'grdDtl.Ref%'
 union all
select FrwId, FrmId=@FrmId, CtrlNm=concat(@WrkId,'.',RefNo), WrkId=@WrkId, FldNm=RefNo,
       CtrlCls='Column', FldTy, FldX=0, FldY=0, FldWidth=0, FldHeight=0,
       FldTitleWidth=120, FldTitle, TitleAlign=null, Popup, DefaultText=null,
       TextAlign=null, FixYn='0', GroupYn='0', ShowYn='1', NeedYn='0',
       EditYn='1', Band1=null, Band2=null, FuncStr=null, FormatStr=null,
       ColorFont=null, ColorBg=null, ToolNm=null, 
       Seq=(ROW_NUMBER() over(order by RefNo))+10000, 
       Id=null,
       Memo, CId, CDt, MId, MDt
  from CDEREF a
 where 1=1
   and a.FrwId = @FrwId
   and a.Cd = @Cd
 order by seq
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId, Cd = cd }).ToList();

                if (result == null)
                {
                    return null;
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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
        public List<WrkFld> GetColumnProperties(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.CtrlCls,
       a.FldNm, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight,
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
   and a.CtrlCls = 'Column'
 order by a.Seq, a.Id
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    return null;
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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

        public List<WrkFld> GetFldsProperties(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.CtrlCls,
       a.FldNm, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight,
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

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

        public WrkFld GetFldProperties(string frwId, string frmId, string ctrlNm)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.CtrlCls,
       a.FldNm, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight,
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.CtrlNm = @CtrlNm
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, CtrlNm = ctrlNm }).SingleOrDefault();

                if (result == null)
                {
                    return null;
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{ctrlNm} was not found.");
                }
                else
                {
                    result.ChangedFlag = MdlState.None;
                    return result;
                }
            }
        }

        public List<WrkFld> GetByFrwFrmWrk(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.CtrlNm, a.WrkId, a.CtrlCls,
       a.FldNm, a.FldTy, a.FldX, a.FldY, a.FldWidth, a.FldHeight, 
       a.FldTitleWidth, a.FldTitle, a.TitleAlign, a.Popup, a.DefaultText,
       a.TextAlign, a.FixYn, a.GroupYn, a.ShowYn, a.NeedYn,
       a.EditYn, a.Band1, a.Band2, a.FuncStr, a.FormatStr,
       a.ColorFont, a.ColorBg, a.ToolNm, a.Seq, a.Id,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKFLD a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new GaiaHelper())
            {
                var result = db.Query<WrkFld>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    return null;
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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

        public void Add(WrkFld wrkFld)
        {
            string sql = @"
insert into WRKFLD
      (FrwId, FrmId, CtrlNm, WrkId, CtrlCls,
       FldNm, FldTy, FldX, FldY, FldWidth, FldHeight, 
       FldTitleWidth, FldTitle, TitleAlign, Popup, DefaultText,
       TextAlign, FixYn, GroupYn, ShowYn, NeedYn,
       EditYn, Band1, Band2, FuncStr, FormatStr,
       ColorFont, ColorBg, ToolNm, Seq,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @CtrlNm, @WrkId, @CtrlCls,
       @FldNm, @FldTy, @FldX, @FldY, @FldWidth, @FldHeight, 
       @FldTitleWidth, @FldTitle, @TitleAlign, @Popup, @DefaultText,
       @TextAlign, @FixYn, @GroupYn, @ShowYn, @NeedYn,
       @EditYn, @Band1, @Band2, @FuncStr, @FormatStr,
       @ColorFont, @ColorBg, @ToolNm, @Seq,
       100020, getdate(), 100020, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkFld);
            }
        }

        public void Update(WrkFld wrkFld)
        {
            string sql = @"
update a
   set FrwId= @FrwId,
       FrmId= @FrmId,
       CtrlNm= @CtrlNm,
       WrkId= @WrkId,
       CtrlCls= @CtrlCls,
       FldNm= @FldNm,
       FldTy= @FldTy,
       FldX= @FldX,
       FldY= @FldY,
       FldWidth= @FldWidth,
       FldHeight= @FldHeight,
       FldTitleWidth= @FldTitleWidth,
       FldTitle= @FldTitle,
       TitleAlign= @TitleAlign,
       Popup= @Popup,
       DefaultText= @DefaultText,
       TextAlign= @TextAlign,
       FixYn= @FixYn,
       GroupYn= @GroupYn,
       ShowYn= @ShowYn,
       NeedYn= @NeedYn,
       EditYn= @EditYn,
       Band1= @Band1,
       Band2= @Band2,
       FuncStr= @FuncStr,
       FormatStr= @FormatStr,
       ColorFont= @ColorFont,
       ColorBg= @ColorBg,
       ToolNm= @ToolNm,
       Seq= @Seq,
       MId= <$gRegId>,
       MDt= getdate()
  from WRKFLD a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkFld);
            }
        }

        public void Delete(WrkFld wrkFld)
        {
            string sql = @"
delete
  from WRKFLD
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkFld);
            }
        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class WrkGet : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _FldNm;
        public string FldNm
        {
            get => _FldNm;
            set => Set(ref _FldNm, value);
        }

        private string _GetWrkId;
        public string GetWrkId
        {
            get => _GetWrkId;
            set => Set(ref _GetWrkId, value);
        }

        private string _GetFldNm;
        public string GetFldNm
        {
            get => _GetFldNm;
            set => Set(ref _GetFldNm, value);
        }

        private string _GetDefalueValue;
        public string GetDefalueValue
        {
            get => _GetDefalueValue;
            set => Set(ref _GetDefalueValue, value);
        }

        private string _SqlId;
        public string SqlId
        {
            get => _SqlId;
            set => Set(ref _SqlId, value);
        }

        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private long _PId;
        public long PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }

    }
    public interface IWrkGetRepo
    {
        List<WrkGet> GetPullFlds(string frwId, string frmId, string wrkId);
        void Add(WrkGet wrkGet);
        void Update(WrkGet wrkGet);
        void Delete(WrkGet wrkGet);
    }
    public class WrkGetRepo : IWrkGetRepo
    {
        public List<WrkGet> GetPullFlds(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.FldNm, a.GetWrkId,
       a.GetFldNm, a.GetDefalueValue, a.SqlId, a.Id, a.PId,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKGET a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkGet>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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
        public List<IdNm> GetPullFlds(object param)
        {
            string sql = @"
select Id = a.FldNm, 
       Nm = isnull(a.GetFldNm,isnull(a.GetDefalueValue,''))
  from WRKGET a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<IdNm>(sql, param).ToList();
                return result;
            }
        }

        public void Add(WrkGet wrkGet)
        {
            string sql = @"
insert into WRKGET
      (FrwId, FrmId, WrkId, FldNm, GetWrkId,
       GetFldNm, GetDefalueValue, SqlId, PId,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @FldNm, @GetWrkId,
       @GetFldNm, @GetDefalueValue, @SqlId, @PId,
       " + Common.GetValue("gRegId") + @", getdate(), " + Common.GetValue("gRegId") + @", getdate() 
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkGet);
            }
        }

        public void Update(WrkGet wrkGet)
        {
            string sql = @"
update a
   set FldNm= @FldNm,
       GetWrkId= @GetWrkId,
       GetFldNm= @GetFldNm,
       GetDefalueValue= @GetDefalueValue,
       SqlId= @SqlId,
       PId= @PId,
       MId= " + Common.GetValue("gRegId") + @",
       MDt= getdate()
  from WRKGET a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkGet);
            }
        }

        public void Delete(WrkGet wrkGet)
        {
            string sql = @"
delete
  from WRKGET
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkGet);
            }
        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class WrkRef : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _FldNm;
        public string FldNm
        {
            get => _FldNm;
            set => Set(ref _FldNm, value);
        }

        private string _RefWrkId;
        public string RefWrkId
        {
            get => _RefWrkId;
            set => Set(ref _RefWrkId, value);
        }

        private string _RefFldNm;
        public string RefFldNm
        {
            get => _RefFldNm;
            set => Set(ref _RefFldNm, value);
        }

        private string _RefDefalueValue;
        public string RefDefalueValue
        {
            get => _RefDefalueValue;
            set => Set(ref _RefDefalueValue, value);
        }

        private string _SqlId;
        public string SqlId
        {
            get => _SqlId;
            set => Set(ref _SqlId, value);
        }

        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private long _PId;
        public long PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }

    }
    public interface IWrkRefRepo
    {
        List<WrkRef> RefDataFlds(string frwId, string frmId, string wrkId);
        void Add(WrkRef wrkRef);
        void Update(WrkRef wrkRef);
        void Delete(WrkRef wrkRef);
    }
    public class WrkRefRepo : IWrkRefRepo
    {
        public List<WrkRef> RefDataFlds(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.FldNm, a.RefWrkId,
       a.RefFldNm, a.RefDefalueValue, a.SqlId, a.Id, a.PId,
       a.CId, a.CDt, a.MId, a.MDt
  from WRKREF a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkRef>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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
        public List<IdNm> RefFlds(object param)
        {
            string sql = @"
select Id = a.RefFldNm, 
       Nm = a.RefDefalueValue
  from WRKREF a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<IdNm>(sql, param).ToList();
                return result;
            }
        }

        public void Add(WrkRef wrkRef)
        {
            string sql = @"
insert into WRKREF
      (FrwId, FrmId, WrkId, FldNm, RefWrkId,
       RefFldNm, RefDefalueValue, SqlId, PId,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @FldNm, @RefWrkId,
       @RefFldNm, @RefDefalueValue, @SqlId, @PId,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkRef);
            }
        }

        public void Update(WrkRef wrkRef)
        {
            string sql = @"
update a
   set FldNm= @FldNm,
       RefWrkId= @RefWrkId,
       RefFldNm= @RefFldNm,
       RefDefalueValue= @RefDefalueValue,
       SqlId= @SqlId,
       PId= @PId,
       MId= @MId,
       MDt= getdate()
  from WRKREF a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkRef);
            }
        }

        public void Delete(WrkRef wrkRef)
        {
            string sql = @"
delete
  from WRKREF
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkRef);
            }
        }
    }
}

```

```C#
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class WrkSet : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _FldNm;
        public string FldNm
        {
            get => _FldNm;
            set => Set(ref _FldNm, value);
        }

        private string _SetWrkId;
        public string SetWrkId
        {
            get => _SetWrkId;
            set => Set(ref _SetWrkId, value);
        }

        private string _SetFldNm;
        public string SetFldNm
        {
            get => _SetFldNm;
            set => Set(ref _SetFldNm, value);
        }

        private string _SetDefaultValue;
        public string SetDefaultValue
        {
            get => _SetDefaultValue;
            set => Set(ref _SetDefaultValue, value);
        }

        private string _SqlId;
        public string SqlId
        {
            get => _SqlId;
            set => Set(ref _SqlId, value);
        }

        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private long _Pid;
        public long Pid
        {
            get => _Pid;
            set => Set(ref _Pid, value);
        }
        //------------------------------
        //FRMCTRL, WRKFLD
        //------------------------------
        private string _ToolNm;
        public string ToolNm
        {
            get => _ToolNm;
            set => Set(ref _ToolNm, value);
        }
    }
    public interface IWrkSetRepo
    {
        List<WrkSet> SetPushFlds(string frwId, string frmId, string wrkId);
        void Add(WrkSet wrkSet);
        void Update(WrkSet wrkSet);
        void Delete(WrkSet wrkSet);
    }
    public class WrkSetRepo : IWrkSetRepo
    {
        public List<WrkSet> GetTargetList(string frwId, string frmId) 
        {
            string sql = @"
select a.FrwId, a.FrmId, WrkId=null, FldNm=null, SetWrkId=WrkId,
       SetFldNm=FldNm, SetDefaultValue=null, SqlId=null, Pid=null, 
       ToolNm
  from WRKFLD a
 where 1=1
   and FrwId = @FrwId
   and FrmId = @FrmId
   and ToolNm in (select CtrlNm from CTRLMST where CtrlGrpCd='Bind')
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkSet>(sql, new { FrwId = frwId, FrmId = frmId }).ToList();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.ChangedFlag = MdlState.None;
                    }
                    return result;
                }
                else { return null; }
            }
        }
        public List<WrkSet> SetPushFlds(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.FldNm, a.SetWrkId,
       a.SetFldNm, a.SetDefaultValue, a.SqlId, a.Id, a.Pid, 
       ToolNm = 'UCTextBox',
       a.CId, a.CDt, a.MId, a.MDt
  from WRKSET a
 where 1=1
   and a.FrmId = @FrmId
   and a.FrwId = @FrwId
   and a.WrkId = @WrkId
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkSet>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    //throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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
        public void Add(WrkSet wrkSet)
        {
            string sql = @"
insert into WRKSET
      (FrwId, FrmId, WrkId, FldNm, SetWrkId,
       SetFldNm, SetDefaultValue, SqlId, Pid,
       CId, CDt, MId, MDt)
select @FrwId, @FrmId, @WrkId, @FldNm, @SetWrkId,
       @SetFldNm, @SetDefaultValue, @SqlId, @Pid,
       @CId, getdate(), @MId, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSet);
            }
        }

        public void Delete(WrkSet wrkSet)
        {
            string sql = @"
delete
  from WRKSET
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSet);
            }
        }


        public void Update(WrkSet wrkSet)
        {
            string sql = @"
update a
   set FldNm= @FldNm,
       SetWrkId= @SetWrkId,
       SetFldNm= @SetFldNm,
       SetDefaultValue= @SetDefaultValue,
       SqlId= @SqlId,
       Pid= @Pid,
       MId= @MId,
       MDt= getdate()
  from WRKSET a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSet);
            }
        }
    }
}

```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repo
{
    public class WrkSql : MdlBase
    {
        private string _FrwId;
        public string FrwId
        {
            get => _FrwId;
            set => Set(ref _FrwId, value);
        }

        private string _FrmId;
        public string FrmId
        {
            get => _FrmId;
            set => Set(ref _FrmId, value);
        }

        private string _WrkId;
        public string WrkId
        {
            get => _WrkId;
            set => Set(ref _WrkId, value);
        }

        private string _CRUDM;
        public string CRUDM
        {
            get => _CRUDM;
            set => Set(ref _CRUDM, value);
        }

        private string _Query;
        public string Query
        {
            get => _Query;
            set => Set(ref _Query, value);
        }

        private string _Memo;
        public string Memo
        {
            get => _Memo;
            set => Set(ref _Memo, value);
        }

        private long _Id;
        public long Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private long _PId;
        public long PId
        {
            get => _PId;
            set => Set(ref _PId, value);
        }
    }
    public interface IWrkSqlRepo
    {
        //WrkSql GetSql(string frwId, string frmId, string wrkId, string crudm); GenFunc로 전환
        List<WrkSql> GetSqls(string frwId, string frmId, string wrkId);
        void Add(WrkSql wrkSql);
        void Save(WrkSql wrkSql);
        void Update(WrkSql wrkSql);
        void Delete(WrkSql wrkSql);
    }
    public class WrkSqlRepo : IWrkSqlRepo
    {
//        public WrkSql GetSql(string frwId, string frmId, string wrkId)
//        {
//            string sql = @"
//select a.FrwId, a.FrmId, a.WrkId, a.CRUDM, a.Query,
//       a.Memo, a.Id, a.PId, a.CId, a.CDt,
//       a.MId, a.MDt
//  from WRKSQL a
// where 1=1
//   and a.FrwId = @FrwId
//   and a.FrmId = @FrmId
//   and a.WrkId = @WrkId
//";
//            using (var db = new Lib.GaiaHelper())
//            {
//                var result = db.Query<WrkSql>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).FirstOrDefault();

//                if (result != null)
//                {
//                    result.ChangedFlag = MdlState.None;
//                    return result;
//                }
//            }
//        }

        public List<WrkSql> GetSqls(string frwId, string frmId, string wrkId)
        {
            string sql = @"
select a.FrwId, a.FrmId, a.WrkId, a.CRUDM, a.Query,
       a.Memo, a.Id, a.PId, a.CId, a.CDt,
       a.MId, a.MDt
  from WRKSQL a
 where 1=1
   and a.FrwId = @FrwId
   and a.FrmId = @FrmId
   and a.WrkId = @WrkId
   and a.CRUDM = @CRUDM
";
            using (var db = new Lib.GaiaHelper())
            {
                var result = db.Query<WrkSql>(sql, new { FrwId = frwId, FrmId = frmId, WrkId = wrkId }).ToList();

                if (result == null)
                {
                    throw new KeyNotFoundException($"A record with the code {frwId},{frmId},{wrkId} was not found.");
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
        public void Add(WrkSql wrkSql)
        {
            string sql = @"
insert into WRKSQL
      (FrwId, FrmId, WrkId, CRUDM, Query,
       Memo, PId, CId, CDt,
       MId, MDt)
select @FrwId, @FrmId, @WrkId, @CRUDM, @Query,
       @Memo, @PId, 
       @CId, getdate(), @MId, getdate()
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSql);
            }
        }
        public void Delete(WrkSql wrkSql)
        {
            string sql = @"
delete
  from WRKSQL
 where 1=1
   and FrmId = @FrmId
   and FrwId = @FrwId
   and WrkId = @WrkId
   and CRUDM = @CRUDM
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSql);
            }
        }
        public void Update(WrkSql wrkSql)
        {
            string sql = @"
update a
   set Query= @Query,
       Memo= @Memo,
       PId= @PId,
       MId= @MId,
       MDt= getdate()
  from WRKSQL a
 where 1=1
   and Id = @Id
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSql);
            }
        }

        public void Save(WrkSql wrkSql)
        {
            string sql = @"
if exists(select 1 from WRKSQL where FrwId=@FrwId and FrmId=@FrmId and WrkId=@WrkId and CRUDM=@CRUDM)
begin 
    update a
       set Query= @Query,
           Memo= @Memo,
           PId= @PId,
           MId= @MId,
           MDt= getdate()
      from WRKSQL a
     where 1=1
       and FrwId = @FrwId
       and FrmId = @FrmId
       and WrkId = @WrkId
       and CRUDM = @CRUDM
end else begin 
    insert into WRKSQL
          (FrwId, FrmId, WrkId, CRUDM, Query,
           Memo, PId, CId, CDt,
           MId, MDt)
    select @FrwId, @FrmId, @WrkId, @CRUDM, @Query,
           @Memo, @PId, 
           @CId, getdate(), @MId, getdate()
end 
";
            using (var db = new Lib.GaiaHelper())
            {
                db.OpenExecute(sql, wrkSql);
            }
        }
    }

}

```
