using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class Leaf : BtBehaviour
    {
        public Leaf()
        {
            Initialize = () => { };
            Update = () => Status.Running;
            Terminate = () => { };
        }
    }
}
