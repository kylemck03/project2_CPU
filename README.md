# CPU Scheduling Simulator

A C# implementation of various CPU scheduling algorithms for educational purposes. This project simulates different CPU scheduling strategies and provides performance metrics for comparison.

## Features

The simulator implements the following CPU scheduling algorithms:

1. **First Come First Served (FCFS)**
   - Processes are executed in the order they arrive
   - Simple and fair, but may lead to poor performance for short processes

2. **Shortest Job First (SJF)**
   - Executes the process with the shortest burst time first
   - Optimal for minimizing average waiting time
   - Non-preemptive version implemented

3. **Round Robin (RR)**
   - Each process gets a fixed time quantum
   - Fair scheduling with good response time
   - Configurable quantum size

4. **Priority Scheduling**
   - Processes are executed based on their priority
   - Lower priority number indicates higher priority
   - Non-preemptive version implemented

5. **Shortest Remaining Time First (SRTF)**
   - Preemptive version of SJF
   - Always executes the process with the shortest remaining time
   - Optimal for minimizing average waiting time

6. **Multi-Level Feedback Queue (MLFQ)**
   - Multiple priority levels with different quantum sizes
   - Processes can move between queues based on their behavior
   - Configurable quantum levels

## Performance Metrics

For each scheduling algorithm, the simulator calculates:
- Average Waiting Time
- Average Turnaround Time
- CPU Utilization
- Throughput (processes per time unit)

## Project Structure

- `Program.cs`: Main entry point with sample process creation and scheduling execution
- `Scheduler.cs`: Implementation of all scheduling algorithms
- `Process.cs`: Process class definition with properties and methods for tracking process metrics

## Requirements

- .NET Core/.NET Framework
- C# development environment

## Usage

1. Clone the repository
2. Open the solution in your preferred C# IDE
3. Build and run the project
4. The program will automatically run all scheduling algorithms with sample processes and display the results

## Sample Output

The program will display results for each scheduling algorithm, showing:
- Algorithm name
- Average waiting time
- Average turnaround time
- CPU utilization
- Throughput

## Customization

To test with different processes, modify the process list in `Program.cs`:

```csharp
var processes = new List<Process>
{
    new Process(1, 0, 6, 3),  // ProcessId, ArrivalTime, BurstTime, Priority
    new Process(2, 2, 8, 2),
    // Add more processes as needed
};
```

## License

This project is open source and available for educational purposes.