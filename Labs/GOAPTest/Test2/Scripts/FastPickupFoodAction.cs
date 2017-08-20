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
                new KeyValuePair<string, object>("hasTool", true)
            };
        }
    }
}

/*
using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FastPickupFood : GoapAction
    {
        public float Duration = 1f;

        [SerializeField] private Food _target;
        private Farmer _farmer;
        private float _speed;
        private float _time;

        protected override void SpecificReset()
        {
            _target = null;
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            _target = FindObjectOfType<Food>();
            if (_target == null)
            {
                return false;
            }

            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _time = 0;
            _farmer = agent.GetComponent<Farmer>();
            _speed = agent.GetComponent<Farmer>().MoveSpeed;

            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            if (_farmer.MoveTo(_target.transform.position, _speed))
            {
                return true;
            }

            _time += Time.deltaTime;
            if (_time > Duration)
            {
                RunOver();
            }
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            Destroy(_target);
            _farmer.FoodCount++;

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", true)
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
*/
