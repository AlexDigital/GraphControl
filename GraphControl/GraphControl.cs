using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace GraphControl
{
    public class GraphControl : Control
    {

        private int YCount = 0;
        private int XCount = 0;

        public GraphControl() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            this.DoubleBuffered = true;
            this.SuspendLayout();
            this.ResumeLayout(false);

            this.XCount = (this.Width - 20) / 20 - 1;
            this.YCount = (this.Height - 20) / 20 - 1;

            Points = new List<Point>();
        }

        #region Properties

        [Category("Values")]
        public bool ConnectPoints { get; set; }

        [Category("Values")]
        public List<Point> Points { get; set; }

        public void AddPoint(Point point)
        {
            Points.Add(point);
            this.Invalidate();
        }

        public void SetPoints(List<Point> points)
        {
            Points = points;
            this.Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(Pens.Black, new Point(10, 10), new Point(10, this.Height - 10));
            e.Graphics.DrawLine(Pens.Black, new Point(10, this.Height - 10), new Point(this.Width - 10, this.Height - 10));
            e.Graphics.DrawLines(Pens.Black, new Point[] { 
                                                    new Point(5, 15), 
                                                    new Point(10, 10), 
                                                    new Point(15, 15) });
            e.Graphics.DrawLines(Pens.Black, new Point[] { 
                                                    new Point(this.Width - 15, this.Height - 15), 
                                                    new Point(this.Width - 10, this.Height - 10), 
                                                    new Point(this.Width - 15, this.Height - 5) });

            e.Graphics.DrawString("0", new Font("Segoe UI", 10), Brushes.Black, new Point(0, this.Height - 15));

            for (int i = 1; i <= XCount; i++)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(10 + i * 20, this.Height - 15),
                                    new Point(10 + i * 20, this.Height - 5));
                e.Graphics.DrawString(i.ToString(), new Font("Segoe UI", 6), Brushes.Black,
                                    new Point(10 + i * 20, this.Height - 10));
            }

            for (int i = 1; i <= YCount; i++)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(5, this.Height - 10 - i * 20),
                                    new Point(15, this.Height - 10 - i * 20));
                e.Graphics.DrawString(i.ToString(), new Font("Segoe UI", 6), Brushes.Black,
                                    new Point(0, this.Height - 10 - i * 20));
            }

            foreach (Point p in Points)
            {
                if (!(p.X > XCount || p.Y > YCount))
                {
                    e.Graphics.FillRectangle(Brushes.Red, new Rectangle((10 + p.X * 20) - 1, 
                        (this.Height - 10 - p.Y * 20) - 1, 3, 3));
                }
            }

            if(ConnectPoints)
            {
                for(int i = 0; i < Points.Count - 1; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(10 + Points[i].X * 20, this.Height - 10 - Points[i].Y * 20),
                        new Point(10 + Points[i + 1].X * 20, this.Height - 10 - Points[i + 1].Y * 20));
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.XCount = (this.Width - 20) / 20 - 1;
            this.YCount = (this.Height - 20) / 20 - 1;
            this.Invalidate();
        }

        public void UpdatePoints()
        {
            this.Invalidate();
        }

    }
}