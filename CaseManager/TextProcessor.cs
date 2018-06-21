using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CaseManager
{

    class TextProcessor
    {
        public String getAbsPath()
        {
            String exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            int i = exePath.LastIndexOf("\\");

            return exePath.Substring(0, i);

        }

        public int getBeginIndex()
        {
            String tmp = File.ReadAllText(this.getAbsPath() + "\\conf\\index.txt", Encoding.UTF8);
            int i = Int32.Parse(tmp);
            return i; 
        }


        //写文本
        public void writeContent(String filePath, String content, bool append)
        {
            StreamWriter sw = new StreamWriter(filePath, append, new UTF8Encoding(false));
            sw.WriteLine(content);
            sw.Close();
        }

        //通过环境文件  和  数据文件 得到数据字典  并加入几个特殊的键
        private Dictionary<String, String> getTestData(String envFileName, String excelFileName)
        {
            
                String envName = File.ReadAllText(envFileName, Encoding.UTF8).Trim().Replace("\r\n", "");

                Dictionary<String, String> dic = new ExcelProcessor().getDicByEnvNameFromExcel(excelFileName, envName);


                return dic;

           


        }


        public List<OneStepMenuAndPrompt> getOneStepMenuAndPromptList(String filePath)
        {
            //Console.WriteLine(DateTime.Now);
            
            //Console.WriteLine("调用getOneStepMenuAndPromptList");

            List<OneStepMenuAndPrompt> list = new List<OneStepMenuAndPrompt>();

            String allText = File.ReadAllText(filePath, Encoding.UTF8);

            String[] tmps = allText.Split(new String[] { "\r\n" } , StringSplitOptions.RemoveEmptyEntries);
       

            foreach (String line in tmps)
            {
                String line2 = line.Trim();

                String menuText="";
                String prompText = "";

                if( line2.Contains("=" ))
                {
                    int i = line2.IndexOf("=");
                    menuText = line2.Substring(0, i);
                    prompText = line2.Substring(i + 1);
                }
                else
                {
                    menuText = line2;
                    prompText = "";
                }

                OneStepMenuAndPrompt oneStepMenuAndPrompt = new OneStepMenuAndPrompt(menuText, prompText);

                list.Add(oneStepMenuAndPrompt);
            }
            return list;
        }



        //从一个文本文件中得到字典  每行用“=”分割
        private Dictionary<String, String> getDic(String filePath)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();
            String allText = File.ReadAllText(filePath, Encoding.UTF8);
            String[] lines = allText.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String line in lines)
            {
                String[] tmps = line.Split(new String[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                dic.Add(tmps[0], tmps[1]);
            }
            return dic;
        }

        //从方法配置文件中得到所有的方法名
        public List<String> getFuns(String filePath)
        {
            List<String> list = new List<string>();
            Dictionary<String, String> dic = getDic(filePath);

            foreach (String str in dic.Keys)
            {
                list.Add(str);
            }
            return list;
        }

        //codeModulFilePath 代码模版路径
        //importFilePath    代码中导入部分的文件路径
        //excelFilePath     用例的电子表格文件路径
        //functionFilePath  方法配置文件路径
        //envFilePath       环境文件路径
        //dataFilePath      数据文件路径
        //得到整个临时python文件内容
        public String getWholePyCodeFromScriptFile(String codeModulFilePath, String testPagePath, String excelFilePath, String envFilePath, String dataFilePath ,String importFilePath)
        {
            String importText = this.createImportCode(testPagePath);

            String importText2 = "";
            if (File.Exists(importFilePath))
            { 
                importText2 = File.ReadAllText(importFilePath, Encoding.UTF8);
            }


            String pyCode = "";

            try
            {

                pyCode = getPyCodes(excelFilePath, testPagePath, envFilePath, dataFilePath);


            }
            catch (Exception e)
            {
                throw (e);
            }

            


            String modulText = File.ReadAllText(codeModulFilePath, Encoding.UTF8);
            return "#encoding=utf-8\r\n\r\nfrom frame.MyTestCase import MyTestCase\r\nfrom frame.DriverInit import DriverInit\r\nfrom frame.PageOfPublic import PageOfPublic\r\nimport time\r\nimport json\r\n" + importText + "\r\n\r\n" + importText2 + "\r\n\r\n\r\n" + modulText.Replace("$CODES$", pyCode);
       
        }




        
        //自动生成 import 代码
        private String createImportCode(String testPagePath)
        {
            List<String> list = new DirectoryManager().getAllFiles(testPagePath, "py");
            List<String> list2 = new List<string>();
            foreach (String str in list)
            {
                if (!str.Contains("__init__"))
                {
                    String codeLine = path2ImportCode(str);
                    list2.Add(codeLine);
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (String str in list2)
            {
                //////Console.WriteLine(str);
                sb.Append( str + "\r\n" );  
            }
            sb.Append("\r\n");
            return sb.ToString();
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

        //自动生成方法字典
        public Dictionary<String, String> createFunctionDic(String testPagePath)
        {

            List<String> list = new DirectoryManager().getAllFiles(testPagePath, "py");
            List<String> list2 = new List<string>();
            foreach (String str in list)
            {
                if (!str.Contains("__init__"))
                {

                    list2.Add(str);
                }
            }

            Dictionary<String, String> dicAll = new Dictionary<string, string>();

            foreach (String filePath in list2)
            {
                Dictionary<String, String> dic = getFunctionFromPythonFile(filePath);

                foreach (KeyValuePair<String, String> kv in dic)
                {
                    if (!dicAll.Keys.Contains(kv.Key))
                    {
                        dicAll.Add(kv.Key, kv.Value);
                    }
                    else
                    {
                        dicAll.Add(kv.Key+"X" , kv.Value);
                    }
                }
            }
            return dicAll;

        
        
        }


        //必须传入一个 python文件的路径
        private Dictionary<String,String > getFunctionFromPythonFile(String pythonFile)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();

            String pythonClassName = Path.GetFileName(pythonFile).Replace(".py", "");

            String[] lines = File.ReadAllLines(pythonFile, Encoding.UTF8);

            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim() ;

                if (line.StartsWith("####") && line.EndsWith("####"))
                {
                    String bFunctionName = line.Replace("#","");

                    try
                    {
                        String nextLine = lines[i + 1];

                        if (nextLine.Contains("def") && nextLine.Contains("self"))
                        {
                            String pythonFunction = nextLine.Replace(":", "").Replace("def", "").Replace("(self)", "").Trim();
                            String pythonCode = pythonClassName + "." + pythonFunction;
                            dic.Add(bFunctionName, pythonCode);
                        }



                    }
                    catch (Exception e)
                    { 
                    
                    }

 



                }
            }
            return dic;


        
        }




     



        //excelFile        用例的电子表格文件
        //functionDic      方法字典
        //dataDic          数据字典
        //生成中间的test里面的执行部分的python代码
        private String getPyCodes(String excelFile, String testPagePath, String envFilePath, String dataFilePath)
        {

            List<Action> list = null;// new ExcelProcessor().readActionListFromExcelFile(excelFile);
            try
            {
                list = new ExcelProcessor().readActionListFromExcelFile(excelFile);
            }
            catch (Exception e )
            {
                
                throw (new Exception( e.Message + "\r\n" + "1读取用例文件出错，可能是因为用其他程序打开了用例文件！"));
            
            }

            Dictionary<String, String> functionDic = null;
            try
            { 
                functionDic = new TextProcessor().createFunctionDic(testPagePath);
            
            }
            catch(Exception e )
            {
                throw( new Exception("自定义方法读取异常") );
            }

            Dictionary<String, String> dataDic = null;

            try
            {

                dataDic = this.getTestData(envFilePath, dataFilePath);
                //这里再加入几个特殊的键
                String timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
                dataDic.Add("TIME", timeStr);
                dataDic.Add("RANDOM2", getRandom(2));
                dataDic.Add("RANDOM3", getRandom(3));
                dataDic.Add("RANDOM4", getRandom(4));
                dataDic.Add("RANDOM5", getRandom(5));
                dataDic.Add("RANDOM6", getRandom(6));
            }
            catch (Exception e)
            {
                throw (new Exception("读取配置文件出错，可能是因为用其他程序打开了配置文件！"));
            }


           
   

           
      



            return getPyCodes(list, functionDic, dataDic,0);

        }


        public String getRandom(int i)
        {
            Random rd = new Random();
            int r = rd.Next((int)System.Math.Pow(10, i));

            String tmp = "00000000000000000000" + r;
            int l = tmp.Length;

            tmp = tmp.Substring(l - i);

            return tmp;

        
        }

        private String getPyCodes(List<Action> list, Dictionary<String, String> functionDic, Dictionary<String, String> dataDic, int indent)
        {
            return getPyCodes(list, functionDic, dataDic, TextProcessor.indent , false);
        }





        //list             动作的列表
        //functionDic      方法字典
        //dataDic          数据字典
        //remove3Action    对新建驱动  打开网址  退出  忽略
        //生成中间的test里面的执行部分的python代码
        private String getPyCodes(List<Action> list, Dictionary<String, String> functionDic, Dictionary<String, String> dataDic , int indent, bool remove3Action )
        {
            StringBuilder sb = new StringBuilder();

            foreach (Action action in list)
            {
                String pythonCode = "";
                if (remove3Action)
                {
                    if (action.GetType().FullName.EndsWith("CreateDriverAction") || action.GetType().FullName.EndsWith("GoAction") || action.GetType().FullName.EndsWith("QuitAction"))
                    {
                        pythonCode = "";

                    }
                    else
                    {
                        pythonCode = getPyCode(action, functionDic,  dataDic,indent);
                    
                    }



                }
                else
                {
                    pythonCode = getPyCode(action, functionDic, dataDic ,indent);
                
                }


                
                sb.Append(pythonCode + "\r\n");
            }
            return sb.ToString();
        }

        public static int indent = 0;


        private String getIndentSpace(int indent)
        {
            String tmp = "";
            for (int i = 0; i < indent; i++)
            {
                tmp += "    ";
            
            }
            return tmp;
 
        
        
        }
        

        //action           动作
        //functionDic      方法字典
        //dataDic          数据字典
        private String getPyCode(Action action, Dictionary<String, String> functionDic, Dictionary<String, String> dataDic , int indent)
        {

            String code = "";

            int index = action.index;
            bool isRun = action.isRun;

            String note = this.getIndentSpace(TextProcessor.indent) + "        #步骤" + index + "生成的代码\r\n";
              //+this.getIndentSpace(TextProcessor.indent) + "        print('步骤" + index + "')";

            if (isRun)
            {
                //创建驱动
                if (action.GetType().FullName.EndsWith("CreateDriverAction"))
                {
                    String browserName = ((CreateDriverAction)action).browserName;
                    String driverName = ((CreateDriverAction)action).driverName;
                    code = this.getIndentSpace(TextProcessor.indent) + "        if self.getPar('LASTSTEP'):";
                    code += "\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "            self.driver = DriverInit(self.logger).getWebDriver('" + browserName + "')";
                    code += "\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "            pP=PageOfPublic(self.pars,self.driver,self.logger)";

                    
                }

                /*

                //创建Appium驱动
                else if (action.GetType().FullName.EndsWith("CreateAppiumDriverAction"))
                {
                    String deviceName = ((CreateAppiumDriverAction)action).deviceName;
                    String appPackageAndAppActivity = ((CreateAppiumDriverAction)action).appPackageAndAppActivity;
                    String[] tmp = appPackageAndAppActivity.Split( new String[] {","},StringSplitOptions.None );
                    String appPackage = tmp[0];
                    String appActivity = tmp[1];
                    code = "        this.appiumDriver = new Init().getAppiumDriver(\"" + deviceName + "\", \"" + appPackage + "\", \"" + appActivity + "\");" + "\r\n        this.appPackage=\"" + appPackage + "\";";
                }

                    //创建IOS驱动
                else if (action.GetType().FullName.EndsWith("CreateIOSDriverAction"))
                {
                    String macIP = ((CreateIOSDriverAction)action).macIP;
                    String appPath = ((CreateIOSDriverAction)action).appPath;
                    code = "        this.appiumDriver = new Init().getIOSDriver(\""+ macIP +"\",\""+ appPath +"\");";
                
                }
                 * */



               // 打开网址

                else if (action.GetType().FullName.EndsWith("GoAction"))
                {

                    String driverName = ((GoAction)action).driverName;
                    String url = ((GoAction)action).url;
                    code = this.getIndentSpace(TextProcessor.indent) + "        if self.getPar('LASTSTEP'):";
                    code += "\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "            self.openURL('" + url + "')";

                }


                //设置参数
                else if (action.GetType().FullName.EndsWith("SetParameterAction"))
                {
                    String parName = ((SetParameterAction)action).parName;
                    String parValue = ((SetParameterAction)action).parValue;

                    
                    code = this.getIndentSpace( TextProcessor.indent ) + "        self.setPar(\"" + parName + "\", \"" + parValue + "\")";

                }

                //执行方法
                else if (action.GetType().FullName.EndsWith("ExecuteFunctionAction"))
                {
                    String driverName = ((ExecuteFunctionAction)action).driverName;

                    String functionName = ((ExecuteFunctionAction)action).functionName;

                    if (functionDic.Keys.Contains(functionName))
                    {

                        String classAndFunction = functionDic[functionName];
                        
                        code = this.getIndentSpace( TextProcessor.indent ) +  "        " + classAndFunction.Replace(".", "(self.pars, self.driver, self.logger).") + "()";
                    }
                    else
                    {
                        
                        code =  this.getIndentSpace(TextProcessor.indent) + "        print('没有找到配置的方法')";
                    }
                }
                    //读取外部配置
                else if (action.GetType().FullName.EndsWith("ReadExternalConfAction"))
                {

                    String excelPath = ((ReadExternalConfAction)action).excelPath;

                    if( ":"!= excelPath.Substring(1,1 ))
                    {
                        String absPath =  new TextProcessor().getAbsPath();
                        excelPath = absPath + "\\files\\" + excelPath;
                    }

                    ExcelProcessor excelProcessor =new ExcelProcessor();

                    List<List<String>> lists = excelProcessor.readfirststr(excelPath);

                    Dictionary<String,String> dic = excelProcessor.strList2Dic(lists);


                    //code = this.getIndentSpace(TextProcessor.indent) + "        print('步骤" + index + "')";
                    foreach( KeyValuePair<String,String> kv in dic )
                    {
                        String k = kv.Key;
                        String v = kv.Value;


                        code += this.getIndentSpace( TextProcessor.indent ) + "        self.setPar(\"" + k + "\", \"" + v + "\")";
                        code += "\r\n";
                    
                    }
                }




                //单步方法
                else if (action.GetType().FullName.EndsWith("OneStepAction"))
                {
                    String oneStepAction = ((OneStepAction)action).oneStepName;
                    String oneStepPars = ((OneStepAction)action).oneStepPars;
                   

                    code =this.getIndentSpace( TextProcessor.indent ) + "        pP.oneStep('" + oneStepAction + "','''" + oneStepPars + "''')";
                
                }

                //暂停
                else if (action.GetType().FullName.EndsWith("SleepAction"))
                {
                    int timeLong = ((SleepAction)action).timeLong;
                   
                    code = this.getIndentSpace( TextProcessor.indent ) +  "        time.sleep(" + timeLong + ")";

                }

                    //调用其他用例
                else if (action.GetType().FullName.EndsWith("ExeOtherCaseAction"))
                {
                    String casePath = ((ExeOtherCaseAction)action).casePath;
                    String caseFullPath = new TextProcessor().getAbsPath() + "\\" + casePath;

                    //////Console.WriteLine(casePath);

                    List<Action> list = new ExcelProcessor().readActionListFromExcelFile(caseFullPath);

                    code = this.getIndentSpace(TextProcessor.indent) + "        ################################################################";
                    code += "\r\n\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "        #执行用例[" + casePath + "]";
                    code += "\r\n\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "        ################################################################";
                    code += "\r\n";

                    code += this.getIndentSpace(TextProcessor.indent) + "        self.logger.appendContent('\\r\\n################################################################\\r\\n执行用例[" + casePath + "]\\r\\n################################################################')";
                    code += "\r\n";
                    code +=  this.getPyCodes(list, functionDic, dataDic, TextProcessor.indent);

                
                }



                //退出
                else if (action.GetType().FullName.EndsWith("QuitAction"))
                {
                    String driverName = ((QuitAction)action).driverName;

                    code = this.getIndentSpace(TextProcessor.indent) + "        if None!=self.driver:";

                    code += "\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "            self.driver.quit()";
                    code += "\r\n";
                    code += this.getIndentSpace(TextProcessor.indent) + "            self.logger.appendContent(\"退出\")";
                }

                //注释
                else if (action.GetType().FullName.EndsWith("QuitAction"))
                {
                    String annotationText = ((AnnotationAction)action).annotationText;
                    
                    code = this.getIndentSpace(TextProcessor.indent) + "        #" + annotationText;
                }


                //Python代码
                else if (action.GetType().FullName.EndsWith("PythonCodeAction"))
                {
                    String pcode = ((PythonCodeAction)action).pcode;
                    

                    code = this.getIndentSpace( TextProcessor.indent ) + "        " + pcode;
                }

                //{
                else if (action.GetType().FullName.EndsWith("AddIndentAction"))
                {
                    TextProcessor.indent++;
                    code =this.getIndentSpace( TextProcessor.indent )+ "        #开始缩进";
                    
                }
                //{
                else if (action.GetType().FullName.EndsWith("RecoverIndentAction"))
                {
             
                    
                    
                    code += this.getIndentSpace(TextProcessor.indent) + "        #恢复缩进";
                    TextProcessor.indent--;
                }


                return note + "\r\n" + replaceVars(code, dataDic) + "\r\n";
            }
            else
            {
                return "";
            }





            /*
            
            String fun = executeObject.fun;
            String code = "";

            if ("设置参数" == fun)
            {
                String par1 = executeObject.par1;
                String par2 = executeObject.par2;
                code =  "        pars.put(\"" + par1 + "\", \"" + par2 + "\");";
            }
            else if ("打开网址" == fun || "转到网址" == fun)
            {
                String par1 = executeObject.par1;

                code = "        openURL(\"" + par1 + "\");";

            }
            else if ("暂停" == fun)
            {
                String par1 = executeObject.par1;
                code = "        sleep("+par1+");";

            }

            else
            {
                if (functionDic.Keys.Contains(fun))
                {
                    String classAndFunction = functionDic[fun];
                    code = "        " + classAndFunction;

                }
                else
                {
                    code = "";
                }


            }
            return replaceVars(code, dataDic);
            */


        }

        //替换多个参数
        private String replaceVars(String str, Dictionary<String, String> dataDic)
        {
            String tmp = str;

            foreach (String key in dataDic.Keys)
            {
                String a = "$" + key + "$";
                String b = dataDic[key];
                tmp = tmp.Replace(a, b);
            }
            return tmp;
        }
    }


    class BusinessFunction
    {
        String bFunctionName;
        String pythonClassName;
        String pythonFunctionName;

        public BusinessFunction(String bFunctionName,String pythonClassName,String pythonFunctionName)
        {
            this.bFunctionName = bFunctionName;
            this.pythonClassName = pythonClassName;
            this.pythonFunctionName = pythonFunctionName;
        }
    
    }


    /*
    class ExecuteObject
    {
        public String fun;
        public String par1;
        public String par2;
        public ExecuteObject(String fun, String par1, String par2)
        {
            this.fun = fun;
            this.par1 = par1;
            this.par2 = par2;
        }
        public ExecuteObject(String fun, String par1)
        {
            this.fun = fun;
            this.par1 = par1;
        }

        public ExecuteObject(String fun)
        {
            this.fun = fun;
        }

    }
     * */

}
