// BoilerplateTestsApp

using System;
using System.Collections.Generic;
using System.IO;

namespace Boilerplate.Tests
{
    class TestFramework
    {
        string Definition;
        List<string> Args;
        List<string> Artifacts;

        public TestFramework(string definition_file)
        {
            Definition = definition_file;
            Args = new List<string>();
            Artifacts = new List<string>();
        }

        public void AddArg(string arg)
        {
            Args.Add(arg);
        }

        public void AddArtifact(string artifact)
        {
            Artifacts.Add(artifact);
        }
        
        private string Format(string root, string path)
        {
            return string.Format("{0}/{1}", root, path);
        }

        public string PreRun(string root)
        {
            string arg_string = Format(root, Definition);

            foreach(string arg in Args)
            {
                arg_string += string.Format(" \"{0}\"", Format(root, arg));
            }

            return arg_string;
        }

        public bool PostRun(string root)
        {
            bool success = true;

            foreach(string artifact in Artifacts)
            {
                success &= File.Exists(Format(root, artifact));
            }

            return success;
        }
    }
}
