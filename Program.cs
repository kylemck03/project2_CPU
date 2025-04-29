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

            // Create a new scheduler
            var scheduler = new Scheduler(processes);

            // Run FCFS
            Console.WriteLine("First Come First Served (FCFS):");
            scheduler.FCFS();
            DisplayResults(scheduler);
            Console.WriteLine();

            // Reset processes
            ResetProcesses(processes);

            // Run SJF
            Console.WriteLine("Shortest Job First (SJF):");
            scheduler.SJF();
            DisplayResults(scheduler);
            Console.WriteLine();

            // Reset processes
            ResetProcesses(processes);

            // Run Round Robin
            Console.WriteLine("Round Robin (Quantum = 2):");
            scheduler.RoundRobin(2);
            DisplayResults(scheduler);
            Console.WriteLine();

            // Reset processes
            ResetProcesses(processes);

            // Run Priority
            Console.WriteLine("Priority Scheduling:");
            scheduler.Priority();
            DisplayResults(scheduler);
            Console.WriteLine();

            // Reset processes
            ResetProcesses(processes);

            // Run SRTF
            Console.WriteLine("Shortest Remaining Time First (SRTF):");
            scheduler.SRTF();
            DisplayResults(scheduler);
            Console.WriteLine();

            // Reset processes
            ResetProcesses(processes);

            // Run MLFQ
            Console.WriteLine("Multi-Level Feedback Queue (MLFQ):");
            int[] quantumLevels = { 2, 4, 8 }; // Quantum levels for each queue
            scheduler.MLFQ(quantumLevels);
            DisplayResults(scheduler);
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

        static void ResetProcesses(List<Process> processes)
        {
            foreach (var process in processes)
            {
                process.RemainingTime = process.BurstTime;
                process.WaitingTime = 0;
                process.TurnaroundTime = 0;
                process.ResponseTime = -1;
                process.HasStarted = false;
            }
        }
    }
} 