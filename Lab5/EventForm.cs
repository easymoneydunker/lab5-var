namespace Lab5;

public partial class EventForm : Form
{
    private readonly Random random = new Random();
    private readonly Bitmap taxi00 = Pictures.Car(Color.Gold, true);
    private readonly Bitmap taxi01 = Pictures.Car(Color.Gold, false, true);

    private EventThread thread1;

    public EventForm()
    {
        InitializeComponent();
        timer1.Interval = (int)numericUpDown1.Value;
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        if (thread1 == null)
        {
            thread1 = new EventThread(taxi00, taxi01, pictureBox1);
            thread1.Start();
        }
        else
        {
            thread1.Resume();
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
        thread1.WaitFor();
        thread1 = null;
        labelCount.Text = "Ожидают пассажиров: 0";
        pictureBox1.Invalidate();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (random.NextDouble() <= (double)numericUpDown2.Value / 100)
            thread1.AddPassenger();

        labelCount.Text = "Ожидают пассажиров: " + thread1.Count;
        pictureBox1.Invalidate();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        timer1.Interval = (int)numericUpDown1.Value;
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        int stopX = pictureBox1.Width - 45;
        e.Graphics.FillRectangle(Brushes.Gray, stopX, 5, 4, pictureBox1.Height - 10);
        e.Graphics.FillRectangle(Brushes.SteelBlue, stopX - 8, 5, 20, 14);

        if (thread1 == null)
            return;

        int n = Math.Min(thread1.Count, 6);
        for (int k = 0; k < n; k++)
        {
            int x = stopX + 10 + (k % 2) * 14;
            int y = 24 + (k / 2) * 24;
            e.Graphics.FillEllipse(Brushes.SaddleBrown, x, y, 8, 8);
            e.Graphics.FillRectangle(Brushes.DarkGreen, x, y + 8, 8, 12);
        }

        e.Graphics.DrawImage(thread1.Picture, thread1.X, thread1.Y);
    }

    private void EventForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
    }
}
