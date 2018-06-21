using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseManager
{
    public class Action
    {
        public int index;
        public bool isRun;
        public Action(int index, bool isRun)
        {
            this.index = index;
            this.isRun = isRun;
        }

    }
    public class NullAction : Action
    {
        public List<String> toList()
        { 
            List<String> list = new List<String>();
            list.Add(  this.index.ToString()  );
            list.Add("");
            list.Add("");
            list.Add("");

            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String par1;
        public String par2;

        public NullAction(int index, String par1, String par2, bool isRun)
            : base(index, isRun)
        {
            this.par1 = par1;
            this.par2 = par2;
        }
    
    }

    



    public class CreateIOSDriverAction : Action
    {


        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("新建IOS驱动");
            list.Add(this.macIP);
            list.Add(this.appPath);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }


        public String macIP;
        public String appPath;

        public CreateIOSDriverAction(int index, String macIP, String appPath, bool isRun)
            : base(index, isRun)
        {

            this.macIP = macIP;
            this.appPath = appPath;
        
        }
    
    
    }



    public class CreateAppiumDriverAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("新建Appium驱动");
            list.Add(this.deviceName);
            list.Add(this.appPackageAndAppActivity);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String deviceName;
        public String appPackageAndAppActivity;


        public CreateAppiumDriverAction(int index, String deviceName, String appPackageAndAppActivity, bool isRun)
            : base(index, isRun)
        {
            this.deviceName = deviceName;
            this.appPackageAndAppActivity = appPackageAndAppActivity;

        }
    
    
    }

    public class CreateDriverAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("新建驱动");
            list.Add(browserName);
            list.Add(driverName);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String browserName;
        public String driverName;

        public CreateDriverAction(int index, String browserName, String driverName, bool isRun)
            : base(index, isRun)
        {
            this.browserName = browserName;
            this.driverName = driverName;

        }

    }

    class GoAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("打开网址");
            list.Add(driverName);
            list.Add(url);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String driverName;
        public String url;
        public GoAction(int index,String driverName,  String url, bool isRun)
            : base(index, isRun)
        {
            this.driverName = driverName;
            this.url = url;
        }

    
    }


    class SetParameterAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("设置参数");
            list.Add(parName);
            list.Add(parValue);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String parName;
        public String parValue;
        public SetParameterAction(int index,  String parName, String parValue, bool isRun)
            : base(index, isRun)
        {
            this.parName = parName;
            this.parValue = parValue;
        }

    }

    class ExecuteFunctionAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("执行方法");
            list.Add(driverName);
            list.Add(functionName);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String driverName;
        public String functionName;
       

        public ExecuteFunctionAction(int index, String driverName, String functionName, bool isRun)
            : base(index, isRun)
        {
            this.driverName = driverName;
            this.functionName = functionName;

        }
    }


    public class OneStepAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("单步方法");
            list.Add(oneStepName);
            list.Add(oneStepPars);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }
        public String oneStepName;
        public String oneStepPars;

        public OneStepAction(int index, String oneStepName, String oneStepPars, bool isRun)
            : base(index, isRun)
        {
            this.oneStepName = oneStepName;
            this.oneStepPars = oneStepPars;
        }
    }


    class SleepAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("暂停");
            list.Add(timeLong.ToString());
            list.Add("");
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public int timeLong;
        public SleepAction(int index, int timeLong, bool isRun)
            : base(index, isRun)
        {
            this.timeLong = timeLong;

        }
    }
    class ExeOtherCaseAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("调用其他用例");
            list.Add("");
            list.Add(casePath);
            
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        
        }
        public String casePath;
        public ExeOtherCaseAction(int index, String casePath, bool isRun)
            : base(index, isRun)
        {
            this.casePath = casePath;
        }
    }

    class AnnotationAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("注释");
            list.Add("");
            list.Add(annotationText);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String annotationText;
        public AnnotationAction(int index, String annotationText, bool isRun)
            : base(index, isRun)
        {
            this.annotationText = annotationText;
        }
    
    
    }

    class PythonCodeAction : Action
    {


        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("Python代码");
            list.Add("");
            list.Add(pcode);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String pcode;
        public PythonCodeAction(int index, String pcode, bool isRun)
            : base(index, isRun)
        {
            this.pcode = pcode;
        }
    }

    //{
    class AddIndentAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("{");
            list.Add("");
            list.Add("");
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }
        public AddIndentAction(int index, bool isRun)
            : base(index, isRun)
        {
        
        }
    }
    //}
    class RecoverIndentAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("}");
            list.Add("");
            list.Add("");
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }
        public RecoverIndentAction(int index, bool isRun)
            : base(index, isRun)
        {
        
        }
    }

    class QuitAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("退出");
            list.Add(driverName);
            list.Add("");
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;
        }

        public String driverName;
        public QuitAction(int index, String driverName, bool isRun)
            : base(index, isRun)
        {
            this.driverName = driverName;
        
        }
    
    }

    class ReadExternalConfAction : Action
    {
        public List<String> toList()
        {
            List<String> list = new List<String>();
            list.Add(this.index.ToString());
            list.Add("读取外部配置");
            list.Add("");
            list.Add(excelPath);
            if (isRun)
            {
                list.Add("是");
            }
            else
            {
                list.Add("否");
            }
            return list;

        }

        public String excelPath;

        public ReadExternalConfAction( int index,String excelPath,bool isRun ) 
            : base(index, isRun)
        {
            this.excelPath = excelPath;

            
        }

    
    
    
    }

}
