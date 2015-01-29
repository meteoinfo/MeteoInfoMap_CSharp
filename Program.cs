using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace MeteoInfo
{
    static class Program
    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
//            //    (sender, e) =>
//            //    {
//            //        if (e.IsTerminating)
//            //        {
//            //            object o = e.ExceptionObject;
//            //            Console.WriteLine(o.ToString()); // use EventLog instead
//            //        }
//            //    }
//            //);


//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//#if DEBUG
//            //don't catch unhandled exceptions if debuggin
//            Application.Run(new frmMain());
//#else
//            //enable unhandled exceptions trapping for release version
//            try
//            {
//            Application.Run(new frmMain());
//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//#endif
//        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0].Substring(args[0].Length - 3) == ".py")
                {
                    // get console output
                    if (!AttachConsole(-1))
                        AllocConsole();

                    // Run script
                    Console.WriteLine("MeteoInfo Scripting");

                    frmMain aFrm = new frmMain(true);

                    string aFile = args[0];

                    ScriptEngine scriptEngine = Python.CreateEngine();
                    ScriptScope pyScope = scriptEngine.CreateScope();

                    //Set path
                    string path = Assembly.GetExecutingAssembly().Location;
                    string rootDir = Directory.GetParent(path).FullName;
                    List<string> paths = new List<string>();
                    paths.Add(rootDir);
                    paths.Add(Path.Combine(rootDir, "Lib"));
                    scriptEngine.SetSearchPaths(paths.ToArray());

                    pyScope.SetVariable("mipy", aFrm);

                    //Run python script
                    try
                    {
                        //ScriptSource sourceCode = scriptEngine.CreateScriptSourceFromString(text, SourceCodeKind.Statements);
                        ScriptSource sourceCode = scriptEngine.CreateScriptSourceFromFile(aFile);
                        sourceCode.Execute(pyScope);
                        //CompiledCode compiled = sourceCode.Compile();
                        //compiled.Execute(pyScope); 
                        //sourceCode.ExecuteProgram();
                    }
                    catch (Exception e)
                    {
                        ExceptionOperations eo = scriptEngine.GetService<ExceptionOperations>();
                        Console.Write(eo.FormatException(e));
                    }

                    //aFrm.RunScript(aFile);
                    //aFrm.Dispose();
                    //aFrm.Show();
                    //Application.Run(aFrm);
                    //aFrm.Close();

                    FreeConsole(); // detach console

                    // get command prompt back
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                }
                else if (args[0].Substring(args[0].Length - 4) == ".mip")
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    frmMain myApp = new frmMain();
                    string pFile = args[0];
                    pFile = Path.Combine(System.Environment.CurrentDirectory, pFile);
                    myApp.OpenProjectFile(pFile);
                    Application.Run(myApp);
                }

            }
            else
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}
