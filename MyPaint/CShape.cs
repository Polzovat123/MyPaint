using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyPaint
{

    abstract class CShape
    {
        public abstract void draw(PictureBox pb);
        public abstract void clear(PictureBox pb);
        public abstract void chose();
        public abstract bool gFl();
        public abstract void changeColor(Color newColor);
        public abstract void reSize(int newSize);
        public abstract bool isHit(int _x, int _y);
        public abstract void move(int dx, int dy, PictureBox obj);
        public abstract void save(String path);
    }
    class CGroup: CShape{
        protected DataStore arr;

        public CGroup(DataStore d) {
            arr = d;
        }
        public void ADD(CShape obj){
            arr.Add(obj);
        }
        public override void draw(PictureBox pb)
        {
            for (arr.first(); arr.need(); arr.next()) {
                ((CShape)arr.GET()).draw(pb);
            }
        }
        public override void clear(PictureBox pb)
        {
            for (arr.first(); arr.need(); arr.next()){
                ((CShape)arr.GET()).clear(pb);
            }
        }
        public override void chose()
        {
            for (arr.first(); arr.need(); arr.next()){
                ((CShape)arr.GET()).chose();
            }
        }
        public override void reSize(int newSize)
        {
            for (arr.first(); arr.need(); arr.next())
            {
                ((CShape)arr.GET()).reSize(newSize);
            }
        }
        public override bool isHit(int _x, int _y){
            return true;
        }
        public override bool gFl()
        {
            return true;
        }
        public override void move(int dx, int dy, PictureBox obj){
            for (arr.first(); arr.need(); arr.next()){
                ((CShape)arr.GET()).move(dx, dy, obj);
            }
        }
        public override void changeColor(Color newColor)
        {
            for (arr.first(); arr.need(); arr.next()) {
                ((CShape)arr.GET()).changeColor(newColor);
            }
        }
        public override void save(String Path)
        {
            
        }
    }
    class CCircle : CShape
    {
        private int x = 400;
        private int y = 300;
        private int D = 40;
        private int olD = 40;
        private bool f = true;
        private Color body = Color.Blue;
        private Color myColor = Color.Black;
        public CCircle(int _x, int _y, int _D)
        {
            x = _x;
            y = _y;
            D = _D;
        }
        public CCircle(String str){
            x = 100;
            y = 200;
            D = 40;
        }
        public override void draw(PictureBox pb)
        {
            Graphics dr = pb.CreateGraphics();
            dr.DrawEllipse(new Pen(myColor), x, y, D, D);
            dr.FillEllipse(new SolidBrush(body), x, y, D, D);
            dr.Dispose();
        }
        public override void clear(PictureBox pb)
        {
            if (f) return;
            Graphics dr = pb.CreateGraphics();
            dr.DrawEllipse(new Pen(Color.White, 10), x, y, olD, olD);
            dr.FillEllipse(new SolidBrush(Color.White), x, y, olD, olD);
            dr.Dispose();
        }
        public override void changeColor(Color newColor)
        {
            body = newColor;
        }
        public override void reSize(int newSize)
        {
            olD = D;
            D = newSize;
        }
        public override void chose()
        {
            if (!f) { myColor = Color.Black; } else { myColor = Color.Red; }
            f = !f;
        }
        public override bool gFl()
        {
            return f;
        }
        public override bool isHit(int _x, int _y)
        {
            _x = _x - D / 2;
            _y = _y - D / 2;
            if ((x - _x) * (x - _x) + (y - _y) * (y - _y) <= D * D / 4)
            {
                return true;
            }
            return false;
        }
        public override void move(int dx, int dy, PictureBox obj)
        {
            Graphics dr = obj.CreateGraphics();
            dr.DrawEllipse(new Pen(Color.White, 10), x, y, D, D);
            dr.FillEllipse(new SolidBrush(Color.White), x, y, D, D);
            dr.Dispose();
            x = dx;
            y = dy;
        }
        public override void save(String path){
            string body =
                "x = " + x.ToString() + " " +
                "y = " + y.ToString();
            string text = "1-Circle " + body + "\n";
            File.AppendAllText(path, text);
        }
    }
    class CSquare : CShape {
        private int x = 400;
        private int y = 300;
        private int D = 40;
        private int olD = 40;
        private bool f = true;
        private Color myColor = Color.Black;
        private Color body = Color.Blue;
        public CSquare(int _x, int _y, int _D)
        {
            x = _x;
            y = _y;
            D = _D;
        }
        public CSquare(String str) { }
        public override void draw(PictureBox pb)
        {
            Graphics dr = pb.CreateGraphics();
            dr.DrawRectangle(new Pen(myColor), x, y, D, D);
            dr.FillRectangle(new SolidBrush(body), x, y, D, D);
            dr.Dispose();
        }
        public override void clear(PictureBox pb)
        {
            if (f) return;
            Graphics dr = pb.CreateGraphics();
            dr.DrawRectangle(new Pen(Color.White, 10), x, y, olD, olD);
            dr.FillRectangle(new SolidBrush(Color.White), x, y, olD, olD);
            dr.Dispose();
        }
        public override void changeColor(Color newColor)
        {
            body = newColor;
        }
        public override void reSize(int newSize)
        {
            olD = D;
            D = newSize;
        }
        public override void chose()
        {
            if (!f) { myColor = Color.Black; } else { myColor = Color.Red; }
            f = !f;
        }
        public override bool gFl()
        {
            return f;
        }
        public override bool isHit(int _x, int _y)
        {
            _x = _x - D / 2;
            _y = _y - D / 2;
            if (x+D/2>=_x&&x-D/2<=_x&&y+D/2>=_y&&y-D/2<=_y)
            {
                return true;
            }
            return false;
        }
        public override void move(int dx, int dy, PictureBox obj)
        {
            clear(obj);
            x = dx;
            y = dy;
        }
        public override void save(String path)
        {
            string body =
                "x = " + x.ToString() + " " +
                "y = " + y.ToString();
            string text = "2-Square " + body + "\n";
            File.AppendAllText(path, text);
        }
    }
    class CTriangle : CShape
    {
        private int x = 400;
        private int y = 300;
        private int D = 40;
        private int olD = 40;
        private bool f = true;
        private Color myColor = Color.Black;
        private Color body = Color.Blue;
        public CTriangle(int _x, int _y, int _D)
        {
            D = _D;
            x = _x + D/2;
            y = _y;
        }
        public CTriangle(String str) { }
        public override void draw(PictureBox pb)
        {
            Graphics dr = pb.CreateGraphics();
            PointF[] triangle =
            {
                new Point(x, y),
                new Point(x+D/2, y+D),
                new Point(x+D/2, y+D),
                new Point(x-D/2, y+D),
                new Point(x-D/2, y+D),
                new Point(x, y)
            };
            dr.DrawPolygon(new Pen(myColor), triangle);
            dr.FillPolygon(new SolidBrush(body), triangle);
            dr.Dispose();
        }
        public override void clear(PictureBox pb)
        {
            if (f) return;
            Graphics dr = pb.CreateGraphics();
            PointF[] triangle =
            {
                new Point(x, y),
                new Point(x+olD/2, y+olD),
                new Point(x+olD/2, y+olD),
                new Point(x-olD/2, y+olD),
                new Point(x-olD/2, y+olD),
                new Point(x, y)
            };
            dr.DrawPolygon(new Pen(Color.White), triangle);
            dr.FillPolygon(new SolidBrush(Color.White), triangle);
            dr.Dispose();
        }
        public override void changeColor(Color newColor)
        {
            body = newColor;
        }
        public override void reSize(int newSize)
        {
            olD = D;
            D = newSize;
        }
        public override void chose()
        {
            if (!f) { myColor = Color.Black; } else { myColor = Color.Red; }
            f = !f;
        }
        public override bool gFl()
        {
            return f;
        }
        public override bool isHit(int _x, int _y)
        {
            if ((x - _x) * (x - _x) + (y - _y) * (y - _y) <= D * D / 4)
            {
                return true;
            }
            return false;
        }
        public override void move(int dx, int dy, PictureBox obj){
            Graphics dr = obj.CreateGraphics();
            dr.DrawRectangle(new Pen(Color.White, 10), x-D/2, y, D, D);
            SolidBrush fil = new SolidBrush(Color.White);
            dr.FillRectangle(fil, x - D / 2, y, D, D);
            dr.Dispose();
            x = dx;
            y = dy;
        }
        public override void save(String path)
        {
            string body = 
                "x = " + x.ToString()+" "+
                "y = " + y.ToString();
            string text = "3-Triangle "  + body +  "\n";
            File.AppendAllText(path, text);
        }
    }
}
