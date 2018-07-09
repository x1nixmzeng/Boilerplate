// BoilerplateApp

using System;
using System.Collections.Generic;
using System.IO;

namespace Boilerplate
{
    class BoilerplateApp
    {
        string TemplateFile;
        string[] SourceFiles;

        Dictionary<string, string> GlobalData;

        public BoilerplateApp(string[] args)
        {
            if (args.Length > 0)
            {
                TemplateFile = args[0];
            }

            if (args.Length > 1)
            {
                int SourceFileCount = args.Length - 1;

                SourceFiles = new string[SourceFileCount];
                Array.Copy(args, 1, SourceFiles, 0, SourceFileCount);
            }

            GlobalData = new Dictionary<string, string>();
        }

        bool IsValid()
        {
            if (File.Exists(TemplateFile))
            {
                if (SourceFiles != null)
                {
                    foreach (string SourceFile in SourceFiles)
                    {
                        if (!File.Exists(SourceFile))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public void Run()
        {
            if (!IsValid())
            {
                throw new Exception("Incorrect usage");
            }

            TemplateReader templateReader = new TemplateReader(TemplateFile);
            if (templateReader.Items == 0)
            {
                throw new Exception("Failed to read template file");
            }

            ContainerData templates = ContainerData.Create();
            foreach (string SourceFile in SourceFiles)
            {
                if (File.Exists(SourceFile))
                {
                    templates.Add(SourceFile);
                }
            }

            if (templates.Data.Count == 0)
            {
                throw new Exception("No files to read");
            }

            int RowIndex = 0;
            foreach (var row in templateReader.Data.Values)
            {
                GlobalData["_index"] = string.Format("{0}", RowIndex);
                GlobalData["_index1"] = string.Format("{0}", RowIndex + 1);

                foreach (var template in templates.Data)
                {
                    GlobalData["_filename"] = "";
                    GlobalData["_filename_upper"] = "";
                    GlobalData["_filepath"] = "";
                    GlobalData["_fileext"] = "";

                    string filename = TranslateAll(template.filename, row);

                    if (File.Exists(filename))
                    {
                        throw new Exception(string.Format("{0} already exists (will not trash existing file)", filename));
                    }

                    string name = Path.GetFileNameWithoutExtension(filename);

                    GlobalData["_filename"] = name;
                    GlobalData["_filename_upper"] = name.ToUpper();
                    GlobalData["_filepath"] = Path.GetFileName(filename);
                    GlobalData["_fileext"] = Path.GetExtension(filename);

                    string filedata = TranslateAll(template.data, row);

                    File.WriteAllText(filename, filedata);
                }

                ++RowIndex;
            }
        }

        private string TranslateAll(string Source, Dictionary<string, string> Items)
        {
            return Translate(Translate(Source, Items), GlobalData);
        }

        private string Translate(string Source, Dictionary<string, string> Items)
        {
            string Result = Source;

            foreach (KeyValuePair<string, string> entry in Items)
            {
                string Haystack = string.Format("%{0}%", entry.Key);
                Result = Result.Replace(Haystack, entry.Value);
            }

            return Result;
        }
    }
}
