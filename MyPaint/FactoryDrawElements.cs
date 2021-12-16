using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    abstract class MyFactory
    {
        public abstract CShape createObject(int code, int x, int y, int D);
        public abstract CShape loadFile(int code);
    }

    class FactoryDrawElements : MyFactory
    {
        public FactoryDrawElements(){
        }
        
        public override CShape createObject(int code, int x, int y, int DS){
            switch (code)
            {
                case 1:
                    return new CCircle(x, y, DS);
                case 2:
                    return new CSquare(x, y, DS);
                case 3:
                    return new CTriangle(x, y, DS);
                case 4:
                    return new CGroup();
                default:
                    return null;
            }
        }
        public override CShape loadFile(int code)
        {
            switch (code)
            {
                case 1:
                    return new CCircle();
                case 2:
                    return new CSquare();
                case 3:
                    return new CTriangle();
                case '{'-'0':
                    return new CGroup();
                default:
                    return null;
            }
        }
    }
}
