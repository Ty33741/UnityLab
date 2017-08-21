using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FastPickupFoodAction : PickupFoodAction
    {
        protected override bool Exit(GoapAgent agent)
        {
            GetComponent<Farmer>().DestroyTool();

            return base.Exit(agent);
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findFood", true),
                new KeyValuePair<string, object>("hasTool", true)
            };
        }
    }
}
