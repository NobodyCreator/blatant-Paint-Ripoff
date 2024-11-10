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
            paintManager = new PaintManager(panel1);
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
            paintManager.SetColor(button.BackColor);
        }
        private void clearButton_click(object sender, EventArgs e)
        {
            paintManager.Clear();
        }

        private void saveButton_click(Object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                paintManager.Save(saveFileDialog.FileName); 
            }
        }
    }
}
