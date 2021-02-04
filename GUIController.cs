using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmoSearch
{
    public static class GUIController
    {
        private static Form1 _instance;

        public static void SetForm(Form1 f) { _instance = f; }

        public static void UpdateCurrentFolderLabel(string folderName)
        {
            Label label = (Label)_instance.Controls["groupBox4"].Controls["lblCurFolder"];
            ControlHelper.InvokeEx(label, new Action(() => label.Text = "Current folder: " + folderName));
        }

        public static void UpdateTotalFilesLabel(int filesCount)
        {
            Label label = (Label)_instance.Controls["groupBox4"].Controls["lblTotal"];
            ControlHelper.InvokeEx(label, new Action(() => label.Text = "Total files: " + filesCount.ToString()));
        }

        public static void UpdateFoundFilesLabel(int filesFoundCount)
        {
            Label label = (Label)_instance.Controls["groupBox4"].Controls["lblFound"];
            ControlHelper.InvokeEx(label, new Action(() => label.Text = "Found files: " + filesFoundCount.ToString()));
        }
        public static void UpdateTimeSearchLabel(int time)
        {
            Label label = (Label)_instance.Controls["groupBox4"].Controls["lblTime"];
            ControlHelper.InvokeEx(label, new Action(() => label.Text = "Time search: " + time.ToString() + " ms"));
        }

        public static void UpdateStopConButtonEnabled(bool enabled)
        {
            Button button = (Button)_instance.Controls["groupBox3"].Controls["btnStopCon"];
            ControlHelper.InvokeEx(button, new Action(() => button.Enabled = enabled));
        }

        public static void UpdateStopConButtonText(string newText)
        {
            Button button = (Button)_instance.Controls["groupBox3"].Controls["btnStopCon"];
            ControlHelper.InvokeEx(button, new Action(() => button.Text = newText));
        }


    }
}
