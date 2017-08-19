using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Scripts
{
    public class WalkToHerbAction : GoapAction
    {
        private bool _isDone;

        public GameObject Target;
        public float MoveSpeed = 5f;

        //private void Start()
        //{
        //    AddEffect("nearHerb", true);
        //}

        public override void DoReset()
        {
            _isDone = false;
            Target = null;
        }

        public override bool IsDone()
        {
            return _isDone;
        }

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

        public override bool Perform(GoapAgent agent)
        {
            if (Target == null)
            {
                Debug.Log("farmer: target disappear while walking");
                return false;
            }

            if (agent.GetComponent<Farmer>().MoveTo(Target.transform.position, MoveSpeed))
            {
                _isDone = true;
            }
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