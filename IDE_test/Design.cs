using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using AtlusScriptLibrary.Common.Libraries;

namespace IDE_test
{
    //Todo sort this fucking code
    class Design
    {
        #region vars
        public string path = ""; //TODO: save the file path in here | it should work
      
        /// <summary>
        /// determin if the file (the opened tab) is saved
        /// </summary>
        public bool isSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                if (value)
                {
                    //is saved
                    ValuesSaved();
                }
                else
                {
                    //is not saved
                    ValuesChanged();
                }
                _isSaved = value;
            }
        }
        bool _isSaved = true;
        /// <summary>
        /// the selected games name used to get the librarys name
        /// </summary>
        string game;

        public string tabTitle { get; set; }
        #endregion

        /// <summary>
        /// a static list to hold track of the tabs
        /// </summary>
        public static List<Design> designList = new List<Design>();
        public Design(List<Design> ts)
        {
            ts.Add(this);
            foreach(var t in ts.ToArray())
            {
                if(!designList.Contains(t))
                    designList.Add(t);
            }


            this.codeTextBox.Clear();
            this.codeTextBox.TextChanged += onTextChanged;
            this.codeTextBox.ToolTipNeeded += /*new EventHandler<ToolTipNeededEventArgs>*/(this.ToolTipIsNeeded);
            this.codeTextBox.KeyDown += codeBox_KeyDown;

            popupMenu = new AutocompleteMenu(codeTextBox)
            {
                AutoSize = false,
                ForeColor = Color.White,
                //MaximumSize = new Size(200, 300),
                Width = 5000,
                BackColor = Color.FromArgb(55, 55, 55),
                SelectedColor = Color.DeepSkyBlue,
                SearchPattern = @"[\w\.:=!<>]",
                AllowTabKey = true,
                AlwaysShowTooltip = true,
                ToolTipDuration = 5000,
            };

            setUpAutoComplete();
        }

        #region tab
        public TabControl tabControl { get; private set; }
        TabPage tabPage = new TabPage()
        {
            //BackColor = Color.FromArgb(55, 55, 55),
            ForeColor = System.Drawing.SystemColors.Control,
            Dock = System.Windows.Forms.DockStyle.Fill,
            Location = new System.Drawing.Point(0, 24),
            BorderStyle = System.Windows.Forms.BorderStyle.None,
            Size = new System.Drawing.Size(786, 416),
            TabIndex = 0,
        };
        #endregion

        //ToDo popup only show up if you write at least 2 letters so make that 1 (I mean still addable)
        #region Auto Complete

        static AutocompleteMenu popupMenu;
        
        /// <summary>
        /// Sets uo the Auto complete option
        /// </summary>
        public void setUpAutoComplete()
        {
            //for the flow script (tbh i just copy pasted this one this is way more time efficent)
            string[] keywords = {/*Added by me (FLOW script exclusive)*/ "import",  /*This part was copy pasted*/"abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
            string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
            string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do${\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
            string[] declarationSnippets = {
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
            };
            //To add for MSG "scripts" can i call it a script? like is it one?? WTH IS IT IF ITS NOT A SCRIPT

            //if (FilePaths.LibraryPath == null)
            //{
            //    MessageBox.Show("[ERROR] Library Path is set to null");
            //    return;
            //}

            var functionNameList = new List<string>();

            game = FilePaths.SelectedGamePath();
            switch (game)
            {
                case "SMT Digital Devil Saga":
                    game = "Digital Devil Saga"; //or dds
                    break;
                case "SMT 3 Nocturne":
                    game = "Shin Megami Tensei III: Nocturne"; // or smt3
                    break;
            }
            var library = LibraryLookup.GetLibrary(game);
            if (library == null)
            {
                MessageBox.Show("[ERROR] Couldn't find library folder please make sure that it is inside of the .exe folder");
                return;
            }
            for (int i = 0; i < library.FlowScriptModules.Count; i++)
            {
                for (int j = 0; j < library.FlowScriptModules[i].Functions.Count; j++)
                {
                    functionNameList.Add(library.FlowScriptModules[i].Functions[j].Name);
                }
            }


            List<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var item in functionNameList)
                items.Add(new AutocompleteItem(item) { ToolTipTitle = item, ToolTipText = ToolTip(item, library) });
            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2, ToolTipText = "WALUIGIIIIIIIIIIIIII" });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item) { });



            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            popupMenu.Items.SetAutocompleteItems(items);
        }
        
        /// <summary>
        /// to update the auto complete (not very opitimal)
        /// </summary>
        public void UpdateAutoComplete()
        {
            setUpAutoComplete();
        }

        #endregion


        #region Tooltip
        /// <summary>
        /// reads the json files in the Libraries folder to give them a tooltip
        /// </summary>
        /// <param name="word">the word that should get the tooltip</param>
        /// <param name="library">from which library is it</param>
        /// <returns></returns>
        private string ToolTip(string word, Library library)
        {

            string toolTip = "";
            for (int i = 0; i < library.FlowScriptModules.Count; i++)
            {
                for (int j = 0; j < library.FlowScriptModules[i].Functions.Count; j++)
                {
                    //MessageBox.Show(library.FlowScriptModules[i].Functions[j].Description);

                    if (library.FlowScriptModules[i].Functions[j].Name == word)
                    {

                        string description = library.FlowScriptModules[i].Functions[j].Description;
                        string returnTyp = library.FlowScriptModules[i].Functions[j].ReturnType;
                        int index = library.FlowScriptModules[i].Functions[j].Index;
                        var parameters = library.FlowScriptModules[i].Functions[j].Parameters;

                        toolTip = Code.addToString(toolTip, "Description: " + description + "\n");
                        toolTip = Code.addToString(toolTip, "ReturnTyp: " + returnTyp + "\n");
                        toolTip = Code.addToString(toolTip, "Index: " + index);

                        foreach (var parameter in parameters)
                        {
                            toolTip = Code.addToString(toolTip, "\n//////\n");
                            toolTip = Code.addToString(toolTip, "Name: " + parameter.Name + ": \n");
                            toolTip = Code.addToString(toolTip, "Type: " + parameter.Type + "\n");
                            toolTip = Code.addToString(toolTip, "Description: " + parameter.Description + "\n");
                        }
                        toolTip = Code.addToString(toolTip, "\n /////////////////");
                        return toolTip;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// called with the event and is called everytime a tooltip is called (mouse hovers over a method)
        /// </summary>
        private void ToolTipIsNeeded(object sender, ToolTipNeededEventArgs e)
        {
            if (String.IsNullOrEmpty(e.HoveredWord)) return;

            var library = LibraryLookup.GetLibrary(game);

            var range = new FastColoredTextBoxNS.Range(sender as FastColoredTextBox, e.Place, e.Place);
            string hoveredWord = range.GetFragment("[^ ( \n \r ]").Text;

            e.ToolTipTitle = hoveredWord;

            for (int i = 0; i < library.FlowScriptModules.Count; i++)
            {
                for (int j = 0; j < library.FlowScriptModules[i].Functions.Count; j++)
                {
                    e.ToolTipText = ToolTip(hoveredWord, library) + "'";
                }
            }

        }

        #endregion


        #region codeTextBox
        public FastColoredTextBox codeTextBox = new FastColoredTextBox()
        {
            BackColor = Color.FromArgb(55, 50, 45),
            ForeColor = System.Drawing.SystemColors.Control,
            AcceptsTab = true,
            Dock = System.Windows.Forms.DockStyle.Fill,
            Location = new System.Drawing.Point(0, 24),
            BorderStyle = System.Windows.Forms.BorderStyle.None,
            Size = new System.Drawing.Size(786, 416),
            TabIndex = 0,
            //ZoomFactor = 1.7F,
            Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
            Text = "",
            IndentBackColor = Color.FromArgb(40, 40, 40),
            LineNumberColor = Color.MediumSpringGreen
        };
        private void codeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Space | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                popupMenu.Show(true);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                popupMenu.Close();
            }
            if (e.KeyData == (Keys.K | Keys.Control))
            {
                codeTextBox.InsertLinePrefix("//");
            }
            if (e.KeyData == (Keys.K | Keys.Control | Keys.Shift))
            {
                codeTextBox.RemoveLinePrefix("//");
            }
            if (e.KeyData == (Keys.Control | Keys.P))
            {
                codeTextBox.CollapseAllFoldingBlocks();
                codeTextBox.ExpandAllFoldingBlocks();
            }
        }
        #endregion


        #region delete/remove Tabs

        /// <summary>
        /// removes/closes this tab
        /// </summary>
        /// <param name="ts">List of tabs</param>
        /// <param name="control"></param>
        public void RemoveTab(List<Design> ts, TabControl control)
        {
            if (this.isSaved)
            {
                switch (MessageBox.Show("Are you sure you want to close " + this.tabTitle + " ?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        control.TabPages.Remove(control.SelectedTab);
                        ts.Remove(this);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            else
            {
                switch (MessageBox.Show("Do you want save " + this.tabTitle + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Yes:
                        Code.Save(this);
                        control.TabPages.Remove(control.SelectedTab);
                        ts.Remove(this);
                        break;
                    case DialogResult.No:
                        control.TabPages.Remove(control.SelectedTab);
                        ts.Remove(this);
                        break;
                }
            }
        }

        /// <summary>
        /// Remove all the tabs
        /// </summary>
        /// <param name="ts">List with the tabs</param>
        /// <param name="e"></param>
        public static void RemoveAll(List<Design> ts, FormClosingEventArgs e)
        {
            foreach (var design in ts.ToArray())
            {
                if (!design.isSaved)
                {
                    switch (MessageBox.Show("Do you want save " + design.tabTitle + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            Code.Save(design);
                            design.tabControl.TabPages.Remove(design.tabControl.SelectedTab);
                            ts.Remove(design);
                            break;
                        case DialogResult.No:
                            design.tabControl.TabPages.Remove(design.tabControl.SelectedTab);
                            ts.Remove(design);
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;

                    }
                }
            }
            ts.Clear();
        }

        #endregion

        #region Value settings
        /// <summary>
        /// everytime the text is changed in a way used to change the name with an * at the moment
        /// </summary>
        void ValuesChanged()
        {
            string newTitle = "*" + tabTitle;
            if (!tabPage.Text.Contains("*"))
                tabPage.Text = newTitle;
        }

        /// <summary>
        /// called when the tab is saved
        /// </summary>
        void ValuesSaved()
        {
            if (tabPage.Text.Contains("*"))
                tabPage.Text = tabTitle;
        }

        /// <summary>
        /// to update values if they are changed
        /// </summary>
        public void updateValues()
        {
            this.tabPage.Text = this.tabTitle;
        }
        

        /// <summary>
        /// called when the text changes
        /// </summary>
        public void onTextChanged(object sender, TextChangedEventArgs e)
        {
            isSaved = false;
            syntaxHighlights(e, this.codeTextBox);
        }
        #endregion

        #region new Tabs/Files

        /// <summary>
        /// called to open a tab with the code textbox
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="rchtext"></param>
        /// <returns></returns>
        public TabPage OpenCodeTab(TabControl tabControl, FastColoredTextBox rchtext)
        {
            string title = "Tab " + (tabControl.TabCount + 1).ToString();
            if (tabTitle == null)
                tabTitle = title;

            this.tabControl = tabControl;
            updateValues();

            tabPage.Controls.Add(rchtext);
            //tabPage.Controls.Add(splitter);
            //tabPage.Controls.Add(documentMap);

            tabControl.TabPages.Add(tabPage);
            return tabPage;
        }


        /// <summary>
        /// called when creating a file
        /// </summary>
        /// <param name="tabControl"></param>
        public void createNewFile(TabControl tabControl)
        {
            TabPage newTab = OpenCodeTab(tabControl, this.codeTextBox);
            tabControl.SelectedTab = newTab;
        }
        /// <summary>
        /// called to open a existing file
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="filePath"></param>
        /// <param name="fileInfo"></param>
        public void openFile(TabControl tabControl, string filePath, string fileInfo)
        {
            this.path = filePath;
            this.tabTitle = Path.GetFileName(filePath);
            this.codeTextBox.Text = fileInfo;
            TabPage newTab = OpenCodeTab(tabControl, this.codeTextBox);
            tabControl.SelectedTab = newTab;
            this.isSaved = true;
            updateValues();
            //Code.Save(this);
        }
        #endregion



        #region sytax Highlighting


        //ToDo make this one customizable
        //styles
        static TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        static TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        static TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        static MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        //Brushes.OrangeRed also looks great
        //BUGGGG
        static TextStyle NumberStyl = new TextStyle(new SolidBrush(Properties.Settings.Default.NumberColor), null, FontStyle.Regular); //check
        static TextStyle Styl = new TextStyle(new SolidBrush(Properties.Settings.Default.baseColor), null, FontStyle.Regular); //check
        static TextStyle singelLineCommentStyl = new TextStyle(new SolidBrush(Properties.Settings.Default.singelLineCommentColor), null, FontStyle.Regular); //check
        static TextStyle MultiLineCommentStyl = new TextStyle(new SolidBrush(Properties.Settings.Default.MultiLineCommentColor), null, FontStyle.Regular); //check
        static TextStyle stringstyl = new TextStyle(new SolidBrush(Properties.Settings.Default.stringColor), null, FontStyle.Regular); //check
        static TextStyle MSGStyl = new TextStyle(new SolidBrush(Properties.Settings.Default.MSGColor), null, FontStyle.Regular);//check
        static TextStyle FunctionNameStyle = new TextStyle(new SolidBrush(Properties.Settings.Default.MSGColor), null, FontStyle.Regular);//well sort of

        /// <summary>
        /// create and update the syntax highlighting
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fctb"></param>
        public static void syntaxHighlights(TextChangedEventArgs e, FastColoredTextBox fctb)
        {
            NumberStyl.ForeBrush = new SolidBrush(Properties.Settings.Default.NumberColor);
            Styl.ForeBrush = new SolidBrush(Properties.Settings.Default.baseColor);
            singelLineCommentStyl.ForeBrush = new SolidBrush(Properties.Settings.Default.singelLineCommentColor);
            MultiLineCommentStyl.ForeBrush = new SolidBrush(Properties.Settings.Default.MultiLineCommentColor);
            stringstyl.ForeBrush = new SolidBrush(Properties.Settings.Default.stringColor);
            MSGStyl.ForeBrush = new SolidBrush(Properties.Settings.Default.MSGColor);
            FunctionNameStyle.ForeBrush = new SolidBrush(Properties.Settings.Default.MSGColor);

            fctb.LeftBracket = '(';
            fctb.RightBracket = ')';
            fctb.LeftBracket2 = '\x0';
            fctb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(Styl, BoldStyle, stringstyl, NumberStyl, singelLineCommentStyl, MultiLineCommentStyl, MSGStyl);

            //fctb.ClearStylesBuffer();
            //fctb.Range.ClearStyle(Styl, BoldStyle, stringstyl, NumberStyl, singelLineCommentStyl, MultiLineCommentStyl);


            //comment highlighting
            e.ChangedRange.SetStyle(singelLineCommentStyl, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(MultiLineCommentStyl, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(MultiLineCommentStyl, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);

            //string highlighting
            e.ChangedRange.SetStyle(stringstyl, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");

            //number highlighting
            e.ChangedRange.SetStyle(NumberStyl, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");

            //attribute highlighting
            e.ChangedRange.SetStyle(MSGStyl, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);

            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");

            //keyword highlighting
            e.ChangedRange.SetStyle(Styl, @"\b(import|abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            foreach (FastColoredTextBoxNS.Range found in fctb.GetRanges(@"\b(void|DEFUN)\s+(?<range>\w+)\b"))
                fctb.Range.SetStyle(FunctionNameStyle, @"\b" + found.Text + @"\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }
        #endregion
    }
}