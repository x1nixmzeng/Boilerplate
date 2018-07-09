// BoilerplateApp

using System.Collections.Generic;
using System.IO;

namespace Boilerplate
{
    struct BoilerplateData
    {
        public List<Dictionary<string, string>> Values;

        public static BoilerplateData Create()
        {
            BoilerplateData inst = new BoilerplateData();
            inst.Values = new List<Dictionary<string, string>>();
            return inst;
        }

        public static Dictionary<string, string> CreateItem()
        {
            return new Dictionary<string, string>();
        }
    }

    struct ContainerNode
    {
        public string filename;
        public string data;

        public ContainerNode(string filename, string data)
        {
            this.filename = filename;
            this.data = data;
        }
    }

    struct ContainerData
    {
        public List<ContainerNode> Data;

        public static ContainerData Create()
        {
            ContainerData inst = new ContainerData();
            inst.Data = new List<ContainerNode>();
            return inst;
        }

        public void Add(string filename)
        {
            string source = File.ReadAllText(filename);
            var pair = new ContainerNode(filename, source);
            Data.Add(pair);
        }
    }
    
}
