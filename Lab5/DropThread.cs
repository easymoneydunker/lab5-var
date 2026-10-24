namespace Lab5;

public class DropThread : MoveThread
{
    private const int DropHeight = 24;

    private readonly Semaphore h0;
    private readonly PictureBox dBox;

    public int X;
    public int I;
    public bool Up;
    public bool Active;
    public int Frame;

    public DropThread(int num, Semaphore h, PictureBox c, PictureBox d) : base(c)
    {
        X = 32 * (2 * num - 1) - 16;
        h0 = h;
        dBox = d;
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            while (!h0.WaitOne(200))
            {
                if (Terminated)
                    return;
                Synchronize(DoWait);
            }

            try
            {
                Synchronize(DoStart);
                while (!Terminated && I > 0)
                    Synchronize(DoMoveUp);
                while (!Terminated && I < Box.Height - DropHeight)
                    Synchronize(DoMoveDown);
                Synchronize(DoStop);
            }
            finally
            {
                h0.Release();
            }
        }
    }

    private void DoWait()
    {
        Frame++;
        dBox.Invalidate();
    }

    private void DoStart()
    {
        Active = true;
        Up = true;
        I = Box.Height - DropHeight;
        dBox.Invalidate();
        Box.Invalidate();
    }

    private void DoMoveUp()
    {
        I -= 3;
        Box.Invalidate();
    }

    private void DoMoveDown()
    {
        Up = false;
        I += 3;
        Box.Invalidate();
    }

    private void DoStop()
    {
        Active = false;
        dBox.Invalidate();
        Box.Invalidate();
    }
}
