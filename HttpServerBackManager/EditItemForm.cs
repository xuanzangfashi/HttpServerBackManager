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
    public partial class EditItemForm : Form
    {
        Form1 MainForm;
        Label[] labels;
        TextBox[] inputs;
        int Count = 0;
        public EditItemForm(Form1 mainform, string[] labelNames)
        {
            InitializeComponent();
            Count = labelNames.Length;
            labels = new Label[] { label1, label2, label3, label4, label5, label6 };
            inputs = new TextBox[] { Input1, Input2, Input3, Input4, Input5, Input6 };
            int index = 0;
            foreach (var i in labelNames)
            {
                labels[index].Text = i;
                index++;
            }
            MainForm = mainform;
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            List<string> tmpstrList = new List<string>();
            for (int i = 0; i < Count; i++)
            {
                tmpstrList.Add(inputs[i].Text);
            }


            MainForm.OnEditOK(tmpstrList.ToArray());

            this.Close();
        }
    }
}
