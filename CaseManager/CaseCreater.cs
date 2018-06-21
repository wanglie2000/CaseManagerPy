using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CaseManager
{
    class CaseCreater
    {
        public void createpy(String casefile, String templatefile)
        {
            String scriptfile = casefile.Replace(".py", "_.py");

            Dictionary<String, String> dic = getAllModuleStr(casefile);

            replace(templatefile, scriptfile, dic);

        
        }




        public void replace(String templatefile, String tofile, Dictionary<String, String> dic)
        {
            StreamReader sr = new StreamReader(templatefile, Encoding.Default);
            String tmp = sr.ReadToEnd();
            sr.Close();

            foreach (String key in dic.Keys)
            {
                tmp = tmp.Replace("$" + key + "$", dic[key]);
            }
            StreamWriter sw = new StreamWriter(tofile, false, Encoding.Default);
            sw.WriteLine(tmp);
            sw.Close();
        }




        //将一个用例文件中的各个部分取出来
        public  Dictionary<String, String> getAllModuleStr(String filepath)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(filepath, Encoding.Default);
            String str = sr.ReadToEnd();
            sr.Close();
            String[] tmps = str.Split(new String[] { "def " }, StringSplitOptions.RemoveEmptyEntries  );
            foreach( String tmp in tmps )
            {
                String[] tmps2 = tmp.Split(new String[] { "():\r\n" }, StringSplitOptions.None);
                
                String moduleName = tmps2[0];
                String moduleStr = tmps2[1];
                dic.Add(moduleName, moduleStr);
                 
                //////////Console.WriteLine();

            }
            return dic;
        }
    }

    
}
