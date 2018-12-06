using System;
using System.Collections.Generic;

namespace S
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("I cried today");
            journal.AddEntry("I ate a bug");
            Console.WriteLine(journal);
            journal.Save("test.txt");
        }
    }
}
