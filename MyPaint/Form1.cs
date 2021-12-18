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
        private int codeShape=-1;
        private ManagerShape mng;
        private CTreeView tree;
        String path = "C:\\Users\\Admin\\Desktop\\save.txt";
        public Form1()
        {
            mng = new ManagerShape();
            InitializeComponent();
            pictureBox1.Width = 1200;
            tBSize.Maximum = 100;
            tBSize.Minimum = 10;
            trbRed.Minimum = trbGreen.Minimum = trbBlue.Minimum = 0;
            trbRed.Maximum = trbGreen.Maximum = trbBlue.Maximum = 255;
            //tVStore.AllowDrop = true;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            mng.draw(pictureBox1);
        }
        private void pictureBox1_Click(object sender, EventArgs e) {
            MouseEventArgs a = (MouseEventArgs)e;
            mng.choseElementUsePoint(a.X, a.Y);
            mng.draw(pictureBox1);
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e){
            MouseEventArgs a = (MouseEventArgs)e;
            mng.createNewElement(codeShape, a.X, a.Y);
            mng.draw(pictureBox1);
        }
        private void ClearMonitor()
        {
            Graphics p = pictureBox1.CreateGraphics();
            p.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codeShape = listBox1.SelectedIndex + 1;
            Console.WriteLine("Chose a point.");
        }
        private void tBSize_Scroll(object sender, EventArgs e){
            tbAns.Text = tBSize.Value.ToString();
            mng.resizeForAllChosenElements(pictureBox1.Width, pictureBox1.Height, tBSize.Value);
            ClearMonitor();
            mng.draw(pictureBox1);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mng.draw(pictureBox1);
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
                    mng.moveAllChosenElement(
                        e.X-ox, 
                        e.Y-oy, 
                        pictureBox1.Width, 
                        pictureBox1.Height
                        );
                    ClearMonitor();
                    mng.draw(pictureBox1);
                    ox = e.X; oy = e.Y;
                }
                
            }
            if (!wantMove){
                ox = oy = -1;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            mng.saveData(path);
            tbAns.Text = "Picture saved.";
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {

            mng.uploadFile(path, pictureBox1);
            mng.draw(pictureBox1);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){
                wantMove = !wantMove;
                if (wantMove) tbAns.Text = "We ready to move";
                else tbAns.Text = "We not ready";
            }
            if(e.KeyCode == Keys.Space){
                mng.changeColor(trbRed.Value, trbGreen.Value, trbBlue.Value);
                mng.draw(pictureBox1);
            }
            if(e.KeyCode == Keys.Delete){
                mng.deleteChosen();
                ClearMonitor();
                mng.draw(pictureBox1);
            }
            if(e.KeyCode == Keys.A) {//Group
                tbAns.Text = "CGroup was created.";
                mng.Group();
                Console.WriteLine("We have a problem");
            }
            if(e.KeyCode == Keys.Z){//Ungroup
                mng.unGroup();
                tbAns.Text = "UnGroup complete";
                ClearMonitor();
                mng.draw(pictureBox1);
            }
        }
    }
}

