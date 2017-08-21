using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FindFoodAction : MoveAndDoAction
    {
        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            var food = FindObjectOfType<Food>();
            if (food == null)
            {
                return false;
            }

            SetDestination(food.gameObject);
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            agent.AddActionData("targetFood", DestTarget);

            return base.Exit(agent);
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findFood", false)
            };
        }  

    public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findFood", true)
            };
        }
    }
}
