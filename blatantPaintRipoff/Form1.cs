using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blatantPaintRipoff
{
    public partial class Form1 : Form
    {

        private PaintManager paintManager;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            paintManager = new PaintManager(panel1);
            panel1.Resize += new EventHandler(panel1_Resize);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            //double buffering
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                paintManager.Undo();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
           //no base class
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            paintManager.Resize(panel1);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            paintManager.Panel_MouseDown(sender, e);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            paintManager.Panel_MouseMove(sender, e);
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            paintManager.Panel_MouseUp(sender, e);
        }

        private void colorButton_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    button.BackColor = colorDialog.Color;

                    paintManager.SetColor(colorDialog.Color);
                }
            }


                paintManager.SetColor(button.BackColor);
        }
        private void clearButton_click(object sender, EventArgs e)
        {
            paintManager.Clear();
        }

        private void undoButton_click(object sender, EventArgs e)
        {
            paintManager.Undo();
        }

        private void saveButton_click(Object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files|*.png|JPEG Files|*.jpg|BMP Files|*.bmp|GIF Files|*.gif";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { 
            {
                string fileExtension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                    switch (fileExtension)
                    {
                        case ".jpg":
                            paintManager.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case ".bmp":
                            paintManager.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case ".gif":
                            paintManager.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case ".png":
                        default:
                            paintManager.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
            }
        }
        }
    }
}
