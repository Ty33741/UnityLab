using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class Sequence : Composite
    {
        protected int SequenceIndex;

        public Sequence()
        {
            Initialize = () =>
            {
                SequenceIndex = 0;
            };

            Update = Sequencing;

            Terminate = () =>
            {
                if (Status != Status.Running)
                {
                    SequenceIndex = 0;
                }
            };
        }

        protected Status Sequencing()
        {
            while (true)
            {
                Status s = Children[SequenceIndex].Tick();

                if (s != Status.Success)
                {
                    return s;
                }

                if (++SequenceIndex >= Children.Count)
                {
                    return Status.Success;
                }
            }
        }
    }
}
