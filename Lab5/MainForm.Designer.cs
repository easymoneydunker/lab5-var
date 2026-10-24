namespace Lab5;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        buttonCritical = new Button();
        buttonMutex = new Button();
        buttonEvent = new Button();
        buttonSemaphore = new Button();
        SuspendLayout();
        buttonCritical.Location = new Point(20, 20);
        buttonCritical.Name = "buttonCritical";
        buttonCritical.Size = new Size(300, 40);
        buttonCritical.TabIndex = 0;
        buttonCritical.Text = "5.2.1 Критическая секция";
        buttonCritical.UseVisualStyleBackColor = true;
        buttonCritical.Click += buttonCritical_Click;
        buttonMutex.Location = new Point(20, 70);
        buttonMutex.Name = "buttonMutex";
        buttonMutex.Size = new Size(300, 40);
        buttonMutex.TabIndex = 1;
        buttonMutex.Text = "5.2.2 Мьютекс";
        buttonMutex.UseVisualStyleBackColor = true;
        buttonMutex.Click += buttonMutex_Click;
        buttonEvent.Location = new Point(20, 120);
        buttonEvent.Name = "buttonEvent";
        buttonEvent.Size = new Size(300, 40);
        buttonEvent.TabIndex = 2;
        buttonEvent.Text = "5.2.3 Событие";
        buttonEvent.UseVisualStyleBackColor = true;
        buttonEvent.Click += buttonEvent_Click;
        buttonSemaphore.Location = new Point(20, 170);
        buttonSemaphore.Name = "buttonSemaphore";
        buttonSemaphore.Size = new Size(300, 40);
        buttonSemaphore.TabIndex = 3;
        buttonSemaphore.Text = "5.2.4 Семафор";
        buttonSemaphore.UseVisualStyleBackColor = true;
        buttonSemaphore.Click += buttonSemaphore_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(340, 230);
        Controls.Add(buttonCritical);
        Controls.Add(buttonMutex);
        Controls.Add(buttonEvent);
        Controls.Add(buttonSemaphore);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Лабораторная работа №5";
        ResumeLayout(false);
    }

    private Button buttonCritical;
    private Button buttonMutex;
    private Button buttonEvent;
    private Button buttonSemaphore;
}
