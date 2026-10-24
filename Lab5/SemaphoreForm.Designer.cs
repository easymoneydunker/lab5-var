namespace Lab5;

partial class SemaphoreForm
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
        pictureBox2 = new PictureBox();
        pictureBox1 = new PictureBox();
        label1 = new Label();
        numericUpDown1 = new NumericUpDown();
        label2 = new Label();
        numericUpDown2 = new NumericUpDown();
        buttonStart = new Button();
        buttonFinish = new Button();
        buttonSleep = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
        SuspendLayout();
        pictureBox2.BackColor = Color.LightCyan;
        pictureBox2.Location = new Point(12, 12);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(660, 40);
        pictureBox2.TabIndex = 0;
        pictureBox2.TabStop = false;
        pictureBox2.Paint += pictureBox2_Paint;
        pictureBox1.BackColor = Color.White;
        pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        pictureBox1.Location = new Point(12, 52);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(660, 250);
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        pictureBox1.Paint += pictureBox1_Paint;
        label1.AutoSize = true;
        label1.Location = new Point(12, 320);
        label1.Name = "label1";
        label1.Size = new Size(220, 15);
        label1.TabIndex = 2;
        label1.Text = "Максимальное количество капель:";
        numericUpDown1.Location = new Point(250, 318);
        numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size(80, 23);
        numericUpDown1.TabIndex = 3;
        numericUpDown1.Value = new decimal(new int[] { 3, 0, 0, 0 });
        label2.AutoSize = true;
        label2.Location = new Point(12, 352);
        label2.Name = "label2";
        label2.Size = new Size(220, 15);
        label2.TabIndex = 4;
        label2.Text = "Время приостановки (Sleep), мс:";
        numericUpDown2.Increment = new decimal(new int[] { 500, 0, 0, 0 });
        numericUpDown2.Location = new Point(250, 350);
        numericUpDown2.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numericUpDown2.Name = "numericUpDown2";
        numericUpDown2.Size = new Size(80, 23);
        numericUpDown2.TabIndex = 5;
        numericUpDown2.Value = new decimal(new int[] { 2000, 0, 0, 0 });
        buttonStart.Location = new Point(380, 315);
        buttonStart.Name = "buttonStart";
        buttonStart.Size = new Size(140, 30);
        buttonStart.TabIndex = 6;
        buttonStart.Text = "Start";
        buttonStart.UseVisualStyleBackColor = true;
        buttonStart.Click += buttonStart_Click;
        buttonFinish.Enabled = false;
        buttonFinish.Location = new Point(532, 315);
        buttonFinish.Name = "buttonFinish";
        buttonFinish.Size = new Size(140, 30);
        buttonFinish.TabIndex = 7;
        buttonFinish.Text = "Finish";
        buttonFinish.UseVisualStyleBackColor = true;
        buttonFinish.Click += buttonFinish_Click;
        buttonSleep.Location = new Point(380, 350);
        buttonSleep.Name = "buttonSleep";
        buttonSleep.Size = new Size(292, 30);
        buttonSleep.TabIndex = 8;
        buttonSleep.Text = "Sleep";
        buttonSleep.UseVisualStyleBackColor = true;
        buttonSleep.Click += buttonSleep_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 395);
        Controls.Add(pictureBox2);
        Controls.Add(pictureBox1);
        Controls.Add(label1);
        Controls.Add(numericUpDown1);
        Controls.Add(label2);
        Controls.Add(numericUpDown2);
        Controls.Add(buttonStart);
        Controls.Add(buttonFinish);
        Controls.Add(buttonSleep);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "SemaphoreForm";
        Text = "5.2.4 Семафор";
        FormClosing += SemaphoreForm_FormClosing;
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private PictureBox pictureBox2;
    private PictureBox pictureBox1;
    private Label label1;
    private NumericUpDown numericUpDown1;
    private Label label2;
    private NumericUpDown numericUpDown2;
    private Button buttonStart;
    private Button buttonFinish;
    private Button buttonSleep;
}
