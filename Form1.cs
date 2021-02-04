using ArmoSearch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmoSearch
{
    public partial class Form1 : Form
    {
        private int time;
        private readonly ThreadJob threadJob = new ThreadJob();
        public Form1()
        {
            InitializeComponent();
            txtPath.Text = Settings.Default["startPath"].ToString();
            txtFileName.Text = Settings.Default["fileNameMask"].ToString();
            GUIController.SetForm(this);
        }

        private void SaveSettings()
        {
            Settings.Default["startPath"] = txtPath.Text;
            Settings.Default["fileNameMask"] = txtFileName.Text;
            Settings.Default.Save();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            fbDialog.ShowDialog();
            txtPath.Text = fbDialog.SelectedPath;
            SaveSettings();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GUIController.UpdateStopConButtonEnabled(true);
            GUIController.UpdateStopConButtonText("Pause");

            threadJob.Start(treeView, txtPath.Text, txtFileName.Text);
            timerSearch.Start();
            SaveSettings();
        }

        private void btnStopCon_Click(object sender, EventArgs e)
        {
            if (threadJob.ThreadWork())
            {
                threadJob.Continue();
                GUIController.UpdateStopConButtonText("Pause");
                timerSearch.Start();
            }
            else
            {
                threadJob.Stop();
                GUIController.UpdateStopConButtonText("Continue");
                timerSearch.Stop();
            }
        }

        private void timerSearch_Tick(object sender, EventArgs e)
        {
            threadJob.ThreadTimeWork();
            if (threadJob.ThreadCurrentState() == System.Threading.ThreadState.Stopped)
            {
                timerSearch.Stop();
                GUIController.UpdateStopConButtonEnabled(false);
                MessageBox.Show("Поиск завершен");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            threadJob.End();
        }
    }
}
