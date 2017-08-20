using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class PickupFoodAction : MoveAndDoAction
    {
        public float Duration = 3f;

        protected ProgressBar _bar;
        protected float _time;

        protected override void Start()
        {
            base.Start();
            _bar = GetComponent<ProgressBar>();
            Debug.Assert(_bar != null);
        }

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

        protected override bool Enter(GoapAgent agent)
        {
            _time = 0;
            
            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            if (!Move())
            {
                return true;
            }

            _time += Time.deltaTime;
            _bar.SetRate(_time / Duration);

            if (_time > Duration)
            {
                RunOver();
            }
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            _bar.SetValue(0);
            agent.AddActionData("targetFood", DestTarget);

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("holdFood", true)
            };
        }
    }
}