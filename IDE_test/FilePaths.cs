using AtlusScriptCompilerGUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
    //so that is good Update: its been 3 days and i just left it maybe ill work on it another time and if im really lucky even get it right. 
    // | to add in future compile and auto open the files that are created.(Future me: Compile and start?)
    class FilePaths
    {
        /// <summary>
        /// should be always the in the executable folder
        /// </summary>
        public static string LibraryPath
        {
            get
            {
                if(!Directory.Exists(AppDomain.CurrentDomain + "\\Libraries"))
                {
                    return null;
                }

                return AppDomain.CurrentDomain + "\\Libraries";
            }
        }

        /// <summary>
        /// returns the Selected game as an int (for example: persona 4 Golden is 6)
        /// </summary>
        public static int selectedGame;

        /// <summary>
        /// to get the text File with the selected game 
        /// </summary>
        /// <returns>String path to the text file</returns>
        public static string SelectedGamePath()
        {
            return File.ReadAllLines("Game.txt")[0];
        }

        /// <summary>
        /// Not in use right now but to get/set The Reloded.exe path
        /// </summary>
        public static string ReloadedPath
        {
            get
            {
                //change this
                string path = Properties.Settings.Default.ReloadedPath;

                if (path == null)
                {
                    OpenFileDialog openFile = new OpenFileDialog
                    {
                        Title = "Select the Reloaded exe File... ",
                        Multiselect = false,
                        Filter = "Executable (*.EXE)|*.EXE;|" +
                        "All files (*.*)|*.*"
                    };

                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        path = openFile.FileName;
                    }
                }

                _reloadedFilePath = path;

                return _reloadedFilePath;
            }
            set
            {
                _reloadedFilePath = value;

                if (_reloadedFilePath == null)
                    MessageBox.Show("[ERROR] the Reloaded filepath is null");

                Properties.Settings.Default.ReloadedPath = _reloadedFilePath;
            }
        }
        private static string _reloadedFilePath;

        /// <summary>
        /// Not in use right now but used to get/set the Games.exe path
        /// </summary>
        public static string GameFilePath
        {
            get
            {
                //change this
                string path = Properties.Settings.Default.GamePath;

                if (path == null)
                {
                    OpenFileDialog openFile = new OpenFileDialog
                    {
                        Title = "Select the P4G exe ... ",
                        Multiselect = false,
                        Filter = "Executable (*.EXE)|*.EXE;|" +
                        "All files (*.*)|*.*"
                    };

                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        path = openFile.FileName;
                    }
                }

                _gameFilePath = path;

                return _gameFilePath;
            }
            set
            {
                _gameFilePath = value;

                if (_gameFilePath == null)
                    MessageBox.Show("[ERROR] gamePath is null");

                Properties.Settings.Default.GamePath = _gameFilePath;
            }
        }
        private static string _gameFilePath;

        /// <summary>
        /// not in use right now was to be used to load the modded file in the mod folder but that won't work
        /// </summary>
        public static string modFolderPath { private set { } 
            get { try { return Path.Combine(Path.GetDirectoryName(GameFilePath), "mods"); } 
                catch { MessageBox.Show("Please select the Game File in the conifgs, \n " +
                    "If you get this Error Pls Write me a message because this shouldn't happen"); return ""; } } }
    }


    /// <summary>
    /// this whole section isn't written by me or any friend of me (copyed from a library not stackoverflow)
    /// </summary>
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