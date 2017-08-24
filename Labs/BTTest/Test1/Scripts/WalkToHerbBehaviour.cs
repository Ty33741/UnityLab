using Assets.Scripts.AI.BT;
using UnityEngine;

namespace Assets.Labs.BTTest.Test1.Scripts
{
    public class WalkToHerbBehaviour : Leaf
    {
        private Herb _target;

        public WalkToHerbBehaviour(Farmer farmer)
        {
            //Initialize = () =>
            //{
            //    _target = null;
            //};

            Update = () =>
            {
                if (_target == null)
                {
                    _target = null;
                    foreach (var keyValuePair in farmer.Data)
                    {
                        if (keyValuePair.Key.Equals("target"))
                        {
                            _target = keyValuePair.Value as Herb;
                            break;
                        }
                    }
                    if (_target == null)
                    {
                        return Status.Failure;
                    }
                }

                if (!farmer.MoveTo(_target.transform.position, farmer.MoveSpeed))
                {
                    return Status.Running;
                }

                return Status.Success;
            };

            Terminate = () =>
            {
                _target = null;
            };
        }
    }
}
