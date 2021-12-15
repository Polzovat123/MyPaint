using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        private DataStore dStShape;
        private Graphics k = null;
        private int dS = 40;
        private int chDS = 40;
        private int codeFactory=-1;
        private FactoryDrawElements fabric;
        public Form1()
        {
            fabric = new FactoryDrawElements();
            dStShape = new DataStore();
            InitializeComponent();
            tBSize.Maximum = 100;
            tBSize.Minimum = 10;
            trbRed.Minimum = trbGreen.Minimum = trbBlue.Minimum = 0;
            trbRed.Maximum = trbGreen.Maximum = trbBlue.Maximum = 255;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            k = e.Graphics;
            Refresh();
        }
        private void pictureBox1_Click(object sender, EventArgs e) {
            MouseEventArgs a = (MouseEventArgs)e;
            if (dStShape != null && dStShape.s() > 0){
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    if(((CShape)dStShape.GET()).isHit(a.X, a.Y)){
                        ((CShape)dStShape.GET()).chose();
                    }
                }
            }
            Refresh();
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e){
            MouseEventArgs a = (MouseEventArgs)e;
            if(codeFactory!=-1)
                dStShape.Add(fabric.createObject(codeFactory, a.X-dS/2, a.Y-dS/2, dS));
            Refresh();
        }
        private void Refresh() {
            if (dStShape != null && dStShape.s() > 0){
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    ((CShape)dStShape.GET()).draw(pictureBox1);
                }
            }
        }
        private void ClearMonitor()
        {
            k.FillRectangle(new SolidBrush(Color.White), 0, 0, 1300, 500);
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codeFactory = listBox1.SelectedIndex + 1;
            Console.WriteLine("Chose a point.");
        }

        private void tBSize_Scroll(object sender, EventArgs e){
            chDS = tBSize.Value;
            tbAns.Text = tBSize.Value.ToString();
            for(dStShape.first(); dStShape.need(); dStShape.next()){
                SShape obj = ((SShape)(dStShape.GET()));
                if (!obj.gFl() && obj.inScreen(0, 0, pictureBox1.Width, pictureBox1.Height))
                {
                    obj.reSize(chDS);
                }
            }
            Console.WriteLine();
            ClearMonitor();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Refresh();
        }
        private bool wantMove = false;
        private int ox = -1, oy = -1;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (wantMove){
                if (ox == -1)
                {
                    ox = e.X;
                    oy = e.Y;
                }
                else
                {
                    for (dStShape.first(); dStShape.need(); dStShape.next()){
                        SShape obj = ((SShape)(dStShape.GET()));
                        if (!obj.gFl() && obj.inScreen(e.X - ox, e.Y - oy, pictureBox1.Width, pictureBox1.Height))
                        {
                            obj.move(e.X - ox, e.Y-oy);
                        }
                    }
                    ClearMonitor();
                    Refresh();
                    ox = e.X; oy = e.Y;
                }
                
            }
            if (!wantMove){
                ox = oy = -1;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){
                wantMove = !wantMove;
                if (wantMove) tbAns.Text = "We ready to move";
                else tbAns.Text = "We not ready";
            }
            if(e.KeyCode == Keys.Space){
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    if (!((CShape)(dStShape.GET())).gFl()){
                        Color clr = Color.FromArgb(125, trbRed.Value, trbGreen.Value, trbBlue.Value);
                        ((CShape)(dStShape.GET())).changeColor(clr);
                    }
                }
                Refresh();
            }
            if(e.KeyCode == Keys.Delete){
                int now = 0;
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    if (!((CShape)(dStShape.GET())).gFl()){
                        ClearMonitor();
                        dStShape.del(now);
                        Refresh();
                    }
                    now++;
                }
            }
        }
    }
}

/*
if (codeFactory == 1){
                codeFactory = -1;
            }
            else {
                codeFactory = 1;
            }
 */