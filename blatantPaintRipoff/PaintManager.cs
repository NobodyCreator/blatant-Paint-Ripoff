using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace blatantPaintRipoff
{
    public class PaintManager
    {
        private bool isDrawing = false;
        private Point lastPoint = Point.Empty;
        private Color currentColor = Color.Black;
        private Bitmap drawingBitmap;
        private Graphics graphics;
        private Panel drawingPanel;

        public PaintManager(Panel panel) 
        {
            drawingPanel = panel;
            drawingBitmap = new Bitmap(panel.Width, panel.Height);
            graphics = Graphics.FromImage(drawingBitmap);
            drawingPanel.BackgroundImage = drawingBitmap;
            drawingPanel.BackgroundImageLayout = ImageLayout.None;
        }

        public void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }
        public void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && lastPoint != null)
            {
                using (Pen pen = new Pen(currentColor, 2))
                {
                    graphics.DrawLine(pen, lastPoint, e.Location);
                }
                drawingPanel.Invalidate();
                lastPoint = e.Location;
            }
        }
        public void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            lastPoint = Point.Empty;
        }

        public void SetColor(Color color)
        {
            currentColor = color;
        }

        public void Clear()
        {
            graphics.Clear(Color.White);
            drawingPanel.Invalidate();
        }

        public void Save(string filePath)
        {
            drawingBitmap.Save(filePath);
        }
    }
}
