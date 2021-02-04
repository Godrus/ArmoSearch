using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmoSearch
{
    class ThreadJob
    {
        private static bool stopRunning = false;

        private string fileNameMask;

        private int numOfFound;
        private int numOfAll;

        private TreeView tView;

        private Thread thread;

        System.Diagnostics.Stopwatch stopwatch;

        public bool ThreadWork()
        {
            return stopRunning;
        }

        public ThreadState ThreadCurrentState()
        {
            if (thread == null) return ThreadState.Aborted;
            else return thread.ThreadState;
        }

        public void ThreadTimeWork()
        {
            GUIController.UpdateTimeSearchLabel((int)stopwatch.Elapsed.TotalMilliseconds);
        }

        public void Start(TreeView _tView, string rootDir, string _mask)
        {
            End();
            stopwatch = new System.Diagnostics.Stopwatch(); 
            DirectoryInfo dir = new DirectoryInfo(rootDir);
            TreeNode node = new TreeNode(dir.Name, 0, 0);

            if (dir.Exists)
            {
                Continue();
                numOfAll = 0; GUIController.UpdateTotalFilesLabel(numOfAll);
                numOfFound = 0; GUIController.UpdateFoundFilesLabel(numOfFound);
                fileNameMask = _mask;
                tView = _tView;

                tView.Nodes.Clear();
                tView.Nodes.Add(node);

                thread = new Thread(() => Work(dir, node));
                thread.Start();
            }
            else
            {
                MessageBox.Show("Проверьте парвильность введённых данных","Ошибка");
            }
        }

        public void Stop() { stopRunning = true; stopwatch.Stop();}

        public void Continue() { stopRunning = false; stopwatch.Start();}

        private void Work(DirectoryInfo rootDir, TreeNode rootNode)
        {
            try
            {
                while (stopRunning) { } // Контроль остановки поиска

                TreeNode node;
                Regex regex = new Regex(fileNameMask);
                DirectoryInfo[] subDirs = rootDir.GetDirectories();
                GUIController.UpdateCurrentFolderLabel(rootDir.Name);

                // Проход всех подпапок в главной папке
                foreach (DirectoryInfo dir in subDirs)
                {
                    node = new TreeNode(dir.Name, 0, 0) { Tag = rootDir };
                    if (subDirs.Length != 0)
                    {
                        Work(dir, node);
                    }
                    ControlHelper.InvokeEx(tView, new Action(() => rootNode.Nodes.Add(node)));
                }

                // Проход по всем полученным файлам и применение к ним регекса
                foreach (FileInfo file in rootDir.GetFiles())
                {
                    if (regex.Match(file.Name).Success)
                    {
                        numOfFound++;
                        node = new TreeNode(file.Name, 1, 1);
                        ControlHelper.InvokeEx(tView, new Action(() => rootNode.Nodes.Add(node)));
                        GUIController.UpdateFoundFilesLabel(numOfFound);
                    }
                    numOfAll++;
                    GUIController.UpdateTotalFilesLabel(numOfAll);
                }
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException) return;
            }
        }

        public void End()
        {
            try
            {
                if (thread != null)
                {
                    if (thread.IsAlive || thread.ThreadState == ThreadState.Running)
                    {
                        thread.Abort();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
