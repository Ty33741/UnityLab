using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class Composite : BtBehaviour
    {
        protected List<BtBehaviour> Children { get; set; }

        public Composite()
        {
            Children = new List<BtBehaviour>();

            Initialize = () => { };
            Update = () => Status.Running;
            Terminate = () => { };
        }

        public T AddChild<T>(T node) where T : BtBehaviour
        {
            node.Parent = this;
            Children.Add(node);
            return node;
        }
    }
}
