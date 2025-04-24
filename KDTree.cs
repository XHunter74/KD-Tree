namespace KDTree;

public class KDTree
{
    public KDTreeNode Root { get; private set; }
    public int Depth { get; private set; } = 0;

    public KDTree(List<Point> points)
    {
        Root = BuildKDTree(0, points);
    }

    public Point FindNeighbor(Point point)
    {
        var neighbor = FindPossibleNeighbor(Root, point.X, point.Y);
        var distance = GetDistance(point, neighbor);
        var neighbors = FindNeighbors(Root, point, distance);

        foreach (var possibleNeighbor in neighbors)
        {
            var dist = GetDistance(point, possibleNeighbor);
            if (dist < distance)
            {
                distance = dist;
                neighbor = possibleNeighbor;
            }
        }

        return neighbor;
    }

    private static List<Point> FindNeighbors(KDTreeNode tree, Point point, double distance)
    {
        var result = new List<Point>();
        if (tree.Point != null)
        {
            if (GetDistance(point, tree.Point) < distance)
                result.Add(tree.Point);
        }
        else
        {
            double limitPositive, limitNegative;
            if (tree.TreeDepth % 2 == 0)
            {
                limitPositive = point.X + distance;
                limitNegative = point.X - distance;
            }
            else
            {
                limitPositive = point.Y + distance;
                limitNegative = point.Y - distance;
            }

            if (limitPositive >= tree.Median)
                result.AddRange(FindNeighbors(tree.Right, point, distance));
            if (limitNegative < tree.Median)
                result.AddRange(FindNeighbors(tree.Left, point, distance));
        }

        return result;
    }

    private static double GetDistance(Point a, Point b) =>
        Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

    private static Point FindPossibleNeighbor(KDTreeNode tree, double coord1, double coord2)
    {
        if (tree.IsLeaf)
            return tree.Point;

        var subTree = (coord1 < tree.Median) ? tree.Left : tree.Right;
        return FindPossibleNeighbor(subTree, coord2, coord1);
    }

    private KDTreeNode BuildKDTree(int depth, List<Point> points)
    {
        if (points == null || points.Count == 0)
            throw new ArgumentException("Points lis is empty");

        if (points.Count == 1)
        {
            Depth = Math.Max(Depth, depth);
            return new KDTreeNode(points[0]);
        }

        var node = new KDTreeNode { TreeDepth = depth };
        List<Point> sorted;
        double median;
        if (depth % 2 == 0)
        {
            sorted = points.OrderBy(p => p.X).ToList();
            median = GetMedian(sorted.Select(p => p.X).ToList());
        }
        else
        {
            sorted = points.OrderBy(p => p.Y).ToList();
            median = GetMedian(sorted.Select(p => p.Y).ToList());
        }
        var isEven = depth % 2 == 0;
        var left = sorted.Where(p => (isEven ? p.X : p.Y) < median).ToList();
        var right = sorted.Where(p => (isEven ? p.X : p.Y) >= median).ToList();

        node.Median = median;
        node.Left = BuildKDTree(depth + 1, left);
        node.Right = BuildKDTree(depth + 1, right);

        return node;
    }

    /// <summary>
    /// Get the median value from a list of values.
    /// </summary>
    /// <param name="values">Sorted values</param>
    /// <returns></returns>
    private static double GetMedian(List<double> values)
    {
        int n = values.Count;
        return values[n / 2];
    }
}