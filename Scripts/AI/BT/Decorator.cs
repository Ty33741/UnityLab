using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class Decorator : BtBehaviour
    {
        protected BtBehaviour Child { get; set; }

        public Decorator()
        {
            Child = null;

            Initialize = () => { };
            Update = () => Status.Running;
            Terminate = () => { };
        }

        public T SetChild<T>(T node) where T : BtBehaviour
        {
            node.Parent = this;
            Child = node;
            return node;
        }
    }
}
