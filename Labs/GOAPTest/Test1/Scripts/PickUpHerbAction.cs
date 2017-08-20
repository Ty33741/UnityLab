using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test1.Scripts
{
    public class PickUpHerbAction : GoapAction
    {
        public GameObject Target;
        public float Duration = 1f;

        private float _time;
        private ProgressBar _bar;
        private Farmer _farmer;

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _time = 0;
            Target = agent.SearchActionData("targetHerb") as GameObject;
            _bar = agent.GetComponent<ProgressBar>();
            _farmer = agent.GetComponent<Farmer>();
            //Debug.Log("pick up enter");
            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            if (Target == null)
            {
                Debug.Log("farmer: target disappear while picking");
                return false;
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
            _farmer.ToolCount--;
            _farmer.HerbCount++;
            Destroy(Target);
            //Debug.Log("pick up exit");
            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", true),
                new KeyValuePair<string, object>("nearHerb", true),
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectHerb", true),
            };
        }
    }
}