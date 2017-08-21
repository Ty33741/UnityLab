using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class EatFoodAction : MoveAndDoAction
    {
        private BlackSmith _blackSmith;
        private FoodBox _foodBox;

        protected override void Start()
        {
            _blackSmith = GetComponent<BlackSmith>();
            base.Start();
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            _foodBox = FindObjectOfType<FoodBox>();
            if (_foodBox == null || _foodBox.Count == 0)
            {
                return false;
            }

            SetDestination(_foodBox.gameObject);
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            _blackSmith.EatFood();
            _foodBox.ConsumeFood(1);

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("eatFood", true)
            };
        }
    }
}
