namespace ShoppingCenter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    public class ShoppinCenterMain
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            ShoppingCenter shoppingCenter = new ShoppingCenter();
            int commandsCount = int.Parse(Console.ReadLine());
            int index = 1;
            while (index <= commandsCount)
            {
                string input = Console.ReadLine();
                int spaceIndex = input.IndexOf(' ');
                if (spaceIndex == -1)
                {
                    continue;
                }

                string command = input.Substring(0, spaceIndex);
                string paramsStr = input.Substring(spaceIndex + 1);
                string[] cmdParams = paramsStr.Split(';');
                List<string> commands = new List<string>();
                commands.Add(command);
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    commands.Add(cmdParams[i]);
                }
              
                shoppingCenter.ExecuteCommands(commands);
                index++;
            }
        }
    }
}
