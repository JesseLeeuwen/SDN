using System;
using System.Diagnostics;

using SDN;

namespace test
{
    public delegate void TimedCall();

    class Program
    {
        private static void Main(string[] args)
        {
			string text = System.IO.File.ReadAllText(@"SDN.txt");
            Parser parser = null;
            const int testCount = 2000;

            // performance check sdn parsing
            TimedCall( "parse sdn file", ()=> parser = new Parser(text), testCount);
            // performance check getters
            TimedCall( "invenory [0]", ()=> parser.GetToken("scene.objects.player.inventory[0]"), testCount );
            TimedCall( "data [0]", ()=> parser.GetToken("data[0]"), testCount );
        }

        private static void TimedCall(string p_name, TimedCall p_call, int p_amount = 1)
        {
            // source: http://www.bradoncode.com/blog/2012/02/benchmarking-methods-in-c-made-easier.html

            // clean up
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            System.GC.Collect();
            //warm up
            p_call();

            Stopwatch stopwatch = Stopwatch.StartNew();
            for(int i = 0; i < p_amount; ++i)
                p_call.Invoke();

            stopwatch.Stop();
            Console.WriteLine("operation {1} executed: {2} times in: {0}", stopwatch.Elapsed, p_name, p_amount);
            Console.WriteLine("avarage execution time: {0} \n", stopwatch.Elapsed / p_amount);
        }
    }
}
