using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxWaitTime = 30 * 1000; //30seconds

            var filter = args[1] != null ? $"--filter Category={args[1]}" : null;
            var reportFileName = args[0];

            var pathToReplace = @"TestRunner/bin/Debug/netcoreapp2.0/";
            var pathToTestProjFile = GetCurrentDir().Replace($"{pathToReplace}", @"AFTests/AFTests.csproj");
            var reportFile = GetCurrentDir().Replace($"{pathToReplace}", $@"Results/{reportFileName}");
            var log = $"-l 'trx;LogFileName={reportFile}'";

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "dotnet.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = false;
            startInfo.Arguments = $"test {pathToTestProjFile} {filter} {log}";
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit(maxWaitTime);

            List<ResultDTO> results = ParseResults(reportFile);

            foreach (var r in results)
            {
                Console.WriteLine("Name: {0}, Result: {1}, ErrorMessage: {2}, StackTrace: {3}", r.Name, r.Outcome, r.ErrorMessage, r.StackTrace);
            }

            Console.ReadLine();


            /*
            string assembly;

            if (args.Length == 0 || args.Length > 2)
            {
                
                Console.WriteLine("usage: TestRunner <assembly> [typeName]"); 
                return 2;
            }

            var testAssembly = args[0];
            assembly = AppDomain.CurrentDomain.BaseDirectory.Replace("TestRunner\\bin\\Debug\\netcoreapp2.0\\", testAssembly);
            var typeName = args.Length == 2 ? args[1] : null;

            using (var runner = AssemblyRunner.WithoutAppDomain(assembly))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;

                Console.WriteLine("Discovering...");
                runner.Start(typeName);

                finished.WaitOne();
                finished.Dispose();

                return result;
            }
            */
        }

        private static List<ResultDTO> ParseResults(String filePath)
        {
            List<ResultDTO> results = new List<ResultDTO>();

            XDocument xmlDoc = XDocument.Load(filePath);
            XNamespace ns = xmlDoc.Root.GetDefaultNamespace();

            var testResults = xmlDoc.Element(ns + "TestRun").Element(ns + "Results").Elements(ns + "UnitTestResult");
            foreach (var testResult in testResults)
            {
                ResultDTO resultDTO = new ResultDTO();
                resultDTO.Name = (string)testResult.Attribute("testName");
                resultDTO.Outcome = (string)testResult.Attribute("outcome");

                if (resultDTO.Outcome == "Failed")
                {
                    var errorInfo = testResult.Element(ns + "Output").Element(ns + "ErrorInfo");
                    resultDTO.ErrorMessage = (string)errorInfo.Element(ns + "Message");
                    resultDTO.StackTrace = (string)errorInfo.Element(ns + "StackTrace");
                }

                results.Add(resultDTO);
            }

            return results;
        }

        private static String GetCurrentDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace(@"\", "/");
        }
    }
}
