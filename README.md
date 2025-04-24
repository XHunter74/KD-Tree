# KD-Tree Application

This is a .NET console application that demonstrates the use of a KD-Tree data structure for efficient nearest neighbor search. The application can generate random points or read points from a file, build a KD-Tree, and find the nearest neighbor to a target point using both the KD-Tree and a brute-force array search.

## Features

- Generate random points within a specified rectangle.
- Read points from a file.
- Build a KD-Tree from a list of points.
- Find the nearest neighbor to a target point using the KD-Tree.
- Compare the performance of KD-Tree search with brute-force array search.

## Requirements

- .NET 8.0 SDK or later

## Usage

Run the application with the following command-line arguments:

```
KD-Tree.exe [options]
```

### Options

- `-h`, `/?`, `--help`: Show help.
- `--file=<path>`: Input file path (optional).
- `--count=<number>`: Number of random points to generate (default: 1,000,000).
- `--target=x,y`: Target point coordinates (default: 157,19).
- `--size=width,height`: Width and height of the rectangle for random generation (default: 200x200).

### Example

Generate 500,000 random points within a 300x300 rectangle and find the nearest neighbor to the point (100, 50):

```
KD-Tree.exe --count=500000 --size=300,300 --target=100,50
```

## Input File Format

The input file should contain points in the following format:

```
x1:y1
x2:y2
...
```

Each line represents a point with its X and Y coordinates separated by a colon (`:`).

## Output

The application outputs the following information:

1. Target point coordinates.
2. KD-Tree depth and build time.
3. Nearest neighbor found using the KD-Tree, its distance from the target point, and the search time.
4. Nearest neighbor found using brute-force array search, its distance from the target point, and the search time.

## Project Structure

- `Program.cs`: Entry point of the application.
- `KDTree.cs`: Implementation of the KD-Tree data structure.
- `KDTreeNode.cs`: Definition of a KD-Tree node.
- `Point.cs`: Definition of a 2D point.
- `Utils.cs`: Utility functions for generating, reading, and writing points.

## License

This project is licensed under the MIT License.