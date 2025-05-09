using System;
using System.Collections.Generic;
using System.Linq;

namespace CPU_Scheduler
{
    public class Scheduler
    {
        private List<Process> processes;
        private int currentTime;
        private int totalProcesses;
        private int completedProcesses;
        private int totalBurstTime;
        private int idleTime;

        public Scheduler(List<Process> processes)
        {
            this.processes = processes;
            currentTime = 0;
            totalProcesses = processes.Count;
            completedProcesses = 0;
            totalBurstTime = processes.Sum(p => p.BurstTime);
            idleTime = 0;
        }

        public void FCFS()
        {
            var readyQueue = new Queue<Process>(processes.OrderBy(p => p.ArrivalTime));
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                if (readyQueue.Count > 0)
                {
                    var currentProcess = readyQueue.Peek();
                    if (currentProcess.ArrivalTime <= currentTime)
                    {
                        currentProcess = readyQueue.Dequeue();
                        currentProcess.UpdateResponseTime(currentTime);
                        currentProcess.UpdateWaitingTime(currentTime);

                        // Execute the process
                        currentTime += currentProcess.BurstTime;
                        currentProcess.UpdateTurnaroundTime(currentTime);
                        completedProcesses++;
                    }
                    else
                    {
                        currentTime++;
                        idleTime++;
                    }
                }
                else
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void SJF()
        {
            var readyQueue = new List<Process>(processes);
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                var availableProcesses = readyQueue.Where(p => p.ArrivalTime <= currentTime).ToList();
                if (availableProcesses.Count > 0)
                {
                    var currentProcess = availableProcesses.OrderBy(p => p.BurstTime).First();
                    readyQueue.Remove(currentProcess);
                    currentProcess.UpdateResponseTime(currentTime);
                    currentProcess.UpdateWaitingTime(currentTime);

                    // Execute the process
                    currentTime += currentProcess.BurstTime;
                    currentProcess.UpdateTurnaroundTime(currentTime);
                    completedProcesses++;
                }
                else
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void RoundRobin(int quantum)
        {
            var readyQueue = new Queue<Process>(processes.OrderBy(p => p.ArrivalTime));
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                if (readyQueue.Count > 0)
                {
                    var currentProcess = readyQueue.Dequeue();
                    if (currentProcess.ArrivalTime <= currentTime)
                    {
                        currentProcess.UpdateResponseTime(currentTime);
                        currentProcess.UpdateWaitingTime(currentTime);

                        int executionTime = Math.Min(quantum, currentProcess.RemainingTime);
                        currentTime += executionTime;
                        currentProcess.RemainingTime -= executionTime;

                        if (currentProcess.RemainingTime > 0)
                        {
                            readyQueue.Enqueue(currentProcess);
                        }
                        else
                        {
                            currentProcess.UpdateTurnaroundTime(currentTime);
                            completedProcesses++;
                        }
                    }
                    else
                    {
                        readyQueue.Enqueue(currentProcess);
                        currentTime++;
                        idleTime++;
                    }
                }
                else
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void Priority()
        {
            var readyQueue = new List<Process>(processes);
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                var availableProcesses = readyQueue.Where(p => p.ArrivalTime <= currentTime).ToList();
                if (availableProcesses.Count > 0)
                {
                    var currentProcess = availableProcesses.OrderBy(p => p.Priority).First();
                    readyQueue.Remove(currentProcess);
                    currentProcess.UpdateResponseTime(currentTime);
                    currentProcess.UpdateWaitingTime(currentTime);

                    // Execute the process
                    currentTime += currentProcess.BurstTime;
                    currentProcess.UpdateTurnaroundTime(currentTime);
                    completedProcesses++;
                }
                else
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void SRTF()
        {
            var readyQueue = new List<Process>(processes);
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                var availableProcesses = readyQueue.Where(p => p.ArrivalTime <= currentTime).ToList();
                if (availableProcesses.Count > 0)
                {
                    var currentProcess = availableProcesses.OrderBy(p => p.RemainingTime).First();
                    currentProcess.UpdateResponseTime(currentTime);
                    currentProcess.UpdateWaitingTime(currentTime);

                    // Execute for 1 time unit
                    currentTime++;
                    currentProcess.RemainingTime--;

                    if (currentProcess.RemainingTime == 0)
                    {
                        currentProcess.UpdateTurnaroundTime(currentTime);
                        readyQueue.Remove(currentProcess);
                        completedProcesses++;
                    }
                }
                else
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void MLFQ(int[] quantumLevels)
        {
            var queues = new List<Queue<Process>>();
            for (int i = 0; i < quantumLevels.Length; i++)
            {
                queues.Add(new Queue<Process>());
            }

            // Initialize by adding all processes to the highest priority queue
            queues[0] = new Queue<Process>(processes.OrderBy(p => p.ArrivalTime));
            
            currentTime = 0;
            completedProcesses = 0;

            while (completedProcesses < totalProcesses)
            {
                bool processExecuted = false;

                // Check for processes in each queue, starting from highest priority
                for (int i = 0; i < queues.Count; i++)
                {
                    while (queues[i].Count > 0)
                    {
                        var currentProcess = queues[i].Peek();
                        if (currentProcess.ArrivalTime <= currentTime)
                        {
                            currentProcess = queues[i].Dequeue();
                            currentProcess.UpdateResponseTime(currentTime);
                            currentProcess.UpdateWaitingTime(currentTime);

                            int executionTime = Math.Min(quantumLevels[i], currentProcess.RemainingTime);
                            currentTime += executionTime;
                            currentProcess.RemainingTime -= executionTime;

                            if (currentProcess.RemainingTime > 0)
                            {
                                // Move to lower priority queue if not the lowest
                                if (i < queues.Count - 1)
                                {
                                    queues[i + 1].Enqueue(currentProcess);
                                }
                                else
                                {
                                    // If at lowest priority, stay in same queue
                                    queues[i].Enqueue(currentProcess);
                                }
                            }
                            else
                            {
                                currentProcess.UpdateTurnaroundTime(currentTime);
                                completedProcesses++;
                            }
                            processExecuted = true;
                            break;
                        }
                        else
                        {
                            break; // Wait for this process's arrival time
                        }
                    }
                    if (processExecuted) break;
                }

                if (!processExecuted)
                {
                    currentTime++;
                    idleTime++;
                }
            }
        }

        public void CalculateMetrics(out double avgWaitingTime, out double avgTurnaroundTime, 
                                   out double cpuUtilization, out double throughput)
        {
            avgWaitingTime = processes.Average(p => p.WaitingTime);
            avgTurnaroundTime = processes.Average(p => p.TurnaroundTime);
            cpuUtilization = ((double)(currentTime - idleTime) / currentTime) * 100;
            throughput = (double)totalProcesses / currentTime;
        }
    }
} 