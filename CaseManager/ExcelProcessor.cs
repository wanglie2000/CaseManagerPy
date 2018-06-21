using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;

namespace CaseManager
{
    class ExcelProcessor
    {
        public List<String> getsheets(String filename)
        {
            using (FileStream fs = File.OpenRead(filename))   //打开myxls.xls文件
            {
                NPOI.HSSF.UserModel.HSSFWorkbook wk = new HSSFWorkbook(fs);   //把xls文件中的数据写入wk中
                int n = wk.NumberOfSheets;

                List<String> sheetnames = new List<string>();
                for (int i = 0; i < n; i++)
                {
                    sheetnames.Add(wk.GetSheetName(i));

                }

                return sheetnames;


            }


        }
 
        public void writeToExcelFile( List<List<String>> listList ,String fileName )
        {
            using (FileStream fs = File.OpenWrite(fileName))
            {
                HSSFWorkbook  workbook = new HSSFWorkbook();

                ISheet iSheet = workbook.CreateSheet("testCase");


                int j = 0;
                foreach (List<String> list in listList)
                {
                    int i = 0;
                    IRow row = iSheet.CreateRow(j);
                    foreach (String value in list)
                    {
                        row.CreateCell(i).SetCellValue(value);
                        i++;
                    }
                    j++;
                }
                workbook.Write(fs);
            
            }

        
        }

        public List<List<String>> readfirststr(String filename)
        {
            using (FileStream fs = File.OpenRead(filename))   //打开myxls.xls文件
            {

                List<List<String>> listlist = new List<List<string>>();           //--------------------------------


                NPOI.HSSF.UserModel.HSSFWorkbook wk = new HSSFWorkbook(fs);   //把xls文件中的数据写入wk中



                ISheet sheet = wk.GetSheetAt(0); //第一个sheet 

                if (null != sheet)
                {
                    for (int j = 0; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        List<String> list = new List<string>();              //***************************************

                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row != null)
                        {

                            for (int k = 0; k < row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                            {
                                ICell cell = row.GetCell(k);  //当前表格
                                if (cell != null)
                                {
                                    list.Add(cell.ToString());
                                }
                                else
                                {
                                    list.Add("");
                                }
                            }


                        }

                        listlist.Add(list);
                    }

                    this.plastic(listlist);

                    return listlist;

                }
                else
                {
                    return null;
                }
            }
        }


        private void plastic(List<List<String>> lists)
        {
            if (lists.Count > 1)
            {
                int w = lists[0].Count;
                for (int i = 1; i < lists.Count;i++ )
                {
                    List<String> list = lists[i];

                    

                    if (list.Count < w)
                    { 
                        int addNum = w- list.Count;

                        for (int j = 0; j < addNum; j++)
                        {
                            list.Add("");
                        }
                    }
                }
            }
        }

        
        public List<List<String>> readfirststr(String filename, String sheetname)
        {
            using (FileStream fs = File.OpenRead(filename))   //打开myxls.xls文件
            {

                List<List<String>> listlist = new List<List<string>>();           //--------------------------------


                NPOI.HSSF.UserModel.HSSFWorkbook wk = new HSSFWorkbook(fs);   //把xls文件中的数据写入wk中

                

                ISheet sheet = wk.GetSheet(sheetname); //根据 sheet 名 

                if (null != sheet)
                {
                    for (int j = 0; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        List<String> list = new List<string>();              //***************************************

                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row != null)
                        {

                            for (int k = 0; k < row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                            {
                                ICell cell = row.GetCell(k);  //当前表格
                                if (cell != null)
                                {
                                    list.Add(cell.ToString());
                                }
                                else
                                {
                                    list.Add("");
                                }
                            }


                        }

                        listlist.Add(list);
                    }
                    this.plastic(listlist);

                    return listlist;

                }
                else
                {
                    return null;
                }
            }
        }

        public List<Action> readActionListFromExcelFile(String fileName)
        {
            try
            {

                //////Console.WriteLine("readActionListFromExcelFile方法的参数= " + fileName);

                //得到第一个页签的名字
                String sheetName = getsheets(fileName)[0];

                //得到所有的文本
                List<List<String>> strListList = readfirststr(fileName, sheetName);

                //转换成  Action的列表
                List<Action> actionList = strListList2ActionList(strListList);
                return actionList;


            }
            catch (Exception e)
            {
                throw (new Exception( e.Message + "\r\n" +  "2读取用例文件出错，可能是因为用其他程序打开了用例文件！"));
            
            }


        }


        //一个二维数组转换成一个字典    里面这一维  是长度为2的数组，转换出来，一个做键一个做值
        public Dictionary<String, String> strList2Dic(List<List<String>> strListList)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();

            foreach (List<String> list in strListList)
            {
                String k = list[0];
                String v = list[1];
                dic.Add(k, v);
            }
            return dic;
        }



        List<Action> strListList2ActionList(List<List<String>> strListList)
        {
            List<Action> actionList = new List<Action>();
            foreach (List<String> strList in strListList)
            {
                Action action = strList2Action(strList);

                if (null != action)
                {
                    actionList.Add(strList2Action(strList));
                
                }  
            
            }
            return actionList;
        
        
        
        }

        Action strList2Action( List<String> strList)
        {
            if (strList.Count == 5)
            {
                int index = Int32.Parse(strList[0]);

                bool isRun = (strList[4].Trim() == "是");

                String actionName = strList[1];

                String driverName = "default";
           
                switch (actionName)
                {
                    case "新建驱动":
                        String browserName = strList[2];
                        driverName = strList[3];
                        return  new CreateDriverAction(index, browserName, driverName, isRun);

                    case "新建Appium驱动":
                        String deviceName = strList[2];
                        String appPackageAndAppActivity = strList[3];
                        return new CreateAppiumDriverAction(index, deviceName, appPackageAndAppActivity, isRun);

                    case "新建IOS驱动":
                        String macIP = strList[2];
                        String appPath = strList[3];
                        return new CreateIOSDriverAction(index, macIP, appPath, isRun);

                    case "打开网址":
                        driverName = strList[2];
                        String url = strList[3];
                        return new GoAction(index,driverName,url,isRun);

                    case "设置参数":
                        String parName = strList[2];
                        String parValue = strList[3];
                        return new SetParameterAction(index, parName, parValue, isRun);

                    case "执行方法":
                        driverName = strList[2];
                        String functionName = strList[3];
                        return new ExecuteFunctionAction(index, driverName, functionName, isRun);


                    case "单步方法":
                        String oneStepAction = strList[2];
                        String oneStepParse = strList[3];
                        return new OneStepAction(index, oneStepAction, oneStepParse, isRun);


                    case "暂停":
                        int timeLong = Int32.Parse(strList[2].Trim());
                        return new SleepAction(index, timeLong, isRun);

                    case "调用其他用例":
                        String casePath = strList[3];
                        return new ExeOtherCaseAction(index, casePath, isRun);




                    case "退出":
                        driverName = strList[2];
                        return new QuitAction(index, driverName, isRun);

                    case "注释":
                        String annotationText = strList[3];
                        return new AnnotationAction(index, annotationText, isRun);

                    case "Python代码":
                        String code = strList[3];
                        return new PythonCodeAction(index, code, isRun);

                    case "{":
                        return new AddIndentAction(index, isRun);

                    case "}":
                        return new RecoverIndentAction(index, isRun);




                    case "读取外部配置":
                        String excelPath = strList[3];

                        return new ReadExternalConfAction(index, excelPath, isRun);





                    case "":
                        String par1 = strList[2];
                        String par2 = strList[3];
                        return new NullAction(index, par1, par2, isRun);
                    default:
                        return null;
                }




            }
            else
            {
                return null;
            
            }

            

        
        
        }


        public Dictionary<String, String> getDicByEnvNameFromExcel(String excelFileName, String envName)
        {
            List<List<String>> lists = this.readfirststr(excelFileName, "Sheet1");
            List<String> head = lists[0];

            int l = lists.Count;

            Dictionary<String, String> dic = new Dictionary<string, string>();

            int x = head.IndexOf(envName);

            if (x > 0)
            {
                for (int y = 1; y < l; y++)
                {
                    List<String> list = lists[y];
                    String key = list[0];
                    String value = list[x];

                    dic.Add(key, value);
                }
            }
            return dic;   
        }
    }
}
