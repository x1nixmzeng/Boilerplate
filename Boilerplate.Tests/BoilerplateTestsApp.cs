// BoilerplateTestsApp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Boilerplate.Tests
{
    class BoilerplateTestsApp
    {
        TestFramework test1;
        TestFramework test2;
        TestFramework test3;

        public BoilerplateTestsApp(string[] args)
        {
            // Test 1: Example usage
            test1 = new TestFramework("test1_definition.txt");
            test1.AddArg("test1_file_%_index%.txt");

            test1.AddArtifact("test1_file_0.txt");
            test1.AddArtifact("test1_file_1.txt");

            // Test 2: Generating 4 files from one template
            test2 = new TestFramework("test2_definition.txt");
            test2.AddArg("test2_class_%name%%ext%");

            test2.AddArtifact("test2_class_one.h");
            test2.AddArtifact("test2_class_one.cpp");
            test2.AddArtifact("test2_class_two.h");
            test2.AddArtifact("test2_class_two.cpp");

            // Test 3: Generating 2 files from 2 templates
            test3 = new TestFramework("test3_definition.txt");
            test3.AddArg("test3_class_%name%.cpp");
            test3.AddArg("test3_class_%name%.h");

            test3.AddArtifact("test3_class_one.h");
            test3.AddArtifact("test3_class_one.cpp");
            test3.AddArtifact("test3_class_two.h");
            test3.AddArtifact("test3_class_two.cpp");

            RunTest(test1);
            RunTest(test2);
            RunTest(test3);

        }

        private void RunTest(TestFramework test)
        {
            string root = "../TestData";
            string args = test.PreRun(root);

            var proc = Process.Start("boilerplate.exe", args);

            proc.WaitForExit();

            if( proc.ExitCode != 0 )
            {
                throw new Exception("Failed to start test");
            }

            if (!test.PostRun(root))
            {
                throw new Exception("Test did not produce the correct artifacts");
            }
        }

        public void Run()
        {

        }
    }
}
