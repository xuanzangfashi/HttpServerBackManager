namespace HttpServerBackManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.账号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.密码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.编辑 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.userListButton = new System.Windows.Forms.Button();
            this.meshListButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.MutilpleAssetAddButton = new System.Windows.Forms.Button();
            this.MutilpleDeleteButton = new System.Windows.Forms.Button();
            this.WaitingGroup_pictureBox1 = new System.Windows.Forms.PictureBox();
            this.WaitingGroup = new System.Windows.Forms.GroupBox();
            this.WaitingGroup_label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaitingGroup_pictureBox1)).BeginInit();
            this.WaitingGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.账号,
            this.密码,
            this.姓名,
            this.性别,
            this.编辑});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(776, 353);
            this.dataGridView1.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // 账号
            // 
            this.账号.HeaderText = "账号";
            this.账号.Name = "账号";
            // 
            // 密码
            // 
            this.密码.HeaderText = "密码";
            this.密码.Name = "密码";
            // 
            // 姓名
            // 
            this.姓名.HeaderText = "姓名";
            this.姓名.Name = "姓名";
            // 
            // 性别
            // 
            this.性别.HeaderText = "性别";
            this.性别.Name = "性别";
            // 
            // 编辑
            // 
            this.编辑.HeaderText = "编辑";
            this.编辑.Name = "编辑";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditItem,
            this.DeleteItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 48);
            // 
            // EditItem
            // 
            this.EditItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.EditItem.Name = "EditItem";
            this.EditItem.Size = new System.Drawing.Size(131, 22);
            this.EditItem.Text = "编辑";
            this.EditItem.Click += new System.EventHandler(this.EditItem_Click);
            // 
            // DeleteItem
            // 
            this.DeleteItem.Name = "DeleteItem";
            this.DeleteItem.Size = new System.Drawing.Size(131, 22);
            this.DeleteItem.Text = "DeleteItem";
            this.DeleteItem.Click += new System.EventHandler(this.DeleteItem_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(706, 56);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(82, 23);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "刷新";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // userListButton
            // 
            this.userListButton.Location = new System.Drawing.Point(13, 56);
            this.userListButton.Name = "userListButton";
            this.userListButton.Size = new System.Drawing.Size(75, 23);
            this.userListButton.TabIndex = 2;
            this.userListButton.Text = "用户列表";
            this.userListButton.UseVisualStyleBackColor = true;
            this.userListButton.Click += new System.EventHandler(this.userListButton_Click);
            // 
            // meshListButton
            // 
            this.meshListButton.Location = new System.Drawing.Point(94, 56);
            this.meshListButton.Name = "meshListButton";
            this.meshListButton.Size = new System.Drawing.Size(75, 23);
            this.meshListButton.TabIndex = 3;
            this.meshListButton.Text = "模型列表";
            this.meshListButton.UseVisualStyleBackColor = true;
            this.meshListButton.Click += new System.EventHandler(this.meshListButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(175, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MutilpleAssetAddButton
            // 
            this.MutilpleAssetAddButton.Location = new System.Drawing.Point(625, 56);
            this.MutilpleAssetAddButton.Name = "MutilpleAssetAddButton";
            this.MutilpleAssetAddButton.Size = new System.Drawing.Size(75, 23);
            this.MutilpleAssetAddButton.TabIndex = 5;
            this.MutilpleAssetAddButton.Text = "批量添加";
            this.MutilpleAssetAddButton.UseVisualStyleBackColor = true;
            this.MutilpleAssetAddButton.Click += new System.EventHandler(this.MutilpleAddButton_Click);
            // 
            // MutilpleDeleteButton
            // 
            this.MutilpleDeleteButton.Location = new System.Drawing.Point(544, 56);
            this.MutilpleDeleteButton.Name = "MutilpleDeleteButton";
            this.MutilpleDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.MutilpleDeleteButton.TabIndex = 6;
            this.MutilpleDeleteButton.Text = "删除";
            this.MutilpleDeleteButton.UseVisualStyleBackColor = true;
            this.MutilpleDeleteButton.Click += new System.EventHandler(this.MutilpleDeleteButton_Click);
            // 
            // WaitingGroup_pictureBox1
            // 
            this.WaitingGroup_pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.WaitingGroup_pictureBox1.Name = "WaitingGroup_pictureBox1";
            this.WaitingGroup_pictureBox1.Size = new System.Drawing.Size(776, 426);
            this.WaitingGroup_pictureBox1.TabIndex = 7;
            this.WaitingGroup_pictureBox1.TabStop = false;
            // 
            // WaitingGroup
            // 
            this.WaitingGroup.Controls.Add(this.WaitingGroup_label1);
            this.WaitingGroup.Controls.Add(this.WaitingGroup_pictureBox1);
            this.WaitingGroup.Location = new System.Drawing.Point(12, 12);
            this.WaitingGroup.Name = "WaitingGroup";
            this.WaitingGroup.Size = new System.Drawing.Size(776, 426);
            this.WaitingGroup.TabIndex = 8;
            this.WaitingGroup.TabStop = false;
            this.WaitingGroup.Text = "groupBox1";
            // 
            // WaitingGroup_label1
            // 
            this.WaitingGroup_label1.AutoSize = true;
            this.WaitingGroup_label1.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WaitingGroup_label1.Location = new System.Drawing.Point(315, 199);
            this.WaitingGroup_label1.Name = "WaitingGroup_label1";
            this.WaitingGroup_label1.Size = new System.Drawing.Size(137, 40);
            this.WaitingGroup_label1.TabIndex = 8;
            this.WaitingGroup_label1.Text = "label1";
            this.WaitingGroup_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WaitingGroup);
            this.Controls.Add(this.MutilpleDeleteButton);
            this.Controls.Add(this.MutilpleAssetAddButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.meshListButton);
            this.Controls.Add(this.userListButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaitingGroup_pictureBox1)).EndInit();
            this.WaitingGroup.ResumeLayout(false);
            this.WaitingGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem EditItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn 账号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 密码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 性别;
        private System.Windows.Forms.DataGridViewButtonColumn 编辑;
        private System.Windows.Forms.Button userListButton;
        private System.Windows.Forms.Button meshListButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button MutilpleAssetAddButton;
        private System.Windows.Forms.Button MutilpleDeleteButton;
        private System.Windows.Forms.PictureBox WaitingGroup_pictureBox1;
        private System.Windows.Forms.GroupBox WaitingGroup;
        private System.Windows.Forms.Label WaitingGroup_label1;
    }
}

