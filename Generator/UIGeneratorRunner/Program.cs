﻿using System;
using System.Threading.Tasks;
using UIGenerator;
using RazorLight;

namespace UIGeneratorRunner
{
    class Program
    {
        private static readonly string OutputFolder = $"Frontend{DateTime.Now:MMddyyyyhhmmss}";

        static async Task Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specify source library as a parameter.");
                return;
            }

            var sourceLibrary = args[0];

            try
            {
                Console.WriteLine("Starting...");
                var engine = CreateEngine();
                var transformer = new UITransformer(engine, sourceLibrary, OutputFolder);
                Console.WriteLine("Loading Assembly...");
                transformer.Initialize();
                Console.WriteLine($"Modules found: {transformer.Modules.Count}");
                Console.WriteLine("Generating...");
                await transformer.Transform();
                Console.WriteLine("Finished!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static RazorLightEngine CreateEngine()
        {
            var absolutePath = $"{System.AppDomain.CurrentDomain.BaseDirectory}";
            var root = $"{absolutePath}Views";
            var engine = new RazorLightEngineBuilder()
                        .UseFilesystemProject(root)
                        .UseMemoryCachingProvider()
                        .Build();
            return engine;
        }
    }
}