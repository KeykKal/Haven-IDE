using AtlusScriptCompilerGUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

            TextBoxOutputter output = new TextBoxOutputter(this.consoleTextBox);
            Console.SetOut(output);


            Console.WriteLine("Welcome to Haven IDE!");


            gameComboBox2.DataSource = GUI.GamesDropdown;

            gameComboBox2.SelectionChangeCommitted += new EventHandler(Game_Changed);
            tabControl1.SelectedIndexChanged += TabChanged;



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

        //to get the File we want to Compile
        public void setInputFile()
        {
            //if (Compiler.DoCompile)
            if (!string.IsNullOrEmpty(designsList[tabControl1.SelectedIndex].path))
                Compiler.InputFilePath = designsList[tabControl1.SelectedIndex].path;
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 0)
                documentMap1.Target = designsList[tabControl1.SelectedIndex].codeTextBox;
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

        #region events

        // The size of the X in each tab's upper right corner.
        private int Xwid = 10;
        private const int tab_margin = 3;


        //Idea from by Tekka
        //code is still stolen cause im bad as fuck if it comes to design
        private void drawCloseButton(object sender, DrawItemEventArgs e)
        {

            Brush txt_brush, box_brush;
            Pen box_pen;

            // We draw in the TabRect rather than on e.Bounds
            // so we can use TabRect later in MouseDown.
            Rectangle tab_rect = tabControl1.GetTabRect(e.Index);

            // Draw the background.
            // Pick appropriate pens and brushes.
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), tab_rect);
                e.DrawFocusRectangle();

                txt_brush = Brushes.LightGray;
                box_brush = new SolidBrush(Color.FromArgb(50,50,50));
                box_pen = Pens.Red;
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(45, 50, 55)), tab_rect);

                txt_brush = Brushes.DarkGray;
                box_brush = new SolidBrush(Color.FromArgb(45, 50, 55));
                box_pen = Pens.Red;
            }

            // Allow room for margins.
            RectangleF layout_rect = new RectangleF(
                tab_rect.Left + tab_margin,
                tab_rect.Y + tab_margin,
                tab_rect.Width - 2 * tab_margin,
                tab_rect.Height - 2 * tab_margin);

            using (StringFormat string_format = new StringFormat())
            {
                // Draw the tab # in the upper left corner.
                //using (Font small_font = new Font(this.Font.FontFamily,
                //    6, FontStyle.Bold))
                //{
                //    string_format.Alignment = StringAlignment.Near;
                //    string_format.LineAlignment = StringAlignment.Near;
                //    e.Graphics.DrawString(
                //        e.Index.ToString(),
                //        small_font,
                //        txt_brush,
                //        layout_rect,
                //        string_format);
                //}

                // Draw the tab's text on the left ide.
                using (Font big_font = new Font(this.Font.FontFamily, 10, FontStyle.Bold))
                {
                    string_format.Alignment = StringAlignment.Near;
                    string_format.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(
                        tabControl1.TabPages[e.Index].Text,
                        big_font,
                        txt_brush,
                        layout_rect,
                        string_format);
                }

                // Draw an X in the upper right corner.
                Rectangle rect = tabControl1.GetTabRect(e.Index);
                e.Graphics.FillRectangle(box_brush,
                    layout_rect.Right - Xwid,
                    layout_rect.Top,
                    Xwid,
                    Xwid);
                e.Graphics.DrawRectangle(box_pen,
                    layout_rect.Right - Xwid,
                    layout_rect.Top,
                    Xwid,
                    Xwid);
                e.Graphics.DrawLine(box_pen,
                    layout_rect.Right - Xwid,
                    layout_rect.Top,
                    layout_rect.Right,
                    layout_rect.Top + Xwid);
                e.Graphics.DrawLine(box_pen,
                    layout_rect.Right - Xwid,
                    layout_rect.Top + Xwid,
                    layout_rect.Right,
                    layout_rect.Top);

            }
        }
        private void TabControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                // Get the TabRect plus room for margins.
                Rectangle tab_rect = tabControl1.GetTabRect(i);
                RectangleF rect = new RectangleF(
                    tab_rect.Left + tab_margin,
                    tab_rect.Y + tab_margin,
                    tab_rect.Width - 2 * tab_margin,
                    tab_rect.Height - 2 * tab_margin);
                if (e.X >= rect.Right - Xwid &&
                    e.X <= rect.Right &&
                    e.Y >= rect.Top &&
                    e.Y <= rect.Top + Xwid)
                {
                    //Console.WriteLine("Tab " + i);
                    //tabControl1.TabPages.RemoveAt(i);
                    closeTab();
                    return;
                }
            }
        }
        #endregion


        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Design design = new Design(designsList);
            design.createNewFile(tabControl1);
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
                    openTheFile(openFile.FileName);
                }
                else
                {
                    MessageBox.Show("Wrong File Typ" + "has the following ext (" + fileExt + ").");
                }
            }
        }

        //originally created to open the file after it is decompiled but now it just looks cleaner
        public void openTheFile(string fileName)
        {
            Design design = new Design(designsList);
            using (StreamReader sr = new StreamReader(fileName))
            {
                //MessageBox.Show(openFile.FileName);
                design.openFile(tabControl1, fileName, sr.ReadToEnd());
                //richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Code.Save(designsList[tabControl1.SelectedIndex]);
        }

        //NOOO!!!
        void onChangeTab(Object sender, EventArgs e)
        {
            //MessageBox.Show(tabControl1.SelectedIndex.ToString());
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
            //DesignTest designTest = new DesignTest();
            //designTest.ShowDialog();
        }

        private void test_IDE_Load(object sender, EventArgs e)
        {
            Design design = new Design(designsList);
            design.createNewFile(tabControl1);
            if (tabControl1.TabCount > 0)
                documentMap1.Target = designsList[tabControl1.SelectedIndex].codeTextBox;


            //Stolen code to design the tabcontrol

            // We will draw the tabs.
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;

            // SizeMode must be Fixed to change tab size.
            tabControl1.SizeMode = TabSizeMode.Fixed;

            // Set the size for the tabs.
            Size tab_size = tabControl1.ItemSize;
            tab_size.Width = 100;
            tab_size.Height += 6;
            tabControl1.ItemSize = tab_size;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            closeTab();
        }

        void closeTab()
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
            consoleTextBox.Text = "";
            if (Code.Save(designsList[tabControl1.SelectedIndex]))
            {
                setInputFile();
                string[] file = { designsList[tabControl1.SelectedIndex].path };
                Code.Compile(file);
            }
        }

        private void decompileButton_Click(object sender, EventArgs e)
        {
            consoleTextBox.Text = "";
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
            //MessageBox.Show(designsList[tabControl1.SelectedIndex].path);
            
            //doesn't rn
            //Code.StartGame(FilePaths.ReloadedPath, FilePaths.GameFilePath, designsList[tabControl1.SelectedIndex].path);
        }

        private void collabsAll_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.CollapseAllFoldingBlocks();
        }

        private void expandAllBlocks_Click(object sender, EventArgs e)
        {
            designsList[tabControl1.SelectedIndex].codeTextBox.ExpandAllFoldingBlocks();
        }

        private void consoleTextBox_TextChanged(object sender, EventArgs e)
        {
            consoleTextBox.SelectionStart = consoleTextBox.Text.Length;
            consoleTextBox.ScrollToCaret();
        }

        private void showHideConsoleButton_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
        }

        private void showHideDocumentMapButton_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }
    }
}
