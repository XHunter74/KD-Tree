using System.Diagnostics;
using System.Globalization;

namespace KDTree;

class Program
{
    static void Main(string[] args)
    {
        string? filePath = null;
        int pointsAmount = 1_000_000;
        double targetX = 157;
        double targetY = 19;
        int width = 200;
        int height = 200;

        foreach (var arg in args)
        {
            if (arg == "-h" || arg == "/?" || arg == "--help")
            {
                ShowHelp();
                return;
            }
            else if (arg.StartsWith("--file=")) filePath = arg.Substring(7);
            else if (arg.StartsWith("--count=")) pointsAmount = int.Parse(arg.Substring(8));
            else if (arg.StartsWith("--target="))
            {
                var coords = arg.Substring(9).Split(',');
                targetX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                targetY = double.Parse(coords[1], CultureInfo.InvariantCulture);
            }
            else if (arg.StartsWith("--size="))
            {
                var size = arg.Substring(7).Split(',');
                width = int.Parse(size[0]);
                height = int.Parse(size[1]);
            }
        }

        List<Point> points;
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            points = Utils.ReadPointsFromFile(filePath);
        }
        else
        {
            points = Utils.GetRandomPoints(pointsAmount, width, height);
        }

        Point targetPoint = new Point(targetX, targetY);
        Console.WriteLine($"Target Point = {targetPoint.X} : {targetPoint.Y}");

        var sw = Stopwatch.StartNew();
        var kdTree = new KDTree(points);
        Console.WriteLine($"Tree Depth = {kdTree.Depth}");
        Console.WriteLine($"Build tree time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var neighborTree = kdTree.FindNeighbor(targetPoint);
        Console.WriteLine($"Point from Tree = {neighborTree.X} : {neighborTree.Y}");
        Console.WriteLine($"Distance from target point = {Utils.GetDistance(targetPoint, neighborTree)}");
        Console.WriteLine($"Time from Tree: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var neighborArray = Utils.GetNearestFromArray(points, targetPoint);
        Console.WriteLine($"Point from Array = {neighborArray.X} : {neighborArray.Y}");
        Console.WriteLine($"Distance from target point = {Utils.GetDistance(targetPoint, neighborArray)}");
        Console.WriteLine($"Time from Array: {sw.ElapsedMilliseconds} ms");
    }

    static void ShowHelp()
    {
        Console.WriteLine("KDTree Console App Usage:");
        Console.WriteLine("  -h, /?, --help           Show help");
        Console.WriteLine("  --file=<path>            Input file path (optional)");
        Console.WriteLine("  --count=<number>         Number of random points to generate (default: 1_000_000)");
        Console.WriteLine("  --target=x,y             Target point coordinates (default: 157,19)");
        Console.WriteLine("  --size=width,height      Width and height of rectangle for random generation (default: 200x200)");
    }
}