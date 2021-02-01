using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IDE_test
{

    //ToDo you can manually change the textbox for the compiler path and it changes absolutly nothing so gotta fix that.
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            loadValues();
            compilerFolderPath.Text = FilePaths.CompilerFilePath;
        }

        void loadValues()
        {
            chk_Hook.Checked = Properties.Settings.Default.Hook;
            chk_disassemble.Checked = Properties.Settings.Default.Disassemble;
            chk_overwrite.Checked = Properties.Settings.Default.Overwrite;
            chk_Log.Checked = Properties.Settings.Default.Log;
        }

        void SaveValues()
        {
            Properties.Settings.Default.Hook = chk_Hook.Checked;
            Properties.Settings.Default.Disassemble = chk_disassemble.Checked;
            Properties.Settings.Default.Overwrite = chk_overwrite.Checked;
            Properties.Settings.Default.Log = chk_Log.Checked;
            Properties.Settings.Default.Save();
        }

        //open explorer to search for the compiler
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Select the AtlusScriptCompiler.exe .. ",
                Multiselect = false,
                Filter = "Executable (*.EXE)|*.EXE;|" +
                "All files (*.*)|*.*"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                compilerFolderPath.Clear();

                using (StreamReader sr = new StreamReader(openFile.FileName))
                {
                    compilerFolderPath.Text = openFile.FileName;
                    sr.Close();
                }
                FilePaths.CompilerFilePath = openFile.FileName;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveValues();
        }
    }
}
