namespace CaseManager
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新用例树ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当前用例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开的用例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用例目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.python工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.需安装的包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载各版本chromedriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chrome浏览器版本与驱动版本对应关系ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常见问题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于啄木鸟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_2 = new System.Windows.Forms.SplitContainer();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.timerExecute = new System.Windows.Forms.Timer(this.components);
            this.timer_hideLogo = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.timer_showLogo = new System.Windows.Forms.Timer(this.components);
            this.panel_loading = new System.Windows.Forms.Panel();
            this.panel_5 = new System.Windows.Forms.Panel();
            this.panel_4 = new System.Windows.Forms.Panel();
            this.panel_3 = new System.Windows.Forms.Panel();
            this.panel_2 = new System.Windows.Forms.Panel();
            this.panel_1 = new System.Windows.Forms.Panel();
            this.panel_0 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.splitContainer_2.Panel1.SuspendLayout();
            this.splitContainer_2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.panel_loading.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(56, 96);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.运行ToolStripMenuItem,
            this.目录ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1275, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.保存全部ToolStripMenuItem,
            this.刷新用例树ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 保存全部ToolStripMenuItem
            // 
            this.保存全部ToolStripMenuItem.Name = "保存全部ToolStripMenuItem";
            this.保存全部ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.保存全部ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.保存全部ToolStripMenuItem.Text = "保存全部";
            this.保存全部ToolStripMenuItem.Click += new System.EventHandler(this.保存全部ToolStripMenuItem_Click);
            // 
            // 刷新用例树ToolStripMenuItem
            // 
            this.刷新用例树ToolStripMenuItem.Name = "刷新用例树ToolStripMenuItem";
            this.刷新用例树ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新用例树ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.刷新用例树ToolStripMenuItem.Text = "刷新用例树";
            this.刷新用例树ToolStripMenuItem.Click += new System.EventHandler(this.刷新用例树ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查找ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.查找ToolStripMenuItem.Text = "查找";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
            // 
            // 运行ToolStripMenuItem
            // 
            this.运行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当前用例ToolStripMenuItem,
            this.打开的用例ToolStripMenuItem});
            this.运行ToolStripMenuItem.Name = "运行ToolStripMenuItem";
            this.运行ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.运行ToolStripMenuItem.Text = "运行";
            // 
            // 当前用例ToolStripMenuItem
            // 
            this.当前用例ToolStripMenuItem.Name = "当前用例ToolStripMenuItem";
            this.当前用例ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.当前用例ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.当前用例ToolStripMenuItem.Text = "当前用例";
            this.当前用例ToolStripMenuItem.Click += new System.EventHandler(this.当前用例ToolStripMenuItem_Click);
            // 
            // 打开的用例ToolStripMenuItem
            // 
            this.打开的用例ToolStripMenuItem.Name = "打开的用例ToolStripMenuItem";
            this.打开的用例ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.打开的用例ToolStripMenuItem.Text = "打开的用例";
            this.打开的用例ToolStripMenuItem.Click += new System.EventHandler(this.打开的用例ToolStripMenuItem_Click);
            // 
            // 目录ToolStripMenuItem
            // 
            this.目录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置文件ToolStripMenuItem,
            this.用例目录ToolStripMenuItem,
            this.python工程ToolStripMenuItem});
            this.目录ToolStripMenuItem.Name = "目录ToolStripMenuItem";
            this.目录ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.目录ToolStripMenuItem.Text = "目录";
            // 
            // 配置文件ToolStripMenuItem
            // 
            this.配置文件ToolStripMenuItem.Name = "配置文件ToolStripMenuItem";
            this.配置文件ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.配置文件ToolStripMenuItem.Text = "配置";
            this.配置文件ToolStripMenuItem.Click += new System.EventHandler(this.配置ToolStripMenuItem_Click);
            // 
            // 用例目录ToolStripMenuItem
            // 
            this.用例目录ToolStripMenuItem.Name = "用例目录ToolStripMenuItem";
            this.用例目录ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.用例目录ToolStripMenuItem.Text = "测试用例";
            this.用例目录ToolStripMenuItem.Click += new System.EventHandler(this.测试用例ToolStripMenuItem_Click);
            // 
            // python工程ToolStripMenuItem
            // 
            this.python工程ToolStripMenuItem.Name = "python工程ToolStripMenuItem";
            this.python工程ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.python工程ToolStripMenuItem.Text = "Python工程";
            this.python工程ToolStripMenuItem.Click += new System.EventHandler(this.python工程ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.需安装的包ToolStripMenuItem,
            this.下载各版本chromedriverToolStripMenuItem,
            this.chrome浏览器版本与驱动版本对应关系ToolStripMenuItem,
            this.常见问题ToolStripMenuItem,
            this.关于啄木鸟ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 需安装的包ToolStripMenuItem
            // 
            this.需安装的包ToolStripMenuItem.Name = "需安装的包ToolStripMenuItem";
            this.需安装的包ToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.需安装的包ToolStripMenuItem.Text = "安装所有需要的包";
            this.需安装的包ToolStripMenuItem.Click += new System.EventHandler(this.需安装的包ToolStripMenuItem_Click);
            // 
            // 下载各版本chromedriverToolStripMenuItem
            // 
            this.下载各版本chromedriverToolStripMenuItem.Name = "下载各版本chromedriverToolStripMenuItem";
            this.下载各版本chromedriverToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.下载各版本chromedriverToolStripMenuItem.Text = "下载各版本chromedriver";
            this.下载各版本chromedriverToolStripMenuItem.Click += new System.EventHandler(this.下载各版本chromedriverToolStripMenuItem_Click);
            // 
            // chrome浏览器版本与驱动版本对应关系ToolStripMenuItem
            // 
            this.chrome浏览器版本与驱动版本对应关系ToolStripMenuItem.Name = "chrome浏览器版本与驱动版本对应关系ToolStripMenuItem";
            this.chrome浏览器版本与驱动版本对应关系ToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.chrome浏览器版本与驱动版本对应关系ToolStripMenuItem.Text = "chrome浏览器版本与驱动版本对应关系";
            // 
            // 常见问题ToolStripMenuItem
            // 
            this.常见问题ToolStripMenuItem.Name = "常见问题ToolStripMenuItem";
            this.常见问题ToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.常见问题ToolStripMenuItem.Text = "常见问题";
            // 
            // 关于啄木鸟ToolStripMenuItem
            // 
            this.关于啄木鸟ToolStripMenuItem.Name = "关于啄木鸟ToolStripMenuItem";
            this.关于啄木鸟ToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.关于啄木鸟ToolStripMenuItem.Text = "关于“啄木鸟”";
            // 
            // splitContainer_2
            // 
            this.splitContainer_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_2.Name = "splitContainer_2";
            // 
            // splitContainer_2.Panel1
            // 
            this.splitContainer_2.Panel1.Controls.Add(this.treeView1);
            this.splitContainer_2.Size = new System.Drawing.Size(187, 127);
            this.splitContainer_2.SplitterDistance = 32;
            this.splitContainer_2.TabIndex = 9;
            this.splitContainer_2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_2_SplitterMoved);
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(0, 0);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(196, 95);
            this.textBox_log.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 38);
            this.splitContainer_main.Name = "splitContainer_main";
            this.splitContainer_main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.splitContainer_2);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.textBox_log);
            this.splitContainer_main.Size = new System.Drawing.Size(226, 319);
            this.splitContainer_main.SplitterDistance = 236;
            this.splitContainer_main.TabIndex = 11;
            this.splitContainer_main.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_main_SplitterMoved);
            // 
            // timerExecute
            // 
            this.timerExecute.Interval = 5000;
            this.timerExecute.Tick += new System.EventHandler(this.timerExecute_Tick);
            // 
            // timer_hideLogo
            // 
            this.timer_hideLogo.Enabled = true;
            this.timer_hideLogo.Interval = 3000;
            this.timer_hideLogo.Tick += new System.EventHandler(this.timer_hideLogo_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox_logo);
            this.panel1.Location = new System.Drawing.Point(391, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 371);
            this.panel1.TabIndex = 13;
            this.panel1.Visible = false;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::CaseManager.Properties.Resources.zmn3;
            this.pictureBox_logo.Location = new System.Drawing.Point(26, 12);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(344, 344);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_logo.TabIndex = 12;
            this.pictureBox_logo.TabStop = false;
            this.pictureBox_logo.Visible = false;
            // 
            // timer_showLogo
            // 
            this.timer_showLogo.Tick += new System.EventHandler(this.timer_showLogo_Tick_1);
            // 
            // panel_loading
            // 
            this.panel_loading.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel_loading.Controls.Add(this.panel_5);
            this.panel_loading.Controls.Add(this.panel_4);
            this.panel_loading.Controls.Add(this.panel_3);
            this.panel_loading.Controls.Add(this.panel_2);
            this.panel_loading.Controls.Add(this.panel_1);
            this.panel_loading.Controls.Add(this.panel_0);
            this.panel_loading.Location = new System.Drawing.Point(813, 182);
            this.panel_loading.Name = "panel_loading";
            this.panel_loading.Size = new System.Drawing.Size(234, 51);
            this.panel_loading.TabIndex = 14;
            // 
            // panel_5
            // 
            this.panel_5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_5.Location = new System.Drawing.Point(199, 18);
            this.panel_5.Name = "panel_5";
            this.panel_5.Size = new System.Drawing.Size(17, 17);
            this.panel_5.TabIndex = 2;
            // 
            // panel_4
            // 
            this.panel_4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_4.Location = new System.Drawing.Point(163, 18);
            this.panel_4.Name = "panel_4";
            this.panel_4.Size = new System.Drawing.Size(17, 17);
            this.panel_4.TabIndex = 2;
            // 
            // panel_3
            // 
            this.panel_3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_3.Location = new System.Drawing.Point(127, 18);
            this.panel_3.Name = "panel_3";
            this.panel_3.Size = new System.Drawing.Size(17, 17);
            this.panel_3.TabIndex = 2;
            // 
            // panel_2
            // 
            this.panel_2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_2.Location = new System.Drawing.Point(91, 18);
            this.panel_2.Name = "panel_2";
            this.panel_2.Size = new System.Drawing.Size(17, 17);
            this.panel_2.TabIndex = 2;
            // 
            // panel_1
            // 
            this.panel_1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_1.Location = new System.Drawing.Point(55, 18);
            this.panel_1.Name = "panel_1";
            this.panel_1.Size = new System.Drawing.Size(17, 17);
            this.panel_1.TabIndex = 2;
            // 
            // panel_0
            // 
            this.panel_0.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel_0.Location = new System.Drawing.Point(19, 18);
            this.panel_0.Name = "panel_0";
            this.panel_0.Size = new System.Drawing.Size(17, 17);
            this.panel_0.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1275, 640);
            this.Controls.Add(this.panel_loading);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "啄木鸟UI自动化工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer_2.Panel1.ResumeLayout(false);
            this.splitContainer_2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            this.splitContainer_main.Panel2.PerformLayout();
            this.splitContainer_main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.panel_loading.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当前用例ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开的用例ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer_2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.Timer timerExecute;
        private System.Windows.Forms.ToolStripMenuItem 目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用例目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem python工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 需安装的包ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Timer timer_hideLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer_showLogo;
        private System.Windows.Forms.ToolStripMenuItem 下载各版本chromedriverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chrome浏览器版本与驱动版本对应关系ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于啄木鸟ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常见问题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_loading;
        private System.Windows.Forms.Panel panel_0;
        private System.Windows.Forms.Panel panel_5;
        private System.Windows.Forms.Panel panel_4;
        private System.Windows.Forms.Panel panel_3;
        private System.Windows.Forms.Panel panel_2;
        private System.Windows.Forms.Panel panel_1;
        private System.Windows.Forms.ToolStripMenuItem 刷新用例树ToolStripMenuItem;
    }
}

