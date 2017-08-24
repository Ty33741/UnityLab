using System.Collections.Generic;
using Assets.Scripts.AI.BT;
using UnityEngine;

namespace Assets.Labs.BTTest.Test1.Scripts
{
    public class FindHerbBehaviour : Leaf
    {
        public FindHerbBehaviour(Farmer farmer)
        {
            Update = () =>
            {
                var herb = GameObject.FindObjectOfType<Herb>();
                if (herb == null)
                {
                    return Status.Failure;
                }

                farmer.Data.Add(new KeyValuePair<string, object>("target", herb));

                return Status.Success;
            };
        }
    }
}
