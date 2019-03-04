using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HttpServerBackManager
{
    public enum MainTag
    {
        USERS_TABLE, MESHES_TABLE,
    }
    public class FileContinuePostThreadParams
    {
        public string i;
        public long sizeReaming;
        public FileInfo fI;
    }
    public partial class Form1 : Form
    {
        Dictionary<MainTag, string[]> DataCols;
        int currentEidtID;
        MainTag mainTag = MainTag.USERS_TABLE;
        int uploadCount = 0;
        int targetUploadCount = 0;
        //JArray 
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            WaitingGroup.Visible = false;
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
            loginForm.ShowDialog();



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
                if (jsonStr["result"].ToString() != "OK")
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show(jsonStr["result"].ToString());
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
                    currentEidtID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
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
            uploadCount = 0;
            targetUploadCount = 0;
            var ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "FBX file|*.fbx|OBJ file|*.obj|ALL file|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var selectFiles = ofd.FileNames;
                targetUploadCount = selectFiles.Length;
                WaitingGroup.Visible = true;
                Thread tmpThread = new Thread(WaitingUploadThreadProc);
                tmpThread.Start();
                Thread tmpPostFileThread = new Thread(PostFileThreadProc);
                tmpPostFileThread.Start(selectFiles);
            }
        }

        void PostFileThreadProc(object fileNames)
        {
            string[] selectFiles = fileNames as string[];
            foreach (var i in selectFiles)
            {
                FileInfo tmpfi = new FileInfo(i);
                var fileSize = tmpfi.Length;
                var sizeReaming = fileSize;
                var s_uploadCount = uploadCount;
                if (fileSize < 2048)
                {
                    byte[] buffer = new byte[fileSize];
                    BinaryReader fs = new BinaryReader(new FileStream(i, FileMode.Open));
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    HttpRequestManager.GetInstance().SendHttpRequest(
                        "http://192.168.50.53:5656/?method=upload&fileName=" + tmpfi.Name + "&dataContinue=false"
                        , "POST", UploadDoneCallback, buffer);
                }
                else
                {
                    Thread ContinuePostThread = new Thread(ContinuePostFileThreadProc);
                    FileContinuePostThreadParams tmpParam = new FileContinuePostThreadParams();
                    tmpParam.i = i;
                    tmpParam.sizeReaming = sizeReaming;
                    tmpParam.fI = tmpfi;
                    ContinuePostThread.Start(tmpParam);

                }
                while (s_uploadCount == uploadCount)
                {
                    Thread.Sleep(1);
                }
            }
            while (!WaitingMutex.WaitOne(1)) ;
            WaitingGroup.Visible = false;
            WaitingMutex.ReleaseMutex();
        }

        void ContinuePostFileThreadProc(object param)
        {
            FileContinuePostThreadParams p = param as FileContinuePostThreadParams;
            long count = 0;
            while (p.sizeReaming > 0)
            {
                byte[] buffer = null;
                var dataContinueFlag = "true";
                if (p.sizeReaming < 2048)
                {
                    buffer = new byte[p.sizeReaming];
                    dataContinueFlag = "end";
                }
                else
                {
                    buffer = new byte[2048];
                }
                FileStream fs = new FileStream(p.i, FileMode.Open);
                fs.Seek(count, SeekOrigin.Begin);
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                count += 2048;
                ContinueHttpIsBack = false;
                HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656/?method=upload&fileName=" + p.fI.Name + "&dataContinue=" + dataContinueFlag, "POST", UploadDoneCallback, buffer);
                while (!ContinueHttpIsBack)
                {
                    Thread.Sleep(1);
                }

                p.sizeReaming -= 2048;
            }
        }

        void WaitingUploadThreadProc()
        {
            WaitingMutex = new Mutex();
            WaitingMutex.WaitOne();
            int s_tickcount = System.Environment.TickCount;
            int dotCount = 0;
            while (uploadCount < targetUploadCount)
            {
                int c_tickcount = System.Environment.TickCount;
                int delta_tickcount = c_tickcount - s_tickcount;
                if (delta_tickcount <= 0.01666666f)
                {
                    Thread.Sleep(1);
                }
                if (dotCount < 6)
                {
                    dotCount++;
                }
                else
                {
                    dotCount = 0;
                }
                WaitingGroup.Visible = true;
                WaitingGroup_label1.Text = "Uploading";
                for (int i = 0; i <= dotCount; i++)
                {
                    WaitingGroup_label1.Text = WaitingGroup_label1.Text + ".";
                }
            }
            uploadCount = targetUploadCount = 0;
            WaitingMutex.ReleaseMutex();
        }

        bool ContinueHttpIsBack = false;
        Mutex WaitingMutex;
        private void UploadDoneCallback(string re)
        {
            JObject jroot = JObject.Parse(re);
            if (jroot["type"].ToString() != "normal")
            {
                MessageBox.Show(jroot["result"].ToString());
            }
            else
            {
                string jsonReStr = jroot["result"].ToString();
                if (jsonReStr == "end" || jsonReStr == "false")
                {
                    uploadCount++;
                }

                ContinueHttpIsBack = true;
            }
        }

        private void MutilpleDeleteButton_Click(object sender, EventArgs e)
        {
            JObject root = new JObject();
            JArray jarr = new JArray();
            if (dataGridView1.SelectedRows.Count == 0)
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
