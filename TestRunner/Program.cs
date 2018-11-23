using System;
using System.IO;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var bin = Environment.CurrentDirectory;
            var adapter = new Gpio.Adapter.GpioAdapter(bin);
            adapter.FileWatchingListening = true;
            Console.ReadLine();
        }
    }
}
