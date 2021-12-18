using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    interface ObserveInt{
        void onDataStoreChange();
    }
    class CTreeView : ObserveInt{
        TreeView obj;//himself
        DataStore arr;//which we subscribe
        public CTreeView(TreeView _h, DataStore _arr) {
            obj = _h;
            arr = _arr;
        }
        public void onDataStoreChange(){
            obj.Nodes.Add(new TreeNode("Circle"));
        }
        public void choseOneVerticle(int chose){
            for (arr.first(); arr.need(); arr.next()) {
                CShape a = (CShape)arr.GET();
                if (!a.gFl()) a.chose();
            }
            CShape chosen = (CShape)arr.get(chose);
            chosen.chose();
        }
    }
}
