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
using FastColoredTextBoxNS;

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

            //color buttons
            baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            nummberStyColorButton.BackColor = Properties.Settings.Default.NumberColor;
            stringStyColorButton.BackColor = Properties.Settings.Default.stringColor;
            singelCommentStyColorButton.BackColor = Properties.Settings.Default.singelLineCommentColor;
            multiCommentStyColorButton.BackColor = Properties.Settings.Default.MultiLineCommentColor;
            MSGStyColorButton.BackColor = Properties.Settings.Default.MSGColor;
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

        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.White; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.White; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.White; }
            }
        }


        private void baseStylColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;
            colorPicker.Color = Properties.Settings.Default.baseColor;
            // Update the text box color if the user clicks OK 


            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.baseColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }

                Properties.Settings.Default.Save();
                baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            }
        }

        private void nummberStyColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;
            colorPicker.Color = Properties.Settings.Default.NumberColor;
            
            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.NumberColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }

                Properties.Settings.Default.Save();
                nummberStyColorButton.BackColor = Properties.Settings.Default.NumberColor;
            }
        }

        private void stringStyColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;
            colorPicker.Color = Properties.Settings.Default.stringColor;
            
            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.stringColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }
                Properties.Settings.Default.Save();
                baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            }
        }

        private void multiCommentStyColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;
            colorPicker.Color = Properties.Settings.Default.MultiLineCommentColor;
            
            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.MultiLineCommentColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }
                Properties.Settings.Default.Save();
                baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            }
        }

        private void singelCommentStyColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;

            colorPicker.Color = Properties.Settings.Default.singelLineCommentColor;

            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.singelLineCommentColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }
                Properties.Settings.Default.Save();
                baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            }
        }

        private void MSGStyColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            colorPicker.SolidColorOnly = false;
            colorPicker.Color = Properties.Settings.Default.MSGColor;


            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.MSGColor = colorPicker.Color;
                foreach (var design in Design.designList.ToArray())
                {
                    design.codeTextBox.OnTextChanged();
                }
                Properties.Settings.Default.Save();
                baseStylColorButton.BackColor = Properties.Settings.Default.baseColor;
            }
        }
    }
}
