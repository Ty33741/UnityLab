using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    // RepeatUntilFail reprocess Child until Child return failure
    // if Child return success/running, RUF return running, else RUF return failure
    public class RepeatUntilFail : Decorator
    {
        public RepeatUntilFail()
        {
            Update = () =>
            {
                Status s = Child.Tick();

                if (s != Status.Failure)
                {
                    return Status.Running;
                }

                return Status.Failure;
            };
        }
    }
}
