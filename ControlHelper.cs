using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmoSearch
{
    public static class ControlHelper
    {
        public static void InvokeEx(this Control control, Action action)
        {
            if (control.InvokeRequired)// Проверка, нужно ли обращаться к методу Invoke
                control.Invoke(action);// Передача делегата основному потоку
            else
                action();
        }
    }
}
