using System;
using System.Collections.Generic;
using System.Text;

namespace Gradius_Core
{
    public class RawData
    {
        public List<string[]> Table { get; set; }
        public void Load(string filePath)
        {

            this.Table = new List<string[]>();
            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (string line in lines)
                this.Table.Add(line.Split(';'));


        }

        public void Concat(RawData add)
        {
            add.Table.RemoveAt(0);
            this.Table.AddRange(add.Table);
        }
    }
}
