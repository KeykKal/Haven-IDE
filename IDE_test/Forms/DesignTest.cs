using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDE_test
{
    public partial class DesignTest : Form
    {
        List<Design> designs = new List<Design>();

        public DesignTest()
        {
            InitializeComponent();
            //var syntaxHighlighter = new SyntaxHighlighter(richTextBox1);
            //syntaxHighlighter.AddPattern(new PatternDefinition("for", "foreach", "int", "var"), new SyntaxStyle(Color.Blue));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Design design = new Design(designs);
            design.OpenCodeTab(tabControl1, design.codeTextBox);
            design.isSaved = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void DesignTest_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        //close button
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
