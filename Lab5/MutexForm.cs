namespace Lab5;

public partial class MutexForm : Form
{
    public readonly Mutex m = new Mutex(false);
    public volatile int time;

    private readonly Bitmap taxi00 = Pictures.Car(Color.Gold, true);
    private readonly Bitmap taxi01 = Pictures.Car(Color.Gold, false);
    private readonly Bitmap rtaxi00 = Pictures.Car(Color.Red, true);
    private readonly Bitmap rtaxi01 = Pictures.Car(Color.Red, false);

    private MutexThread thread1;
    private MutexThread thread2;

    public MutexForm()
    {
        InitializeComponent();
        time = (int)numericUpDown1.Value;
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        if (thread1 == null)
        {
            thread1 = new MutexThread(taxi00, taxi01, pictureBox1, this, 10);
            thread2 = new MutexThread(rtaxi00, rtaxi01, pictureBox1, this, 60);
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
        pictureBox1.Invalidate();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        thread1.Wake();
        thread2.Wake();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        time = (int)numericUpDown1.Value;
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        if (thread1 != null)
            e.Graphics.DrawImage(thread1.Picture, thread1.X, thread1.Y);
        if (thread2 != null)
            e.Graphics.DrawImage(thread2.Picture, thread2.X, thread2.Y);
    }

    private void MutexForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
        m.Dispose();
    }
}
