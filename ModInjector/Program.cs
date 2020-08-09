using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AgonylModInjector
{
    internal static class Program
    {
        private static void Main()
        {
            if (!File.Exists("Config.json"))
            {
                Console.WriteLine("Config.json not found!");
            }
            else
            {
                Console.WriteLine(@"
                                  _   __  __           _   _____       _           _
     /\                          | | |  \/  |         | | |_   _|     (_)         | |
    /  \   __ _  ___  _ __  _   _| | | \  / | ___   __| |   | |  _ __  _  ___  ___| |_ ___  _ __
   / /\ \ / _` |/ _ \| '_ \| | | | | | |\/| |/ _ \ / _` |   | | | '_ \| |/ _ \/ __| __/ _ \| '__|
  / ____ \ (_| | (_) | | | | |_| | | | |  | | (_) | (_| |  _| |_| | | | |  __/ (__| || (_) | |
 /_/    \_\__, |\___/|_| |_|\__, |_| |_|  |_|\___/ \__,_| |_____|_| |_| |\___|\___|\__\___/|_|
           __/ |             __/ |                                   _/ |
          |___/             |___/                                   |__/
");
                foreach (var config in JsonConvert.DeserializeObject<List<Config>>(File.ReadAllText("Config.json")))
                {
                    if (!File.Exists(config.DllPath))
                    {
                        Console.WriteLine("Skipping mod injection of {0} as DLL specified was not found!", config.ExecutableName);
                        continue;
                    }

                    var result = DllInjectionHelper.InjectDLL(config.DllPath, config.ExecutableName);
                    if (result)
                    {
                        Console.WriteLine("Successfully injected {0} into {1}!", Path.GetFileName(config.DllPath), config.ExecutableName);
                    }
                    else
                    {
                        Console.WriteLine("Could not inject {0} into {1}!", Path.GetFileName(config.DllPath), config.ExecutableName);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
