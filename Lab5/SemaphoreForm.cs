namespace Lab5;

public partial class SemaphoreForm : Form
{
    private readonly Bitmap dropUp = Pictures.Drop(true);
    private readonly Bitmap dropDown = Pictures.Drop(false);
    private readonly Bitmap cloudActive = Pictures.Cloud(Color.SteelBlue);
    private readonly Bitmap cloudWait1 = Pictures.Cloud(Color.Silver);
    private readonly Bitmap cloudWait2 = Pictures.Cloud(Color.Gainsboro);

    private readonly DropThread[] thread = new DropThread[10];
    private Semaphore fsemaphore;

    public SemaphoreForm()
    {
        InitializeComponent();
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        if (fsemaphore != null)
        {
            for (int i = 0; i < 10; i++)
                thread[i].Resume();
        }
        else
        {
            int maxCount = (int)numericUpDown1.Value;
            fsemaphore = new Semaphore(maxCount, 10);
            for (int i = 0; i < 10; i++)
            {
                thread[i] = new DropThread(i + 1, fsemaphore, pictureBox1, pictureBox2);
                thread[i].Start();
            }
        }

        buttonStart.Enabled = false;
        buttonFinish.Enabled = true;
        buttonFinish.Text = "Finish (пауза)";
    }

    private void buttonFinish_Click(object sender, EventArgs e)
    {
        if (thread[0].Suspended)
        {
            StopThreads();
            buttonFinish.Enabled = false;
        }
        else
        {
            for (int i = 0; i < 10; i++)
                thread[i].Suspend();
            buttonFinish.Text = "Finish (завершить)";
        }

        buttonStart.Enabled = true;
    }

    private void StopThreads()
    {
        if (fsemaphore == null)
            return;

        for (int i = 0; i < 10; i++)
            thread[i].Terminate();
        for (int i = 0; i < 10; i++)
        {
            thread[i].WaitFor();
            thread[i] = null;
        }

        fsemaphore.Dispose();
        fsemaphore = null;
        pictureBox1.Invalidate();
        pictureBox2.Invalidate();
    }

    private void buttonSleep_Click(object sender, EventArgs e)
    {
        Thread.Sleep((int)numericUpDown2.Value);
    }

    private void pictureBox2_Paint(object sender, PaintEventArgs e)
    {
        foreach (DropThread t in thread)
        {
            if (t == null)
                continue;

            Bitmap cloud;
            if (t.Active)
                cloud = cloudActive;
            else if (t.Frame % 2 == 0)
                cloud = cloudWait1;
            else
                cloud = cloudWait2;
            e.Graphics.DrawImage(cloud, t.X, 5);
        }
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        foreach (DropThread t in thread)
        {
            if (t == null || !t.Active)
                continue;

            Bitmap drop = t.Up ? dropUp : dropDown;
            e.Graphics.DrawImage(drop, t.X + 17, t.I);
        }
    }

    private void SemaphoreForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopThreads();
    }
}
