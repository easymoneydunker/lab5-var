using System.Drawing.Drawing2D;

namespace Lab5;

public static class Pictures
{
    public static Bitmap Car(Color color, bool toRight, bool withPassenger = false)
    {
        Bitmap bmp = new Bitmap(80, 40);
        using (Graphics g = Graphics.FromImage(bmp))
        using (SolidBrush body = new SolidBrush(color))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Point[] roof = { new Point(18, 15), new Point(26, 3), new Point(54, 3), new Point(64, 15) };
            g.FillPolygon(body, roof);
            g.FillRectangle(body, 2, 14, 76, 16);

            Point[] window = { new Point(24, 14), new Point(29, 6), new Point(51, 6), new Point(58, 14) };
            g.FillPolygon(Brushes.LightSkyBlue, window);

            if (withPassenger)
                g.FillEllipse(Brushes.SaddleBrown, 36, 6, 8, 8);

            g.FillRectangle(Brushes.Gold, 72, 17, 6, 5);
            g.FillEllipse(Brushes.Black, 10, 24, 15, 15);
            g.FillEllipse(Brushes.Black, 54, 24, 15, 15);
            g.FillEllipse(Brushes.Gray, 14, 28, 7, 7);
            g.FillEllipse(Brushes.Gray, 58, 28, 7, 7);
        }

        if (!toRight)
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
        return bmp;
    }

    public static Bitmap Drop(bool up)
    {
        Bitmap bmp = new Bitmap(16, 24);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Point[] top = { new Point(8, 0), new Point(2, 13), new Point(14, 13) };
            g.FillPolygon(Brushes.DodgerBlue, top);
            g.FillEllipse(Brushes.DodgerBlue, 2, 8, 12, 14);
            g.FillEllipse(Brushes.White, 5, 12, 3, 4);
        }

        if (up)
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
        return bmp;
    }

    public static Bitmap Cloud(Color color)
    {
        Bitmap bmp = new Bitmap(50, 30);
        using (Graphics g = Graphics.FromImage(bmp))
        using (SolidBrush brush = new SolidBrush(color))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(brush, 2, 12, 20, 16);
            g.FillEllipse(brush, 12, 2, 24, 24);
            g.FillEllipse(brush, 28, 10, 20, 18);
        }
        return bmp;
    }
}
