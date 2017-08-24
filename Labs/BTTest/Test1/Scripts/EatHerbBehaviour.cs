using Assets.Scripts.AI.BT;
using UnityEngine;

namespace Assets.Labs.BTTest.Test1.Scripts
{
    public class EatHerbBehaviour : Leaf
    {
        private Herb _target;

        public EatHerbBehaviour(Farmer farmer)
        {
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

                GameObject.Destroy(_target.gameObject);
                farmer.Data.Clear();
                _target = null;

                return Status.Success;
            };
        }
    }
}
