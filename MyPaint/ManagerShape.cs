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
    class ManagerShape{
        private DataStore dstShape;
        private MyFactory fabric;
        private int dS = 40;
        private int ox = -1, oy = -1;
        //private int chDS = 40;
        public ManagerShape() {
            fabric = new FactoryDrawElements();
            dstShape = new DataStore();
        }
        public ManagerShape(DataStore arr) {
            dstShape = arr;
            fabric = new FactoryDrawElements();
        }
        public void draw(PictureBox back) {
            if (dstShape!=null && dstShape.s()>0) {
                for (dstShape.first(); dstShape.need(); dstShape.next()){
                    ((CShape)dstShape.GET()).draw(back);
                }
            }
        }
        public void choseElementUsePoint(int x, int y) {
            if (dstShape != null && dstShape.s() > 0){
                for (dstShape.first(); dstShape.need(); dstShape.next()){
                    if (((CShape)dstShape.GET()).isHit(x, y)){
                        ((CShape)dstShape.GET()).chose();
                    }
                }
            }
        }
        public void createNewElement(int codeShape, int x, int y) {
            if (codeShape != -1) {
                dstShape.Add(
                    fabric.createObject(codeShape, x-dS/2, y-dS/2, dS
                    ));
            }
        }
        public void resizeForAllChosenElements(int W, int H, int chDS) {
            for (dstShape.first(); dstShape.need(); dstShape.next())
            {
                SShape obj = ((SShape)(dstShape.GET()));
                if (!obj.gFl() && obj.inScreen(0, 0, W, H))
                {
                    obj.reSize(chDS, H, W);
                }
            }
        }
        public void moveAllChosenElement(int dx, int dy, int W, int H) {
            for (dstShape.first(); dstShape.need(); dstShape.next()){
                SShape obj = ((SShape)(dstShape.GET()));
                if (!obj.gFl() && obj.inScreen(dx, dy, W, H)){
                    obj.move(dx, dy, H, W);
                }
            }
        }
        public void saveData(String path) {
            File.WriteAllText(path, "");
            for (dstShape.first(); dstShape.need(); dstShape.next())
            {
                ((CShape)dstShape.GET()).save(path);
            }
        }
        public void uploadFile(String path, PictureBox pictureBox1) {
            String line;
            try
            {
                StreamReader sr = new StreamReader(path);

                line = sr.ReadLine();
                Stack<CGroup> last = new Stack<CGroup>();
                CShape elem = null;
                while (line != null)
                {
                    CShape newFigure = fabric.loadFile(line[0] - '0');
                    if (line[0] != '{' && line[0] != '}') newFigure.load(line.Substring(2));
                    if (line[0] == '{')
                    {
                        last.Push((CGroup)newFigure);
                        elem = (CGroup)newFigure;
                        line = sr.ReadLine();
                        continue;
                    }
                    if (line[0] == '}')
                    {
                        if (last.Count() > 1)
                        {
                            last.Pop();
                            CGroup elem2 = last.Pop();
                            elem2.Add(elem);
                            last.Push(elem2);
                            elem = elem2;
                        }
                        else
                        {
                            last.Pop();
                            dstShape.Add(elem);
                            elem.draw(pictureBox1);
                        }
                        line = sr.ReadLine();
                        continue;
                    }
                    if (last.Count() > 0)
                    {
                        ((CGroup)elem).Add(newFigure.copy());
                    }
                    if (last.Count() == 0)
                    {
                        dstShape.Add(newFigure);
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public void changeColor(int R, int G, int B) {
            for (dstShape.first(); dstShape.need(); dstShape.next()){
                if (!((CShape)(dstShape.GET())).gFl()){
                    Color clr = Color.FromArgb(125, R, G, B);
                    ((CShape)(dstShape.GET())).changeColor(clr);
                }
            }
        }
        public void deleteChosen() {
            int now = 0;
            for (dstShape.first(); dstShape.need(); dstShape.next()){
                if (!((CShape)(dstShape.GET())).gFl()){
                    dstShape.del(now);
                    now--;
                }
                now++;
            }
        }
        public void Group() {
            CGroup newGroup;
            newGroup = (CGroup)fabric.createObject(4, 0, 0, 0);
            int u = 0;
            for (dstShape.first(); dstShape.need(); dstShape.next()){
                CShape sh = (CShape)dstShape.GET();
                if (!sh.gFl()){
                    newGroup.Add(sh.copy());
                    dstShape.del(u);
                    u--;
                }
                u++;
            }
            dstShape.Add(newGroup);
        }
        public void unGroup() {
            int i = 0;
            DataStore timeObj;
            for (dstShape.first(); dstShape.need(); dstShape.next()){
                CShape obj = ((CShape)dstShape.GET());
                if (!obj.gFl()){
                    timeObj = obj.Des();
                    dstShape.del(i); i--;
                    for (timeObj.first(); timeObj.need(); timeObj.next()){
                        dstShape.Add(timeObj.GET());
                    }
                }
                i++;
            }
        }
    }
}
