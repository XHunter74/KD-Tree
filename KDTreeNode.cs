namespace KDTree;

public class KDTreeNode
{
    public double Median { get; set; }
    public KDTreeNode Left { get; set; }
    public KDTreeNode Right { get; set; }
    public Point Point { get; set; }
    public int TreeDepth { get; set; }
    public bool IsLeaf => Point != null;

    public KDTreeNode() { }
    public KDTreeNode(Point point) => Point = point;
}