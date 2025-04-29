using System;
using System.Collections.Generic;
using System.Linq;

namespace CPU_Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample processes
            var processes = new List<Process>
            {
                new Process(1, 0, 6, 3),
                new Process(2, 2, 8, 2),
                new Process(3, 4, 7, 1),
                new Process(4, 5, 3, 4),
                new Process(5, 6, 4, 5)
            };

            Console.WriteLine("CPU Scheduling Simulator");
            Console.WriteLine("=======================");
            Console.WriteLine();

            RunScheduler("First Come First Served (FCFS)", processes, scheduler => scheduler.FCFS());
            RunScheduler("Shortest Job First (SJF)", processes, scheduler => scheduler.SJF());
            RunScheduler("Round Robin (Quantum = 2)", processes, scheduler => scheduler.RoundRobin(2));
            RunScheduler("Priority Scheduling", processes, scheduler => scheduler.Priority());
            RunScheduler("Shortest Remaining Time First (SRTF)", processes, scheduler => scheduler.SRTF());
            RunScheduler("Multi-Level Feedback Queue (MLFQ)", processes, scheduler => scheduler.MLFQ(new int[] { 2, 4, 8 }));
        }

        static void RunScheduler(string title, List<Process> originalProcesses, Action<Scheduler> schedule)
        {
            // Deep copy of the original processes
            var processesCopy = originalProcesses.Select(p => new Process(p.ProcessId, p.ArrivalTime, p.BurstTime, p.Priority)).ToList();
            
            // Create a new scheduler instance with fresh processes
            var scheduler = new Scheduler(processesCopy);

            Console.WriteLine(title + ":");
            schedule(scheduler);
            DisplayResults(scheduler);
            Console.WriteLine();
        }

        static void DisplayResults(Scheduler scheduler)
        {
            double avgWaitingTime, avgTurnaroundTime, cpuUtilization, throughput;
            scheduler.CalculateMetrics(out avgWaitingTime, out avgTurnaroundTime, 
                                     out cpuUtilization, out throughput);

            Console.WriteLine($"Average Waiting Time: {avgWaitingTime:F2}");
            Console.WriteLine($"Average Turnaround Time: {avgTurnaroundTime:F2}");
            Console.WriteLine($"CPU Utilization: {cpuUtilization:F2}%");
            Console.WriteLine($"Throughput: {throughput:F2} processes per time unit");
        }
    }
} 