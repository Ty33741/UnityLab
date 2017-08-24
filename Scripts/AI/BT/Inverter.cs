using System;
using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    // Inverter invert success and failure, running will not be changed
    public class Inverter : Decorator
    {
        public Inverter()
        {
            Update = () =>
            {
                Status s = Child.Tick();

                switch (s)
                {
                    case Status.Success:
                        return Status.Failure;
                    case Status.Failure:
                        return Status.Success;
                    case Status.Running:
                        return Status.Running;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }
    }
}
