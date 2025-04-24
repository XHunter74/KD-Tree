namespace KDTree;

public static class Utils
{
    public static List<Point> GetRandomPoints(int count, int maxX, int maxY)
    {
        var rnd = new Random();
        var list = new List<Point>();
        for (int i = 0; i < count; i++)
            list.Add(new Point(rnd.NextDouble() * maxX, rnd.NextDouble() * maxY));
        return list;
    }

    public static double GetDistance(Point a, Point b) =>
        Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

    public static List<Point> ReadPointsFromFile(string path)
    {
        var list = new List<Point>();
        foreach (var line in File.ReadAllLines(path))
        {
            var parts = line.Split(':');
            list.Add(new Point(double.Parse(parts[0]), double.Parse(parts[1])));
        }
        return list;
    }

    public static void WritePointsToFile(string path, List<Point> points)
    {
        using var writer = new StreamWriter(path);
        foreach (var p in points)
            writer.WriteLine($"{p.X}:{p.Y}");
    }

    public static Point GetNearestFromArray(List<Point> points, Point center)
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