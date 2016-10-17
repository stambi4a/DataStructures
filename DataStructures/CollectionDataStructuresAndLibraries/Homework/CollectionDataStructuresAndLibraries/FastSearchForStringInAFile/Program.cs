namespace FastSearchForStringInAFile
{
    using System.Diagnostics;
    using System.IO;

    internal class Program
    {
        private const string SearchTimesPath = "../../SearchTimes.txt";
        private static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter(SearchTimesPath, true))
            {
                StringSearcher searchEngine = new StringSearcher();
                Stopwatch watch = new Stopwatch();
                watch.Start();
                searchEngine.SearchSusbtringsInText();
                watch.Stop();
                var time = watch.Elapsed;
                writer.WriteLine($"Search time using IndexOf(): {time}");
            }                
        }
    }
}
