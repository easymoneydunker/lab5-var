namespace Lab5;

public class EventThread : MoveThread
{
    private readonly Bitmap b0;
    private readonly Bitmap b1;
    private readonly ManualResetEvent e = new ManualResetEvent(false);
    private int count;

    public int X;
    public int Y;
    public Bitmap Picture;

    public EventThread(Bitmap empty, Bitmap withPassenger, PictureBox box) : base(box)
    {
        b0 = empty;
        b1 = withPassenger;
        Picture = b0;
        X = -b0.Width;
        Y = box.Height - b0.Height - 10;
    }

    public int Count
    {
        get { return Volatile.Read(ref count); }
    }

    public void AddPassenger()
    {
        Interlocked.Increment(ref count);
        e.Set();
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            while (!Terminated && X <= Box.Width - b0.Width - 50)
                Synchronize(DoVisualSwap);

            WaitPassenger();

            while (!Terminated && X >= -b0.Width)
                Synchronize(DoVisual);
        }
        e.Dispose();
    }

    private void WaitPassenger()
    {
        e.Reset();
        while (!Terminated && Count <= 0)
        {
            WaitHandle.WaitAny(new WaitHandle[] { e, StopEvent });
            e.Reset();
        }

        if (!Terminated)
            Interlocked.Decrement(ref count);
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
