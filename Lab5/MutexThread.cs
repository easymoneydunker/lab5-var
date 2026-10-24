namespace Lab5;

public class MutexThread : MoveThread
{
    private readonly Bitmap b0;
    private readonly Bitmap b1;
    private readonly MutexForm form;
    private readonly AutoResetEvent wake = new AutoResetEvent(false);

    public int X;
    public int Y;
    public Bitmap Picture;

    public MutexThread(Bitmap toRight, Bitmap toLeft, PictureBox box, MutexForm form, int y) : base(box)
    {
        b0 = toRight;
        b1 = toLeft;
        this.form = form;
        Picture = b0;
        X = -b0.Width;
        Y = y;
    }

    public void Wake()
    {
        wake.Set();
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            bool owned = WaitHandle.WaitAny(new WaitHandle[] { form.m, StopEvent }, form.time) == 0;
            try
            {
                while (!Terminated && X <= Box.Width)
                    Synchronize(DoVisualSwap);
            }
            finally
            {
                if (owned)
                    form.m.ReleaseMutex();
            }
            WaitForTimer();

            owned = WaitHandle.WaitAny(new WaitHandle[] { form.m, StopEvent }, form.time) == 0;
            try
            {
                while (!Terminated && X >= -b0.Width)
                    Synchronize(DoVisual);
            }
            finally
            {
                if (owned)
                    form.m.ReleaseMutex();
            }
            WaitForTimer();
        }
    }

    private void WaitForTimer()
    {
        wake.Reset();
        WaitHandle.WaitAny(new WaitHandle[] { wake, StopEvent });
    }

    private void DoVisualSwap()
    {
        Picture = b0;
        X += 4;
        Box.Invalidate();
    }

    private void DoVisual()
    {
        Picture = b1;
        X -= 4;
        Box.Invalidate();
    }
}
