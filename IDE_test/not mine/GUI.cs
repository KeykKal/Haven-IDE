using IDE_test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// IMPORTANT: this and every thing else in the folder not mine is indeed as the name already should tell
// not mine and i dont have any right or reason to claim that i code this.
// i got this script from the AtlusScriptCompilerGUI source code and i selfishly modified use it. 
// you can find the source code here: https://github.com/ShrineFox/AtlusScriptCompiler-GUI


namespace AtlusScriptCompilerGUI
{
    public partial class GUI
    {
        public static bool Hook { get; set; }
        public static bool Disassemble { get; set; }
        public static bool Overwrite { get; set; }
        public static bool Log { get; set; }
        public static int Selection { get; set; }

        public static List<string> GamesDropdown = new List<string>()
        {
            "SMT 3 Nocturne",
            "SMT Digital Devil Saga",
            "Persona 3 Portable",
            "Persona 3",
            "Persona 3 FES",
            "Persona 4",
            "Persona 4 Golden",
            "Persona 5",
            "Persona 5 Royal",
            "Persona Q2",
        };

        public static void Compile(string[] fileList/*, string compilerPath*/)
        {
            var args = new List<string>();
            for (int i = 0; i < fileList.Count(); i++)
            {
                string ext = Path.GetExtension(fileList[i]).ToUpper();
                if (ext == ".MSG" || ext == ".FLOW")
                {
                    //args.Add(GetArgument(fileList[i], ext, "-Compile ", compilerPath));
                    Compiler.Run(_GetArguments(fileList[i], ext, "-Compile", args));
                }
            }
            args.Clear();
            //Compiler.Run(args.ToArray());
        }
        public static void Decompile(string[] fileList/*, string compilerPath*/)
        {
            //Bug: it saved the whole command in ONE SINGEL array.
            var args = new List<string>();
            for (int i = 0; i < fileList.Count(); i++)
            {
                string ext = Path.GetExtension(fileList[i]).ToLowerInvariant();
                if (ext == ".bmd" || ext == ".bf")
                {

                    //args.Add(GetArgument(fileList[i], ext, "-Decompile ", compilerPath));
                    Compiler.Run(_GetArguments(fileList[i], ext, "-Decompile", args));
                }
            }
        }

        private static string GetArgument(string droppedFilePath, string extension, string compileArg, string compilerPath)
        {
            string encodingArg = "";
            string libraryArg = "";
            string outFormatArg = "";

            switch (Selection)
            {
                case 0: //SMT3
                    encodingArg = "-Encoding P3";
                    if (extension != ".BMD")
                        libraryArg = "-Library SMT3";
                    if (extension == ".MSG")
                        outFormatArg = "-OutFormat V1";
                    if (extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 1: //DDS
                    encodingArg = "-Encoding P3";
                    if (extension != ".BMD")
                        libraryArg = "-Library DDS";
                    if (extension == ".MSG")
                        outFormatArg = "-OutFormat V1DDS";
                    if (extension == ".FLOW")
                        outFormatArg = "-OutFormat V1DDS";
                    break;
                case 2: //P3P
                    encodingArg = "-Encoding P3";
                    if (extension != ".BMD")
                        libraryArg = "-Library P3P";
                    if (extension == ".MSG" || extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 3: //P3
                    encodingArg = "-Encoding P3";
                    if (extension != ".BMD")
                        libraryArg = "-Library P3";
                    if (extension == ".MSG" || extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 4: //P3FES
                    encodingArg = "-Encoding P3";
                    if (extension != ".BMD")
                        libraryArg = "-Library P3F";
                    if (extension == ".MSG" || extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 5: //P4
                    encodingArg = "-Encoding P4";
                    if (extension != ".BMD")
                        libraryArg = "-Library P4";
                    if (extension == ".MSG" || extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 6: //P4G
                    encodingArg = "-Encoding P4";
                    if (extension != ".BMD")
                        libraryArg = "-Library P4G";
                    if (extension == ".MSG" || extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
                case 7: //P5
                    encodingArg = "-Encoding P5";
                    if (extension != ".BMD")
                        libraryArg = "-Library P5";
                    if (extension == ".MSG")
                        outFormatArg = "-OutFormat V1BE";
                    if (extension == ".FLOW")
                        outFormatArg = "-OutFormat V3BE";
                    break;
                case 8: //P5R
                    encodingArg = "-Encoding P5";
                    if (extension != ".BMD")
                        libraryArg = "-Library P5R";
                    if (extension == ".MSG")
                        outFormatArg = "-OutFormat V1BE";
                    if (extension == ".FLOW")
                        outFormatArg = "-OutFormat V3BE";
                    break;
                case 9: //PQ2
                    encodingArg = "-Encoding SJ";
                    if (extension != ".BMD")
                        libraryArg = "-Library PQ2";
                    if (extension == ".MSG")
                        outFormatArg = "-OutFormat V1";
                    if (extension == ".FLOW")
                        outFormatArg = "-OutFormat V1";
                    break;
            }

            StringBuilder args = new StringBuilder();
            //args.Append(compilerPath+" ");
            args.Append($" {droppedFilePath} ");
            if (Disassemble) //Omits all args if you are disassembling
                args.Append($" -Disassemble");
            else
            {
                args.Append($"{compileArg} ");
                args.Append($"{outFormatArg} ");
                args.Append($"{libraryArg} ");
                args.Append($"{encodingArg} ");
                if (Hook)
                    args.Append($" -Hook ");
                if (compileArg == "-Compile " && Overwrite)
                {
                    string outPath = droppedFilePath.Replace(".flow", "")
                        .Replace(".FLOW", "").Replace(".msg", "").Replace(".MSG", "")
                        .Replace(".bf", "").Replace(".BF", "").Replace(".bmd", "")
                        .Replace(".BMD", "");
                    if (extension == ".MSG")
                        args.Append($"-Out \"{outPath + ".bmd"}\" ");
                    else if (extension == ".FLOW")
                        args.Append($"-Out \"{outPath + ".bf"}\" ");
                }
            }

            MessageBox.Show(args.ToString());
            return args.ToString();
        }

        private static string[] _GetArguments(string droppedFilePath, string extension, string compileArg, List<string> args)
        {
            args.Clear();
            string encodingArg = "";
            string libraryArg = "";
            //string outFormatArg = "";

            switch (Selection)
            {
                case 0: //SMT3
                    encodingArg = "P3";
                    if (extension != ".BMD")
                        libraryArg = "SMT3";
                    if (extension == ".MSG")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    if (extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; //"-OutFormat V1";
                    break;
                case 1: //DDS
                    encodingArg = "P3";
                    if (extension != ".BMD")
                        libraryArg = "DDS";
                    if (extension == ".MSG")
                        Compiler.OutputFileFormat = OutputFileFormat.V1DDS; // "-OutFormat V1DDS";
                    if (extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1DDS; // "-OutFormat V1DDS";
                    break;
                case 2: //P3P
                    encodingArg = "P3";
                    if (extension != ".BMD")
                        libraryArg = "P3P";
                    if (extension == ".MSG" || extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
                case 3: //P3
                    encodingArg = "P3";
                    if (extension != ".BMD")
                        libraryArg = "P3";
                    if (extension == ".MSG" || extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
                case 4: //P3FES
                    encodingArg = "P3";
                    if (extension != ".BMD")
                        libraryArg = "P3F";
                    if (extension == ".MSG" || extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
                case 5: //P4
                    encodingArg = "P4";
                    if (extension != ".BMD")
                        libraryArg = "P4";
                    if (extension == ".MSG" || extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
                case 6: //P4G
                    encodingArg = "P4";
                    if (extension != ".BMD")
                        libraryArg = "P4G";
                    if (extension == ".MSG" || extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
                case 7: //P5
                    encodingArg = "P5";
                    if (extension != ".BMD")
                        libraryArg = "P5";
                    if (extension == ".MSG")
                        Compiler.OutputFileFormat = OutputFileFormat.V1BE; // "-OutFormat V1BE";
                    if (extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V3BE; // "-OutFormat V3BE";
                    break;
                case 8: //P5R
                    encodingArg = "P5";
                    if (extension != ".BMD")
                        libraryArg = "P5R";
                    if (extension == ".MSG")
                        Compiler.OutputFileFormat = OutputFileFormat.V1BE; // "-OutFormat V1BE";
                    if (extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V3BE; // "-OutFormat V3BE";
                    break;
                case 9: //PQ2
                    encodingArg = "SJ";
                    if (extension != ".BMD")
                        libraryArg = "PQ2";
                    if (extension == ".MSG")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    if (extension == ".FLOW")
                        Compiler.OutputFileFormat = OutputFileFormat.V1; // "-OutFormat V1";
                    break;
            }

            args.Add(droppedFilePath);
            args.Add($" {compileArg} ");
            args.Add("-Library");
            args.Add(/*"-Library " +*/ libraryArg);
            args.Add("-Encoding");
            args.Add(/*"-Encoding " +*/ encodingArg);

            return args.ToArray();
        }

        private static void RunCMD(ArrayList args)
        {
            Process process = new Process();

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "cmd";
            start.UseShellExecute = true;
            start.RedirectStandardOutput = false;

            StringBuilder cmdInput = new StringBuilder();
            cmdInput.Append($"/C {args[0]} && ");
            for (int i = 1; i < args.Count - 1; i++)
                cmdInput.Append($"{args[i]} && ");
            cmdInput.Append(args[args.Count - 1]);
            if (Overwrite)
                cmdInput.Append(" && EXIT");

            start.Arguments = cmdInput.ToString();
            //MessageBox.Show(cmdInput.ToString());
            



            //Whether or not to show log while compiling
            if (!Log)
                start.WindowStyle = ProcessWindowStyle.Hidden;
            else
            {
                start.Arguments = start.Arguments.Replace("/C", "/K"); //WHAT??!
            }

            process.StartInfo = start;
            process.Start();

            ////output of the console
            //string strOutput = process.StandardOutput.ReadToEnd();

            ////Wait for process to finish
            //process.WaitForExit();

        }

        public static void OpenLog()
        {
            if (File.Exists("AtlusScriptCompiler.log"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "AtlusScriptCompiler.log";
                start.UseShellExecute = true;
                Process.Start(start);
            }
        }
    }
}
