using System;
using System.Collections.Generic;
using System.IO;

namespace S
{
    class Journal
    {
        private readonly List<string> entries = new List<string>();
        private int count = 0;

        public int AddEntry(string message)
        {
            entries.Add($"{++count}: {message}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, entries);
        }
    }
}