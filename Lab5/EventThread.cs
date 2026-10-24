namespace Lab5;

public class EventThread : MoveThread
{
    public const int StopX = 560;

    private readonly Bitmap b0;
    private readonly Bitmap b1;
    private readonly EventForm form;

    public readonly string Name;
    public int X;
    public int Y;
    public Bitmap Picture;
    public string State = "стоит";
    public int Delivered;

    public EventThread(string name, Bitmap empty, Bitmap withPassenger, PictureBox box, EventForm form, int y) : base(box)
    {
        Name = name;
        b0 = empty;
        b1 = withPassenger;
        this.form = form;
        Picture = b0;
        X = -b0.Width;
        Y = y;
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            State = "едет к остановке";
            Picture = b0;
            while (!Terminated && X < StopX - b0.Width)
                Synchronize(DoVisualSwap);

            State = "ждёт пассажира (WaitOne)";
            Synchronize(Box.Invalidate);
            if (!WaitPassenger())
                break;

            State = "везёт пассажира";
            Picture = b1;
            while (!Terminated && X > -b0.Width)
                Synchronize(DoVisual);

            if (!Terminated)
                Delivered++;
        }
    }

    private bool WaitPassenger()
    {
        while (!Terminated)
        {
            if (form.TakePassenger())
                return true;
            WaitHandle.WaitAny(new WaitHandle[] { form.passengerEvent, StopEvent });
        }
        return false;
    }

    private void DoVisualSwap()
    {
        X += 4;
        Box.Invalidate();
    }

    private void DoVisual()
    {
        X -= 4;
        Box.Invalidate();
    }
}
