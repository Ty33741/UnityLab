using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Scripts
{
    public class PickUpHerbAction : GoapAction
    {
        public float Duration = 1.5f;

        private float _time;


        protected override void SpecificReset()
        {
            _time = 0;
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            Debug.Log("pick up enter");
            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            var target = agent.SearchActionData("targetHerb") as GameObject;
            if (target == null)
            {
                Debug.Log("farmer: target disappear while picking");
                return false;
            }

            _time += Time.deltaTime;
            agent.GetComponent<ProgressBar>().SetRate(_time / Duration);
            if (_time > Duration)
            {
                agent.GetComponent<ProgressBar>().SetValue(0);
                agent.GetComponent<Farmer>().ToolCount--;
                agent.GetComponent<Farmer>().HerbCount++;
                Destroy(target);
                ActionOver();
            }
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            Debug.Log("pick up exit");
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