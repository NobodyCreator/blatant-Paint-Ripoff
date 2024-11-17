using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

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
        private Stack<Bitmap> undoStack;

        public PaintManager(Panel panel) 
        {
            drawingPanel = panel;
            undoStack = new Stack<Bitmap>();
            InitializeBitmap();
        }

        private void InitializeBitmap()
        {
            drawingBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);
            graphics = Graphics.FromImage(drawingBitmap);
            graphics.Clear(Color.White);
            drawingPanel.BackgroundImage = drawingBitmap;
            drawingPanel.BackgroundImageLayout = ImageLayout.None;
        }

        public void Resize(Panel panel)
        {
            if (panel.Width > 0 && panel.Height > 0)
            {
                Bitmap newBitmap = new Bitmap(panel.Width, panel.Height);
                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.Clear(Color.White);
                    g.DrawImage(drawingBitmap, 0, 0);
                }
                drawingBitmap = newBitmap;
                graphics = Graphics.FromImage(drawingBitmap);
                drawingPanel.BackgroundImage = drawingBitmap;
            }
        }

        public void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;

            undoStack.Push((Bitmap)drawingBitmap.Clone());//undo moment
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

                drawingPanel.Invalidate(new Rectangle(lastPoint.X, lastPoint.Y, 2, 2));
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

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                drawingBitmap = undoStack.Pop();
                graphics = Graphics.FromImage(drawingBitmap);
                drawingPanel.BackgroundImage = drawingBitmap;
                drawingPanel.Invalidate();
            }
        }
    }
}
