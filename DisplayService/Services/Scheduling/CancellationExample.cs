using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayService.Services.Scheduling
{
    public class CancellationExample
    {

        public void Main()
        {
            // Define the cancellation token.
            var source = new CancellationTokenSource();
            var token = source.Token;



            var rnd = new Random();
            var lockObj = new object();

            var tasks = new List<Task<int[]>>();
            var factory = new TaskFactory(token);
            for (var taskCtr = 0; taskCtr <= 10; taskCtr++)
            {
                var iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(() =>
                {
                    int value;
                    var values = new int[10];
                    for (var ctr = 1; ctr <= 10; ctr++)
                    {
                        lock (lockObj)
                        {
                            value = rnd.Next(0, 101);
                        }
                        if (value == 0)
                        {
                            source.Cancel();
                            Console.WriteLine("Cancelling at task {0}", iteration);
                            break;
                        }
                        values[ctr - 1] = value;
                    }
                    return values;
                }, token));

            }
            try
            {
                var fTask = factory.ContinueWhenAll(tasks.ToArray(),
                                                             (results) =>
                                                             {
                                                                 Console.WriteLine("Calculating overall mean...");
                                                                 long sum = 0;
                                                                 var n = 0;
                                                                 foreach (var t in results)
                                                                 {
                                                                     foreach (var r in t.Result)
                                                                     {
                                                                         sum += r;
                                                                         n++;
                                                                     }
                                                                 }
                                                                 return sum / (double)n;
                                                             }, token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                    {
                        Console.WriteLine("Unable to compute mean: {0}",
                                          ((TaskCanceledException)e).Message);
                    }
                    else
                    {
                        Console.WriteLine("Exception: " + e.GetType().Name);
                    }
                }
            }
            finally
            {
                source.Dispose();
            }
        }
    }
    // Repeated execution of the example produces output like the following:
    //       Cancelling at task 5
    //       Unable to compute mean: A task was canceled.
    //       
    //       Cancelling at task 10
    //       Unable to compute mean: A task was canceled.
    //       
    //       Calculating overall mean...
    //       The mean is 5.29545454545455.
    //       
    //       Cancelling at task 4
    //       Unable to compute mean: A task was canceled.
    //       
    //       Cancelling at task 5
    //       Unable to compute mean: A task was canceled.
    //       
    //       Cancelling at task 6
    //       Unable to compute mean: A task was canceled.
    //       
    //       Calculating overall mean...
    //       The mean is 4.97363636363636.
    //       
    //       Cancelling at task 4
    //       Unable to compute mean: A task was canceled.
    //       
    //       Cancelling at task 5
    //       Unable to compute mean: A task was canceled.
    //       
    //       Cancelling at task 4
    //       Unable to compute mean: A task was canceled.
    //       
    //       Calculating overall mean...
    //       The mean is 4.86545454545455.
}

