## Water Jug Challenge Documentation

This documentation provides an overview of the Water Jug Challenge project, its purpose, and instructions on how to use it.

### Purpose
The Water Jug Challenge is a problem-solving exercise that involves using two jugs of different sizes to measure a specific amount of water. The goal of this project is to provide a solution to the Water Jug Challenge, allowing users to input the sizes of the jugs and the desired amount of water, and getting the steps required to reach that amount.

### Functionality
The Water Jug Challenge program allows users to:
- Input the sizes of the two jugs.
- Specify the desired amount of water to be measured.
- Get a step-by-step solution to reach the desired amount using the provided jugs.

## How to Use
To use the program, follow these steps:
1. Clone the repository to your local machine.
2. Open the project in your preferred integrated development environment (IDE), such as Visual Studio.
3. Restore any NuGet packages required by the project. You can do this by right-clicking on the project in Visual Studio's Solution Explorer and selecting "Restore NuGet Packages".
4. Build the project by selecting "Build" -> "Build Solution" in Visual Studio's menu.
5. Run the program by selecting "Debug" -> "Start Debugging" or pressing F5 in Visual Studio. Alternatively, you can locate the compiled executable file (usually found in the "bin" folder of the project) and run it directly.
6. Follow the intructions showed in the user interface provided by swagger so you can display the desire data.

## Dependencies
The C# .NET Entity Framework program may have dependencies specific to its requirements. These dependencies are specified in the appsettings.json. Make sure you have the necessary dependencies installed before running the program.

## Algorithm Overview
The Breadth-First Search (BFS) algorithm is applied to solve the Water Jug Problem. BFS is a search technique used to explore all possible states in a graph or tree. In this context, each state represents a combination of the water levels in both jugs.

## How BFS is applied
1. Create an empty queue and add the initial state (where both jugs are empty) to the queue.
2. While the queue is not empty, perform the following steps:
   - Dequeue the first state from the queue.
   - Generate all possible states reachable from the current state by applying the following operations:
     - Fill one of the jugs completely.
     - Empty one of the jugs completely.
     - Transfer water from one jug to another until either the first jug is empty or the second jug is full.
   - For each generated state, check if it is the goal state (i.e., if it contains the desired amount of water). If so, the shortest solution has been found, and the algorithm can be stopped.
   - If the state is not the goal state, add it to the queue to be explored in subsequent iterations.

## Benefits of BFS
The BFS algorithm guarantees finding the shortest possible solution as it explores all possible states in a level-by-level order. This means that it will first explore all states reachable from the initial state, then states reachable from those states, and so on.

## Usage
To use this algorithm for solving the Water Jug Problem, follow these steps:
1. Define the capacities of the two jugs and the desired amount of water.
2. Implement the BFS algorithm described above, considering the necessary operations to generate all possible states.
3. Run the algorithm with the initial state (both jugs empty) and check if a solution is found.
4. If a solution is found, retrieve the sequence of steps from the initial state to the goal state.
5. Optionally, optimize the algorithm by using data structures to store visited states and avoid redundant exploration.

## Conclusion
The BFS algorithm is a suitable choice for solving the Water Jug Problem as it guarantees finding the shortest solution and systematically explores all possible states. By following the steps outlined above, you can efficiently solve this problem and find the shortest sequence of steps to measure a desired amount of water using two jugs.

## Endpoints

### POST /

This endpoint solves the water container problem. You must submit the following data in the body of the request as JSON:

```json
\{
  "jugX": 2,
  "jugY": 10,
  "jugZ": 4
\}
```
- jugX: Size of the first container.
- jugY: Size of the second container.
- jugZ: Desired amount of water.

The answer will be a JSON object with the solution to the problem:

```json
\{
    {
        "jugX": 2,
        "jugY": 0,
        "explanation": "Filling JugX"
    },
    {
        "jugX": 0,
        "jugY": 2,
        "explanation": "Transfer from JugX to JugY"
    },
    {
        "jugX": 2,
        "jugY": 2,
        "explanation": "Filling JugX"
    },
    {
        "jugX": 0,
        "jugY": 4,
        "explanation": "Transfer from JugX to JugY"
    }
\}

```
