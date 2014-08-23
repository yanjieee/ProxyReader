using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;

namespace ProxyReader
{
    public struct TProxy
    {
        public String proxy;
        public String username;
        public String password;
        public String country;
        public String company;
    }    

    public struct TAccount
    {
        public String host;
        public String code;
        public String refer;
        public int thread;
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            /*
            String s = "";
            String proxypath = Application.StartupPath + "\\Data\\Proxy.txt";
            FileInfo uafi = new FileInfo(proxypath);
            StreamReader uasr = uafi.OpenText();

            int i = 0;

            while ((s = uasr.ReadLine()) != null)
            {
                //TProxy newproxy = GetBestproxyandvpnProxy(s);
                //TProxy newproxy = GetSquidproxiesProxy(s);
                TProxy newproxy = GetAnonyProxy(s);
                //TProxy newproxy = GetBuyProxiesProxy(s);
                _proxylist.Add(newproxy);
            }

            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath + ";Jet OLEDB;");
            _conn.Open();

            OleDbCommand sql = _conn.CreateCommand();

            sql.CommandText = "DELETE * FROM Proxy";
            sql.ExecuteNonQuery();

            foreach (TProxy pr in _proxylist)
            {
                i++;                
                
                sql.CommandText = "INSERT INTO Proxy VALUES ("
                                + i.ToString() + ",'"
                                + pr.username + "','"
                                + pr.password + "','"
                                + pr.country + "','"
                                + pr.proxy + "','"
                                + pr.company + "')";
                sql.ExecuteNonQuery();
            }

            MessageBox.Show("Finished");
            */
            InitializeComponent();
        }

        private List<TProxy> _proxylist = new List<TProxy>();
        private string _mdbPath;
        private OleDbConnection _conn;
        private int _id = 1;
        private List<TProxy> _prolist = new List<TProxy>();
        private List<TAccount> _accountList = new List<TAccount>();
        private List<TProxy> _newlist = new List<TProxy>();
        private List<TProxy> _otherlist = new List<TProxy>();

        private String GetBestproxyandvpnCountry(String country)
        {
            switch (country)
            { 
                case "Netherlands":
                    return "ne";

                case "United Kingdom":
                    return "uk";

                case "Germany":
                    return "de";

                case " US ":
                    return "us";

                case "Canada":
                    return "ca";

                default:
                    return "unknown";
            }
        }

        private TProxy GetBestproxyandvpnProxy(String s)
        {
            TProxy newproxy = new TProxy();

            newproxy.proxy = GetMid(s, "Http * ", "");
            newproxy.username = "wangping";
            newproxy.password = "7emiH3Bv";
            newproxy.country = GetBestproxyandvpnCountry(GetMid(s, "[", "]"));
            newproxy.company = "best";

            return newproxy;
        }

        private TProxy GetSquidproxiesProxy(String s)
        {
            TProxy newproxy = new TProxy();
            newproxy.proxy = s;
            newproxy.username = "nousername";
            newproxy.password = "nopassword";
            newproxy.country = "sw";
            newproxy.company = "squid";

            return newproxy;
        }

        private TProxy GetBuyProxiesProxy(String s)
        {
            TProxy newproxy = new TProxy();
            String[] sArray = s.Split(':');
            newproxy.proxy = sArray[0] + ":" + sArray[1];
            newproxy.username = sArray[2];
            newproxy.password = sArray[3];
            newproxy.country = "fr";
            newproxy.company = "other";

            return newproxy;
        }

        private TProxy GetAnonyProxy(String s)
        {
            TProxy newproxy = new TProxy();
            newproxy.proxy = s;
            newproxy.username = "jxiong";
            newproxy.password = "cDjnt7FW";
            newproxy.country = "us";
            newproxy.company = "other";

            return newproxy;
        }

        private String GetAnonyProxyCountry(String p)
        {
            String[] sArray = p.Split('.');

            switch (sArray[0])
            {
                case "176":
                    {
                        if ("223" == sArray[1])
                        {
                            if ("65" == sArray[2])
                            {
                                return "other";
                            }
                            else if (("81" == sArray[2])
                                    || ("83" == sArray[2])
                                    || ("84" == sArray[2])
                                    || ("85" == sArray[2]))
                            {
                                return "de";
                            }
                            else if (("89" == sArray[2])
                                    || ("90" == sArray[2])
                                    || ("91" == sArray[2]))
                            {
                                return "other";
                            }
                        }
                        else if ("126" == sArray[1])
                        {
                            if ("173" == sArray[2])
                            {
                                return "other";
                            }
                        }
                    }
                    break;

                case "178":
                    {
                        if ("132" == sArray[1])
                        {
                            if ("74" == sArray[2])
                            {
                                return "other";
                            }
                        }
                    }
                    break;

                case "185":
                    {
                        if ("9" == sArray[1])
                        {
                            if (("180" == sArray[2]) || ("181" == sArray[2]))
                            {
                                return "de";
                            }
                        }
                    }
                    break;

                case "188":
                    {
                        if ("65" == sArray[1])
                        {
                            if (("146" == sArray[2]) || ("147" == sArray[2]) || ("150" == sArray[2]) || ("151" == sArray[2]))
                            {
                                return "de";
                            }
                        }
                    }
                    break;

                case "196":
                    {
                        if ("196" == sArray[1])
                        {
                            return "other";
                        }
                    }
                    break;

                case "31":
                    {
                        if ("132" == sArray[1])
                        {
                            return "uk";
                        }
                    }
                    break;

                case "5":
                    {
                        if ("157" == sArray[1])
                        {
                            if ("59" == sArray[2])
                            {
                                return "other";
                            }
                            else if ("60" == sArray[2])
                            {
                                return "other";
                            }
                            else if ("61" == sArray[2])
                            {
                                return "other";
                            }
                            else if ("62" == sArray[2])
                            {
                                return "other";
                            }
                            else if ("63" == sArray[2])
                            {
                                return "other";
                            }
                        }
                        else if ("101" == sArray[1])
                        {
                            if ("145" == sArray[2])
                            {
                                return "uk";
                            }
                        }
                    }
                    break;

                case "78":
                    {
                        if ("157" == sArray[1])
                        {
                            return "uk";
                        }
                    }
                    break;

                default:
                    break;
            }

            return "us";
        }

        private TProxy GetAnonyProxy(String s, String username, String password)
        {
            TProxy newproxy = new TProxy();
            newproxy.proxy = s;
            newproxy.username = username;
            newproxy.password = password;
            newproxy.country = "us";
            newproxy.company = "other";

            return newproxy;
        }

        private String GetMid(String input, String s, String e)
        {
            int pos = input.IndexOf(s);
            if (pos == -1)
            {
                return "";
            }

            pos += s.Length;

            int pos_end = 0;
            if (e == "")
            {
                pos_end = input.Length;
            }
            else
            {
                pos_end = input.IndexOf(e, pos);
            }

            if (pos_end == -1)
            {
                return "";
            }

            return input.Substring(pos, pos_end - pos);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            String s = "";
            String proxypath = Application.StartupPath + "\\Data\\Proxy.txt";
            FileInfo uafi = new FileInfo(proxypath);
            StreamReader uasr = uafi.OpenText();

            int i = 0;

            while ((s = uasr.ReadLine()) != null)
            {
                //TProxy newproxy = GetBestproxyandvpnProxy(s);
                //TProxy newproxy = GetSquidproxiesProxy(s);
                //TProxy newproxy = GetAnonyProxy(s);
                TProxy newproxy = GetAnonyProxy(s, this.username.Text.ToString(), this.password.Text.ToString());
                //TProxy newproxy = GetBuyProxiesProxy(s);
                _proxylist.Add(newproxy);
            }

            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();

            OleDbCommand sql = _conn.CreateCommand();

            sql.CommandText = "DELETE * FROM Proxy";
            sql.ExecuteNonQuery();

            foreach (TProxy pr in _proxylist)
            {
                i++;

                sql.CommandText = "INSERT INTO Proxy VALUES ("
                                + i.ToString() + ",'"
                                + pr.username + "','"
                                + pr.password + "','"
                                + pr.country + "','"
                                + pr.proxy + "','"
                                + pr.company + "')";
                sql.ExecuteNonQuery();
            }

            MessageBox.Show("Finished");
        }

        private void SaveProxy(int index, TProxy pr)
        {
            String Path = Application.StartupPath + "\\Data\\RM" + index.ToString() + "\\Data.mdb";
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path);
            conn.Open();

            OleDbCommand sql = conn.CreateCommand();
            _id++;
            sql.CommandText = "INSERT INTO Proxy VALUES ("
                            + _id.ToString() + ",'"
                            + pr.username + "','"
                            + pr.password + "','"
                            + pr.country + "','"
                            + pr.proxy + "','"
                            + pr.company + "')";
            sql.ExecuteNonQuery();
            conn.Close();
        }

        private void SaveCountryProxy(int index, String country, int num)
        {
            for (int i = 0; i < num; i++)
            {
                OleDbCommand sql = _conn.CreateCommand();
                sql.CommandText = "SELECT * FROM Proxy WHERE country = '" + country + "'";
                OleDbDataReader ret = sql.ExecuteReader();

                if (ret.Read())
                {
                    TProxy pr = new TProxy();
                    pr.username = ret["username"].ToString();
                    pr.password = ret["password"].ToString();
                    pr.proxy = ret["proxy"].ToString();
                    pr.company = ret["company"].ToString();
                    pr.country = ret["country"].ToString();
                    ret.Close();

                    SaveProxy(index, pr);

                    sql.CommandText = "DELETE * FROM Proxy WHERE proxy = '" + pr.proxy + "'";
                    sql.ExecuteNonQuery();
                }
            }
        }

        private void btn_gen_Click(object sender, EventArgs e)
        {
            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();

            int DatabaseNum = int.Parse(this.database.Text.ToString());

            for (int i = 1; i <= DatabaseNum; i ++)
            {
                _id = 1;
                String Path = Application.StartupPath + "\\Data\\RM" + i.ToString() + "\\Data.mdb";
                OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path);
                conn.Open();

                OleDbCommand sql = conn.CreateCommand();

                sql.CommandText = "DELETE * FROM Proxy";
                sql.ExecuteNonQuery();

                conn.Close();

                int CountryNum = int.Parse(this.de.Text.ToString());
                SaveCountryProxy(i, "de", CountryNum);

                CountryNum = int.Parse(this.uk.Text.ToString());
                SaveCountryProxy(i, "uk", CountryNum);

                CountryNum = int.Parse(this.us.Text.ToString());
                SaveCountryProxy(i, "us", CountryNum);
                
                CountryNum = int.Parse(this.other.Text.ToString());
                SaveCountryProxy(i, "other", CountryNum);

                conn.Close();
            }

            MessageBox.Show("Finished");
        }

        private List<TProxy> GetProxyList()
        {
            OleDbCommand sql = _conn.CreateCommand();
            sql.CommandText = "SELECT * FROM Proxy";
            OleDbDataReader ret = sql.ExecuteReader();

            List<TProxy> proxylist = new List<TProxy>();

            while (ret.Read())
            {
                TProxy pro = new TProxy();
                pro.username = ret["username"].ToString();
                pro.password = ret["password"].ToString();
                pro.proxy = ret["proxy"].ToString();
                pro.country = ret["country"].ToString();
                proxylist.Add(pro);
            }

            ret.Close();
            return proxylist;
        }

        public List<TAccount> GetAccountlist()
        {
            List<TAccount> accountlist = new List<TAccount>();

            OleDbCommand sql = _conn.CreateCommand();
            sql.CommandText = "SELECT * FROM Account";
            OleDbDataReader ret = sql.ExecuteReader();

            while (ret.Read())
            {
                TAccount account = new TAccount();
                account.code = ret["code"].ToString();
                account.host = ret["host"].ToString();
                account.refer = ret["refer"].ToString();
                account.thread = (int)ret["thread"];
                accountlist.Add(account);
            }

            ret.Close();
            return accountlist;
        }

        public List<TAccount> GetAccountlistByCompany(string company)
        {
            List<TAccount> accountlist = new List<TAccount>();

            OleDbCommand sql = _conn.CreateCommand();
            sql.CommandText = "SELECT * FROM Account WHERE company=\"" + company + "\"";
            OleDbDataReader ret = sql.ExecuteReader();

            while (ret.Read())
            {
                TAccount account = new TAccount();
                account.code = ret["code"].ToString();
                account.host = ret["host"].ToString();
                account.refer = ret["refer"].ToString();
                account.thread = (int)ret["thread"];
                accountlist.Add(account);
            }

            ret.Close();
            return accountlist;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();

            _prolist = GetProxyList();

            String file_path = Application.StartupPath + "\\Data\\private.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Create);
            StreamWriter sw_http = new StreamWriter(fs_http, Encoding.UTF8);
            foreach (TProxy p in _prolist)
            {
                String[] sArray = p.proxy.Split(':');
                String proxy = sArray[0] + "|" + sArray[1] + "|" + p.country + "|" + p.username + "|" + p.password;
                sw_http.Write(proxy + "\n");
            }

            sw_http.Close();
            MessageBox.Show("Finished");
        }

        private void readproxy()
        {
            String s = "";
            String file_path = Application.StartupPath + "\\Data\\private.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Open);
            StreamReader sw_http = new StreamReader(fs_http, Encoding.UTF8);

            while ((s = sw_http.ReadLine()) != null)
            {
                TProxy newproxy = new TProxy();

                String[] sArray = s.Split('|');
                newproxy.proxy = sArray[0] + ":" + sArray[1];
                newproxy.country = sArray[2];
                newproxy.username = sArray[3];
                newproxy.password = sArray[4];
                _prolist.Add(newproxy);
            }

            sw_http.Close();
            fs_http.Close();
        }

        private void readnewproxy()
        {
            String s = "";
            String file_path = Application.StartupPath + "\\Data\\good.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Open);
            StreamReader sw_http = new StreamReader(fs_http, Encoding.UTF8);

            while ((s = sw_http.ReadLine()) != null)
            {
                TProxy newproxy = new TProxy();

                String[] sArray = s.Split('|');
                newproxy.proxy = sArray[0] + ":" + sArray[1];
                newproxy.country = sArray[2];
                newproxy.username = sArray[3];
                newproxy.password = sArray[4];
                _newlist.Add(newproxy);
            }

            sw_http.Close();
            fs_http.Close();
        }

        private void writeproxy()
        {
            String file_path = Application.StartupPath + "\\Data\\private.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Create);
            StreamWriter sw_http = new StreamWriter(fs_http, Encoding.UTF8);
            foreach (TProxy p in _prolist)
            {
                String[] sArray = p.proxy.Split(':');
                String proxy = sArray[0] + "|" + sArray[1] + "|" + p.country + "|" + p.username + "|" + p.password;
                sw_http.Write(proxy + "\n");
            }

            sw_http.Close();
            fs_http.Close();
        }

        private void writeotherproxy()
        {
            String file_path = Application.StartupPath + "\\Data\\other.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Create);
            StreamWriter sw_http = new StreamWriter(fs_http, Encoding.UTF8);
            foreach (TProxy p in _otherlist)
            {
                String[] sArray = p.proxy.Split(':');
                String proxy = sArray[0] + "|" + sArray[1] + "|" + p.country + "|" + p.username + "|" + p.password;
                sw_http.Write(proxy + "\n");
            }

            sw_http.Close();
            fs_http.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readproxy();
            writeproxy();
            MessageBox.Show("Finished");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();

            _accountList = GetAccountlist();

            String file_path = Application.StartupPath + "\\Data\\account.html";
            FileStream fs_http = new FileStream(file_path, FileMode.Create);
            StreamWriter sw_http = new StreamWriter(fs_http, Encoding.UTF8);
            
            sw_http.Write("apnstart");

            foreach (TAccount a in _accountList)
            {
                String account = a.host + "|" + a.code.Replace("\r", "") + "|" + a.refer + "|" + a.thread.ToString();
                sw_http.Write(account + "\n");
            }

            sw_http.Close();
            fs_http.Close();

            int count = 1;
            int num = 0;

            int size = _accountList.Count / int.Parse(textBox1.Text) + 1;

            while (num < _accountList.Count)
            {
                file_path = Application.StartupPath + "\\Data\\account" + count.ToString() + ".html";
                fs_http = new FileStream(file_path, FileMode.Create);
                sw_http = new StreamWriter(fs_http, Encoding.UTF8);
                sw_http.Write("apnstart");
                for (int i = 0; i < size; i++)
                {
                    if (num >= _accountList.Count)
                    {
                        break;
                    }
                    else
                    {
                        TAccount a = _accountList[num];
                        String account = a.host + "|" + a.code.Replace("\r", "") + "|" + a.refer + "|" + a.thread.ToString();
                        sw_http.Write(account + "\n");
                        num++;
                    }
                }
                count++;
                sw_http.Close();
                fs_http.Close();
            }



            MessageBox.Show("Finished");
        }

        private bool IsInList(TProxy pr)
        {
            foreach (TProxy p in _prolist)
            {
                if (p.proxy == pr.proxy)
                {
                    return true;
                }
            }

            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            readproxy();
            readnewproxy();
            foreach (TProxy pr in _newlist)
            {
                if (IsInList(pr) == false)
                {
                    _prolist.Add(pr);
                }
            }
            writeproxy();
            MessageBox.Show("Finished");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            readproxy();
            readnewproxy();
            foreach (TProxy pr in _newlist)
            {
                if (IsInList(pr) == false)
                {
                    _otherlist.Add(pr);
                }
            }
            writeotherproxy();
            MessageBox.Show("Finished");
        }

        /// <summary>
        /// 读取注册表
        /// </summary>
        private void loadRegisterList()
        {
            RegistryKey software = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
            RegistryKey dbstudio = software.OpenSubKey("DBSTUDIO", true);
            if (dbstudio == null)
            {
                dbstudio = software.CreateSubKey("DBSTUDIO");
                dbstudio.SetValue("copies", textBox1.Text);
            }
            else
            {
                if (dbstudio.GetValue("copies") == null)
                {
                    dbstudio.SetValue("copies", textBox1.Text);
                }
                else
                {
                    textBox1.Text = dbstudio.GetValue("copies").ToString();
                }
            }
        }

        /// <summary>
        /// 保存注册表
        /// </summary>
        private void saveRegisterList()
        {
            RegistryKey software = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
            RegistryKey dbstudio = software.OpenSubKey("DBSTUDIO", true);
            if (dbstudio == null)
            {
                dbstudio = software.CreateSubKey("DBSTUDIO");
            }
            dbstudio.SetValue("copies", textBox1.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loadRegisterList();
            //初始化company的列表
            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();
            OleDbCommand sql = _conn.CreateCommand();
            sql.CommandText = "SELECT company from Account group by company;";
            OleDbDataReader ret = sql.ExecuteReader();

            while (ret.Read())
            {
                comboBox1.Items.Add(ret["company"].ToString());
            }
            ret.Close();
            _conn.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveRegisterList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _mdbPath = Application.StartupPath + "\\Data\\Data.mdb";
            _conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _mdbPath);
            _conn.Open();

            _accountList = GetAccountlistByCompany(comboBox1.SelectedItem.ToString());

            String file_path = Application.StartupPath + "\\Data\\account_" + comboBox1.SelectedItem.ToString() + ".html";
            FileStream fs_http = new FileStream(file_path, FileMode.Create);
            StreamWriter sw_http = new StreamWriter(fs_http, Encoding.UTF8);

            sw_http.Write("apnstart");

            foreach (TAccount a in _accountList)
            {
                String account = a.host + "|" + a.code.Replace("\r", "") + "|" + a.refer + "|" + a.thread.ToString();
                sw_http.Write(account + "\n");
            }

            sw_http.Close();
            fs_http.Close();
            MessageBox.Show("Finished");
        }
    }
}
