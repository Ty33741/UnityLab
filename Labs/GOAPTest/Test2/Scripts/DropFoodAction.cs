using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class DropFoodAction : MoveAndDoAction
    {
        private FoodBox _foodBox;
        private GameObject _food;

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            _foodBox = FindObjectOfType<FoodBox>();
            if (_foodBox == null)
            {
                return false;
            }

            DestTarget = _foodBox.gameObject;

            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _food = agent.SearchActionData("targetFood") as GameObject;

            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            _food.transform.position = transform.position;

            return base.Run(agent);
        }

        protected override bool Exit(GoapAgent agent)
        {
            _foodBox.AddFood(1);
            Destroy(_food);

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("holdFood", true)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectFood", true)
            };
        }
    }

}

