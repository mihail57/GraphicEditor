using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor
{
    public class History
    {
        private const int MaxHistoryLen = 10;

        private LinkedList<BasicTool> history;

        public int Length
        {
            get { return history.Count; }
        }

        public History() { history = new LinkedList<BasicTool>();}

        public void Add(BasicTool basicTool) {
            if (history.Count > MaxHistoryLen) history.RemoveFirst();
            history.AddLast(basicTool); 
        }

        public BasicTool Remove()
        {
            BasicTool tmp = history.Last();
            history.RemoveLast();
            return tmp;
        }

        public void Show(Graphics g)
        {
            foreach (BasicTool b in history) b.DrawToScreen(g);
        }
        public void Save(Graphics g)
        {
            foreach (BasicTool b in history) b.DrawToImage(g);
        }

        public void Clear() => history.Clear();
    }
}
