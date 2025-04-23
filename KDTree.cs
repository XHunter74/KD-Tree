namespace KDTree;

public class KDTree
{
    public KDTreeNode RootNode { get; private set; }
    public int TreeDepth { get; private set; } = 0;

    public KDTree(List<Point> points)
    {
        RootNode = BuildKDTree(0, points);
    }

    public Point FindNeighbor(Point point)
    {
        var neighbor = FindPossibleNeighbor(point);
        var distance = GetDistance(point, neighbor);
        var neighbors = FindNeighborsInt(RootNode, point, distance);

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

    private static List<Point> FindNeighborsInt(KDTreeNode tree, Point point, double distance)
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
                result.AddRange(FindNeighborsInt(tree.Right, point, distance));
            if (limitNegative < tree.Median)
                result.AddRange(FindNeighborsInt(tree.Left, point, distance));
        }

        return result;
    }

    private static double GetDistance(Point a, Point b) =>
        Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

    private Point FindPossibleNeighbor(Point point) =>
        FindPossibleNeighborInt(RootNode, point.X, point.Y);

    private static Point FindPossibleNeighborInt(KDTreeNode tree, double coord1, double coord2)
    {
        if (tree.Point != null)
            return tree.Point;

        var subTree = (coord1 < tree.Median) ? tree.Left : tree.Right;
        return FindPossibleNeighborInt(subTree, coord2, coord1);
    }

    private KDTreeNode BuildKDTree(int depth, List<Point> points)
    {
        if (points.Count == 1)
        {
            TreeDepth = Math.Max(TreeDepth, depth);
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

        var left = sorted.Where(p => (depth % 2 == 0 ? p.X : p.Y) < median).ToList();
        var right = sorted.Where(p => (depth % 2 == 0 ? p.X : p.Y) >= median).ToList();

        node.Median = median;
        node.Left = BuildKDTree(depth + 1, left);
        node.Right = BuildKDTree(depth + 1, right);

        return node;
    }

    private static double GetMedian(List<double> values)
    {
        values.Sort();
        int n = values.Count;
        return n % 2 == 0
            ? (values[n / 2 - 1] + values[n / 2]) / 2.0
            : values[n / 2];
    }
}