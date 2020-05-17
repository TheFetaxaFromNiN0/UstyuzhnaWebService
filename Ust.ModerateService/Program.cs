using System;
using System.Threading;
using System.Threading.Tasks;
using Ust.ModerateService.Models;

namespace Ust.ModerateService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting auto-moderate. Press ctrl+c to stop...");

            var canc = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                canc.Cancel();
            };

            var moderateTask = new Task(() =>
                {
                    new ModerateWoker(canc.Token).RunModerate();
                }
            );

            moderateTask.Start();

            Task.WaitAll(new Task[]
            {
                moderateTask
            });

            Console.WriteLine("Stop service");
        }
    }
}
