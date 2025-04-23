using System.Diagnostics;

namespace KDTree;

class Program
{
    static void Main()
    {
        List<Point> points;
        string filePath = "points.txt";

        if (!File.Exists(filePath))
        {
            points = Utils.GetRandomPoints(1_000_000, 200, 200);
        }
        else
        {
            points = Utils.ReadPointsFromFile(filePath);
        }

        Point targetPoint = new Point(157f, 19f);
        Console.WriteLine($"Target Point = {targetPoint.X} : {targetPoint.Y}");

        var sw = Stopwatch.StartNew();
        var kdTree = new KDTree(points);
        Console.WriteLine($"Tree Depth = {kdTree.TreeDepth}");
        Console.WriteLine($"Build tree time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var neighborTree = kdTree.FindNeighbor(targetPoint);
        Console.WriteLine($"Point from Tree = {neighborTree.X} : {neighborTree.Y}");
        Console.WriteLine($"Distance from target point = {Utils.GetDistance(targetPoint, neighborTree)}");
        Console.WriteLine($"Time from Tree: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var neighborArray = GetNearestFromArray(points, targetPoint);
        Console.WriteLine($"Point from Array = {neighborArray.X} : {neighborArray.Y}");
        Console.WriteLine($"Distance from target point = {Utils.GetDistance(targetPoint, neighborArray)}");
        Console.WriteLine($"Time from Array: {sw.ElapsedMilliseconds} ms");
    }

    static Point GetNearestFromArray(List<Point> points, Point center)
    {
        Point nearest = null;
        double minDist = double.MaxValue;
        foreach (var point in points)
        {
            var dist = Utils.GetDistance(center, point);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = point;
            }
        }
        return nearest;
    }
}