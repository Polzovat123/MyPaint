using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class FactoryDrawElements
    {
        public FactoryDrawElements(){
        }
        public CShape createObject(int code, int x, int y, int DS){
            switch (code)
            {
                case 1:
                    return new CCircle(x, y, DS);
                case 2:
                    return new CSquare(x, y, DS);
                case 3:
                    return new CTriangle(x, y, DS);
                default:
                    return null;
            }
        }
        public CShape loadObject(int code, String infObject){
            switch (code)
            {
                case 1:
                    return new CCircle(infObject);
                case 2:
                    return new CSquare(infObject);
                case 3:
                    return new CTriangle(infObject);
                default:
                    return null;
            }
        }
    }
}
