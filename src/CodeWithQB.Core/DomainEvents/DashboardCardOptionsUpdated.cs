namespace CodeWithQB.Core.DomainEvents
{
    public class DashboardCardOptionsUpdated: DomainEvent
    {
        public DashboardCardOptionsUpdated(int top, int left, int height, int width)
        {
            Top = top;
            Left = left;
            Height = height;
            Width = width;
        }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
