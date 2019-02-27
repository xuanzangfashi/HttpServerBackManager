using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HttpServerBackManager
{
    public enum MainTag
    {
        USERS_TABLE, MESHES_TABLE,
    }
    public partial class Form1 : Form
    {
        Dictionary<MainTag, string[]> DataCols;
        int currentEidtID;
        MainTag mainTag = MainTag.USERS_TABLE;
        //JArray 
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DataCols = new Dictionary<MainTag, string[]>();
            DataCols.Add(MainTag.MESHES_TABLE, new string[] { "null" });
            DataCols.Add(MainTag.USERS_TABLE, new string[] { "userName", "password", "name", "sexual" });
            this.dataGridView1.CellMouseUp += dataGridView1_CellMouseUp;
            this.dataGridView1.AllowUserToAddRows = false;
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminGetTable&tableName=users", "GET", InitRequestCallback);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            // loginForm.ShowDialog();



        }

        private void InitRequestCallback(string re)
        {
            if (re == "false")
            {
                MessageBox.Show("网络发生错误!");
                return;
            }
            dataGridView1.Rows.Clear();
            try
            {
                JObject jsonStr = JObject.Parse(re);
                if (jsonStr["result"].ToString() == "NullTable")
                {
                    return;
                }
                JArray jArr = JArray.Parse(jsonStr["objs"].ToString());
                for (int i = 0; i < jArr.Count; i++)
                {
                    JObject tmp = JObject.Parse(jArr[i].ToString());

                    int newindex = dataGridView1.Rows.Add();
                    // dataGridView1.Rows[newindex].Cells[0].Value = (i + 1).ToString();

                    int index = 0;

                    while (tmp[index.ToString()] != null)
                    {
                        dataGridView1.Rows[newindex].Cells[index].Value = tmp[index.ToString()].ToString();
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                dataGridView1.Rows.Clear();
                MessageBox.Show("数据加载错误!");
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            var tablename = GetTableNameByMainTag();
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminGetTable&tableName=" + tablename, "GET", InitRequestCallback);


        }

        private string GetTableNameByMainTag()
        {
            string tmptablename = "";
            switch (mainTag)
            {
                case MainTag.MESHES_TABLE:
                    tmptablename = "meshes";
                    break;
                case MainTag.USERS_TABLE:
                    tmptablename = "users";
                    break;
            }
            return tmptablename;
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    currentEidtID = e.RowIndex;
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);

                }
            }
        }

        private void EditItem_Click(object sender, EventArgs e)
        {
            EditItemForm tmpForm = new EditItemForm(this, DataCols[mainTag]);
            tmpForm.Show();
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            var tablename = GetTableNameByMainTag();
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminDeleteItem&tableName=" + tablename + "&id=" + currentEidtID,
               "POST", DeleteItemCallback);
        }

        private void DeleteItemCallback(string re)
        {
            JObject jroot = JObject.Parse(re);
            if (jroot["result"].ToString() != "OK")
            {
                MessageBox.Show(jroot["result"].ToString());

            }
            RefreshButton_Click(null, null);
        }

        public void OnEditOK(string[] strs)
        {
            var tablename = GetTableNameByMainTag();
            List<string> keys = new List<string>();
            List<string> values = new List<string>();

            int index = 0;
            foreach (var i in strs)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    values.Add(i);
                    keys.Add(DataCols[mainTag][index]);
                }
                index++;
            }


            //if (!string.IsNullOrEmpty(userName))
            //{
            //    values.Add(userName);
            //    keys.Add("userName");
            //}
            //if (!string.IsNullOrEmpty(password))
            //{
            //    values.Add(password);
            //    keys.Add("password");

            //}
            //if (!string.IsNullOrEmpty(name))
            //{
            //    values.Add(name);
            //    keys.Add("name");

            //}
            //if (!string.IsNullOrEmpty(sexual))
            //{
            //    values.Add(sexual);
            //    keys.Add("sexual");

            //}

            var keyarr = keys.ToArray();
            var valuearr = values.ToArray();
            EditTable("users", keyarr, valuearr);

        }

        private void EditTable(string tablename, string[] keys, string[] values)
        {
            JObject root = new JObject();
            var arr = new JArray();
            for (int i = 0; i < keys.Length; i++)
            {
                var tmp = new JObject(new JProperty(keys[i], values[i]));
                arr.Add(tmp);
            }
            root.Add("obj", arr);
            string tmpjsonstr = root.ToString();
            tmpjsonstr = tmpjsonstr.Replace(" ", "");

            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminEditTable&tableName=" + tablename + "&id=" + currentEidtID,
                "POST", EidtTableCallback, tmpjsonstr);

        }

        private void EidtTableCallback(string re)
        {
            JObject jroot = JObject.Parse(re);
            if (jroot["result"].ToString() != "OK")
            {
                MessageBox.Show(jroot["result"].ToString());

            }
            RefreshButton_Click(null, null);
        }

        private void userListButton_Click(object sender, EventArgs e)
        {
            mainTag = MainTag.USERS_TABLE;
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminGetTable&tableName=" + "users", "GET", InitRequestCallback);
        }

        private void meshListButton_Click(object sender, EventArgs e)
        {
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminGetTable&tableName=" + "meshes", "GET", InitRequestCallback);
            mainTag = MainTag.MESHES_TABLE;
        }

        private void MutilpleAddButton_Click(object sender, EventArgs e)
        {
            JObject jroot = new JObject();

        }

        private void MutilpleDeleteButton_Click(object sender, EventArgs e)
        {
            JObject root = new JObject();
            JArray jarr = new JArray();
            if(dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow i in dataGridView1.SelectedRows)
            {
                var id = i.Cells[0].Value.ToString();
                jarr.Add(id);

            }
            root.Add("objs", jarr);
            var jsonstr = root.ToString();
            jsonstr = jsonstr.Replace(" ", "");
            var tablename = GetTableNameByMainTag();
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=adminDeleteItem&tableName=" + tablename,
               "POST", DeleteItemCallback, jsonstr);

        }
    }
}
