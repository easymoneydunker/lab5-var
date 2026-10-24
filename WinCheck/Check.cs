using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using Lab5;

namespace WinCheck;

static class Check
{
    static int failures;

    [STAThread]
    static int Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) => Fail("UI exception: " + e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Console.WriteLine("FAIL unhandled: " + e.ExceptionObject);
            Console.Out.Flush();
            Environment.Exit(3);
        };
        Directory.CreateDirectory("shots");

        var main = new MainForm();
        main.Show();
        Pump(300);
        Shot(main, "main");

        Run("Critical", CheckCritical);
        Run("Mutex", CheckMutex);
        Run("Event", CheckEvent);
        Run("Semaphore", CheckSemaphore);

        main.Close();
        Console.WriteLine(failures == 0 ? "ALL OK" : "FAILURES: " + failures);
        return failures == 0 ? 0 : 1;
    }

    static void Run(string name, Action a)
    {
        Console.WriteLine("=== " + name);
        try { a(); } catch (Exception e) { Fail(name + " threw " + e); }
    }

    static void Fail(string msg) { failures++; Console.WriteLine("FAIL: " + msg); }
    static void Ok(bool cond, string msg) { if (cond) Console.WriteLine("ok: " + msg); else Fail(msg); }

    static object Get(object o, string name) =>
        o.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(o);

    static void Call(object o, string name) =>
        o.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(o, new object[] { o, EventArgs.Empty });

    static void Click(Form f, string name)
    {
        var b = (Button)Get(f, name);
        if (!b.Enabled) Fail(f.Name + "." + name + " is disabled");
        b.PerformClick();
    }

    static void Pump(int ms, Action each = null)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < ms)
        {
            Application.DoEvents();
            each?.Invoke();
            Thread.Sleep(5);
        }
    }

    static long Timed(Action a)
    {
        var sw = Stopwatch.StartNew();
        a();
        return sw.ElapsedMilliseconds;
    }

    static void Shot(Control c, string name)
    {
        using var bmp = new Bitmap(c.Width, c.Height);
        c.DrawToBitmap(bmp, new Rectangle(0, 0, c.Width, c.Height));
        string path = Path.Combine("shots", name + ".png");
        bmp.Save(path, ImageFormat.Png);
        string b64 = Convert.ToBase64String(File.ReadAllBytes(path));
        Console.WriteLine("SHOT-BEGIN " + name);
        for (int i = 0; i < b64.Length; i += 3000)
            Console.WriteLine("B64 " + b64.Substring(i, Math.Min(3000, b64.Length - i)));
        Console.WriteLine("SHOT-END " + name);
    }

    static void StopAndClose(Form f, string threadField)
    {
        Click(f, "buttonFinish");
        Pump(300);
        long t = Timed(() => Click(f, "buttonFinish"));
        Ok(t < 1500, $"terminate took {t} ms");
        Ok(Get(f, threadField) == null, "threads released after finish");
        Click(f, "buttonStart");
        Pump(700);
        t = Timed(() => f.Close());
        Ok(t < 1500, $"close while running took {t} ms");
    }

    static void CheckCritical()
    {
        var f = new CriticalForm();
        f.Show();
        Click(f, "buttonStart");
        var t1 = (CriticalThread)Get(f, "thread1");
        var t2 = (CriticalThread)Get(f, "thread2");

        bool both = false, waitSeen = false, shot = false;
        Pump(20000, () =>
        {
            if (t1.OnBridge && t2.OnBridge) both = true;
            bool w = (t1.OnBridge && t2.State.StartsWith("ждёт")) || (t2.OnBridge && t1.State.StartsWith("ждёт"));
            if (w) waitSeen = true;
            if (w && !shot) { Shot(f, "critical"); shot = true; }
        });
        if (!shot) Shot(f, "critical");
        Console.WriteLine($"passes {t1.Passes} / {t2.Passes}");
        Ok(!both, "never two cars on the bridge");
        Ok(waitSeen, "one car waited for the section");
        Ok(t1.Passes > 0 && t2.Passes > 0, "both cars crossed the bridge");

        ((TrackBar)Get(f, "trackBar1")).Value = 4;
        Call(f, "trackBar1_Scroll");
        Ok(t1.Priority == ThreadPriority.Highest, "priority changed by trackbar");

        Click(f, "buttonFinish");
        Pump(200);
        int a = t1.X, b = t2.X;
        Pump(1000);
        Ok(a == t1.X && b == t2.X, "paused cars do not move");
        Click(f, "buttonStart");
        Pump(1000);
        Ok(a != t1.X || b != t2.X, "resumed cars move");

        StopAndClose(f, "thread1");
    }

    static long leftAt;

    static (long, long) MutexLag(MutexForm f, int time, int ms)
    {
        leftAt = -1;
        ((NumericUpDown)Get(f, "numericUpDown1")).Value = time;
        Click(f, "buttonStart");
        var t1 = (MutexThread)Get(f, "thread1");
        var t2 = (MutexThread)Get(f, "thread2");
        long s1 = -1, s2 = -1;
        var sw = Stopwatch.StartNew();
        Pump(ms, () =>
        {
            if (s1 < 0 && t1.X > -t1.Picture.Width) s1 = sw.ElapsedMilliseconds;
            if (s2 < 0 && t2.X > -t2.Picture.Width) s2 = sw.ElapsedMilliseconds;
            if (leftAt < 0 && (t1.X > 655 || t2.X > 655)) leftAt = sw.ElapsedMilliseconds;
        });
        return (s1, s2);
    }

    static void CheckMutex()
    {
        var f = new MutexForm();
        f.Show();

        var (a, b) = MutexLag(f, 1000, 2500);
        Shot(f, "mutex");
        long lag = Math.Abs(a - b);
        Console.WriteLine($"time=1000: starts {a} / {b}, lag {lag} ms");
        Ok(a >= 0 && b >= 0 && lag > 700 && lag < 1500, "second car starts about 1000 ms later");
        Click(f, "buttonFinish"); Pump(200); Click(f, "buttonFinish");

        (a, b) = MutexLag(f, 0, 1500);
        lag = Math.Abs(a - b);
        Console.WriteLine($"time=0: starts {a} / {b}, lag {lag} ms");
        Ok(a >= 0 && b >= 0 && lag < 400, "with time=0 cars start together");
        Click(f, "buttonFinish"); Pump(200); Click(f, "buttonFinish");

        (a, b) = MutexLag(f, 10000, 12000);
        lag = Math.Abs(a - b);
        Console.WriteLine($"time=10000: starts {a} / {b}, first car left at {leftAt} ms");
        Ok(a >= 0 && b >= 0 && leftAt > 0 && Math.Max(a, b) >= leftAt - 100, "with big time second car waits until first releases mutex");

        Click(f, "buttonFinish");
        Pump(200);
        var t1 = (MutexThread)Get(f, "thread1");
        int x = t1.X;
        Pump(800);
        Ok(x == t1.X, "paused");
        Click(f, "buttonStart");
        StopAndClose(f, "thread1");
    }

    static void CheckEvent()
    {
        var f = new EventForm();
        f.Show();
        var prob = (NumericUpDown)Get(f, "numericUpDown2");
        prob.Value = 0;
        Click(f, "buttonStart");
        var t1 = (EventThread)Get(f, "thread1");
        var t2 = (EventThread)Get(f, "thread2");
        Pump(9000);
        int x1 = t1.X, x2 = t2.X;
        Pump(1500);
        Shot(f, "event-wait");
        Console.WriteLine($"stopped at {x1} / {x2}, states: {t1.State} / {t2.State}");
        Ok(x1 == t1.X && x2 == t2.X && x1 > 300 && x2 > 300, "both taxis wait at the stop");
        Ok(!f.EventIsSet && f.Count == 0, "event is reset without passengers");
        Click(f, "buttonFinish");
        Pump(200);
        long ms = Timed(() => Click(f, "buttonFinish"));
        Ok(ms < 1000, $"terminate while waiting for event took {ms} ms");

        prob.Value = 100;
        ((NumericUpDown)Get(f, "numericUpDown1")).Value = 100;
        Click(f, "buttonStart");
        t1 = (EventThread)Get(f, "thread1");
        t2 = (EventThread)Get(f, "thread2");
        int maxCount = 0;
        bool badEvent = false;
        Pump(15000, () =>
        {
            int c = f.Count;
            maxCount = Math.Max(maxCount, c);
            if (c > 2 && !f.EventIsSet) badEvent = true;
        });
        Console.WriteLine($"delivered {t1.Delivered} / {t2.Delivered}, max queue {maxCount}");
        Ok(t1.Delivered > 0 && t2.Delivered > 0, "both taxis deliver passengers");
        Ok(maxCount > 3, "passengers accumulate");
        Ok(!badEvent, "event is set while passengers wait");
        Click(f, "buttonFinish"); Pump(200); Click(f, "buttonFinish");

        prob.Value = 20;
        ((NumericUpDown)Get(f, "numericUpDown1")).Value = 500;
        Click(f, "buttonStart");
        Pump(9000);
        Shot(f, "event");
        StopAndClose(f, "thread1");
    }

    static (int max, int distinct) Drops(SemaphoreForm f, int ms, bool shot)
    {
        var threads = (DropThread[])Get(f, "thread");
        int max = 0;
        var seen = new HashSet<int>();
        bool done = !shot;
        var sw = Stopwatch.StartNew();
        Pump(ms, () =>
        {
            int n = 0;
            for (int i = 0; i < 10; i++)
                if (threads[i].Active) { n++; seen.Add(i); }
            max = Math.Max(max, n);
            if (!done && sw.ElapsedMilliseconds > 3000) { Shot(f, "semaphore"); done = true; }
        });
        return (max, seen.Count);
    }

    static void CheckSemaphore()
    {
        var f = new SemaphoreForm();
        f.Show();
        ((NumericUpDown)Get(f, "numericUpDown1")).Value = 3;
        Click(f, "buttonStart");
        var (max, distinct) = Drops(f, 12000, true);
        Console.WriteLine($"max=3: active max {max}, distinct {distinct}");
        Ok(max == 3, "exactly 3 drops at the same time");
        Ok(distinct > 3, "drops take turns");

        ((NumericUpDown)Get(f, "numericUpDown2")).Value = 1500;
        long ms = Timed(() => Click(f, "buttonSleep"));
        Ok(ms >= 1400, $"sleep blocked for {ms} ms");
        Pump(500);

        Click(f, "buttonFinish"); Pump(200);
        ms = Timed(() => Click(f, "buttonFinish"));
        Ok(ms < 1500, $"terminate took {ms} ms");

        ((NumericUpDown)Get(f, "numericUpDown1")).Value = 10;
        Click(f, "buttonStart");
        (max, distinct) = Drops(f, 3000, false);
        Console.WriteLine($"max=10: active max {max}");
        Ok(max == 10, "all 10 drops fly");

        ((NumericUpDown)Get(f, "numericUpDown1")).Value = 1;
        Click(f, "buttonFinish"); Pump(200); Click(f, "buttonFinish");
        Click(f, "buttonStart");
        (max, distinct) = Drops(f, 3000, false);
        Console.WriteLine($"max=1: active max {max}");
        Ok(max == 1, "single drop");
        StopAndClose(f, "fsemaphore");
    }
}
