using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class Selector : Composite
    {
        protected int SelectorIndex;

        public Selector()
        {
            Initialize = () =>
            {
                SelectorIndex = 0;
            };

            Update = Selecting;

            Terminate = () =>
            {
                if (Status != Status.Running)
                {
                    SelectorIndex = 0;
                }
            };
        }

        protected Status Selecting()
        {
            while (true)
            {
                Status s = Children[SelectorIndex].Tick();

                if (s != Status.Failure)
                {
                    return s;
                }

                if (++SelectorIndex >= Children.Count)
                {
                    return Status.Failure;
                }
            }
        }
    }
}
