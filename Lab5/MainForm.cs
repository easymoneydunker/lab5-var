namespace Lab5;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void buttonCritical_Click(object sender, EventArgs e)
    {
        new CriticalForm().Show();
    }

    private void buttonMutex_Click(object sender, EventArgs e)
    {
        new MutexForm().Show();
    }

    private void buttonEvent_Click(object sender, EventArgs e)
    {
        new EventForm().Show();
    }

    private void buttonSemaphore_Click(object sender, EventArgs e)
    {
        new SemaphoreForm().Show();
    }
}
