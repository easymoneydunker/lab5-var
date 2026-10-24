namespace Lab5;

partial class MutexForm
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
        buttonStart = new Button();
        buttonFinish = new Button();
        timer1 = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        SuspendLayout();
        pictureBox1.BackColor = Color.White;
        pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        pictureBox1.Location = new Point(12, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(660, 110);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        pictureBox1.Paint += pictureBox1_Paint;
        label1.AutoSize = true;
        label1.Location = new Point(12, 140);
        label1.Name = "label1";
        label1.Size = new Size(200, 15);
        label1.TabIndex = 1;
        label1.Text = "Тайм-аут ожидания мьютекса, мс:";
        numericUpDown1.Increment = new decimal(new int[] { 100, 0, 0, 0 });
        numericUpDown1.Location = new Point(230, 138);
        numericUpDown1.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new Size(100, 23);
        numericUpDown1.TabIndex = 2;
        numericUpDown1.Value = new decimal(new int[] { 1000, 0, 0, 0 });
        numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
        buttonStart.Location = new Point(380, 135);
        buttonStart.Name = "buttonStart";
        buttonStart.Size = new Size(140, 30);
        buttonStart.TabIndex = 3;
        buttonStart.Text = "Start";
        buttonStart.UseVisualStyleBackColor = true;
        buttonStart.Click += buttonStart_Click;
        buttonFinish.Enabled = false;
        buttonFinish.Location = new Point(532, 135);
        buttonFinish.Name = "buttonFinish";
        buttonFinish.Size = new Size(140, 30);
        buttonFinish.TabIndex = 4;
        buttonFinish.Text = "Finish";
        buttonFinish.UseVisualStyleBackColor = true;
        buttonFinish.Click += buttonFinish_Click;
        timer1.Interval = 500;
        timer1.Tick += timer1_Tick;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 180);
        Controls.Add(pictureBox1);
        Controls.Add(label1);
        Controls.Add(numericUpDown1);
        Controls.Add(buttonStart);
        Controls.Add(buttonFinish);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MutexForm";
        Text = "5.2.2 Мьютекс";
        FormClosing += MutexForm_FormClosing;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private PictureBox pictureBox1;
    private Label label1;
    private NumericUpDown numericUpDown1;
    private Button buttonStart;
    private Button buttonFinish;
    private System.Windows.Forms.Timer timer1;
}
