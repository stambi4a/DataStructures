namespace Phonebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class PhonebookCreator
    {
        public void CreatePhonebook()
        {
            Dictionary<string, string> phonebook = new Dictionary<string, string>();

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "search")
                {
                    break;
                }
                char[] dels = { ' ', '-', '+' };
                string[] input = line.Split(dels, StringSplitOptions.RemoveEmptyEntries);
                this.AddEntry(input, phonebook);
            }

            while (true)
            {
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }

                this.SearchPhonebook(name, phonebook);
            }
        }

        private void AddEntry(string[] inputParams, Dictionary<string, string> phonebook)
        {
            phonebook.Add(inputParams[0], inputParams[1]);
        }

        private void SearchPhonebook(string name, Dictionary<string, string> phonebook)
        {
            if (!phonebook.Any(x=>x.Key == name))
            {
                Console.WriteLine($"Contact {name} does not exist.");
                return;
            }

            Console.WriteLine($"{name} -> {phonebook[name]}");
        }
    }
}
