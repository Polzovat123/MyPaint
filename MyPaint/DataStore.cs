using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class DataStore
    {
        private int size = 0;
        private int now = 0;
        private int indexL = 0;
        private Point nowP = null;
        private Point last = null, start = null;
        private bool fl = true;
        public DataStore()
        {
            Console.WriteLine("Create Data Store");
            last = null;
        }
        public void Add(Object element)
        {
            bool mustch = false;
            if (last == null)
            {
                mustch = true;
            }
            last = new Point(element, last, null);
            if (mustch) start = last;
            if (last.father != null)
            {
                (last.father).child = last;
            }
            size++;
        }
        public Object get(int index)
        {
            if (index >= size)
                return null;
            now = size - 1;
            nowP = last;
            while (true)
            {
                if (now != index)
                {
                    nowP = nowP.father;
                    now--;
                }
                else
                {
                    return nowP.data;
                }
            }
        }
        public Object getD(int index)
        {
            if (index >= size)
                return null;
            now = size - 1;
            nowP = last;
            while (true)
            {
                if (now != index)
                {
                    nowP = nowP.father;
                    now--;
                }
                else
                {
                    Object ans = nowP.data;
                    if (index > 0)
                        (nowP.father).child = nowP.child;
                    if (index < (size - 1))
                        (nowP.child).father = nowP.father;
                    size--;
                    return ans;
                }
            }
        }
        public int s()
        {
            return size;
        }
        public bool isEmpty()
        {
            if (size == 0) return true;
            return false;
        }
        public void del(int index)
        {
            if (index >= size || index < 0)
                return;
            now = 0;
            nowP = start;
            while (true)
            {
                if (now != index)
                {
                    nowP = nowP.child;
                    now++;
                }
                else
                {
                    if (index > 0)
                        (nowP.father).child = nowP.child;
                    if (index < (size - 1) && nowP != null)
                        (nowP.child).father = nowP.father;
                    if (index == 0)
                    {
                        start = (nowP.child);
                    }
                    if (index == size - 1)
                    {
                        last = nowP.father;
                    }
                    size--;
                    return;
                }
            }
        }
        public void clear()
        {
            int f = size - 1;
            for (int i = 0; i < f; i++)
            {
                del(0);
            }
        }
        public void first()
        {
            nowP = start;
            fl = true;
            indexL = 0;
        }
        public void next()
        {
            if (nowP.child != null)
            {
                nowP = nowP.child;
                indexL++;
            }
            else
                fl = false;
        }
        public bool need()
        {
            if (nowP == null) return false;
            return fl;
        }
        public Object GET()
        {
            if (nowP == null)
            {
                Console.WriteLine("nowP is a null");
                return null;
            }
            return nowP.data;
        }
        public int getIndex()
        {
            return indexL;
        }
        private class Point
        {
            public Object data;
            public Point father = null, child = null;
            public Point(Object _data, Point _father, Point _child)
            {
                data = _data;
                father = _father;
                child = _child;
            }
            ~Point()
            {
            }
        }
    }
}
