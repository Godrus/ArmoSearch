using ArmoSearch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmoSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtPath.Text = Settings.Default["startPath"].ToString();
            txtFileName.Text = Settings.Default["fileNameMask"].ToString();
        }
        private void SaveSettings()
        {
            Settings.Default["startPath"] = txtPath.Text;
            Settings.Default["fileNameMask"] = txtFileName.Text;
            Settings.Default.Save();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            GUIController GUI = new GUIController();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnStopCon_Click(object sender, EventArgs e)
        {

        }
    }
}
