namespace Lab5;

public partial class CriticalForm : Form
{
    private readonly object section = new object();
    private readonly Bitmap taxi00 = Pictures.Car(Color.Gold, true);
    private readonly Bitmap taxi01 = Pictures.Car(Color.Gold, false);
    private readonly Bitmap rtaxi00 = Pictures.Car(Color.Red, true);
    private readonly Bitmap rtaxi01 = Pictures.Car(Color.Red, false);

    private CriticalThread thread1;
    private CriticalThread thread2;

    public CriticalForm()
    {
        InitializeComponent();
        ShowPriorities();
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        if (thread1 == null)
        {
            thread1 = new CriticalThread("Такси", taxi00, taxi01, pictureBox1, section, 15);
            thread2 = new CriticalThread("Машина", rtaxi00, rtaxi01, pictureBox1, section, 70);
            thread1.Start();
            thread2.Start();
            thread1.Priority = (ThreadPriority)trackBar1.Value;
            thread2.Priority = (ThreadPriority)trackBar2.Value;
        }
        else
        {
            thread1.Resume();
            thread2.Resume();
        }

        buttonStart.Enabled = false;
        buttonFinish.Enabled = true;
        buttonFinish.Text = "Finish (пауза)";
    }

    private void buttonFinish_Click(object sender, EventArgs e)
    {
        if (thread1.Suspended)
        {
            StopThreads();
            buttonFinish.Enabled = false;
        }
        else
        {
            thread1.Suspend();
            thread2.Suspend();
            buttonFinish.Text = "Finish (завершить)";
        }

        buttonStart.Enabled = true;
    }

    private void StopThreads()
    {
        if (thread1 == null)
            return;

        thread1.Terminate();
        thread2.Terminate();
        thread1.WaitFor();
        thread2.WaitFor();
        thread1 = null;
        thread2 = null;
        pictureBox1.Invalidate();
    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
        if (thread1 != null)
            thread1.Priority = (ThreadPriority)trackBar1.Value;
        ShowPriorities();
    }

    private void trackBar2_Scroll(object sender, EventArgs e)
    {
        if (thread2 != null)
            thread2.Priority = (ThreadPriority)trackBar2.Value;
        ShowPriorities();
    }

    private void ShowPriorities()
    {
        label1.Text = "Приоритет такси: " + (ThreadPriority)trackBar1.Value;
        label2.Text = "Приоритет машины: " + (ThreadPriority)trackBar2.Value;
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        int left = CriticalThread.BridgeLeft;
        int width = CriticalThread.BridgeRight - CriticalThread.BridgeLeft;

        g.FillRectangle(Brushes.DimGray, 0, 10, pictureBox1.Width, 50);
        g.FillRectangle(Brushes.DimGray, 0, 65, pictureBox1.Width, 50);

        CriticalThread owner = null;
        if (thread1 != null && thread1.OnBridge)
            owner = thread1;
        if (thread2 != null && thread2.OnBridge)
            owner = thread2;

        using (SolidBrush bridge = new SolidBrush(owner == null ? Color.FromArgb(120, Color.LimeGreen) : Color.FromArgb(120, Color.OrangeRed)))
            g.FillRectangle(bridge, left, 5, width, 115);
        g.DrawRectangle(Pens.Black, left, 5, width, 115);
        g.DrawString("МОСТ (критическая секция)", Font, Brushes.White, left + 4, 47);

        if (thread1 != null)
            g.DrawImage(thread1.Picture, thread1.X, thread1.Y);
        if (thread2 != null)
            g.DrawImage(thread2.Picture, thread2.X, thread2.Y);

        string bridgeText = owner == null ? "Секция свободна" : "Секция занята: " + owner.Name;
        g.DrawString(bridgeText, Font, owner == null ? Brushes.Green : Brushes.Red, 470, 125);
        DrawStatus(g, thread1, 5, 125);
        DrawStatus(g, thread2, 5, 142);
    }

    private void DrawStatus(Graphics g, CriticalThread t, int x, int y)
    {
        if (t == null)
            return;

        Brush brush = Brushes.Black;
        if (t.OnBridge)
            brush = Brushes.Red;
        else if (t.State.StartsWith("ждёт"))
            brush = Brushes.DarkOrange;
        g.DrawString(t.Name + ": " + t.State + " (проездов: " + t.Passes + ")", Font, brush, x, y);
    }

    private void CriticalForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
    }
}
