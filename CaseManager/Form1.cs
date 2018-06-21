using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace CaseManager
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  
            Control.CheckForIllegalCrossThreadCalls = false;
            nodeManager = new iTreeNodeManager(this);
            actionLineListClipboard = new List<ActionLine>();
        }

        private int numOfLoadPanel = 0;

        public List<ActionLine> actionLineListClipboard;

        public TextBox getTextBox()
        {
            return this.textBox_log;
        }

        public void showLoading()
        {
            this.panel_loading.Left = (this.Width - this.panel_loading.Width) / 2;
            this.panel_loading.Top = (this.Height - this.panel_loading.Height) / 2-80;
            this.panel_loading.Visible = true;
        }
        public void hideLoading()
        {
            this.panel_loading.Visible = false;
        }

        public iTreeNodeManager nodeManager;// = new iTreeNodeManager(  );

        public PanelAndTabControl panelAndTabControl;

        public Form_CopySteps form_CopySteps;

        public bool closeAfterRunEnd = false;

        iTreeNode itn = null;


        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel_loading.Visible = false;
  

            String prPath = new TextProcessor().getAbsPath();
            this.Text += "-" + Directory.CreateDirectory(prPath).Name + "20180514版本" ;


            //自动运行
            if (File.Exists(prPath + "\\AUTORUN.txt"))
            {
                this.WindowState = FormWindowState.Minimized;


                this.closeAfterRunEnd = true;

                List<String> list = new DirectoryManager().getAllFiles(prPath + "\\测试用例", "xls");

                List<String> list2 = new List<string>();

                foreach(String str in list)
                {
                    if (!str.Contains("\\测试用例\\公共\\"))
                    {
                        list2.Add(str);
                    }
                }

                new Runner().xxxxxx(new FormAndList(this, list2));

            }
            else
            {
                Form1_ResizeEnd(sender, e);

                itn = nodeManager.getTreefromPath(prPath + "\\测试用例");


                treeView1.Nodes.Add(itn);
                treeView1.Width = treeView1.Parent.Width;
                treeView1.Height = treeView1.Parent.Height;
                treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseDoubleClick);

                panelAndTabControl = new PanelAndTabControl();


           //     splitContainer_main.SplitterDistance = 100;

                this.splitContainer_2.Panel2.Controls.Add(panelAndTabControl);
                panelAndTabControl.Top = 0;
                panelAndTabControl.Left = 0;

                panelAndTabControl.Width = panelAndTabControl.Parent.Width;
                panelAndTabControl.Height = panelAndTabControl.Parent.Height;

                panelAndTabControl.tabControl.Width = panelAndTabControl.Width;
                panelAndTabControl.tabControl.Height = panelAndTabControl.Height;

                this.Form1_SizeChanged(null, null);



                this.timer_showLogo.Enabled = true;




   

                

   

            }



        }
        int caseHadOpened(TabControl tabControl, String caseFilePath)
        {
            int i = 0;
            foreach (TabPage tabPage in tabControl.Controls)
            {
                if (tabPage.Name == caseFilePath)
                {
                    return i;
                }
                i++;
            }

            return -1;



        }



        //树结点双击
        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //////////Console.WriteLine("双击");
            iTreeNode node = (iTreeNode)e.Node;

            if (node.ntype == NodeType.FILE)
            {
                //////////Console.WriteLine(node.path);

                //      System.Diagnostics.Process.Start("excel.exe", node.path);

                //////////Console.WriteLine("文件路径" + node.path);



                String filePath = node.path;





                int i = caseHadOpened(this.panelAndTabControl.tabControl, filePath);

                if (i > -1) //已经打开
                {
                    this.panelAndTabControl.tabControl.SelectedIndex = i;


                }
                else
                {

                    try
                    {


                        this.showLoading();

                        List<Action> actionList = new ExcelProcessor().readActionListFromExcelFile(node.path);

                        ActionLines actionLines = new ActionLines(actionList, this.panelAndTabControl.Width - 33, this);

                        //Console.WriteLine("所有actionLines生成完成" +  DateTime.Now);

                        ETabPage eTabPage = new ETabPage(this, actionLines, filePath);


                        //Console.WriteLine("eTabPage生成完成" + DateTime.Now);




                        this.panelAndTabControl.tabControl.Controls.Add(eTabPage);
                        int count = this.panelAndTabControl.tabControl.Controls.Count;
                        this.panelAndTabControl.tabControl.SelectedIndex = count - 1;



                    }
                    catch (Exception e99)
                    {

                        MessageBox.Show(e99.Message);
                    }
                    
                    

                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            nodeManager.selectTree((iTreeNode)(e.Node), e.Node.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Dictionary<String, String> dic = new Dictionary<string, string>();

            //dic.Add("a", "A");
            //dic.Add("b", "B");

            new CaseCreater().createpy("case.py", "template.txt");


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<List<String>> list = new ExcelProcessor().readfirststr("a.xls", "Sheet1");
            ////////Console.WriteLine(list);
        }

















        private void button_save_Click(object sender, EventArgs e)
        {
            ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
            eTabPage.save();
        }

        private void splitContainer_2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.treeView1.Width = this.treeView1.Parent.Width;
            this.treeView1.Height = this.treeView1.Parent.Height;


            if (this.panelAndTabControl != null)
            {
                this.panelAndTabControl.Width = this.panelAndTabControl.Parent.Width;
                this.panelAndTabControl.Height = this.panelAndTabControl.Parent.Height;
                this.panelAndTabControl.tabControl.Width = this.panelAndTabControl.Width;
                this.panelAndTabControl.tabControl.Height = this.panelAndTabControl.Height;
                this.panelAndTabControl.setETabPageSizes(this.panelAndTabControl.tabControl.Width, this.panelAndTabControl.tabControl.Height);


                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    actionLines.setIndent();

                }

            }



        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            List<String> list = new List<String>();
            foreach (ETabPage eTabPage in this.panelAndTabControl.tabControl.Controls)
            {
                list.Add(eTabPage.Name);


            }

            form_CopySteps = new Form_CopySteps(this, list);
            form_CopySteps.Show();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.panelAndTabControl.copyOrCutSteps(0, 1, 5, 1, 1, "cut");


        }

        private void splitContainer_main_Resize(object sender, EventArgs e)
        {
            ////////Console.WriteLine("ssssssssssss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ETabPage etabPage = (ETabPage)this.panelAndTabControl.tabControl.Controls[0];
            etabPage.BackColor = Color.Blue;
            etabPage.Width -= 20;
        }



        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal)
            {
                this.Form1_ResizeEnd(sender, e);
            }


        }

        private void 复制或剪切步骤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<String> targetCasePath = new List<string>();

            foreach (ETabPage eTabPage in this.panelAndTabControl.tabControl.TabPages)
            {
                ////////Console.WriteLine(eTabPage.Name);
                targetCasePath.Add(eTabPage.Name);
            }


            new Form_CopySteps(this, targetCasePath).ShowDialog();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelAndTabControl.saveSelectedTab();

        }

        private void 保存全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panelAndTabControl.saveAllTab();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void splitContainer_main_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.Form1_ResizeEnd(sender, e);//  .Form1_SizeChanged(sender, e);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Dictionary<String, String> dic = new ExcelProcessor().getDicByEnvNameFromExcel("d:\\e.xls", "e3");
            ////////Console.WriteLine(dic);

        }

        private void button_runAll_Click(object sender, EventArgs e)
        {
            this.closeAfterRunEnd = true;

            String prPath = new TextProcessor().getAbsPath();
            List<String> list = new DirectoryManager().getAllFiles(prPath + "\\测试用例", "xls");

            new Runner().xxxxxx(new FormAndList(this, list));

        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            new CommandExecutor().execute2(@"E:\myCsharp\CaseManagerPy\CaseManager\bin\Debug\PythonSelenium2\src\testCase\aaa.py");
        }

        private void timerExecute_Tick(object sender, EventArgs e)
        {

        }

        private String path2ImportCode(String path)
        {
            String[] tmps = path.Split(new String[] { "src\\" }, StringSplitOptions.None);
            String tmp = tmps[1].Replace(".py", "").Replace("\\", ".");
            String[] tmps2 = tmp.Split(new String[] { "." }, StringSplitOptions.None);
            String pythonClassName = tmps2[tmps2.Length - 1];
            String code = "from " + tmp + " import " + pythonClassName;
            return code;
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            List<String> list = new DirectoryManager().getAllFiles(@"E:\myCsharp\CaseManagerPy\CaseManager\bin\Debug\PythonSelenium2\src\testPage", "py");

            List<String> list2 = new List<string>();


            foreach (String str in list)
            {
                if (!str.Contains("__init__"))
                {
                    String codeLine = path2ImportCode(str);
                    list2.Add(codeLine);
                }

            }
            foreach (String str in list2)
            {
                //////Console.WriteLine(str);

            }



        }

        private void button1_Click_6(object sender, EventArgs e)
        {
            String tt = new TextProcessor().getRandom(6);

            //////Console.WriteLine(tt);
        }

        private void button1_Click_7(object sender, EventArgs e)
        {
            this.Height -= 1;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {/*

            //Console.WriteLine("Form1_ResizeEnd");

            //Console.WriteLine("this.Height=" + this.Height);
            //Console.WriteLine("this.splitContainer_main.SplitterDistance=" + this.splitContainer_main.SplitterDistance);

   */


            this.splitContainer_main.Width = this.Width - splitContainer_main.Left - 18;
            this.splitContainer_main.Top = 30;
            this.splitContainer_main.Height = this.Height - splitContainer_main.Top - 40;




            this.splitContainer_2.Width = this.splitContainer_2.Parent.Width;
            this.splitContainer_2.Height = this.splitContainer_2.Parent.Height - 4;



            treeView1.Width = treeView1.Parent.Width;
            treeView1.Height = treeView1.Parent.Height;

            if (null != panelAndTabControl)
            {
                panelAndTabControl.Width = panelAndTabControl.Parent.Width;
                panelAndTabControl.Height = panelAndTabControl.Parent.Height;


                panelAndTabControl.tabControl.Width = panelAndTabControl.Width;
                panelAndTabControl.tabControl.Height = panelAndTabControl.Height;


                panelAndTabControl.setETabPageSizes(panelAndTabControl.tabControl.Width, panelAndTabControl.tabControl.Height);

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    actionLines.setIndent();

                }



            }
            this.textBox_log.Size = this.textBox_log.Parent.Size;






        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            //Console.WriteLine(e.KeyCode);


            //Console.WriteLine("shift=" + e.Shift);

            //Console.WriteLine("control=" + e.Control);

            //Console.WriteLine("shift + control=" + (e.Shift && e.Control));

            //Console.WriteLine("按下c=" + ( e.KeyCode==   Keys.C));


            //Console.WriteLine(e.KeyCode == Keys.C && e.Shift && e.Control);
             * */


            





            //快捷键

            //运行
            /*
            if (e.KeyCode == Keys.R && e.Control)
            {
                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    String filePath = eTabPage.Name;
                    List<String> list = new List<String>();
                    list.Add(filePath);
                    new Runner().xxxxxx(new FormAndList(this, list));
                }
            }
             * */
            //保存
            /*
            if (e.KeyCode == Keys.S && e.Control)
            {
                this.panelAndTabControl.saveSelectedTab();
            }
             * */
            //插入一行
            if (e.KeyCode == Keys.I && e.Control || e.KeyCode == Keys.Q && e.Control)
            {
                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;
                    int currentIndex = actionLines.currentIndex;
                    actionLines.insertActionLine(currentIndex, 1);
                }
            }
            //删除一行
            else if (e.KeyCode == Keys.D && e.Control)
            {
                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;
                    int currentIndex = actionLines.currentIndex;
                    actionLines.deleteActionLine(currentIndex);
                }


            }
            else if (e.KeyCode == Keys.Divide && e.Control)
            {

               
                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    ((ActionLine)actionLines.Controls[currentIndex]).setColorByIsRunToOther();

                   
                }

            }










            //设置为单步
            else if (e.KeyCode == Keys.D1 && e.Control)
            {


                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;
                    int currentIndex = actionLines.currentIndex;

                    ((ActionLine)actionLines.Controls[currentIndex]).comboBox_key.Text = "单步方法";

                }
            }


            else if (e.KeyCode == Keys.Up && e.Control)
            {

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    actionLines.up(currentIndex);
                }
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    actionLines.down(currentIndex);
                }
            }

           

    

/*
            else if (e.KeyCode == Keys.C && e.Shift && e.Control  )
            {

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    ActionLine actionLine = ((ActionLine)(actionLines.Controls[currentIndex]));

                    ((ActionLineContextMenuStrip)(actionLine.ContextMenuStrip)).item_CopyMul_Click(null, null);
                }
            }



            else if ((e.KeyCode == Keys.C) && e.Shift)
            {

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    ActionLine actionLine = ((ActionLine)(actionLines.Controls[currentIndex]));

                    ((ActionLineContextMenuStrip)(actionLine.ContextMenuStrip)).item_Copy_Click(null, null);
                }
            }
            


            else if (e.KeyCode == Keys.V && e.Shift)
            {

                ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
                if (null != eTabPage)
                {
                    ActionLines actionLines = eTabPage.actionLines;

                    int currentIndex = actionLines.currentIndex;

                    ActionLine actionLine = ((ActionLine)(actionLines.Controls[currentIndex]));

                    ((ActionLineContextMenuStrip)(actionLine.ContextMenuStrip)).item_Paste_Click(null, null);
                }
            }
            */




        }

        private void 当前用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
            if (null != eTabPage)
            {


                String filePath = eTabPage.Name;
                List<String> list = new List<String>();
                list.Add(filePath);
                new Runner().xxxxxx(new FormAndList(this, list));
            }

        }

        private void 打开的用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;

            if (null != eTabPage)
            {
                List<String> list = new List<String>();

                System.Windows.Forms.Control.ControlCollection eTabPages = eTabPage.Parent.Controls;
                foreach (ETabPage tmp in eTabPages)
                {
                    list.Add(tmp.Name);
                }
                new Runner().xxxxxx(new FormAndList(this, list));
            }



        }

        private void 测试用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", new TextProcessor().getAbsPath() + "\\测试用例");

        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", new TextProcessor().getAbsPath() + "\\conf");

        }

        private void python工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", new TextProcessor().getAbsPath() + "\\PythonSelenium2");

        }

        private void seleniumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandExecutor().execute("pip install selenium==2.53.0");
        }

        private void pillowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandExecutor().execute("pip install pillow");

        }

        private void pymysqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandExecutor().execute("pip install pymysql");

        }

        private void requestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandExecutor().execute("pip install requests");

        }

        private void pynputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandExecutor().execute("pip install pynput");

        }

        private void 需安装的包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String command = new TextProcessor().getAbsPath() + "\\pip\\pip.bat";
            new CommandExecutor().execute(command);


        }

        private void button1_Click_8(object sender, EventArgs e)
        {
            List<List<String>> lists = new ExcelProcessor().readfirststr("d:\\Book1.xls");

           // //Console.WriteLine(lists);


        }

        private void timer_hideLogo_Tick(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.timer_hideLogo.Enabled = false;

        }



        private  Bitmap KiCut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            if (StartX >= w || StartY >= h)
            {
                return null;
            }

            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }

            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }


        private void showLogo()
        {

            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2;

            panel1.Visible = false;
            pictureBox_logo.Visible = false;


            Bitmap b = new Bitmap(this.Width, this.Height);
            //得到窗口的图片
            this.DrawToBitmap(b, new Rectangle(0, 0, this.Width, this.Height));
            Bitmap b2 = this.KiCut(b, panel1.Left + 8, panel1.Top + 31, panel1.Width, panel1.Height);
            panel1.BackgroundImage = b2;
            pictureBox_logo.BackColor = Color.Transparent;

            panel1.Visible = true;
            pictureBox_logo.Visible = true;
        
        
        }
        


        private void button1_Click_9(object sender, EventArgs e)
        {
            

        }

        private void timer_showLogo_Tick(object sender, EventArgs e)
        {
           
            this.showLogo();
            this.timer_hideLogo.Enabled = true;
        }

        private void timer_showLogo_Tick_1(object sender, EventArgs e)
        {
            this.timer_showLogo.Enabled = false;

            this.showLogo();

            this.timer_hideLogo.Enabled = true;
        }

        private void button1_Click_10(object sender, EventArgs e)
        {
            this.showLogo();
        }

        private void 下载各版本chromedriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openUrl("http://chromedriver.storage.googleapis.com/index.html");


        }

        private void openUrl(String url)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();

            String[] tmps = s.Split(new String[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
            String tmp = tmps[0];
            System.Diagnostics.Process.Start(tmp,url);
        
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ETabPage eTabPage = (ETabPage)this.panelAndTabControl.tabControl.SelectedTab;
            new Form_Search(eTabPage).ShowDialog();



        }

        private void timer_loading_Tick(object sender, EventArgs e)
        {
            this.numOfLoadPanel = (this.numOfLoadPanel + 1) % 7;
            Console.WriteLine(numOfLoadPanel);
            if (0 == this.numOfLoadPanel)
            {
                this.panel_0.Visible = false;
                this.panel_1.Visible = false;
                this.panel_2.Visible = false;
                this.panel_3.Visible = false;
                this.panel_4.Visible = false;
                this.panel_5.Visible = false;
            
            }
            else if (1 == this.numOfLoadPanel)
            {
                this.panel_0.Visible = true;
            
            }
            else if (2 == this.numOfLoadPanel)
            {
                this.panel_1.Visible = true;

            }
            else if (3 == this.numOfLoadPanel)
            {
                this.panel_2.Visible = true;

            }
            else if (4 == this.numOfLoadPanel)
            {
                this.panel_3.Visible = true;

            }
            else if (5 == this.numOfLoadPanel)
            {
                this.panel_4.Visible = true;

            }
            else
            {
                this.panel_5.Visible = true;

            }


        }

        private void 刷新用例树ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String prPath = new TextProcessor().getAbsPath();
            itn = nodeManager.getTreefromPath(prPath + "\\测试用例");

            treeView1.Nodes.RemoveAt(0);

            treeView1.Nodes.Add(itn);
        }

        






    }





    public class AnnotationTextBox : TextBox
    {
        public AnnotationTextBox()
            : base()
        {
            this.ForeColor = Color.Green;

        }
    }

    public class PythonTextBox : TextBox
    {
        public PythonTextBox()
            : base()
        {
            this.ForeColor = Color.Blue;

        }
    }


    //带有一个TabControl的Panel
    public class PanelAndTabControl : Panel
    {
        public TabControl tabControl;



        //构造方法
        public PanelAndTabControl()
            : base()
        {
            tabControl = new TabControl();
            tabControl.Left = 0;
            tabControl.Top = 0;

            ////////Console.WriteLine("tabControl宽度=" + tabControl.Width);
            this.Controls.Add(tabControl);
        }

        //增加一个 tabPage
        public void addTabPage(ETabPage tabPage)
        {
            this.tabControl.Controls.Add(tabPage);
            ////////Console.WriteLine(tabPage.Name);
        }

        //设置所有 eTabPage的宽度和高度
        public void setETabPageSizes(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            foreach (ETabPage tabPage in this.tabControl.Controls)
            {
                //     tabPage.BackColor = Color.FromArgb(196, 196, 196);

                tabPage.Width = width;
                tabPage.Height = height;
                tabPage.setActionLinesSize(width - 40, height);
            }




        }

        //list1为插入的一个list
        //list2为被插入的
        public List<Action> insertListToList(List<Action> list1, List<Action> list2, int i)
        {
            List<Action> list3 = new List<Action>();

            if (i >= 0 && i < list2.Count)
            {
                List<Action> tmp1 = list2.GetRange(0, i);
                List<Action> tmp2 = list2.GetRange(i, list2.Count - i);

                list3.AddRange(tmp1);
                list3.AddRange(list1);
                list3.AddRange(tmp2);
            }
            else if (i < 0)
            {
                list3.AddRange(list1);
                list3.AddRange(list2);

            }
            else if (i >= list2.Count)
            {

                list3.AddRange(list2);
                list3.AddRange(list1);

            }
            return list3;


        }

        public void saveSelectedTab()
        {
            ETabPage eTabPage = (ETabPage)this.tabControl.SelectedTab;
            if (null != eTabPage)
            {
                eTabPage.save();
            }
        }
        public void saveAllTab()
        {
            foreach (ETabPage eTabPage in this.tabControl.TabPages)
            {
                eTabPage.save();
            }
        }


        //复制或剪切一些步骤
        public void copyOrCutSteps(int sourceTabIndex, int sourceStepStart, int sourceStepEnd, int targetTabIndex, int targetStep, String copyOrCut)
        {
            ETabPage eTabPage_source = (ETabPage)this.tabControl.Controls[sourceTabIndex];
            ETabPage eTabPage_target = (ETabPage)this.tabControl.Controls[targetTabIndex];

            eTabPage_source.actionLines.refreshActionList();
            eTabPage_target.actionLines.refreshActionList();

            List<Action> actionList_source = eTabPage_source.actionLines.actionList;
            List<Action> actionList_target = eTabPage_target.actionLines.actionList;

            if (actionList_source.Count > 0)
            {
                //sourceStepStart
                if (sourceStepStart < 0)
                {
                    sourceStepStart = 0;
                }
                if (sourceStepStart > actionList_source.Count - 1)
                {
                    sourceStepStart = actionList_source.Count - 1;
                }
                //sourceStepEnd

                if (sourceStepEnd < 0)
                {
                    sourceStepEnd = 0;
                }

                if (sourceStepEnd > actionList_source.Count - 1)
                {
                    sourceStepEnd = actionList_source.Count - 1;

                }


                if (targetStep < 0)
                {
                    targetStep = 0;

                }
                ///****************
                if (targetStep > actionList_target.Count)
                {
                    targetStep = actionList_target.Count;

                }



                List<Action> listTmp = actionList_source.GetRange(sourceStepStart, sourceStepEnd - sourceStepStart + 1);

                List<Action> actionList_targetNew = new List<Action>();

                //复制或剪切给自己
                if (sourceTabIndex == targetTabIndex)
                {
                    //List<Action> actionList_targetNew = new List<Action>();

                    //复制
                    if (copyOrCut.ToUpper() == "COPY")
                    {


                        //复制到自己中  不允许
                        if (targetStep > sourceStepStart && targetStep <= sourceStepEnd)
                        {
                            MessageBox.Show("不允许往源步骤中粘贴");

                        }

                        //复制中间其他地方复制
                        else
                        {

                            actionList_targetNew = insertListToList(listTmp, actionList_source, targetStep);

                            eTabPage_source.actionLines.actionList = actionList_targetNew;

                            eTabPage_source.actionLines.refreshGui();


                        }



                    }
                    //剪切
                    else
                    {
                        if (targetStep > sourceStepStart && targetStep <= sourceStepEnd)
                        {
                            MessageBox.Show("不允许往源步骤中粘贴");

                        }


                        //后面剪切到前面  //先删除  后插入

                        else if (targetStep <= sourceStepStart)
                        {

                            int l = listTmp.Count;

                            for (int i = 0; i < l; i++)
                            {

                                actionList_source.RemoveAt(sourceStepStart);
                            }
                            actionList_targetNew = insertListToList(listTmp, actionList_source, targetStep);

                            eTabPage_source.actionLines.actionList = actionList_targetNew;

                            eTabPage_source.actionLines.refreshGui();

                        }

                        //前面剪切到后面   //先插入  后删除
                        else if (targetStep > sourceStepEnd)
                        {
                            actionList_targetNew = insertListToList(listTmp, actionList_source, targetStep);
                            int l = listTmp.Count;

                            for (int i = 0; i < l; i++)
                            {

                                actionList_targetNew.RemoveAt(sourceStepStart);
                            }

                            eTabPage_source.actionLines.actionList = actionList_targetNew;

                            eTabPage_source.actionLines.refreshGui();


                        }
                        else
                        {
                            MessageBox.Show("不允许");
                        }
                    }



                }
                else //复制或剪切到其他tab
                {
                    //List<Action> actionList_targetNew = new List<Action>();
                    //复制
                    if (copyOrCut.ToUpper() == "COPY")
                    {


                        actionList_targetNew = insertListToList(listTmp, actionList_target, targetStep);

                        eTabPage_target.actionLines.actionList = actionList_targetNew;

                        eTabPage_target.actionLines.refreshGui();

                        this.tabControl.SelectedIndex = targetTabIndex;


                    }
                    else
                    {

                        actionList_targetNew = insertListToList(listTmp, actionList_target, targetStep);

                        eTabPage_target.actionLines.actionList = actionList_targetNew;

                        eTabPage_target.actionLines.currentIndex = targetStep;

                        eTabPage_target.actionLines.refreshGui();

                        this.tabControl.SelectedIndex = targetTabIndex;


                        int l = listTmp.Count;

                        for (int i = 0; i < l; i++)
                        {

                            actionList_source.RemoveAt(sourceStepStart);
                            eTabPage_source.actionLines.actionList = actionList_source;

                        }
                        eTabPage_source.actionLines.currentIndex = sourceStepStart - 1;

                        eTabPage_source.actionLines.refreshGui();

                    }



                }

            }
            else
            {
                MessageBox.Show("源长度为0");
            }








            /*

                        if (targetStep < actionList_target.Count)
                        {

                        }
                        else
                        {
                            //    actionList_targetNew.AddRange(actionList_target);
                            //    actionList_targetNew.AddRange(listTmp);
                        }
                        //  eTabPage_target.actionLines.actionList = actionList_targetNew;
                        eTabPage_target.actionLines.refreshGui();

                        */






        }
    }

    public class ETabPage : TabPage
    {
        public Form1 form;

        public ActionLines actionLines;

        

        public ETabPage(Form1 form, ActionLines actionLines, String filePath)
            : base()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  



            this.form = form;
            this.Name = filePath;

            String[] tmps = filePath.Split(new String[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            String fileName = tmps[tmps.Length - 1];

            this.Text = fileName;

            this.actionLines = actionLines;
            this.Controls.Add(this.actionLines);

            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(196, 196, 196);


            this.ContextMenuStrip = new EtagContextMenuStrip(this.form, this);


        }

        public void save()
        {
            List<List<String>> listList = this.actionLines.toListList();
            String filePath = this.Name;
            new ExcelProcessor().writeToExcelFile(listList, filePath);

        }

        public void setActionLinesSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.actionLines.setActionLineWidth(width);

        }

    }

    //iTreeNode管理器

    public class iTreeNodeManager
    {

        Form1 form;

        public iTreeNodeManager(Form1 form)
        {
            this.form = form;
        }

        public iTreeNode getTreefromPath(String path)
        {
            iTreeNode itn = new iTreeNode(this.form, path, NodeType.DIR);
            itn.ChildNodesAddChildNodes();
            return itn;
        }



        //得到一个文件夹节点下的所有选中的文件节点
        public ArrayList getSelectedFileNodes(iTreeNode itn)
        {
            selectedFileNodes.Clear();
            addChildNodestoList(itn);
            return selectedFileNodes;
        }




        public ArrayList selectedFileNodes = new ArrayList();

        //得到这个节点下的所有的选中的文件
        public void addChildNodestoList(iTreeNode itn)
        {

            foreach (iTreeNode n in itn.Nodes)
            {

                //是目录的
                if (n.ntype == NodeType.DIR)
                {
                    //递归调用自己
                    addChildNodestoList(n);

                }
                //是文件
                else
                {
                    //是选中的
                    if (n.Checked == true)
                    {

                        selectedFileNodes.Add(n);

                    }
                }

            }

        }

        public void selectTree(iTreeNode itn, bool isSelect)
        {

            TreeNodeCollection list = itn.Nodes;
            foreach (iTreeNode n in list)
            {
                n.Checked = isSelect;
                if (n.ntype == NodeType.DIR)
                {
                    selectTree(n, isSelect);
                }
            }
        }
    }

    class FormAndList
    {
        public Form1 form;
        public List<String> list;

        public FormAndList(Form1 form, List<String> list)
        {
            this.form = form;
            this.list = list;

        }

    }

    class FormAndListAndIndex
    {
        public Form1 form;
        public String filePath;
        public int index;

        public FormAndListAndIndex(Form1 form, String filePath, int index)
        {
            this.form = form;
            this.filePath = filePath;
            this.index = index;
        }

    }


    class Runner
    {


        String reportFile = new TextProcessor().getAbsPath() + "\\report\\report.html";
        String pyFile = new TextProcessor().getAbsPath() + "\\PythonSelenium2\\src\\testCase\\TmpCase.py";


        String logFile = new TextProcessor().getAbsPath() + "\\PythonSelenium2\\log\\testCase\\TmpCase\\log.txt";
        String picFile = new TextProcessor().getAbsPath() + "\\PythonSelenium2\\log\\testCase\\TmpCase\\screenCapture\\img.jpg";

        /*
        public Runner(Form1 form)
        {
            this.form = form;
        
        }
         * */


        public void xxxxxx(Object o)
        {
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(a);

            Thread thread = new Thread(Pts);
            thread.Start(o);

        }


        public void a(Object o)
        {
            FormAndList formAndList = (FormAndList)o;

            Form1 form = formAndList.form;
            List<String> filePaths = formAndList.list;

            runFiles(form, filePaths);//


        }






        public void runFiles(Form1 form, List<String> filePaths)
        {
            TextProcessor tp = new TextProcessor();
            int beginIndex = tp.getBeginIndex();

            String prPath = new TextProcessor().getAbsPath();

            //得到报告目录下的日志的目录
            String reportLogFile = prPath + "\\report\\log";

            //得到报告目录下日志目录下的图片目录
            String reportPicFile = prPath + "\\report\\pic";


            //清空这两个目录
            new DirectoryManager().clearDir(reportLogFile);
            new DirectoryManager().clearDir(reportPicFile);




            tp.writeContent(reportFile, "<html>", false);
            tp.writeContent(reportFile, "<head>", true);
            tp.writeContent(reportFile, "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />", true);
            tp.writeContent(reportFile, "<title>自动化测试报告</title>", true);
            tp.writeContent(reportFile, "<style type=\"text/css\">", true);
            tp.writeContent(reportFile, ".success {background-color: #66FF33}", true);
            tp.writeContent(reportFile, ".failed {background-color: red}", true);
            tp.writeContent(reportFile, "table {border-spacing:0}", true);
            tp.writeContent(reportFile, "td {padding: 4px;border:1px solid #7F7F7F}", true);
            tp.writeContent(reportFile, "</style>", true);
            tp.writeContent(reportFile, "</head>", true);
            tp.writeContent(reportFile, "<body>", true);
            tp.writeContent(reportFile, "<table>", true);
            tp.writeContent(reportFile, "<tr><td>序号</td><td>用例文件</td><td>结果</td><td>日志</td><td>截图</td></tr>", true);



            int l = filePaths.Count;

            String exeStatuFile = prPath + "\\executestatu.txt";
            for (int i = 0; i < l; i++)
            {
                //tp.writeContent(exeStatuFile, "RUNNING_" + (i + beginIndex), false); 
                String trStr = runFile(form, filePaths[i], i + beginIndex);
                tp.writeContent(reportFile, trStr, true);
            }

            tp.writeContent(reportFile, "</table>", true);
            tp.writeContent(reportFile, "</body>", true);
            tp.writeContent(reportFile, "</html>", true);




            //如果是使用 执行全部用例 这个按钮调用的 ，跑完了就会关闭窗口
            if (form.closeAfterRunEnd)
            {
                form.Close();
            }
        }





        String runFile(Form1 form, String filePath, int num)
        {
            String prPath = new TextProcessor().getAbsPath();


            try
            {

                String script = new TextProcessor().getWholePyCodeFromScriptFile(prPath + "\\conf\\pythoncode.txt", prPath + "\\PythonSelenium2\\src\\testPage", filePath, prPath + "\\conf\\env.txt", prPath + "\\conf\\data.xls", prPath + "\\conf\\import.txt");


                File.WriteAllText(pyFile, script, new UTF8Encoding(false));

                FormAndListAndIndex formAndListAndIndex = new FormAndListAndIndex(form, filePath, num);


                ParameterizedThreadStart Pts = new ParameterizedThreadStart(ant);

                Thread thread = new Thread(Pts);




                this.tr = null;



                thread.Start(formAndListAndIndex);




                //每一秒检查 tr 的值，如果是 null ，说明这个用例还没跑，如果达到次数还没跑完，说明就是超时了
                int t = 0;


                while (this.tr == null && t < 300)
                {

                    Thread.Sleep(1000);
                    t++;


                }
                if (tr == null)
                {

                    form.getTextBox().AppendText("用例超时，被强行停止。");

                    thread.Abort();

                    new CommandExecutor().execute("taskkill /im chromedriver.exe /F");
                    new CommandExecutor().execute("taskkill /im chrome.exe /F");


                    String tdNum = "<td>" + num + "</td>";
                    String tdFilePath = "<td>" + filePath + "</td>";
                    String tdResult = "<td class ='timeout'>超时</td>";
                    String tdLog = "<td>无日志</td>";
                    String tdPic = "<td>无截屏</td>";

                    String prPah = new TextProcessor().getAbsPath();

                    //得到日志的文本
                    String reportLogFile = prPah + "\\report\\log\\" + num + "log.txt";
                    String reportLogFileInHtml = "log/" + num + "log.txt";
                    String reportPicFile = prPah + "\\report\\pic\\" + num + "pic.jpg";
                    String reportPicFileInHtml = "pic/" + num + "pic.jpg";


                    if (File.Exists(logFile))
                    {
                        if (File.Exists(reportLogFile))
                        {
                            File.Delete(reportLogFile);
                        }
                        File.Copy(logFile, reportLogFile, true);

                        tdLog = "<td><a href='" + reportLogFileInHtml + "' target='_black'>日志下载</a></td>";
                    }
                    if (File.Exists(picFile))
                    {
                        if (File.Exists(reportPicFile))
                        {
                            File.Delete(reportPicFile);
                        }
                        File.Copy(picFile, reportPicFile, true);
                        tdPic = "<td><a href='" + reportPicFileInHtml + "' target='_black' class='failed'>错误截屏</a></td>";
                    }


                    this.tr = "<tr>" + tdNum + tdFilePath + tdResult + tdLog + tdPic + "</tr>";
                    return this.tr;


                }
                else
                {
                    return this.tr;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return "xxx";
            
            
            
            }

            


        }


        public String tr = null;

        public void ant(Object o)
        {
            this.tr = null;

            FormAndListAndIndex t = (FormAndListAndIndex)o;
            this.tr = ant(t.form, t.filePath, t.index);

        }



        public String ant(Form1 form, String filePath, int num)
        {




            String tdNum = "<td>" + num + "</td>";
            String tdFilePath = "<td>" + filePath + "</td>";
            String tdResult = "<td>未执行</td>";
            String tdLog = "<td>无日志</td>";
            String tdPic = "<td>无截屏</td>";


            String prPath = new TextProcessor().getAbsPath();

            System.IO.Directory.SetCurrentDirectory(prPath + "\\PythonSelenium2\\src\\testCase");

            form.getTextBox().AppendText("当前目录=" + prPath + "\\PythonSelenium2\\src\\testCase");


            String srcPath = prPath + "\\PythonSelenium2\\src";
            String setpythonpathCommand = "set pythonpath=" + srcPath;





            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";


            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;

            Process p = Process.Start(info);


            p.StandardInput.WriteLine(setpythonpathCommand);
            p.StandardInput.WriteLine("echo %pythonpath%");
            p.StandardInput.WriteLine("Runner.py&exit");
            p.StandardInput.AutoFlush = true;
            String resultLine = "";


            //      DateTime dateTime = DateTime.Now;

            //dateTime = dateTime.AddMinutes(1);
            //      dateTime = dateTime.AddSeconds(10);





            while (!p.StandardOutput.EndOfStream)
            {
                String tmp = p.StandardOutput.ReadLine();
                ////////Console.WriteLine("tmp=" + tmp);



                form.getTextBox().AppendText(tmp + "\r\n");

                //如果到了这一句  就可以跳出了

                if (tmp.StartsWith("测试结果") && tmp.EndsWith("通过"))
                {
                    resultLine = tmp;
                    break;
                }
            }
            ////////Console.WriteLine("读取到最末尾");

            p.StandardOutput.Close();
            p.StandardInput.Close();
            p.Close();



            System.IO.Directory.SetCurrentDirectory(prPath);


            //if (DateTime.Now >= dateTime)
            if (false)
            {
                //////Console.WriteLine("超时");

                tdResult = "<td class ='failed'>超时</td>";

                new CommandExecutor().execute("taskkill /im chromedriver.exe /F");
                new CommandExecutor().execute("taskkill /im chrome.exe /F");
            }
            else
            {
                bool success = false;

                success = (resultLine == "测试结果通过");







                if (success)
                {
                    tdResult = "<td  class='success'>成功</td>";
                }
                else
                {
                    tdResult = "<td class ='failed'>失败</td>";
                }



            }


            String prPah = new TextProcessor().getAbsPath();

            //得到日志的文本
            String reportLogFile = prPah + "\\report\\log\\" + num + "log.txt";
            String reportLogFileInHtml = "log/" + num + "log.txt";
            String reportPicFile = prPah + "\\report\\pic\\" + num + "pic.jpg";
            String reportPicFileInHtml = "pic/" + num + "pic.jpg";


            if (File.Exists(logFile))
            {
                if (File.Exists(reportLogFile))
                {
                    File.Delete(reportLogFile);
                }
                File.Copy(logFile, reportLogFile, true);

                tdLog = "<td><a href='" + reportLogFileInHtml + "' target='_black'>日志下载</a></td>";
            }
            if (File.Exists(picFile))
            {
                if (File.Exists(reportPicFile))
                {
                    File.Delete(reportPicFile);
                }
                File.Copy(picFile, reportPicFile, true);
                tdPic = "<td><a href='" + reportPicFileInHtml + "' target='_black' class='failed'>错误截屏</a></td>";
            }


            String tr = "<tr>" + tdNum + tdFilePath + tdResult + tdLog + tdPic + "</tr>";

            return tr;
        }



    }



    //右键菜单的类
    class iContextMenuStrip : ContextMenuStrip
    {
        Form1 form;

        //构造函数
        public iContextMenuStrip(Form1 form, iTreeNode node)
            : base()
        {
            ToolStripMenuItem item_run = new ToolStripMenuItem("运行");
            ToolStripMenuItem item_clone = new ToolStripMenuItem("克隆");

            ToolStripMenuItem item_rename = new ToolStripMenuItem("改名");
            //    item_rename.ShortcutKeys = Keys.F2 ;
            //    item_rename.ShowShortcutKeys = true;

            ToolStripMenuItem item_delete = new ToolStripMenuItem("删除");


            ToolStripMenuItem item_createDir = new ToolStripMenuItem("新建目录");
            ToolStripMenuItem item_createCase = new ToolStripMenuItem("新建用例");

            ToolStripMenuItem item_copyPath = new ToolStripMenuItem("复制路径到剪贴板");



            item_run.Click += new EventHandler(this.runNode);
            item_clone.Click += new EventHandler(this.cloneNode);
            item_rename.Click += new EventHandler(this.renameNode);
            item_delete.Click += new EventHandler(this.deleteNode);
            item_createDir.Click += new EventHandler(this.createDirNode);
            item_createCase.Click += new EventHandler(this.createCaseNode);
            item_copyPath.Click += new EventHandler(this.copyPath);

            this.sendernode = node;


            this.Items.Add(item_run);


            if (node.ntype == NodeType.DIR)
            {
                this.Items.Add(item_createDir);
                this.Items.Add(item_createCase);
            }
            else
            {
                this.Items.Add(item_clone);
                this.Items.Add(item_rename);

            }
            this.Items.Add(item_delete);

            this.Items.Add(item_copyPath);


            this.form = form;



        }

        //成员变量
        iTreeNode sendernode = null;

        //增加目录节点
        void createDirNode(object sender, EventArgs e)
        {
            if (this.sendernode.ntype == NodeType.DIR)
            {
                String dirName = Interaction.InputBox("请输入要新建的目录名", "提示", "", 100, 100).Trim();

                if (dirName.Length > 0 && !dirName.Contains("\\") && !dirName.Contains("/") && !dirName.Contains(":") && !dirName.Contains("*") && !dirName.Contains("?") && !dirName.Contains("\"") && !dirName.Contains("<") && !dirName.Contains(">") && !dirName.Contains("|"))
                {
                    String path = this.sendernode.path;
                    String dirPath = path + "\\" + dirName;

                    if (new DirectoryInfo(dirPath).Exists)
                    {
                        MessageBox.Show("已经存在这个目录！", "新建目录失败");
                    }
                    else
                    {
                        Directory.CreateDirectory(dirPath);
                        iTreeNode node = new iTreeNode(form, dirPath, NodeType.DIR);
                        this.sendernode.Nodes.Add(node);
                    }


                }
                else
                {
                    MessageBox.Show("输入值长度为0或包含非法字符，请重新输入！");
                }


            }
            else
            {

                MessageBox.Show("文件下怎么新建目录？", "呵呵");
            }






        }

        void copyPath(object sender, EventArgs e)
        {
            String path = this.sendernode.path;

            int i = path.IndexOf("测试用例");

            path = path.Substring(i);

            //////Console.WriteLine(path);

            Clipboard.SetDataObject(path, true);

        }


        //增加文件节点
        void createCaseNode(object sender, EventArgs e)
        {
            String caseName = Interaction.InputBox("请输入要新建的用例名", "提示", "", 100, 100).Trim();
            if (caseName.Length > 0 && !caseName.Contains("\\") && !caseName.Contains("/") && !caseName.Contains(":") && !caseName.Contains("*") && !caseName.Contains("?") && !caseName.Contains("\"") && !caseName.Contains("<") && !caseName.Contains(">") && !caseName.Contains("|"))
            {
                String path = this.sendernode.path;
                String casePath = path + "\\" + caseName + ".xls";

                if (File.Exists(casePath))
                {
                    MessageBox.Show("已经存在这个用例！", "新建用例失败");
                }
                else
                {
                    try
                    {
                        String prPath = new TextProcessor().getAbsPath();
                        File.Copy(prPath + "\\conf\\初始用例.xls", casePath);
                        iTreeNode node = new iTreeNode(form, casePath, NodeType.FILE);
                        this.sendernode.Nodes.Add(node);

                    }
                    catch (Exception e16)
                    {
                        //////////Console.WriteLine(e16.Message);
                        MessageBox.Show("新建用例失败，是否使用了特殊字符！", "提示");
                    }


                }

            }
            else
            {

                MessageBox.Show("输入值长度为0或包含非法字符，请重新输入！");
            }




        }

        //重命名节点
        void renameNode(object sender, EventArgs e)
        {


            if (this.sendernode.ntype == NodeType.FILE)
            {

                String oldName = this.sendernode.Text.Split(new String[] { "." }, StringSplitOptions.None)[0];


                //弹出一个对话框，得到对话框的输入值
                String newName = Interaction.InputBox("请输入新的用例名", "提示", oldName, 100, 100).Trim();

                //////Console.WriteLine("新名=" + newName);


                if (newName.Length > 0 && !newName.Contains("\\") && !newName.Contains("/") && !newName.Contains(":") && !newName.Contains("*") && !newName.Contains("?") && !newName.Contains("\"") && !newName.Contains("<") && !newName.Contains(">") && !newName.Contains("|"))
                {
                    String path = this.sendernode.path;
                    String newPath = ((iTreeNode)(this.sendernode.Parent)).path + "\\" + newName + ".xls";

                    if (File.Exists(newPath))
                    {
                        MessageBox.Show("已经存在这个文件！", "改名失败");
                    }
                    else
                    {

                        File.Move(path, newPath);
                        this.sendernode.path = newPath;
                        this.sendernode.Text = newName + ".xls";
                    }
                }
                else
                {
                    //MessageBox.Show("输入值长度为0或包含非法字符，请重新输入！");
                }
            }
            else
            {
                MessageBox.Show("只能给用例改名，不能给目录改名！");
            }
        }

        //删除节点
        void deleteNode(object sender, EventArgs e)
        {
            //文件节点
            if (this.sendernode.ntype == NodeType.FILE)
            {
                DialogResult dr = MessageBox.Show("确认删除！", "警告", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)
                {
                    iTreeNode parent = (iTreeNode)this.sendernode.Parent;

                    String path = this.sendernode.path;
                    //////////Console.WriteLine(path);
                    File.Delete(path);
                    parent.Nodes.Remove(this.sendernode);
                }
            }
            //目录节点
            else
            {
                //空的目录节点
                if (this.sendernode.Nodes.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("确认删除！", "警告", MessageBoxButtons.OKCancel);
                    {
                        iTreeNode parent = (iTreeNode)this.sendernode.Parent;
                        String path = this.sendernode.path;
                        new DirectoryInfo(path).Delete();
                        parent.Nodes.Remove(this.sendernode);
                    }
                }
                //非空的目录节点
                else
                {
                    MessageBox.Show("只能删除空的目录！", "提示");
                }
            }



        }


        //克隆节点
        void cloneNode(object sender, EventArgs e)
        {
            //只有文件节点支持克隆
            if (this.sendernode.ntype == NodeType.FILE)
            {
                String path = this.sendernode.path;
                //////////Console.WriteLine(path);
                String pathClone = path.Replace(".xls", "_副本.xls");

                try
                {
                    File.Copy(path, pathClone);
                    iTreeNode nodeClone = new iTreeNode(form, pathClone, NodeType.FILE);
                    this.sendernode.Parent.Nodes.Add(nodeClone);
                }
                catch (Exception e3)
                {
                    MessageBox.Show("可能目标文件已经存在！");
                    //////////Console.WriteLine(e3.Message);
                }
            }
            else
            {
                MessageBox.Show("只支持克隆用例，不支持克隆目录！");
            }

        }

        //运行一个节点
        void runNode(object sender, EventArgs e)
        {
            // ////////Console.WriteLine(this.sendernode.Text);
            List<String> runFileList = new List<string>();

            //如果是一个目录节点
            if (this.sendernode.ntype == NodeType.DIR)
            {
                ArrayList list = new iTreeNodeManager(this.form).getSelectedFileNodes(this.sendernode);
                foreach (iTreeNode n in list)
                {
                    //////////Console.WriteLine(n.path);
                    runFileList.Add(n.path);

                }
            }
            //如果是一个文件节点
            else
            {
                //////////Console.WriteLine(this.sendernode.path);         // .sendernode.path
                runFileList.Add(this.sendernode.path);

            }

            new Runner().xxxxxx(new FormAndList(this.form, runFileList));


        }
    }


    //自定义的节点类
    public class iTreeNode : TreeNode
    {

        Form1 form;
        public NodeType ntype;
        public String path;

        //增加多个节点的方法
        public void AddNodes(ArrayList nodes)
        {
            foreach (iTreeNode n in nodes)
            {
                this.Nodes.Add(n);
            }
        }


        //得到孩子节点 返回一个  ArrayList
        public ArrayList getChildNodes()
        {
            ArrayList nodes = new ArrayList();
            String path = this.path;
            DirectoryInfo dirinfo = new DirectoryInfo(path);

            //遍历目录下的目录
            DirectoryInfo[] child_dirinfos = dirinfo.GetDirectories();
            foreach (DirectoryInfo di in child_dirinfos)
            {
                String p = di.FullName;
                iTreeNode n = new iTreeNode(this.form, p, NodeType.DIR);
                nodes.Add(n);
            }
            //遍历目录下的 电子表格 文件
            FileInfo[] child_fileinfos = dirinfo.GetFiles("*.xls");
            foreach (FileInfo fi in child_fileinfos)
            {
                String p = fi.FullName;
                iTreeNode n = new iTreeNode(this.form, p, NodeType.FILE);
                nodes.Add(n);
            }
            return nodes;

        }

        //增加孩子节点
        public void AddChildNodes()
        {
            //如果一个节点是文件夹节点  那么就得到这个节点的孩子节点 然后增加
            if (this.ntype == NodeType.DIR)
            {
                ArrayList childnodes = getChildNodes();
                this.AddNodes(childnodes);
            }
        }


        //给孩子节点增加孩子节点
        public void ChildNodesAddChildNodes()
        {
            this.AddChildNodes();
            //遍历这个节点的孩子节点
            foreach (iTreeNode itn in this.Nodes)
            {
                //递归调用
                itn.ChildNodesAddChildNodes();
            }
        }




        //构造函数  
        // path   文件夹或文件的全路径 
        // ntype  是文件夹 还是 文件
        public iTreeNode(Form1 form, String path, NodeType ntype)
            : base()
        {
            this.form = form;
            this.ntype = ntype;
            this.path = path;
            this.Text = getShortName(path);
            //增加右键菜单
            this.ContextMenuStrip = new iContextMenuStrip(this.form, this);
        }


        //
        public String getShortName(String path)
        {
            String[] tmp = path.Split('\\');
            int l = tmp.Length;
            return tmp[l - 1];

        }

    }


    public class ActionLines : Panel
    {
        

        public void search(String keyWord)
        {
            int l = this.Controls.Count;
            for (int i =0; i <l; i++)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));
                actionLine.search(keyWord);
               
            }
            
        
        
        }


        public int searchAndReplace(String keyWord, String replaceWord)
        {

            int l = this.Controls.Count;
            int count = 0;
            for (int i = 0; i < l; i++)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));
                if (actionLine.searchAndReplace(keyWord, replaceWord))
                {
                    count++;
                }
                

            }
            return count;
           
        }




        public void recoverySearchColor()
        {

            int l = this.Controls.Count;
            for (int i = 0; i < l; i++)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));
                actionLine.recoverySearchColor();

            }
        }


        //移除所有空白的步骤
        public void removeAllNull()
        {

            List<Int32> indexes = new List<int>();
            int l = this.Controls.Count;
            for (int i = l - 1; i > -1; i--)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));
                if (actionLine.comboBox_key.Text=="")
                {
                    indexes.Add(i);
                }
            }
            foreach (Int32 index in indexes)
            {
                removeByIndex(index);
            }

            l = this.Controls.Count;
            for (int i = 0; i < l; i++)
            {
                ((ActionLine)(this.Controls[i])).label_index.Text = (i + 1).ToString();
            }
            this.refreshActionList();
            this.setIndent();
        
        
        
        
        }

        //移除所有不运行的步骤
        public void removeAllNotRun()
        {
            List<Int32> indexes = new List<int>();
            int l = this.Controls.Count;
            for (int i = l - 1; i > -1; i--)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));
                if (actionLine.checkBox_isRun.Checked == false)
                {
                    indexes.Add(i);
                }
            }
            foreach (Int32 index in indexes)
            {
                removeByIndex(index);
            }

            l = this.Controls.Count;
            for (int i = 0; i < l; i++)
            {
                ((ActionLine)(this.Controls[i])).label_index.Text = (i + 1).ToString();
            }
            this.refreshActionList();
            this.setIndent();
        }
        private void removeByIndex(int index)
        {
            int l = this.Controls.Count;

            for (int i = index + 1; i < l; i++)
            {
                ActionLine actionLine = ((ActionLine)(this.Controls[i]));

                actionLine.Top -= 23;
            }
            this.Controls.RemoveAt(index);  
        }


        public void insertListInControls(List<ActionLine> list2, int index)
        {
            int l = this.Controls.Count;
            if (index < l)
            {
                List<ActionLine> tmp = new List<ActionLine>();

                int width = this.Controls[index].Width;
                int top = this.Controls[index].Top;




           
                for (int i = index; i < l; i++)
                {
         
                    ActionLine control = (ActionLine)(this.Controls[i]);

     
                    tmp.Add(control);
                }

                //将 从 i 到最后一个 actionLine 全部 移除  

                for (int i = 0; i < tmp.Count; i++)
                {
                    this.Controls.RemoveAt(index);
                }

                Console.WriteLine(this.Controls.Count);

                //将要增加到增加到末尾
                foreach (ActionLine control in list2)
                {
                    control.Top = top;
                    this.Controls.Add(    control );
                    top += 23;
                }

                Console.WriteLine(this.Controls.Count);


                foreach (ActionLine control in tmp)
                {

                    control.Top = top;
                    this.Controls.Add(control);
                    top += 23;


               

                    /*
                    control.Top += 23 * list2.Count;
                    this.Controls.Add(control);
                     * */

                }

                Console.WriteLine(this.Controls.Count);
                this.Height += 23 * list2.Count;

                l = this.Controls.Count;
           //     //Console.WriteLine("新的长度=" + l);

                for (int i = index; i < l; i++)
                {

                    ((ActionLine)(this.Controls[i])).label_index.Text = (i + 1).ToString();

                }
                this.refreshActionList();
                this.setIndent();

            }
            else
            {
                int width = this.Controls[index-1].Width;
                int top = this.Controls[index-1].Top+23;

                //将要增加到增加到末尾
                foreach (ActionLine control in list2)
                {
                    control.Top = top;
                    this.Controls.Add(control);
                    top += 23;
                }

                this.Height += 23 * list2.Count;

                l = this.Controls.Count;
       //         //Console.WriteLine("新的长度=" + l);

                for (int i = index; i < l; i++)
                {

                    ((ActionLine)(this.Controls[i])).label_index.Text = (i + 1).ToString();

                }
                this.refreshActionList();
                this.setIndent();

            }


                       
        }



        //设置缩进
        public void setIndent()
        {
            foreach (ActionLine actionLine in this.Controls)
            {
                actionLine.setWidth(actionLine.Width);

            }
            int indent = 0;

            foreach (ActionLine actionLine in this.Controls)
            {
                if ("{" == actionLine.comboBox_key.Text && actionLine.checkBox_isRun.Checked)
                {

                    actionLine.setIndent(indent);
                    indent++;

                }
                else if ("}" == actionLine.comboBox_key.Text && actionLine.checkBox_isRun.Checked)
                {
                    indent--;
                    actionLine.setIndent(indent);

                }
                else
                {
                    actionLine.setIndent(indent);
                }

            }
        }


        Color currentColor = Color.FromArgb(88, 117, 184);


        public List<Action> actionList;

        //转换为二维list
        public List<List<String>> toListList()
        {
            this.refreshActionList();
            List<List<String>> listList = new List<List<string>>();
            foreach (Action action in this.actionList)
            {
                List<String> list = new List<string>();
                if (action.GetType().FullName.EndsWith("CreateDriverAction"))
                {

                    list = ((CreateDriverAction)(action)).toList();

                }
                else if (action.GetType().FullName.EndsWith("CreateAppiumDriverAction"))
                {

                    list = ((CreateAppiumDriverAction)(action)).toList();

                }

                else if (action.GetType().FullName.EndsWith("CreateIOSDriverAction"))
                {

                    list = ((CreateIOSDriverAction)(action)).toList();

                }


                else if (action.GetType().FullName.EndsWith("GoAction"))
                {
                    list = ((GoAction)(action)).toList();


                }
                else if (action.GetType().FullName.EndsWith("SetParameterAction"))
                {
                    list = ((SetParameterAction)(action)).toList();

                }
                else if (action.GetType().FullName.EndsWith("ExecuteFunctionAction"))
                {

                    list = ((ExecuteFunctionAction)(action)).toList();
                }

                else if (action.GetType().FullName.EndsWith("OneStepAction"))
                {

                    list = ((OneStepAction)(action)).toList();
                }


                else if (action.GetType().FullName.EndsWith("SleepAction"))
                {

                    list = ((SleepAction)(action)).toList();
                }

                else if (action.GetType().FullName.EndsWith("ExeOtherCaseAction"))
                {
                    list = ((ExeOtherCaseAction)(action)).toList();
                }



                else if (action.GetType().FullName.EndsWith("QuitAction"))
                {
                    list = ((QuitAction)(action)).toList();

                }

                else if (action.GetType().FullName.EndsWith("AnnotationAction"))
                {
                    list = ((AnnotationAction)(action)).toList();
                }

                else if (action.GetType().FullName.EndsWith("PythonCodeAction"))
                {

                    list = ((PythonCodeAction)(action)).toList();

                }

                else if (action.GetType().FullName.EndsWith("AddIndentAction"))
                {

                    list = ((AddIndentAction)(action)).toList();

                }


                else if (action.GetType().FullName.EndsWith("RecoverIndentAction"))
                {

                    list = ((RecoverIndentAction)(action)).toList();

                }





                else if (action.GetType().FullName.EndsWith("ReadExternalConfAction"))
                {

                    list = ((ReadExternalConfAction)(action)).toList();

                }





                else if (action.GetType().FullName.EndsWith("NullAction"))
                {
                    list = ((NullAction)(action)).toList();

                }
                listList.Add(list);

            }
            return listList;
        }


        //当前被选中的序号  从0开始
        public int currentIndex;

        //最后一行的top
        public int top;
        //最后一行的 index
        public int i;


        //设置当前选中行的颜色
        public void setCurrentColor()
        {
            //this.Controls 就是一些  ActionLine ，现将所有的 ActionLine 的背景设置为白色
            foreach (Control control in this.Controls)
            {
                ((ActionLine)(control)).BackColor = Color.White;
            }


            if (currentIndex > -1)
            {
                //如果越界了  ，就到最后一行
                if (currentIndex > this.actionList.Count - 1)
                {
                    currentIndex = this.actionList.Count - 1;
                }

                ((ActionLine)(this.Controls[currentIndex])).BackColor = currentColor;


            }
            else
            {
                if (this.actionList.Count > 0)
                {
                    currentIndex = 0;


                    ((ActionLine)(this.Controls[currentIndex])).BackColor = currentColor; //Color.FromArgb(88, 117, 184);

                }


            }



        }


        //
        //   public List<Action> actionList;

        //用界面刷新list
        public void refreshActionList()
        {
            actionList.Clear();
            foreach (ActionLine actionLine in this.Controls)
            {
                actionLine.refreshAction();
                actionList.Add(actionLine.action);
            }

        }

        //设置 ActionLine 的宽度
        public void setActionLineWidth(int width)
        {
            this.Width = width;

            int height = 0;

            foreach (ActionLine actionLine in this.Controls)
            {
                actionLine.setWidth(width);
                height += actionLine.Height;

            }


        }
        /*

        public void refreshGui()
        {
            XXXX xxxx = new XXXX(refreshGuiXX);

            ThreadStart threadStart = new ThreadStart(xxxx);

            Thread thread = new Thread(threadStart);

            thread.Start();

        
        }
        */




        //用list刷新界面
        public  void refreshGui()
        {
     

            this.Visible = false;
            //Console.WriteLine("refreshGui开始的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

            DateTime d1 = DateTime.Now;

            this.Controls.Clear();
            top = 0;
            i = 0;
            foreach (Action action in actionList)
            {
                action.index = i + 1;


                //      //Console.WriteLine("new ActionLine开始的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));
                ActionLine actionLine = new ActionLine(action, this.Width);
                //       //Console.WriteLine("new ActionLine结束的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

                actionLine.Top += top;
                top += 23;
                //      //Console.WriteLine("Controls.Add开始的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));
                this.Controls.Add(actionLine);
                //       //Console.WriteLine("Controls.Add结束的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));
                i++;
            }
            this.Height = top;
            setCurrentColor();
            //Console.WriteLine("refreshGui结束的时间点" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));
            DateTime d2 = DateTime.Now;


            this.setIndent();

            Console.WriteLine("设置为可见之前" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

            

            this.Visible = true;

            this.form.hideLoading();

            Console.WriteLine("设置为可见之后" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

            

            

            
        }

        public Form1 form;


        //构造方法
        public ActionLines(List<Action> list, int width ,Form1 form)
            : base()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
           

            this.form = form;
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  

            this.Width = width;

            this.actionList = list;
            if (list.Count == 0)
            {
                this.currentIndex = -1;
            }

            refreshGui();

        }

        public void currentIndexAdd()
        {

            if (this.currentIndex < this.Controls.Count - 1)
            {
                this.currentIndex++;

            }
            this.setCurrentColor();

        }
        public void currentIndexReduce()
        {
            if (this.currentIndex > 0)
            {
                this.currentIndex--;

            }
            this.setCurrentColor();

        }



        //在某行插入一个空行
        public void insertActionLine(int index, int num)
        {
            refreshActionList();

            List<Action> list1 = this.actionList.GetRange(0, index);
            List<Action> list2 = this.actionList.GetRange(index, actionList.Count - index);

            NullAction nullAction = new NullAction(0, "", "", true);
            actionList.Clear();
            actionList.AddRange(list1);

            for (int i = 0; i < num; i++)
            {
                actionList.Add(nullAction);

            }
            actionList.AddRange(list2);
            //refreshGui();
            insertRefreshGui(index, num);
            this.setCurrentColor();
        }

        public void deleteRefreshGui(int index)
        {
            int leng = this.Controls.Count;
            for (int i2 = index + 1; i2 < leng; i2++)
            {
                ActionLine actionLine = (ActionLine)Controls[i2];
                actionLine.indexReduce(1);
            }
            this.Controls.RemoveAt(index);
            this.Height -= 23;
            this.setCurrentColor();
            this.setIndent();
        }

        public void upRefreshGui(int index)
        {
            int leng = this.Controls.Count;

            List<Control> list = new List<Control>();

            foreach (Control control in this.Controls)
            {
                list.Add(control);
            }

            ((ActionLine)list[index]).indexReduce(1);

            ((ActionLine)list[index - 1]).indexAdd(1);

            Control tmp = list[index];

            list[index] = list[index - 1];

            list[index - 1] = tmp;

            //this.Controls.Clear();


            for (int i = index - 1; i < leng; i++)
            {
                this.Controls.RemoveAt(index - 1);

            }

            for (int i = index - 1; i < leng; i++)
            {
                this.Controls.Add(list[i]);
            }
            this.setCurrentColor();
            this.setIndent();
        }


        public void downRefreshGui(int index)
        {
            int leng = this.Controls.Count;

            List<Control> list = new List<Control>();

            foreach (Control control in this.Controls)
            {
                list.Add(control);
            }

            ((ActionLine)list[index]).indexAdd(1);

            ((ActionLine)list[index + 1]).indexReduce(1);

            Control tmp = list[index];

            list[index] = list[index + 1];

            list[index + 1] = tmp;




            for (int i = index; i < leng; i++)
            {
                this.Controls.RemoveAt(index);
            }
            for (int i = index; i < leng; i++)
            {
                this.Controls.Add(list[i]);
            }

            this.setCurrentColor();
            this.setIndent();
        }






        public void insertRefreshGui(int index, int num)
        {
            int width = ((ActionLine)Controls[index]).Width;
            int top = ((ActionLine)Controls[index]).Top;


            List<ActionLine> addList = new List<ActionLine>();

            for (int i = 0; i < num; i++)
            {
                NullAction nullAction = new NullAction(index + 1 + i, "", "", true);
                ActionLine nullLine = new ActionLine(nullAction, width);

                nullLine.Top = top + i * 23;

                addList.Add(nullLine);
            }
            List<ActionLine> tmpList = new List<ActionLine>();


            int leng = this.Controls.Count;
            for (int i2 = index; i2 < leng; i2++)
            {

                ActionLine actionLine = (ActionLine)Controls[index];
                actionLine.indexAdd(num);

                tmpList.Add(actionLine);
                Controls.Remove(actionLine);

            }

            this.Height += 23 * num;


            foreach (ActionLine tmp in addList)
            {
                this.Controls.Add(tmp);

            }


            foreach (ActionLine tmp in tmpList)
            {
                this.Controls.Add(tmp);
            }

            this.setIndent();
            /*

            //Console.WriteLine(((ETabPage)this.Parent).VerticalScroll.Value);

            //Console.WriteLine(((ETabPage)this.Parent).VerticalScroll.Maximum);

            //Console.WriteLine(((ETabPage)this.Parent).Height  );
             * */





        }


        //最后一行增加一个空行
        public void addActionLineAtEnd()
        {
            NullAction nullAction = new NullAction(0, "", "", true);
            nullAction.index = i + 1;
            ActionLine nullActionLine = new ActionLine(nullAction, this.Width);
            nullActionLine.Top = top;
            top += 20;
            i++;
            this.Controls.Add(nullActionLine);
            this.Height = top;
            this.currentIndex = i - 1;
            this.setCurrentColor();

        }

        public void deleteActionLine(int index)
        {
            if( this.Controls.Count>1 )
            {
                 refreshActionList();
            this.actionList.RemoveAt(index);

            this.deleteRefreshGui(index);
            
            }
            else
            {
                MessageBox.Show("至少要有一个步骤！", "提示");
            
            }


           


            
            // refreshGui();
        }

        //某行上移
        public void up(int index)
        {
            List<Action> list = this.actionList;
            if (index > 0 && index < list.Count)
            {
                Action tmpAction = list[index];
                list[index] = list[index - 1];
                list[index - 1] = tmpAction;
                this.currentIndex = index - 1;

                this.upRefreshGui(index);

            }
            else if (index == 0)
            {
                MessageBox.Show("已经是第一个");
            }
            else
            {
                MessageBox.Show("index太大");
            }
        }

        //某行下移
        public void down(int index)
        {
            List<Action> list = this.actionList;
            if (index >= 0 && index < list.Count - 1)
            {
                Action tmpAction = list[index];
                list[index] = list[index + 1];
                list[index + 1] = tmpAction;
                this.currentIndex = index + 1;
                this.downRefreshGui(index);

            }
            else if (index == list.Count - 1)
            {
                MessageBox.Show("已经是最后一个");
            }
            else
            {
                MessageBox.Show("index太小");
            }
        }

        public void thisAndAfterNotRun(int index)
        {
            int leng = this.Controls.Count;

            for (int i = index; i < leng; i++)
            {
                ActionLine actionLine = (ActionLine)(this.Controls[i]);
                actionLine.setColorByIsRun(false);
            }
        }

        public void thisAndAfterNotRun(int index, int index2)
        {
            int leng = this.Controls.Count;

            if (index2 >= leng)
            {
                index2 = leng;
            }

            for (int i = index; i < index2; i++)
            {
                ActionLine actionLine = (ActionLine)(this.Controls[i]);
                actionLine.setColorByIsRun(false);
            }
        }




        public void thisAndAfterRecoveryRun(int index)
        {
            int leng = this.Controls.Count;

            for (int i = index; i < leng; i++)
            {
                ActionLine actionLine = (ActionLine)(this.Controls[i]);
                actionLine.setColorByIsRun(true);
            }

        }

        public void thisAndAfterRecoveryRun(int index, int index2)
        {
            int leng = this.Controls.Count;

            if (index2 >= leng)
            {
                index2 = leng;
            }

            for (int i = index; i < index2; i++)
            {
                ActionLine actionLine = (ActionLine)(this.Controls[i]);
                actionLine.setColorByIsRun(true);
            }

        }


    }


    public delegate void XXXX();




    public class EtagContextMenuStrip : ContextMenuStrip
    {
        ETabPage clickedETabPage;

        public Form1 form;

        public EtagContextMenuStrip(Form1 form, ETabPage t)
            : base()
        {
            this.form = form;

            clickedETabPage = t;

            //////////Console.WriteLine(t.Name);

            ToolStripMenuItem item_runThis = new ToolStripMenuItem("运行本用例");
            ToolStripMenuItem item_runAllOpened = new ToolStripMenuItem("运行所有已打开用例");
            ToolStripMenuItem item_closeThis = new ToolStripMenuItem("关闭本用例");
            ToolStripMenuItem item_closeOthers = new ToolStripMenuItem("关闭其他用例");
            ToolStripMenuItem item_closeAll = new ToolStripMenuItem("关闭所有用例");


            item_runThis.Click += new EventHandler(item_runThis_Click);
            item_runAllOpened.Click += new EventHandler(item_runAllOpened_Click);
            item_closeThis.Click += new EventHandler(item_closeThis_Click);
            item_closeOthers.Click += new EventHandler(item_closeOther_Click);
            item_closeAll.Click += new EventHandler(item_closeAll_Click);

            this.Items.Add(item_runThis);
            this.Items.Add(item_runAllOpened);
            this.Items.Add(item_closeThis);
            this.Items.Add(item_closeOthers);
            this.Items.Add(item_closeAll);

        }

        void item_runThis_Click(object sender, EventArgs e)
        {
            String filePath = clickedETabPage.Name;
            List<String> list = new List<String>();
            list.Add(filePath);

            new Runner().xxxxxx(new FormAndList(this.form, list));

        }

        void item_runAllOpened_Click(object sender, EventArgs e)
        {
            List<String> list = new List<String>();
            ControlCollection eTabPages = clickedETabPage.Parent.Controls;
            foreach (ETabPage eTabPage in eTabPages)
            {
                list.Add(eTabPage.Name);
            }

            new Runner().xxxxxx(new FormAndList(this.form, list));
        }

        //关闭当前的这个 page
        void item_closeThis_Click(object sender, EventArgs e)
        {
            ControlCollection eTabPages = clickedETabPage.Parent.Controls;
            foreach (ETabPage eTabPage in eTabPages)
            {
                if (eTabPage.Equals(clickedETabPage))
                {
                    ((TabControl)(clickedETabPage.Parent)).TabPages.Remove(eTabPage);
                    break;
                }
            }

        }

        //关闭除这个之外的所有page
        void item_closeOther_Click(object sender, EventArgs e)
        {
            List<ETabPage> closeList = new List<ETabPage>();

            ControlCollection eTabPages = clickedETabPage.Parent.Controls;

            foreach (ETabPage eTabPage in eTabPages)
            {
                if (!eTabPage.Equals(clickedETabPage))
                {
                    closeList.Add(eTabPage);
                }
            }
            foreach (ETabPage tmp in closeList)
            {
                ((TabControl)(clickedETabPage.Parent)).TabPages.Remove(tmp);

            }

        }

        //关闭所有用例
        void item_closeAll_Click(object sender, EventArgs e)
        {
            ControlCollection eTabPages = clickedETabPage.Parent.Controls;
            int l = eTabPages.Count;

            TabControl tabControl = (TabControl)(clickedETabPage.Parent);

            for (int i = 0; i < l; i++)
            {
                tabControl.TabPages.RemoveAt(0);

            }


        }



    }



    public class ActionLineContextMenuStrip : ContextMenuStrip
    {
        ActionLine clickedActionLine;



        void item_add_befor_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.currentIndex = clickedIndex;

            actionLines.insertActionLine(clickedIndex, 1);

        }

        void item_ImportStepsFromXls_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.currentIndex = clickedIndex;
            int width = actionLines.Controls[clickedIndex].Width;





            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"xls文件|*.xls";
            openFileDialog.ShowDialog();
            String fileName = openFileDialog.FileName;

        //    //Console.WriteLine(fileName);

            if ("" != fileName)
            {

                try
                {

                    List<Action> actionList = new ExcelProcessor().readActionListFromExcelFile(fileName);
                    List<ActionLine> actionLineList = new List<ActionLine>();


                    actionLineList.Add(new ActionLine(new NullAction(0, "", "", true), width));


                    foreach (Action action in actionList)
                    {
                        ActionLine actionLine = new ActionLine(action, width);
                        actionLineList.Add(actionLine);
                    }

                    actionLineList.Add(new ActionLine(new NullAction(0, "", "", true), width));

                    actionLines.insertListInControls(actionLineList, clickedIndex);


                }
                catch (Exception e90)
                {
                    MessageBox.Show(e90.Message);
                
                }
                
                  




                
                


          

            
            
            }


        
        
        
        }

         public void item_Paste_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            Form1 form = (Form1)(clickedActionLine.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent);


            actionLines.insertListInControls(form.actionLineListClipboard, clickedIndex);

            

        
        }
         public void item_PasteToEnd_Click(object sender, EventArgs e)
         {
             ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

             int l = actionLines.Controls.Count;

             Form1 form = (Form1)(clickedActionLine.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent);

             actionLines.insertListInControls(form.actionLineListClipboard, l);

             
         
         
         
         }


        public void item_Copy_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            ActionLine actionLine = ((ActionLine)(actionLines.Controls[clickedIndex]));
            
            Form1 form = (Form1)(clickedActionLine.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent);

            form.actionLineListClipboard.Clear();

            form.actionLineListClipboard.Add(actionLine.deepCopy());
             
        }

        public void item_QuicklyCopyALine_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            ActionLine actionLine = ((ActionLine)(actionLines.Controls[clickedIndex]));

            List<ActionLine> tmpList = new List<ActionLine>();

            tmpList.Add(actionLine.deepCopy());


            actionLines.insertListInControls(tmpList, clickedIndex+1);




        
        }


        public void item_CopyMul_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;



            String stepNum = Interaction.InputBox("复制从当前步骤至第几个步骤", "请输入一个数字");
            int step2 = 0;
            try
            {
                step2 = Int32.Parse(stepNum);

                if (step2 > actionLines.Controls.Count)
                {
                    step2 = actionLines.Controls.Count;

                }
                int toIndex = step2 - 1;

                Form1 form = (Form1)(clickedActionLine.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent);
                form.actionLineListClipboard.Clear();


                if (toIndex >= clickedIndex)
                {
                    

                    for (int i = clickedIndex; i <= toIndex; i++)
                    {
                        ActionLine actionLine = ((ActionLine)(actionLines.Controls[i]));
                        form.actionLineListClipboard.Add(actionLine.deepCopy());

                    }
                
                }




            }
            catch (Exception e10)
            {
            }
            




        
        
        }




        void item_AddStepsOfRunByXls_Click(object sender, EventArgs e)
        {

            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.currentIndex = clickedIndex;


            List<ActionLine> list = new List<ActionLine>();

            int width = actionLines.Controls[clickedIndex].Width;



            list.Add(new ActionLine(new OneStepAction(0, "读取多行数据到序列", "电子表格文件的路径||多行测试数据", true), width));


            list.Add(new ActionLine(new PythonCodeAction(0, "allPass=True", true), width));
            list.Add(new ActionLine(new PythonCodeAction(0, "for data in self.getPar('多行测试数据'):", true), width));
            list.Add(new ActionLine(new AddIndentAction(0, true), width));
            list.Add(new ActionLine(new PythonCodeAction(0, "self.addDic(data)", true), width));
            list.Add(new ActionLine(new AnnotationAction(0, "请在这里增加循环执行的内容", true), width));
            list.Add(new ActionLine(new PythonCodeAction(0, "allPass=allPass and self.getPar('LASTSTEP')", true), width));
            list.Add(new ActionLine(new PythonCodeAction(0, "self.setPar('LASTSTEP',True)", true), width));
            list.Add(new ActionLine(new RecoverIndentAction(0, true), width));
            list.Add(new ActionLine(new PythonCodeAction(0, "self.setPar('LASTSTEP',allPass)", true), width));




            actionLines.insertListInControls(list, clickedIndex);





        }



        void item_add5_befor_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.currentIndex = clickedIndex;
            actionLines.insertActionLine(clickedIndex, 5);
        }




        void item_delete_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

         
            actionLines.currentIndex = clickedIndex;
            actionLines.deleteActionLine(clickedIndex);

            



      
        }

        void item_up_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.up(clickedIndex);
        }
        void item_down_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.down(clickedIndex);
        }


        void item_ThisAndAfterNotRun_Click(object sender, EventArgs e)
        {
            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.thisAndAfterNotRun(clickedIndex);



        }

        void item_ThisAndAfterRecoveryRun_Click(object sender, EventArgs e)
        {

            int clickedIndex = clickedActionLine.action.index - 1;
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;
            actionLines.thisAndAfterRecoveryRun(clickedIndex);
        }


        void item_ThisToDesignatedNotRun_Click(object sender, EventArgs e)
        {


            String stepNum = Interaction.InputBox("从当前步骤至第几个步骤被禁用", "请输入一个数字");
            int step2 = 0;
            try
            {
                step2 = Int32.Parse(stepNum);

            }
            catch (Exception e10)
            {
            }


            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            actionLines.thisAndAfterNotRun(clickedIndex, step2);

        }
        void item_ThisToDesignatedRecoveryRun_Click(object sender, EventArgs e)
        {

            String stepNum = Interaction.InputBox("从当前步骤至第几个步骤被恢复", "请输入一个数字");
            int step2 = 0;
            try
            {
                step2 = Int32.Parse(stepNum);

            }
            catch (Exception e10)
            {
            }


            int clickedIndex = clickedActionLine.action.index - 1;

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            actionLines.thisAndAfterRecoveryRun(clickedIndex, step2);

        }

        //移除所有不跑的行
        void item_RemoveAllNotRun_Click(object sender, EventArgs e)
        {
            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            DialogResult dr = MessageBox.Show("删除后不可恢复，确定？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                actionLines.removeAllNotRun();
            }
        }

        //移除所有空白到行
        void item_RemoveAllNull_Click(object sender, EventArgs e)
        {

            ActionLines actionLines = (ActionLines)clickedActionLine.Parent;

            actionLines.removeAllNull();
        
        }




        public ActionLineContextMenuStrip(ActionLine actionLine)
            : base()
        {

            ////Console.WriteLine("ActionLineContextMenuStrip构造方法开始" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

            clickedActionLine = actionLine;

            ToolStripMenuItem item_Copy = new ToolStripMenuItem("复制");

            ToolStripMenuItem item_CopyMul = new ToolStripMenuItem("复制多行");


            ToolStripMenuItem item_Paste = new ToolStripMenuItem("粘贴(插入到这里)");
            ToolStripMenuItem item_PasteToEnd = new ToolStripMenuItem("粘贴(加到末尾)");

            ToolStripMenuItem item_QuicklyCopyALine = new ToolStripMenuItem("快速复制一行");
            

            


            ToolStripMenuItem item_add_befor = new ToolStripMenuItem("插入一个步骤 Ctrl+I");
            ToolStripMenuItem item_add5_befor = new ToolStripMenuItem("插入五个步骤");

            ToolStripMenuItem item_delete = new ToolStripMenuItem("删除这个步骤 Ctrl+D");
            ToolStripMenuItem item_up = new ToolStripMenuItem("上移             Ctrl+上");
            ToolStripMenuItem item_down = new ToolStripMenuItem("下移             Ctrl+下");

            ToolStripMenuItem item_ThisAndAfterNotRun = new ToolStripMenuItem("禁用以下所有步骤");

            ToolStripMenuItem item_ThisAndAfterRecoveryRun = new ToolStripMenuItem("恢复以下所有步骤");


            ToolStripMenuItem item_ThisToDesignatedNotRun = new ToolStripMenuItem("禁用多个步骤");

            ToolStripMenuItem item_ThisToDesignatedRecoveryRun = new ToolStripMenuItem("恢复多个步骤");


            ToolStripMenuItem item_RemoveAllNotRun = new ToolStripMenuItem("删除所有禁用的步骤");

            ToolStripMenuItem item_RemoveAllNull = new ToolStripMenuItem("删除所有空的步骤");



            ToolStripMenuItem item_AddStepsOfRunByXls = new ToolStripMenuItem("增加根据数据表执行的前后python步骤");

            ToolStripMenuItem item_ImportStepsFromXls = new ToolStripMenuItem("导入步骤");









            item_add_befor.Click += new EventHandler(item_add_befor_Click);
            item_add5_befor.Click += new EventHandler(item_add5_befor_Click);
            item_delete.Click += new EventHandler(item_delete_Click);
            item_up.Click += new EventHandler(item_up_Click);
            item_down.Click += new EventHandler(item_down_Click);

            item_ThisAndAfterNotRun.Click += new EventHandler(item_ThisAndAfterNotRun_Click);
            item_ThisAndAfterRecoveryRun.Click += new EventHandler(item_ThisAndAfterRecoveryRun_Click);

            item_ThisToDesignatedNotRun.Click += new EventHandler(item_ThisToDesignatedNotRun_Click);
            item_ThisToDesignatedRecoveryRun.Click += new EventHandler(item_ThisToDesignatedRecoveryRun_Click);

            item_RemoveAllNotRun.Click += new EventHandler(item_RemoveAllNotRun_Click);
            item_RemoveAllNull.Click+=new EventHandler(item_RemoveAllNull_Click);

            item_AddStepsOfRunByXls.Click += new EventHandler(item_AddStepsOfRunByXls_Click);

            item_Copy.Click+=new EventHandler(item_Copy_Click);
            item_CopyMul.Click+=new EventHandler(item_CopyMul_Click);
            item_Paste.Click+=new EventHandler(item_Paste_Click);
            item_PasteToEnd.Click+=new EventHandler(item_PasteToEnd_Click);

            item_QuicklyCopyALine.Click+=new EventHandler(item_QuicklyCopyALine_Click);

            item_ImportStepsFromXls.Click+=new EventHandler(item_ImportStepsFromXls_Click);




            //复制和粘贴
            this.Items.Add(item_Copy);
            this.Items.Add(item_CopyMul);
            this.Items.Add(item_Paste);
            this.Items.Add(item_PasteToEnd);
            this.Items.Add(item_QuicklyCopyALine);


            



            //插入和删除
            this.Items.Add("    ");
            this.Items.Add(item_add_befor);
            this.Items.Add(item_add5_befor);
            this.Items.Add(item_delete);


            //上移 下移
            this.Items.Add("    ");
            this.Items.Add(item_up);
            this.Items.Add(item_down);


            //禁用和恢复
            this.Items.Add("    ");
            this.Items.Add(item_ThisToDesignatedNotRun);
            this.Items.Add(item_ThisToDesignatedRecoveryRun);
            this.Items.Add(item_ThisAndAfterNotRun);
            this.Items.Add(item_ThisAndAfterRecoveryRun);
            this.Items.Add("    ");
            this.Items.Add(item_RemoveAllNotRun);
            this.Items.Add(item_RemoveAllNull);



            //快捷增加代码块
            this.Items.Add("    ");
            this.Items.Add(item_AddStepsOfRunByXls);
            this.Items.Add(item_ImportStepsFromXls);



            




            ////Console.WriteLine("ActionLineContextMenuStrip构造方法结束" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

        }


    }

    class KeyColor
    {
        public static Color CreateDriverActionBackColor = Color.FromArgb(128, 128, 255);
        public static Color CreateDriverActionFontColor = Color.Black;


        public static Color GoActionBackColor = Color.FromArgb(255, 0, 255);
        public static Color GoActionFontColor = Color.Black;


        public static Color SetParameterActionBackColor = Color.FromArgb(255, 128, 64);
        public static Color SetParameterActionFontColor = Color.Black;


        public static Color ExecuteFunctionActionBackColor = Color.FromArgb(181, 230, 29);
        public static Color ExecuteFunctionActionFontColor = Color.Black;

        public static Color SleepActionBackColor = Color.FromArgb(255, 0, 0);
        public static Color SleepActionFontColor = Color.Black;

        public static Color QuitActionBackColor = Color.FromArgb(0, 128, 192);
        public static Color QuitActionFontColor = Color.Black;


        public static Color NullActionBackColor = Color.White;
        public static Color NullActionFontColor = Color.Black;


        public static Color OneStepActionBackColor = Color.FromArgb(0, 128, 0);
        public static Color OneStepActionFontColor = Color.White;

        public static Color ExeOtherCaseActionBackColor = Color.FromArgb(255, 200, 200);
        public static Color ExeOtherCaseActionFontColor = Color.Black;


        public static Color PythonCodeActionBackColor = Color.Yellow;
        public static Color PythonCodeActionFontColor = Color.Black;


        public static Color AnnotationActionBackColor = Color.Brown;
        public static Color AnnotationActionFontColor = Color.White;



        public static Color ReadExternalConfBackAction = Color.Aqua;
        public static Color ReadExternalConfFontAction = Color.Black;




    }

   
    public class ActionLine : Panel
    {
        List<OneStepMenuAndPrompt> oneStepMenuAndPrompts;


        public void search(String keyWord)
        {
            if (this.control_par2.Text.Contains(keyWord))
            {
                this.control_par2.BackColor = Color.Yellow;
            }
        }

        public bool searchAndReplace(String keyWord ,String replaceWord )
        {
            if (this.control_par2.Text.Contains(keyWord))
            {
                this.control_par2.BackColor = Color.Yellow;

                this.control_par2.Text = this.control_par2.Text.Replace(keyWord, replaceWord);
                return true;

            }
            else
            {
                return false;
            }
        }



        public void recoverySearchColor()
        {
            this.control_par2.BackColor = Color.White;

            this.setColorByIsRun( this.checkBox_isRun.Checked );
        
        }


        public ActionLine deepCopy()
        {
            this.refreshAction();
            Action action = this.action;
            int width = this.Width;
            return new ActionLine(action, width);
        }
        

        Dictionary<String, String> oneStepNameAndPrompt = new Dictionary<string, string>();



        public void refreshAction()
        {
            int index = Int32.Parse(this.label_index.Text);
            String key = this.comboBox_key.Text;
            String par1 = this.control_par1.Text;

            String par2 = this.control_par2.Text;
            bool isRun = this.checkBox_isRun.Checked;

            if ("新建驱动" == key)
            {
                this.action = new CreateDriverAction(index, par1, par2, isRun);
            }

            else if ("新建Appium驱动" == key)
            {
                this.action = new CreateAppiumDriverAction(index, par1, par2, isRun);
            }
            else if ("新建IOS驱动" == key)
            {
                this.action = new CreateIOSDriverAction(index, par1, par2, isRun);
            }

            else if ("打开网址" == key)
            {
                this.action = new GoAction(index, par1, par2, isRun);
            }
            else if ("设置参数" == key)
            {
                this.action = new SetParameterAction(index, par1, par2, isRun);
            }
            else if ("执行方法" == key)
            {
                this.action = new ExecuteFunctionAction(index, par1, par2, isRun);

            }
            else if ("单步方法" == key)
            {
                this.action = new OneStepAction(index, par1, par2, isRun);

            }

            else if ("暂停" == key)
            {
                this.action = new SleepAction(index, Int32.Parse(par1), isRun);
            }
            else if ("调用其他用例" == key)
            {
                this.action = new ExeOtherCaseAction(index, par2, isRun);


            }


            else if ("退出" == key)
            {
                this.action = new QuitAction(index, par1, isRun);
            }

            else if ("注释" == key)
            {
                this.action = new AnnotationAction(index, par2, isRun);
            }

            else if ("Python代码" == key)
            {

                this.action = new PythonCodeAction(index, par2, isRun);

            }
            else if ("{" == key)
            {

                this.action = new AddIndentAction(index, isRun);

            }
            else if ("}" == key)
            {

                this.action = new RecoverIndentAction(index, isRun);

            }

            else if ("读取外部配置" == key)
            {

                this.action = new ReadExternalConfAction(index, par2, isRun);

            }
            else if ("" == key)
            {
                this.action = new NullAction(index, par1, par2, true);
            
            }





        }

        public void setIndent(int indent)
        {
            int oneIndentWidth = 32;

            int indentWidth = indent * oneIndentWidth;

            this.comboBox_key.Left += indentWidth;
            this.control_par1.Left += indentWidth;
            this.control_par2.Left += indentWidth;
            this.control_par2.Width -= indent * oneIndentWidth;
        }


        //将 执行的变成不可以执行  将不可以执行的变成可执行
        public void setColorByIsRunToOther()
        {
            if (this.checkBox_isRun.Checked)
            {
                setColorByIsRun(false);

            }
            else
            {
                setColorByIsRun(true);
            }
        
        }

        public void setColorByIsRun(bool isRun)
        {

            if (isRun)
            {
                this.checkBox_isRun.Checked = true;

                control_par1.BackColor = Color.White;
                control_par2.BackColor = Color.White;

            }
            else
            {

                this.checkBox_isRun.Checked = false;

                control_par1.BackColor = Color.Gray;



                control_par2.BackColor = Color.Gray;


            }

        }

        public void checkBox_isRun_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            setColorByIsRun(checkBox.Checked);



        }

        //被点击
        public void ActionLine_Click(object sender, EventArgs e)
        {
            ((ActionLines)(this.Parent)).currentIndex = this.action.index - 1;

            ((ActionLines)(this.Parent)).setCurrentColor();


        }

        public Action action;
        public Label label_index;
        public ComboBox comboBox_key;
        public Control control_par1;
        public Control control_par2;
        public CheckBox checkBox_isRun;

        public void indexAdd(int x)
        {
            for (int i = 0; i < x; i++)
            {
                this.Top += 23;
            }
            this.label_index.Text = (Int32.Parse(this.label_index.Text) + x).ToString();
            this.action.index += x;
        }
        public void indexReduce(int x)
        {
            for (int i = 0; i < x; i++)
            {
                this.Top -= 23;
            }
            this.label_index.Text = (Int32.Parse(this.label_index.Text) - x).ToString();
            this.action.index -= x;

        }


        public void setWidth(int width)
        {
            this.Width = width;
            int width_label_index = 30;
            int width_comboBox_key = (width - 50) * 10 / 100;
            int width_textBox_par1 = (width - 50) * 15 / 100;
            int width_control_par2 = (width - 50) * 75 / 100;
            int width_checkBox_isRun = 20;


            label_index.Left = 0;
            label_index.Top = 1;
            label_index.Width = width_label_index;

            comboBox_key.Left = label_index.Left + label_index.Width;
            comboBox_key.Top = 1;
            comboBox_key.Width = width_comboBox_key;

            control_par1.Left = comboBox_key.Left + comboBox_key.Width;
            control_par1.Top = 1;
            control_par1.Width = width_textBox_par1;



            control_par2.Left = control_par1.Left + control_par1.Width;
            control_par2.Top = 1;
            control_par2.Width = width_control_par2;

            checkBox_isRun.Left = control_par2.Left + control_par2.Width;
            checkBox_isRun.Top = 1;
            checkBox_isRun.Width = width_checkBox_isRun;


        }





        private void comboBox_oneStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            String oneStepName = ((ComboBox)control_par1).Text;

      //      //Console.WriteLine(oneStepName);

            control_par2.Text = this.oneStepNameAndPrompt[oneStepName];


        }


        private void comboBox_key_SelectedIndexChanged(object sender, EventArgs e)
        {

            //////Console.WriteLine("更改key");




            Point lacation1 = control_par1.Location;

            Size size1 = control_par1.Size;

            Color backColor1 = control_par1.BackColor;


            Point lacation2 = control_par2.Location;

            Size size2 = control_par2.Size;

            Color backColor2 = control_par2.BackColor;


            String key = comboBox_key.Text;

            this.Controls.Remove(control_par1);

            this.Controls.Remove(control_par2);

            if ("执行方法" == key)
            {

                this.control_par1 = new TextBox();
                this.setPar2Items();

                this.control_par1.Enabled = false;
                this.control_par2.Enabled = true;

                ((ComboBox)sender).BackColor = KeyColor.ExecuteFunctionActionBackColor;
                ((ComboBox)sender).ForeColor = KeyColor.ExecuteFunctionActionFontColor;

            }
            else if ("单步方法" == key)
            {
                this.setPar1Items();
                this.control_par2 = new TextBox();

                ((ComboBox)control_par1).SelectedIndexChanged += comboBox_oneStep_SelectedIndexChanged;

                this.control_par1.Enabled = true;
                this.control_par2.Enabled = true;


                ((ComboBox)sender).BackColor = KeyColor.OneStepActionBackColor;
                ((ComboBox)sender).ForeColor = KeyColor.OneStepActionFontColor;

            }



            else
            {
                this.control_par1 = new TextBox();
                this.control_par2 = new TextBox();
                if ("新建驱动" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.CreateDriverActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.CreateDriverActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par2.Enabled = false;

                }
                else if ("打开网址" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.GoActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.GoActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;
                }

                else if ("暂停" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.SleepActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.SleepActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par1.Text = "1";
                    this.control_par2.Enabled = false;
                }
                else if ("设置参数" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.SetParameterActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.SetParameterActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par2.Enabled = true;
                }

                else if ("调用其他用例" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.ExeOtherCaseActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.ExeOtherCaseActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }

                else if ("退出" == key)
                {
                    ((ComboBox)sender).BackColor = KeyColor.QuitActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.QuitActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }

                else if ("注释" == key)
                {
                    this.control_par2 = new AnnotationTextBox();
                    ((ComboBox)sender).BackColor = KeyColor.AnnotationActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.AnnotationActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }
                else if ("Python代码" == key)
                {
                    this.control_par2 = new PythonTextBox();

                    ((ComboBox)sender).BackColor = KeyColor.PythonCodeActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }
                else if ("{" == key)
                {

                    ((ComboBox)sender).BackColor = KeyColor.PythonCodeActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }
                else if ("}" == key)
                {

                    ((ComboBox)sender).BackColor = KeyColor.PythonCodeActionBackColor;
                    ((ComboBox)sender).ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }


                else if ("读取外部配置" == key)
                {

                    ((ComboBox)sender).BackColor = KeyColor.ReadExternalConfBackAction;
                    ((ComboBox)sender).ForeColor = KeyColor.ReadExternalConfFontAction;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }



            }

            control_par1.Location = lacation1;

            control_par1.Size = size1;

            control_par1.BackColor = backColor1;




            control_par2.Location = lacation2;

            control_par2.Size = size2;

            control_par2.BackColor = backColor2;

            this.Controls.Add(control_par1);
            this.Controls.Add(control_par2);

            Console.WriteLine(this.Parent.Controls.Count);

            this.refreshAction();

           



        }
        public void setPar1Items()
        {
            this.control_par1 = new MyComboBox();
            ((ComboBox)control_par1).DropDownStyle = ComboBoxStyle.DropDownList;
            ((ComboBox)control_par1).FlatStyle = FlatStyle.Flat;

            foreach (OneStepMenuAndPrompt oneStepMenuAndPrompt in oneStepMenuAndPrompts)
            {
                ((ComboBox)control_par1).Items.Add(oneStepMenuAndPrompt.menuText);

            }
            /*

            ((ComboBox)control_par1).Items.Add("点击");
            ((ComboBox)control_par1).Items.Add("选择单选按钮或复选框");


            ((ComboBox)control_par1).Items.Add("悬停");



            ((ComboBox)control_par1).Items.Add("双击");

            ((ComboBox)control_par1).Items.Add("右键");

            ((ComboBox)control_par1).Items.Add("拖拽");




            ((ComboBox)control_par1).Items.Add("输入");


            ((ComboBox)control_par1).Items.Add("选择by文本");


            ((ComboBox)control_par1).Items.Add("选择by值");


            ((ComboBox)control_par1).Items.Add("选择by序号");


            ((ComboBox)control_par1).Items.Add("验证是否存在");
            ((ComboBox)control_par1).Items.Add("得到是否存在");


            ((ComboBox)control_par1).Items.Add("得到文本");


            ((ComboBox)control_par1).Items.Add("验证文本");


            ((ComboBox)control_par1).Items.Add("得到值");


            ((ComboBox)control_par1).Items.Add("验证值");


            ((ComboBox)control_par1).Items.Add("验证是否选中");

            ((ComboBox)control_par1).Items.Add("得到选中状态");


            ((ComboBox)control_par1).Items.Add("得到属性");


            ((ComboBox)control_par1).Items.Add("验证属性");


            ((ComboBox)control_par1).Items.Add("移除属性");


            ((ComboBox)control_par1).Items.Add("修改属性");


            ((ComboBox)control_par1).Items.Add("强制可输入");

            ((ComboBox)control_par1).Items.Add("chrome上传单个文件");


            ((ComboBox)control_par1).Items.Add("点击js弹出框的确定");
            ((ComboBox)control_par1).Items.Add("点击js弹出框的取消");


            ((ComboBox)control_par1).Items.Add("进入iframe");


            ((ComboBox)control_par1).Items.Add("退出iframe");


            ((ComboBox)control_par1).Items.Add("切换窗口");
            ((ComboBox)control_par1).Items.Add("只保留第一窗口");


            ((ComboBox)control_par1).Items.Add("回退窗口");


            ((ComboBox)control_par1).Items.Add("前进窗口");


            ((ComboBox)control_par1).Items.Add("刷新窗口");


            ((ComboBox)control_par1).Items.Add("执行js");

            ((ComboBox)control_par1).Items.Add("得到相对日期");

            ((ComboBox)control_par1).Items.Add("得到相对时间");

            ((ComboBox)control_par1).Items.Add("发送钉钉消息");

            ((ComboBox)control_par1).Items.Add("调试输出");

            ((ComboBox)control_par1).Items.Add("得到子字符串");

            ((ComboBox)control_par1).Items.Add("替换字符串");

            ((ComboBox)control_par1).Items.Add("去除字符串");

            ((ComboBox)control_par1).Items.Add("分割后取第几个");

            ((ComboBox)control_par1).Items.Add("mysql查询");
            ((ComboBox)control_par1).Items.Add("mysql执行");

            ((ComboBox)control_par1).Items.Add("键盘单键输入");


            ((ComboBox)control_par1).Items.Add("鼠标点击坐标");

            ((ComboBox)control_par1).Items.Add("鼠标滚轮");

            ((ComboBox)control_par1).Items.Add("鼠标拖拽");

            ((ComboBox)control_par1).Items.Add("屏幕找图并点击");

            ((ComboBox)control_par1).Items.Add("屏幕找图得到坐标");

            ((ComboBox)control_par1).Items.Add("屏幕区域截图并保存");

            ((ComboBox)control_par1).Items.Add("识别图片中的字母和数字");






            //  ((ComboBox)control_par1).Items.Add("最小化浏览器");

            ((ComboBox)control_par1).Items.Add("最大化浏览器");

            ((ComboBox)control_par1).Items.Add("设置浏览器位置和大小");

            ((ComboBox)control_par1).Items.Add("执行命令");

            ((ComboBox)control_par1).Items.Add("读取多行数据到序列");

            */



        }

        public void setPar2Items()
        {
            String prPath = new TextProcessor().getAbsPath();

            this.control_par2 = new MyComboBox();

            ((ComboBox)control_par2).DropDownStyle = ComboBoxStyle.DropDownList;
            ((ComboBox)control_par2).FlatStyle = FlatStyle.Flat;

            // List<String> funList = new TextProcessor().getFuns(prPath + "\\conf\\function.txt");

            // List<String> funList = 


            Dictionary<String, String> functionDic = new TextProcessor().createFunctionDic(prPath + "\\PythonSelenium2\\src\\testPage");

            foreach (KeyValuePair<String, String> kv in functionDic)
            {
                String fun = kv.Key;
                ((ComboBox)control_par2).Items.Add(fun);
            }

            /*
            foreach (String fun in funList)
            {
                ((ComboBox)control_par2).Items.Add(fun);

            }
             * */


        }






        //构造方法
        public ActionLine(Action action, int width)
            : base()
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            this.DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  
           

            //Console.WriteLine("初始化ActionLine开始" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));


            TextProcessor tp = new TextProcessor();
            String prPath = tp.getAbsPath();
            String oneStepConfFile = prPath + "\\conf\\onestep.txt";

            oneStepMenuAndPrompts = tp.getOneStepMenuAndPromptList(oneStepConfFile);

            

            
            foreach (OneStepMenuAndPrompt oneStepMenuAndPrompt in oneStepMenuAndPrompts)
            {

                this.oneStepNameAndPrompt.Add(oneStepMenuAndPrompt.menuText, oneStepMenuAndPrompt.promptText);
            
            }

            //Console.WriteLine("初始化ActionLine结束" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));


            /*

            this.oneStepNameAndPrompt.Add("点击", "id或xpath||控件名称");

            this.oneStepNameAndPrompt.Add("选择单选按钮或复选框", "id或xpath||控件名称||是或否");



            this.oneStepNameAndPrompt.Add("悬停", "id或xpath||控件名称");

            this.oneStepNameAndPrompt.Add("双击", "id或xpath||控件名称");
            this.oneStepNameAndPrompt.Add("右键", "id或xpath||控件名称");
            this.oneStepNameAndPrompt.Add("拖拽", "拖拽开始的id或xpath||拖拽开始的控件名称||拖拽结束的id或xpath||拖拽结束的控件名称");


            this.oneStepNameAndPrompt.Add("输入", "id或xpath||控件名称||输入的值");
            this.oneStepNameAndPrompt.Add("选择by文本", "id或xpath||控件名称||选择的文本");
            this.oneStepNameAndPrompt.Add("选择by值", "id或xpath||控件名称||选择的值");
            this.oneStepNameAndPrompt.Add("选择by序号", "id或xpath||控件名称||选择的序号");
            this.oneStepNameAndPrompt.Add("验证是否存在", "id或xpath||控件名称||是或否||等待秒数");
            this.oneStepNameAndPrompt.Add("得到是否存在", "id或xpath||控件名称||存放的变量");
            this.oneStepNameAndPrompt.Add("得到文本", "id或xpath||控件名称||存放的变量");
            this.oneStepNameAndPrompt.Add("验证文本", "id或xpath||控件名称||期望的文本");
            this.oneStepNameAndPrompt.Add("得到值", "id或xpath||控件名称||存放的变量");
            this.oneStepNameAndPrompt.Add("验证值", "id或xpath||控件名称||期望的值");
            this.oneStepNameAndPrompt.Add("验证是否选中", "id或xpath||控件名称||是或否");
            this.oneStepNameAndPrompt.Add("得到选中状态", "id或xpath||控件名称||存放的变量");
            this.oneStepNameAndPrompt.Add("得到属性", "id或xpath||控件名称||属性名||存放的变量");
            this.oneStepNameAndPrompt.Add("验证属性", "id或xpath||控件名称||属性名||期望的属性值");
            this.oneStepNameAndPrompt.Add("移除属性", "id或xpath||控件名称||属性名");
            this.oneStepNameAndPrompt.Add("修改属性", "id或xpath||控件名称||属性名||新的属性值");
            this.oneStepNameAndPrompt.Add("强制可输入", "id或xpath||控件名称");
            this.oneStepNameAndPrompt.Add("chrome上传单个文件", "文件的路径（使用正斜杠或双反斜杠）");
            this.oneStepNameAndPrompt.Add("点击js弹出框的确定", "");
            this.oneStepNameAndPrompt.Add("点击js弹出框的取消", "");
            this.oneStepNameAndPrompt.Add("进入iframe", "iframe的id或xpath||iframe名称");
            this.oneStepNameAndPrompt.Add("退出iframe", "");
            this.oneStepNameAndPrompt.Add("切换窗口", "窗口的序号(从0开始)");
            this.oneStepNameAndPrompt.Add("只保留第一窗口", "");
            this.oneStepNameAndPrompt.Add("回退窗口", "");
            this.oneStepNameAndPrompt.Add("前进窗口", "");
            this.oneStepNameAndPrompt.Add("刷新窗口", "");
            this.oneStepNameAndPrompt.Add("执行js", "js语句||id或xpath||控件名称");
            this.oneStepNameAndPrompt.Add("发送钉钉消息", "钉钉登录账号||钉钉登录密码||接收人1,接收人2||发送内容");
            this.oneStepNameAndPrompt.Add("得到相对日期", "相对于今天的天数(负数表示前，正数表示后，0表示当天)||存放的变量");
            this.oneStepNameAndPrompt.Add("得到相对时间", "单位(\"秒\"或\"分\"或\"小时\"或\"天\"或\"周\")||相对于当前的值(负数表示前，正数表示后，0表示现在)||存放的变量");

            this.oneStepNameAndPrompt.Add("调试输出", "输出的字符串");

            this.oneStepNameAndPrompt.Add("得到子字符串", "来源字符串||开始||结束(不写表示到末尾)||存放的变量");

            this.oneStepNameAndPrompt.Add("替换字符串", "来源字符串||旧的||新的||存放的变量");

            this.oneStepNameAndPrompt.Add("去除字符串", "来源字符串||要去除的字符串||存放的变量");

            this.oneStepNameAndPrompt.Add("分割后取第几个", "来源字符串||分隔符||第几个(从0开始)||存放的变量");

            this.oneStepNameAndPrompt.Add("mysql查询", "mysqlIP||mysql端口||数据库名||用户名||密码||sql语句||存放的变量");

            this.oneStepNameAndPrompt.Add("mysql执行", "mysqlIP||mysql端口||数据库名||用户名||密码||sql语句");
            this.oneStepNameAndPrompt.Add("键盘单键输入", "键");

            this.oneStepNameAndPrompt.Add("鼠标点击坐标", "x||y");

            this.oneStepNameAndPrompt.Add("鼠标滚轮", "滚动值(正数为向下滚动，负数为向上滚动)");

            this.oneStepNameAndPrompt.Add("鼠标拖拽", "x1,y1,y2,y2,x3,y3,xn,yn");


            this.oneStepNameAndPrompt.Add("屏幕找图并点击", "图片的路径（使用正斜杠或双反斜杠）");

            this.oneStepNameAndPrompt.Add("屏幕找图得到坐标", "图片的路径（使用正斜杠或双反斜杠）||左上角x||左上角y||右下角x||右下角y");
            this.oneStepNameAndPrompt.Add("屏幕区域截图并保存", "左上角x||左上角y||右下角x||右下角y||保存图片的路径（使用正斜杠或双反斜杠）");
            this.oneStepNameAndPrompt.Add("识别图片中的字母和数字", "图片的路径（使用正斜杠或双反斜杠）||识别出的文本存放的变量");





            this.oneStepNameAndPrompt.Add("最小化浏览器", "");
            this.oneStepNameAndPrompt.Add("最大化浏览器", "");
            this.oneStepNameAndPrompt.Add("设置浏览器位置和大小", "左||顶||宽度||高度");

            this.oneStepNameAndPrompt.Add("执行命令", "命令||命令输出文本存放的变量");


            this.oneStepNameAndPrompt.Add("读取多行数据到序列", "电子表格路径||序列存放的变量");
             * 
             * 
             * */














            this.action = action;

            int index = action.index;
            String key = "";
            String par1 = "";
            String par2 = "";
            bool isRun = action.isRun;
            this.BackColor = Color.White;

            this.Click += new EventHandler(ActionLine_Click);
            this.ContextMenuStrip = new ActionLineContextMenuStrip(this);

            if (action.GetType().FullName.EndsWith("CreateDriverAction"))
            {

                key = "新建驱动";
                par1 = ((CreateDriverAction)(action)).browserName;
                par2 = ((CreateDriverAction)(action)).driverName;
            }
            else if (action.GetType().FullName.EndsWith("CreateAppiumDriverAction"))
            {

                key = "新建Appium驱动";
                par1 = ((CreateAppiumDriverAction)(action)).deviceName;
                par2 = ((CreateAppiumDriverAction)(action)).appPackageAndAppActivity;
            }

            else if (action.GetType().FullName.EndsWith("CreateIOSDriverAction"))
            {

                key = "新建IOS驱动";
                par1 = ((CreateIOSDriverAction)(action)).macIP;
                par2 = ((CreateIOSDriverAction)(action)).appPath;
            }



            else if (action.GetType().FullName.EndsWith("GoAction"))
            {
                key = "打开网址";

                par1 = ((GoAction)(action)).driverName;
                par2 = ((GoAction)(action)).url;
            }
            else if (action.GetType().FullName.EndsWith("SetParameterAction"))
            {
                key = "设置参数";

                par1 = ((SetParameterAction)(action)).parName;
                par2 = ((SetParameterAction)(action)).parValue;
            }
            else if (action.GetType().FullName.EndsWith("ExecuteFunctionAction"))
            {
                key = "执行方法";

                par1 = ((ExecuteFunctionAction)(action)).driverName;
                par2 = ((ExecuteFunctionAction)(action)).functionName;
            }

            else if (action.GetType().FullName.EndsWith("OneStepAction"))
            {
                key = "单步方法";

                par1 = ((OneStepAction)(action)).oneStepName;
                par2 = ((OneStepAction)(action)).oneStepPars;
            }





            else if (action.GetType().FullName.EndsWith("SleepAction"))
            {
                key = "暂停";

                par1 = ((SleepAction)(action)).timeLong.ToString();
                par2 = "";
            }

            else if (action.GetType().FullName.EndsWith("ExeOtherCaseAction"))
            {
                key = "调用其他用例";
                par1 = "";
                par2 = ((ExeOtherCaseAction)(action)).casePath; ;


            }



            else if (action.GetType().FullName.EndsWith("QuitAction"))
            {
                key = "退出";

                par1 = ((QuitAction)(action)).driverName;
                par2 = "";
            }

            else if (action.GetType().FullName.EndsWith("AnnotationAction"))
            {
                key = "注释";

                par1 = "";
                par2 = ((AnnotationAction)(action)).annotationText;
            }

            else if (action.GetType().FullName.EndsWith("PythonCodeAction"))
            {
                key = "Python代码";
                par1 = "";
                par2 = ((PythonCodeAction)action).pcode;


            }
            else if (action.GetType().FullName.EndsWith("AddIndentAction"))
            {
                key = "{";
                par1 = "";
                par2 = "";
            }
            else if (action.GetType().FullName.EndsWith("RecoverIndentAction"))
            {
                key = "}";
                par1 = "";
                par2 = "";
            }

            else if (action.GetType().FullName.EndsWith("ReadExternalConfAction"))
            {
                key = "读取外部配置";
                par1 = "";
                par2 = ((ReadExternalConfAction)action).excelPath;


            }


            else if (action.GetType().FullName.EndsWith("NullAction"))
            {
                key = "";

                par1 = ((NullAction)(action)).par1;
                par2 = ((NullAction)(action)).par2;
            }

            label_index = new Label();
            label_index.Text = index.ToString();
            label_index.Click += new EventHandler(ActionLine_Click);


            comboBox_key = new MyComboBox();


            comboBox_key.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_key.FlatStyle = FlatStyle.Flat;
            comboBox_key.Items.Add("");
            comboBox_key.Items.Add("新建驱动");
            //     comboBox_key.Items.Add("新建Appium驱动");
            //     comboBox_key.Items.Add("新建IOS驱动");
            comboBox_key.Items.Add("打开网址");
            comboBox_key.Items.Add("设置参数");
            comboBox_key.Items.Add("执行方法");
            comboBox_key.Items.Add("单步方法");
            comboBox_key.Items.Add("Python代码");
            comboBox_key.Items.Add("{");
            comboBox_key.Items.Add("}");
            comboBox_key.Items.Add("暂停");
            comboBox_key.Items.Add("调用其他用例");
            comboBox_key.Items.Add("读取外部配置");
            comboBox_key.Items.Add("退出");
            comboBox_key.Items.Add("注释");

            comboBox_key.Text = key;

            ////////////////   
            comboBox_key.SelectedIndexChanged += new EventHandler(comboBox_key_SelectedIndexChanged);


            //control_par1 = new TextBox();

            ////Console.WriteLine("判断动作名称结束" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));

            ////Console.WriteLine("判断key开始" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));



            if (key == "执行方法")
            {

                this.control_par1 = new TextBox();
                this.setPar2Items();

                this.control_par1.Enabled = false;
                this.control_par2.Enabled = true;




                comboBox_key.BackColor = KeyColor.ExecuteFunctionActionBackColor;
                comboBox_key.ForeColor = KeyColor.ExecuteFunctionActionFontColor;


            }
            else if (key == "单步方法")
            {
                this.setPar1Items();
                ((MyComboBox)control_par1).SelectedIndexChanged += new EventHandler(comboBox_oneStep_SelectedIndexChanged);


                this.control_par2 = new TextBox();

                this.control_par1.Enabled = true;
                this.control_par2.Enabled = true;

                comboBox_key.BackColor = KeyColor.OneStepActionBackColor;
                comboBox_key.ForeColor = KeyColor.OneStepActionFontColor;


            }


            else
            {
                control_par1 = new TextBox();
                control_par2 = new TextBox();

                if (key == "新建驱动")
                {
                    comboBox_key.BackColor = KeyColor.CreateDriverActionBackColor;
                    comboBox_key.ForeColor = KeyColor.CreateDriverActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par2.Enabled = false;

                }
                else if (key == "打开网址")
                {
                    comboBox_key.BackColor = KeyColor.GoActionBackColor;
                    comboBox_key.ForeColor = KeyColor.GoActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }
                else if (key == "设置参数")
                {
                    comboBox_key.BackColor = KeyColor.SetParameterActionBackColor;
                    comboBox_key.ForeColor = KeyColor.SetParameterActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par2.Enabled = true;

                }





                else if (key == "暂停")
                {
                    comboBox_key.BackColor = KeyColor.SleepActionBackColor;
                    comboBox_key.ForeColor = KeyColor.SleepActionFontColor;
                    this.control_par1.Enabled = true;
                    this.control_par2.Enabled = false;

                }

                else if (key == "调用其他用例")
                {
                    comboBox_key.BackColor = KeyColor.ExeOtherCaseActionBackColor;
                    comboBox_key.ForeColor = KeyColor.ExeOtherCaseActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }


                else if (key == "退出")
                {
                    comboBox_key.BackColor = KeyColor.QuitActionBackColor;
                    comboBox_key.ForeColor = KeyColor.QuitActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }
                else if (key == "注释")
                {
                    this.control_par2 = new AnnotationTextBox();
                    comboBox_key.BackColor = KeyColor.AnnotationActionBackColor;
                    comboBox_key.ForeColor = KeyColor.AnnotationActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }



                else if (key == "Python代码")
                {
                    this.control_par2 = new PythonTextBox();
                    comboBox_key.BackColor = KeyColor.PythonCodeActionBackColor;
                    comboBox_key.ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }
                else if (key == "{")
                {
                    comboBox_key.BackColor = KeyColor.PythonCodeActionBackColor;
                    comboBox_key.ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }
                else if (key == "}")
                {
                    comboBox_key.BackColor = KeyColor.PythonCodeActionBackColor;
                    comboBox_key.ForeColor = KeyColor.PythonCodeActionFontColor;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }


                else if (key == "读取外部配置")
                {
                    comboBox_key.BackColor = KeyColor.ReadExternalConfBackAction;
                    comboBox_key.ForeColor = KeyColor.ReadExternalConfFontAction;
                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = true;

                }



                else
                {
                    comboBox_key.BackColor = KeyColor.NullActionBackColor;
                    comboBox_key.ForeColor = KeyColor.NullActionFontColor;

                    this.control_par1.Enabled = false;
                    this.control_par2.Enabled = false;

                }

            }

            ////Console.WriteLine("判断key结束" + DateTime.Now.ToString("yyyyMMss HH:mm:ss.fff"));
            control_par1.Text = par1;
            control_par2.Text = par2;






            checkBox_isRun = new CheckBox();
            checkBox_isRun.Checked = isRun;

            checkBox_isRun.Left = control_par2.Left + control_par2.Width;


            checkBox_isRun.CheckedChanged += new EventHandler(checkBox_isRun_CheckedChanged);


            setColorByIsRun(isRun);


            setWidth(width);


            this.Controls.Add(label_index);
            this.Controls.Add(comboBox_key);
            this.Controls.Add(control_par1);
            this.Controls.Add(control_par2);
            this.Controls.Add(checkBox_isRun);

            this.Height = comboBox_key.Height + 2;


            /*
                        this.label_index.Visible = true;
                        this.comboBox_key.Visible = true;
                        this.control_par1.Visible = true;
                        this.control_par2.Visible = true;
                        this.checkBox_isRun.Visible = true;
             * 
             * 
             * */
            //this.Visible = true;


        }






        private void comboBox_key_MouseWheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("ssss");


        }

    }


    //节点类型的枚举  目录 或者 文件
    public enum NodeType { DIR, FILE }


    public class Form_Search : Form
    {
        
        ETabPage eTabPage;
        String searchKeyword;
        String replaceWord;
        TextBox textBox_searchKeyword ;

        TextBox textBox_replaceWord;


        Button button_search ;
        Button button_searchReplace;

       

        public Form_Search(ETabPage eTabPage)
            : base()
        {
            this.Text = "查找 替换";
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.eTabPage = eTabPage;

            Label label1 = new Label();
            label1.Text = "关键词：";

            Label label2 = new Label();
            label2.Text = "替换词：";


        


            textBox_searchKeyword = new TextBox();
            textBox_replaceWord = new TextBox();


            button_search = new Button();

            button_searchReplace = new Button();


            button_search.Text = "查找";
            button_searchReplace.Text = "查找并替换";

            label1.Width = label2.Width = 60;
            label1.Left = label2.Left = 10;
            label1.Top = 20;
            label2.Top = label1.Top + label1.Height + 10;

            textBox_searchKeyword.Top = label1.Top-5;
            textBox_searchKeyword.Left = label1.Left + 60;

           

            textBox_replaceWord.Top = label2.Top-5;
            textBox_replaceWord.Left = textBox_searchKeyword.Left;

            textBox_searchKeyword.Width = textBox_replaceWord.Width = 200;



    

            button_search.Top = textBox_replaceWord.Top + textBox_replaceWord.Height + 16;
            button_searchReplace.Top = button_search.Top;
            button_search.Left = label1.Left;
            button_searchReplace.Left = button_search.Left + button_search.Width + 32;

            this.Height = button_searchReplace.Top + button_searchReplace.Height + 40;

            this.Controls.Add(label1);

            this.Controls.Add(label2);

            this.Controls.Add(textBox_searchKeyword );

            this.Controls.Add(textBox_replaceWord);

            this.Controls.Add(button_searchReplace);

            this.Controls.Add(button_search);

            button_search.Click+=new EventHandler(button_search_Click);
            button_searchReplace.Click+=new EventHandler(button_searchReplace_Click);


        }

        public void button_search_Click(object sender, EventArgs e)
        {
            eTabPage.actionLines.recoverySearchColor();

            searchKeyword = this.textBox_searchKeyword.Text.Trim();
            if (null != eTabPage)
            {
                eTabPage.actionLines.search(searchKeyword);
            
            }
        }

        public void button_searchReplace_Click(object sender, EventArgs e)
        {
            eTabPage.actionLines.recoverySearchColor();

            searchKeyword = this.textBox_searchKeyword.Text;
            replaceWord = this.textBox_replaceWord.Text;
            if (null != eTabPage)
            {
                int count =  eTabPage.actionLines.searchAndReplace(searchKeyword, replaceWord);

                MessageBox.Show("一共有 "+ count + " 行中的文本被替换！");

            }
        }


    
    }


    public class OneStepMenuAndPrompt
    {
        public String menuText;
        public String promptText;

        public OneStepMenuAndPrompt(String menuText, String promptText)
        {
            this.menuText = menuText;
            this.promptText = promptText;
        }
    
    
    }


    public class Form_CopySteps : Form
    {
        Form fatherForm;

        int sourceTabStepSize;

        //源的步骤范围
        Label label_sourceSteps;
        public TextBox textBox_sourceSteps;

        //复制还是剪切
        Label label_copyOrCut;
        public ComboBox comboBox_copyOrCut;

        //复制或剪切到哪个已打开的用例
        Label label_targetCase;
        public ComboBox comboBox_targetCase;

        //复制或剪切到哪一步
        Label label_targetStep;
        public ComboBox comboBox_targetStep;


        //确定按钮
        public Button button_OK;


        private void comboBox_targetCase_SelectedIndexChanged(object sender, EventArgs e)
        {

            //////////Console.WriteLine(comboBox_targetCase.SelectedIndex);

            int targetIndex = comboBox_targetCase.SelectedIndex;

            ETabPage eTabPage = (ETabPage)((Form1)(this.fatherForm)).panelAndTabControl.tabControl.TabPages[targetIndex];
            int size = eTabPage.actionLines.actionList.Count;


            //////////Console.WriteLine("步骤数量=" + size);

            this.comboBox_targetStep.Items.Clear();

            for (int i = 0; i < size; i++)
            {
                this.comboBox_targetStep.Items.Add(i + 1);

            }
            comboBox_targetStep.Items.Add("最后");





        }


        public Form_CopySteps(Form fatherForm, List<String> targetCasePath)
            : base()
        {
            this.fatherForm = fatherForm;

            //得到源tag中一共有多少步骤

            sourceTabStepSize = ((ETabPage)(((Form1)(this.fatherForm)).panelAndTabControl.tabControl.SelectedTab)).actionLines.actionList.Count;



            label_sourceSteps = new Label();
            label_sourceSteps.Text = "源步骤范围";
            textBox_sourceSteps = new TextBox();
            label_sourceSteps.Top = 0;
            label_sourceSteps.Left = 0;
            textBox_sourceSteps.Top = 0;
            textBox_sourceSteps.Left = 100;

            if (0 == sourceTabStepSize)
            {
                textBox_sourceSteps.Enabled = false;
            }


            label_copyOrCut = new Label();
            label_copyOrCut.Text = "复制或剪切";
            comboBox_copyOrCut = new ComboBox();
            comboBox_copyOrCut.Items.AddRange(new String[] { "复制", "剪切" });
            label_copyOrCut.Top = 50;
            label_copyOrCut.Left = 0;
            comboBox_copyOrCut.Top = 50;
            comboBox_copyOrCut.Left = 100;


            label_targetCase = new Label();
            label_targetCase.Text = "目标用例";
            comboBox_targetCase = new ComboBox();
            comboBox_targetCase.SelectedIndexChanged += new EventHandler(comboBox_targetCase_SelectedIndexChanged);
            label_targetCase.Top = 100;
            label_targetCase.Left = 0;
            comboBox_targetCase.Top = 100;
            comboBox_targetCase.Left = 100;

            int selectedIndex = ((Form1)(this.fatherForm)).panelAndTabControl.tabControl.SelectedIndex;



            int i = 0;
            foreach (String casePath in targetCasePath)
            {

                int l = casePath.LastIndexOf("\\");
                String fileName = casePath.Substring(l + 1);

                if (i == selectedIndex)
                {
                    fileName += "(自己)";

                }

                comboBox_targetCase.Items.Add(fileName);

                i++;
            }



            label_targetStep = new Label();
            label_targetStep.Text = "从哪一步插入";
            comboBox_targetStep = new ComboBox();
            label_targetStep.Top = 150;
            label_targetStep.Left = 0;
            comboBox_targetStep.Top = 150;
            comboBox_targetStep.Left = 100;


            button_OK = new Button();
            button_OK.Text = "确定";
            button_OK.Top = 200;


            this.Controls.Add(this.label_sourceSteps);
            this.Controls.Add(this.textBox_sourceSteps);

            this.Controls.Add(this.label_copyOrCut);
            this.Controls.Add(this.comboBox_copyOrCut);

            this.Controls.Add(this.label_targetCase);
            this.Controls.Add(this.comboBox_targetCase);

            this.Controls.Add(this.label_targetStep);
            this.Controls.Add(this.comboBox_targetStep);

            this.Controls.Add(this.button_OK);

            button_OK.Click += new EventHandler(button_OK_Click);


        }

        public void button_OK_Click(object sender, EventArgs e)
        {
            int sourceTabIndex = 0;
            int sourceStepStart = 0;
            int sourceStepEnd = 0;
            int targetTabIndex = 0;
            int targetStep = 0;
            String copyOrCut = "";

            sourceTabIndex = ((Form1)(this.fatherForm)).panelAndTabControl.tabControl.SelectedIndex;

            String tmpStr = this.textBox_sourceSteps.Text;

            try
            {

                if (tmpStr.Contains("-"))
                {
                    String[] tmps = tmpStr.Split(new String[] { "-" }, StringSplitOptions.None);

                    sourceStepStart = Int32.Parse(tmps[0]) - 1;
                    sourceStepEnd = Int32.Parse(tmps[1]) - 1;

                }
                else
                {
                    sourceStepStart = Int32.Parse(tmpStr) - 1;
                    sourceStepEnd = sourceStepStart;

                }



                if (sourceStepStart >= 0 && sourceStepEnd <= this.sourceTabStepSize - 1 && sourceStepStart <= sourceStepEnd)
                {
                    if ("复制" == comboBox_copyOrCut.Text)
                    {
                        copyOrCut = "COPY";

                    }
                    else
                    {

                        copyOrCut = "CUT";
                    }

                    targetTabIndex = this.comboBox_targetCase.SelectedIndex;
                    targetStep = this.comboBox_targetStep.SelectedIndex;

                    ((Form1)(this.fatherForm)).panelAndTabControl.copyOrCutSteps(sourceTabIndex, sourceStepStart, sourceStepEnd, targetTabIndex, targetStep, copyOrCut);

                }
                else
                {
                    MessageBox.Show("请输入正确的范围", "错误");


                }


            }
            catch (Exception e2)
            {
                //////////Console.WriteLine(e2.Message);
                MessageBox.Show("请输入正确的格式，如“2-6”", "错误");

            }









        }


    }

}
