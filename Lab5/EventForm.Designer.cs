namespace Lab5;

partial class EventForm
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
        components = new System.ComponentModel.Container();
        pictureBox1 = new PictureBox();
        label1 = new Label();
        numericUpDown1 = new NumericUpDown();
        label2 = new Label();
        numericUpDown2 = new NumericUpDown();
        buttonStart = new Button();
        buttonFinish = new Button();
        timer1 = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
        SuspendLayout();
        pictureBox1.BackColor = Color.White;
        pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        pictureBox1.Location = new Point(12, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(660, 180);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        pictureBox1.Paint += pictureBox1_Paint;
        label1.AutoSize = true;
        label1.Location = new Point(12, 210);
        label1.Name = "label1";
        label1.Size = new Size(150, 15);
        label1.TabIndex = 1;
        label1.Text = "Интервал таймера, мс:";
        numericUpDown1.Increment = new decimal(new int[] { 100, 0, 0, 0 });
        numericUpDown1.Location = new Point(200, 208);
        numericUpDown1.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
        numericUpDown1.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size(100, 23);
        numericUpDown1.TabIndex = 2;
        numericUpDown1.Value = new decimal(new int[] { 500, 0, 0, 0 });
        numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
        label2.AutoSize = true;
        label2.Location = new Point(12, 242);
        label2.Name = "label2";
        label2.Size = new Size(170, 15);
        label2.TabIndex = 3;
        label2.Text = "Вероятность пассажира, %:";
        numericUpDown2.Location = new Point(200, 240);
        numericUpDown2.Name = "numericUpDown2";
        numericUpDown2.Size = new Size(100, 23);
        numericUpDown2.TabIndex = 4;
        numericUpDown2.Value = new decimal(new int[] { 30, 0, 0, 0 });
        buttonStart.Location = new Point(532, 205);
        buttonStart.Name = "buttonStart";
        buttonStart.Size = new Size(140, 30);
        buttonStart.TabIndex = 6;
        buttonStart.Text = "Start";
        buttonStart.UseVisualStyleBackColor = true;
        buttonStart.Click += buttonStart_Click;
        buttonFinish.Enabled = false;
        buttonFinish.Location = new Point(532, 240);
        buttonFinish.Name = "buttonFinish";
        buttonFinish.Size = new Size(140, 30);
        buttonFinish.TabIndex = 7;
        buttonFinish.Text = "Finish";
        buttonFinish.UseVisualStyleBackColor = true;
        buttonFinish.Click += buttonFinish_Click;
        timer1.Interval = 500;
        timer1.Tick += timer1_Tick;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 280);
        Controls.Add(pictureBox1);
        Controls.Add(label1);
        Controls.Add(numericUpDown1);
        Controls.Add(label2);
        Controls.Add(numericUpDown2);
        Controls.Add(buttonStart);
        Controls.Add(buttonFinish);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "EventForm";
        Text = "5.2.3 Событие";
        FormClosing += EventForm_FormClosing;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private PictureBox pictureBox1;
    private Label label1;
    private NumericUpDown numericUpDown1;
    private Label label2;
    private NumericUpDown numericUpDown2;
    private Button buttonStart;
    private Button buttonFinish;
    private System.Windows.Forms.Timer timer1;
}
