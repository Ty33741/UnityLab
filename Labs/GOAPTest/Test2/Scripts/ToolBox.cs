using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class ToolBox : Box
    {
        public void AddTool(int x)
        {
            AddItem(x);
        }

        public bool ConsumeTool(int x)
        {
            return ConsumeItem(x);
        }
    }
}

