using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test1.Scripts
{
    public class WalkToHerbAction : GoapAction
    {
        public GameObject Target;
        public float MoveSpeed = 5f;

        private Farmer _farmer;

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            var herb = FindObjectOfType<Herb>();

            if (herb == null)
            {
                Debug.Log("farmer: cant find herb");
                return false;
            }

            Target = herb.gameObject;
            agent.AddActionData("targetHerb", Target);

            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _farmer = agent.GetComponent<Farmer>();
            //Debug.Log("walk enter");
            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            if (Target == null)
            {
                Debug.Log("farmer: target disappear while walking");
                return false;
            }

            if (_farmer.MoveTo(Target.transform.position, MoveSpeed))
            {
                RunOver();
            }
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            //Debug.Log("walk exit");
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
                new KeyValuePair<string, object>("nearHerb", true)
            };
        }
    }
}