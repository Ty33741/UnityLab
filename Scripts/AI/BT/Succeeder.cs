using System;

namespace Assets.Scripts.AI.BT
{
    // Succeeder change failure to success, failure/running will not be changed
    public class Succeeder : Decorator
    {
        public Succeeder()
        {
            Update = () =>
            {
                Status s = Child.Tick();

                if (s == Status.Running)
                {
                    return Status.Running;
                }

                return Status.Success;
            };
        }
    }
}
