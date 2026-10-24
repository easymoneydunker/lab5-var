namespace Lab5;

public abstract class MoveThread
{
    protected readonly PictureBox Box;
    protected volatile bool Terminated;
    protected readonly ManualResetEvent StopEvent = new ManualResetEvent(false);

    private readonly Thread thread;
    private readonly ManualResetEvent running = new ManualResetEvent(true);

    protected MoveThread(PictureBox box)
    {
        Box = box;
        thread = new Thread(Execute);
        thread.IsBackground = true;
        thread.Priority = ThreadPriority.Lowest;
    }

    public void Start()
    {
        thread.Start();
    }

    public ThreadPriority Priority
    {
        get { return thread.Priority; }
        set
        {
            if (thread.IsAlive)
                thread.Priority = value;
        }
    }

    public bool Suspended
    {
        get { return !running.WaitOne(0); }
    }

    public void Suspend()
    {
        running.Reset();
    }

    public void Resume()
    {
        running.Set();
    }

    public void Terminate()
    {
        Terminated = true;
        StopEvent.Set();
        running.Set();
    }

    public void WaitFor()
    {
        thread.Join();
    }

    protected abstract void Execute();

    protected void Synchronize(Action action)
    {
        running.WaitOne();
        if (Terminated)
            return;

        IAsyncResult result = Box.BeginInvoke(action);
        WaitHandle.WaitAny(new WaitHandle[] { result.AsyncWaitHandle, StopEvent });
        Thread.Sleep(5);
    }
}
