using System;
using DotRas;

namespace EnablingDiagnosticLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            using (var pbk = new RasPhoneBook())
            {
                pbk.Open(phoneBookPath);

                foreach (var entry in pbk.Entries)
                {
                    Console.WriteLine(entry.Name);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}