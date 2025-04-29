using System;

namespace CPU_Scheduler
{
    public class Process
    {
        public int ProcessId { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int RemainingTime { get; set; }
        public int WaitingTime { get; set; }
        public int TurnaroundTime { get; set; }
        public int ResponseTime { get; set; }
        public bool HasStarted { get; set; }

        public Process(int id, int arrivalTime, int burstTime, int priority)
        {
            ProcessId = id;
            ArrivalTime = arrivalTime;
            BurstTime = burstTime;
            Priority = priority;
            RemainingTime = burstTime;
            WaitingTime = 0;
            TurnaroundTime = 0;
            ResponseTime = -1; // -1 means not yet responded
            HasStarted = false;
        }

        public void UpdateWaitingTime(int currentTime)
        {
            if (!HasStarted)
            {
                WaitingTime = currentTime - ArrivalTime;
            }
        }

        public void UpdateTurnaroundTime(int currentTime)
        {
            TurnaroundTime = currentTime - ArrivalTime;
        }

        public void UpdateResponseTime(int currentTime)
        {
            if (!HasStarted)
            {
                ResponseTime = currentTime - ArrivalTime;
                HasStarted = true;
            }
        }
    }
} 