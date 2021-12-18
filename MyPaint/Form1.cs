using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
                int i = 0;
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    if(((CShape)dStShape.GET()).isHit(a.X, a.Y)){
                        ((CShape)dStShape.GET()).chose();
                        break;
                    }
                    i++;
                }
                int j = 0;  
                for (dStShape.first(); dStShape.need(); dStShape.next()){
                    if (i!=j && !((CShape)dStShape.GET()).gFl())
                    {
                        ((CShape)dStShape.GET()).chose();
                    }
                    j++;
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
            if (dStShape != null)
            {
                for (dStShape.first(); dStShape.need(); dStShape.next())
                {
                    ((CShape)dStShape.GET()).clear(pictureBox1);
                }
            }
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
                Console.WriteLine(((CShape)dStShape.GET()).gFl());
                if (!((CShape)dStShape.GET()).gFl()){
                    ((CShape)dStShape.GET()).reSize(chDS);
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (wantMove){
                for(dStShape.first(); dStShape.need(); dStShape.next()){
                    if (!((CShape)(dStShape.GET())).gFl()){
                        ((CShape)(dStShape.GET())).move(e.X, e.Y, pictureBox1);
                    }
                }
                ClearMonitor();
                Refresh();
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
                        break;
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
                        break;
                    }
                    now++;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText("C:\\Users\\Admin\\Desktop\\output.txt", "");//dStShape.s().ToString()
            for (dStShape.first(); dStShape.need(); dStShape.next()){
                ((CShape)dStShape.GET()).save("C:\\Users\\Admin\\Desktop\\output.txt");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            dStShape.clear();
            ClearMonitor();
            Refresh();
            foreach (string line in System.IO.File.ReadLines(@"C:\\Users\\Admin\\Desktop\\output.txt"))
            {
                dStShape.Add(fabric.loadObject(line[0] - '0', line));
            }
            Console.WriteLine(dStShape.s());
            Refresh();
        }
    }
}