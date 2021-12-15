using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    abstract class CShape
    {
        public abstract void draw(PictureBox pb);
        public abstract void chose();
        public abstract bool gFl();
        public abstract void changeColor(Color newColor);
        public abstract void reSize(int newSize);
        public abstract bool isHit(int _x, int _y);
        public abstract void move(int dx, int dy);
        public abstract bool inScreen(int dx, int dy, int height, int width);
        public abstract bool inScreen(int newSize, int height, int width);
    }
    class SShape : CShape
    {
        protected int x = 400;
        protected int y = 300;
        protected int D = 40;
        protected bool f = true;
        protected Color body = Color.Blue;
        protected Color myColor = Color.Black;

        
        public override bool isHit(int _x, int _y)
        {
            return true;
        }
        public override bool inScreen(int dx, int dy, int height, int width)
        {
            return true;
        }
        public override bool inScreen(int newSize, int height, int width)
        {
            throw new NotImplementedException();
        }
        public override void draw(PictureBox pb){}
        //Simple part
        public override void move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
        public override bool gFl()
        {
            return f;
        }
        public override void chose(){
            if (!f) { myColor = Color.Black; } else { myColor = Color.Red; }
            f = !f;
        }
        public override void changeColor(Color newColor){
            body = newColor;
        }
        public override void reSize(int newSize)
        {
            D = newSize;
        }
    }
    //1 123 123 123
    class CCircle : SShape
    {
        
        public CCircle(int _x, int _y, int _D)
        {
            x = _x;
            y = _y;
            D = _D;
        }
        public override void draw(PictureBox pb)
        {
            Graphics dr = pb.CreateGraphics();
            dr.DrawEllipse(new Pen(myColor), x, y, D, D);
            dr.FillEllipse(new SolidBrush(body), x, y, D, D);
            dr.Dispose();
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
        public override bool inScreen(int dx, int dy, int height, int width)
        {
            if (x + dx < 0 || y + dy < 0) return false;
            if (x + dx + D >height || y + dy + D > width) return false;
            return true;
        }
        public override bool inScreen(int newSize, int height, int width)
        {
            if (x + newSize > height || y + newSize > width) return false;
            return true;
        }
    }
    class CSquare : SShape {
        public CSquare(int _x, int _y, int _D)
        {
            x = _x;
            y = _y;
            D = _D;
        }
        public override void draw(PictureBox pb)
        {
            Graphics dr = pb.CreateGraphics();
            dr.DrawRectangle(new Pen(myColor), x, y, D, D);
            dr.FillRectangle(new SolidBrush(body), x, y, D, D);
            dr.Dispose();
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
        public override bool inScreen(int dx, int dy, int height, int width)
        {
            if (x + dx < 0 || y + dy < 0) return false;
            if (x + dx + D > height || y + dy + D > width) return false;
            return true;
        }
        public override bool inScreen(int newSize, int height, int width)
        {
            if (x + newSize > height || y + newSize > width) return false;
            return true;
        }
    }
    class CTriangle : SShape
    {
        public CTriangle(int _x, int _y, int _D)
        {
            D = _D;
            x = _x + D/2;
            y = _y;
        }
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
        public override bool isHit(int _x, int _y)
        {
            if ((x - _x) * (x - _x) + (y - _y) * (y - _y) <= D * D / 4)
            {
                return true;
            }
            return false;
        }
        public override bool inScreen(int dx, int dy, int height, int width)
        {
            if (x + dx -D/2 < 0 || y + dy < 0) return false;
            if (x + dx + D/2 > height || y + dy + D > width) return false;
            return true;
        }
        public override bool inScreen(int newSize, int height, int width)
        {
            if (x + newSize < 0 || y + newSize < 0) return false;
            if (x + newSize > height || y + newSize > width) return false;
            return true;
        }
    }
}
