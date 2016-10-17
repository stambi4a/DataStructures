namespace StringEditor
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Wintellect.PowerCollections;

    internal class StringEditor
    {
        private const string PathName = "..\\..\\Commands.txt";
        private const string PathResults = "..\\..\\Results.txt";

        internal StringEditor()
        {
            this.Rope = new Rope();
        }

        internal Rope Rope { get; set; }

        public void EditString()
        {
            ICollection<string> commandResults = new List<string>();
            using (StreamReader reader = new StreamReader(PathName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    string[] commandParams = line.Split();
                    string result = this.ExecuteCommand(commandParams);
                    commandResults.Add(result);
                }
            }
                
            Console.WriteLine();
            this.PrintResults(commandResults);
        }

        private void PrintResults(ICollection<string> commandResults)
        {
            using (StreamWriter writer = new StreamWriter(PathResults, true))
            {
                writer.WriteLine(string.Join(" ", commandResults));
            }
        }

        private string ExecuteCommand(string[] commandParams)
        {
            switch (commandParams[0])
            {
                case "INSERT":
                    {
                        this.Rope.Insert(commandParams[1]);
                        return "OK";
                    }

                case "APPEND":
                    {
                        this.Rope.Append(commandParams[1]);
                        return "OK";
                    }

                case "DELETE":
                    {
                        try
                        {
                            int startIndex = int.Parse(commandParams[1]);
                            int count = int.Parse(commandParams[2]);
                            this.Rope.Delete(startIndex, count);
                            return "OK";
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return "ERROR";
                        }
                        
                    }

                case "PRINT":
                    {
                        this.Rope.Print();
                        return null;
                    }

                default:
                    {
                        throw new ArgumentException("Invalid command");
                    }
            }
        }
    }
}
