namespace Lab5;

public partial class EventForm : Form
{
    public readonly ManualResetEvent passengerEvent = new ManualResetEvent(false);
    private readonly object stop = new object();
    private int count;

    private readonly Random random = new Random();
    private readonly Bitmap taxi00 = Pictures.Car(Color.Gold, true);
    private readonly Bitmap taxi01 = Pictures.Car(Color.Gold, false, true);
    private readonly Bitmap gtaxi00 = Pictures.Car(Color.LimeGreen, true);
    private readonly Bitmap gtaxi01 = Pictures.Car(Color.LimeGreen, false, true);

    private EventThread thread1;
    private EventThread thread2;

    public EventForm()
    {
        InitializeComponent();
        timer1.Interval = (int)numericUpDown1.Value;
    }

    public int Count
    {
        get { lock (stop) return count; }
    }

    public bool EventIsSet
    {
        get { return passengerEvent.WaitOne(0); }
    }

    public void AddPassenger()
    {
        lock (stop)
        {
            count++;
            passengerEvent.Set();
        }
    }

    public bool TakePassenger()
    {
        lock (stop)
        {
            if (count == 0)
                return false;

            count--;
            if (count == 0)
                passengerEvent.Reset();
            return true;
        }
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        if (thread1 == null)
        {
            thread1 = new EventThread("Жёлтое такси", taxi00, taxi01, pictureBox1, this, 15);
            thread2 = new EventThread("Зелёное такси", gtaxi00, gtaxi01, pictureBox1, this, 70);
            thread1.Start();
            thread2.Start();
        }
        else
        {
            thread1.Resume();
            thread2.Resume();
        }

        timer1.Start();
        buttonStart.Enabled = false;
        buttonFinish.Enabled = true;
        buttonFinish.Text = "Finish (пауза)";
    }

    private void buttonFinish_Click(object sender, EventArgs e)
    {
        timer1.Stop();
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
        timer1.Stop();
        if (thread1 == null)
            return;

        thread1.Terminate();
        thread2.Terminate();
        thread1.WaitFor();
        thread2.WaitFor();
        thread1 = null;
        thread2 = null;

        lock (stop)
        {
            count = 0;
            passengerEvent.Reset();
        }
        pictureBox1.Invalidate();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (random.NextDouble() <= (double)numericUpDown2.Value / 100)
            AddPassenger();
        pictureBox1.Invalidate();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        timer1.Interval = (int)numericUpDown1.Value;
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        int stopX = EventThread.StopX;

        g.FillRectangle(Brushes.DimGray, 0, 10, stopX + 10, 50);
        g.FillRectangle(Brushes.DimGray, 0, 65, stopX + 10, 50);

        g.FillRectangle(Brushes.Gray, stopX + 14, 5, 4, 110);
        g.FillRectangle(Brushes.SteelBlue, stopX + 6, 5, 20, 14);
        g.DrawString("A", Font, Brushes.White, stopX + 10, 5);

        int n = Count;
        for (int k = 0; k < Math.Min(n, 12); k++)
        {
            int x = stopX + 26 + (k % 3) * 24;
            int y = 25 + (k / 3) * 24;
            g.FillEllipse(Brushes.SaddleBrown, x, y, 8, 8);
            g.FillRectangle(Brushes.DarkBlue, x, y + 8, 8, 12);
        }
        if (n > 12)
            g.DrawString("+" + (n - 12), Font, Brushes.Black, stopX + 30, 120);

        if (thread1 != null)
            g.DrawImage(thread1.Picture, thread1.X, thread1.Y);
        if (thread2 != null)
            g.DrawImage(thread2.Picture, thread2.X, thread2.Y);

        bool set = EventIsSet;
        g.FillEllipse(set ? Brushes.LimeGreen : Brushes.Red, 5, 123, 14, 14);
        g.DrawString("Событие e: " + (set ? "установлено (Set)" : "сброшено (Reset)") + ", пассажиров на остановке: " + n,
            Font, Brushes.Black, 22, 122);

        DrawStatus(g, thread1, 5, 140);
        DrawStatus(g, thread2, 5, 158);
    }

    private void DrawStatus(Graphics g, EventThread t, int x, int y)
    {
        if (t == null)
            return;

        Brush brush = t.State.StartsWith("ждёт") ? Brushes.Red : Brushes.Black;
        g.DrawString(t.Name + ": " + t.State + ", перевезено " + t.Delivered, Font, brush, x, y);
    }

    private void EventForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
        passengerEvent.Dispose();
    }
}
