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
            thread1 = new CriticalThread(taxi00, taxi01, pictureBox1, section);
            thread2 = new CriticalThread(rtaxi00, rtaxi01, pictureBox1, section);
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
        label1.Text = "Такси (жёлтое): " + (ThreadPriority)trackBar1.Value;
        label2.Text = "Машина (красная): " + (ThreadPriority)trackBar2.Value;
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        if (thread1 != null)
            e.Graphics.DrawImage(thread1.Picture, thread1.X, thread1.Y);
        if (thread2 != null)
            e.Graphics.DrawImage(thread2.Picture, thread2.X, thread2.Y);
    }

    private void CriticalForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
    }
}
