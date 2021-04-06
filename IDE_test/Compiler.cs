using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using AtlusScriptCompiler;
using AtlusScriptLibrary.Common.Logging;
using AtlusScriptLibrary.Common.Libraries;
using AtlusScriptLibrary.Common.Text;
using AtlusScriptLibrary.Common.Text.Encodings;
using AtlusScriptLibrary.FlowScriptLanguage;
using AtlusScriptLibrary.FlowScriptLanguage.BinaryModel;
using AtlusScriptLibrary.FlowScriptLanguage.Compiler;
using AtlusScriptLibrary.FlowScriptLanguage.Decompiler;
using AtlusScriptLibrary.FlowScriptLanguage.Disassembler;
using AtlusScriptLibrary.MessageScriptLanguage;
using AtlusScriptLibrary.MessageScriptLanguage.Compiler;
using AtlusScriptLibrary.MessageScriptLanguage.Decompiler;
using FormatVersion = AtlusScriptLibrary.FlowScriptLanguage.FormatVersion;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace IDE_test
{
    class Compiler
    {
        static AssemblyName AssemblyName = Assembly.GetExecutingAssembly().GetName();
        static Version Version = AssemblyName.Version;
        static Logger Logger = new Logger(nameof(IDE_test));


        public static LogListener Listener = new FileAndConsoleLogListener(true, LogLevel.Info | LogLevel.Warning | LogLevel.Error | LogLevel.Fatal);
        public static string InputFilePath;
        public static string OutputFilePath;
        public static bool IsActionAssigned;
        public static bool DoCompile;
        public static bool DoDecompile;
        public static bool DoDisassemble;
        public static InputFileFormat InputFileFormat;
        public static OutputFileFormat OutputFileFormat;
        public static string MessageScriptTextEncodingName;
        public static Encoding MessageScriptEncoding;
        public static string LibraryName;
        public static bool LogTrace;
        public static bool FlowScriptEnableProcedureTracing;
        public static bool FlowScriptEnableProcedureCallTracing;
        public static bool FlowScriptEnableFunctionCallTracing;
        public static bool FlowScriptEnableStackCookie;
        public static bool FlowScriptEnableProcedureHook;
        public static Library library = LibraryLookup.GetLibrary(FilePaths.SelectedGamePath());


        /// <summary>
        /// Runs the Compiler hopefully asynchronous
        /// </summary>
        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Logger.ConsoleError("No arguments specified!");
                //DisplayUsage(); //this one originally is here to show all the commands to compile/decompile. not needed anymore
                return;
            }

 

            library = LibraryLookup.GetLibrary(FilePaths.SelectedGamePath());

            // Set up log listener
            Listener.Subscribe(Logger);

            // Log arguments
            Logger.ConsoleTrace($"Arguments: {string.Join(" ", args)}");

            if (!TryParseArguments(args))
            {
                Logger.ConsoleError("Failed to parse arguments!");
                //DisplayUsage();
                return;
            }

            if (LogTrace)
                Listener.Filter |= LogLevel.Trace;


            bool success;

#if !DEBUG
            try
#endif
            {
                if (DoCompile)
                {
                    //if (MessageBox.Show("Do you want to Start game together with the mod?", "Start Game?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    //{
                    //    OutputFilePath = Path.Combine(FilePaths.modFolderPath, Path.GetFileNameWithoutExtension(InputFilePath) + ".bf");
                    //}
                    success = TryDoCompilation();
                }
                else if (DoDecompile)
                {
                    success = TryDoDecompilation();
                    //MessageBox.Show(OutputFilePath);
                }
                else if (DoDisassemble)
                {
                    success = TryDoDisassembling();
                }
                else
                {
                    ResetValues();
                    Array.Clear(args, 0, args.Length);
                    Logger.ConsoleError("No compilation, decompilation or disassemble instruction given!");
                    //DisplayUsage();
                    return;
                }
            }
#if !DEBUG
            catch ( Exception e )
            {
                //LogException( "Unhandled exception thrown", e );
                success = false;

                if ( Debugger.IsAttached )
                    throw;
            }
#endif

            if (success)
            {
                Logger.ConsoleInfo("Task completed successfully!");
                MessageBox.Show("Task completed sucessfully");

            }
            else
            {
                Logger.ConsoleError("One or more errors occured while executing task!");
            }

            ResetValues();
            Array.Clear(args, 0, args.Length);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Needed to reset the values after each compile/decopmile
        /// </summary>
        static void ResetValues()
        {
            //library = null;
            InputFilePath = null;
            OutputFilePath = null;
            LibraryName = null;
            IsActionAssigned = false;
            DoCompile = false;
            DoDecompile = false;
            DoDisassemble = false;
            InputFileFormat = InputFileFormat.None;
            OutputFileFormat = OutputFileFormat.None;
            MessageScriptTextEncodingName = null;
            MessageScriptEncoding = Encoding.Default;
            //LibraryName = null;
            LogTrace = false;
            FlowScriptEnableProcedureTracing = false;
            FlowScriptEnableProcedureCallTracing = false;
            FlowScriptEnableFunctionCallTracing = false;
            FlowScriptEnableStackCookie = false;
            FlowScriptEnableProcedureHook = false;
        }

        /// <summary>
        /// returns true if it successfully could parse the input of the compiler
        /// </summary>
        private static bool TryParseArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                bool isLast = i + 1 == args.Length;
                //MessageBox.Show(args[i]);
                switch (args[i])
                {
                    // General
                    case "-In":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -In parameter");
                            return false;
                        }

                        InputFilePath = args[++i];
                        break;

                    case "-InFormat":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -InFormat parameter");
                            return false;
                        }

                        if (!Enum.TryParse(args[++i], true, out InputFileFormat))
                        {
                            Logger.ConsoleError("Invalid input file format specified");
                            return false;
                        }

                        break;

                    case "-Out":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -Out parameter");
                            return false;
                        }

                        OutputFilePath = args[++i];
                        break;

                    case "-OutFormat":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -OutFormat parameter");
                            return false;
                        }

                        if (!Enum.TryParse(args[++i], true, out OutputFileFormat))
                        {
                            Logger.ConsoleError("Invalid output file format specified");
                            return false;
                        }

                        break;

                    case "-Compile":
                        if (!IsActionAssigned)
                        {
                            IsActionAssigned = true;
                        }
                        else
                        {
                            Logger.ConsoleError("Attempted to assign compilation action while another action is already assigned.");
                            return false;
                        }

                        DoCompile = true;
                        break;

                    case "-Decompile":
                        if (!IsActionAssigned)
                        {
                            IsActionAssigned = true;
                        }
                        else
                        {
                            Logger.ConsoleError("Attempted to assign decompilation action while another action is already assigned.");
                            return false;
                        }

                        DoDecompile = true;
                        break;

                    case "-Disassemble":
                        if (!IsActionAssigned)
                        {
                            IsActionAssigned = true;
                        }
                        else
                        {
                            Logger.ConsoleError("Attempted to assign disassembly action while another action is already assigned.");
                            return false;
                        }

                        DoDisassemble = true;
                        break;

                    case "-Library":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -Library parameter");
                            return false;
                        }

                        //MessageBox.Show("Library before: " + args[i]);
                        LibraryName = args[++i];
                        //MessageBox.Show("Library after: " + LibraryName);
                        library = LibraryLookup.GetLibrary(LibraryName);
                        break;

                    case "-LogTrace":
                        LogTrace = true;
                        break;

                    // MessageScript
                    case "-Encoding":
                        if (isLast)
                        {
                            Logger.ConsoleError("Missing argument for -Encoding parameter");
                            return false;
                        }

                        MessageScriptTextEncodingName = args[++i];

                        switch (MessageScriptTextEncodingName.ToLower())
                        {
                            case "sj":
                            case "shiftjis":
                            case "shift-jis":
                                MessageScriptEncoding = ShiftJISEncoding.Instance;
                                break;
                            default:
                                try
                                {
                                    MessageScriptEncoding = AtlusEncoding.GetByName(MessageScriptTextEncodingName);
                                }
                                catch (ArgumentException)
                                {
                                    Logger.ConsoleError($"Unknown encoding: {MessageScriptTextEncodingName}");
                                    return false;
                                }
                                break;
                        }

                        Logger.ConsoleInfo($"Using {MessageScriptTextEncodingName} encoding");
                        break;

                    case "-TraceProcedure":
                        FlowScriptEnableProcedureTracing = true;
                        break;

                    case "-TraceProcedureCalls":
                        FlowScriptEnableProcedureCallTracing = true;
                        break;

                    case "-TraceFunctionCalls":
                        FlowScriptEnableFunctionCallTracing = true;
                        break;

                    case "-StackCookie":
                        FlowScriptEnableStackCookie = true;
                        break;

                    case "-Hook":
                        FlowScriptEnableProcedureHook = true;
                        break;
                }
            }

            if (InputFilePath == null)
            {
                InputFilePath = args[0];
            }
            //MessageBox.Show(InputFilePath);
            if (!File.Exists(InputFilePath))
            {
                Logger.ConsoleError($"Specified input file doesn't exist! ({InputFilePath})");
                return false;
            }

            if (InputFileFormat == InputFileFormat.None)
            {
                var extension = Path.GetExtension(InputFilePath);

                switch (extension.ToLowerInvariant())
                {
                    case ".bf":
                        InputFileFormat = InputFileFormat.FlowScriptBinary;
                        break;

                    case ".flow":
                        InputFileFormat = InputFileFormat.FlowScriptTextSource;
                        break;

                    case ".flowasm":
                        InputFileFormat = InputFileFormat.FlowScriptAssemblerSource;
                        break;

                    case ".bmd":
                        InputFileFormat = InputFileFormat.MessageScriptBinary;
                        break;

                    case ".msg":
                        InputFileFormat = InputFileFormat.MessageScriptTextSource;
                        break;

                    default:
                        Logger.ConsoleError("Unable to detect input file format");
                        return false;
                }
            }


            if (!IsActionAssigned)
            {
                // Decide on default action based on input file format
                switch (InputFileFormat)
                {
                    case InputFileFormat.FlowScriptBinary:
                    case InputFileFormat.MessageScriptBinary:
                        DoDecompile = true;
                        break;
                    case InputFileFormat.FlowScriptTextSource:
                    case InputFileFormat.MessageScriptTextSource:
                        DoCompile = true;
                        break;
                    default:
                        Logger.ConsoleError("No compilation, decompilation or disassemble instruction given!");
                        return false;
                }
            }

            if (OutputFilePath == null)
            {
                if (DoCompile)
                {
                    switch (InputFileFormat)
                    {
                        case InputFileFormat.FlowScriptTextSource:
                        case InputFileFormat.FlowScriptAssemblerSource:
                            OutputFilePath = InputFilePath + ".bf";
                            break;
                        case InputFileFormat.MessageScriptTextSource:
                            OutputFilePath = InputFilePath + ".bmd";
                            break;
                    }
                }
                else if (DoDecompile)
                {
                    switch (InputFileFormat)
                    {
                        case InputFileFormat.FlowScriptBinary:
                            OutputFilePath = InputFilePath + ".flow";
                            break;
                        case InputFileFormat.MessageScriptBinary:
                            OutputFilePath = InputFilePath + ".msg";
                            break;
                    }
                }
                else if (DoDisassemble)
                {
                    switch (InputFileFormat)
                    {
                        case InputFileFormat.FlowScriptBinary:
                            OutputFilePath = InputFilePath + ".flowasm";
                            break;
                    }
                }
            }

            Logger.ConsoleInfo($"Output file path is set to {OutputFilePath}");

            return true;
        }

        /// <summary>
        /// to Disassemble the files
        /// </summary>
        private static bool TryDoDisassembling()
        {
            switch (InputFileFormat)
            {
                case InputFileFormat.FlowScriptTextSource:
                case InputFileFormat.FlowScriptAssemblerSource:
                case InputFileFormat.MessageScriptTextSource:
                    Logger.ConsoleError("Can't disassemble a text source!");
                    return false;

                case InputFileFormat.FlowScriptBinary:
                    return TryDoFlowScriptDisassembly();

                case InputFileFormat.MessageScriptBinary:
                    Logger.ConsoleInfo("Error. Disassembling message scripts is not supported.");
                    return false;

                default:
                    Logger.ConsoleError("Invalid input file format!");
                    return false;
            }
        }

        /// <summary>
        /// to Disassemble the Flow script
        /// </summary>
        private static bool TryDoFlowScriptDisassembly()
        {
            // load binary file
            Logger.ConsoleInfo("Loading binary FlowScript file...");

            FlowScriptBinary script = null;
            var format = GetFlowScriptFormatVersion();

            if (!TryPerformAction("Failed to load flow script from file.", () =>
            {
                script = FlowScriptBinary.FromFile(InputFilePath, (BinaryFormatVersion)format);
            }))
            {
                return false;
            }

            Logger.ConsoleInfo("Disassembling FlowScript...");
            if (!TryPerformAction("Failed to disassemble flow script to file.", () =>
            {
                var disassembler = new FlowScriptBinaryDisassembler(OutputFilePath);
                disassembler.Disassemble(script);
                disassembler.Dispose();
            }))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// trys to Compile file returns true if it is compiled succesfully
        /// </summary>
        private static bool TryDoCompilation()
        {
            switch (InputFileFormat)
            {
                case InputFileFormat.FlowScriptTextSource:
                case InputFileFormat.FlowScriptAssemblerSource:
                    return TryDoFlowScriptCompilation();

                case InputFileFormat.MessageScriptTextSource:
                    return TryDoMessageScriptCompilation();

                case InputFileFormat.FlowScriptBinary:
                case InputFileFormat.MessageScriptBinary:
                    Logger.ConsoleError("Binary files can't be compiled again!");
                    return false;

                default:
                    Logger.ConsoleError("Invalid input file format!");
                    return false;
            }
        }

        /// <summary>
        /// Compiles the Flow script returns true if it is compiled succesfully
        /// </summary>
        private static bool TryDoFlowScriptCompilation()
        {
            Logger.ConsoleInfo("Compiling FlowScript...");

            // Get format verson
            var version = GetFlowScriptFormatVersion();
            if (version == FormatVersion.Unknown)
            {
                Logger.ConsoleError("Invalid FlowScript file format specified");
                return false;
            }
            //MessageBox.Show("Console.WriteLine();");
            // Compile source
            var compiler = new FlowScriptCompiler(version);
            compiler.Library = library;
            compiler.AddListener(Listener);
            compiler.Encoding = MessageScriptEncoding;
            compiler.EnableProcedureTracing = FlowScriptEnableProcedureTracing;
            compiler.EnableProcedureCallTracing = FlowScriptEnableProcedureCallTracing;
            compiler.EnableFunctionCallTracing = FlowScriptEnableFunctionCallTracing;
            compiler.EnableStackCookie = FlowScriptEnableStackCookie;
            compiler.HookImportedProcedures = FlowScriptEnableProcedureHook;


            if (LibraryName != null)
            {
                var library = LibraryLookup.GetLibrary(LibraryName);
                //MessageBox.Show("LibraryName: " + LibraryName);
                if (library == null)
                {
                    Logger.ConsoleError("Invalid library name specified");
                    return false;
                }

                compiler.Library = library;
            }

            FlowScript flowScript = null;
            var success = false;
            using (var file = File.Open(InputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    //MessageBox.Show(file.Name);
                    success = compiler.TryCompile(file, out flowScript);
                }
                catch (UnsupportedCharacterException e)
                {
                    Logger.ConsoleError($"Character '{e.Character}' not supported by encoding '{e.EncodingName}'");
                }

                if (!success)
                {
                    Logger.ConsoleError("One or more errors occured during compilation!");
                    return false;
                }
            }

            // Write binary
            Logger.ConsoleInfo("Writing binary to file...");
            return TryPerformAction("An error occured while saving the file.", () => flowScript.ToFile(OutputFilePath));
        }

        /// <summary>
        /// Compiles the Message script returns true if it is compiled succesfully
        /// </summary>
        private static bool TryDoMessageScriptCompilation()
        {
            // Compile source
            Logger.ConsoleInfo("Compiling MessageScript...");

            var version = GetMessageScriptFormatVersion();
            if (version == AtlusScriptLibrary.MessageScriptLanguage.FormatVersion.Detect)
            {
                Logger.ConsoleError("Invalid MessageScript file format");
                return false;
            }

            var compiler = new MessageScriptCompiler(GetMessageScriptFormatVersion(), MessageScriptEncoding);
            compiler.AddListener(Listener);

            if (LibraryName != null)
            {
                var library = LibraryLookup.GetLibrary(LibraryName);

                if (library == null)
                {
                    Logger.ConsoleError("Invalid library name specified");
                    return false;
                }

                compiler.Library = library;
            }

            bool success = false;
            MessageScript script = null;

            try
            {
                success = compiler.TryCompile(File.OpenText(InputFilePath), out script);
            }
            catch (UnsupportedCharacterException e)
            {
                Logger.ConsoleError($"Character '{e.Character}' not supported by encoding '{e.EncodingName}'");
            }

            if (!success)
            {
                Logger.ConsoleError("One or more errors occured during compilation!");
                return false;
            }

            // Write binary
            Logger.ConsoleInfo("Writing binary to file...");
            if (!TryPerformAction("An error occured while saving the file.", () => script.ToFile(OutputFilePath)))
                return false;

            return true;
        }

        /// <summary>
        /// To determine the flowscript version
        /// </summary>
        private static FormatVersion GetFlowScriptFormatVersion()
        {
            FormatVersion version;
            switch (OutputFileFormat)
            {
                case OutputFileFormat.V1:
                    version = FormatVersion.Version1;
                    break;
                case OutputFileFormat.V1BE:
                    version = FormatVersion.Version1BigEndian;
                    break;
                case OutputFileFormat.V1DDS:
                    version = FormatVersion.Version1; // TODO: relay proper MessageScript version to FlowScript loader
                    break;
                case OutputFileFormat.V2:
                    version = FormatVersion.Version2;
                    break;
                case OutputFileFormat.V2BE:
                    version = FormatVersion.Version2BigEndian;
                    break;
                case OutputFileFormat.V3:
                    version = FormatVersion.Version3;
                    break;
                case OutputFileFormat.V3BE:
                    version = FormatVersion.Version3BigEndian;
                    break;
                default:
                    version = FormatVersion.Unknown;
                    break;
            }

            return version;
        }

        /// <summary>
        /// To determine the Messageversion
        /// </summary>
        private static AtlusScriptLibrary.MessageScriptLanguage.FormatVersion GetMessageScriptFormatVersion()
        {
            AtlusScriptLibrary.MessageScriptLanguage.FormatVersion version;

            switch (OutputFileFormat)
            {
                case OutputFileFormat.V1:
                    version = AtlusScriptLibrary.MessageScriptLanguage.FormatVersion.Version1;
                    break;
                case OutputFileFormat.V1DDS:
                    version = AtlusScriptLibrary.MessageScriptLanguage.FormatVersion.Version1DDS;
                    break;
                case OutputFileFormat.V1BE:
                    version = AtlusScriptLibrary.MessageScriptLanguage.FormatVersion.Version1BigEndian;
                    break;
                default:
                    version = AtlusScriptLibrary.MessageScriptLanguage.FormatVersion.Detect;
                    break;
            }

            return version;
        }

        /// <summary>
        /// Tries to Decompile returns true if it is successful
        /// </summary>
        private static bool TryDoDecompilation()
        {
            switch (InputFileFormat)
            {
                case InputFileFormat.FlowScriptTextSource:
                case InputFileFormat.FlowScriptAssemblerSource:
                case InputFileFormat.MessageScriptTextSource:
                    Logger.ConsoleError("Can't decompile a text source!");
                    return false;

                case InputFileFormat.FlowScriptBinary:
                    return TryDoFlowScriptDecompilation();

                case InputFileFormat.MessageScriptBinary:
                    return TryDoMessageScriptDecompilation();

                default:
                    Logger.ConsoleError("Invalid input file format!");
                    return false;
            }
        }

        /// <summary>
        /// decompiles the flowscript returns true if it is successful
        /// </summary>
        private static bool TryDoFlowScriptDecompilation()
        {
            // Load binary file
            Logger.ConsoleInfo("Loading binary FlowScript file...");
            FlowScript flowScript = null;
            var encoding = MessageScriptEncoding;
            var format = GetFlowScriptFormatVersion();

            if (!TryPerformAction("Failed to load flow script from file", () => flowScript = FlowScript.FromFile(InputFilePath, encoding, format)))
                return false;

            Logger.ConsoleInfo("Decompiling FlowScript...");

            var decompiler = new FlowScriptDecompiler();

            decompiler.Library = library;
            
            decompiler.AddListener(Listener);
            
            if (LibraryName != null)
            {
                var library = LibraryLookup.GetLibrary(LibraryName);

                if (library == null)
                {
                    Logger.ConsoleError("Invalid library name specified");
                    return false;
                }

                decompiler.Library = library;
            }

            if (!decompiler.TryDecompile(flowScript, OutputFilePath))
            {
                Logger.ConsoleError("Failed to decompile FlowScript");
                return false;
            }

            //open flow script


            return true;
        }

        /// <summary>
        /// decompiles the messagescript returns true if it is successful
        /// </summary>
        private static bool TryDoMessageScriptDecompilation()
        {
            // load binary file
            Logger.ConsoleInfo("Loading binary MessageScript file...");
            MessageScript script = null;
            var encoding = MessageScriptEncoding;
            var format = GetMessageScriptFormatVersion();

            if (!TryPerformAction("Failed to load message script from file.", () => script = MessageScript.FromFile(InputFilePath, format, encoding)))
                return false;

            Logger.ConsoleInfo("Decompiling MessageScript...");

            if (!TryPerformAction("Failed to decompile message script to file.", () =>
            {
                using (var decompiler = new MessageScriptDecompiler(new FileTextWriter(OutputFilePath)))
                {
                    if (LibraryName != null)
                    {
                        var library = LibraryLookup.GetLibrary(LibraryName);

                        if (library == null)
                        {
                            Logger.ConsoleError("Invalid library name specified");
                        }

                        decompiler.Library = library;
                    }

                    decompiler.Decompile(script);
                }
            }))
            {
                return false;
            }

            //open msg file

            return true;
        }

        /// <summary>
        /// always returns true and preforms the action 
        /// </summary>
        private static bool TryPerformAction(string errorMessage, Action action)
        {
#if !DEBUG
            try
            {
#endif
            action();
#if !DEBUG
            }
            catch ( Exception e )
            {
                LogException( errorMessage, e );
                return false;
            }
#endif

            return true;
        }

        /// <summary>
        /// Isn't used anywhere and i am not sure if it is even usefull
        /// but it just prints the errors to the console
        /// </summary>
        private static void LogException(string message, Exception e)
        {
            Logger.Error(message);
            Logger.Error("Exception info:");
            Logger.Error($"{e.Message}");
            Logger.Error("Stacktrace:");
            Logger.Error($"{e.StackTrace}");
        }


    }

    /// <summary>
    /// Possible File Formats for the input file
    /// </summary>
    public enum InputFileFormat
    {
        None,
        FlowScriptBinary,
        FlowScriptTextSource,
        FlowScriptAssemblerSource,
        MessageScriptBinary,
        MessageScriptTextSource
    }

    /// <summary>
    /// Possible File Formats for the output file
    /// </summary>
    public enum OutputFileFormat
    {
        None,
        V1,
        V1DDS,
        V1BE,
        V2,
        V2BE,
        V3,
        V3BE
    }



}
