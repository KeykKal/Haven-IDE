using AtlusScriptCompilerGUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.Linq;
using System.Reflection;
using AtlusScriptLibrary.Common.Libraries;

namespace IDE_test
{

    //ToDo still not finished with changing the folder of the compiled Files But it is already 4 am and i should sleep. Found a new method to check if an file was changed thou
    //so that is good Update: its been 3 days and i just left it maybe ill work on it another time and if im really lucky even get it right. | to add in future compile and 
    //auto open the files that are created.
    class FilePaths
    {
        public static string CompilerFilePath
        {
            get
            {
                if (!File.Exists(Vars.PROPERTIES))
                    File.Create(Vars.PROPERTIES);

                using (StreamReader sr = new StreamReader(Vars.PROPERTIES))
                {
                    _compilerFilePath = sr.ReadLine();
                    sr.Close();
                }
                return _compilerFilePath;
            }
            set
            {
                _compilerFilePath = value;

                if (!File.Exists(Vars.PROPERTIES))
                    File.Create(Vars.PROPERTIES);

                File.OpenWrite(_compilerFilePath);

                StreamWriter output = new StreamWriter(Vars.PROPERTIES);
                output.WriteLine(_compilerFilePath);
                output.Close();
            }
        }

        private static string _compilerFilePath;

        public static string libraryPath = Path.GetDirectoryName(CompilerFilePath) + "\\Libraries";

        public static int selectedGame;
    }

    class Design
    {
        public string path = ""; //ToDo save the file path in here
        public string tabTitle { get; set; }
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
        public TabControl tabControl { get; private set; }
        static AutocompleteMenu popupMenu;

        public Design(List<Design> ts)
        {
            ts.Add(this);
            this.codeTextBox.Clear();
            this.codeTextBox.TextChanged += onTextChanged;

            popupMenu = new AutocompleteMenu(codeTextBox)
            {
                ForeColor = Color.White,
                BackColor = Color.FromArgb(55, 55, 55),
                SelectedColor = Color.DeepSkyBlue,
                SearchPattern = @"[\w\.:=!<>]",
                AllowTabKey = true,
                AlwaysShowTooltip = true,
            };
            setUpAutoComplete();
        }

        //ToDo popup only show up if you write at least 2 letters so make that 1
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

            if (FilePaths.libraryPath == null)
            {
                MessageBox.Show("[ERROR] Library Path is set to null");
                return;
            }

            var functionNameList = new List<string>();
            string game = File.ReadAllLines("Game.txt")[0];
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
                MessageBox.Show("[ERROR] Couldn't find library folder please make sure that AtlusScriptCompiler.exe was selected in the Cofingaration Settings");
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
            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));
            foreach (var item in functionNameList)
                items.Add(new AutocompleteItem(item));


            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            popupMenu.Items.SetAutocompleteItems(items);
        }

        public void UpdateAutoComplete()
        {
            setUpAutoComplete();
        }

        void updateValues()
        {
            this.tabPage.Text = this.tabTitle;
        }

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
            Text = ""
        };

        public TabPage OpenCodeTab(TabControl tabControl, FastColoredTextBox rchtext)
        {
            string title = "Tab " + (tabControl.TabCount + 1).ToString();
            if(tabTitle == null)
                tabTitle = title;

            this.tabControl = tabControl;
            updateValues();
            tabPage.Controls.Add(rchtext);
            tabControl.TabPages.Add(tabPage);
            return tabPage;
        }

        public void RemoveTab(List<Design> ts)
        {
            if (this.isSaved)
            {
                switch (MessageBox.Show("Are you sure you want to close " + this.tabTitle + " ?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        this.tabControl.TabPages.Remove(this.tabControl.SelectedTab);
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
                        this.tabControl.TabPages.Remove(this.tabControl.SelectedTab);
                        ts.Remove(this);
                        break;
                    case DialogResult.No:
                        this.tabControl.TabPages.Remove(this.tabControl.SelectedTab);
                        ts.Remove(this);
                        break;
                }
            }
        }

        public static void RemoveAll(List<Design> ts)
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
                    }
                }
            }
            ts.Clear();
        }


        void ValuesChanged()
        {
            string newTitle = "*" + tabTitle;
            if (!tabPage.Text.Contains("*"))
                tabPage.Text = newTitle;
        }

        void ValuesSaved()
        {
            if (tabPage.Text.Contains("*"))
                tabPage.Text = tabTitle;
        }

        public void createNewFile(TabControl tabControl, FastColoredTextBox rchtxt)
        {
            TabPage newTab = OpenCodeTab(tabControl, rchtxt);
            tabControl.SelectedTab = newTab;
        }

        public void openFile(TabControl tabControl, FastColoredTextBox rchtxt, string filePath, string fileInfo)
        {
            this.path = filePath;
            this.tabTitle = Path.GetFileName(filePath);
            rchtxt.Text = fileInfo;
            TabPage newTab = OpenCodeTab(tabControl, rchtxt);
            tabControl.SelectedTab = newTab;
            this.isSaved = true;
            updateValues();
            //Code.Save(this);
        }

        void onTextChanged(object sender, TextChangedEventArgs e)
        {
            isSaved = false;
            syntaxHighlights(e, this.codeTextBox);
        }

        //ToDo make this one customizable
        //styles
        static TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        static TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        static TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        static MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        //Brushes.OrangeRed also looks great
        static TextStyle Styl = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
        static TextStyle singelLineCommentStyl = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        static TextStyle MultiLineCommentStyl = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        static TextStyle NumberStyl = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        static TextStyle stringstyl = new TextStyle(Brushes.DarkSeaGreen, null, FontStyle.Regular);
        static TextStyle MSGStyl = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
        public static void syntaxHighlights(TextChangedEventArgs e, FastColoredTextBox fctb)
        {
            fctb.LeftBracket = '(';
            fctb.RightBracket = ')';
            fctb.LeftBracket2 = '\x0';
            fctb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(Styl, BoldStyle, GreenStyle, BrownStyle, stringstyl, NumberStyl, singelLineCommentStyl, MultiLineCommentStyl);

            //string highlighting
            e.ChangedRange.SetStyle(stringstyl, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(singelLineCommentStyl, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(MultiLineCommentStyl, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(MultiLineCommentStyl, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyl, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(MSGStyl, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(Styl, @"\b(import|abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }
    }

    class Code
    {
        public static void Decompile(string[] filepaths)
        {
            MessageBox.Show("Started Decompiling. this will be done in the background and could take a while");

            GUI.Hook = Properties.Settings.Default.Hook;
            GUI.Disassemble = Properties.Settings.Default.Disassemble;
            GUI.Overwrite = Properties.Settings.Default.Overwrite;
            GUI.Log = Properties.Settings.Default.Log;

            GUI.Selection = FilePaths.selectedGame;
            GUI.Decompile(filepaths, FilePaths.CompilerFilePath);
            //to move the .FLOW and .MSG files | doesn't work
            #region Move
            //        CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            //        {
            //            Title = "Choose a location to save .FLOW/.MSG data in",
            //            IsFolderPicker = true,
            //        };

            //        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //        {

            //            MessageBox.Show(dialog.FileName);
            //            for (int i = 0; i < filepaths.Length; i++)
            //            {
            //                //FileSystemWatcher fsw = new FileSystemWatcher(Path.GetDirectoryName(filepaths[i]));

            //                //new approach
            //                using (FileSystemWatcher watcher = new FileSystemWatcher())
            //                {
            //                    watcher.Path = Path.GetDirectoryName(filepaths[i]);

            //                    // Watch for changes in LastAccess and LastWrite times, and
            //                    // the renaming of files or directories.
            //                    watcher.NotifyFilter = NotifyFilters.LastAccess
            //                                         | NotifyFilters.LastWrite
            //                                         | NotifyFilters.FileName
            //                                         | NotifyFilters.DirectoryName;

            //                    // Only watch text files.
            //                    watcher.Filter = "*.FLOW";
            //                    //watcher.Filters.Add("*.FLOW");
            //                    //watcher.Filters.Add("*.MSG");

            //                    // Add event handlers.
            //                    //watcher.Changed += MoveFileToDir;
            //                    watcher.Created += MoveFileToDir;
            //                    //watcher.Deleted += MoveFileToDir;
            //                    //watcher.Renamed += OnRenamed;

            //                    // Begin watching.
            //                    watcher.EnableRaisingEvents = true;

            //                    // Wait for the user to quit the program.
            //                    //Console.WriteLine("Press 'q' to quit the sample.");
            //                    //while () ;
            //                }



            //                //old approach
            //                FileSystemWatcher msw = new FileSystemWatcher(filepaths[i] + ".flow");

            //                do
            //                {
            //                    WaitForChangedResult cr = fsw.WaitForChanged(WatcherChangeTypes.Created);


            //                    MessageBox.Show(cr.Name + " and " + cr.OldName);

            //                    if (File.Exists(dialog.FileName + "\\" + cr.Name))
            //                        File.Delete(dialog.FileName + "\\" + cr.Name);

            //                    File.Move(Path.GetDirectoryName(filepaths[i]) + "\\" + cr.Name, dialog.FileName + "\\" + cr.Name);

            //                } while ();


            //                MessageBox.Show(cr.Name);

            //                if (File.Exists(filepaths[i] + ".flow"))
            //                {
            //                    //if (File.Exists(dialog.FileName + "\\" + Path.GetFileName(filepaths[i]) + ".flow"))
            //                    //    File.Delete(dialog.FileName + "\\" + Path.GetFileName(filepaths[i]) + ".flow");

            //                    //File.Move(filepaths[i] + ".flow", dialog.FileName +"\\" + Path.GetFileName(filepaths[i]) + ".flow");
            //                }
            //                else
            //                {

            //                }

            //                if (File.Exists(filepaths[i] + ".msg"))
            //                {
            //                    if (File.Exists(dialog.FileName + "\\" + Path.GetFileName(filepaths[i]) + ".msg"))
            //                        File.Delete(dialog.FileName + "\\" + Path.GetFileName(filepaths[i]) + ".msg");

            //                    File.Move(filepaths[i] + ".msg", dialog.FileName + "\\" + Path.GetFileName(filepaths[i]) + ".msg");
            //                }
            //            }
            //      }
            #endregion
        }

        public static void Compile(string[] filepaths)
        {
            MessageBox.Show("Started Compiling. this will be done in the background and could take a while");

            GUI.Hook = Properties.Settings.Default.Hook;
            GUI.Disassemble = Properties.Settings.Default.Disassemble;
            GUI.Overwrite = Properties.Settings.Default.Overwrite;
            GUI.Log = Properties.Settings.Default.Log;

            GUI.Selection = FilePaths.selectedGame;
            GUI.Compile(filepaths, FilePaths.CompilerFilePath);
        }

        public static void CloseCurrentTab(TabControl tabControl)
        {
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        public static bool Save(Design design)
        {
            if (File.Exists(design.path))
            {
                //save in the already existing path
                StreamWriter output = new StreamWriter(design.path);
                output.Write(design.codeTextBox.Text);
                output.Close();
                design.isSaved = true;
                MessageBox.Show("Saved");
                return true;
            }
            else
            {
                if (SaveAs(design))
                    return true;

                return false;
            }
        }

        public static bool SaveAs(Design design)
        {
            //ToDo Change that
            SaveFileDialog savefile = new SaveFileDialog()
            {
                Filter = "DataType (*.BF;*.BMD;*.FLOW;*.MSG)|*.BF;*.BMG;*.FLOW;*.MSG"
            };

            savefile.Title = "Save file as.. ";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                //not sure
                design.path = savefile.FileName;
                design.tabTitle = Path.GetFileName(savefile.FileName);
                StreamWriter output = new StreamWriter(savefile.FileName);
                output.Write(design.codeTextBox.Text);
                output.Close();
                design.isSaved = true;
                return true;
            }
            return false;
        }
    
    }

    class Vars
    {
        public const string PROPERTIES = "Properties.txt";
    }

    #region not mine
    internal class DynamicCollection : IEnumerable<AutocompleteItem>
    {
        private AutocompleteMenu menu;
        private FastColoredTextBox tb;

        public DynamicCollection(AutocompleteMenu menu, FastColoredTextBox tb)
        {
            this.menu = menu;
            this.tb = tb;
        }

        public IEnumerator<AutocompleteItem> GetEnumerator()
        {
            //get current fragment of the text
            var text = menu.Fragment.Text;

            //extract class name (part before dot)
            var parts = text.Split('.');
            if (parts.Length < 2)
                yield break;
            var className = parts[parts.Length - 2];

            //find type for given className
            var type = FindTypeByName(className);

            if (type == null)
                yield break;

            //return static methods of the class
            foreach (var methodName in type.GetMethods().AsEnumerable().Select(mi => mi.Name).Distinct())
                yield return new MethodAutocompleteItem(methodName + "()")
                {
                    ToolTipTitle = methodName,
                    ToolTipText = "Description of method " + methodName + " goes here.",
                };

            //return static properties of the class
            foreach (var pi in type.GetProperties())
                yield return new MethodAutocompleteItem(pi.Name)
                {
                    ToolTipTitle = pi.Name,
                    ToolTipText = "Description of property " + pi.Name + " goes here.",
                };
        }

        Type FindTypeByName(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type type = null;
            foreach (var a in assemblies)
            {
                foreach (var t in a.GetTypes())
                    if (t.Name == name)
                    {
                        return t;
                    }
            }

            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// This item appears when any part of snippet text is typed
    /// </summary>
    class DeclarationSnippet : SnippetAutocompleteItem
    {
        public DeclarationSnippet(string snippet)
            : base(snippet)
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var pattern = Regex.Escape(fragmentText);
            if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                return CompareResult.Visible;
            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// Divides numbers and words: "123AND456" -> "123 AND 456"
    /// Or "i=2" -> "i = 2"
    /// </summary>
    class InsertSpaceSnippet : AutocompleteItem
    {
        string pattern;

        public InsertSpaceSnippet(string pattern) : base("")
        {
            this.pattern = pattern;
        }

        public InsertSpaceSnippet()
            : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (Regex.IsMatch(fragmentText, pattern))
            {
                Text = InsertSpaces(fragmentText);
                if (Text != fragmentText)
                    return CompareResult.Visible;
            }
            return CompareResult.Hidden;
        }

        public string InsertSpaces(string fragment)
        {
            var m = Regex.Match(fragment, pattern);
            if (m == null)
                return fragment;
            if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                return fragment;
            return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
        }

        public override string ToolTipTitle
        {
            get
            {
                return Text;
            }
        }
    }

    /// <summary>
    /// Inerts line break after '}'
    /// </summary>
    class InsertEnterSnippet : AutocompleteItem
    {
        Place enterPlace = Place.Empty;

        public InsertEnterSnippet()
            : base("[Line break]")
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var r = Parent.Fragment.Clone();
            while (r.Start.iChar > 0)
            {
                if (r.CharBeforeStart == '}')
                {
                    enterPlace = r.Start;
                    return CompareResult.Visible;
                }

                r.GoLeftThroughFolded();
            }

            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            //extend range
            FastColoredTextBoxNS.Range r = Parent.Fragment;
            Place end = r.End;
            r.Start = enterPlace;
            r.End = r.End;
            //insert line break
            return Environment.NewLine + r.Text;
        }

        public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
        {
            base.OnSelected(popupMenu, e);
            if (Parent.Fragment.tb.AutoIndent)
                Parent.Fragment.tb.DoAutoIndent();
        }

        public override string ToolTipTitle
        {
            get
            {
                return "Insert line break after '}'";
            }
        }
    }
    #endregion
}
