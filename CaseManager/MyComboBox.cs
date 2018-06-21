using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaseManager
{
    class MyComboBox:ComboBox
    {
        
        protected override void WndProc(ref Message m)
        {
            int WM_MOUSEWHEEL = 0x020A;
            if (m.Msg == WM_MOUSEWHEEL)
            { 
            
            }
            else
                base.WndProc(ref m);
        }
        private void MyComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                e.Handled = true;
            }
        }
        public MyComboBox():base() 
        {
            //Console.WriteLine("MyComboBox");
           this.KeyDown+=new KeyEventHandler(MyComboBox_KeyDown);
        }

    }
}
