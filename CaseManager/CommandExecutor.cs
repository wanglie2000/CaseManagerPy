using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace CaseManager
{
    class CommandExecutor
    {
        

        public void execute(String command)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";


            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;

            Process p = Process.Start(info);

            p.StandardInput.WriteLine(command + "&exit");
            p.StandardInput.AutoFlush = true;


            String oo =  p.StandardOutput.ReadToEnd();
            String ee = p.StandardError.ReadToEnd();

            ////Console.WriteLine(oo);
            ////Console.WriteLine(ee);

            p.WaitForExit();
            p.StandardOutput.Close();
            p.StandardInput.Close();
            p.Close();
        }

        public void execute2(String pyFile)
        {
            ProcessStartInfo info = new ProcessStartInfo();

            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.FileName = "python.exe";
            info.Arguments = pyFile;


            Process p = Process.Start(info);
            p.Start();
            
            while (!p.StandardOutput.EndOfStream)
            {
                String tmp = p.StandardOutput.ReadLine();
                ////////Console.WriteLine(tmp);
            }

            p.StandardOutput.Close();
            p.StandardInput.Close();
            p.Close();


        }

    }
}
