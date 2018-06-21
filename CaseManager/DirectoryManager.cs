using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CaseManager
{
    class DirectoryManager
    {
        public void clearDir(String path)
        {
            DirectoryInfo direct = new DirectoryInfo(path);

            FileInfo[] files = direct.GetFiles();

            foreach (FileInfo fInfo in files)
            {
                fInfo.Delete();
            }


        }



        public List<String> getAllFiles(String path ,String fileFormat)
        {
            getAllFile2(path ,fileFormat);
            return list;
        }




        private List<String> list = new List<string>();

        private void getAllFile2(String path ,String fileFormat)
        {
            DirectoryInfo direct = new DirectoryInfo(path);

            DirectoryInfo[] dirs = direct.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {
                getAllFile2(dir.FullName ,fileFormat);
            }

            FileInfo[] files = direct.GetFiles("*." + fileFormat);


            foreach (FileInfo f in files)
            {
                list.Add(f.FullName);
            }

        }
    
    
    }
}
