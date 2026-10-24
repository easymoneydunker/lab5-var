namespace Lab5;

public class CriticalThread : MoveThread
{
    private readonly Bitmap b0;
    private readonly Bitmap b1;
    private readonly object section;

    public int X;
    public int Y;
    public Bitmap Picture;

    public CriticalThread(Bitmap toRight, Bitmap toLeft, PictureBox box, object section) : base(box)
    {
        b0 = toRight;
        b1 = toLeft;
        this.section = section;
        Picture = b0;
        X = -b0.Width;
        Y = (box.Height - b0.Height) / 2;
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            lock (section)
            {
                while (!Terminated && X <= Box.Width)
                    Synchronize(DoVisualSwap);
            }
            Thread.Sleep(100);

            lock (section)
            {
                while (!Terminated && X >= -b0.Width)
                    Synchronize(DoVisual);
            }
            Thread.Sleep(100);
        }
    }

    private void DoVisualSwap()
    {
        Picture = b0;
        X += 2;
        Box.Invalidate();
    }

    private void DoVisual()
    {
        Picture = b1;
        X -= 2;
        Box.Invalidate();
    }
}
