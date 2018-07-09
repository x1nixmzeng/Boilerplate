// BoilerplateApp

using System.IO;

namespace Boilerplate
{
    class TemplateReader
    {
        public BoilerplateData Data;

        public TemplateReader(string filename)
        {
            Data = BoilerplateData.Create();

            string[] Lines = File.ReadAllLines(filename);
            foreach(string Line in Lines)
            {
                var DataItem = BoilerplateData.CreateItem();

                string[] Items = Line.Split(new char[] { ';' });    
                foreach(string Item in Items)
                {
                    string[] Parts = Item.Split(new char[] { '=' });

                    if( Parts.Length == 2 )
                    {
                        DataItem.Add(Parts[0], Parts[1]);
                    }
                }

                Data.Values.Add(DataItem);
            }
        }

        public int Items
        {
            get
            {
                return Data.Values.Count;
            }
        }
    }
}
