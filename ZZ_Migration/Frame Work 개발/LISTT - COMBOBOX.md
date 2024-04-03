```C#
\#region RepoDIANA, ClsDIANA
    public class RepoDIANA
    {
        public List<ClsDIANA> dianas = new List<ClsDIANA>();

        public RepoDIANA()
        {
            dianas = new List<ClsDIANA>();
            dianas.Add(new ClsDIANA() { DIANA_ip = @"10.150.1.92", DIANA_nm = "LOCAL", DIANA_id = "sa", DIANA_pwd = "Qwert1234%" });
            dianas.Add(new ClsDIANA() { DIANA_ip = @"10.150.1.92", DIANA_nm = "VPN", DIANA_id = "sa", DIANA_pwd = "Qwert1234%" });
            dianas.Add(new ClsDIANA() { DIANA_ip = @"115.84.112.140", DIANA_nm = "Public IP", DIANA_id = "sa", DIANA_pwd = "Qwert1234%" });
        }
    }
    public class ClsDIANA
    {
        public string DIANA_ip { get; set; }
        public string DIANA_nm { get; set; }
        public string DIANA_id { get; set; }
        public string DIANA_pwd { get; set; }
    }
\#endregion

private void LogIn_Load(object sender, EventArgs e)
{
    RepoDIANA repos = new RepoDIANA();
    this.cmbSVR.DataSource = repos.dianas;
    this.cmbSVR.ValueMember = "DIANA_ip";
    this.cmbSVR.DisplayMember = "DIANA_nm";
}
```