namespace Lab5;

public class CriticalThread : MoveThread
{
    public const int BridgeLeft = 250;
    public const int BridgeRight = 410;

    private readonly Bitmap b0;
    private readonly Bitmap b1;
    private readonly object section;

    public readonly string Name;
    public int X;
    public int Y;
    public Bitmap Picture;
    public string State = "стоит";
    public bool OnBridge;
    public int Passes;

    public CriticalThread(string name, Bitmap toRight, Bitmap toLeft, PictureBox box, object section, int y) : base(box)
    {
        Name = name;
        b0 = toRight;
        b1 = toLeft;
        this.section = section;
        Picture = b0;
        X = -b0.Width;
        Y = y;
    }

    protected override void Execute()
    {
        while (!Terminated)
        {
            Drive(1, BridgeLeft - b0.Width);
            CrossBridge(1, BridgeRight);
            Drive(1, Box.Width);

            Drive(-1, BridgeRight);
            CrossBridge(-1, BridgeLeft - b0.Width);
            Drive(-1, -b0.Width);
        }
    }

    private void CrossBridge(int dir, int target)
    {
        State = "ждёт входа в секцию";
        Synchronize(Box.Invalidate);

        lock (section)
        {
            OnBridge = true;
            State = "в критической секции";
            Drive(dir, target);
            OnBridge = false;
            Passes++;
        }

        State = "едет";
    }

    private void Drive(int dir, int target)
    {
        if (!OnBridge)
            State = "едет";
        Picture = dir > 0 ? b0 : b1;

        while (!Terminated && (dir > 0 ? X < target : X > target))
        {
            if (dir > 0)
                Synchronize(DoVisualSwap);
            else
                Synchronize(DoVisual);
        }
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
