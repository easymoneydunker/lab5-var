namespace Lab5;

partial class CriticalForm
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
        pictureBox1 = new PictureBox();
        trackBar1 = new TrackBar();
        trackBar2 = new TrackBar();
        label1 = new Label();
        label2 = new Label();
        buttonStart = new Button();
        buttonFinish = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
        SuspendLayout();
        pictureBox1.BackColor = Color.White;
        pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        pictureBox1.Location = new Point(12, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(660, 160);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        pictureBox1.Paint += pictureBox1_Paint;
        label1.AutoSize = true;
        label1.Location = new Point(12, 185);
        label1.Name = "label1";
        label1.Size = new Size(100, 15);
        label1.TabIndex = 1;
        label1.Text = "Такси";
        trackBar1.Location = new Point(12, 205);
        trackBar1.Maximum = 4;
        trackBar1.Name = "trackBar1";
        trackBar1.Size = new Size(230, 45);
        trackBar1.TabIndex = 2;
        trackBar1.Scroll += trackBar1_Scroll;
        label2.AutoSize = true;
        label2.Location = new Point(270, 185);
        label2.Name = "label2";
        label2.Size = new Size(100, 15);
        label2.TabIndex = 3;
        label2.Text = "Машина";
        trackBar2.Location = new Point(270, 205);
        trackBar2.Maximum = 4;
        trackBar2.Name = "trackBar2";
        trackBar2.Size = new Size(230, 45);
        trackBar2.TabIndex = 4;
        trackBar2.Scroll += trackBar2_Scroll;
        buttonStart.Location = new Point(532, 185);
        buttonStart.Name = "buttonStart";
        buttonStart.Size = new Size(140, 30);
        buttonStart.TabIndex = 5;
        buttonStart.Text = "Start";
        buttonStart.UseVisualStyleBackColor = true;
        buttonStart.Click += buttonStart_Click;
        buttonFinish.Enabled = false;
        buttonFinish.Location = new Point(532, 220);
        buttonFinish.Name = "buttonFinish";
        buttonFinish.Size = new Size(140, 30);
        buttonFinish.TabIndex = 6;
        buttonFinish.Text = "Finish";
        buttonFinish.UseVisualStyleBackColor = true;
        buttonFinish.Click += buttonFinish_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 260);
        Controls.Add(pictureBox1);
        Controls.Add(label1);
        Controls.Add(trackBar1);
        Controls.Add(label2);
        Controls.Add(trackBar2);
        Controls.Add(buttonStart);
        Controls.Add(buttonFinish);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "CriticalForm";
        Text = "5.2.1 Критическая секция";
        FormClosing += CriticalForm_FormClosing;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
        ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private PictureBox pictureBox1;
    private TrackBar trackBar1;
    private TrackBar trackBar2;
    private Label label1;
    private Label label2;
    private Button buttonStart;
    private Button buttonFinish;
}
