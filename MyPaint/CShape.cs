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
        public abstract void reSize(int newSize, int height, int width);
        public abstract bool isHit(int _x, int _y);
        public abstract void move(int dx, int dy, int width, int height);
        public abstract bool inScreen(int dx, int dy, int height, int width);
        public abstract bool inScreen(int newSize, int height, int width);
        public abstract void load(String path);
        public abstract void save(String path);
        public abstract CShape copy();
        public abstract DataStore Des();
        public virtual void Add(CShape obj) {}
    }
    class CGroup : SShape
    {
        DataStore arr;
        public CGroup()
        {
            arr = new DataStore();
        }
        public CGroup(DataStore ds)
        {
            arr = ds;
        }
        public override void draw(PictureBox pb)
        {
            if(arr!=null&&arr.s()>0)
                for (arr.first(); arr.need(); arr.next()){
                    ((CShape)arr.GET()).draw(pb);
                }
        }
        public override void changeColor(Color newColor)
        {
            if (arr != null && arr.s() > 0)
                for (arr.first(); arr.need(); arr.next())
                {
                    ((CShape)arr.GET()).changeColor(newColor);
                }
        }
        public override void reSize(int newSize, int height, int width){
            if (arr != null && arr.s() > 0) {
                if (checkReSize(newSize, height, width))
                {
                    Console.WriteLine("we got accses");
                    for (arr.first(); arr.need(); arr.next())
                    {
                        ((CShape)arr.GET()).reSize(newSize, height, width);
                    }
                }
            }
        }
        public override void move(int dx, int dy, int width, int height)
        {
            Console.WriteLine(arr.s());
            if (arr != null && arr.s() > 0)
            {
                    for (arr.first(); arr.need(); arr.next()){
                        ((CShape)arr.GET()).move(dx, dy, width, height);
                    }
            }
            
        }
        public override void chose(){
            for (arr.first(); arr.need(); arr.next()) {
                ((CShape)arr.GET()).chose();
            }
        }
        public override bool isHit(int _x, int _y)
        {
            for (arr.first(); arr.need(); arr.next()) {
                if (((CShape)arr.GET()).isHit(_x, _y)) return true;
            }
            return false;
        }
        public override bool gFl(){
            for (arr.first(); arr.need(); arr.next())
            {
                if (((CShape)arr.GET()).gFl()) return true;
            }
            return false;
        }
        private bool checkReSize(int newSize, int width, int height)
        {
            for (arr.first(); arr.need(); arr.next())
            {
                if (!((CShape)arr.GET()).inScreen(newSize, height, width)) return false;
            }
            return true;
        }
        public override bool inScreen(int dx, int dy, int height, int width)
        {
            for (arr.first(); arr.need(); arr.next())
            {
                if (!((CShape)arr.GET()).inScreen(dx, dy, height, width)) return false;
            }
            return true;
        }
        public void Add(CShape obj){
            if (obj.gFl()) obj.chose();
            arr.Add(obj);
        }
        public override CShape copy()
        {
            return new CGroup(arr);
        }
        public override DataStore Des() {
            return arr;
        }
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
        public override void move(int dx, int dy, int height, int width)
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
        public override void reSize(int newSize, int height, int width)
        {
            D = newSize;
        }
        public override void save(string path)
        {
        }
        public override void load(string path){}
        public override DataStore Des() {
            DataStore dataStore = new DataStore();
            dataStore.Add(copy());
            Console.WriteLine(dataStore.s());
            return dataStore;
        }
        public override CShape copy() { return null; }
    }
    
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
        public override CShape copy()
        {
            return new CCircle(x, y, D);
        }
        public DataStore des()
        {
            DataStore dataStore = new DataStore();
            dataStore.Add(copy());
            Console.WriteLine(dataStore.s());
            return dataStore;
        }
        public CCircle(String encode) { 
            //example we got ""
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
        public override CShape copy()
        {
            return new CSquare(x, y, D);
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
        public override CShape copy()
        {
            return new CTriangle(x, y, D);
        }
    }
}
