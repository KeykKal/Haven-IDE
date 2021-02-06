﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using AtlusScriptCompilerGUI;

namespace IDE_test
{

    /*
     * Ok so everything works up until this point the finishing touch would be to 
     * to add the compiling make it so that it won't try to decompile .FLOW and .MSG files
     * yea the rest is desin (don't forget to add everything inside of the GUI like a togglable console Log) and up to you.
     */

    public partial class test_IDE : Form
    {
        List<Design> designsList = new List<Design>();

        public test_IDE()
        {
            InitializeComponent();

            gameComboBox2.DataSource = GUI.GamesDropdown;

            gameComboBox2.SelectionChangeCommitted += new EventHandler(Game_Changed);

            if (!File.Exists("Game.txt"))
                File.Create("Game.txt");

            try
            {
                gameComboBox2.SelectedIndex = GUI.GamesDropdown.IndexOf(File.ReadAllLines("Game.txt")[0]);
                FilePaths.selectedGame = gameComboBox2.SelectedIndex;
            }
            catch { }

            //tabControl1.SelectedIndexChanged += onChangeTab;
        }

        private void Game_Changed(object sender, EventArgs e)
        {
            if (gameComboBox2.SelectedIndex != 0)
            {
                File.WriteAllText("Game.txt", gameComboBox2.SelectedItem.ToString());
                FilePaths.selectedGame = gameComboBox2.SelectedIndex;
            }
            designsList[tabControl1.SelectedIndex].UpdateAutoComplete();
        }


        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Design design = new Design(designsList);
            design.createNewFile(tabControl1, design.codeTextBox);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open a new file.. ",
                Multiselect = true,
                Filter = "DataType (*.BF;*.FLOW;*.MSG,*.BMD)|*.BF;*.FLOW;*.MSG;*.BMG|" +
                "All files (*.*)|*.*"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //this works
                string fileExt = Path.GetExtension(openFile.FileName);

                fileExt = fileExt.ToUpper();
                //richTextBox1.Clear();

                //this one gives me a bug i guess
                if (fileExt == ".BF" || fileExt == ".BMD")
                {
                    Code.Decompile(openFile.FileNames);
                }
                else if (fileExt == ".FLOW" || fileExt == ".MSG")
                {
                    Design design = new Design(designsList);
                    using (StreamReader sr = new StreamReader(openFile.FileName))
                    {
                        //MessageBox.Show(openFile.FileName);
                        design.openFile(tabControl1, design.codeTextBox, openFile.FileName, sr.ReadToEnd());
                        //richTextBox1.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong File Typ" + "has the following ext (" + fileExt + ").");
                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Code.Save(designsList[tabControl1.SelectedIndex]);
        }

        void onChangeTab(Object sender, EventArgs e)
        {
            MessageBox.Show(tabControl1.SelectedIndex.ToString());
        }

        #region Not important

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Paste();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Undo();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Redo();
        }

        private void selectAll()
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.SelectAll();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripButton_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton.PerformClick();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripButton.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripButton.PerformClick();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripButton.PerformClick();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteToolStripButton.PerformClick();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAll();
        }

        private void settingsButon_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
        #endregion


        private void TestWindowButton_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show(File.ReadAllLines("Game.txt")[0]);
            DesignTest designTest = new DesignTest();
            designTest.ShowDialog();
        }

        private void test_IDE_Load(object sender, EventArgs e)
        {
            Design design = new Design(designsList);
            design.createNewFile(tabControl1, design.codeTextBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (designsList.Count > 0) designsList[tabControl1.SelectedIndex].RemoveTab(designsList, tabControl1);
            if (designsList.Count == 0) this.Close();
        }

        private void test_IDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Design.RemoveAll(designsList, e);
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            if (Code.Save(designsList[tabControl1.SelectedIndex]))
            {
                string[] file = { designsList[tabControl1.SelectedIndex].path };
                Code.Compile(file);
            }
        }

        private void decompileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Select .BF or .BMG file.. ",
                Multiselect = true,
                Filter = "DataType (*.BF;*.BMD)|*.BF;*.BMG|" +
                "All files (*.*)|*.*"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //this works
                string fileExt = Path.GetExtension(openFile.FileName);

                fileExt = fileExt.ToUpper();
                //richTextBox1.Clear();
                if (fileExt == ".BF" || fileExt == ".BMD")
                {
                    Code.Decompile(openFile.FileNames);
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void playButton_T_Click(object sender, EventArgs e)
        {
            Design design = new Design(designsList);
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open a new file.. ",
                Multiselect = true,
                //Filter = "DataType (*.BF;*.FLOW;*.MSG,*.BMD)|*.BF;*.FLOW;*.MSG;*.BMG|" +
                //"All files (*.*)|*.*"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {

                Code.AutoCompile(openFile.FileName);
            }
        }

        private void collabsAll_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.CollapseAllFoldingBlocks();
        }

        private void expandAllBlocks_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.ExpandAllFoldingBlocks();
        }
    }
}
