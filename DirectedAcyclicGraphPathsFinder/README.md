# Directed Acyclic Graph Paths Finder in C#

## Overview

Created and presented in 11.2023.
This project contains a C# class designed to compute all possible paths between two vertices in a given acyclic directed graph. The graph is represented as vertex pairs, with vertices denoted by random integers. The core functionality of this class is to analyze the graph, provided in the form of a `List<Tuple<int, int>>`, where each `Tuple<int, int>` signifies a directed edge from the first element of the tuple to the second. The objective is to return all possible paths between any two specified vertices in a `List<List<int>>` format.

## Graph Representation

The graph is defined as a list of integer pairs, where each pair (`Tuple<int, int>`) represents a directed edge from the first integer to the second. This approach is used to model the connections and directions in an acyclic directed graph.

```csharp
List<Tuple<int, int>> graph = new List<Tuple<int, int>>();
```

## Example

Consider the graph defined as follows:

```csharp
var graph = new List<(int, int)> { (1, 2), (1, 3), (2, 4), (3, 4) };
```

## Features

- Calculation of all paths between two vertices in an acyclic directed graph.
- Representation of graphs through list of tuples.
- Efficient path finding suitable for acyclic directed graphs.


## Installation

### Prerequisites

- .NET 5.0 SDK or later

### Getting Started

1. Clone the repository to your local machine.
```
git clone https://github.com/eiramada/CodeShowcase.git
```
2. Navigate to the project directory.
```
cd DirectedAcyclicGraphPathsFinder
```
3. Build the project.
 ```
 dotnet build
 ```

