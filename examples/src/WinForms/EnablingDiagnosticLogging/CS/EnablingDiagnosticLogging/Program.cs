using System;
using System.Collections.Generic;
using System.Text;
using DotRas;

namespace EnablingDiagnosticLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            using (DotRas.RasPhoneBook pbk = new DotRas.RasPhoneBook())
            {
                pbk.Open(phoneBookPath);

                foreach (RasEntry entry in pbk.Entries)
                {
                    Console.WriteLine(entry.Name);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}