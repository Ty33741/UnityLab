using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    // Repeater reprocess Child N times
    // if Child return running, Repeater return running
    // if Child return success/failure, N=N-1
    //      if N is zero, return Child.Status, else return running
    public class Repeater : Decorator
    {
        protected int RepeatCount;
        protected int RepeatIndex;

        public Repeater(int t)
        {
            Initialize = () =>
            {
                Debug.Assert(t > 0);

                RepeatIndex = 0;
                RepeatCount = t;
            };

            Update = () =>
            {
                Status s = Child.Tick();

                if (s == Status.Running)
                {
                    return Status.Running;
                }

                if (++RepeatIndex >= RepeatCount)
                {
                    return s;
                }

                return Status.Running;
            };

            Terminate = () =>
            {
                if (Status != Status.Running)
                {
                    RepeatIndex = 0;
                }
            };
        }
    }
}
