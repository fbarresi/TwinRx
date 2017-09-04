﻿using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using TwinCAT.Ads;
using TwinRx;

namespace TestApplication
{
    // Uncomment for TwinCAT2
    // [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MyStruct
    {
        public short myInt;

        [MarshalAs(UnmanagedType.I1)]
        public bool myBool;
    }

    /**
     * Sample making use of test.pro
     **/
    public class Samples
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            var adsClient = new TcAdsClient();
            adsClient.Connect(851); // 801 for TwinCAT 2, 851 for TwinCAT 3

            var client = new TwinRxClient(adsClient);

            var counter = client.ObservableFor<MyStruct>("MAIN.var5", 200);
            var subscription = counter.Select(c => c.myInt + ":" + c.myBool).Subscribe(v => Console.WriteLine("Last 10 values were:" + String.Join(" - ", v)));

            //// Print out 10 values at a time
            //var buffered = counter.Buffer(10);
            //buffered.Subscribe(v => Console.WriteLine("Last 10 values were:" + String.Join(" - ", v)));

            //// Values including timestamp
            //var valuesWithTimestamp = counter.Select(i => new Tuple<short, DateTime>(i, DateTime.Now));
            //valuesWithTimestamp.Subscribe(Console.WriteLine);

            //// Take a single value each second
            //valuesWithTimestamp
            //    .Sample(TimeSpan.FromSeconds(5))
            //    .Subscribe(Console.WriteLine);

            //var myString = client.ObservableFor<string>("MAIN.var2");
            //myString.Subscribe(_ => Console.WriteLine("String has changed! " + _));

            //// Write a value to the PLC periodically
            //var valueEverySecond = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Select(i => (short)i);
            //var writer = client.StreamTo("MAIN.var3", valueEverySecond);

            //// Only even ones
            //var evens = client.ObservableFor<short>("MAIN.var4").Where(i => i % 2 == 0);
            //var evensWithTimestamp = evens
            //    .Timestamp()
            //    .Zip(evens.TimeInterval(), (valWithTimestamp, interval) => new { val = "Even value is " + valWithTimestamp, interval });
            //evensWithTimestamp.Subscribe(Console.WriteLine);

            Thread.Sleep(3000);

            ////// Print out each value as it changes
            //counter.Timestamp().Subscribe(v => Console.WriteLine("Variable is now:" + v + " on " + Thread.CurrentThread.ManagedThreadId));

            //Thread.Sleep(5000);
            //Console.WriteLine("Subscribing second notifier");
            //counter.Timestamp().Subscribe(v => Console.WriteLine("Variable 2 is now:" + v + " on " + Thread.CurrentThread.ManagedThreadId));


            //client.Write<long>("MAIN.var1", 1234); //"blabla!").Subscribe(_ => Console.WriteLine("Done writing on thread " + Thread.CurrentThread.ManagedThreadId));

            Console.WriteLine("Disposing ADS client and reconnecting");
            adsClient.Dispose();


            adsClient = new TcAdsClient();
            adsClient.Connect(851);
            client.Reconnect(adsClient);

            Thread.Sleep(3000);

            subscription.Dispose();
            Console.WriteLine("Disposing again");

            Console.WriteLine("Connected, press key to exit");
            Console.ReadKey();

            //writer.Dispose();
            adsClient.Dispose();
        }
    }
}
