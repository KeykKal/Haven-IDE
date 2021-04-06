using AtlusScriptCompilerGUI;
using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace IDE_test
{
    class Code
    {

        #region Compiler
        public static void Decompile(string[] filepaths)
        {

            MessageBox.Show("Started Decompiling. If you want to see the Log, activate the console (View --> Show/Hide Console)");

            GUI.Hook = Properties.Settings.Default.Hook;
            GUI.Disassemble = Properties.Settings.Default.Disassemble;
            GUI.Overwrite = Properties.Settings.Default.Overwrite;
            GUI.Log = Properties.Settings.Default.Log;

            GUI.Selection = FilePaths.selectedGame;
            GUI.DecompileAsync(filepaths);



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
            MessageBox.Show("Started Compiling. Log can be seen with the built in Console (View --> Hide/Show Console)");

            GUI.Hook = Properties.Settings.Default.Hook;
            GUI.Disassemble = Properties.Settings.Default.Disassemble;
            GUI.Overwrite = Properties.Settings.Default.Overwrite;
            GUI.Log = Properties.Settings.Default.Log;

            GUI.Selection = FilePaths.selectedGame;
            GUI.CompileAsync(filepaths);

        }
        #endregion

        #region can be usefull at times
        //ok I need to change this to the stringBuilder that would save preforments and is actually a smarter way that this one
        public static string addToString(string original, string toAdd)
        {
            return original + toAdd;
        }
        #endregion


        #region Tab Setting
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
                {
                    design.updateValues();
                    return true;
                }

                return false;
            }
        }

        public static bool SaveAs(Design design)
        {
            //ToDo Change that
            SaveFileDialog savefile = new SaveFileDialog()
            {
                Filter = "DataType (*.BF;*.BMD;*.FLOW;*.MSG)|*.FLOW;*.BF;*.BMG;*.MSG"
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
        #endregion


        #region not finished

        /// <summary>
        /// to start the moded game (Doesn't work)
        /// </summary>
        /// <param name="reloadedPath">reloaded.exe file path</param>
        /// <param name="gamePath">the game file path (persona 4G or any other game that is connected with Reloaded)</param>
        /// <param name="inputFilePath">the file path</param>
        /// <returns></returns>
        //unfinished and for later approaches 
        public static string StartGame(string reloadedPath, string gamePath, string inputFilePath)
        {
            //Save and Compile if the current file if user wants to
            //just Temp
            string[] filePaths = { inputFilePath };
            //MessageBox.Show("Ok works");
            //move files to the mod folder
            string bfFilePath = Path.GetFileNameWithoutExtension(inputFilePath);

            //start the game
            //var args = new List<string>();
            //args.Add($"{reloadedPath}");
            //args.Add($"--launch {gamePath}");
            //RunCMD(args);

            //retrun the output file
            return Path.Combine(FilePaths.modFolderPath, inputFilePath);
        }

        /// <summary>
        /// also not working way to start the game
        /// </summary>
        /// <param name="reloadedPath"></param>
        /// <param name="gamePath"></param>
        //Temporary solution until the other one is finished
        public static void StartGame(string reloadedPath, string gamePath)
        { 
            //Save and Compile if the current file if user wants to

            //move files to the mod folder
            

            //start the game
            var args = new List<string>();
            args.Add($"{reloadedPath}");
            args.Add($"--launch {gamePath}");
            RunCMD(args);
        }
        
        /// <summary>
        /// to start CMD
        /// </summary>
        /// <param name="args"></param>
        static void RunCMD(List<string> args)
        {
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = "cmd";

            cmdStartInfo.UseShellExecute = true;
            cmdStartInfo.RedirectStandardOutput = false;
            cmdStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            StringBuilder cmdInput = new StringBuilder();

            cmdInput.Append($"/C {args[0]} ");
            for (int i = 1; i < args.Count - 1; i++)
                cmdInput.Append($"{args[i]} ");
            cmdInput.Append(args[args.Count - 1]);

            //for(int i = 0; i < args.Count; i++)
            //{
            //    MessageBox.Show(args[i]);
            //    cmdInput.Append(args[i]);
            //}
            //MessageBox.Show(cmdInput.ToString());
            cmdStartInfo.Arguments = cmdInput.ToString();//"Reloaded-II32.exe --launch P4G.exe";
            cmdStartInfo.Arguments = cmdStartInfo.Arguments.Replace("/C", "/K");
            using (Process cmdProcess = Process.Start(cmdStartInfo))
            {
            }
        }

        #endregion

        #region no fucking clue how to do that
        //doesn't work not even a bit 
        //Update: this is useless reminder to myself get rid of this shit 
        //Update: I really need to get rid of this shit holy fuck
        public static void AutoCompile(string filePath)
        {
            //checkCreatedFiles(filePath);
            string tempFolder = AppContext.BaseDirectory + Path.Combine("Temp");
            string fileExt = Path.GetExtension(filePath).ToUpper();
            string newPath = tempFolder + "\\" + Path.GetFileName(filePath);

            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);

            if (File.Exists(newPath))
                File.Delete(newPath);


            File.Copy(filePath, newPath);

            string[] newPathArray = { newPath };

            if (fileExt == ".BF")
            {
                Decompile(newPathArray);
                checkCreatedFiles(filePath);
            }


        }
        //the same here
        static void checkCreatedFiles(string filePath)
        {
            string tempFolder = AppContext.BaseDirectory + Path.Combine("Temp");
            string fileExt = Path.GetExtension(filePath).ToUpper();
            string newPath = tempFolder + "\\" + Path.GetFileName(filePath);

            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                string extNeeded1, extNeeded2;

                //watcher.Path = ;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.flow";

                // Add event handlers.
                //watcher.Changed += ;
                //watcher.Created += ;
                //watcher.Deleted += ;
                //watcher.Renamed += ;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");

                switch (fileExt)
                {
                    case ".BMD":
                    case ".BF":
                        while (!File.Exists(Path.GetFileName(filePath) + ".flow"));
                        break;
                    case ".FLOW":
                    case ".MSG":
                        while (!File.Exists(Path.GetFileName(filePath) + ".bf") || !File.Exists(Path.GetFileName(filePath) + ".bmd")) ;
                        break;
                    default: MessageBox.Show("Something went terrebliy wrong"); break;
                }
                //MessageBox.Show("oke");
            }
        }
        #endregion

    }
}
