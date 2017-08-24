using System;
using Boo.Lang;
using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class BtBehaviour
    {
        public Status Status { get; set; }
        public BtBehaviour Parent { get; set; }

        protected Action Initialize { get; set; }
        protected Func<Status> Update { get; set; }
        protected Action Terminate { get; set; }

        public Status Tick()
        {
            if (Status == Status.Invalid)
            {
                Initialize();
            }

            // status is assigned with any Status except invalid
            Status = Update();

            var s = Status;

            // succeeded or failed...
            if (Status != Status.Running)
            {
                Terminate();
            }

            return Status;  // if return s, then you can change Status in Terminate()
                            // im not sure whether it is a good idea
        }

        //public virtual void Reset()
        //{
        //}
    }
}
