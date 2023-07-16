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

        private List<BasicTool> history;

        public int Length
        {
            get { return history.Count; }
        }

        public History() { history = new List<BasicTool>();}

        public void Add(BasicTool basicTool) {
            if (history.Count > MaxHistoryLen) history.RemoveAt(0);
            history.Add(basicTool); 
        }

        public BasicTool Remove()
        {
            BasicTool tmp = history.Last();
            history.RemoveAt(history.Count - 1);
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
