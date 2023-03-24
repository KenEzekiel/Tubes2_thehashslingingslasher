# Tugas Besar 2 TheHashSlingingSlasher

## Table of Content

- [Tugas Besar 2 TheHashSlingingSlasher](#tugas-besar-2-thehashslingingslasher)
  - [Table of Content](#table-of-content)
  - [Project Description](#project-description)
  - [Problem Description](#problem-description)
  - [Program Features](#program-features)
  - [Algorithm Description](#algorithm-description)
  - [Team Member and Responsibilities](#team-member-and-responsibilities)
  - [Program Structure](#program-structure)
  - [Running The Program](#running-the-program)
  - [Libraries Used](#libraries-used)

## Project Description

A GUI Project implementation of DFS and BFS searching algorithm for getting the route that gets all treasures made for the Algorithms and Strategy course, Bandung Institute of Technology, made by Kenneth Ezekiel (13521089), Chiquita Ahsanunnisa (13521129) and Vanessa Rebecca Wiyono (13521151).

## Problem Description

The problem in this project is getting a route that gets all the treasure, using BFS and DFS, given a map in the form of a .txt file.

There is also an option to enable or disable the Traveling Salesperson Problem, which will end the route on the starting node.


## Program Features

* Route finding with Depth-First Search
* Route finding with Breadth-First Search
* Enable or disable the TSP feature to go back to the starting node
* Node heatmap to indicate how many times a node has been visited
* Visualizer for the route finding in DFS, BFS, and TSP
* Execution time for each algorithm
* Slider to adjust the delay time of each step in visualization

## Algorithm Description

The algorithms used in this project is the Depth-First Search algorithm and the Breadth-First Search algorithm, with the addition of optional TSP in each algorithm. 

The Depth-First Search algorithm traverses the nodes and their children in a LIFO order, so that the children of the nodes' children are traversed first until it reaches a deadend, then backtracks to the other children of the first node, and so on and os forth. Thus producing a route that will get all the treasure.

The Breadth-First Search algorithm traverses the node and their children in a FIFO order, so that the childrens of the first node will be traversed first, then their children, and so forth. Thus it will be modified so that after getting each treasure, the search will be reset with the starting node, the treasure node. Hence, it will produce a route that gets all the treasure in the map.

## Team Member and Responsibilities

| Team Member                |   NIM    | Responsibilities                                                                |
| :------------------------- | :------: | :------------------------------------------------------------------------------ |
| Kenneth Ezekiel Suprantoni | 13521089 | Create graph representation of map matrix and DFS algorithm                     |
| Chiquita Ahsanunnisa       | 13521129 | Create input handler from file, map visualization, and BFS algorithm            |
| Vanessa Rebecca Wiyono     | 13521171 | Create GUI and TSP algorithm                                                    |

## Program Structure
.
|   .gitattributes
│   .gitignore
|    README.md
|    Tubes2_stima.sln
│
├───Tubes2_Stima
│   └───Properties
│   │     AssemblyInfo.cs
|   |     Resources.Designer.cs
|   |     Resources.resx
|   |     Settings.Designer.cs
|   |     Settings.settings
│   │
│   └───bin
|   |     debug
|   |     release
|   |         Tubes2_Stima.exe
│   |
|   |
|   └───config
|   |      test.txt
|   |
|   |
|   └───src
|   |       BFS.cs
|   |       Block.cs
|   |       DFS.cs
|   |       Matrix.cs
|   |       Position.cs
|   |       SearchAlgorithm.cs
|   |
|   |
|   └─── visualization
|   |
|   |  App.config
|   |  Form1.Designer.cs
|   |  Form1.cs
|   |  Form1.resx
|   |  Program.cs
|   |  Tubes2_Stima.csproj
|   |  packages.config
|   |  treasure.ico


## Running The Program
1. Buka folder bin/Release pada folder repositori
2. Jalankan file Tubes2_Stima.exe
P.S. program hanya bisa dijalankan pada platform windows

## Libraries Used
* System.Random
* System.Drawing
* System.Collections.Generic
* System.IO
* System.Windows.Forms
* System.Windows.Forms.Timer
