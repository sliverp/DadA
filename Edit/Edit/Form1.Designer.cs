namespace Edit
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openfileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.开始执行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.programInput = new System.Windows.Forms.TextBox();
            //嘿嘿嘿
            //this.programInput.Text = "main() {\r\n a = 1;\r\n    while (a < 100) {\r\n print(a );\r\n a = a + 1;\r\n    }\r\n}";
            this.programInput.Text = "";
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.开始执行ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(126, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openfileBtn,
            this.saveFileBtn});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // openfileBtn
            // 
            this.openfileBtn.Name = "openfileBtn";
            this.openfileBtn.Size = new System.Drawing.Size(124, 22);
            this.openfileBtn.Text = "打开文件";
            this.openfileBtn.Click += new System.EventHandler(this.openfileBtn_Click);
            // 
            // saveFileBtn
            // 
            this.saveFileBtn.Name = "saveFileBtn";
            this.saveFileBtn.Size = new System.Drawing.Size(124, 22);
            this.saveFileBtn.Text = "保存文件";
            this.saveFileBtn.Click += new System.EventHandler(this.saveFileBtn_Click);
            // 
            // 开始执行ToolStripMenuItem
            // 
            this.开始执行ToolStripMenuItem.Name = "开始执行ToolStripMenuItem";
            this.开始执行ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开始执行ToolStripMenuItem.Text = "开始执行";
            this.开始执行ToolStripMenuItem.Click += new System.EventHandler(this.开始执行ToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.menuStrip1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(24, 6);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1572, 70);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // programInput
            // 
            this.programInput.AcceptsReturn = true;
            this.programInput.AcceptsTab = true;
            this.programInput.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.programInput.Location = new System.Drawing.Point(24, 41);
            this.programInput.Margin = new System.Windows.Forms.Padding(6);
            this.programInput.Multiline = true;
            this.programInput.Name = "programInput";
            this.programInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.programInput.Size = new System.Drawing.Size(1047, 484);
            this.programInput.TabIndex = 1;
            this.programInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.programInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_keyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(24, 561);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(1047, 200);
            this.textBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 531);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Console:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 776);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.programInput);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "DadA语言IDE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openfileBtn;
        private System.Windows.Forms.ToolStripMenuItem saveFileBtn;
        private System.Windows.Forms.ToolStripMenuItem 开始执行ToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox programInput;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

